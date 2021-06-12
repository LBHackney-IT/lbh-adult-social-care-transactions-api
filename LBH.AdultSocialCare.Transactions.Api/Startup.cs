using Amazon.XRay.Recorder.Handlers.AwsSdk;
using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Controllers;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Handlers;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.BillGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.InvoiceGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PayRunGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Concrete;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Concrete;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using LBH.AdultSocialCare.Transactions.Api.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LBH.AdultSocialCare.Transactions.Api.V1.Gateways.DepartmentGateways;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.DepartmentUseCases.Concrete;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.DepartmentUseCases.Interfaces;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Concrete;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;

namespace LBH.AdultSocialCare.Transactions.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AWSSDKHandler.RegisterXRayForAllServices();
        }

        public IConfiguration Configuration { get; }

        private static List<ApiVersionDescription> _apiVersions { get; set; }

        //TODO update the below to the name of your API
        private const string ApiName = "Adult Social Care Transactions API";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddScoped<ModelStateValidationFilterAttribute>();
            services
                .AddMvc(config =>
                {
                    config.ReturnHttpNotAcceptable = true;
                    config.Filters.Add(typeof(ApiExceptionFilter));
                    // config.Filters.Add(typeof(ModelStateValidationFilterAttribute));
                })
                .AddNewtonsoftJson(x
                    => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .ConfigureApiBehaviorOptions(opt => opt.InvalidModelStateResponseFactory = (context => throw new InvalidModelStateException(context.ModelState.AllModelStateErrors(),
                    "There are some validation errors. Please correct and try again")))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true; // assume that the caller wants the default version if they don't specify
                o.ApiVersionReader = new UrlSegmentApiVersionReader(); // read the version number from the url segment header)
            });

            services.AddSingleton<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Token",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Your Hackney API Key",
                        Name = "X-Api-Key",
                        Type = SecuritySchemeType.ApiKey
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Token" }
                        },
                        new List<string>()
                    }
                });

                //Looks at the APIVersionAttribute [ApiVersion("x")] on controllers and decides whether or not
                //to include it in that version of the swagger document
                //Controllers must have this [ApiVersion("x")] to be included in swagger documentation!!
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    apiDesc.TryGetMethodInfo(out var methodInfo);

                    var versions = methodInfo?
                        .DeclaringType?.GetCustomAttributes()
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions).ToList();

                    return versions?.Any(v => $"{v.GetFormattedApiVersion()}" == docName) ?? false;
                });

                //Get every ApiVersion attribute specified and create swagger docs for them
                foreach (var apiVersion in _apiVersions)
                {
                    var version = $"v{apiVersion.ApiVersion.ToString()}";
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = $"{ApiName}-api {version}",
                        Version = version,
                        Description = $"{ApiName} version {version}. Please check older versions for depreciated endpoints."
                    });
                }

                c.CustomSchemaIds(x => x.FullName);
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });

            // Add auto mapper
            services.AddAutoMapper(typeof(Startup));

            ConfigureLogging(services, Configuration);

            ConfigureDbContext(services);

            RegisterGateways(services);
            RegisterUseCases(services);
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                                      Configuration.GetConnectionString("DatabaseConnectionString");

            string assemblyName = Assembly.GetCallingAssembly().GetName().Name;

            services.AddDbContext<DatabaseContext>(opt
                => opt.UseNpgsql(connectionString, b => b.MigrationsAssembly(assemblyName)));
        }

        private static void ConfigureLogging(IServiceCollection services, IConfiguration configuration)
        {
            // We rebuild the logging stack so as to ensure the console logger is not used in production.
            // See here: https://weblog.west-wind.com/posts/2018/Dec/31/Dont-let-ASPNET-Core-Default-Console-Logging-Slow-your-App-down
            services.AddLogging(config =>
            {
                // clear out default configuration
                config.ClearProviders();

                config.AddConfiguration(configuration.GetSection("Logging"));
                config.AddDebug();
                config.AddEventSourceLogger();

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                {
                    config.AddConsole();
                }
            });
        }

        private static void RegisterGateways(IServiceCollection services)
        {
            services.AddScoped<IBillGateway, BillGateway>();
            services.AddScoped<IBillItemGateway, BillItemGateway>();
            services.AddScoped<IBillFileGateway, BillFileGateway>();
            services.AddScoped<IBillStatusGateway, BillStatusGateway>();

            #region Invoices

            services.AddScoped<IInvoiceGateway, InvoiceGateway>();

            #endregion Invoices

            #region PayRuns

            services.AddScoped<IPayRunGateway, PayRunGateway>();

            #endregion PayRuns

            #region Departments

            services.AddScoped<IDepartmentGateway, DepartmentGateway>();

            #endregion Departments
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<ICreateBillAsyncUseCase, CreateBillAsyncUseCase>();
            services.AddScoped<ICreatePayRunUseCase, CreatePayRunUseCase>();
            services.AddScoped<IGetPayRunSummaryListUseCase, GetPayRunSummaryListUseCase>();
            services.AddScoped<IGetUniqueSuppliersInPayRunUseCase, GetUniqueSuppliersInPayRunUseCase>();
            services.AddScoped<IGetReleasedHoldsCountUseCase, GetReleasedHoldsCountUseCase>();
            services.AddScoped<IGetUniquePackageTypesInPayRunUseCase, GetUniquePackageTypesInPayRunUseCase>();
            services.AddScoped<IGetReleasedHoldsUseCase, GetReleasedHoldsUseCase>();
            services.AddScoped<IGetUniqueInvoiceItemPaymentStatusInPayRunUseCase, GetUniqueInvoiceItemPaymentStatusInPayRunUseCase>();
            services.AddScoped<IGetSinglePayRunDetailsUseCase, GetSinglePayRunDetailsUseCase>();
            services.AddScoped<IGetInvoiceItemPaymentStatusesUseCase, GetInvoiceItemPaymentStatusesUseCase>();
            services.AddScoped<IChangePayRunStatusUseCase, ChangePayRunStatusUseCase>();
            services.AddScoped<IReleaseHeldPaymentsUseCase, ReleaseHeldPaymentsUseCase>();
            services.AddScoped<IGetPaymentDepartmentsUseCase, GetPaymentDepartmentsUseCase>();
            services.AddScoped<IInvoicesUseCase, InvoicesUseCase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (IServiceScope appScope = app.ApplicationServices.CreateScope())
            {
                DatabaseContext databaseContext = appScope.ServiceProvider.GetRequiredService<DatabaseContext>();

                // Run pending database migrations
                if (databaseContext.Database.GetPendingMigrations().Any())
                {
                    // Perform migrations
                    databaseContext.Database.Migrate();
                }
            }

            app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());
            app.UseCorrelation();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Configure extension methods to use auto mapper
            IMapper mapper = app.ApplicationServices.GetService<IMapper>();
            ApiToDomainFactory.Configure(mapper);
            DomainToEntityFactory.Configure(mapper);
            EntityToDomainFactory.Configure(mapper);
            ResponseFactory.Configure(mapper);

            // TODO
            // If you DON'T use the renaming script, PLEASE replace with your own API name manually
            app.UseXRay("base-api");

            //Get All ApiVersions,
            IApiVersionDescriptionProvider api = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            _apiVersions = api.ApiVersionDescriptions.ToList();

            // Swagger ui to view the swagger.json file
            app.UseSwaggerUI(c =>
            {
                foreach (ApiVersionDescription apiVersionDescription in _apiVersions)
                {
                    // Create a swagger endpoint for each swagger version
                    c.SwaggerEndpoint($"{apiVersionDescription.GetFormattedApiVersion()}/swagger.json",
                        $"{ApiName}-api {apiVersionDescription.GetFormattedApiVersion()}");
                }
            });
            app.UseSwagger();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // SwaggerGen won't find controllers that are routed via this technique.
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

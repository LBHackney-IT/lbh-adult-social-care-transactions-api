FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./LBH.AdultSocialCare.Transactions.Api.sln ./
COPY ./LBH.AdultSocialCare.Transactions.Api/LBH.AdultSocialCare.Transactions.Api.csproj ./LBH.AdultSocialCare.Transactions.Api/
COPY ./LBH.AdultSocialCare.Transactions.Api.Tests/LBH.AdultSocialCare.Transactions.Api.Tests.csproj ./LBH.AdultSocialCare.Transactions.Api.Tests/

RUN dotnet restore ./LBH.AdultSocialCare.Transactions.Api/LBH.AdultSocialCare.Transactions.Api.csproj
RUN dotnet restore ./LBH.AdultSocialCare.Transactions.Api.Tests/LBH.AdultSocialCare.Transactions.Api.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out LBH.AdultSocialCare.Transactions.Api.Tests/LBH.AdultSocialCare.Transactions.Api.Tests.csproj

CMD dotnet test

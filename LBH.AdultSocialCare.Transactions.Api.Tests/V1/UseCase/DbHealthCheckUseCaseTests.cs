namespace LBH.AdultSocialCare.Transactions.Api.Tests.V1.UseCase
{

    public class DbHealthCheckUseCaseTests
    {

        //private Mock<IHealthCheckService> _mockHealthCheckService;
        //private DbHealthCheckUseCase _classUnderTest;

        //private readonly Faker _faker = new Faker();
        //private string _description;

        //[SetUp]
        //public void SetUp()
        //{
        //    _description = _faker.Random.Words();

        //    _mockHealthCheckService = new Mock<IHealthCheckService>();
        //    CompositeHealthCheckResult compositeHealthCheckResult = new CompositeHealthCheckResult(CheckStatus.Healthy);
        //    compositeHealthCheckResult.Add("test", CheckStatus.Healthy, _description);

        //    _mockHealthCheckService.Setup(s => s.CheckHealthAsync(It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(compositeHealthCheckResult);

        //    _classUnderTest = new DbHealthCheckUseCase(_mockHealthCheckService.Object);
        //}

        //[Test]
        //public void ReturnsResponseWithStatus()
        //{
        //    var response = _classUnderTest.Execute();

        //    response.Should().NotBeNull();
        //    response.Success.Should().BeTrue();
        //    response.Message.Should().BeEquivalentTo("test: " + _description);
        //}

    }

}

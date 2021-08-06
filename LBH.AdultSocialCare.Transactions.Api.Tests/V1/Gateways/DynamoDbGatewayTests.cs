namespace LBH.AdultSocialCare.Transactions.Api.Tests.V1.Gateways
{

    //TODO: Remove this file if DynamoDb gateway not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-base-api/wiki/Running-the-test-suite.
    public class DynamoDbGatewayTests
    {

        //private readonly Fixture _fixture = new Fixture();
        //private Mock<IDynamoDBContext> _dynamoDb;
        //private DynamoDbGateway _classUnderTest;

        //public void Setup()
        //{
        //    _dynamoDb = new Mock<IDynamoDBContext>();
        //    _classUnderTest = new DynamoDbGateway(_dynamoDb.Object);
        //}

        //public void GetEntityByIdReturnsNullIfEntityDoesntExist()
        //{
        //    var response = _classUnderTest.GetEntityById(123);

        //    response.Should().BeNull();
        //}

        //public void GetEntityByIdReturnsTheEntityIfItExists()
        //{
        //    var entity = _fixture.Create<Entity>();
        //    var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

        //    _dynamoDb.Setup(x => x.LoadAsync<DatabaseEntity>(entity.Id, default)).ReturnsAsync(dbEntity);

        //    var response = _classUnderTest.GetEntityById(entity.Id);

        //    _dynamoDb.Verify(x => x.LoadAsync<DatabaseEntity>(entity.Id, default), Times.Once);

        //    entity.Id.Should().Be(response.Id);
        //    entity.CreatedAt.Should().BeSameDateAs(response.CreatedAt);
        //}

    }

}

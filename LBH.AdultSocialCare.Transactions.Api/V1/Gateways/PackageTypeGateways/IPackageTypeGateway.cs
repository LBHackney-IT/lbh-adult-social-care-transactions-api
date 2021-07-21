namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PackageTypeGateways
{
    public interface IPackageTypeGateway
    {
        bool IsValidPackageType(int packageTypeId);
    }
}

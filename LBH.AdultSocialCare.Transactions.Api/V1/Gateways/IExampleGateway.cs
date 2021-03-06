using System.Collections.Generic;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}

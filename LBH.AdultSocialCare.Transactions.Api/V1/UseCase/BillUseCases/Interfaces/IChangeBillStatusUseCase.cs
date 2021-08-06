using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces
{
    public interface IChangeBillStatusUseCase
    {
        Task CheckAndSetBillStatus(long billId);
    }
}

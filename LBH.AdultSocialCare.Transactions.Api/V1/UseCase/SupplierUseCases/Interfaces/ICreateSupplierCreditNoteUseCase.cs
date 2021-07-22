using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces
{
    public interface ICreateSupplierCreditNoteUseCase
    {
        Task CreateSupplierCreditNote(long billId);
    }
}

using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Factories
{
    public static class DomainToEntityFactory
    {
        private static IMapper _mapper { get; set; }

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Bill

        public static Bill ToDb(this BillCreationDomain billCreationDomain)
        {
            return _mapper.Map<Bill>(billCreationDomain);
        }

        #endregion Bill

        #region PayRuns

        public static PayRun ToDb(this PayRunForCreationDomain payRunForCreationDomain)
        {
            var payRunForCreation = _mapper.Map<PayRun>(payRunForCreationDomain);
            payRunForCreation.PayRunItems = new List<PayRunItem>();
            foreach (var invoiceDomain in payRunForCreationDomain.Invoices)
            {
                payRunForCreation.PayRunItems.Add(new PayRunItem
                {
                    PayRunId = payRunForCreation.PayRunId,
                    InvoiceId = invoiceDomain.InvoiceId,
                    InvoiceItemId = null,
                    PaidAmount = 0,
                    RemainingBalance = invoiceDomain.TotalAmount
                });
            }
            payRunForCreation.PayRunStatusId = (int) PayRunStatusesEnum.Draft;
            return payRunForCreation;
        }

        #endregion PayRuns

        #region Supplier

        public static SupplierCreditNote ToDb(this SupplierCreditNotesCreationDomain supplierCreditNotesCreationDomain)
        {
            return _mapper.Map<SupplierCreditNote>(supplierCreditNotesCreationDomain);
        }

        #endregion Supplier

        #region Invoices

        public static DisputedInvoice ToDb(this DisputedInvoiceForCreationDomain disputedInvoiceForCreationDomain)
        {
            var entity = _mapper.Map<DisputedInvoice>(disputedInvoiceForCreationDomain);
            entity.DisputedInvoiceChats = new List<DisputedInvoiceChat>
            {
                new DisputedInvoiceChat
                {
                    DisputedInvoiceId = entity.DisputedInvoiceId,
                    MessageRead = false,
                    Message = entity.ReasonForHolding,
                    ActionRequiredFromId = entity.ActionRequiredFromId
                }
            };
            return entity;
        }

        public static Invoice ToDb(this InvoiceForCreationDomain invoiceForCreationDomain)
        {
            var entity = _mapper.Map<Invoice>(invoiceForCreationDomain);
            return entity;
        }

        #endregion Invoices
    }
}

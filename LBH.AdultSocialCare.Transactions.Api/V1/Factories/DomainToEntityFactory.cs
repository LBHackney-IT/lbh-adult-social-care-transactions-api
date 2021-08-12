using System;
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
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.LedgerDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Factories
{
    public static class DomainToEntityFactory
    {
        private static IMapper _mapper;

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Bill

        public static Bill ToDb(this BillCreationDomain billCreationDomain)
        {
            return _mapper.Map<Bill>(billCreationDomain);
        }

        public static BillPayment ToDb(this BillPaymentDomain billPaymentDomain)
        {
            return _mapper.Map<BillPayment>(billPaymentDomain);
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

        public static IEnumerable<Invoice> ToDb(this IEnumerable<InvoiceForCreationDomain> invoiceForCreationDomain)
        {
            return _mapper.Map<IEnumerable<Invoice>>(invoiceForCreationDomain);
        }

        public static DisputedInvoiceChat ToDb(this DisputedInvoiceChatForCreationDomain disputedInvoiceChatForCreationDomain, Guid disputedInvoiceId)
        {
            var entity = _mapper.Map<DisputedInvoiceChat>(disputedInvoiceChatForCreationDomain);
            entity.DisputedInvoiceId = disputedInvoiceId;
            entity.MessageRead = false;
            return entity;
        }

        #endregion Invoices

        #region Ledger

        public static Ledger ToDb(this LedgerDomain ledgerDomain)
        {
            return _mapper.Map<Ledger>(ledgerDomain);
        }

        #endregion

        #region SupplierReturn

        public static SupplierReturnItemDisputeConversation ToDb
            (this SupplierReturnItemDisputeConversationCreationDomain supplierReturnItemDisputeConversationCreationDomain)
        {
            supplierReturnItemDisputeConversationCreationDomain.DateCreated = DateTimeOffset.Now;
            supplierReturnItemDisputeConversationCreationDomain.MessageRead = false;
            return _mapper.Map<SupplierReturnItemDisputeConversation>(supplierReturnItemDisputeConversationCreationDomain);
        }

        #endregion SupplierReturn
    }
}

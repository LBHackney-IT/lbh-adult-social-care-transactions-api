using AutoMapper;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using System;
using System.Collections.Generic;
using System.Net;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Factories
{
    public static class ApiToDomainFactory
    {
        private static IMapper _mapper { get; set; }

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static BillCreationDomain ToDomain(this BillCreationRequest billCreationRequest)
        {
            var res = _mapper.Map<BillCreationDomain>(billCreationRequest);
            return res;
        }

        #region ReleaseHeldInvoiceItems

        public static ReleaseHeldInvoiceItemDomain ToDomain(this ReleaseHeldInvoiceItemRequest releaseHeldInvoiceItemRequest)
        {
            var res = _mapper.Map<ReleaseHeldInvoiceItemDomain>(releaseHeldInvoiceItemRequest);
            return res;
        }

        public static IEnumerable<ReleaseHeldInvoiceItemDomain> ToDomain(this IEnumerable<ReleaseHeldInvoiceItemRequest> releaseHeldInvoiceItemRequests)
        {
            var res = _mapper.Map<IEnumerable<ReleaseHeldInvoiceItemDomain>>(releaseHeldInvoiceItemRequests);
            return res;
        }

        #endregion ReleaseHeldInvoiceItems

        #region Invoices

        public static DisputedInvoiceForCreationDomain ToDomain(this DisputedInvoiceForCreationRequest disputedInvoiceForCreationRequest)
        {
            var res = _mapper.Map<DisputedInvoiceForCreationDomain>(disputedInvoiceForCreationRequest);
            return res;
        }

        public static InvoiceForCreationDomain ToDomain(this InvoiceForCreationRequest invoiceForCreationRequest)
        {
            if (invoiceForCreationRequest.InvoiceItems.IsNullOrEmpty<InvoiceItemForCreationRequest>())
            {
                throw new ApiException("Invoice cannot be created without invoice items",
                    (int) HttpStatusCode.UnprocessableEntity);
            }
            var res = _mapper.Map<InvoiceForCreationDomain>(invoiceForCreationRequest);
            return res;
        }

        public static DisputedInvoiceChatForCreationDomain ToDomain(this DisputedInvoiceChatForCreationRequest disputedInvoiceChatForCreationRequest, Guid payRunId)
        {
            var res = _mapper.Map<DisputedInvoiceChatForCreationDomain>(disputedInvoiceChatForCreationRequest);
            res.PayRunId = payRunId;
            return res;
        }

        #endregion Invoices

        public static BillItemCreationDomain ToDomain(this BillItemCreationRequest billItemCreationRequest)
        {
            var res = _mapper.Map<BillItemCreationDomain>(billItemCreationRequest);
            return res;
        }

        #region SupplierReturn

        public static SupplierReturnItemDisputeConversationCreationDomain ToDomain(this SupplierReturnItemDisputeConversationCreationRequest supplierReturnItemDisputeConversationCreationRequest)
        {
            return _mapper.Map<SupplierReturnItemDisputeConversationCreationDomain>(supplierReturnItemDisputeConversationCreationRequest);
        }

        #endregion SupplierReturn
    }
}

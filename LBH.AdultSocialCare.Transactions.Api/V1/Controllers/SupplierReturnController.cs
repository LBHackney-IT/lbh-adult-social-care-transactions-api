using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierReturnUseCases.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/supplier-returns")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class SupplierReturnController : BaseController
    {
        private readonly IAcceptAllSupplierReturnPackageItemsUseCase _acceptAllSupplierReturnPackageItemsUseCase;
        private readonly IChangeSupplierReturnPackageValuesUseCase _changeSupplierReturnPackageValuesUseCase;
        private readonly ICreateDisputeItemChatUseCase _createDisputeItemChatUseCase;
        private readonly IDisputeAllSupplierReturnPackageItemsUseCase _disputeAllSupplierReturnPackageItemsUseCase;
        private readonly IGetDisputeItemChatUseCase _getDisputeItemChatUseCase;
        private readonly IGetSingleSupplierReturnInsightsUseCase _getSingleSupplierReturnInsightsUseCase;
        private readonly IMarkDisputeItemChatUseCase _markDisputeItemChatUseCase;

        public SupplierReturnController(IAcceptAllSupplierReturnPackageItemsUseCase acceptAllSupplierReturnPackageItemsUseCase,
            IChangeSupplierReturnPackageValuesUseCase changeSupplierReturnPackageValuesUseCase,
            ICreateDisputeItemChatUseCase createDisputeItemChatUseCase,
            IDisputeAllSupplierReturnPackageItemsUseCase disputeAllSupplierReturnPackageItemsUseCase,
            IGetDisputeItemChatUseCase getDisputeItemChatUseCase,
            IGetSingleSupplierReturnInsightsUseCase getSingleSupplierReturnInsightsUseCase,
            IMarkDisputeItemChatUseCase markDisputeItemChatUseCase)
        {
            _acceptAllSupplierReturnPackageItemsUseCase = acceptAllSupplierReturnPackageItemsUseCase;
            _changeSupplierReturnPackageValuesUseCase = changeSupplierReturnPackageValuesUseCase;
            _createDisputeItemChatUseCase = createDisputeItemChatUseCase;
            _disputeAllSupplierReturnPackageItemsUseCase = disputeAllSupplierReturnPackageItemsUseCase;
            _getDisputeItemChatUseCase = getDisputeItemChatUseCase;
            _getSingleSupplierReturnInsightsUseCase = getSingleSupplierReturnInsightsUseCase;
            _markDisputeItemChatUseCase = markDisputeItemChatUseCase;

        }

        /// <summary>Accept all the supplier return package items</summary>
        /// <param name="supplierReturnId">The supplier return id identifier.</param>
        /// <response code="200"></response>
        /// <response code="404">If the supplier return id is not found</response>
        [HttpPut("{supplierReturnId}/accept-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AcceptAllSupplierReturnPackageItems(Guid supplierReturnId)
        {
            await _acceptAllSupplierReturnPackageItemsUseCase.AcceptAllSupplierReturnPackageItems(supplierReturnId).ConfigureAwait(false);
            return Ok();
        }

        [HttpPut("{supplierReturnId}/dispute-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DisputeAllSupplierReturnPackageItems(Guid supplierReturnId, [FromBody] string message)
        {
            await _disputeAllSupplierReturnPackageItemsUseCase.DisputeAllSupplierReturnPackageItems(supplierReturnId, message).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>Update the supplier return package Values in the supplier return</summary>
        /// <param name="supplierReturnPackageValuesRequest">The supplier return package values</param>
        /// <response code="200"></response>
        /// <response code="404">If the supplier return id is not found</response>
        [HttpPut("change-package-values")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangeSupplierReturnPackageValues([FromBody] ChangeSupplierReturnPackageValuesRequest supplierReturnPackageValuesRequest)
        {
            await _changeSupplierReturnPackageValuesUseCase.ChangeSupplierReturnPackageValues(supplierReturnPackageValuesRequest.SupplierReturnId, supplierReturnPackageValuesRequest.SupplierReturnItemId, supplierReturnPackageValuesRequest.HoursDelivered,
                supplierReturnPackageValuesRequest.ActualVisits, supplierReturnPackageValuesRequest.Comment).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("dispute-chat")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateDisputeChat([FromBody] SupplierReturnItemDisputeConversationCreationRequest supplierReturnItemDisputeConversationCreationRequest)
        {
            await _createDisputeItemChatUseCase.CreateDisputeChat(supplierReturnItemDisputeConversationCreationRequest.ToDomain()).ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("dispute-chat")]
        [ProducesResponseType(typeof(IEnumerable<SupplierReturnItemDisputeConversationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<SupplierReturnItemDisputeConversationResponse>> GetDisputeItemChat([FromBody] Guid packageId, Guid supplierReturnItemId)
        {
            var result = await _getDisputeItemChatUseCase.GetDisputeItemChat(packageId, supplierReturnItemId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{suppliersReturnsId}/single-insight")]
        [ProducesResponseType(typeof(IEnumerable<SupplierReturnItemDisputeConversationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<SupplierReturnItemDisputeConversationResponse>> GetSingleSupplierReturnInsights(Guid suppliersReturnsId)
        {
            var result = await _getSingleSupplierReturnInsightsUseCase.GetSingleSupplierReturnInsights(suppliersReturnsId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPut("mark-dispute-item-chat")]
        [ProducesResponseType(typeof(IEnumerable<SupplierReturnItemDisputeConversationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> MarkDisputeItemChat([FromBody] Guid packageId, Guid supplierReturnItemId, Guid disputeConversationId, bool messageRead)
        {
            await _markDisputeItemChatUseCase.MarkDisputeItemChat(packageId, supplierReturnItemId, disputeConversationId, messageRead).ConfigureAwait(false);
            return Ok();
        }


    }
}

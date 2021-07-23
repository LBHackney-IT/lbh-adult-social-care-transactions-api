using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request
{
    public class PayRunHeldChatForCreationRequest
    {
        [Required] public Guid PayRunId { get; set; }
        [Required] public Guid PackageId { get; set; }
        [Required] public string Message { get; set; }
    }
}

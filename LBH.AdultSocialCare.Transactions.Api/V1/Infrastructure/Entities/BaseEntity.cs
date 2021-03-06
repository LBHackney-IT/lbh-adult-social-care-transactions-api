using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreated = DateTimeOffset.UtcNow;
            DateUpdated = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Gets or sets the date added.
        /// </summary>
        // public DateTimeOffset DateCreated { get; private set; }
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }
    }
}

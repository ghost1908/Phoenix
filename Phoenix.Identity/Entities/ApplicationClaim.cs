using System;

namespace Phoenix.Identity.Entities
{
    public class ApplicationClaim
    {
        public virtual int Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
    }
}

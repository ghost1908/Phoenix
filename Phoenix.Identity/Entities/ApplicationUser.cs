using System;
using System.Security.Principal;

namespace Phoenix.Identity.Entities
{
    public class ApplicationUser : IIdentity
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public virtual string UserName { get; set; }
        public virtual string NormalizedUserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string NormalizedEmail { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; } = null;
        public virtual string ConcurrencyStamp { get; set; } = null;
        public virtual string PhoneNumber { get; set; } = null;
        public virtual bool PhoneNumberConfirmed { get; set; } = true;
        public virtual bool TwoFactorEnabled { get; set; } = false;
        public virtual DateTimeOffset? LockoutEnd { get; set; }
        public virtual bool LockoutEnabled { get; set; } = true;
        public virtual int AccessFailedCount { get; set; } = 0;
        
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
    }
}

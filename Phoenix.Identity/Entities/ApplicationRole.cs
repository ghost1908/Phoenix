using System;

namespace Phoenix.Identity.Entities
{
    public class ApplicationRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}

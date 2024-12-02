using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities
{
    public class ObjectAccess
    {
        public Guid Id { get; set; }
        public string ObjectName { get; set; }
        public string ObjectDisplayName { get; set; }
        public string ObjectType { get; set; }
    }
}

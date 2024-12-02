using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities
{
    public class UserAccess
    {
        public Guid UserId { get; set; }
        public Guid ObjectId { get; set; }
        public object ObjectValue { get; set; }
    }
}

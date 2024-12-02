using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IBaseRepository
    {
        Guid UserID { get; set; }
    }
}

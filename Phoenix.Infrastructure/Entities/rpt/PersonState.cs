using Microsoft.SqlServer.Types;

namespace Phoenix.Infrastructure.Entities
{
    public class PersonState
    {
        public SqlHierarchyId Level { get; set; }
        public string Name { get; set; }
        public long Count { get; set; }
        public string Style { get; set; }
    }
}

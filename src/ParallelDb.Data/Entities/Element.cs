using System.Collections.Generic;
using System.Linq;

namespace ParallelDb.Data.Entities
{
    public class Element : EntityBase
    {
        public ICollection<DepElement> DepElements { get; set; }
    }
}

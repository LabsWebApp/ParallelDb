using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ParallelDb.Data.Entities
{
    public class DepElement : EntityBase
    {
        [ForeignKey("Element")]
        public int? ElementId { get; set; }
        public Element Element { get; set; }

        public DepElement() { }
        public DepElement(IEnumerable<Element> elements) : base(true)
        {
            if (R.Next(0, 3) > 0)
                ElementId = elements?.Select(e => e.Id).Shuffle(R).FirstOrDefault();
        }
    }
}

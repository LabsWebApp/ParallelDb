using System;
using System.Collections.Generic;

namespace ParallelDb.Data.Entities
{
    public class DepElement : EntityBase
    {
        public int? ElementId { get; set; }
       // [ForeignKey("Element")]
        public Element Element { get; set; }
        
        public DepElement() : base() { }
        private DepElement(Random r) : base(r) { }

        public static DepElement CreateInstance(Random r, IList<int> elementIds)
        {
            DepElement res = new(r);
            if (elementIds is not null)
            {
                if (res.R.Next(0, 3) > 0)
                {
                    var id = elementIds[r.Next(elementIds.Count)];
                    if (id != 0)
                        res.ElementId = id;
                    else throw new NullReferenceException();
                }
                return res;
            }
            throw new NullReferenceException("Elements is null");
        }
    }
}

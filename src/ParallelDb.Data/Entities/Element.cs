using System;
using System.Collections.Generic;

namespace ParallelDb.Data.Entities
{
    public class Element : EntityBase
    {
        public virtual ICollection<DepElement> DepElements { get; set; }
        public Element() : base(){}
        private Element(Random r) : base(r) {}
        public static Element CreateInstance(Random r)=> new(r);
    }
}

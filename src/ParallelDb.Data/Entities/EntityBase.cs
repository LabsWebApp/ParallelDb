using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParallelDb.Data.Entities
{
    public abstract class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        public string Name { get; set; }
        public int? Number { get; set; }
        
        [NotMapped]
        protected Random R { get; set; }
        private void SetRandom()
        {
            if (R.Next(0, 4) > 0)
                Name = R.Next(1000, 10000).ToString();
            if (Name is null || R.Next(0, 2) > 0)
                Number = R.Next();
        }

        protected EntityBase(Random r)
        {
            R = r ?? throw new NullReferenceException("r is null");
            SetRandom();
        }

        protected EntityBase() { }
    }
}

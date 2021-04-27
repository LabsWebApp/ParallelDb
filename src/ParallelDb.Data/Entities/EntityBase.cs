using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParallelDb.Data.Entities
{
    public abstract class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }
        public int? Number { get; set; }

        internal Random R { get; init; }
        public void SetRandom()
        {
            Name = R.Next(1000, 10000).ToString();
            if (R.Next(0, 2) > 0)
                Number = R.Next();
        }

        internal EntityBase(bool first = false)
        {
            if (first)
            {
                R = new Random(DateTime.Now.Millisecond);
                SetRandom();
            }
        }
    }
}

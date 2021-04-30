using Microsoft.EntityFrameworkCore;
using ParallelDb.Data.Entities;

namespace ParallelDb.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Element> Elements { get; set; }
        public DbSet<DepElement> DepElements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString =
            //    ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            optionsBuilder.UseSqlServer(@"Data Source=(local)\SQLEXPRESS; Database=ParallelDB; Persist Security Info=false; User ID='sa'; Password='sa'; MultipleActiveResultSets=True; Trusted_Connection=False;");
        }

        public void Truncate()
        {
            string de = nameof(DepElements), 
                e = nameof(Elements),
                eid = nameof(DepElement.ElementId),
                fk = $"FK_{de}_{e}_{eid}";
            Database.ExecuteSqlRaw($"ALTER TABLE {de} DROP CONSTRAINT {fk}");
            Database.ExecuteSqlRaw($"TRUNCATE TABLE {de}");
            Database.ExecuteSqlRaw($"TRUNCATE TABLE {e}");
            Database.ExecuteSqlRaw($"ALTER TABLE {de} WITH CHECK ADD CONSTRAINT {fk} FOREIGN KEY({eid}) REFERENCES {e}({nameof(Element.Id)})");
            Database.ExecuteSqlRaw($"ALTER TABLE {de} CHECK CONSTRAINT {fk}");
        }
    }
}

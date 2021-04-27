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

        public async void Truncate()
        {
            await Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {nameof(DepElements)}");
            await Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {nameof(Elements)}");
        }
    }
}

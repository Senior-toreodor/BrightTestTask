using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BrightTestTask.Data
{
    public class NumbersTable
    {
        public int Id { get; set; }
        public int Value { get; set; } 
    }

    public class SortedNumbersTable
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int Position { get; set; }
    }

    public class NumbersDbContext : DbContext
    {
        public NumbersDbContext()
            : base(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=NumbersDb_CodeFirst;Integrated Security=True;MultipleActiveResultSets=True")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<NumbersDbContext>());
        }

        public DbSet<NumbersTable> Numbers { get; set; } = null!;
        public DbSet<SortedNumbersTable> SortedNumbers { get; set; } = null!;
    }
}


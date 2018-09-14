using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TQ.Models;

namespace TQ
{
    public class TestAppDataBaseContext : DbContext
    {
        public TestAppDataBaseContext(DbContextOptions<TestAppDataBaseContext> options)
            : base(options)
                  {

        }

        public DbSet<Buyer> Buyers { get; set; }
    }
}

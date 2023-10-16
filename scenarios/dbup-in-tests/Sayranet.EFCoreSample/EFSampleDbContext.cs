using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sayranet.EFCoreSample
{
    public sealed class EFSampleDbContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }

        public EFSampleDbContext()
        {

        }

        public EFSampleDbContext(DbContextOptions<EFSampleDbContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            optionsBuilder.UseSqlServer();

            base.OnConfiguring(optionsBuilder);
        }
    }
}

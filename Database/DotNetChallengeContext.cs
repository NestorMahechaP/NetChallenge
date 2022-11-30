using Microsoft.EntityFrameworkCore;
using NetChallenge.Database.Model;

namespace NetChallenge.Database
{
    public class DotNetChallengeContext : DbContext
    {
        public DotNetChallengeContext(DbContextOptions<DotNetChallengeContext> options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
        }
    }
}
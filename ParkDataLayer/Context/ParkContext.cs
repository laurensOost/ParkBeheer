using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ParkDataLayer.Entitites;

namespace ParkDataLayer.Context
{
    public class ParkContext : DbContext
    {
        public DbSet<ParkEF> Parks { get; set; }
        public DbSet<HuisEF> Huizen { get; set; }
        public DbSet<HuurderEF> Huurders { get; set; }
        public DbSet<HuurcontractEF> Huurcontracten { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-8H8592P3\SQLEXPRESS;Initial Catalog=ParkbeheerDB;Integrated Security=True;TrustServerCertificate=True");
        }
        
    }
}
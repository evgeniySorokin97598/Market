using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Market.IdentetyServer.Entities;

namespace Market.IdentetyServer.Logic
{
    public class Context : IdentityDbContext
    {
        public DbSet<IdentetyUser> Users { get; set; }
        public Context()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.133.128;Port=5432;Database=Identety;Username=postgres;Password=123qwe45asd");
        }
    }
}
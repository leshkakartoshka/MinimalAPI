using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

namespace MinimalAPI.Contexts
{
    public class Context : DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Parameters> Parameters { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}

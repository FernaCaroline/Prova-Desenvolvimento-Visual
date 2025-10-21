using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal; 
namespace Fernanda.Models;
public class AppDataContext : DbContext
{
    public DbSet<Consumo> Consumos{ get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Fernanda_Fernanda.db");
    }
}

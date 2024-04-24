using EFCorePractice.PatrickGodTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice.PatrickGodTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Team> Teams { get; set; }
    }

}
        
    


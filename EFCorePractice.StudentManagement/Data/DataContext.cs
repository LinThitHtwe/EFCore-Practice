using EFCorePractice.StudentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice.StudentManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

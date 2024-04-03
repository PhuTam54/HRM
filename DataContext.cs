using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employee> Employee => Set<Employee>();
    }
}

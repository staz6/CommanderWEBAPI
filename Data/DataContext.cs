using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) :base(option)
        {
            
        }
        public DbSet<Command> Commands { get; set; }
    }
}
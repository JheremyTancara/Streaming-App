using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
    }
}

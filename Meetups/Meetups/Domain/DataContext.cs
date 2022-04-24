using Meetups.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetups.Domain
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Meetup> Meetups { get; set; }
    }

}

using Abyssal_Events.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Abyssal_Events.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }

        public DbSet<EventPost> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EventPostLike> EventPostLike { get; set; }
    }
}

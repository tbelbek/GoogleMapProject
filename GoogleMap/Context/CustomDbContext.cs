using GoogleMap.Models.DbModels;
using System.Data.Entity;

namespace GoogleMap.Context
{
    public class CustomDbContext : DbContext
    {
        public DbSet<Event> Event { get; set; }

        public DbSet<Stop> Stop { get; set; }

        public DbSet<EventStops> EventStops { get; set; }

        public DbSet<Waypoint> EventWaypoints { get; set; }
    }
}
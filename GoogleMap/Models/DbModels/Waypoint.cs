namespace GoogleMap.Models.DbModels
{
    public class Waypoint
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string WaypointX { get; set; }

        public string WaypointY { get; set; }
    }
}
namespace GoogleMap.Models.DbModels
{
    public class EventStops
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int StopId { get; set; }

        public int? DistanceBetweenStops { get; set; }

        public string CoordinateX { get; set; }

        public string CoordinateY { get; set; }
    }
}
using GoogleMap.Models.DbModels;
using Microsoft.Owin.Security.Provider;

namespace GoogleMap.Models
{
    public class EventStopViewModel
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int StopId { get; set; }

        public StopType Type { get; set; }
        public int? DistanceBetweenStops { get; set; }

        public string CoordinateX { get; set; }

        public string CoordinateY { get; set; }

        public string IconUrl { get; set; }

        public string Name { get; set; }
    }

    public class EventStopAddressModel
    {
        public string Status { get; set; }

        public string Address { get; set; }

        public string ErrorDescription { get; set; }
    }
}
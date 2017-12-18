namespace GoogleMap.Models.DbModels
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StartPoint { get; set; }
        
        public string FinishPoint { get; set; }

        public string StartX { get; set; }

        public string StartY { get; set; }

        public string EndX { get; set; }

        public string EndY { get; set; }

        public string Distance { get; set; }

        public string ImageSrc { get; set; }
    }
}
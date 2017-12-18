namespace GoogleMap.Models.DbModels
{
    public enum StopType
    {
        Dynamic = 1,
        Manuel = 2
    }

    public class Stop
    {
        public int Id { get; set; }

        public StopType Type { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }
    }
}
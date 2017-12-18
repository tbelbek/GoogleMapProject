using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoogleMap.Models
{
    public class EventCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Etkinlik Adı")]
        public string Name { get; set; }
    }

    public class EventListViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Adı")]
        public string Name { get; set; }

        [Display(Name = "Baslangic Noktası")]
        public string StartPoint { get; set; }

        [Display(Name = "Bitiş Noktası")]
        public string FinishPoint { get; set; }

        [Display(Name = "Baslangic Noktası X")]
        public string StartX { get; set; }
        
        [Display(Name = "Baslangic Noktası Y")]
        public string StartY { get; set; }
        
        [Display(Name = "Bitiş Noktası X")]
        public string EndX { get; set; }
        
        [Display(Name = "Bitiş Noktası Y")]
        public string EndY { get; set; }
        
        [Display(Name = "Toplam Mesafe")]
        public string Distance { get; set; }


        [Display(Name = "Güzergah Fotoğrafı")]
        public string ImgUrl { get; set; }
    }

    public class EventEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }
    }

    public class EventPlanViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }

        public string StartPoint { get; set; }
        
        public string FinishPoint { get; set; }

        [Required]
        [Display(Name = "Baslangic Noktası X")]
        public string StartX { get; set; }

        [Required]
        [Display(Name = "Baslangic Noktası Y")]
        public string StartY { get; set; }

        [Required]
        [Display(Name = "Bitiş Noktası X")]
        public string EndX { get; set; }

        [Required]
        [Display(Name = "Bitiş Noktası Y")]
        public string EndY { get; set; }

        [Required]
        [Display(Name = "Toplam Mesafe")]
        public string Distance { get; set; }

        
        public string ImageSrc { get; set; }

        [Required]
        [Display(Name = "Duraklar")]
        public List<EventStopViewModel> Stops { get; set; }

        [Required]
        [Display(Name = "Durak Tipleri")]
        public IEnumerable<StopListViewModel> StopNames { get; set; }

        [Required]
        [Display(Name = "Durak Tipleri")]
        public List<WaypointViewModel> Waypoints { get; set; }
    }


   

}
using GoogleMap.Helper;
using GoogleMap.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace GoogleMap.Models
{
    public class StopCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipi")]
        public StopType Type { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }
        
        [CheckImageSize]
        [Display(Name = "icon Url")]
        public string IconUrl { get; set; }

    }

    public class StopListViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipi")]
        public StopType Type { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }
        
        [Display(Name = "icon Url")]
        public string IconUrl { get; set; }

        public string Stop { get; set; }
    }

    public class StopEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipi")]
        public StopType Type { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }

        [CheckImageSize]
        [Display(Name = "icon Url")]
        public string IconUrl { get; set; }
    }

    public class StopGetViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipi")]
        public StopType Type { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }
        
        [Display(Name = "icon Url")]
        public string IconUrl { get; set; }

        public int? StopId { get; set; }

        
    }
}
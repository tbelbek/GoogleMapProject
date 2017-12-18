using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoogleMap.Models.DbModels;

namespace GoogleMap.Models
{
    public class ExcelViewModel
    {

    }

    public class ExcelViewMainModel
    {

        public string Name { get; set; }

        public string StartPoint { get; set; }

        public string FinishPoint { get; set; }

        public string StartCoordinates { get; set; }

        public string EndCoordinates { get; set; }

        public string Distance { get; set; }

    }

    public class ExcelViewStopModel
    {

        public string Name { get; set; }

        public string Coordinates { get; set; }

        public string Address { get; set; }
    }

    public class ExcelViewWPModel
    {
        public string WaypointCoordinates { get; set; }

        public string Address { get; set; }
    }
}
using System;
using GoogleMap.Context;
using GoogleMap.Models;
using GoogleMap.Models.DbModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml.Linq;

namespace GoogleMap.Controllers
{
    public class HomeController : Controller
    {
        CustomDbContext context;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateStopView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStopView(StopCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                context = new CustomDbContext();
                Stop dbModel = new Stop()
                {
                    Name = model.Name,
                    Type = model.Type,
                    IconUrl = model.IconUrl
                };

                context.Stop.Add(dbModel);
                context.SaveChanges();

                return RedirectToAction("ListStopView");
            }

            return View();
        }

        public ActionResult DeleteEvent(int id)
        {
            context = new CustomDbContext();
            context.Event.Remove(context.Event.FirstOrDefault(x => x.Id == id));

            context.EventStops.RemoveRange(context.EventStops.Where(x => x.EventId == id).ToList());
            context.EventWaypoints.RemoveRange(context.EventWaypoints.Where(x => x.EventId == id).ToList());

            context.SaveChanges();

            return RedirectToAction("ListEventView");
        }

        public ActionResult DeleteStopView(int id)
        {
            context = new CustomDbContext();
            context.Stop.Remove(context.Stop.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();

            return RedirectToAction("ListStopView");
        }

        public ActionResult ListStopView()
        {
            context = new CustomDbContext();
            var dbStops = context.Stop.ToList();

            List<StopListViewModel> stopList = dbStops.Select(dbStop => new StopListViewModel()
            {
                Id = dbStop.Id,
                Name = dbStop.Name,
                Type = dbStop.Type,
                IconUrl = dbStop.IconUrl
            }).ToList();

            IEnumerable<StopListViewModel> returnModel = stopList;
            return View(returnModel);
        }

        public ActionResult EditStopView(int id)
        {
            context = new CustomDbContext();

            var dbStop = context.Stop.FirstOrDefault(x => x.Id == id);


            if (dbStop != null)
            {
                StopEditViewModel model = new StopEditViewModel()
                {
                    Id = dbStop.Id,
                    Name = dbStop.Name,
                    Type = dbStop.Type,
                    IconUrl = dbStop.IconUrl
                };

                return View(model);
            }

            return RedirectToAction("ListStopView");
        }

        [HttpPost]
        public ActionResult EditStopView(StopEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                context = new CustomDbContext();

                Stop dbModel = context.Stop.FirstOrDefault(x => x.Id == model.Id);

                if (dbModel != null)
                {
                    dbModel.Name = model.Name;
                    dbModel.Type = model.Type;
                    dbModel.IconUrl = model.IconUrl;
                }

                context.SaveChanges();

                return RedirectToAction("ListStopView");
            }

            return View();
        }

        public ActionResult ListEventView()
        {
            context = new CustomDbContext();

            List<Event> dbModels = context.Event.ToList();

            List<EventListViewModel> listModel = dbModels.Select(model => new EventListViewModel()
            {
                Name = model.Name,
                EndX = model.EndX,
                EndY = model.EndY,
                Distance = model.Distance ?? "0",
                StartX = model.StartX,
                StartY = model.StartY,
                Id = model.Id,
                StartPoint = model.StartPoint,
                FinishPoint = model.FinishPoint
            }).ToList();

            IEnumerable<EventListViewModel> returnModel = listModel;

            return View(returnModel);
        }

        public ActionResult CreateEventView()
        {
            EventCreateViewModel returnModel = new EventCreateViewModel();
            context = new CustomDbContext();

            return View(returnModel);
        }

        [HttpPost]
        public ActionResult CreateEventView(EventCreateViewModel eventDataItem)
        {
            context = new CustomDbContext();
            Event dbModel = new Event()
            {
                Name = eventDataItem.Name

            };
            context.Event.Add(dbModel);
            context.SaveChanges();


            return RedirectToAction("ListEventView");
        }

        public JsonResult FindStopById(int id)
        {
            context = new CustomDbContext();

            Stop stop = context.Stop.FirstOrDefault(x => x.Id == id);

            return Json(stop, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllStops()
        {
            context = new CustomDbContext();

            List<Stop> stops = context.Stop.ToList();

            return Json(stops, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventPlanner(int id)
        {
            context = new CustomDbContext();
            Event eventData = context.Event.FirstOrDefault(x => x.Id == id);

            if (eventData != null)
            {
                EventPlanViewModel eventPlanning = new EventPlanViewModel()
                {
                    Id = eventData.Id,
                    Name = eventData.Name,
                    StartX = eventData.StartX,
                    StartY = eventData.StartY,
                    EndX = eventData.EndX,
                    EndY = eventData.EndY,
                    Distance = eventData.Distance,
                    FinishPoint = eventData.FinishPoint,
                    StartPoint = eventData.StartPoint
                };

                List<Stop> stopNames = context.Stop.ToList();
                List<StopListViewModel> stopViews = stopNames.Select(item => new StopListViewModel()
                {
                    Name = item.Name,
                    IconUrl = item.IconUrl,
                    Id = item.Id,
                    Type = item.Type,
                    Stop = string.Concat(item.Id, "|", (Int32)item.Type)
                }).ToList();

                eventPlanning.StopNames = stopViews;
                return View(eventPlanning);
            }
            return RedirectToAction("ListEventView");
        }

        [HttpPost]
        public ActionResult EventPlanner(EventPlanViewModel eventPlanned)
        {
            bool returnObject = false;
            context = new CustomDbContext();

            var eventData = context.Event.FirstOrDefault(x => x.Id == eventPlanned.Id);
            if (eventData != null)
            {
                eventData.StartPoint = eventPlanned.StartPoint;
                eventData.FinishPoint = eventPlanned.FinishPoint;
                eventData.StartY = eventPlanned.StartY;
                eventData.StartX = eventPlanned.StartX;
                eventData.EndY = eventPlanned.EndY;
                eventData.EndX = eventPlanned.EndX;
                eventData.Distance = eventPlanned.Distance;
            }

            List<EventStops> oldStops = context.EventStops.Where(x => x.EventId == eventPlanned.Id).ToList();
            if (oldStops.Count > 0)
            {
                context.EventStops.RemoveRange(oldStops);
            }

            if (eventPlanned.Stops != null)
            {
                List<EventStops> stops = eventPlanned.Stops.Select(model => new EventStops
                {
                    CoordinateX = model.CoordinateX,
                    CoordinateY = model.CoordinateY,
                    DistanceBetweenStops = model.DistanceBetweenStops,
                    EventId = model.EventId,
                    StopId = model.StopId
                }).ToList();

                context.EventStops.AddRange(stops);
            }

            if (eventPlanned.Waypoints != null)
            {
                List<Waypoint> waypoints = eventPlanned.Waypoints.Select(model => new Waypoint
                {
                    EventId = model.EventId,
                    WaypointX = model.WaypointX,
                    WaypointY = model.WaypointY
                }).ToList();

                context.EventWaypoints.AddRange(waypoints);
            }

            if (context.SaveChanges() > 0)
            {
                returnObject = true;
            }

            return Json(returnObject, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEventDetails(int id)
        {

            context = new CustomDbContext();
            Event eventData = context.Event.FirstOrDefault(x => x.Id == id);

            if (eventData != null)
            {
                EventPlanViewModel eventPlanning = new EventPlanViewModel()
                {
                    Id = eventData.Id,
                    Name = eventData.Name,
                    StartX = eventData.StartX,
                    StartY = eventData.StartY,
                    EndX = eventData.EndX,
                    EndY = eventData.EndY,
                    Distance = eventData.Distance,
                    FinishPoint = eventData.FinishPoint,
                    StartPoint = eventData.StartPoint
                };


                return View(eventPlanning);
            }
            return null;

        }

        public JsonResult EventGet(int id)
        {
            try
            {
                context = new CustomDbContext();
                Event mainEvent = context.Event.FirstOrDefault(x => x.Id == id && x.StartX != null && x.StartY != null && x.EndX != null && x.EndY != null);

                if (mainEvent != null)
                {
                    List<Stop> stops = context.Stop.ToList();
                    List<StopGetViewModel> stopsView = new List<StopGetViewModel>();
                    foreach (Stop item in stops)
                    {
                        StopGetViewModel stopView = new StopGetViewModel();
                        stopView.Id = item.Id;
                        stopView.IconUrl = item.IconUrl;
                        stopView.Name = item.Name;
                        stopView.Type = item.Type;
                        stopView.StopId = (int)item.Type;
                        stopsView.Add(stopView);
                    }

                    List<Waypoint> eventWaypoints = context.EventWaypoints.Where(x => x.EventId == id).ToList();

                    List<WaypointViewModel> wptToLoad = new List<WaypointViewModel>();
                    foreach (Waypoint item in eventWaypoints)
                    {
                        WaypointViewModel wpt = new WaypointViewModel();
                        wpt.Id = item.Id;
                        wpt.EventId = item.EventId;
                        wpt.WaypointX = item.WaypointX;
                        wpt.WaypointY = item.WaypointY;
                        wptToLoad.Add(wpt);

                    }
                    List<EventStops> eventStops = context.EventStops.Where(x => x.EventId == id).ToList();
                    List<EventStopViewModel> stopToLoad = new List<EventStopViewModel>();
                    foreach (EventStops item in eventStops)
                    {
                        EventStopViewModel stop = new EventStopViewModel();
                        stop.Id = item.Id;
                        stop.EventId = item.EventId;
                        stop.CoordinateX = item.CoordinateX;
                        stop.CoordinateY = item.CoordinateY;
                        stop.DistanceBetweenStops = item.DistanceBetweenStops;
                        stop.StopId = item.StopId;

                        var stopGetViewModel = stopsView.Where(x => x.Id == item.StopId).FirstOrDefault();
                        if (stopGetViewModel != null)
                        {
                            stop.IconUrl = stopGetViewModel.IconUrl;
                            stop.Name = stopGetViewModel.Name;
                        }

                        stopToLoad.Add(stop);

                    }

                    EventPlanViewModel eventToLoad = new EventPlanViewModel();
                    if (mainEvent != null)
                    {
                        eventToLoad.Id = mainEvent.Id;
                        eventToLoad.Distance = mainEvent.Distance;
                        eventToLoad.EndX = mainEvent.EndX;
                        eventToLoad.EndY = mainEvent.EndY;
                        eventToLoad.FinishPoint = mainEvent.FinishPoint;
                        eventToLoad.StartPoint = mainEvent.StartPoint;
                        eventToLoad.Name = mainEvent.Name;
                        eventToLoad.StartY = mainEvent.StartY;
                        eventToLoad.StartX = mainEvent.StartX;
                    }
                    eventToLoad.Waypoints = wptToLoad;
                    eventToLoad.Stops = stopToLoad;

                    return Json(eventToLoad, JsonRequestBehavior.AllowGet);
                }
                else {
                    return null;
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        EventStopAddressModel GetGeocodeAddress(string latlng)
        {
            EventStopAddressModel returnValue = new EventStopAddressModel();
            var requestUri = string.Format("http://maps.google.com/maps/api/geocode/xml?latlng={0}&sensor=false", Uri.EscapeDataString(latlng));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());
            var status = xdoc.Element("GeocodeResponse").Element("status").Value;


            if (status == "OK")
            {
                var address = xdoc.Element("GeocodeResponse").Element("result").Element("formatted_address").Value;
                returnValue.Address = address;
                returnValue.Status = status;
            }
            else
            {
                returnValue.ErrorDescription = String.Concat("(", xdoc.Element("GeocodeResponse").Element("status").Value, ") ", xdoc.Element("GeocodeResponse").Element("error_message").Value);
                returnValue.Status = "ERR";
            }

            return returnValue;
        }

        public JsonResult ExportData(int id)
        {
            try
            {
                context = new CustomDbContext();
                GridView eventMainData = new GridView();
                List<Event> eventData = context.Event.Where(x => x.Id == id).ToList();
                List<ExcelViewMainModel> eventDataForExcel = new List<ExcelViewMainModel>();
                foreach (Event item in eventData)
                {
                    ExcelViewMainModel mainEvent = new ExcelViewMainModel()
                    {
                        Name = item.Name,
                        StartPoint = item.StartPoint,
                        FinishPoint = item.FinishPoint,
                        StartCoordinates = string.Concat("Enlem/Boylam; ", item.StartX, ", ", item.StartY),
                        EndCoordinates = string.Concat("Enlem/Boylam; ", item.EndX, ", ", item.EndY),
                        Distance = item.Distance
                    };
                    eventDataForExcel.Add(mainEvent);
                }
                eventMainData.DataSource = eventDataForExcel;
                eventMainData.DataBind();


                GridView eventStopData = new GridView();
                List<EventStops> stopData = context.EventStops.Where(x => x.EventId == id && x.StopId != 0).ToList();
                List<ExcelViewStopModel> eventStopDataForExcel = new List<ExcelViewStopModel>();
                List<Stop> stops = context.Stop.ToList();
                foreach (EventStops item in stopData)
                {
                    ExcelViewStopModel stopEvent = new ExcelViewStopModel();
                    var firstOrDefault = stops.FirstOrDefault(x => item.StopId == x.Id);
                    if (firstOrDefault != null)
                        stopEvent.Name = firstOrDefault.Name;
                    stopEvent.Coordinates = string.Concat("Enlem/Boylam; ", item.CoordinateX, ", ", item.CoordinateY);

                    string latlng = string.Concat(item.CoordinateX, ",", item.CoordinateY);
                    EventStopAddressModel returnAddress = new EventStopAddressModel();
                    int maxRepeatCount = 0;

                    for (;;)
                    {
                        returnAddress = GetGeocodeAddress(latlng);
                        if (returnAddress.Status == "OK")
                        {
                            stopEvent.Address = returnAddress.Address;
                            break;
                        }
                        else if (maxRepeatCount >= 20)
                        {
                            stopEvent.Address = returnAddress.ErrorDescription;
                            break;
                        }

                        maxRepeatCount++;
                    }

                    eventStopDataForExcel.Add(stopEvent);
                }

                eventStopData.DataSource = eventStopDataForExcel;
                eventStopData.DataBind();

                GridView eventWPData = new GridView();
                List<Waypoint> waypointData = context.EventWaypoints.Where(x => x.EventId == id).ToList();
                List<ExcelViewWPModel> eventWpDataForExcel = new List<ExcelViewWPModel>();
                foreach (Waypoint item in waypointData)
                {
                    ExcelViewWPModel WpEvent = new ExcelViewWPModel();
                    WpEvent.WaypointCoordinates = string.Concat("Enlem/Boylam; ", item.WaypointX, ", ", item.WaypointY);
                    string latlng = string.Concat(item.WaypointX, ",", item.WaypointY);
                    EventStopAddressModel returnAddress = new EventStopAddressModel();
                    int maxRepeatCount = 0;

                    for (;;)
                    {
                        returnAddress = GetGeocodeAddress(latlng);
                        if (returnAddress.Status == "OK")
                        {
                            WpEvent.Address = returnAddress.Address;
                            break;
                        }
                        else if (maxRepeatCount >= 20)
                        {
                            WpEvent.Address = returnAddress.ErrorDescription;
                            break;
                        }

                        maxRepeatCount++;
                    }

                    eventWpDataForExcel.Add(WpEvent);
                }
                eventWPData.DataSource = eventWpDataForExcel;
                eventWPData.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=event-excel.xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = Encoding.UTF8;
                Response.BinaryWrite(Encoding.UTF8.GetPreamble());
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                eventMainData.RenderControl(htw);
                eventStopData.RenderControl(htw);
                eventWPData.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

                return Json("Excel dosyası oluşturuldu.");
            }
            catch (Exception)
            {
                return Json("Excel dosyası oluşturulurken bir hata oluştu.");
            }
        }

        public ActionResult PrintMap(int id)
        {
            context = new CustomDbContext();
            Event eventData = context.Event.FirstOrDefault(x => x.Id == id);

            EventPlanViewModel eventPlanning = new EventPlanViewModel()
            {
                Id = eventData.Id,
                Name = eventData.Name,
                StartX = eventData.StartX,
                StartY = eventData.StartY,
                EndX = eventData.EndX,
                EndY = eventData.EndY,
                Distance = eventData.Distance,
                FinishPoint = eventData.FinishPoint,
                StartPoint = eventData.StartPoint
            };


            return View(eventPlanning);
        }
    }
}
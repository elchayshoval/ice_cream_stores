using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.placesApi
{
    public class DirectionService
    {
        public void GetDirection()
        {
            //var request = new DirectionsRequest
            //{
            //    Key = "AIzaSyCepi3EaE4INsbTbIZjPGjCZQyM0ivpAf0",
            //    Origin = new Location("285 Bedford Ave, Brooklyn, NY, USA"),
            //    Destination = new Location("185 Broadway Ave, Manhattan, NY, USA")
            //};

            //var result = GoogleMaps.Directions.Query(request);
            //var json=result.RawJson;
            //var overviewPath = result.Routes.First().OverviewPath;
            //var polyline = result.Routes.First().Legs.First().Steps.First().PolyLine;


            //var request2 = new StaticMapsRequest
            //{
            //    Key = "AIzaSyCepi3EaE4INsbTbIZjPGjCZQyM0ivpAf0",
            //    Center = new Location(60.170877, 24.942796),
            //    ZoomLevel = 1
            //};

            //var result2 = GoogleMaps.StaticMaps.Query(request2);
            //var map = result2.Stream;

        }
    }
}

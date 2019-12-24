using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using GoogleApi.Entities.Maps.StaticMaps.Request.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.placesApi
{
    class MapService
    {
        public byte[] GetMap(string location)
        {
            var request2 = new StaticMapsRequest
            {
                Key = "AIzaSyCepi3EaE4INsbTbIZjPGjCZQyM0ivpAf0",
                Markers = new List<MapMarker>() { 
                    new MapMarker() {Label="S",Color=MapColor.Purple,Locations=new List<Location>(){new Location(location) } } 
                },
                //Center = new GoogleApi.Entities.Common.Location(location),
                ZoomLevel = 19
            };

            var result2 = GoogleMaps.StaticMaps.Query(request2);
            var map = result2.Buffer;
            return map;



        }
    }
}

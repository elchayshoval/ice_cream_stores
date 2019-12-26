using GoogleApi;
using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleApi.Entities.Places.AutoComplete.Request.Enums;
using MaterialDesignExtensions.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.placesApi
{
    public class PlacesAutoComplete : IAutocompleteSource
    {
        public IEnumerable Search(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = " ";
                }
                var request = new PlacesAutoCompleteRequest
                {
                    Key = "AIzaSyCepi3EaE4INsbTbIZjPGjCZQyM0ivpAf0",
                    Input = searchTerm,
                    Types = new List<RestrictPlaceType> { RestrictPlaceType.Address }
                };
                var response = GooglePlaces.AutoComplete.Query(request);
                //var result = response.Predictions.ToList();
                var result = from item in response.Predictions
                             select item.Description;
                return result;
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}

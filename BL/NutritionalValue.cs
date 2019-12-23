using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using BE;

namespace BL
{
    public static class NutritionalValue
    {
        private const  string foodDataUrl = "https://api.nal.usda.gov/ndb/V2/";
        private const  string apiKey = "DLwuthaqdeeHBGvd25VyoUskN3OmOf55j0bpUqLA";
        private const  int energyId = 208;
        private const  int proteinId = 203;
        private const  int fatId = 204;


        public static Nutrition getNutritionalValue(string productDescription)
        {
            return null;//TDOD implement please
        }

        public static Nutrition getNutritionalValue(int foodId)
        {
            string result = getProductNutrition(foodId).Result;
            return fetchResponse(result);
        }

        private static async Task<int> getProductId(string productDescription)
        {
            return -1;//TDOD Ihave to implement
        }
        private static async Task<string> getProductNutrition(int productId)
        {
            using (var client = new HttpClient())
            {
                string url = "https://api.nal.usda.gov/fdc/v1/" + productId;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));

                HttpResponseMessage response = await client.GetAsync(string.Format("?api_key={0}", apiKey));

                HttpContent content = response.Content;



                string result = await content.ReadAsStringAsync();
                return result;
            }
        }

        private static Nutrition fetchResponse(string response)
        {
            Nutrition nutrition = new Nutrition();
            JObject jobject = JObject.Parse(response);

            foreach (var item in jobject["foodNutrients"])
            {
                int nutId;
                int value;
                try
                {
                    nutId = (int)item["nutrient"]["number"];
                    value = (int)item["amount"];
                }
                catch (Exception)
                {

                    continue;
                }
                switch (nutId)
                {
                    case energyId:
                        nutrition.Energy = value;
                        break;
                    case proteinId:
                        nutrition.Protein = value;
                        break;
                    case fatId:
                        nutrition.Fat = value;
                        break;

                    default:
                        break;
                }
            }

            return nutrition;
        }


    }
}


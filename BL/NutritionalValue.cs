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


        public static async Task<Nutrition> getNutritionalValue(string productDescription)
        {
            int productId =await getProductId(productDescription);
            return await getNutritionalValue(productId);
        }

        public static async Task<Nutrition> getNutritionalValue(int foodId)
        {
            string result =await getProductNutrition(foodId);
            return fetchResponse(result);
        }

        public static async Task<int> getProductId(string productDescription)
        {
            using (var client = new HttpClient())
            {
                string url = "https://api.nal.usda.gov/fdc/v1/search" ;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));

                HttpResponseMessage response = await client.GetAsync(string.Format("?api_key={0}&generalSearchInput={1}", apiKey,productDescription));

                HttpContent content = response.Content;



                string result = await content.ReadAsStringAsync();

                return FetchNutrienIdSearchResponse(result); ;
            }
            
        }

        private static int FetchNutrienIdSearchResponse(string response)
        {
            int result = -1;
            JObject jobject = JObject.Parse(response);

            foreach (var item in jobject["foods"])
            {
                result = (int)item["fdcId"];
                break;
            }
            return result;
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


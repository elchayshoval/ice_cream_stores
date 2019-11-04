using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;



namespace BL
{
    public static class ImageAnlyzer
    {
        private static readonly string imaggaUrl = "https://api.imagga.com/v2/";
        private static readonly string apiKey = "acc_eed74da3ce61108";
        private static readonly string apiSecret = "0f47674d79b58a8a615d82b4d84e967e";
        private static readonly string authorization = "YWNjX2VlZDc0ZGEzY2U2MTEwODowZjQ3Njc0ZDc5YjU4YThhNjE1ZDgyYjRkODRlOTY3ZQ==";
        private static readonly double CONFIDENT_VALUE=80;

        public static Boolean IsIceCreamConfident(string url)
        {
            string response = requestImageAnalyzer(url).Result;
            return fetchResponse(response);
        }

        private static async Task<string> requestImageAnalyzer(string url)
        {
            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(imaggaUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", basicAuthValue));

                HttpResponseMessage response = await client.GetAsync(String.Format("tags?image_url={0}", url));

                HttpContent content = response.Content;
                string result = await content.ReadAsStringAsync();
                return result;
            }

        }

        private static Boolean fetchResponse(string response)
        {
            JObject jobject = JObject.Parse(response);

            var selectedTages = (from t in jobject["result"]["tags"]
                                 where (string)t["tag"]["en"] == "ice cream" //i have to check the tag iceceream is corect
                                 select new { confidence = (double)t["confidence"], tag = (string)t["tag"]["en"] });


            Boolean result = false;
            foreach (var item in selectedTages)
            {
                if (item.confidence > CONFIDENT_VALUE)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}

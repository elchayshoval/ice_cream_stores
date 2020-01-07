using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace BL
{
    public static class ImageAnlyzer
    {
        private static readonly string imaggaUrl = "https://api.imagga.com/v2/";
        private static readonly string apiKey = "acc_eed74da3ce61108";
        private static readonly string apiSecret = "0f47674d79b58a8a615d82b4d84e967e";
        //private static readonly string authorization = "YWNjX2VlZDc0ZGEzY2U2MTEwODowZjQ3Njc0ZDc5YjU4YThhNjE1ZDgyYjRkODRlOTY3ZQ==";
        private static readonly double CONFIDENT_VALUE = 80;
        private static string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));


        public static async Task<Boolean> IsIceCreamConfident(string url)
        {
            string response = await requestImageAnalyzer(url);
            return fetchResponse(response);
        }

        public static async Task<Boolean> CheckIceCreamConfidentByPath(string fileName)
        {
            string requestURL = "https://api.imagga.com/v2/tags";
            
            byte[] bytes = File.ReadAllBytes(fileName);

            Dictionary<string, FileParameter> postParameters = new Dictionary<string, FileParameter>();

            postParameters.Add("image", new FileParameter(bytes, Path.GetFileName(fileName), "image/jpeg"));

            HttpWebResponse webResponse =  FileUpload.MultipartFormPost(requestURL, postParameters, basicAuthValue);

            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            var returnResponseText =await responseReader.ReadToEndAsync();

            webResponse.Close();
            return fetchResponse(returnResponseText);
        }



        private static async Task<string> requestImageAnalyzer(string url)
        {

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
                                 where (string)t["tag"]["en"] == "ice cream"|| (string)t["tag"]["en"]=="ice" //i have to check the tag iceceream is corect
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




        public static class FileUpload
        {
            private static readonly Encoding encoding = Encoding.UTF8;
            public static HttpWebResponse MultipartFormPost(string postUrl, Dictionary<string, FileParameter> postParameters, string authValue)
            {
                string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
                string contentType = "multipart/form-data; boundary=" + formDataBoundary;

                byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

                return PostForm(postUrl, contentType, formData, authValue);
            }
            private static HttpWebResponse PostForm(string postUrl, string contentType, byte[] formData, string authValue)
            {
                HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

                if (request == null)
                {
                    throw new NullReferenceException("request is not a http request");
                }

                request.Method = "POST";
                request.ContentType = contentType;
                request.ContentLength = formData.Length;

                request.Headers.Add("Authorization", String.Format("Basic {0}", authValue));

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(formData, 0, formData.Length);
                    requestStream.Close();
                }

                return request.GetResponse() as HttpWebResponse;
            }

            private static byte[] GetMultipartFormData(Dictionary<string, FileParameter> postParameters, string boundary)
            {
                Stream formDataStream = new System.IO.MemoryStream();
                bool needsCLRF = false;

                foreach (var p in postParameters)
                {

                    if (needsCLRF)
                        formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                    needsCLRF = true;

                    if (p.Value is FileParameter)
                    {
                        FileParameter fileToUpload = (FileParameter)p.Value;

                        string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                            boundary,
                            p.Key,
                            fileToUpload.FileName ?? p.Key,
                            fileToUpload.ContentType ?? "application/octet-stream");

                        formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                        formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                    }
                    else
                    {
                        string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                            boundary,
                            p.Key,
                            p.Value);
                        formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                    }
                }

                string footer = "\r\n--" + boundary + "--\r\n";
                formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

                formDataStream.Position = 0;
                byte[] formData = new byte[formDataStream.Length];
                formDataStream.Read(formData, 0, formData.Length);
                formDataStream.Close();

                return formData;
            }
        }

        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }
}


using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DL
{
    public class GoogleDriveService
    {
        private static readonly string[] scopes = { DriveService.Scope.Drive };
        private static readonly string applicationName = "Ice Cream Kiosk Information";
        private static readonly string iceCreamImageFolderCode = "1oMGH8cSlTGZ-9a3bkSlonFgh4JVSL_Pw";
        private static readonly string imageUrl = "https://drive.google.com/uc?export=view&id=";


        public static void Init()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });

            var folderMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "FlowerStoreImages",
                MimeType = "application/vnd.google-apps.folder"
            };
            var req = service.Files.Create(folderMetaData);
            req.Fields = "id";
            req.Execute();
            //await req.ExecuteAsync();
            //var resp=req.re



            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }

        }


        public static async Task<string> UploadImage(string path, string name="defaultName")
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
               // Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });



            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                Parents = new List<string>() { iceCreamImageFolderCode }

            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(path,
                                    System.IO.FileMode.Open))
            {
                request = service.Files.Create(
                    fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                //request.ContentStream = stream;
                await request.UploadAsync();
                //request.Upload();
            }
            var file = request.ResponseBody;
            return imageUrl+ file.Id;


        }
    }
}

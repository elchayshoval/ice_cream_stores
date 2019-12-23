using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DL;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.Threading.Tasks;

namespace TestIceCream
{
    [TestClass]
    public class GoogleDriveTest
    {
        

        [TestMethod]
        public async Task UplodingTest()
        {







            //GoogleDriveService.Init();

            string path = @"C:\Users\Chayim\Documents\KioskInformation\Pictures\4.jpg";
            var result =await GoogleDriveService.UploadImage(path);
            Assert.IsNotNull(result);
        }
        
        
    }
}

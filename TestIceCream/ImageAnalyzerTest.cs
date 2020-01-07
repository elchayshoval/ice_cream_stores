using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using System.Threading.Tasks;

namespace TestIceCream
{
    [TestClass]
    public class ImageAnalyzerTest
    {
        [TestMethod]
        public async Task TestIsIceCreamConfident()
        {
            Boolean result1 =await ImageAnlyzer.CheckIceCreamConfidentByPath(@"C:\Users\Chayim\Documents\KioskInformation\Pictures\rockyroad8a.jpg");
            Assert.AreEqual(true, result1, "success");
            
            Boolean result =await ImageAnlyzer.IsIceCreamConfident("https://drive.google.com/uc?export=view&id=1TBtVfEBr_02FRRW2HJCDAuOU0kioTvBy");
            Assert.AreEqual(true, result, "success");

            result =await ImageAnlyzer.IsIceCreamConfident("https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/easiest-ever-fruit-ice-cream-ghk-1532637317.jpg");
            Assert.AreEqual(true, result, "success");
        }
    }
}

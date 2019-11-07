using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;

namespace TestIceCream
{
    [TestClass]
    public class ImageAnalyzerTest
    {
        [TestMethod]
        public void TestIsIceCreamConfident()
        {
            Boolean result = ImageAnlyzer.IsIceCreamConfident("https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/easiest-ever-fruit-ice-cream-ghk-1532637317.jpg");
            Assert.AreEqual(true, result, "success");

            result = ImageAnlyzer.IsIceCreamConfident("https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/easiest-ever-fruit-ice-cream-ghk-1532637317.jpg");
            Assert.AreEqual(false, result, "success");
        }
    }
}

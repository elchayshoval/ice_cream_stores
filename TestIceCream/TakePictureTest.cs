using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using DL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestIceCream
{
    [TestClass]
    public class TakePictureTest
    {
        [TestMethod]
        public void RunningProcessTest()
        {
            Assert.IsNotNull(TakePicture.TakePictureProcess());
        }
    }
}

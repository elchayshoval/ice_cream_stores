using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE;
using BL;

namespace TestIceCream
{
    /// <summary>
    /// Summary description for NutritionalTest
    /// </summary>
    [TestClass]
    public class NutritionalTest
    {
        [TestMethod]
        public void TestById()
        {
            Nutrition nutrition = NutritionalValue.getNutritionalValue(167575);
            Assert.IsNotNull(nutrition);
        }
    }
}

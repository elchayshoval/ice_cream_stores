using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE;
using BL;
using System.Threading.Tasks;

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
            //Nutrition nutrition = NutritionalValue.getNutritionalValue(167575);
            //Assert.IsNotNull(nutrition);
        }
        [TestMethod]
        public async Task TestByGeneralSearch()
        {
            int nutid =await NutritionalValue.getProductId("Cheddar cheese");
            Assert.AreNotEqual(-1, nutid);
        }
        [TestMethod]
        public async Task IntegratedTest()
        {
            int nutid = await NutritionalValue.getProductId("Cheddar cheese");
            Nutrition nutrition =NutritionalValue.getNutritionalValue(nutid).Result;
            Assert.IsNotNull(nutrition);
            
        }

    }
}

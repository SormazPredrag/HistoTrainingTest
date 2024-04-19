using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationToolTest
{
    [TestClass]
    public class ScenarioFusionApp : HistoTraningSession
    {
        [TestMethod]
        public void Maximize3D()
        {
            WindowsElement FusionApp = sessionHTT.FindElementByName("Fusion App");
            //LoadExsistingPatient();
            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //editBox.Click();
            //Console.WriteLine("Kliknuo!");
            //Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}

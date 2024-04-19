using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationToolTest
{
    [TestClass]
    public class ScenarioFusionApp : HistoTraningSession
    {
        [TestMethod]
        public void Maximize3D()
        {
            var app = new ScenarioPatientOpen();
            app.ExpandExsistingPatientList();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            WindowsElement FusionApp = sessionHTT.FindElementByName("Fusion App");
            
            var builder = new Actions(sessionHTT);
            builder.MoveToElement(FusionApp, 1190, 414).Click().Build().Perform(); // pixel offset from top left
            builder.DragAndDropToOffset(FusionApp, 200, 100).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(4));

            app.ShutDownMenuClick();
            
            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //Console.WriteLine("Kliknuo!");

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

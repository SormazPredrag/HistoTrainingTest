using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.ImageComparison;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
            int xCoord = 1190 * WinWidth / 1680;
            builder.MoveToElement(FusionApp, xCoord, 414).Click().Build().Perform(); // pixel offset from top left
            builder.DragAndDropToOffset(FusionApp, 200, 100).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            // Take a screenshot
            Screenshot screenshot = sessionHTT.GetScreenshot();
            /*
            var options = new SimilarityMatchingOptions { Visualize = true };
            var similarityResult = sessionHTT.GetImagesSimilarity(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);
            Console.WriteLine("Sim result: " + similarityResult);
            */
            screenshot.SaveAsFile("D:\\testHistoScreenshot.png");
            
            //var screenshot1 = FusionApp.GetScreenshot();
            //screenshot1.SaveAsFile("D:\\fusionAppScreenshot.png");

            //var img = screenshot.AsByteArray.Clone();//new Rectangle(FusionApp.Location, FusionApp.Size), img.PixelFormat);
            //img.Save("D:\\1test.png", System.Drawing.Imaging.ImageFormat.Png);

            //Minimize 3D 
            builder = new Actions(sessionHTT);
            xCoord = 1188 * WinWidth / 1680;
            builder.MoveToElement(FusionApp, xCoord, 22).Click().Build().Perform(); // pixel offset from top left
            //builder.MoveToElement(FusionApp, 313, 193).ClickAndHold().Build().Perform();

            //Study tree
            //"MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.patientPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.studyTreeView"

            Thread.Sleep(TimeSpan.FromSeconds(1));
            //sessionHTT.FindElementByName("HSR^ABDOMEN").Click(); // Exsist 2 with same name
            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.patientPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.studyTreeView").Click();


            //OLD version v3.7.9
            //WindowsElement elementOko = sessionHTT.FindElementByName("VIBE DINAMICO 4 MEDIDAS 20 seg"); // Postoje 2 sa istim imenom - nadje prvi sto je oko
            //builder = new Actions(sessionHTT);
            //builder.MoveToElement(elementOko, 60,20).Click().Build().Perform(); // pixel offset from top left


            //IList<WindowsElement> static trt = sessionHTT.FindElements(By.Name("VIBE DINAMICO 4 MEDIDAS 20 seg"));
            Console.Write(sessionHTT.PageSource);
            File.WriteAllText("D:\\pageSource.xml", sessionHTT.PageSource);


            /*
            //Add Plan button:
            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.patientPage.addPlanButton").Click();
            sessionHTT.FindElementByAccessibilityId("PlanInfoEditWidget.planLineEdit").SendKeys("Plan 1");
            sessionHTT.FindElementByAccessibilityId("PlanInfoEditWidget.physicianLineEdit").SendKeys("Dr 1");
            //Save Plan
            sessionHTT.FindElementByAccessibilityId("PlanInfoEditWidget.savePlanButton").Click();
            */

            //Sidebar
            // "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar"
            //ResetButton: "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.viewResetButton"

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

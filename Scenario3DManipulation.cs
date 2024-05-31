using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace SimulationToolTest
{
    [TestClass]
    public class Scenario3DManipulation : HistoTraningSession
    {
        private string screenFileName = "D:\\HistoFusionAppScreenshot.png";

        [TestMethod]
        [DataRow("new target", 5)]
        public void Manipupation3D(string targetName, int browserPause)
        {
            var app = new ScenarioPatientOpen();
            app.ExpandExsistingPatientList();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            WindowsElement FusionApp = sessionHTT.FindElementByName("Fusion App");
            int x = FusionApp.Location.X;
            int y = FusionApp.Location.Y;
            int dx = FusionApp.Size.Width;
            int dy = FusionApp.Size.Height;


            var builder = new Actions(sessionHTT);
            int xCoord = 1190 * WinWidth / 1680;
            int yCoord = 414 * WinHeigth / 1010;
            builder.MoveToElement(FusionApp, xCoord, yCoord).Click().Build().Perform(); // pixel offset from top left
            /*builder.DragAndDropToOffset(FusionApp, 200, 0).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(3));
            builder.DragAndDropToOffset(FusionApp, 0, 200).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(3));*/

            // Take a screenshot
            //Iz nekog razloga ne mozemo uzeti screenshot Fusion App elementa pa je treba iseci iz cele slike
            Screenshot screenshot = sessionHTT.GetScreenshot();
            //screenshot.SaveAsFile(screenFileName);

            //ConvertTo Image
            Mat pic = new Mat();
            CvInvoke.Imdecode(screenshot.AsByteArray, LoadImageType.AnyColor, pic);
            Rectangle rectangle = new Rectangle(x, y, dx, dy);
            Mat cropped = new Mat(pic, rectangle);
            cropped.Save(screenFileName);

            //Image<Bgr, Byte > img1 = new Image<Bgr, Byte>(screenFileName);

            //var img = screenshot.AsByteArray.Clone();//new Rectangle(FusionApp.Location, FusionApp.Size), img.PixelFormat);
            //img.Save("D:\\1test.png", System.Drawing.Imaging.ImageFormat.Png);

            //Rotate view
            var builder_app = new Actions(sessionHTT);
            xCoord = FusionApp.Size.Width / 2;
            yCoord = FusionApp.Size.Height / 2;
            builder_app.MoveToElement(FusionApp, xCoord, yCoord)
                .Click()
                .ClickAndHold()
                .MoveByOffset(0, yCoord / 2)
                .Release()
                .Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Translate
            builder_app = new Actions(sessionHTT);
            xCoord = FusionApp.Size.Width / 2;
            yCoord = FusionApp.Size.Height / 2;
            builder_app.MoveToElement(FusionApp, xCoord, yCoord)
                .Click()
                .ClickAndHold()
                .MoveByOffset(xCoord / 2, 0)
                .Release()
                .Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));


            /*
            //Minimize 3D 
            builder = new Actions(sessionHTT);
            xCoord = 1188 * WinWidth / 1680;
            yCoord = 22 * WinHeigth / 1100;
            builder.MoveToElement(FusionApp, xCoord, yCoord).Click().Build().Perform();
            //builder.MoveToElement(FusionApp, 313, 193).ClickAndHold().Build().Perform();
            */

            //IList<WindowsElement> static trt = sessionHTT.FindElements(By.Name("VIBE DINAMICO 4 MEDIDAS 20 seg"));

            //Add Target button
            //Button[@Name=\"Add Target\"][@AutomationId=\"PlanWidget.frame.planBodyWidget.addTargetButton\"]"
            sessionHTT.FindElementByAccessibilityId("PlanWidget.frame.planBodyWidget.addTargetButton").Click();
            FusionApp = sessionHTT.FindElementByName("Fusion App");
            //"/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Window[@ClassName=\"QDialog\"][@Name=\"Fusion App\"]/Group[@AutomationId=\"TargetInfoEditWidget\"]/Edit[@AutomationId=\"TargetInfoEditWidget.nameEdit\"]"
            WindowsElement targetNameEdit = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.nameEdit") as WindowsElement;
            targetNameEdit.Click();
            targetNameEdit.Clear();
            targetNameEdit.SendKeys(targetName);
            //WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.saveButton") as WindowsElement;
            WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.startButton") as WindowsElement;
            startTarget.Click();


            
            /*
            //WindowsElement Lateral = sessionHTT.FindElementByName("Treatment Head Angles");
            WindowsElement Lateral = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxAngles.horizontalSliderAngleZ");
            var builder1 = new Actions(sessionHTT);
            xCoord = Lateral.Size.Width / 2;
            int yCord = Lateral.Size.Height / 2;
            builder1.MoveToElement(Lateral, xCoord, yCord).Click().Build().Perform(); // pixel offset from top left
            builder1.DragAndDropToOffset(Lateral, 30, 0).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            WindowsElement CranioCaudal = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxAngles.horizontalSliderAngleX");
            var builder2 = new Actions(sessionHTT);
            //builder2.MoveToElement(CranioCaudal, xCoord, yCord).Click().Build().Perform(); // pixel offset from top left
            builder2.DragAndDropToOffset(CranioCaudal, -40, 0).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            */

            //Sidebar
            // "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar"
            //ResetButton: "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.viewResetButton"

            Thread.Sleep(TimeSpan.FromSeconds(4));
            app.ShutDownMenuClick();
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

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
using Keys = OpenQA.Selenium.Keys;
using OpenQA.Selenium.Appium.MultiTouch;

namespace SimulationToolTest
{
    [TestClass]
    public class Scenario3DManipulation : HistoTraningSession
    {
        private string screenFileName = "D:\\HistoFusionAppScreenshot.png";
        private string screenFileName_1 = "D:\\HistoFusionAppScreenshot_1.png";
        
        [TestMethod]
        [DataRow("new target")]
        public void Manipupation3D(string targetName)
        {
            var app = new ScenarioPatientOpen();
            app.ExpandExsistingPatientList();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            //Add Target button
            //Button[@Name=\"Add Target\"][@AutomationId=\"PlanWidget.frame.planBodyWidget.addTargetButton\"]"
            sessionHTT.FindElementByAccessibilityId("PlanWidget.frame.planBodyWidget.addTargetButton").Click();
            WindowsElement FusionApp = sessionHTT.FindElementByName("Fusion App");
            //"/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Window[@ClassName=\"QDialog\"][@Name=\"Fusion App\"]/Group[@AutomationId=\"TargetInfoEditWidget\"]/Edit[@AutomationId=\"TargetInfoEditWidget.nameEdit\"]"
            WindowsElement targetNameEdit = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.nameEdit") as WindowsElement;
            targetNameEdit.Click();
            targetNameEdit.Clear();
            targetNameEdit.SendKeys(targetName);
            //WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.saveButton") as WindowsElement;
            WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.startButton") as WindowsElement;
            startTarget.Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));

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

            FusionApp = sessionHTT.FindElementByName("Fusion App");
            int x = FusionApp.Location.X;
            int y = FusionApp.Location.Y;
            int dx = FusionApp.Size.Width;
            int dy = FusionApp.Size.Height;

            var builder = new Actions(sessionHTT);
            int xCoord = 1190 * WinWidth / 1680;
            int yCoord = 479 * WinHeigth / 1010;
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
            xCoord = FusionApp.Size.Width / 2 + 30;
            yCoord = FusionApp.Size.Height / 2 + 20;
            /*builder_app.MoveToElement(FusionApp, xCoord, yCoord)
                .Click().KeyDown(Keys.Control).KeyUp(Keys.Control) //to prevent double click
                .ClickAndHold().Click()
                .MoveByOffset(0, yCoord / 2)
                .Release()
                .Build().Perform(); */
            builder_app.MoveToElement(FusionApp, xCoord, yCoord)
                .ClickAndHold()
                .DragAndDropToOffset(FusionApp, 0, yCoord / 4)
                .Release()
                .Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Translate
            builder_app = new Actions(sessionHTT);
            builder_app.MoveToElement(FusionApp, xCoord, yCoord)
                .Click().KeyDown(Keys.Control).KeyUp(Keys.Control) //to prevent double click
                .ClickAndHold()
                .MoveByOffset(xCoord / 4, 0)
                .Release()
                .Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            /*//Right mouse click
            builder_app = new Actions(sessionHTT);
            builder_app.MoveToElement(FusionApp, xCoord, yCoord)
                .ContextClick()
                .Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            */

            /*//Mouse whell do not work
            var tactions = new TouchActions(sessionHTT); //tactions.scroll(10, 10) tactions.perform()
            tactions.Scroll(0, 100).Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1)); */

            // Take a screenshot
            //Iz nekog razloga ne mozemo uzeti screenshot Fusion App elementa pa je treba iseci iz cele slike
            screenshot = sessionHTT.GetScreenshot();

            //ConvertTo Image
            pic = new Mat();
            CvInvoke.Imdecode(screenshot.AsByteArray, LoadImageType.AnyColor, pic);
            Mat cropped_1 = new Mat(pic, rectangle);
            cropped_1.Save(screenFileName_1);

            /*//Difference
            Mat ResultImage = new Mat();
            CvInvoke.AbsDiff(cropped, cropped_1, ResultImage);
            int diff = CvInvoke.CountNonZero(ResultImage);
            Console.WriteLine($"Difference is: {diff.ToString()}");
            */


            //Sidebar
            // "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar"
            //ResetButton: "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.viewResetButton"
            WindowsElement couplingKitButton = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.couplingKitButton");
            couplingKitButton.Click();

            WindowsElement transducerButton = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.transducerButton");
            transducerButton.Click();


            //Minimize 3D 
            builder = new Actions(sessionHTT);
            xCoord = 1188 * WinWidth / 1680;
            yCoord = 22 * WinHeigth / 1100;
            builder.MoveToElement(FusionApp, xCoord, yCoord).Click().Build().Perform();
            //builder.MoveToElement(FusionApp, 313, 193).ClickAndHold().Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));


            //IList<WindowsElement> static trt = sessionHTT.FindElements(By.Name("VIBE DINAMICO 4 MEDIDAS 20 seg"));


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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.ImageComparison;
using System.Threading;
using System.Drawing;
using System.IO;
using Emgu.CV;
using OpenQA.Selenium.Appium;
using Emgu.CV.CvEnum;

namespace SimulationToolTest
{

    [TestClass]
    public class ScenarioTargetSave : HistoTraningSession
    {
        private string targetName = "new target";
        private string screenFileName = "D:\\testHistoScreenshot_1.png";
        private string croppedFileName = "D:\\cropped.jpg";

        [TestMethod]
        public void SaveTarget()
        {
            var app = new ScenarioPatientOpen();
            app.ExpandExsistingPatientList();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            //Add Target button
            //Button[@Name=\"Add Target\"][@AutomationId=\"PlanWidget.frame.planBodyWidget.addTargetButton\"]"
            sessionHTT.FindElementByAccessibilityId("PlanWidget.frame.planBodyWidget.addTargetButton").Click();
            WindowsElement FusionApp = sessionHTT.FindElementByName("Fusion App");
            //Console.WriteLine("Saving element to D:\\Fusion_App.png");
            //var screenshot1 = FusionApp.GetScreenshot();
            //screenshot1.SaveAsFile("D:\\Fusion_App.png");

            //"/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Window[@ClassName=\"QDialog\"][@Name=\"Fusion App\"]/Group[@AutomationId=\"TargetInfoEditWidget\"]/Edit[@AutomationId=\"TargetInfoEditWidget.nameEdit\"]"
            WindowsElement targetNameEdit = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.nameEdit") as WindowsElement;
            targetNameEdit.Click();
            targetNameEdit.Clear();
            targetNameEdit.SendKeys(targetName);
            //WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.saveButton") as WindowsElement;
            WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.startButton") as WindowsElement;
            startTarget.Click();

            //This is DICOM now
            FusionApp = sessionHTT.FindElementByName("Fusion App");
            Console.WriteLine($"FusionApp size {FusionApp.Rect.ToString()}");
            int x = FusionApp.Location.X + FusionApp.Size.Width / 2;
            int y = FusionApp.Location.Y + FusionApp.Size.Height / 2;
            int dx = FusionApp.Size.Width/2;
            int dy = FusionApp.Size.Height/2;

            var builder_app = new Actions(sessionHTT);
            int xCoord = 295 * WinWidth / 1680;
            int yCord = 219 * WinHeigth / 1010;
            builder_app.MoveToElement(FusionApp, xCoord, yCord)
                .Click()
                .ClickAndHold()
                .MoveByOffset(0, -80)
                .Release()
                .Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            builder_app = new Actions(sessionHTT);
            xCoord = 301 * WinWidth / 1680;
            yCord = 228 * WinHeigth / 1010;
            builder_app.MoveToElement(FusionApp, xCoord, yCord)
                .ClickAndHold()
                .MoveByOffset(0, -70)
                .Release()
                .Build().Perform();
            //builder_app.DragAndDropToOffset(FusionApp, 0, -100).Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //WindowsElement Lateral = sessionHTT.FindElementByName("Treatment Head Angles");
            WindowsElement Lateral = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxAngles.horizontalSliderAngleZ");
            var builder = new Actions(sessionHTT);
            xCoord = Lateral.Size.Width / 2;
            yCord = Lateral.Size.Height / 2;
            builder.MoveToElement(Lateral, xCoord, yCord).Click().Build().Perform(); // pixel offset from top left
            builder.DragAndDropToOffset(Lateral, 30, 0).Perform();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            WindowsElement CranioCaudal = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxAngles.horizontalSliderAngleX");
            var builder1 = new Actions(sessionHTT);
            //builder1.MoveToElement(CranioCaudal, xCoord, yCord).Click().Build().Perform(); // pixel offset from top left
            builder1.DragAndDropToOffset(CranioCaudal, -40, 0).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            WindowsElement DeviceRoll = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxAngles.horizontalSliderAngleY");
            var builder2 = new Actions(sessionHTT);
            //builder2.MoveToElement(CranioCaudal, xCoord, yCord).Click().Build().Perform(); // pixel offset from top left
            builder2.DragAndDropToOffset(DeviceRoll, 5, 0).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Z-
            WindowsElement PlanPoint = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.sevenPointWidget");
            var builder3 = new Actions(sessionHTT);
            builder3.MoveToElement(PlanPoint, 52, 15).Click().Build().Perform(); // pixel offset from top left
            Thread.Sleep(TimeSpan.FromSeconds(1));
            //Console.WriteLine("Saving element to D:\\Plan_Point.png");
            //var screenshot = PlanPoint.GetScreenshot();
            //screenshot.SaveAsFile("D:\\Plan_Point.png");

            //Y-
            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.widget.comboBoxSetFocus").Click();
            sessionHTT.FindElementByName("Y-").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Y+
            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.widget.comboBoxSetFocus").Click();
            sessionHTT.FindElementByName("Y+").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            
            //Center
            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.widget.comboBoxSetFocus").Click();
            sessionHTT.FindElementByName("Center").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            // Take a screenshot
            Screenshot screenshot = sessionHTT.GetScreenshot();
            /*
            var options = new SimilarityMatchingOptions { Visualize = true };
            var similarityResult = sessionHTT.GetImagesSimilarity(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);
            Console.WriteLine("Sim result: " + similarityResult);
            */
            screenshot.SaveAsFile(screenFileName);
            //Mora ovde da se uhvati screen jer se ImFusion prozor smanji

            sessionHTT.FindElementByName("Accept Planned Target(s)").Click();

            //Click to Menu
            sessionHTT.FindElementByClassName("QToolButton").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Fusion App - Window !!Napravi posebnu app na Desktop-u
            FusionApp = sessionRoot.FindElementByName("Fusion App");
            WindowsElement shtMeny = FusionApp.FindElementByName("Save") as WindowsElement;
            shtMeny.Click();


            //Crop image
            Mat pic = new Mat();
            pic = CvInvoke.Imread(screenFileName, LoadImageType.AnyColor);

            Rectangle rectangle = new Rectangle(x, y, dx, dy);
            Mat cropped = new Mat(pic, rectangle);
            
            cropped.Save(croppedFileName);


            Bitmap bMap = Bitmap.FromFile(croppedFileName) as Bitmap;
            PictureAnalysis.GetMostUsedColor(bMap);
            Color MostUsed = PictureAnalysis.MostUsedColor;
            Console.WriteLine("Najkoriscenija boja na slici " + MostUsed);
            Console.WriteLine("10 Najkoriscenijih boja na slici: " + PictureAnalysis.MostUsedColorIncidence.ToString());
            List<Color> lista = PictureAnalysis.TenMostUsedColors;
            int redColor = 0;
            int greenColor = 0;
            foreach (Color color in lista)
            {
                redColor += color.R;
                greenColor += color.G;
                Console.WriteLine(color.ToString());
            }
            if (redColor > greenColor)
            {
                Console.WriteLine("Preovladjuje CRVENA boja!");
            } else if (greenColor > redColor)
            {
                Console.WriteLine("Preovladjuje ZELENA boja!");
            } else
            {
                Console.WriteLine("Preovladjuje siva boja!");
            }
            //Sidebar
            // "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar"
            //ResetButton: "MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.viewResetButton"

            Thread.Sleep(TimeSpan.FromSeconds(1));
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

    public static class PictureAnalysis
    {
        public static List<Color> TenMostUsedColors { get; private set; }
        public static List<int> TenMostUsedColorIncidences { get; private set; }

        public static Color MostUsedColor { get; private set; }
        public static int MostUsedColorIncidence { get; private set; }

        private static int pixelColor;

        private static Dictionary<int, int> dctColorIncidence;

        public static void GetMostUsedColor(Bitmap theBitMap)
        {
            TenMostUsedColors = new List<Color>();
            TenMostUsedColorIncidences = new List<int>();

            MostUsedColor = Color.Empty;
            MostUsedColorIncidence = 0;

            // does using Dictionary<int,int> here
            // really pay-off compared to using
            // Dictionary<Color, int> ?

            // would using a SortedDictionary be much slower, or ?

            dctColorIncidence = new Dictionary<int, int>();

            // this is what you want to speed up with unmanaged code
            for (int row = 0; row < theBitMap.Size.Width; row++)
            {
                for (int col = 0; col < theBitMap.Size.Height; col++)
                {
                    pixelColor = theBitMap.GetPixel(row, col).ToArgb();

                    if (dctColorIncidence.Keys.Contains(pixelColor))
                    {
                        dctColorIncidence[pixelColor]++;
                    }
                    else
                    {
                        dctColorIncidence.Add(pixelColor, 1);
                    }
                }
            }

            // note that there are those who argue that a
            // .NET Generic Dictionary is never guaranteed
            // to be sorted by methods like this
            var dctSortedByValueHighToLow = dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // this should be replaced with some elegant Linq ?
            foreach (KeyValuePair<int, int> kvp in dctSortedByValueHighToLow.Take(10))
            {
                TenMostUsedColors.Add(Color.FromArgb(kvp.Key));
                TenMostUsedColorIncidences.Add(kvp.Value);
            }

            MostUsedColor = Color.FromArgb(dctSortedByValueHighToLow.First().Key);
            MostUsedColorIncidence = dctSortedByValueHighToLow.First().Value;
        }

    }

}

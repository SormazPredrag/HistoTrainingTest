using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.OCR;
using Microsoft.JScript;

namespace SimulationToolTest
{

    [TestClass]
    public class ScenarioTreatmentHead : HistoTraningSession
    {
        private string targetName = "new target";
        private string targetName1 = "target 1";
        private string screenFileName = "D:\\testTreatmentHead.png";
        private string transducerName = "8-14 cm";
        private string dataPath = "D:\\Users\\Luka\\source\\repos\\WinAppDriver\\Samples\\C#\\HistoTrainingTest\\tessdata";

        [TestMethod]
        public void TreatmentHeadChange()
        {
            var app = new ScenarioPatientOpen();
            app.ExpandExsistingPatientList();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            //Add Target button
            //Button[@Name=\"Add Target\"][@AutomationId=\"PlanWidget.frame.planBodyWidget.addTargetButton\"]"
            sessionHTT.FindElementByAccessibilityId("PlanWidget.frame.planBodyWidget.addTargetButton").Click();

            //WindowsElement FusionApp = sessionHTT.FindElementByName("Fusion App");
            string xpathFusion = "//Window[@ClassName=\"QDialog\"][@Name=\"Fusion App\"]/Group[@AutomationId=\"TargetInfoEditWidget\"]";
            WindowsElement FusionApp = sessionHTT.FindElementByXPath(xpathFusion);
            WindowsElement targetNameEdit = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.nameEdit") as WindowsElement;
            targetNameEdit.Click();
            targetNameEdit.Clear();
            targetNameEdit.SendKeys(targetName);
            //WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.saveButton") as WindowsElement;
            WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.startButton") as WindowsElement;
            startTarget.Click();

            //Current Target edit
            WindowsElement CurrentTargetButton = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.toolButtonTarget");
            CurrentTargetButton.Click();

            //Fusion App Edit Name - menu
            FusionApp = sessionRoot.FindElementByName("Fusion App");
            var EditName = FusionApp.FindElementByName("Edit Name");
            //string xpath_LeftClickButton = "//Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Group[@AutomationId=\"MainWindow.centralwidget\"]/Custom[@AutomationId=\"MainWindow.centralwidget.stackedWidget\"]/Group[@AutomationId=\"MainWindow.centralwidget.stackedWidget.patientRecordPage\"]/Custom[starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]/Group[starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]/Group[starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]/Group[starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]/Group[starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]/Custom[starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]/Group[starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]/Button[@Name=\"...\"][starts-with(@AutomationId,\"MainWindow.centralwidget.stackedWidget.patientRecordPage.stacked\")]";
            //var EditName = sessionHTT.FindElementByXPath(xpath_LeftClickButton);
            EditName.Click();

            //Edit Target Name
            string xpath_LeftClickEditEdittarget = "//Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Window[@ClassName=\"QInputDialog\"][@Name=\"Target Treatment\"]/Edit[@ClassName=\"QLineEdit\"][@Name=\"Edit target name:\"]";
            var winElem_LeftClickEditEdittarget = sessionHTT.FindElementByXPath(xpath_LeftClickEditEdittarget);
            winElem_LeftClickEditEdittarget.Click();
            winElem_LeftClickEditEdittarget.Clear();
            winElem_LeftClickEditEdittarget.SendKeys(targetName1);

            //OK
            string xpath_LeftClickButtonOK = "//Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Window[@ClassName=\"QInputDialog\"][@Name=\"Target Treatment\"]/Group[@ClassName=\"QDialogButtonBox\"]/Button[@ClassName=\"QPushButton\"][@Name=\"OK\"]";
            var winElem_LeftClickButtonOK = sessionHTT.FindElementByXPath(xpath_LeftClickButtonOK);
            winElem_LeftClickButtonOK.Click();

            //Current Target
            WindowsElement CurrentTarget = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.comboBoxTarget");
            Console.WriteLine($"!!!DONT WORK IN APLICATION Target Edit Name: {CurrentTarget.Text}");
            //Assert.AreEqual(CurrentTarget.Text, targetName1);

            //Treatment Head - Transducer
            var THElement = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.treatmentHeadWidget.comboBoxTransducer");
            THElement.Click();
            //Name:	"2-12 cm"
            sessionHTT.FindElementByName(transducerName).Click();

            THElement = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.treatmentHeadWidget.comboBoxTransducer");
            Console.WriteLine($"Transduser set to: {THElement.Text}");
            Assert.AreEqual(THElement.Text, transducerName);

            //Proveriti da li se TH pomerio
            Screenshot screenshot = THElement.GetScreenshot();
            /*
            var options = new SimilarityMatchingOptions { Visualize = true };
            var similarityResult = sessionHTT.GetImagesSimilarity(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);
            Console.WriteLine("Sim result: " + similarityResult);
            */
            //screenshot.SaveAsFile(screenFileName);

            //Convert Screenshot to OpenCV Mat
            Mat pic = new Mat();
            //pic = CvInvoke.Imread(screenFileName, LoadImageType.AnyColor);
            CvInvoke.Imdecode(screenshot.AsByteArray, LoadImageType.AnyColor, pic);
            //Resize image for OCR
            CvInvoke.Resize(pic, pic, new System.Drawing.Size(pic.Width*2, pic.Height*2));

            //To gray color
            //pic.Save("D:\\mrt.png");
            var img1 = pic.ToImage<Gray, Byte>();

            //ConvertTo Image
            //Image<Bgr, Byte > img1 = new Image<Bgr, Byte>(screenFileName);
            //Image<Gray, Byte> img1 = new Image<Gray, Byte>("D:\\mrt.jpg");

            //Create OCR engine
            Tesseract _ocr;
            _ocr = new Tesseract(dataPath, "eng", OcrEngineMode.TesseractCubeCombined);
            //_ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwyz-1234567890");
            _ocr.SetVariable("tessedit_char_whitelist", "abcdefghijklmnopqrstuvwyz-1234567890");
            //_ocr.PageSegMode = PageSegMode.SingleBlock;
            //Console.WriteLine(Tesseract.Version);
            img1._ThresholdBinary(new Gray(127), new Gray(255));
            //img1.Save("D:\\mrt.png");

            try
            {
                //TEXT IS TO SMALL for OCR
                //recognize the text
                _ocr.Recognize(img1);
                //get the text
                string result = _ocr.GetText();
                Console.WriteLine($"OCR text {result}");
            } catch (Exception ex)
            {

            }

            //Focal Steering SPINNER
            var SpinnerElement = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.treatmentHeadWidget.spinBoxSteering");
            var builder = new Actions(sessionHTT);
            builder.MoveToElement(SpinnerElement, SpinnerElement.Size.Width / 2, SpinnerElement.Size.Height / 3).Click().Build().Perform();
            Thread.Sleep(100);
            builder.MoveToElement(SpinnerElement, SpinnerElement.Size.Width / 2, SpinnerElement.Size.Height / 3).Click().Build().Perform();
            Thread.Sleep(100);
            builder = new Actions(sessionHTT);
            builder.MoveToElement(SpinnerElement, SpinnerElement.Size.Width / 2, SpinnerElement.Size.Height - 10).Click().Build().Perform();
            SpinnerElement = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.treatmentHeadWidget.spinBoxSteering");
            Console.WriteLine($"Fosal Steering: {SpinnerElement.Text}");
            //Assert.AreEqual(SpinnerElement.Text, "10 mm");

            //Planned Treatment Volume
            var PTVElementX = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.doubleSpinBoxVolumeX");
            builder = new Actions(sessionHTT);
            builder.MoveToElement(PTVElementX, PTVElementX.Size.Width / 2, PTVElementX.Size.Height/ 3).Click().Click().Click().Build().Perform(); //3 clicks
            Thread.Sleep(100);
            Console.WriteLine($"X: {PTVElementX.Text}");
            Assert.AreEqual(PTVElementX.Text, "23.0 mm");

            var PTVElementY = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.doubleSpinBoxVolumeY");
            builder = new Actions(sessionHTT);
            builder.MoveToElement(PTVElementY, PTVElementY.Size.Width / 2, PTVElementY.Size.Height - 10).Click().Build().Perform();
            Thread.Sleep(100);
            Console.WriteLine($"Y: {PTVElementY.Text}");
            Assert.AreEqual(PTVElementY.Text, "19.0 mm");

            var PTVElementZ = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.doubleSpinBoxVolumeZ");
            builder = new Actions(sessionHTT);
            builder.MoveToElement(PTVElementZ, PTVElementZ.Size.Width / 2, PTVElementZ.Size.Height / 3).Click().Click().Build().Perform(); //2 clicks
            Thread.Sleep(100);
            Console.WriteLine($"Y: {PTVElementZ.Text}");
            Assert.AreEqual(PTVElementZ.Text, "22.0 mm");

            //Margin 2 up + 1 down
            var PTVElementM = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.doubleSpinBoxMargin");
            builder = new Actions(sessionHTT);
            builder.MoveToElement(PTVElementM, PTVElementM.Size.Width / 2, PTVElementM.Size.Height / 3).Click().Click().Build().Perform(); //2 clicks
            Thread.Sleep(100);
            builder = new Actions(sessionHTT);
            builder.MoveToElement(PTVElementM, PTVElementM.Size.Width / 2, PTVElementM.Size.Height - 10).Click().Build().Perform();
            Thread.Sleep(100);
            Console.WriteLine($"Y: {PTVElementM.Text}");
            Assert.AreEqual(PTVElementM.Text, "6.0 mm");

            //Setup
            //Patient Rotation
            WindowsElement PatientRotation = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.groupBox_2.horizontalSliderPatientRotation");
            builder = new Actions(sessionHTT);
            builder.DragAndDropToOffset(PatientRotation, 20, 0).Perform();
            Thread.Sleep(100);

            //Wather Level
            WindowsElement WatherLevel = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.groupBox_2.horizontalSliderWaterLevel");
            builder = new Actions(sessionHTT);
            builder.DragAndDropToOffset(WatherLevel, -15, 0).Perform();
            Thread.Sleep(100);

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

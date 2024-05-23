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

namespace SimulationToolTest
{

    [TestClass]
    public class ScenarioTargetSave : HistoTraningSession
    {
        private string targetName = "New Target";

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

            //WindowsElement Lateral = sessionHTT.FindElementByName("Treatment Head Angles");
            WindowsElement Lateral = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxAngles.horizontalSliderAngleZ");
            var builder = new Actions(sessionHTT);
            int xCoord = Lateral.Size.Width / 2;
            int yCord = Lateral.Size.Height / 2;
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

            sessionHTT.FindElementByName("Accept Planned Target(s)").Click();

            //Click to Menu
            sessionHTT.FindElementByClassName("QToolButton").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Fusion App - Window !!Napravi posebnu app na Desktop-u
            FusionApp = sessionRoot.FindElementByName("Fusion App");
            WindowsElement shtMeny = FusionApp.FindElementByName("Save") as WindowsElement;
            shtMeny.Click();


            // Take a screenshot
            Screenshot screenshot = sessionHTT.GetScreenshot();
            /*
            var options = new SimilarityMatchingOptions { Visualize = true };
            var similarityResult = sessionHTT.GetImagesSimilarity(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);
            Console.WriteLine("Sim result: " + similarityResult);
            */
            screenshot.SaveAsFile("D:\\testHistoScreenshot_1.png");


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

}

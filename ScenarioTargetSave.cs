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
            var builder1 = new Actions(sessionHTT);
            int xCoord = Lateral.Size.Width / 2;
            int yCord = Lateral.Size.Height / 2;
            builder1.MoveToElement(Lateral, xCoord, yCord).Click().Build().Perform(); // pixel offset from top left
            builder1.DragAndDropToOffset(Lateral, 30, 0).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            WindowsElement CranioCaudal = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxAngles.horizontalSliderAngleX");
            var builder2 = new Actions(sessionHTT);
            //builder2.MoveToElement(CranioCaudal, xCoord, yCord).Click().Build().Perform(); // pixel offset from top left
            builder2.DragAndDropToOffset(CranioCaudal, -40, 0).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Z-
            WindowsElement PlanPoint = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.targetControlsWidget.groupBoxVolume.sevenPointWidget");
            var builder3 = new Actions(sessionHTT);
            builder3.MoveToElement(PlanPoint, 52, 15).Click().Build().Perform(); // pixel offset from top left
            Thread.Sleep(TimeSpan.FromSeconds(1));

            sessionHTT.FindElementByName("Accept Planned Target(s)").Click();

            //Click to Menu
            sessionHTT.FindElementByClassName("QToolButton").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Fusion App - Window !!Napravi posebnu app na Desktop-u
            FusionApp = sessionRoot.FindElementByName("Fusion App");
            WindowsElement shtMeny = FusionApp.FindElementByName("Save") as WindowsElement;
            shtMeny.Click();

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

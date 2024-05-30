using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationToolTest
{
    [TestClass]
    public class ScenarioPatientRotate : HistoTraningSession
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

            //Setup
            //Patient Rotation
            WindowsElement PatientRotation = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.groupBox_2.horizontalSliderPatientRotation");
            var builder = new Actions(sessionHTT);
            builder.DragAndDropToOffset(PatientRotation, 30, 0).Perform();
            Thread.Sleep(100);
            WindowsElement PatientAngle = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.groupBox_2.labelPatientRotation");
            Console.WriteLine($"Patient Angle is: {PatientAngle.Text}");
            Thread.Sleep(1000);

            //Click to reset angle
            //AutomationId:	
            WindowsElement PatientAngleReset = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.groupBox_2.resetPatientRotationButton");
            PatientAngleReset.Click();

            //Wather Level
            WindowsElement WatherLevel = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.stackedWidget.treatmentPlanPage.scrollArea.qt_scrollarea_viewport.scrollAreaWidgetContents.treatmentPlanControllerBg.TreatmentPlanController.groupBox_2.horizontalSliderWaterLevel");
            builder = new Actions(sessionHTT);
            builder.DragAndDropToOffset(WatherLevel, -15, 0).Perform();
            Thread.Sleep(100);

            app.ShutDownMenuClick();
        }
    }
}

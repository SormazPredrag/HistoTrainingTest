using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationToolTest
{
    [TestClass]
    public class ScenarioScreenShot : HistoTraningSession
    {
        private string saveFilename = "Screen-1.png";
        [TestMethod]
        public void ClickScreenShot()
        {
            var app = new ScenarioPatientOpen();
            //app.ExpandExsistingPatientList();

            app.LoadExsistingPatient();

            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.ellipsisButton").Click();

            //Click Screen Shot
            WindowsElement FusionApp = sessionRoot.FindElementByXPath("//Window[@ClassName='QMenu'][@Name='Fusion App']//MenuItem[@ClassName='QWidgetAction']");
            var builder1 = new Actions(sessionRoot);
            builder1.MoveToElement(FusionApp, 34, 27).Click().Build().Perform();

            sessionHTT.FindElementByXPath("//Pane[@ClassName='DUIViewWndClassName']//ComboBox[@Name='File name:'][@AutomationId='FileNameControlHost']//Edit[@ClassName='Edit'][@Name='File name:']").Click();
            sessionHTT.FindElementByXPath("//Pane[@ClassName='DUIViewWndClassName']//ComboBox[@Name='File name:'][@AutomationId='FileNameControlHost']//Edit[@ClassName='Edit'][@Name='File name:']").SendKeys(SanitizeBackslashes("%USERPROFILE%\\Desktop"));
            sessionHTT.FindElementByXPath("//Button[@ClassName='Button'][@Name='Save']").Click();

            sessionHTT.FindElementByXPath("//Pane[@ClassName='DUIViewWndClassName']//ComboBox[@Name='File name:'][@AutomationId='FileNameControlHost']//Edit[@ClassName='Edit'][@Name='File name:']").SendKeys(SanitizeBackslashes(saveFilename));
            sessionHTT.FindElementByXPath("//Button[@ClassName='Button'][@Name='Save']").Click();

            try {
                sessionHTT.FindElementByXPath("//Button[@Name='Yes'][starts-with(@AutomationId,'CommandButton_')]").Click();
            }
            catch { }

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

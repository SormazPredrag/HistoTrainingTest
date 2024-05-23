using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
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
    public class ScenarioScreenShot : HistoTraningSession
    {
        private string saveFilename = "Screen-1.png";
        private const string TargetSaveLocation = @"%USERPROFILE%\Desktop";
        private const string ExplorerAppId = @"C:\Windows\System32\explorer.exe";

        [TestMethod]
        public void ClickScreenShot()
        {
            var app = new ScenarioPatientOpen();
            
            app.ExpandExsistingPatientList();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //app.LoadExsistingPatient();


            //Add Target button
            //Button[@Name=\"Add Target\"][@AutomationId=\"PlanWidget.frame.planBodyWidget.addTargetButton\"]"
            sessionHTT.FindElementByAccessibilityId("PlanWidget.frame.planBodyWidget.addTargetButton").Click();
            WindowsElement FusionApp = sessionHTT.FindElementByName("Fusion App");
            //"/Pane[@ClassName=\"#32769\"][@Name=\"Desktop 1\"]/Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Window[@ClassName=\"QDialog\"][@Name=\"Fusion App\"]/Group[@AutomationId=\"TargetInfoEditWidget\"]/Edit[@AutomationId=\"TargetInfoEditWidget.nameEdit\"]"
            WindowsElement targetNameEdit = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.nameEdit") as WindowsElement;
            targetNameEdit.Click();
            targetNameEdit.Clear();
            targetNameEdit.SendKeys("New Target");
            //WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.saveButton") as WindowsElement;
            WindowsElement startTarget = FusionApp.FindElementByAccessibilityId("TargetInfoEditWidget.startButton") as WindowsElement;
            startTarget.Click();

            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRecordPage.displayAndControlsWidget.displayStackedWidget.displayPageContainer.viewSidebar.ellipsisButton").Click();

            //Click Screen Shot
            FusionApp = sessionRoot.FindElementByXPath("//Window[@ClassName='QMenu'][@Name='Fusion App']//MenuItem[@ClassName='QWidgetAction']");
            var builder1 = new Actions(sessionRoot);
            builder1.MoveToElement(FusionApp, 34, 27).Click().Build().Perform();

            sessionHTT.FindElementByXPath("//Pane[@ClassName='DUIViewWndClassName']//ComboBox[@Name='File name:'][@AutomationId='FileNameControlHost']//Edit[@ClassName='Edit'][@Name='File name:']").Click();
            sessionHTT.FindElementByXPath("//Pane[@ClassName='DUIViewWndClassName']//ComboBox[@Name='File name:'][@AutomationId='FileNameControlHost']//Edit[@ClassName='Edit'][@Name='File name:']").SendKeys(SanitizeBackslashes(TargetSaveLocation));
            sessionHTT.FindElementByXPath("//Button[@ClassName='Button'][@Name='Save']").Click();

            sessionHTT.FindElementByXPath("//Pane[@ClassName='DUIViewWndClassName']//ComboBox[@Name='File name:'][@AutomationId='FileNameControlHost']//Edit[@ClassName='Edit'][@Name='File name:']").SendKeys(SanitizeBackslashes(saveFilename));
            sessionHTT.FindElementByXPath("//Button[@ClassName='Button'][@Name='Save']").Click();
            //sessionHTT.FindElementByXPath("//Button[@ClassName='Button'][@Name='Cancel']").Click();

            try {
                sessionHTT.FindElementByXPath("//Button[@Name='Yes'][starts-with(@AutomationId,'CommandButton_')]").Click();
            }
            catch { }

            app.ShutDownMenuClick();
        }

        [TestMethod]
        public void explorerCheck()
        {
            // Create a Windows Explorer session to delete the saved text file above
            /*DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ExplorerAppId);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            WindowsDriver<WindowsElement> windowsExplorerSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            */

            var appiumOptionsE = new OpenQA.Selenium.Appium.AppiumOptions();
            appiumOptionsE.AddAdditionalCapability("app", ExplorerAppId);
            appiumOptionsE.AddAdditionalCapability("deviceName", "WindowsPC");
            WindowsDriver<WindowsElement> windowsExplorerSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptionsE);
            Thread.Sleep(500);
            Assert.IsNotNull(windowsExplorerSession);

            // Navigate Windows Explorer to the target save location folder
            windowsExplorerSession.Keyboard.SendKeys(Keys.Alt + "d" + Keys.Alt + SanitizeBackslashes(TargetSaveLocation) + Keys.Enter);

            // Verify that the file is indeed saved in the working directory and delete it
            windowsExplorerSession.FindElementByAccessibilityId("SearchEditBox").SendKeys(saveFilename + Keys.Enter);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            WindowsElement testFileEntry = null;
            try
            {
                testFileEntry = windowsExplorerSession.FindElementByName("Items View").FindElementByName(saveFilename) as WindowsElement;  // In case extension is added automatically
            }
            catch
            {
                try
                {
                    testFileEntry = windowsExplorerSession.FindElementByName("Items View").FindElementByName(saveFilename) as WindowsElement;
                }
                catch { }
            }
            
            // Delete the test file when it exists
            if (testFileEntry != null)
            {
                testFileEntry.Click();
                testFileEntry.SendKeys(Keys.Delete);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            
            windowsExplorerSession.Quit();
            windowsExplorerSession = null;

            Assert.IsNotNull(testFileEntry);
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

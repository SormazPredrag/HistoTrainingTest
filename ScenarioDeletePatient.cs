using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
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
    public class ScenarioDeletePatient : HistoTraningSession 
    {
        private string LastNameSearch = "HISTO PHANTOM 1";

        [FindsBy(How = How.Name, Using = "HISTO PHANTOM 1")]
        private WindowsElement _lastName;


        [TestMethod]
        public void DeletePatient()
        {

            //Patient list
            WindowsElement PatinentList = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.modelView");
            WindowsElement phantom = PatinentList.FindElementByName(LastNameSearch) as WindowsElement;
            //WindowsElement phantom = sessionHTT.FindElementByName(LastNameSearch);
            phantom.Click();

            WindowsElement phantomLast = sessionHTT.FindElementByName("22 Apr 2024");
            var builder = new Actions(sessionHTT);
            int xCoord = 126 * WinWidth / 1680;
            builder.MoveToElement(phantomLast, xCoord, 32).Click().Build().Perform();

            Thread.Sleep(200);
            sessionHTT.FindElementByName("Yes").Click();
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

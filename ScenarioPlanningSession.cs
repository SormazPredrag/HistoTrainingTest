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
    public class ScenarioPlaningSession : HistoTraningSession
    {
        //private static string patientId = "02-006";
        //private static string FirstNameSearch = "Example_1";
        //private string LastNameSearch = "Histosonics";

        private static string patientId = "01012000V201";
        private static string FirstNameSearch = "";
        private string LastNameSearch = "V201";

        [TestMethod]
        public void ExpandExsistingPatientList()
        {
            //Search Name
            //sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.firstNameLineEdit").Clear();
            //sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.firstNameLineEdit").SendKeys("Example_1");
            //sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.firstNameLineEdit").SendKeys(""); //AutomationId: 
            Thread.Sleep(100);
            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.lastNameLineEdit").SendKeys(LastNameSearch); //AutomationId: 

            //Verification - Patient Registration
            //Double click on list
            //sessionHTT.FindElementByName("Example_1").Click(); sessionHTT.FindElementByName("Example_1").Click();
            WindowsElement example = sessionHTT.FindElementByName(patientId);
            //sessionHTT.FindElementByName(FirstNameSearch);
            sessionHTT.FindElementByName(LastNameSearch);
            //Today imported files
            DateTime now = DateTime.Now;
            sessionHTT.FindElementByName(now.ToString("dd MMM yyyy"));

            example.Click();
            /*
            //Expand patient
            //Actions act = new Actions(session2);
            //act.DoubleClick(example).Perform();
            var builder = new Actions(sessionHTT);
            builder.MoveToElement(example, -11, 30).Click().Build().Perform();

            //Series click
            WindowsElement seriesElement = sessionHTT.FindElementByName("Series");
            builder = new Actions(sessionHTT);
            builder.MoveToElement(seriesElement, -11, 30).Click().Build().Perform();

            sessionHTT.FindElementByName("01012000V201 - Series 5001 : axial T1 vibe_rad outPh B1-Ps 1,2mm").Click();
            */

            LoadExsistingPatient();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            // Ovo je samo imFusion element ali ne vidim elemente u njemu
            // LeftClick on Pane "Fusion App" at (1164,696)
            Console.WriteLine("LeftClick on Pane \"Fusion App\" at (1164,696)");
            string xpath_LeftClickPaneFusionApp_1164_696 = "//Pane[@ClassName='Qt51515QWindowOwnDCIcon'][@Name='Fusion App']";
            var winElem_LeftClickPaneFusionApp_1164_696 = sessionHTT.FindElementByXPath(xpath_LeftClickPaneFusionApp_1164_696);
            if (winElem_LeftClickPaneFusionApp_1164_696 != null)
            {
                //winElem_LeftClickPaneFusionApp_1164_696.Click();
                var builder1 = new Actions(sessionHTT);
                builder1.MoveToElement(winElem_LeftClickPaneFusionApp_1164_696, 1355, 395).Click().Build().Perform();
                Console.WriteLine(winElem_LeftClickPaneFusionApp_1164_696.Rect.ToString());
            }
            else
            {
                Console.WriteLine($"Failed to find element using xpath: {xpath_LeftClickPaneFusionApp_1164_696}");
                return;
            }

        }

        public void LoadExsistingPatient()
        {
            //Old version v3.7.9
            //sessionHTT.FindElementByName(patientId).Click();
            sessionHTT.FindElementByName("Go to Patient Record").Click();
            //sessionHTT.FindElementByName("Start Planning Session").Click();

            /*
            //Bug HT v3.8.1 Physician can be empty!!!
            //sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.planningSessionSetupPage.PhysicianLineEdit").SendKeys("can be empty");
            //SendKeys have problem with uppercase!!!

            //Liver Checkbox
            //Checkboxes with same xpath
            string xpath_LeftClickCheckBox_14_11 = "//Window[@Name=\"Histosonics Training Tool\"][@AutomationId=\"MainWindow\"]/Group[@AutomationId=\"MainWindow.centralwidget\"]/Custom[@AutomationId=\"MainWindow.centralwidget.stackedWidget\"]/Group[@AutomationId=\"MainWindow.centralwidget.stackedWidget.planningSessionSetupPage\"]/Group[@ClassName=\"QWidget\"]/CheckBox[@ClassName=\"QCheckBox\"]";
            var winElem_LeftClickCheckBox_14_11 = sessionHTT.FindElementsByXPath(xpath_LeftClickCheckBox_14_11);
            for (int i = 0; i < winElem_LeftClickCheckBox_14_11.Count; i++)
            {
                if (i == 0)
                {
                    //Ovo sam deaktivirao jer kod mene cesto pukne program
                    //winElem_LeftClickCheckBox_14_11[i].Click();
                }
            }

            sessionHTT.FindElementByName("Launch Planning Session").Click();
            */
            Thread.Sleep(TimeSpan.FromSeconds(5));

        }

        [TestMethod]
        public void ShutDownMenuClick()
        {
            //Click to Menu
            sessionHTT.FindElementByClassName("QToolButton").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Fusion App - Window !!Napravi posebnu app na Desktop-u
            WindowsElement FusionApp = sessionRoot.FindElementByName("Fusion App");
            WindowsElement shtMeny = FusionApp.FindElementByName("Shut Down") as WindowsElement;
            shtMeny.Click();

            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            sessionHTT.FindElementByName("Yes").Click();

            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            sessionHTT.FindElementByName("No").Click();
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

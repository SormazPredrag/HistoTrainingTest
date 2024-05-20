//******************************************************************************
//
// Copyright (c) 2017 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using System;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;

namespace SimulationToolTest
{
    [TestClass]
    public class ScenarioPatientOpen : HistoTraningSession
    {
        private static string patientId = "02-006";
        private static string FirstNameSearch = "Example_1";
        private string LastNameSearch = "Histosonics";

        [TestMethod]
        public void ExpandExsistingPatientList()
        {
            //Search Name
            //sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.firstNameLineEdit").Clear();
            //sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.firstNameLineEdit").SendKeys("Example_1");
            //sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.firstNameLineEdit").SendKeys(""); //AutomationId: 
            Thread.Sleep(100);
            sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.patientRecordListView.patientFilteringWidget.lastNameLineEdit").SendKeys(LastNameSearch); //AutomationId: 

            //Double click on list
            //sessionHTT.FindElementByName("Example_1").Click(); sessionHTT.FindElementByName("Example_1").Click();
            WindowsElement example = sessionHTT.FindElementByName("02-006");
            sessionHTT.FindElementByName(FirstNameSearch);
            sessionHTT.FindElementByName(LastNameSearch);
            sessionHTT.FindElementByName("01 Jan 1900");
            
            //Actions act = new Actions(session2);
            //act.DoubleClick(example).Perform();
            var builder = new Actions(sessionHTT);
            builder.MoveToElement(example, -11, 30).Click().Build().Perform();

            WindowsElement seriesElement = sessionHTT.FindElementByName("Series");
            builder = new Actions(sessionHTT);
            builder.MoveToElement(seriesElement, -11, 30).Click().Build().Perform();

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
            sessionHTT.FindElementByName(patientId).Click();
            sessionHTT.FindElementByName("Go to Patient Record").Click();
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        [TestMethod]
        public void ShutDownMenuClick()
        {
            //Click to Menu
            sessionHTT.FindElementByClassName("QToolButton").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            //Fusion App - Window !!Napravi posebnu app na Desktop-u
            //WindowsElement shtMeny = session1.FindElementByName("Fusion App");
            WindowsElement shtMeny = sessionRoot.FindElementByName("Shut Down");
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

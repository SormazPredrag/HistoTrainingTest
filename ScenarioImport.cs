using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
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
    public class ScenarioImport : HistoTraningSession
    {

        [TestMethod]
        public void ClickImportButton()
        {
            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //editBox.Click();
            //Console.WriteLine("Kliknuo!");
            //Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        [DataRow("D:\\Users\\Luka\\Documents\\Predrag\\dokumenta\\Vesa Rezultati\\CT Abdomena i Male karlice\\DICOM")]
        public void ImportDICOMFile(string filePath1)
        {
            Console.WriteLine($"DICOM file path {filePath1}");
            
            //WindowsElement importBtn = sessionHTT.FindElementByName("Import");
            WindowsElement importBtn = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.importButton");
            importBtn.Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            //Import modal
            //WindowsElement ChangeDirBtn = sessionHTT.FindElementByName("Change Directory");
            WindowsElement ChangeDirBtn = sessionHTT.FindElementByAccessibilityId("MainWindow.HistosonicsDicomBrowser.changeDirectoryButton"); //AutomationId:
            ChangeDirBtn.Click();

            //Folder edit
            WindowsElement FolderEdit = sessionHTT.FindElementByAccessibilityId("1152"); //AutomationId:
            FolderEdit.Clear();
            FolderEdit.Click();
            FolderEdit.SendKeys(SanitizeBackslashes("D:\\Users\\Luka\\Documents\\Predrag\\dokumenta\\Vesa Rezultati\\CT Abdomena i Male karlice\\DICOM") + Keys.Enter);
            //Select Folder
            sessionHTT.FindElementByAccessibilityId("1").Click();
            Thread.Sleep(TimeSpan.FromSeconds(3));
            WindowsElement Study = sessionHTT.FindElementByName("23.04.27-19:05:56-DST-1.3.12.2.1107.5.1.4.69591");

            /*TouchAction touchAction = new TouchAction(sessionHTT);
            touchAction.Tap(Study, 16, 16).Perform(); //40, 18
            Thread.Sleep(TimeSpan.FromSeconds(0.5));*/

            //Click to select
            Actions builder = new Actions(sessionHTT);
            builder.MoveToElement(Study, 16, 16).Click().Build().Perform();

            //Study.Click();

            //DICOM Browser
            WindowsElement DICOMelement = sessionHTT.FindElementByName("DICOM Browser");
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //DICOMelement.FindElementByTagName("button").Click();
            Console.WriteLine(DICOMelement.Coordinates);

            DICOMelement.FindElementByName("Next").Click();

            //Error 5010
            try
            {
                //Continue Import Wait for data
                Thread.Sleep(TimeSpan.FromSeconds(5));
                sessionHTT.FindElementByName("Continue Import").Click();
                Thread.Sleep(TimeSpan.FromSeconds(11));
                sessionHTT.FindElementByName("Continue Import").Click();
                Thread.Sleep(TimeSpan.FromSeconds(3));
                sessionHTT.FindElementByName("OK").Click();
            }
            catch (Exception)
            {
                //throw;
            }

            //Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //sessionHTT.FindElementByName("No").Click();

            //Click to Menu
            sessionHTT.FindElementByClassName("QToolButton").Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            sessionRoot.FindElementByName("Back to Patient Registration").Click();
            /*
            //Fusion App - Window !!Napravi posebnu app na Desktop-u
            //WindowsElement shtMeny = session1.FindElementByName("Fusion App");
            WindowsElement shtMeny = sessionRoot.FindElementByName("Shut Down");
            shtMeny.Click();

            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            sessionHTT.FindElementByName("Yes").Click();
            */

            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            sessionHTT.FindElementByName("No").Click();
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

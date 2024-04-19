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
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Interactions;
using static System.Collections.Specialized.BitVector32;

namespace SimulationToolTest
{
    public class NotepadSession
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string NotepadAppId = @"C:\Windows\System32\notepad.exe";
        private const string HistoAppId = @"D:\Program Files\ImFusion\ImFusion Suite\Suite\SimulationTool.exe";

        protected static WindowsDriver<WindowsElement> session;
        protected static WindowsDriver<WindowsElement> sessionHTT;
        protected static WindowsElement editBox;
        protected static WindowsElement ImportBtn;
        static Process WinDriverproc = new Process();

        public static void Setup(TestContext context)
        {
            // Launch a new instance of application
            if (session == null)
            {
                // Create a new session to launch Notepad application
                //DesiredCapabilities appCapabilities = new DesiredCapabilities();
                //appCapabilities.SetCapability("app", NotepadAppId);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

                //Start WinAppDriver
                WinDriverproc = Process.Start(@"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe");
                Process.Start(HistoAppId);
                //Wait to start Histosonic Training Tool
                Thread.Sleep(TimeSpan.FromSeconds(5));

                // Appium.WebDriver.4.4.5
                // Launch Notepad
                var appiumOptions = new OpenQA.Selenium.Appium.AppiumOptions();
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                appiumOptions.AddAdditionalCapability("app", @"C:\Windows\System32\notepad.exe");
                appiumOptions.AddAdditionalCapability("shouldTerminateApp", true);
                //appiumOptions.AddAdditionalCapability("appArguments", @"MyTestFile.txt");
                //appiumOptions.AddAdditionalCapability("appWorkingDir", @"C:\MyTestFolder\");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

                /*
                //get Handle of window to extract it's position and styles
                Process[] processes = Process.GetProcesses();
                Process process;
                foreach (Process proc in processes)
                {
                    Console.WriteLine(proc.MainWindowHandle + " : " + proc.ProcessName);
                    if (proc.ProcessName == "Histosonics Training Tool")
                    {
                        process = proc;
                        break;
                    }
                }*/

                //Find open HTT app by WindowHandle from Root session1
                var appiumOptions1 = new OpenQA.Selenium.Appium.AppiumOptions();
                appiumOptions1.AddAdditionalCapability("app", "Root");
                WindowsDriver<WindowsElement> session1 = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions1);
                var histoTT = session1.FindElementByName("Histosonics Training Tool");
                Assert.IsNotNull(histoTT);
                int HistoWindowI = Int32.Parse(histoTT.GetAttribute("NativeWindowHandle"));
                string HistoWindow = HistoWindowI.ToString("X");
                Console.WriteLine(HistoWindow);
               
                var appiumOptions2 = new OpenQA.Selenium.Appium.AppiumOptions();
                appiumOptions2.AddAdditionalCapability("appTopLevelWindow", HistoWindow);
                sessionHTT = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions2);

                //WindowsElement importBtn = sessionHTT.FindElementByName("Import");
                WindowsElement importBtn = sessionHTT.FindElementByAccessibilityId("MainWindow.centralwidget.stackedWidget.patientRegistrationPage.patientRegistrationBg.importButton");
                importBtn.Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                //Import modal
                WindowsElement ChangeBtn = sessionHTT.FindElementByName("Change Directory");
                //ChangeBtn.Click();
                
                //sessionHTT.FindElementByName("Close").Click(); //File open dialog
                sessionHTT.FindElementByName("Cancel").Click();


                //Double click
                //WindowsElement example = 
                sessionHTT.FindElementByName("Example_1").Click(); sessionHTT.FindElementByName("Example_1").Click();
                //Actions act = new Actions(session2);
                //act.DoubleClick(example).Perform();


                //Click to Menu
                sessionHTT.FindElementByClassName("QToolButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));


                //Fusion App - Window !!Napravi posebnu app na Desktop-u
                //WindowsElement shtMeny = session1.FindElementByName("Fusion App");
                WindowsElement shtMeny = session1.FindElementByName("Shut Down");
                shtMeny.Click();

                //sessionHTT.Close();
                //sessionHTT.Quit();

                //session1.FindElementByName("Back to Patient Registration").Click();

                //Ovo ne radi
                //session1.FindElementByXPath($"//Fusion App[starts-with(@Name, \"Shut Down\")]").Click();
                //WindowsElement trt = session1.FindElementByXPath("//Window[@name='Fusion App']"); //  .Click();
                //session1.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Shut\")]").Click();



                Thread.Sleep(TimeSpan.FromSeconds(1));
                sessionHTT.FindElementByName("Yes").Click();



                Thread.Sleep(TimeSpan.FromSeconds(1));
                Assert.IsNotNull(session);
                Assert.IsNotNull(session.SessionId);
                Console.WriteLine($"Sesija Id: {session.SessionId}");

                // Verify that Notepad is started with untitled new file
                //Assert.AreEqual("Histosonics Training", session.Title); // Tool
                Assert.AreEqual("Untitled - Notepad", session.Title);
                Assert.AreEqual("Desktop 1", session1.Title);
                

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                // Keep track of the edit box to be used throughout the session
                //editBox = session.FindElementByClassName("Import");
                editBox = session.FindElementByName("Help");
                Assert.IsNotNull(editBox);
                //session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Shut Down\")]").Click();
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Close();

                try
                {
                    // Dismiss Save dialog if it is blocking the exit
                    session.FindElementByName("Don't Save").Click();
                }
                catch { }

                Console.WriteLine("Quit!");
                session.Quit();
                session = null;
            }
            try
            {
                WinDriverproc.Close();
                WinDriverproc.Dispose();
                WinDriverproc.WaitForExit();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Select all text and delete to clear the edit box
            //editBox.SendKeys(Keys.Control + "a" + Keys.Control);
            //editBox.SendKeys(Keys.Delete);
            //Assert.AreEqual(string.Empty, editBox.Text);
        }

        protected static string SanitizeBackslashes(string input) => input.Replace("\\", Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt);
    }
}
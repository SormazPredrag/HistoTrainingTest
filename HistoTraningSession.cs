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
using System.Drawing;
using System.Security.Cryptography;
using OpenQA.Selenium.Appium.MultiTouch;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace SimulationToolTest
{
    public class HistoTraningSession
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        //private const string NotepadAppId = @"C:\Windows\System32\notepad.exe";
        private const string HistoAppId = @"D:\Program Files\ImFusion\ImFusion Suite\Suite\SimulationTool.exe";

        //protected static WindowsDriver<WindowsElement> session;
        protected static WindowsDriver<WindowsElement> sessionHTT;
        protected static WindowsDriver<WindowsElement> sessionRoot;
        protected static WindowsElement editBox;
        protected static WindowsElement ImportBtn;
        static Process WinDriverproc = new Process();
        protected static int WinWidth;
        private static int WinHeigth;

        public static void Setup(TestContext context)
        {
            // Launch a new instance of application
            if (sessionHTT == null)
            {
                //Microsoft.WinAppDriver.Appium.WebDriver 1.0.1-preview
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
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

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

                //Find open HTT app by WindowHandle from Root sessionRoot
                var appiumOptions1 = new OpenQA.Selenium.Appium.AppiumOptions();
                appiumOptions1.AddAdditionalCapability("app", "Root");
                sessionRoot = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions1);
                Thread.Sleep(TimeSpan.FromSeconds(3));
                var histoTT = sessionRoot.FindElementByName("Histosonics Training Tool");
                Assert.IsNotNull(histoTT);
                int HistoWindowId = Int32.Parse(histoTT.GetAttribute("NativeWindowHandle"));
                //string HistoWindow = HistoWindowId.ToString("X");
                //Console.WriteLine(HistoWindow);
               
                var appiumOptions2 = new OpenQA.Selenium.Appium.AppiumOptions();
                appiumOptions2.AddAdditionalCapability("appTopLevelWindow", HistoWindowId.ToString("X"));
                sessionHTT = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions2);

                CheckWindowsSize();

                /*
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Assert.IsNotNull(session);
                Assert.IsNotNull(session.SessionId);
                Console.WriteLine($"Sesija Id: {session.SessionId}");

                // Verify that Notepad is started with untitled new file
                //Assert.AreEqual("Histosonics Training", session.Title); // Tool
                Assert.AreEqual("Untitled - Notepad", session.Title);
                Assert.AreEqual("Desktop 1", sessionRoot.Title);
                

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                // Keep track of the edit box to be used throughout the session
                //editBox = session.FindElementByClassName("Import");
                editBox = session.FindElementByName("Help");
                Assert.IsNotNull(editBox);
                //session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Shut Down\")]").Click();
                */
            }
        }

        public static void CheckWindowsSize()
        {
            //Get Window size
            //sessionHTT.Manage().Window.Maximize();
            //Thread.Sleep(TimeSpan.FromSeconds(7));
            // ovo radi svaki drugi put
            try
            {
                //sessionHTT.FindElementByName("Minimise").Click();
                sessionHTT.FindElementByName("Maximise").Click();
                sessionHTT.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            }
            catch (Exception ex)
            {

            }
            var windowSize = sessionHTT.Manage().Window.Size;
            Console.WriteLine($"Win width: {windowSize.Width} height: {windowSize.Height} ");
            WinWidth = windowSize.Width;
            WinHeigth = windowSize.Height;
            //Assert.AreEqual(windowSize.Width, 1920);
            //Assert.AreEqual(windowSize.Height, 1040);

        }

        public static void TearDown()
        {
            // Close the application and delete the session
            /*if (session != null)
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
            }*/

            sessionHTT.Close();
            sessionHTT.Quit();

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
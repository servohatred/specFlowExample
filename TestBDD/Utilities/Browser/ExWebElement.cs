using OpenQA.Selenium;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.Evidences;

namespace TestBDD.Utilities.Browser
{
 public class ExWebElement : IWebElement, ISearchContext, ILocatable
    {
        public IWebElement webElement { get; set; }

        private string name = "NoName";

        private int timeout = 30;

        public string TagName { get; }

        public string Text { get; }

        public bool Enabled { get; }

        public bool Selected { get; }

        public Point Location { get; }

        public Size Size { get; }

        public bool Displayed { get; }

        public static IWebDriver WrappedDriver { get; set; }

        public Point LocationOnScreenOnceScrolledIntoView { get; }

        public ICoordinates Coordinates { get; }

        public String SUCCESS_MSG = "Action '{0}' executed successfully on web element '{1}'. {2}";
        public String EVIDENCE_MSG = "Evidence saved at: {0}";


        public ExWebElement(IWebElement element)
        {
            webElement = element;
            TagName = webElement.TagName;
            Text = webElement.Text;
            Enabled = webElement.Enabled;
            Selected = webElement.Selected;
            Location = webElement.Location;
            Size = webElement.Size;
            Displayed = webElement.Displayed;
 
        }

        public ExWebElement(IWebElement element, string name)
        {
            webElement = element;
            this.name = name;
            TagName = webElement.TagName;
            Text = webElement.Text;
            Enabled = webElement.Enabled;
            Selected = webElement.Selected;
            Location = webElement.Location;
            Size = webElement.Size;
            Displayed = webElement.Displayed;
        }
        public void Clear(int timeout, Boolean isScreenShotable)
        {
            const string ACTION = "Clear";
            IWebElement validElement = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(timeout))
                .Until(ExpectedConditions.ElementToBeClickable(webElement));
            if (isScreenShotable) { Evidence.GetScreenshot(WrappedDriver, this); }
            validElement.Clear();
            string evidenceText = isScreenShotable ? String.Format(EVIDENCE_MSG, Evidence.lastImagePath) : String.Empty;
            Evidence.TestLog.Info(String.Format(SUCCESS_MSG, ACTION,name,evidenceText));
        }

        public void Clear()
        {
            Clear(timeout,true);
        }

        public void Click(int timeout, Boolean isScreenShotable)
        {
            const string ACTION = "Click";
            IWebElement validElement = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(timeout))
                .Until(ExpectedConditions.ElementToBeClickable(webElement));
            if (isScreenShotable) { Evidence.GetScreenshot(WrappedDriver, this); }
            validElement.Click();
            string evidenceText = isScreenShotable ? String.Format(EVIDENCE_MSG, Evidence.lastImagePath) : String.Empty;
            Evidence.TestLog.Info(String.Format(SUCCESS_MSG, ACTION, name, evidenceText));

        }

        public void Click()
        {
            Click(timeout, true);
        }
        

        public string GetText(int timeout, Boolean isScreenShotable)
        {
            const string ACTION = "GetText";
            IWebElement validElement = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(timeout))
                .Until(ExpectedConditions.ElementToBeClickable(webElement));
            if (isScreenShotable) { Evidence.GetScreenshot(WrappedDriver, this); }
            string evidenceText = isScreenShotable ? String.Format(EVIDENCE_MSG, Evidence.lastImagePath) : String.Empty;
            Evidence.TestLog.Info(String.Format(SUCCESS_MSG, ACTION, name, evidenceText));
            return validElement.Text;
        }

        public string GetText()
        {
            return GetText(timeout, true);
        }

        public void SendKeys(string text,int timeout, Boolean isScreenShotable)
        {
            const string ACTION = "SendKeys";
            IWebElement validElement = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(timeout))
                .Until(ExpectedConditions.ElementToBeClickable(webElement));
            if (isScreenShotable) { Evidence.GetScreenshot(WrappedDriver, this); }
            validElement.SendKeys(text);
            string evidenceText = isScreenShotable ? String.Format(EVIDENCE_MSG, Evidence.lastImagePath) : String.Empty;
            Evidence.TestLog.Info(String.Format(SUCCESS_MSG, ACTION, name, evidenceText));
        }

        public void SendKeys(string text)
        {
            SendKeys(text,timeout, true);
        }

        public void Submit(int timeout, Boolean isScreenShotable)
        {
            const string ACTION = "Submit";
            IWebElement validElement = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(timeout))
                .Until(ExpectedConditions.ElementToBeClickable(webElement));
            if (isScreenShotable) { Evidence.GetScreenshot(WrappedDriver, this); }
            validElement.Submit();
                string evidenceText = isScreenShotable ? String.Format(EVIDENCE_MSG, Evidence.lastImagePath) : String.Empty;
            Evidence.TestLog.Info(String.Format(SUCCESS_MSG, ACTION,name,evidenceText));
        }

        public void Submit()
        {
            Submit(timeout, true);
        }

        public ExWebElement FindElement(By by, string name)
        {
            return new ExWebElement(webElement.FindElement(by), name);
        }
        public IWebElement FindElement(By by)
        {
            return webElement.FindElement(by);
        }

        public ExWebElement FindWebElement(By by)
        {
            return new ExWebElement(webElement.FindElement(by));
        }

       
        public ReadOnlyCollection<ExWebElement> FindWebElements(By by)
        {
            List<ExWebElement> list = new List<ExWebElement>();
            webElement.FindElements(by).ToList().ForEach(webElement =>
            {
                list.Add(new ExWebElement(webElement));
            });
            return new ReadOnlyCollection<ExWebElement>(list) ;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return new ReadOnlyCollection<IWebElement>(webElement.FindElements(by));
        }

        public string GetAttribute(string attributeName)
        {
            return webElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return webElement.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return webElement.GetProperty(propertyName);
        }

        public Screenshot GetScreenshot(string path)
        {
            const String BORDER_SCRIPT = "arguments[0].setAttribute('style','border:4px solid %s ; border-radius: 10px;')";
            const String NO_BORDER_SCRIPT = "arguments[0].setAttribute('style','border: none ; border-radius: none ; box-shadow: none')";
            IJavaScriptExecutor js = (IJavaScriptExecutor)WrappedDriver;
            js.ExecuteScript(BORDER_SCRIPT, this);
            Screenshot file = ((ITakesScreenshot)WrappedDriver).GetScreenshot();
            file.SaveAsFile(@path,ScreenshotImageFormat.Png);
            js.ExecuteScript(NO_BORDER_SCRIPT, this);
            return file;
        }
      
    }
}

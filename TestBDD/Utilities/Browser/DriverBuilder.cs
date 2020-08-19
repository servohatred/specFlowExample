using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBDD.Utilities.Browser
{
    public class DriverBuilder : BrowserBuilder
    {
        private BrowserType browserType = BrowserType.Chrome;
        private Boolean maximized = true;
        private Boolean sandBox = true;
        private Boolean testType = true;
        private Boolean incognito = false;
 
        public DriverBuilder() { }

        public BrowserBuilder setBrowserType(BrowserType browser)
        {
            browserType = browser;
            return this;
        }
        public BrowserBuilder setMaximized(Boolean isMaximized)
        {
            maximized = isMaximized;
            return this;
        }
        public BrowserBuilder setSandbox(Boolean isSandboxed)
        {
            sandBox = isSandboxed;
            return this;
        }
        public BrowserBuilder setTestType(Boolean isTestType)
        {
            testType = isTestType;
            return this;
        }
        public BrowserBuilder setIncognito(Boolean isIncognito)
        {
            incognito = isIncognito;
            return this;
        }
        public IWebDriver build()
        {
            IWebDriver driver = null;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions chromeOptions = configureChrome();
                   var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = false;
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    break;
                case BrowserType.Firefox:
                    FirefoxOptions firefoxOptions = configureFirefox();
                    driver = new FirefoxDriver(firefoxOptions);
                    break;
            }
            return driver;
        }
        private ChromeOptions configureChrome()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            List<string> arguments = new List<string>();
            if (sandBox)
            {
                arguments.Add("--no-sandbox");
            }   
            if (maximized)
            {
                arguments.Add("--start-maximized");
            }   
            if (testType)
            {
                arguments.Add("test-type");
            }
            chromeOptions.AddArguments(arguments.ToArray());
            return chromeOptions;
        }
        private FirefoxOptions configureFirefox()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddAdditionalCapability(CapabilityType.UnexpectedAlertBehavior, "dismiss");
            return firefoxOptions;
        }
       
    }
}

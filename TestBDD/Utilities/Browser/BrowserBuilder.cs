using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBDD.Utilities.Browser
{
    public enum BrowserType
    {
        Chrome,
        Firefox
    }
    public interface BrowserBuilder
    {
        BrowserBuilder setMaximized(Boolean isMaximized);
        BrowserBuilder setSandbox(Boolean isSandboxed);
        BrowserBuilder setTestType(Boolean isTestType);
        BrowserBuilder setIncognito(Boolean isIncognito);
        BrowserBuilder setBrowserType(BrowserType browser);
        IWebDriver build();
    }
}

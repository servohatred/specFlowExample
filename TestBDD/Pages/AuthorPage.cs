using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using TestBDD.Evidences;

namespace TestBDD.Pages
{
    class AuthorPage
    {

        private IWebDriver driver;

        public AuthorPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        const int TIME_OUT = 30;



        internal bool checkAuthorPage(String expectedAuthorPage)
        {
            bool result = new WebDriverWait(driver, TimeSpan.FromSeconds(TIME_OUT))
                .Until(ExpectedConditions.TitleContains(expectedAuthorPage));
            Evidence.GetScreenshot(driver);
            return result;
        }


    }
}

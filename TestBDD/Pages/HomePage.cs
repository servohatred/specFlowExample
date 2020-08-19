using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TestBDD.Utilities.Browser;

namespace TestBDD.Pages
{
    class HomePage
    {
        private IWebDriver driver;

        private HomePage(IWebDriver driver)
        {
            this.driver = driver;
            ExWebElement.WrappedDriver = driver;
            this.driver.Navigate().GoToUrl("https://Www.google.com.co");
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[contains(@class,'gLFyf') and contains(@class,'gsfi')]")]
        private IWebElement txtSearch;

        [FindsBy(How = How.ClassName, Using = "gNO89b")]
        private IWebElement btnSearch;

        [FindsBy(How = How.ClassName, Using = "sbct")]
        private IList<IWebElement> listSuggested;

        const int TIME_OUT = 30;

        public void typeWord(String word)
        {
            ExWebElement exTxtSearch = new ExWebElement(txtSearch, nameof(txtSearch)); 
            exTxtSearch.Clear(TIME_OUT, false);
            exTxtSearch.SendKeys(word, TIME_OUT, true);
        }

        internal bool suggestionsArePresent()
        {
          return new WebDriverWait(driver, TimeSpan.FromSeconds(TIME_OUT))
          .Until(driver => listSuggested.Count() > 1);
        }

        internal ResultsPage clickOnSuggestion(int position)
        {
            ExWebElement exSuggestion = new ExWebElement(listSuggested[(position - 1)], "SuggestionLink");
            exSuggestion.Click(TIME_OUT, true);
            return new ResultsPage(driver);
        }

        public ResultsPage pressSearchWithGoogle()
        {
            ExWebElement exBtnSearch = new ExWebElement(btnSearch,nameof(btnSearch));
            exBtnSearch.Click(TIME_OUT, true);
            return new ResultsPage(driver);
        }

        public static HomePage goToPage(IWebDriver driver)
        {
            return new HomePage(driver);
        }
    }
}

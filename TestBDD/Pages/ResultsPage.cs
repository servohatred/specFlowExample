using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.Utilities.Browser;

namespace TestBDD.Pages
{
   class ResultsPage
    {

        private IWebDriver driver;

        public ResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            ExWebElement.WrappedDriver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='search']//*[@class='g']")]
        private IList<IWebElement> listResultTitle;

        const int TIME_OUT = 30;


        internal AuthorPage clickOnResult(int position)
        {
           Boolean isSizeValid = new WebDriverWait(driver, TimeSpan.FromSeconds(TIME_OUT))
           .Until( driver => listResultTitle.Count() > 1);
            Assert.IsTrue(isSizeValid, "Searched with no results.");
            ExWebElement exResultTitle = new ExWebElement(listResultTitle[(position - 1)],"ResultLink");
            exResultTitle.Click(TIME_OUT, true);
            return new AuthorPage(driver);
        }

        internal bool isPresent()
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(TIME_OUT))
                .Until(ExpectedConditions.UrlContains("google.com.co/search?"));
        }

        internal void checkExpected(string expectedResult, int expectedPosition)
        {
            ExWebElement resultAtPosition = new ExWebElement(listResultTitle[(expectedPosition - 1)].FindElement(By.ClassName("LC20lb")),"ResultLink");
            string actualResult = resultAtPosition.GetText(TIME_OUT,true);
            Assert.AreEqual(expectedResult,actualResult, "Results are not the same.",new String[]{});
        }
    }
}

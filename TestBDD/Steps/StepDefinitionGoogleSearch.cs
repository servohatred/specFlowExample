using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestBDD.Evidences;
using TestBDD.UI;
using TestBDD.Utilities.Browser;

namespace TestBDD.Steps
{
    [Binding]
    public sealed class StepDefinitionGoogleSearch
    {
        IWebDriver driver;
        HomeNavigationUi homeNavigation = new HomeNavigationUi();

        [BeforeScenario]
        public void setUp(TestContext context)
        {
            configureDriver();
            string featureName = "GoogleSearch";
            string scenarioName = context.TestName;
            string projectName = "GoogleSearch";
            string evidencePath = "c://evidencePath";
            Evidence.init(evidencePath, projectName, featureName, scenarioName);
        }
        [AfterScenario]
        public void tearDown()
        {
            closeBrowser(driver);
        }


        [Given(@"I’m on the homepage")]
        public void GivenIMOnTheHomepage()
        {
            homeNavigation.goToHome(driver);
        }

        [When(@"I type a '(.*?)' into the search field")]
        public void WhenITypeASearchIntoTheSearchField(string word)
        {
            homeNavigation.enterSearch(word);
        }

        [When(@"I click the Google Search button")]
        public void WhenIClickTheGoogleSearchButton()
        {
            homeNavigation.searchWithGoole();
        }

        [When(@"I click on the '(.*?)' result link")]
        public void WhenIClickOnTheFirstResultLink(string position)
        {
            homeNavigation.selectResultByPosition(position);
        }


        [Then(@"^I go to the search results page$")]
        public void ThenIGoToTheSearchResultsPage()
        {
            homeNavigation.checkResultPage();
        }

        [When(@"the suggestions list is displayed")]
        public void WhenTheSuggestionsListIsDisplayed()
        {
            homeNavigation.checkSuggestions();
        }

        [When(@"I click on the '(.*?)' suggestion in the list")]
        public void WhenIClickOnTheFirstSuggestionInTheList(string position)
        {
            homeNavigation.selectSugestionByPosition(position);
        }


        [Then(@"the '(.*?)' result is '(.*?)'")]
        public void ThenTheFirstResultIsSomething(string expectedPosition, string expectedResult)
        {
            homeNavigation.checkExpectedResult(expectedResult, expectedPosition);
        }


        [Then(@"^I go to the '(.*?)' page")]
        public void ThenIGoToTheAuthorWebsite(string expectedAuthorPage)
        {
            homeNavigation.CheckAuthorPage(expectedAuthorPage);
        }

        public void configureDriver()
        {
            driver = new DriverBuilder()
                 .setBrowserType(BrowserType.Chrome)
                 .setIncognito(false)
                 .setMaximized(true)
                 .build();
        }
        public void closeBrowser(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }
    }
}

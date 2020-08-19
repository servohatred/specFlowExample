using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TestBDD.Enums;
using TestBDD.Evidences;
using TestBDD.Pages;

namespace TestBDD.UI
{
    public class HomeNavigationUi
    {
        HomePage homePage;
        ResultsPage resultsPage;
        AuthorPage authorPage;

        const string MSG_SUCCESS = "Execution of {0} has been completed successfully";
        const string MSG_FAIL = "An error ocurred during execution of {0} . Error:\n {1}";
        internal void goToHome(IWebDriver driver)
        {
            const string ACTION = "HOMEPAGE ENTRY";
            try
            {
                homePage = HomePage.goToPage(driver);
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }
        internal void enterSearch(string word)
        {
            string ACTION = "WORD TYPING '" + word + "'";
            try
            {
                homePage.typeWord(word);
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }
        internal void searchWithGoole()
        {
            const string ACTION = "CLICK ON SEARCH WITH GOOGLE";
            try
            {
                resultsPage = homePage.pressSearchWithGoogle();
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }

        internal void selectResultByPosition(string position)
        {
            string ACTION = "CLICK ON RESULT AT " + position + " POSITION ";
            try
            {
                int numericPosition = (int)(Position)Enum.Parse(typeof(Position), position);
                authorPage = resultsPage.clickOnResult(numericPosition);
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }

        internal void checkResultPage()
        {
            const string ACTION = "CHECKING PAGE WITH VALID RESULTS ";
            try
            {
                bool isResultPage = resultsPage.isPresent();
                Assert.IsTrue(isResultPage, "Searched with no results.");
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }

        internal void checkSuggestions()
        {
            const string ACTION = "CHECKING PAGE WITH VALID SUGGESTIONS ";
            try
            {
                bool areSuggestionsPresent = homePage.suggestionsArePresent();
                Assert.IsTrue(areSuggestionsPresent, "Suggestions not present.");
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }

        internal void selectSugestionByPosition(string position)
        {
            string ACTION = "CLICK ON SUGGESTION AT " + position + " POSITION";
            try
            {
                int numericPosition = (int)((Position)Enum.Parse(typeof(Position), position));
                resultsPage = homePage.clickOnSuggestion(numericPosition);
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }

        internal void checkExpectedResult(string expectedResult, string expectedPosition)
        {
            string ACTION = "CHECKING EXPECTED RESULT AT " + expectedPosition + " POSITION TO BE " + expectedResult;
            try
            {
                int numericExpectedPosition = (int)((Position)Enum.Parse(typeof(Position), expectedPosition));
                resultsPage.checkExpected(expectedResult, numericExpectedPosition);
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }

        internal void CheckAuthorPage(string expectedAuthorPage)
        {
            string ACTION = "CHECKING AUTHOR PAGE TO BE " + expectedAuthorPage;
            try
            {
                authorPage.checkAuthorPage(expectedAuthorPage);
                Evidence.BusinessLog.Info(string.Format(MSG_SUCCESS, ACTION));
            }
            catch (Exception e)
            {
                Evidence.BusinessLog.Error(string.Format(MSG_FAIL, ACTION, e));
                throw;
            }
        }
    }
}

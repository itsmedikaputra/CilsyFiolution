using System;
using GLMSys.SpecFlow.Playwright.Drivers;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CilsyFiolution.StepDefinitions
{
    [Binding]
    public class SearchStepDefinitions
    {
        private readonly Driver _driver;
        public SearchStepDefinitions(Driver driver)
        {
            _driver = driver;
        }

        [When(@"User input an items in the search bar")]
        public async Task WhenUserInputAnItemsInTheSearchBar()
        {
            await _driver.Page.Locator("//form[@class='navbar-form navbar-left form-search hidden-xs']//input[@placeholder='Search']").ClickAsync();
            await _driver.Page.Locator("//form[@class='navbar-form navbar-left form-search hidden-xs']//input[@placeholder='Search']").ClearAsync();
            await _driver.Page.Locator("//form[@class='navbar-form navbar-left form-search hidden-xs']//input[@placeholder='Search']").FillAsync("Tutorial Mantap");
        }

        [When(@"User click the search icon")]
        public async Task WhenUserClickTheSearchIcon()
        {
            var searchButton = _driver.Page.Locator("xpath=//div[@id='bs-example-navbar-collapse-1']/form/div/span/button/i");
            await searchButton.ClickAsync();
        }

        [Then(@"User should see the existing items")]
        public async Task ThenUserShouldSeeTheExistingItems()
        {
            var Result = await _driver.Page.Locator("xpath=//strong[contains(.,'Tutorial Mantap')]")
                                        .InnerTextAsync();
            Assert.That(Result, Is.EqualTo("Tutorial Mantap"));
        }
    }
}

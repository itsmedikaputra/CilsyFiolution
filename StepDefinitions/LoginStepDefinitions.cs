using GLMSys.SpecFlow.Playwright.Drivers;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GLMSys.SpecFlow.Playwright.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private readonly Driver _driver;

        public LoginStepDefinitions(Driver driver)
        {
            _driver = driver;
        }

        [Given(@"User navigate to url")]
        public async Task GivenUserNavigateToUrl()
        {
            await _driver.Page.GotoAsync("https://vuln.profesor.id/");
        }

        [When(@"User enter valid credentials")]
        public async Task WhenUserEnterValidCredentials()
        {
            await _driver.Page.ClickAsync("text=Masuk");
            await _driver.Page.FillAsync("//input[@name='email']", "andikas.1922@gmail.com");
            await _driver.Page.FillAsync("//input[@id='password-field']", "Password@123");
        }

        [When(@"User click the login button")]
        public async Task WhenUserClickTheLoginButton()
        {
            await _driver.Page.ClickAsync("(//button[@type='submit'])[3]");
        }

        [Then(@"User should see the dashboard page")]
        public async Task ThenUserShouldSeeTheDashboardPage()
        {
            await _driver.Page.WaitForSelectorAsync("//img[contains(@src,'https://vuln.profesor.id/template/web/img/drop-down-round-button.png')]");
            await _driver.Page.ClickAsync("//img[contains(@src,'https://vuln.profesor.id/template/web/img/drop-down-round-button.png')]");
            await _driver.Page.Locator("text=Andika").First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        }

        [When(@"User enter invalid credentials")]
        public async Task WhenUserEnterInvalidCredentials()
        {
            await _driver.Page.ClickAsync("text=Masuk");
            await _driver.Page.FillAsync("//input[@name='email']", "emailngasal@gmail.com");
            await _driver.Page.FillAsync("//input[@id='password-field']", "Passwordjugangasal");
        }

        [Then(@"User should see the error")]
        public async Task ThenUserShouldSeeTheError()
        {
            var errorElement = await _driver.Page.Locator("xpath=//strong[contains(text(), 'Whoops!')]")
                            .InnerTextAsync();

            // Use NUnit Assert to confirm that the element contains the expected text
            Assert.That(errorElement, Is.EqualTo("Whoops!"));
        }
    }
}

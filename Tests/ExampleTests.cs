using NUnit.Framework;
using OpenQA.Selenium;
using NUnitSeleniumGridDocker.Drivers;

namespace CrossDockSelenium.Tests
{
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    public class ExampleTests(string browser)
    {
        private IWebDriver? driver;
        private readonly string browser = browser;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateDriver(browser);
        }

        [Test]
        public void DemoQA_Title_ShouldContainDemoQA()
        {
            driver!.Navigate().GoToUrl("https://demoqa.com");
            WaitHelpers.WaitForPageLoad(driver);
            Assert.That(driver.Title, Does.Contain("DEMOQA"));
        }

        [Test]
        public void DemoQA_CheckFormLoginElements()
        {
            driver!.Navigate().GoToUrl("https://demoqa.com/login");
            WaitHelpers.WaitForPageLoad(driver);
            Assert.That(driver.FindElement(By.Id("userName")).Displayed, Is.True);
            Assert.That(driver.FindElement(By.Id("password")).Displayed, Is.True);
            Assert.That(driver.FindElement(By.Id("login")).Displayed, Is.True);
        }

        [Test]
        public void DemoQA_SearchBox_IsClickable()
        {
            driver!.Navigate().GoToUrl("https://demoqa.com/books");
            WaitHelpers.WaitForPageLoad(driver);
            var searchBox = driver.FindElement(By.Id("searchBox"));
            Assert.That(searchBox.Displayed, Is.True);
            searchBox.SendKeys("Git Pocket Guide");
            Assert.That(searchBox.GetAttribute("value"), Does.Contain("Git"));
        }

        [Test]
        public void BrokenLink_ShouldReturn404()
        {
            driver!.Navigate().GoToUrl("https://demoqa.com/broken");
            WaitHelpers.WaitForPageLoad(driver);
            var brokenLink = driver.FindElement(By.XPath("//a[text()='Click Here for Broken Link']"));
            var href = brokenLink.GetAttribute("href");

            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(href).Result;

            Assert.That((int)response.StatusCode, Is.EqualTo(500).Or.EqualTo(404));
        }

        [TearDown]
        public void TearDown()
        {
            driver!.Quit();
        }
    }
}

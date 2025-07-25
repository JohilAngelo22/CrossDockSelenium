using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace NUnitSeleniumGridDocker.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            DriverOptions options = browser.ToLower() switch
            {
                "chrome" => new ChromeOptions(),
                "firefox" => new FirefoxOptions(),
                _ => throw new ArgumentException($"Unsupported browser: {browser}"),
            };

            // Cast options to the specific type to use AddArgument method  
            if (options is ChromeOptions chromeOptions)
            {
                chromeOptions.AddArgument("--start-maximized");
            }
            else if (options is FirefoxOptions firefoxOptions)
            {
                firefoxOptions.AddArgument("--start-maximized");
            }

            return new RemoteWebDriver(
                new Uri("http://localhost:4444/wd/hub"),
                options
            );
        }
    }
}

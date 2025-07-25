using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public static class WaitHelpers
{
    public static void WaitForPageLoad(IWebDriver driver, int timeoutInSeconds = 60)
    {
        try
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState")!.Equals("complete"));
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception("Page did not load within " + timeoutInSeconds + " seconds");
        }
    }
}

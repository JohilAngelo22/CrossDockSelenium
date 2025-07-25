# 🚀 CrossDockSelenium

This is a **parallel, cross-browser test automation framework** built with **C#**, **NUnit**, **Selenium Grid**, and **Docker**.  
It demonstrates how to run **Selenium tests in Docker containers** with **Selenium Grid**, enabling **parallel execution** across multiple browsers.

---

## 📌 Features

✅ Selenium WebDriver with RemoteWebDriver  
✅ NUnit test framework  
✅ Selenium Grid with Docker  
✅ Parallel cross-browser tests (Chrome, Firefox)  
✅ Docker Compose setup for Grid and Nodes  
✅ Easy to scale nodes and sessions  
✅ Works on local or CI/CD pipelines  
✅ Clean example tests — easy to extend

---

## ⚙️ Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/) installed (e.g., .NET 6+)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running
- Basic knowledge of Selenium & NUnit

---

## 🐳 Docker Compose Setup

Your `docker-compose.yml` spins up:

- **Selenium Hub**  
- **Chrome Node**
- **Firefox Node**

Example:

```yaml
version: "3"

services:
  selenium-hub:
    image: selenium/hub:latest
    container_name: selenium-hub
    ports:
      - "4444:4444"

  chrome:
    image: selenium/node-chrome:latest
    shm_size: 2gb
    depends_on:
      - selenium-hub
    environment:
      - SE_EVENT_BUS_HOST=selenium-hub
      - SE_EVENT_BUS_PUBLISH_PORT=4442
      - SE_EVENT_BUS_SUBSCRIBE_PORT=4443
      - SE_NODE_MAX_SESSIONS=2

  firefox:
    image: selenium/node-firefox:latest
    shm_size: 2gb
    depends_on:
      - selenium-hub
    environment:
      - SE_EVENT_BUS_HOST=selenium-hub
      - SE_EVENT_BUS_PUBLISH_PORT=4442
      - SE_EVENT_BUS_SUBSCRIBE_PORT=4443
      - SE_NODE_MAX_SESSIONS=2
🚦 How to Run
1️⃣ Start Selenium Grid with Docker

docker-compose up -d
2️⃣ Run the NUnit tests


dotnet test
3️⃣ Open your browser and check the Grid console:
http://localhost:4444/ui
Here you can watch sessions live!

🧩 Example Driver Factory
public static IWebDriver CreateDriver(string browser)
{
    var options = browser.ToLower() switch
    {
        "chrome" => new ChromeOptions(),
        "firefox" => new FirefoxOptions(),
        _ => throw new ArgumentException($"Unsupported browser: {browser}")
    };

    options.AddArgument("--window-size=1920,1080");

    return new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
}
📑 Example Test

[TestFixture]
public class ExampleTests
{
    [TestCase("chrome")]
    [TestCase("firefox")]
    public void DemoQA_Title_ShouldContainDemoQA(string browser)
    {
        using var driver = DriverFactory.CreateDriver(browser);
        driver.Navigate().GoToUrl("https://demoqa.com");
        Assert.That(driver.Title, Does.Contain("DEMOQA"));
    }
}

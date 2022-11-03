using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace SeleniumXUnitTest
{
    public class DriverFixture : IDisposable
    {
        public ChromeDriver Driver { get; }
        public DriverFixture()
        {
            Driver = new ChromeDriver();
            Debug.WriteLine("Init driver");
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }

    public class SeleniumXUnitTest : IClassFixture<DriverFixture>
    {
        public DriverFixture fix;

        public SeleniumXUnitTest(DriverFixture fixture)
        {
            fix = fixture;
            fix.Driver.Navigate().GoToUrl("http://www.apple.com");
        }

        [Fact, Trait("Priority", "1"), Trait("Priority", "2")]
        public void SeleniumTraining_TestAppleWebSite_HomeIconFound()
        {
            
            new WebDriverWait(fix.Driver, TimeSpan.FromSeconds(5)).Until(d => d.FindElement(By.XPath("//a[@data-analytics-title='apple home' and @id='ac-gn-firstfocus-small']")));
        }

        /*<a class="ac-gn-link ac-gn-link-store" href="/us/shop/goto/store" data-analytics-title="store">
					<span class="ac-gn-link-text">Store</span>
				</a>*/
        [Fact, Trait("Priority", "2")]
        public void SeleniumTraining_TestAppleWebSite_StoreButtonFound()
        {

            new WebDriverWait(fix.Driver, TimeSpan.FromSeconds(5)).Until(d => d.FindElement(By.XPath("//a[@data-analytics-title='store']//span[@class='ac-gn-link-text' and text()='Store']")));
        }

        /* <a class="ac-gn-link ac-gn-link-mac" href="/mac/" data-analytics-title="mac">
					<span class="ac-gn-link-text">Mac</span>
				</a>*/
        [Fact, Trait("Priority", "2")]
        public void SeleniumTraining_TestAppleWebSite_MacButtonFound()
        {

            new WebDriverWait(fix.Driver, TimeSpan.FromSeconds(5)).Until(d => d.FindElement(By.XPath("//a[@data-analytics-title='mac']//span[@class='ac-gn-link-text' and text()='Mac']")));
        }

    }

    public class SeleniumXUnitTest2
    {
        private ChromeDriver InitDriver()
        {
            return new ChromeDriver();
        }

        private void CloseDriver(ChromeDriver driver)
        {
            driver.Quit();
            //driver.Close();
        }

        [Fact(Skip = "Ignore"), Trait("Priority", "1"), Trait("Priority", "2")]
        public void SeleniumTraining_TestGoogleWebSite_OpenPageSuccess()
        {
            var driver = InitDriver();
            driver.Navigate().GoToUrl("http://www.google.com");
            var inputTextField = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(d => d.FindElement(By.XPath("//input[@class='gLFyf gsfi']")));
            inputTextField.SendKeys("Test" + Keys.Enter);
            //<div class="RJn8N xXEKkb ellip tNxQIb ynAwRc">test</div>
            var resultText = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(d => d.FindElement(By.XPath("//div[@class='RJn8N xXEKkb ellip tNxQIb ynAwRc' and text()='test']")));
            CloseDriver(driver);
        }
    }
}

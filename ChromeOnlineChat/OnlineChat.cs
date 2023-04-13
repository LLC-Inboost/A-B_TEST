using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace InBoost.ChromeOnlineChat
{
    internal class OnlineChat
    {
        private IWebDriver driver;
        private Actions actions;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            //IWebDriver driver = new FirefoxDriver();
            //IWebDriver driver = new EdgeDriver();
            actions = new Actions(driver);
            driver.Manage().Window.Maximize();
            driver.Url = "https://qa3prod.inboost.ai/";
            // driver.Url = "https://qa3dev.inboost.ai/";
            WaitForElement(By.XPath("//input[@placeholder='Робочий Email']"));
        }

        private IWebElement WaitForElement(By locator, int maxTimeout = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxTimeout))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        public void LogIn()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                .SendKeys("amina_qa3_prod@inboost.ai");
            // driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
            //   .SendKeys("amina_qa3_dev@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina21amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
        }
        [Test, Order(1)]
        public void ChatTest()
        {
            LogIn();
            Thread.Sleep(1000);
            IWebElement mainMenu = driver.FindElement(By.XPath("//*[@id='root']/div/div[1]/div[2]/div[5]/div[1]"));
            actions.MoveToElement(mainMenu).Pause(TimeSpan.FromMilliseconds(500));
            IWebElement subMenu = driver.FindElement(By.XPath("//*[@id='root']/div/div[1]/div[2]/div[5]/div[2]"));
            actions.MoveToElement(subMenu).Pause(TimeSpan.FromMilliseconds(500));
            actions.Click().Build().Perform();
            Thread.Sleep(1500);

            IWebElement clientsButton = driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/div/div[1]/ul/li[1]/button"));
            actions.MoveToElement(clientsButton).Pause(TimeSpan.FromMilliseconds(500));
            actions.Click().Build().Perform();
            Thread.Sleep(3000);

            IWebElement selectClient = driver.FindElement(By.XPath("//*[@id=\"panelUser\"]"));
            actions.MoveToElement(selectClient).Pause(TimeSpan.FromMilliseconds(500));
            actions.Click().Build().Perform();
            Thread.Sleep(1000);

            driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div/div/div[2]/div[3]/div/div/button[1]")).Click();
            Thread.Sleep(6000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);
        }
        [Test, Order(2)]
        public void ChatStart()
        {
            LogIn();
            Thread.Sleep(1000);
            IWebElement mainMenu = driver.FindElement(By.XPath("//*[@id='root']/div/div[1]/div[2]/div[5]/div[1]"));
            actions.MoveToElement(mainMenu).Pause(TimeSpan.FromMilliseconds(500));
            IWebElement subMenu = driver.FindElement(By.XPath("//*[@id='root']/div/div[1]/div[2]/div[5]/div[2]"));
            actions.MoveToElement(subMenu).Pause(TimeSpan.FromMilliseconds(500));
            actions.Click().Build().Perform();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("//*[@id=\"panelUser\"]")).Click();
            // Розпочати чат
            WaitForElement(By.XPath("//button[contains(text(),'Розпочати чат')]"));
            driver.FindElement(By.XPath("//button[contains(text(),'Розпочати чат')]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div/div/div/div[2]/div[4]/div[3]/div/textarea"))
                .SendKeys("Hello world");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div/div/div/div[2]/div[4]/div[3]/div/ul/li[2]/button")).Click();
            Thread.Sleep(1000);
            //Смайлик в чат
            //driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div/div/div/div[2]/div[4]/div[3]/div/ul/li[1]/button")).Click();
            //driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/div/div[1]/div[2]/div/button[2]/span/span")).Click();
            //driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div/div/div/div[2]/div[4]/div[3]/div/ul/li[2]/button")).Click();
            // Фото в чат
            IWebElement photoIcon = driver.FindElement(By.XPath("//div[@class='chat-new__chat-wrap']//li[2]//button[1]"));
            actions.MoveToElement(photoIcon).Click().Build().Perform();
            Thread.Sleep(300);
            System.Windows.Forms.SendKeys.SendWait("C:\\Users\\amina\\Downloads\\image.jfif");
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            Thread.Sleep(300);
            driver.FindElement(By.XPath("//button[contains(text(),'Завершити чат')]")).Click();
        }

       // [TearDown]
        public void CloseBrowser()
        {
            Thread.Sleep(5000);
            driver.Quit();
        }
    }
}

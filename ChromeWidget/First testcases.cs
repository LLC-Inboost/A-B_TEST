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
using System.Configuration;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace InBoost.ChromeWidget
{
    internal class Widget
    {
        private IWebDriver driver;
        private Actions actions;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            actions = new Actions(driver);
            driver.Manage().Window.Maximize();
            //driver.Url = "https://qa3prod.inboost.ai/";
            driver.Url = "https://qa3dev.inboost.ai/";
            WaitForElement(By.XPath("//input[@placeholder='Робочий Email']"));
        }

        private IWebElement WaitForElement(By locator, int maxTimeout = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxTimeout))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }
        public void LogIn()
        {
            //driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                //.SendKeys("amina_qa3_prod@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
              .SendKeys("amina_qa3_dev@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina21amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[2]/div[1]"));
            IWebElement mainMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[2]/div[1]"));
            IWebElement subMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[2]/div[2]/div[2]"));
            actions
                .MoveToElement(mainMenu)
                .MoveToElement(subMenu)
                .Click().Build().Perform();
        }
        [Test, Order(1)]
        public void OpenPage()
        {
            LogIn();
            // Заповнення полів
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[2]/div/div/input"))
                .SendKeys("Привіт автотест");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[3]/div/div/input"))
                .SendKeys("3");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[4]/div/div/input"))
                .SendKeys("2");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[5]/div[1]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[5]/div[2]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[6]/div/div/input"))
                .SendKeys("5");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[7]/div/div/input"))
                .SendKeys("7");
            // Кольори заповлення
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/div/div[2]/div[1]/div/button")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/div/div[2]/div[1]/div[2]/div[4]/div[3]/span/div")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/div/div[2]/div[2]/div/button")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/div/div[2]/div[2]/div[2]/div[4]/div[1]/span/div")).Click();
            // Заповнення поля месенджери
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[1]/div/div/header")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/ul/li[1]/div/div/ul/li[3]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/main/div[1]/div/button")).Click();
            // Копіювання коду віджета
            driver.FindElement(By.XPath("//button[@class='constructor-widget__code-copy']")).Click();
            Thread.Sleep(1000);
        }
        [Test, Order(2)]
        public void EmptyField()
        {
            LogIn();
            //Переключення між виглядами
            driver.FindElement(By.XPath("//div[@class='constructor-widget__preview']//li[4]//*[name()='svg']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//li[@class='constructor-widget__view-item constructor-widget__view-item_active']")).Click();
            //Залишення полів пустими
            IWebElement saveButton = driver.FindElement(By.XPath("//button[contains(text(),'Зберегти налаштування')]"));
            actions
                .MoveToElement(saveButton)
                .Click().Build().Perform();
            Thread.Sleep(1000);
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[1]/div"));
            string errorNotification = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[1]/div")).Text;
            Assert.IsTrue(errorNotification.StartsWith("Ви не заповнили всі поля налаштувань"), "Error message is NOT correct. Actual result: "
               + errorNotification + "  Expected result: Ви не заповнили всі поля налаштувань");
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}


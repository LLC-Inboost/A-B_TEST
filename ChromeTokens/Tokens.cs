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
using System.Security.Cryptography.X509Certificates;

namespace InBoost.ChromeTokens
{
    internal class Tokens
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
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[4]/div[1]"));
            IWebElement mainMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[4]/div[1]"));
            IWebElement subMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[4]/div[2]"));
            actions
                .MoveToElement(mainMenu)
                .MoveToElement(subMenu)
                .Click().Build().Perform();
        }

        [Test, Order(1)]
        public void ViberBot()
        {
            LogIn();
            // Підключення вайбер токена
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[1]/div[2]/div/div/input"))
                .SendKeys("50bc21b2f2a7e02e-bb4108c6e255158b-e2956accda71209b");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[1]/div[2]/button")).Click();
            Thread.Sleep(1000);
            // Вимкнення поп-апу з інформацією
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[3]/div"));
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[3]/div/main/div/button")).Click();
            // Натиск на кнопку "Відключення" Вайберу
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[1]/div[2]/div[2]/div[3]/button")).Click();
            WaitForElement(By.XPath("//*[@id=\"root\"]/div[2]/div"));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div/button[2]")).Click();
            WaitForElement(By.XPath("//*[@class='delete-notification']"));
            string deleteNotification = driver.FindElement(By.XPath("//*[@class='delete-notification']")).Text;
            Assert.IsTrue(deleteNotification.Equals("Чатбот видалено"), "Tooltip message is NOT correct. Actual result: "
               + deleteNotification + "  Expected result: Чатбот видалено");
        }

        [Test, Order(2)]
        public void TelegramBot()
        {
            LogIn();
            // Підключення телеграм токена
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[2]/div[2]/div/div/input"))
                .SendKeys("6250005952:AAF7IMbtMI1JVdPvi0i-05nyJFG50QWWlsE");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[2]/div[2]/button")).Click();
            Thread.Sleep(1000);
            // Вимкнення поп-апу з інформацією
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[3]/div"));
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[3]/div/main/div/button")).Click();
            // Натиск на кнопку "Відключення" Телеграм
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[2]/div[2]/div[2]/div[3]/button")).Click();
            WaitForElement(By.XPath("//*[@id=\"root\"]/div[2]/div"));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div/button[2]")).Click();
            Thread.Sleep(1000);
            WaitForElement(By.XPath("//*[@class='delete-notification']"));
            string deleteNotification = driver.FindElement(By.XPath("//*[@class='delete-notification']")).Text;
            Assert.IsTrue(deleteNotification.Equals("Чатбот видалено"), "Tooltip message is NOT correct. Actual result: "
               + deleteNotification + "  Expected result: Чатбот видалено");
        }



        [Test, Order(2)]
        public void WebBot()
        {
            LogIn();
            // Скрол в кінець сторінки
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[5]"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", element);
            // Веб-бот підключення
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[5]/div[2]/div/div[1]/div/input"))
                .SendKeys("Aminka");
            string randomBotName = RandomStringUtils.RandomStringUtils.RandomAlphabetic(10);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[5]/div[2]/div/div[2]/div/input"))
                .SendKeys(randomBotName);
            // Завантаження фотки з комп'ютера
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[5]/span/button")).Click();
            Thread.Sleep(300);
            System.Windows.Forms.SendKeys.SendWait("C:\\Users\\amina\\Downloads\\image.jfif");
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//button[contains(text(),'Зберегти')]")).Click();
            // Поп-ап відключння
            WaitForElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[3]/div"));
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[3]/div/main/div/button")).Click();
            // Відключення ботa
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div[2]/div[5]/div[2]/div[2]/div[3]/button")).Click();
            WaitForElement(By.XPath("//*[@id=\"root\"]/div[2]/div"));
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div/main/div/button[2]")).Click();
        }


        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

    }
}
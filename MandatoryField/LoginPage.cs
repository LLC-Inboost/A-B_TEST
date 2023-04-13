using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InBoost.MandatoryField
{
    internal class LoginPage
    {
        private IWebDriver driver;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://qa3prod.inboost.ai/";
            WaitForElement(By.XPath("//input[@placeholder='Робочий Email']"));
        }

        [Test, Order(1)]
        public void EmailFieldIsRequired()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina21amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
            string emailErrorMessage = driver.FindElement(By.XPath("//div[@class='auth__authorization-body']//div[1]//p[1]")).Text;
            Assert.IsTrue(emailErrorMessage.Equals("Введіть email, щоб зайти в систему"), "Error message is NOT correct. Actual result: " 
                + emailErrorMessage + "  Expected result: Введіть email, щоб зайти в систему");
        }

        [Test, Order(2)]
        public void PasswordFieldIsRequired()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                .SendKeys("amina_qa3_prod@inboost.ai");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
            string passwordErrorMessage = driver.FindElement(By.XPath("//div[@class='auth__authorization-body']//div[2]//p[1]")).Text;
            Assert.IsTrue(passwordErrorMessage.Equals("Введіть пароль"), "Error message is NOT correct. Actual result: "
                + passwordErrorMessage + "  Expected result: Введіть пароль");
        }

        [Test, Order(3)]
        public void CheckingForInvalidEmail()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                .SendKeys("amina_qa2_prod@inboost");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina21amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
            string emailErrorMessage = driver.FindElement(By.XPath("//div[@class='auth__authorization-body']//div[1]//p[1]")).Text;
            Assert.IsTrue(emailErrorMessage.Equals("Email введено не вірно. Перевірте та введіть повторно"), "Error message is NOT correct. Actual result: "
                + emailErrorMessage + "  Expected result: Email введено не вірно. Перевірте та введіть повторно");
        }

        [Test, Order(4)]
        public void CheckingForIncorrectPassword()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                .SendKeys("amina_qa3_prod@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Aminaami13na");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
            WaitForElement(By.XPath("//p[contains(text(),'Неправильний пароль')]"));
            string passwordErrorMessage = driver.FindElement(By.XPath("//div[@class='auth__authorization-body']//div[2]//p[1]")).Text;
            Assert.IsTrue(passwordErrorMessage.Equals("Неправильний пароль"), "Error message is NOT correct. Actual result: "
                + passwordErrorMessage + "  Expected result: Неправильний пароль");
        }

        [Test, Order(5)]
        public void CheckingShortPassword()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                .SendKeys("amina_qa3_prod@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina13");
            string passwordErrorMessage = driver.FindElement(By.XPath("//div[@class='auth__authorization-body']//div[2]//p[1]")).Text;
            Assert.IsTrue(passwordErrorMessage.Equals("Пароль повинен містити мінімум 8 символів"), "Error message is NOT correct. Actual result: "
                + passwordErrorMessage + "  Expected result: Пароль повинен містити мінімум 8 символів");
        }

        [Test, Order(6)]
        public void CheckingValidLogin()
        {
            driver.FindElement(By.XPath("//input[@placeholder='Робочий Email']"))
                .SendKeys("amina_qa3_prod@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina21amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
            WaitForElement(By.XPath("//li[@class='ui__main__header--item ui__main__header--item--title']"));
            driver.FindElement(By.XPath("//li[@class='ui__main__header--item ui__main__header--item--title']"));
        }

        private IWebElement WaitForElement(By locator, int maxTimeout = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxTimeout))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}

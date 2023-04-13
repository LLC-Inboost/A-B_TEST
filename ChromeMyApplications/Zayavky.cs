using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InBoost.ChromeMyApplications
{
    internal class Zayavky
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
            driver.Url = "https://pashatesting.inboost.ai/";
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
                .SendKeys("pashatesting_amina@inboost.ai");
            driver.FindElement(By.XPath("//input[@placeholder='Пароль із 8-ми і більше символів']"))
                .SendKeys("Amina13amina");
            driver.FindElement(By.XPath("//button[contains(text(),'Увійти')]")).Click();
            WaitForElement(By.XPath("//body/div[@id='root']/div[@class='container-page']/div[@class='navbar']/div[@class='navbar__links']/div[3]/div[1]"));
            IWebElement mainMenu = driver.FindElement(By.XPath("//body/div[@id='root']/div[@class='container-page']/div[@class='navbar']/div[@class='navbar__links']/div[3]/div[1]"));
            IWebElement subMenu = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[2]/div[3]/div[2]/div[2]/span"));
            actions
                .MoveToElement(mainMenu)
                .MoveToElement(subMenu)
                .Click().Build().Perform();
        }

        [Test, Order(1)]
        public void OpenList()
        {
            LogIn();
            Thread.Sleep(15000);
            IWebElement hopper = driver.FindElement(By.XPath("//div[contains(@class,'incoming__orders__dashboard--new--subjects')]//header[@class='select-time__header']"));
            IWebElement salesItem = driver.FindElement(By.XPath("//div[contains(@class,'incoming__orders__dashboard--new')]//li[6]"));
            actions
                .MoveToElement(hopper).Click()
                .MoveToElement(salesItem).Click()
                .Build().Perform();
        }

        [Test, Order(2)]
        public void ScrollList()
        {
            LogIn();
            Thread.Sleep(11000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.querySelector('body > div:nth-child(1) > div:nth-child(1) > div:nth-child(2) > div:nth-child(2) > div:nth-child(2) > div:nth-child(3) > div:nth-child(1) > div:nth-child(2) > div:nth-child(2) > div:nth-child(2) > div:nth-child(2)')" +
                ".scrollBy(0,6000)");
        }

        [Test, Order(3)]
        public void DragAndDrop()
        {
            LogIn();
            Thread.Sleep(8000);
            IWebElement source = driver.FindElement(By.XPath("//*[@id='clients']/div/div[3]/div/div[2]/div[2]/div[1]/div[2]/div"));
            IWebElement target = driver.FindElement(By.XPath("//*[@id='clients']/div/div[3]/div/div[2]/div[2]/div[2]/div[2]/div[1]"));
            actions
                .MoveToElement(source)
                .ClickAndHold()
                .MoveToElement(target)
                .Release()
                .Build()
                .Perform();
            //WebElement frame = (WebElement)driver.FindElement(By.XPath("//div[@class='incoming__orders__board__scroll--content incoming__orders__board__scroll--v2--content']"));
            //driver.SwitchTo().Frame(frame);
            //actions.DragAndDrop(driver.FindElement(By.XPath("//*[@id='clients']/div/div[3]/div/div[2]/div[2]/div[1]/div[2]/div")), driver.FindElement(By.XPath("//*[@id='clients']/div/div[3]/div/div[2]/div[2]/div[3]/div[2]")));
            //actions.DragAndDropToOffset(driver.FindElement(By.XPath("//*[@id='clients']/div/div[3]/div/div[2]/div[2]/div[2]/div[2]/div[1]")), 300, 0);
            //actions.Build().Perform();
        }

        [Test, Order(4)]
        public void CreateApp()
        {
            LogIn();
            driver.FindElement(By.XPath("//button[contains(text(),'Заявка')]")).Click();
            WaitForElement(By.XPath("//div[contains(@class,'for_modal')]"));
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[1]/div/div/input"))
                .SendKeys("Amina");
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[3]/div/div/input"))
                .SendKeys("Oliinyk");
            driver.FindElement(By.XPath("//input[@placeholder='+38 (0**) ***-****']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='+38 (0**) ***-****']")).SendKeys("986754323");
            //Обрати сегмент
            driver.FindElement(By.XPath("//div[contains(@class,'statement-client__wrapper')]//div[2]//div[1]//div[1]//header[1]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[2]/div/div/ul/li[4]")).Click();
            //Обрати етап
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[4]/div/div/header")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[4]/div/div/ul/li[4]")).Click();
            //Обрати статус
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[6]/div/div/header")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[6]/div/div/ul/li[2]")).Click();
            //Обрати відповідального
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[7]/div/div/header")).Click();
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/main/div[1]/div[1]/div[2]/div[7]/div/div/ul/li[3]")).Click();
            //Зберегти
            driver.FindElement(By.XPath("//button[contains(text(),'Зберегти')]")).Click();
            //Перевірити повідомлення


        }

        [TearDown]
        public void CloseBrowser()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }
    }
}

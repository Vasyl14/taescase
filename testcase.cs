using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Linq;
using TestForm.Framework;
using TestForm.ServiceForm;

namespace TestForm
{    
    public class Test
    {
        private readonly IWebDriver driver;
        private static readonly TimeSpan ImplicitWait = TimeSpan.FromSeconds(3);
        private Headr headr;
        private WebDriverWait wait;

        public BaseTest()
        {
            driver = new ChromeDriver(/*Directory.GetCurrentDirectory()*/);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = ImplicitWait;
            driver.Navigate().GoToUrl(Settings.url);
            headr = new Headr(driver);
        }
      
        public void OneTimeTearDown() => driver.Quit();
        public void TestForm()
        {

            ControllerPage controllerPage = headr.ClickOnContactUs();
            bool isFormWork = controllerPage
                              .ChooseSubject()
                              .EnterEmail("test@case.com")
                              .EnterMassage("Something")
                              .PressSend().IsFormOk();
            Assert.That(isFormWork, "Form isn't work with correct datas");

        }
   
        public void TestRetrievePassword()
        {
            AuthentificationPage authntificationPage = headr.ClickOnSignIn();
            bool isEmailOk = authntificationPage
                           .ClickOnForgotPassword()
                           .PutEmail("test@case.com")
                           .ClickRetrievePassword()
                           .IsEmailOk();
            Assert.That(isEmailOk, "Email not Ok");   

        }
    }
}
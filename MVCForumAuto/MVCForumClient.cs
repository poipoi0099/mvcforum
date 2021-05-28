using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;

namespace MVCForumAuto
{
    public class MVCForumClient
    {
        private readonly ChromeDriver _webDriver;
        private readonly TestDefaults _testDefaults;

        public MVCForumClient(TestDefaults testDefaults)
        {
            _testDefaults = testDefaults;
            _webDriver = new ChromeDriver();
            _webDriver.Url = "http://localhost:8080";
        }

        ~MVCForumClient()
        {
            _webDriver.Quit();
        }
        private RegistrationPage GoToRegistrationPage()
        {
            var registerLink = _webDriver.FindElement(By.ClassName("auto-register"));
            registerLink.Click();
            return new RegistrationPage(_webDriver);    
        }
        public LatestDiscussions LatestDiscussions 
        {
            get { throw new NotImplementedException(); }
        }

        public LoggedInUser RegisterNewUserAndLogin()
        {
            var username = Guid.NewGuid().ToString();
            const string password = "123456";
            var email = $"abc@{Guid.NewGuid()}.com";
            var registrationPage = GoToRegistrationPage();
            registrationPage.Username = username;
            registrationPage.Password = password;
            registrationPage.ConfirmPassword = password;
            registrationPage.Email = email;
            registrationPage.Register();
            return new LoggedInUser(_webDriver);
        }

        public LoggedInAdmin LoginAsAdmin()
        {
            return LoginAs(_testDefaults.AdminUsername, _testDefaults.AdminPassword, () => new LoggedInAdmin(_webDriver));
        }
        private TLoggedInUser LoginAs<TLoggedInUser>(string username, string password, Func<TLoggedInUser> createLoggedInUser)
    where TLoggedInUser : LoggedInUser
        {
            var loginPage = GoToLoginPage();
            loginPage.Username = username;
            loginPage.Password = password;
            loginPage.LogOn();

            return createLoggedInUser();
        }

        private LoginPage GoToLoginPage()
        {
            var logonLink = _webDriver.FindElement(By.ClassName("auto-logon"));
            logonLink.Click();

            return new LoginPage(_webDriver);
        }

        public void TakeScreenshot(string screenshotFilename)
        {
            _webDriver.TakeScreenshot().SaveAsFile(screenshotFilename);
        }
    }
}
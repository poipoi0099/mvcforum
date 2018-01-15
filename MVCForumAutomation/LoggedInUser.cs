using System;
using OpenQA.Selenium;

namespace MVCForumAutomation
{
    public class LoggedInUser
    {
        protected readonly IWebDriver WebDriver;

        public LoggedInUser(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public Discussion CreateDiscussion(Discussion.DiscussionBuilder builder)
        {
            var newDiscussionButton = WebDriver.FindElement(By.ClassName("createtopicbutton"));
            newDiscussionButton.Click();

            var createDisucssionPage = new CreateDiscussionPage(WebDriver);
            builder.Fill(createDisucssionPage);
            createDisucssionPage.CreateDiscussion();

            return new Discussion(WebDriver);
        }

        public void Logout()
        {
            var dropdownMenu = WebDriver.FindElement(By.ClassName("dropdown"));
            dropdownMenu.Click();

            var logoffMenuItem = dropdownMenu.FindElement(By.ClassName("auto-logoff"));
            logoffMenuItem.Click();
        }
    }
}
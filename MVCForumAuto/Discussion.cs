﻿using System;
using OpenQA.Selenium;

namespace MVCForumAuto
{
    public class Discussion
    {
        private readonly IWebDriver _webDriver;

        public Discussion(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string Title
        {
            get
            {
                var titleElement = _webDriver.FindElement(By.CssSelector(".topicheading h1"));
                return titleElement.Text;
            }
        }


        public string Body
        {
            get
            {
                var bodyElement = _webDriver.FindElement(By.ClassName("postcontent"));
                return bodyElement.Text;
            }
        }

        public class DiscussionBuilder
        {
            private readonly TestDefaults _testDefaults;
            private string _body;

            public DiscussionBuilder(TestDefaults testDefaults)
            {
                _testDefaults = testDefaults;
            }

            public DiscussionBuilder Body(string body)
            {
                _body = body;
                return this;
            }

            public void Fill(CreateDiscussionPage createDiscussionPage)
            {
                createDiscussionPage.Title = Guid.NewGuid().ToString();
                createDiscussionPage.SelectCategory(_testDefaults.ExampleCategory);
                createDiscussionPage.Body = _body;

                createDiscussionPage.CreateDiscussion();
            }
        }
    }
}
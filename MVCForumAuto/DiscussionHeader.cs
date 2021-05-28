using OpenQA.Selenium;
using System;

namespace MVCForumAuto
{
    public class DiscussionHeader
    {
        private readonly IWebElement _topicRow;

        public DiscussionHeader(IWebElement topicRow)
        {
            _topicRow = topicRow;
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }
        public Discussion OpenDiscussion()
        {
            throw new NotImplementedException();
        }
    }
}
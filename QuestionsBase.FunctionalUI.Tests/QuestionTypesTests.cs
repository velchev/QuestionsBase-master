namespace QuestionsBase.FunctionalUI.Tests
{
    using Controllers;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using TestStack.Seleno.PageObjects;

    [TestFixture]
    public class QuestionTypes:Page
    {
        private const string CategoryName = "Category2";
        [Test]
        public void ShouldEnterNewQuestionTypeAndDeleteIt()
        {
            ShouldEnterNewQuestionType();
            ShouldDeleteNewQuestionType();
        }

        private void ShouldEnterNewQuestionType()
        {
            BrowserHost.Instance.Application.Browser.Navigate().GoToUrl(BrowserHost.RootUrl + @"QuestionType\Create");

            var questionTypeTextBox = BrowserHost.Instance.Application.Browser.FindElement(By.Id("Type"));
            questionTypeTextBox.SendKeys(CategoryName);

            DemoHelper.Wait();

            var createButton = BrowserHost.Instance.Application.Browser.FindElement(By.CssSelector(".btn-default"));
            createButton.Click();
            BrowserHost.Instance.Application.Browser.Navigate().GoToUrl(BrowserHost.RootUrl + @"QuestionType\Index");
            var indexPageSource = BrowserHost.Instance.Application.Browser.PageSource;

            Assert.That(indexPageSource.Contains(CategoryName));

            DemoHelper.Wait(4000);
        }

        private void ShouldDeleteNewQuestionType()
        {
            BrowserHost.Instance.Application.Browser.Navigate().GoToUrl(BrowserHost.RootUrl + @"QuestionType\Index");

            var questionTypeTextBox = BrowserHost.Instance.Application.Browser.PageSource;

            var elements = BrowserHost.Instance.Application.Browser.FindElements(By.XPath("//*[text()='Delete']"));
            elements[elements.Count-1].Click();

            DemoHelper.Wait();
            var deleteButton = BrowserHost.Instance.Application.Browser.FindElement(By.CssSelector(".btn-default"));
            deleteButton.Click();
            DemoHelper.Wait(4000);

            BrowserHost.Instance.Application.Browser.Navigate().GoToUrl(BrowserHost.RootUrl + @"QuestionType\Index");
            var indexPageSource = BrowserHost.Instance.Application.Browser.PageSource;

            Assert.That(!indexPageSource.Contains(CategoryName));
        }
    }
}

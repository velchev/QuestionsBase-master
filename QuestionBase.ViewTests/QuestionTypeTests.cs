namespace QuestionBase.ViewTests
{
    using HtmlAgilityPack;
    using NUnit.Framework;
    using QuestionsBase.Controllers;
    using QuestionsBase.Views.Home;

    [TestFixture]
    public class QuestionTypeTests
    {
        [Test]
        public void Sh()
        {
            var sut = new  Index(); 

            //var aa = sut.RenderAsHtml();

            var html = sut.RenderBody();
        }
    }
}

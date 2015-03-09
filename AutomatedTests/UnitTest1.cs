using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomatedTests
{
    using System.Data.Entity;
    using Repository;
    using Repository.Entities;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mock = new Moq.Mock<IQuestionBaseContext>();
         
            mock.Setup(x => x.SaveChanges()).Returns(1);
            //mock.Setup(x => x.QuestionType).Returns(new DbSet<QuestionTypeEntity>();

            var obj = mock.Object;
            

            Assert.AreEqual(obj.SaveChanges(), 1);

            Assert.AreNotEqual(obj.SaveChanges(), 2);

            //Assert.IsTrue(obj.ToString().Contains("fi"));
         
        }
    }
}

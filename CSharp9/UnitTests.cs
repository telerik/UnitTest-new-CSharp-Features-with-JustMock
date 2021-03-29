using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.JustMock;

namespace CSharp9
{
    [TestClass]
    public class Unittests
    {
        [TestMethod]
        public void Mock_RecordTest()
        {
            // Arrange
            var mock = Mock.Create<Teacher>("first", "second", "subject");
            Mock.Arrange(() => mock.Subject).Returns("newvalue");

            // Act
            var result = mock.Subject;

            // Assert
            Assert.AreEqual("newvalue", result);
        }

        [TestMethod]
        public void TestInit()
        {
            // Arrange  
            var fooMock = Mock.Create<Foo>();
            bool properyInitCalled = false;

            Mock.NonPublic.ArrangeSet(fooMock, "Bar", 10)
                .IgnoreInstance()
                .DoInstead(() => properyInitCalled = true);

            // Act  
            var foo = new Foo();

            // Assert 
            Assert.IsTrue(properyInitCalled);
        }

        [TestMethod]
        public void Mock_PatternMatchingTest()
        {
            // Arrange
            var foo = Mock.Create<Foo>(Behavior.CallOriginal);
            Mock.Arrange(() => foo.IsInRange(Arg.AnyInt)).Returns(true);

            // Act
            var result20 = foo.IsInRange(20);
            var result150 = foo.IsInRange(150);

            //Assert
            Assert.AreEqual(true, result20);
            Assert.AreEqual(true, result150);
        }
        
        [TestMethod]
        public void TestInit2()
        {
            // Arrange 
            var fooMock = Mock.Create<Foo>(Constructor.NotMocked);
            dynamic fooMockWrapper = Mock.NonPublic.Wrap(fooMock);

            Mock.NonPublic.Arrange(fooMockWrapper.Bar = 10)
                .IgnoreInstance()
                .MustBeCalled();
            
            // Act 
            var foo = new Foo();

            // Assert 
            Mock.NonPublic.Assert(fooMockWrapper.Bar = 10, Occurs.Once());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharp8
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestStaticLocal()
        {
            // Arrange
            var sut = new Foo();

            Mock.Local.Function.Arrange<int>(sut, "MethodWithStaticLocal", "Add", Arg.Expr.AnyInt, Arg.Expr.AnyInt).Returns(1);

            // Act
            var result = sut.MethodWithStaticLocal();

            // Assert
            Mock.Assert(sut);
            Assert.AreNotEqual(12, result);
        }

        [TestMethod]
        public async Task TestAsyncStaticLocal()
        {
            // Arrange
            var sut = new Foo();

            Mock.Local.Function.Arrange<Task<int>>(sut, "AsyncMethodWithAsyncStaticLocal", "AsyncAdd", Arg.Expr.AnyInt, Arg.Expr.AnyInt)
                .TaskResult(1);

            // Act
            var result = await sut.AsyncMethodWithAsyncStaticLocal();

            // Assert
            Mock.Assert(sut);
            Assert.AreNotEqual(12, result);
        }

        [TestMethod]
        public void TestRange()
        {
            // Arrange
            var sut = new Foo();
            var values = new int[] { 1, 2 };

            // using sut.MethodWithArrayArgument(values[^0..]) causes CS8790,
            // so the arrangement is possible throught temporary variable
            var range = values[^0..];
            Mock.Arrange(() => sut.MethodWithArrayArgument(range));

            // Act
            sut.MethodWithArrayArgument(values[^0..]);

            // Assert
            Mock.Assert(sut);
        }

        [TestMethod]
        public async Task TestAsyncEnumFromArray()
        {
            // Arrange
            var expected = new int[] { 10, 20, 30 };

            Mock.Arrange(() => Foo.GetAsyncCollection())
                .Returns(expected.GetEnumerator().ToAsyncEnumerable<int>());

            // Act
            var result = Foo.GetAsyncCollection();

            // Assert
            Mock.Assert<Foo>();
            int index = 0;
            await foreach (var number in result)
            {
                Assert.AreEqual(expected[index++], number);
            }
        }

        [TestMethod]
        public async Task TestAsyncEnumFromList()
        {
            // Arrange
            var expected = new List<int>() { 100, 200, 300 };

            Mock.Arrange(() => Foo.GetAsyncCollection())
                .Returns(expected.GetEnumerator().ToAsyncEnumerable());

            // Act
            var result = Foo.GetAsyncCollection();

            // Assert
            Mock.Assert<Foo>();
            int index = 0;
            await foreach (var number in result)
            {
                Assert.AreEqual(expected[index++], number);
            }
        }
    }
}

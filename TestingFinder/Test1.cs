using MatrixFinder;

namespace TestingFinder
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] matrix = { "catgnaun", "aecdotno", "tumtrpno", "catoperr", "dogrorat", "catirryu", "rdogrowq", "xrcattvv" };
            string[] wordStream = { "dog", "cat", "mice" };

            WordFinder wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.IsTrue(result.ElementAt(0) == "cat");
            Assert.IsTrue(result.ElementAt(1) == "dog");
            Assert.IsTrue(result.ElementAt(2) == "mice");
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] matrix = { "datgnaun", "oecdotno", "gumtrpno", "micepdog", "dogrorat", "micerryu", "rdogrowq", "xrmicevv" };
            string[] wordStream = { "dog", "cat", "mice" };

            WordFinder wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.IsTrue(result.ElementAt(0) == "dog");
            Assert.IsTrue(result.ElementAt(1) == "mice");
            Assert.IsTrue(result.ElementAt(2) == "cat");
        }
    }
}

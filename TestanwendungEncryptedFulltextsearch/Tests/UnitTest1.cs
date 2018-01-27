using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestanwendungEncryptedFulltextsearch;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void SingleUnd()
        {
            var search = new Indizierung();

            search.IndexDocuments();

            foreach (var item in search.Search("James+Bond"))
            {
                Assert.AreEqual("", item.filename);
            }

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestAdd()
        {
            var array = new ClassLibrary.Event();
            array.AddMessage("Testing");
            array.count.ShouldEqual(1);
        }
    }
}

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using ClassLibrary;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestActivity()
        {
            var activity = new Activity();
            activity.Create("Prima incercare");
            activity.Read().ShouldContain("Prima");
            activity.Read().ShouldContain("12/13/2015");
        }
    }
}

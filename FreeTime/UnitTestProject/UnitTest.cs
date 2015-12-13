using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using ClassLibrary;
using System.Globalization;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ActivityCreate()
        {
            var activity = new Activity();
            activity.Create("Prima incercare");
            activity.Read().ShouldContain("Prima");
            string dateNow = DateTime.Now.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            activity.Read().ShouldContain(dateNow);
        }

        [TestMethod]
        public void StreamAddAndList()
        {
            var text = new TextRepository(new MemoryStream());
            var activity = new Activity();
            activity.Create("primul test");
            text.Add(activity);
            activity.Create("testul 2");
            text.Add(activity);
            text.List().Contains("primul test").ShouldBeTrue();
            text.List().Contains("testul 2").ShouldBeTrue();
            text.List().Contains("testul 3").ShouldBeFalse();
        }
    }
}

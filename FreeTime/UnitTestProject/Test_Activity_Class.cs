using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using ClassLibrary;
using System.Globalization;

namespace UnitTestProject
{
    [TestClass]
    public class Test_Activity_Class
    {
        [TestMethod]
        public void Activity_Has_Value()
        {
            var activity = new Activity("Test");
            activity.ShouldNotBeNull();
        }

        [TestMethod]
        public void Activity_List_Return_A_Value()
        {
            var activity = new Activity("Test");
            activity.List().ShouldNotBeNull();
        }

        [TestMethod]
        public void Activity_Contain_Exact_Message()
        {
            var activity = new Activity("Test");
            activity.List().ShouldContain("Test");
        }

        [TestMethod]
        public void Activity_Implement_NowDate()
        {
            var activity = new Activity("Test");
            string dateNow = DateTime.Now.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            activity.List().ShouldContain(dateNow);
        }
    }
}

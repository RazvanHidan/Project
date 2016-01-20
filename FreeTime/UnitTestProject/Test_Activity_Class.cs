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
            activity.List()["message"].ShouldEqual("Test");
        }

        [TestMethod]
        public void Activity_Not_Contain()
        {
            var activity = new Activity("Test");
            activity.List()["message"].ShouldNotEqual("Testt");
        }

        [TestMethod]
        public void Activity_Contain_Date()
        {
            var activity = new Activity("Test");
            DateTime dateParse;
            bool containDate = false;
            if (DateTime.TryParse(activity.List()["date"], CultureInfo.DefaultThreadCurrentUICulture, DateTimeStyles.None, out dateParse))
                containDate = true;
            containDate.ShouldBeTrue();
        }

        [TestMethod]
        public void Activity_Change_Date()
        {
            var activity = new Activity("Test");
            activity.ChangeDate("11.02.2015");
            activity.List()["date"].ShouldContain("11.02.2015 00:00:00");
        }
    }
}

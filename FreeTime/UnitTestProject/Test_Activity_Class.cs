namespace UnitTestProject
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Should;
    using ClassLibrary;
    using System.Globalization;

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
            var containDate = false;
            if (DateTime.TryParse(activity.List()["date"], CultureInfo.DefaultThreadCurrentUICulture, DateTimeStyles.None, out dateParse))
                containDate = true;
            containDate.ShouldBeTrue();
        }

        [TestMethod]
        public void New_activity_has_no_project()
        {
            var activity = new Activity("Test");
            activity.List()["project"].ShouldEqual("n/a");
        }

        [TestMethod]
        public void New_activity_should_contain_a_project_label()
        {
            var activity = new Activity("Test", "Project I");
            activity.List()["project"].Equals("Project I");
        }
    }
}

namespace UnitTestProject
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Should;
    using ClassLibrary;
    using System.Globalization;
    using System.Collections.Generic;

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
            activity.List()["project"].ShouldEqual("");
        }

        [TestMethod]
        public void New_activity_should_contain_a_project_label()
        {
            var activity = new Activity("Test", "Project I");
            activity.List()["project"].Equals("Project I");
        }

        [TestMethod]
        public void New_activity_with_enddate()
        {
            var dictionary = new Dictionary<string, string>()
                {
                    {"project","n/a" },
                    {"enddate", "11.11.2017"},
                    {"message","Old message" }
                };
            var activity = new Activity(dictionary);
            activity.List()["enddate"].ShouldContain("11.11.2017");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormat))]
        public void New_activity_with_end_date_sooner_than_start_date()
        {
            var dictionary = new Dictionary<string, string>()
                {
                    {"project","n/a" },
                    {"date","11.11.2018" },
                    {"enddate", "11.11.2017 11:11:11"},
                    {"message","Old message" }
                };
            var activity = new Activity(dictionary);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormat))]
        public void New_activity_with_invalid_date()
        {
            var dictionary = new Dictionary<string, string>()
                {
                    {"project","n/a" },
                    {"date","11.11.2018" },
                    {"enddate", "11.11.2017 11:11:11s"},
                    {"message","Old message" }
                };
            var activity = new Activity(dictionary);
        }

        [TestMethod]
        public void Autocomplete_date_if_contain_only_years_months_days()
        {
            var dictionary = new Dictionary<string, string>()
                {
                    {"project","n/a" },
                    {"date","11.11.2016" },
                    {"enddate", "11.11.2017"},
                    {"message","Old message" }
                };
            var activity = new Activity(dictionary);
            activity.List()["enddate"].ShouldContain("11.11.2017 00:00:00");
        }

        [TestMethod]
        public void Calculating_the_duration_of_an_activity()
        {
            var dictionary = new Dictionary<string, string>()
                {
                    {"project","n/a" },
                    {"date","11.11.2016" },
                    {"enddate", "12.11.2016"},
                    {"message","Old message" }
                };
            var activity = new Activity(dictionary);
            activity.List()["duration"].ShouldContain("24h 0min");
        }
    }
}

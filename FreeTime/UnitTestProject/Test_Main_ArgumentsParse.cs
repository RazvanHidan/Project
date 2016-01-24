using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ClassLibrary;
using Should;

namespace UnitTestProject
{
    [TestClass]
    public class Test_Main_ArgumentsParse
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentMissing))]
        public void ArgumentParse_Should_trow_argument_missing_when_argument_list_is_emty()
        {
            var text = new RepositoryText(new MemoryStream());
            var args = new string[] { };
            var stream = new ArgumentsParser(args, text);
        }

        [TestMethod]
        public void ArgumentParse_Should_Add_A_Messasge_To_Stream()
        {
            var text = new RepositoryText(new MemoryStream());
            var args = new string[] { "add", "First add" };
            var stream = new ArgumentsParser(args, text);
            bool test = false;
            foreach(var activity in text.List())
            {
                if (activity.List()["message"] == args[1])
                    test = true;
            }
            test.ShouldBeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentMissing))]
        public void ArgumentParse_Should_Trow_argument_missing_when_a_argument_with_value_is_missing()
        {
            var text = new RepositoryText(new MemoryStream());
            var args = new string[] { "add" };
            var stream = new ArgumentsParser(args, text);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgument))]
        public void ArgumentParse_Should_Trow_invalid_argument_when_an_expected_parameter_is_missing()
        {
            var text = new RepositoryText(new MemoryStream());
            var args = new string[] { "invalid command" };
            var stream = new ArgumentsParser(args, text);
        }

        [TestMethod]
        public void ArgumentParse_Should_list_all_activity_from_stream()
        {
            var text = new RepositoryText(new MemoryStream());
            text.Add(new Activity("Go to"));
            text.Add(new Activity("Learn to Ride A Bike"));
            var args = new string[] {"list"};
            var stream = new ArgumentsParser(args, text);
            stream.Result().ShouldContain("Learn to Ride A Bike");
            stream.Result().ShouldContain("Go to");
        }

        [TestMethod]
        public void ArgumentParse_arguments_list_week_should_list_activity_from_last_week()
        {
            var text = new RepositoryText(new MemoryStream());
            text.Add(new Activity("Go to"));
            text.Add(new Activity("Learn to Ride A Bike"));
            var args = new string[] { "list", "week" };
            var stream = new ArgumentsParser(args, text);
            stream.Result().ShouldContain("Learn to Ride A Bike");
            stream.Result().ShouldContain("Go to");
        }
    }
}

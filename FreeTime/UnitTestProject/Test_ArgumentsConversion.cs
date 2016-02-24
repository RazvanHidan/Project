namespace UnitTestProject
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClassLibrary;
    using Should;
    [TestClass]
    public class Test_ArgumentsConversion
    {

        [TestMethod]
        public void One_argument_conversion()
        {
            var args = new string[] { "change" };
            var argsConversion = new ArgumentsConverter(args).Conversion();
            argsConversion[0].ShouldEqual("change");
        }

        [TestMethod]
        public void Separate_optional_command_and_value_by_space()
        {
            var args = new string[] { "change", "--message", "This is the Message","--date","New Date"};
            var argsConversion = new ArgumentsConverter(args).Conversion();
            argsConversion[0].ShouldEqual("change");
            argsConversion[1].ShouldEqual("--message:This is the Message");
            argsConversion[2].ShouldEqual("--date:New Date");
        }

        [TestMethod]
        public void Separate_optional_command_and_value_by_space_or_carcaterisitc_separator()
        {
            var args = new string[] { "change", "--message", "This is the Message", "--date:New Date","--enddate","End of activity"};
            var argsConversion = new ArgumentsConverter(args).Conversion();
            argsConversion[0].ShouldEqual("change");
            argsConversion[1].ShouldEqual("--message:This is the Message");
            argsConversion[2].ShouldEqual("--date:New Date");
            argsConversion[3].ShouldEqual("--enddate:End of activity");
        }

        [TestMethod]
        public void Separate_optional_command_and_null_value()
        {
            var args = new string[] { "change", "--message:", "Message", "--date:New Date", "--enddate", "End of activity" };
            var argsConversion = new ArgumentsConverter(args).Conversion();
            argsConversion[0].ShouldEqual("change");
            argsConversion[1].ShouldEqual("--message:");
            argsConversion[2].ShouldEqual("Message");
            argsConversion[3].ShouldEqual("--date:New Date");
            argsConversion[4].ShouldEqual("--enddate:End of activity");
        }

        [TestMethod]
        public void Last_argumnet_is_optioanl_argument_without_value()
        {
            var args = new string[] { "change", "--enddate" };
            var argsConversion = new ArgumentsConverter(args).Conversion();
            argsConversion[0].ShouldEqual("change");
            argsConversion[1].ShouldEqual("--enddate");
        }

        [TestMethod]
        public void Separate_two_optional_commands_one_without_value_argument_another_with_value()
        {
            var args = new string[] { "change", "--message", "--date:New Date", "End of activity", "--enddate" };
            var argsConversion = new ArgumentsConverter(args).Conversion();
            argsConversion[0].ShouldEqual("change");
            argsConversion[1].ShouldEqual("--message");
            argsConversion[2].ShouldEqual("--date:New Date");
            argsConversion[3].ShouldEqual("End of activity");
            argsConversion[4].ShouldEqual("--enddate");
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using Should;

namespace UnitTestProject
{
    [TestClass]
    public class Test_Argument_Class
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentMissing))]
        public void Should_throw_argument_is_missing_if_the_arguments_list_is_empty_when_expecting_an_argument()
        {
            var arg = new Arguments("list", new string[] { });
        }

        [TestMethod]
        public void Should_return_true_for_an_given_positional_argument()
        {
            var arg = new Arguments("list", new string[] { "list" });
            arg["list"].ShouldEqual("true");
        }

        [TestMethod]
        public void Should_return_a_positional_argument_and_an_optional_argument_when_given()
        {
            var arg = new Arguments("list [week]", new string[] { "list", "week" });
            arg["list"].ShouldEqual("true");
            arg["[week]"].ShouldEqual("true");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgument))]
        public void Should_throw_argument_missing_when_an_expected_parameter_is_missing()
        {
            var arg = new Arguments("list", new string[] { "somethingElse" });
        }

        [TestMethod]
        public void Should_return_false_when_querying_the_valaue_of_an_optional_argument_which_is_missing()
        {
            var arg = new Arguments("list [week]", "list".Split(" ".ToCharArray()));
            arg["list"].ShouldEqual("true");
            arg["[week]"].ShouldEqual("false");
        }

        [TestMethod]
        public void Should_support_positional_arguments_with_value()
        {
            const string workingOnNextProject = "working on my next big project";
            var arg = new Arguments("add <message>", new string[] { "add", workingOnNextProject });
            arg["add"].ShouldEqual("true");
            arg["<message>"].ShouldEqual(workingOnNextProject);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentMissing))]
        public void Should_throw_argument_missing_when_a_positional_argument_with_value_is_missing()
        {
            var arg = new Arguments("add <message>", new string[] { "add" });
        }

        [TestMethod]
        public void Should_handle_multiple_commands()
        {
            var schema = "add <message>\n" +
                         "list [week]";
            var arg = new Arguments(schema, new string[] { "add", "m" });
            arg["add"].ShouldEqual("true");
            arg["<message>"].ShouldEqual("m");
            arg["list"].ShouldEqual("false");
            arg["[week]"].ShouldEqual("false");
        }

        [TestMethod]
        public void Should_handle_multiple_commandsII()
        {
            var schema = "list [week]\n" +
                         "add <message>";
            var arg = new Arguments(schema, new string[] { "list", "week" });
            arg["list"].ShouldEqual("true");
            arg["[week]"].ShouldEqual("true");
            arg["add"].ShouldEqual("false");
            arg["<message>"].ShouldEqual("false");
        }

        [TestMethod]
        public void Should_handle_triple_commands()
        {
            var schema = "list [week]\n" +
                         "add <message>\n" +
                         "help [list]";
            var arg = new Arguments(schema, new string[] { "list", "week", "add", "Go To" });
            arg["list"].ShouldEqual("true");
            arg["[week]"].ShouldEqual("true");
            arg["add"].ShouldEqual("true");
            arg["<message>"].ShouldEqual("Go To");
            arg["help"].ShouldEqual("false");
            arg["[list]"].ShouldEqual("false");
        }

        [TestMethod]
        public void Should_handle_optional_multiple_command_in_order()
        {
            var schema = "change <id> [--date<date>] [--message<message>]";
            var arg = new Arguments(schema, new string[] { "change", "1", "--d 02.03.2001" });
            arg["change"].ShouldEqual("true");
            arg["<id>"].ShouldEqual("1");
            arg["[--date<date>]"].ShouldEqual("02.03.2001");
            arg["[--message<message>]"].ShouldEqual("false");
        }

        [TestMethod]
        public void Should_handle_optional_multiple_command_stepover()
        {
            var schema = "change <id> [--date<date>] [--message<message>]";
            var arg = new Arguments(schema, new string[] { "change", "5", "--m Need to work" });
            arg["change"].ShouldEqual("true");
            arg["<id>"].ShouldEqual("5");
            arg["[--date<date>]"].ShouldEqual("false");
            arg["[--message<message>]"].ShouldEqual("Need to work");
        }
    }
}

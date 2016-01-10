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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void No_argument_return_exception()
        {
            var arg = new Arguments(new List < string >{ });
            arg.Parse();
        }

        [TestMethod]
        public void One_valid_argument_should_return_value()
        {
            var list = new List<string> { "list" };
            var arg = new Arguments(list);
            arg[list].ShouldEqual("true");
        }

        [TestMethod]
        public void Two_valid_argument_should_return_value()
        {
            var arg = new Arguments(new List<string> { "list","week" });
            arg.Parse().ShouldEqual("true");
        }
    }
}

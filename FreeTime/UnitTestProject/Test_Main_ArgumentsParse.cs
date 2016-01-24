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
    }
}

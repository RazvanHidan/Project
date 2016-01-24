using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.IO;
using Should;

namespace UnitTestProject
{
    [TestClass]
    public class Test_RepositoryCSV
    {

        [TestMethod]
        public void CSV_Repository_Should_Contain_added_activity()
        {
            var csv = new RepositoryCSV(new MemoryStream());
            var activity = new Activity("First add");
        }
    }
}

namespace UnitTestProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClassLibrary;
    using System.IO;

    [TestClass]
    public class Test_RepositoryCSV
    {

        [TestMethod]
        public void CSV_Repository_Should_Contain_added_activity()
        {
            var csv = new DocumentCSV(new MemoryStream());
            var activity = new Activity("First add");
        }
    }
}

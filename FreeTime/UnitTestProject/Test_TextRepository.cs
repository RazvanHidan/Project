using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using ClassLibrary;
using System.Globalization;

namespace UnitTestProject
{
    [TestClass]
    public class Test_Class
    {
        [TestMethod]
        public void TextRepository_Initialization()
        {
            var text = new RepositoryText(new MemoryStream());
            text.ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_First_Add()
        {
            var text = new RepositoryText(new MemoryStream());
            var activity=new Activity("First add");
            text.Add(activity);
            text.ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_Add()
        {
            var text = new RepositoryText(new MemoryStream());
            var activity = new Activity("First add");
            text.Add(activity);
            activity = new Activity("Second add");
            text.Add(activity);
            text.ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_List_Is_Not_Null()
        {
            var text = new RepositoryText(new MemoryStream());
            var activity = new Activity("First add");
            text.Add(activity);
            text.List().ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_List_Return_Contents()
        {
            var text = new RepositoryText(new MemoryStream());
            var activity = new Activity("First add");
            text.Add(activity);
            text.List().Equals(activity);
        }

        [TestMethod]
        public void TextRepository_ListWeek()
        {
            var text = new RepositoryText(new MemoryStream());
            var activity=new Activity("First test");
            text.Add(activity);
            activity=new Activity("Second 2");
            text.Add(activity);
            text.ListWeek().Equals(activity);
        }

        [TestMethod]
        public void TextRepository_Change_Date()
        {
            var text = new RepositoryText(new MemoryStream());
            var activity = new Activity("First add");
            text.Add(activity);
            activity = new Activity("Second add");
            text.Add(activity);
            text.ChangeDate(2, "5/4/2001");
            text.List().ShouldNotBeNull();
        }
    }
}

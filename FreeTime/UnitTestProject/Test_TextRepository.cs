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
            var text = new TextRepository(new MemoryStream());
            text.ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_First_Add()
        {
            var text = new TextRepository(new MemoryStream());
            var activity=new Activity("First add");
            text.Add(activity);
            text.ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_Add()
        {
            var text = new TextRepository(new MemoryStream());
            var activity = new Activity("First add");
            text.Add(activity);
            activity = new Activity("Second add");
            text.Add(activity);
            text.ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_List_Return_A_Value()
        {
            var text = new TextRepository(new MemoryStream());
            var activity = new Activity("First add");
            text.Add(activity);
            text.List().ShouldNotBeNull();
        }

        [TestMethod]
        public void TextRepository_List_Return_Contents()
        {
            var text = new TextRepository(new MemoryStream());
            var activity = new Activity("First add");
            text.Add(activity);
            text.List().Contains("First add").ShouldBeTrue();
        }

        [TestMethod]
        public void TextRepository_ListWeek()
        {
            var text = new TextRepository(new MemoryStream());
            var activity=new Activity("primul test");
            text.Add(activity);
            activity=new Activity("testul 2");
            text.Add(activity);
            text.ListWeek().Contains("primul test").ShouldBeTrue();
            text.ListWeek().Contains("testul 2").ShouldBeTrue();
            text.ListWeek().Contains("testul 3").ShouldBeFalse();
        }
    }
}

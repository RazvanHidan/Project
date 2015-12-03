using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestAdd()
        {
            var array = new ClassLibrary.Activity();
            array.ShouldNotBeNull();
            string path = Directory.GetCurrentDirectory() + "\\" + "Add.txt";
            array.Add("hello Motto",path);
            File.Exists(path).ShouldBeTrue();
            File.ReadAllText(path).Contains("hello").ShouldBeTrue();
            File.ReadAllText(path).Contains("Motto").ShouldBeTrue();
            File.Delete(path);
        }

        [TestMethod]
        public void TestList()
        {
            var array = new ClassLibrary.Activity();
            string path = Directory.GetCurrentDirectory() + "\\" + "List.txt";
            string date = Convert.ToString(DateTime.Now.Year);
            array.Add("hello Motto and Eu", path);
            array.List(path).Contains("hello Motto and Eu").ShouldBeTrue();
            array.List(path).Contains(date).ShouldBeTrue();
            array.List(path).Contains("add").ShouldBeFalse();
            File.Delete(path);
        }

        [TestMethod]
        public void TestListLastWeek()
        {
            var array = new ClassLibrary.Activity();
            string path = Directory.GetCurrentDirectory() + "\\" + "List.txt";
            string date = Convert.ToString(DateTime.Now.Year);
            array.Add("hello Motto and Eu", path);
            array.ListWeek(path).Contains("hello Motto and Eu").ShouldBeTrue();
            array.ListWeek(path).Contains(date).ShouldBeTrue();
            array.ListWeek(path).Contains("add").ShouldBeFalse();
            File.Delete(path);
        }
    }
}

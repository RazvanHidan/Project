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
            string path = Directory.GetCurrentDirectory() + "\\" + "Add.txt";
            var array = new ClassLibrary.Content(path);
            array.ShouldNotBeNull();
            array.Add("hello Motto");
            File.Exists(path).ShouldBeTrue();
            File.ReadAllText(path).Contains("hello").ShouldBeTrue();
            File.ReadAllText(path).Contains("Motto").ShouldBeTrue();
            File.Delete(path);
        }

        [TestMethod]
        public void TestList()
        {
            
            string path = Directory.GetCurrentDirectory() + "\\" + "List.txt";
            var array = new ClassLibrary.Content(path);
            string date = Convert.ToString(DateTime.Now.Year);
            array.Add("hello Motto and Eu");
            array.List().Contains("hello Motto and Eu").ShouldBeTrue();
            array.List().Contains(date).ShouldBeTrue();
            array.List().Contains("add").ShouldBeFalse();
            File.Delete(path);
        }

        [TestMethod]
        public void TestListLastWeek()
        {
            string path = Directory.GetCurrentDirectory() + "\\" + "List.txt";
            var array = new ClassLibrary.Content(path);
            string date = Convert.ToString(DateTime.Now.Year);
            array.Add("hello Motto and Eu");
            array.ListWeek().Contains("hello Motto and Eu").ShouldBeTrue();
            array.ListWeek().Contains(date).ShouldBeTrue();
            array.ListWeek().Contains("add").ShouldBeFalse();
            File.Delete(path);
        }

        [TestMethod]
        public void TestAddTextFile()
        {
            FileStream test = File.Create(Directory.GetCurrentDirectory() + "\\" + "List.txt");
            var continut = new ClassLibrary.TextFile(test);
            continut.Add("Primul mesaj");
            continut.Add("Al Doilea mesaj");
            continut.ShouldNotBeNull();
        }
    }
}

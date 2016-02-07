using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ClassLibrary;
using Should;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class Test_Commands
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentMissing))]
        public void Should_throw_exception_ArgumentMissing_if_list_of_arguments_is_emty()
        {
            var command = new Commands(new string[] { }, new MemoryStream());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentMissing))]
        public void Should_throw_exception_ArgumentMissing_if_an_argument_is_missing()
        {
            var command = new Commands(new string[] { "add" }, new MemoryStream());
            command.Execute();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidArgument))]
        public void Should_throw_exception_InvalidArgument_if_argument_is_not_valid()
        {
            var command = new Commands(new string[] { "adding" }, new MemoryStream());
        }

        [TestMethod]
        public void Should_add_to_stream_a_new_activity()
        {
            using (var stream = new MemoryStream())
            {
                var command = new Commands(new string[] { "add", "New Activity" }, stream);
                command.Execute().Equals("Added a new activity");
                ActivitysField(stream, "message").ShouldContain("New Activity");
            }
        }

        [TestMethod]
        public void Should_list_the_contents_of_repository()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var command = new Commands(new string[] { "list" }, stream);
                command.Execute().ShouldContain("activity1");
                command.Execute().ShouldContain("activity9");
            }
        }

        [TestMethod]
        public void Should_change_activity_message()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryText(stream);
                repository.Add(new Activity("12345678", "01.01.2001", "Old Message",""));
                var command = new Commands(new string[] { "change", "12345678", "--m:New Message" }, stream);
                command.Execute();
                ActivitysField(stream, "message").ShouldContain("New Message");
            }
        }

        [TestMethod]
        public void Should_change_activity_date()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryText(stream);
                repository.Add(new Activity("12345678", "01.01.2001", "Old Message",""));
                var command = new Commands(new string[] { "change", "12345678", "--d:11.11.1010" }, stream);
                command.Execute();
                ActivitysField(stream, "date").ShouldContain("11.11.1010");
            }
        }

        [TestMethod]
        public void Should_change_activity_date_and_message()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryText(stream);
                repository.Add(new Activity("12345678", "01.01.2001", "Old Message", ""));
                var command = new Commands(new string[] { "change", "12345678", "--d:11.11.1010", "--m:New message" }, stream);
                command.Execute();
                ActivitysField(stream, "date").ShouldContain("11.11.1010");
                ActivitysField(stream, "message").ShouldContain("New message");
            }
        }

        [TestMethod]
        public void Should_show_the_help_message()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryText(stream);
                var command = new Commands(new string[] { "help" }, stream);
                command.Execute().ShouldContain("list [week]");
                command.Execute().ShouldContain("add");
            }
        }

        [TestMethod]
        public void Should_list_the_activity_from_the_last_week()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryText(stream);
                var date = DateTime.UtcNow.AddDays(-8).ToString();
                repository.Add(new Activity("12345678", date, "Old Message",""));
                var command = new Commands(new string[] { "list", "week" }, stream);
                command.Execute().ShouldNotContain("Old Message");
            }
        }

        [TestMethod]
        public void Should_List_Table_Header_written_with_uppercase()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryText(stream);
                repository.Add(new Activity("Testing"));
                var command = new Commands(new string[] { "list" }, stream);
                command.Execute().ShouldContain("ID");
                command.Execute().ShouldContain("DATE");
            }
        }
        
        [TestMethod]
        public void Should_add_to_stream_a_new_activity_with_project_label()
        {
            using (var stream = new MemoryStream())
            {
                var command = new Commands(new string[] { "add", "--project:Project Number One", "New Activity" }, stream);
                command.Execute().Equals("Added a new activity");
                ActivitysField(stream, "message").ShouldContain("New Activity");
                ActivitysField(stream, "project").ShouldContain("Project Number One");
            }
        }

        [TestMethod]
        public void If_an_element_can_not_be_displayed_wholly_will_end_with_3_points()
        {
            using (var stream = new MemoryStream())
            {
                var command = new Commands(new string[] { "add", "--project:Project Number One and 3 4 5 6", "New Activity" }, stream);
                command.Execute().Equals("Added a new activity");
                ActivitysField(stream, "message").ShouldContain("New Activity");
                ActivitysField(stream, "project").ShouldContain("Project Number One and 3 4 5 6");
                command = new Commands(new string[] { "list" }, stream);
                command.Execute().ShouldNotContain("Project Number One and 3 4 5 6");
                command.Execute().ShouldContain("...");
            }
        }

        [TestMethod]
        public void Should_change_activity_project()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryText(stream);
                repository.Add(new Activity("12345678", "01.01.2001", "Old Message", ""));
                var command = new Commands(new string[] { "change", "12345678", "--p:Project OK", "--d:12.12.1010", "--m:New message" }, stream);
                command.Execute();
                ActivitysField(stream, "project").ShouldContain("Project OK");
                ActivitysField(stream, "date").ShouldContain("12.12.1010");
                ActivitysField(stream, "message").ShouldContain("New message");
            }
        }

        private static void GenerateActivity(MemoryStream stream)
        {
            var repository = new RepositoryText(stream);
            for (int i = 1; i < 10; i++)
                repository.Add(new Activity("activity" + i.ToString()));
        }

        private static IEnumerable<string> ActivitysField(MemoryStream stream,string field)
        {
            var repository = new RepositoryText(stream);
            foreach(var activity in repository.List())
            {
                yield return activity.List()[field];
            }
        }
    }
}

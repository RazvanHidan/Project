namespace UnitTestProject
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using ClassLibrary;
    using Should;

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
        [ExpectedException(typeof(RepositoryEmty))]
        public void Should_throw_exception_RepositoryEmty_if_try_to_list_a_emty_repository()
        {
            var command = new Commands(new string[] { "list" }, new MemoryStream());
            command.Execute();
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
        [ExpectedException(typeof(InvalidArgument))]
        public void Should_throw_invalid_argument_if_the_optional_command_is_incomplet()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--proj" }, stream);
                command.Execute();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormat))]
        public void Should_throw_invalid_format_if_the_optional_command_date_is_not_valid_format()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--date:date" }, stream);
                command.Execute();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormat))]
        public void Should_throw_InvalidFormat_if_date_is_not_valid_format()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--date:\" \"" }, stream);
                command.Execute();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgument))]
        public void Should_throw_invalid_argument_if_optional_command_change_message_is_emty()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--message:" }, stream);
                command.Execute();
            }
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
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--message:New Message" }, stream);
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
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--date:11.11.1010" }, stream);
                command.Execute();
                ActivitysField(stream, "date").ShouldContain("11.11.1010 00:00:00");
            }
        }

        [TestMethod]
        public void Should_change_activity_date_and_message()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--date:11.11.1010", "--message:New message" }, stream);
                command.Execute();
                ActivitysField(stream, "date").ShouldContain("11.11.1010 00:00:00");
                ActivitysField(stream, "message").ShouldContain("New message");
            }
        }

        [TestMethod]
        public void Should_show_the_help_message()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryXML(stream);
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
                var repository = new RepositoryXML(stream);
                var date = DateTime.UtcNow.AddDays(-8).ToString();
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date",date },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                activity["date"] = DateTime.UtcNow.ToString();
                activity["message"] = "New Message";
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "list", "week" }, stream);
                command.Execute().ShouldContain("New Message");
                command.Execute().ShouldNotContain("Old Message");
            }
        }

        [TestMethod]
        public void Should_List_Table_Header_written_with_uppercase()
        {
            using (var stream = new MemoryStream())
            {
                GenerateActivity(stream);
                var repository = new RepositoryXML(stream);
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
                command.Execute().ShouldContain("Project ...");
            }
        }

        [TestMethod]
        public void Should_change_activity_project()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--project:Project OK", "--date:12.12.1010", "--message:New message" }, stream);
                command.Execute();
                ActivitysField(stream, "project").ShouldContain("Project OK");
                ActivitysField(stream, "date").ShouldContain("12.12.1010 00:00:00");
                ActivitysField(stream, "message").ShouldContain("New message");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidArgument))]
        public void Should_throw_invalid_argument_if_optinal_argument_is_not_written_complet()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var command = new Commands(new string[] { "add", "--proj", "New message" }, stream);
                command.Execute();
                ActivitysField(stream, "message").ShouldContain("New message");
            }
        }

        [TestMethod]
        public void Should_change_activity_enddate()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--project:Project OK", "--enddate:10.10.2002", "--date:12.12.1010", "--message:New message" }, stream);
                command.Execute();
                ActivitysField(stream, "project").ShouldContain("Project OK");
                ActivitysField(stream, "enddate").ShouldContain("10.10.2002 00:00:00");
                ActivitysField(stream, "date").ShouldContain("12.12.1010 00:00:00");
                ActivitysField(stream, "message").ShouldContain("New message");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormat))]
        public void Should_throw_InvalidFormat_exception_if_try_to_change_Enddate_to_be_earlier_than_startDate()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "change", "12345678", "--project:Project OK", "--enddate:10.10.2002", "--message:New message" }, stream);
                command.Execute();
            }
        }

        [TestMethod]
        public void Should_list_activity_duration()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","n/a" },
                    {"date","11.12.2015" },
                    {"enddate","12.12.2015 03:19:00" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                var command = new Commands(new string[] { "list" }, stream);
                var s = command.Execute();
                command.Execute().ShouldContain("1d 03h 19m");
            }
        }

        [TestMethod]
        public void Should_list_Projects_duration_and_name()
        {
            using (var stream = new MemoryStream())
            {
                var repository = new RepositoryXML(stream);
                var activity = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","Razvan" },
                    {"date","11.12.2015" },
                    {"enddate","12.12.2015 " },
                    {"message","Old message" }
                };
                var activity1 = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","Razvan" },
                    {"date","11.12.2015" },
                    {"enddate","11.12.2015 02:19:00" },
                    {"message","Old message" }
                };
                var activity2 = new Dictionary<string, string>()
                {
                    {"id","12345678" },
                    {"project","New Project" },
                    {"date","11.12.2015" },
                    {"enddate","11.12.2015 03:57:00" },
                    {"message","Old message" }
                };
                repository.Add(new Activity(activity));
                repository.Add(new Activity(activity1));
                repository.Add(new Activity(activity2));
                var command = new Commands(new string[] { "list","projects" }, stream);
                var result = command.Execute();
                result.ShouldContain("1d 02h 19m");
                result.ShouldContain("Razvan");
                result.ShouldContain("03h 57m");
                result.ShouldContain("New Project");
            }
        }


        private static void GenerateActivity(MemoryStream stream)
        {
            var repository = new RepositoryXML(stream);
            for (int i = 1; i < 10; i++)
                repository.Add(new Activity("activity" + i.ToString()));
        }

        private static IEnumerable<string> ActivitysField(MemoryStream stream, string field)
        {
            var repository = new RepositoryXML(stream);
            foreach (var activity in repository.List())
            {
                yield return activity.List()[field];
            }
        }
    }
}

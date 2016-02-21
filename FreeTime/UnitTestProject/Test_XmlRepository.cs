namespace UnitTestProject
{
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClassLibrary;
    using System.IO;
    using Should;

    [TestClass]
    public class Test_XmlRepository
    {
        [TestMethod]
        public void Xml_Repository_is_XML_format()
        {
            var memoryStream = new MemoryStream();
            var text = new RepositoryXML(memoryStream);
            text.Add(new Activity("First add"));
            var memoryStreamContent = StreamContent(memoryStream);
            memoryStreamContent.ShouldContain("<repository>");
            memoryStreamContent.ShouldContain("</repository>");
        }

        [TestMethod]
        public void Xml_Repository_add_a_new_activity()
        {
            var memoryStream = new MemoryStream();
            var text = new RepositoryXML(memoryStream);
            text.Add(new Activity("First add"));
            var memoryStreamContent = StreamContent(memoryStream);
            memoryStreamContent.ShouldContain("<message>First add</message>");
        }

        [TestMethod]
        public void Xml_Repository_List_returns_a_list_of_activities()
        {
            var text = new RepositoryXML(new MemoryStream());
            text.Add(new Activity("First add"));
            text.Add(new Activity("First adding"));
            text.Add(new Activity("Message is full"));
            var contain = false;
            var listContent = StreamList(text);
            listContent.ToString().ShouldContain("Message is full");
        }

        [TestMethod]
        public void Xml_Repository_change_message()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","n/a" },
                {"date","11.12.2015" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            var beforStremList = StreamList(text);
            beforStremList.ShouldContain("Old activity");
            text.Change("12345678", new Dictionary<string, string> { { "message", "New message" } });
            var afterStremList = StreamList(text);
            afterStremList.ShouldContain("New message");
        }

        [TestMethod]
        public void Xml_Repository_change_date()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","n/a" },
                {"date","11.11.2015 22:11:25" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            var beforStremList = StreamList(text);
            beforStremList.ShouldContain("11.11.2015 22:11:25");
            text.Change("12345678", new Dictionary<string, string> { { "date", "09.10.2002 12:11:23" } });
            var afterStremList = StreamList(text);
            afterStremList.ShouldNotContain("11.11.2015 22:11:25");
            afterStremList.ShouldContain("09.10.2002 12:11:23");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormat))]
        public void Repository_change_date_throw_InvalidFormat_Exception_if_date_format_is_not_valid()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","n/a" },
                {"date","11.12.2015 22:11:2015" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            text.Change("12345678", new Dictionary<string, string> { { "date", "25.25.2002" } });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidID))]
        public void Throw_InvalidID_Exception_if_try_to_change_an_invalid_ID()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","n/a" },
                {"date","11.11.2015 22:11:20" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            text.Change("12345677",new Dictionary<string, string> { { "message", "This is the message" } });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormat))]
        public void Throw_InvalidFormat_Exception_if_try_to_change_a_date_with_invalid_format_of_date()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","n/a" },
                {"date","11.11.2015 22:11:20" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            text.Change("12345678",new Dictionary<string, string> { { "date", "11.12.yyyy" } });
        }

        [TestMethod]
        public void Xml_Repository_delete_activity()
        {
            var stream = new MemoryStream();
            var text = new RepositoryXML(stream);
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","n/a" },
                {"date","11.11.2015 22:11:25" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            var beforStremList = StreamList(text);
            beforStremList.ShouldContain("11.11.2015 22:11:25");
            var streamLength = stream.Length;
            text.Delete("12345678");
            streamLength.ShouldNotEqual(stream.Length);
        }

        [TestMethod]
        public void Xml_Repository_Activity_Duration_calculation()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","n/a" },
                {"date","11.11.2015 10:11:00" },
                {"enddate","11.11.2015 10:11:00" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            var beforStremList = StreamList(text);
            beforStremList.ShouldContain("00h 00m");
            text.Change("12345678", new Dictionary<string, string> { { "enddate", "13.11.2015 10:43:00" } });
            var afterStremList = StreamList(text);
            afterStremList.ShouldContain("2d 00h 32m");
        }

        [TestMethod]
        public void Xml_Repository_ListProject_count_activities_with_the_same_project()
        {
            var text = new RepositoryXML(new MemoryStream());
            text.Add(new Activity("New activity", "Proj 1"));
            text.Add(new Activity("New activity1", "Proj 2"));
            text.Add(new Activity("New activity2", "Proj 1"));
            text.Add(new Activity("New activity3", "Proj 3"));
            text.Add(new Activity("New activity4", "Proj 1"));
            text.Add(new Activity("New activity4", "Proj 3"));
            text.ListProject().Find(x => x.name.Contains("Proj 1")).count.ShouldEqual(3);
            text.ListProject().Find(x => x.name.Contains("Proj 3")).count.ShouldEqual(2);
            text.ListProject().Find(x => x.name.Contains("Proj 2")).count.ShouldEqual(1);
        }

        [TestMethod]
        public void Xml_Repository_ListProject_count_activities_with_the_same_project_and_calculate_duration()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","Project One" },
                {"date","11.11.2015 10:11:00" },
                {"enddate","11.11.2015 11:46:00" },
                {"message","Old activity" }
            };
            var activity1 = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","Project Ten" },
                {"date","11.11.2015 5:11:00" },
                {"enddate","11.11.2015 10:33:00" },
                {"message","Activity 1" }
            };
            var activity2 = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","Project One" },
                {"date","11.11.2015 10:11:00" },
                {"enddate","11.11.2015 14:56:00" },
                {"message","activity3" }
            };
            text.Add(new Activity(activity));
            text.Add(new Activity(activity1));
            text.Add(new Activity(activity2));
            text.ListProject().Find(x => x.name.Contains("Project One")).List()["duration"].ShouldEqual("06h 20m");
            text.ListProject().Find(x => x.name.Contains("Project Ten")).List()["duration"].ShouldEqual("05h 22m");
        }

        [TestMethod]
        public void Xml_Repository_ListProject_if_duration_is_over_one_day_list_days()
        {
            var text = new RepositoryXML(new MemoryStream());
            var activity = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","Project One" },
                {"date","11.11.2015 10:11:00" },
                {"enddate","11.11.2015 22:46:00" },
                {"message","Old activity" }
            };
            var activity1 = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","Project Ten" },
                {"date","11.11.2015 5:11:00" },
                {"enddate","11.11.2015 10:33:00" },
                {"message","Activity 1" }
            };
            var activity2 = new Dictionary<string, string>()
            {
                {"id","12345678" },
                {"project","Project One" },
                {"date","11.11.2015 10:11:00" },
                {"enddate","11.11.2015 23:56:00" },
                {"message","activity3" }
            };
            text.Add(new Activity(activity));
            text.Add(new Activity(activity1));
            text.Add(new Activity(activity2));
            text.ListProject().Find(x => x.name.Contains("Project One")).List()["duration"].ShouldEqual("1d 02h 20m");
            text.ListProject().Find(x => x.name.Contains("Project Ten")).List()["duration"].ShouldEqual("05h 22m");
        }

        private static string StreamList(RepositoryXML text)
        {
            var listContent = new StringBuilder();
            foreach (var activity in text.List())
            {
                foreach (var value in activity.List().Values)
                    listContent.Append(value);
            }
            return listContent.ToString();
        }

        private static string StreamContent(MemoryStream memoryStream)
        {
            string memoryStreamContent = "";
            memoryStream.Position = 0;
            using (var read = new StreamReader(memoryStream))
            {
                memoryStreamContent = read.ReadToEnd();
            }
            return memoryStreamContent;
        }
    }
}

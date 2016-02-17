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
            text.Delete("12345678");
            var afterStremList = StreamList(text);
            afterStremList.ShouldNotContain("11.11.2015 22:11:25");
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
            beforStremList.ShouldContain("0h 0min");
            text.Change("12345678", new Dictionary<string, string> { { "enddate", "13.11.2015 10:43:00" } });
            var afterStremList = StreamList(text);
            afterStremList.ShouldContain("48h 32min");
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

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
            text.Change("12345678", "message", "New message");
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
                {"date","11.12.2015 22:11:2015" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            var beforStremList = StreamList(text);
            beforStremList.ShouldContain("11.12.2015 22:11:2015");
            text.Change("12345678", "date", "12.10.2056 12:11:23");
            var afterStremList = StreamList(text);
            afterStremList.ShouldContain("12.10.2056 12:11:23");
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
            text.Change("12345678", "date", "25.25.2002");
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
                {"date","11.12.2015 22:11:2015" },
                {"message","Old activity" }
            };
            text.Add(new Activity(activity));
            text.Change("12345677", "message", "This is the message");
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

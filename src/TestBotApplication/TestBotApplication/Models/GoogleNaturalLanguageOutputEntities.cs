using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestBotApplication.Models
{
    public class GoogleNaturalLanguageOutputEntities
    {
        public List<Entity> entities { get; set; }
        public string language { get; set; }
        public class Metadata
        {
            public string mid { get; set; }
            public string wikipedia_url { get; set; }
        }

        public class Text
        {
            public string content { get; set; }
            public int beginOffset { get; set; }
        }

        public class Mention
        {
            public Text text { get; set; }
            public string type { get; set; }
        }

        public class Entity
        {
            public string name { get; set; }
            public string type { get; set; }
            public Metadata metadata { get; set; }
            public double salience { get; set; }
            public List<Mention> mentions { get; set; }
        }
    }
}
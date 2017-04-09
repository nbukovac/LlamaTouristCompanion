using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestBotApplication.Models
{
    public class NaturalLanguagePostRequestcs
    {
        public string encodingType { get; set; }
        public Document document { get; set; }
        public class Document
        {
            public string type { get; set; }
            public string content { get; set; }
        }

        public NaturalLanguagePostRequestcs()
        {
            document = new Document();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestBotApplication.Models
{
    public class GoogleTranslationApiModel
    {
        public Data data { get; set; }
        public class Translation
        {
            public string translatedText { get; set; }
        }

        public class Data
        {
            public List<Translation> translations { get; set; }
        }
    }
}
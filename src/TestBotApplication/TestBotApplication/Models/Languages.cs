using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestBotApplication.Models
{
    public class LanguagesDict
    {
        public Dictionary<int, string> LanguageCodes {get; set;}

        public LanguagesDict()
        {
            LanguageCodes = new Dictionary<int, string>();
            LanguageCodes.Add(1, "hr");
            LanguageCodes.Add(2, "en");
            LanguageCodes.Add(3, "de");
        }
    }
}
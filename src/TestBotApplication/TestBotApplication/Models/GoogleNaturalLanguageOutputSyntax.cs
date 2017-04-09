using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestBotApplication.Models
{
    public class GoogleNaturalLanguageOutputSyntax
    {
        public List<Sentence> sentences { get; set; }
        public List<Token> tokens { get; set; }
        public string language { get; set; }
        public class Text
        {
            public string content { get; set; }
            public int beginOffset { get; set; }
        }

        public class Sentence
        {
            public Text text { get; set; }
        }

        public class PartOfSpeech
        {
            public string tag { get; set; }
        }

        public class DependencyEdge
        {
            public int headTokenIndex { get; set; }
            public string label { get; set; }
        }

        public class Token
        {
            public Text text { get; set; }
            public PartOfSpeech partOfSpeech { get; set; }
            public DependencyEdge dependencyEdge { get; set; }
            public string lemma { get; set; }
        }
    }
}
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using TestBotApplication.Models;
using System.Net.Http.Headers;
using System.Text;
using static TestBotApplication.Models.GoogleMapLocationModel;

namespace TestBotApplication.Services
{
    public class GoogleTranslateService
    {
        public string TranslateToEnglish(string inputText, string fromLanguage)
        {
            string googleTranslateApiKey = ConfigurationManager.AppSettings["GoogleTransalteApiKey"];
            var encodedString = HttpUtility.UrlEncode(inputText);
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://translation.googleapis.com/language/translate/");
                HttpResponseMessage googleApiResponse =  client.GetAsync("v2?key="+ googleTranslateApiKey + "&source="+ fromLanguage + "&target=en&q="+ encodedString).Result;
                googleApiResponse.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<GoogleTranslationApiModel>(googleApiResponse.Content.ReadAsStringAsync().Result);
                return result.data.translations.FirstOrDefault() != null ? result.data.translations.FirstOrDefault().translatedText : "";
            }
        }
        public string TranslateToSelectedLanguage(string inputText, string selectedLanguage)
        {
            if (selectedLanguage == "en")
                return inputText;

            string googleTranslateApiKey = ConfigurationManager.AppSettings["GoogleTransalteApiKey"];
            var encodedString = HttpUtility.UrlEncode(inputText);
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://translation.googleapis.com/language/translate/");
                HttpResponseMessage googleApiResponse = client.GetAsync("v2?key=" + googleTranslateApiKey + "&source=en&target=" + selectedLanguage + "&q=" + encodedString).Result;
                googleApiResponse.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<GoogleTranslationApiModel>(googleApiResponse.Content.ReadAsStringAsync().Result);
                return result.data.translations.FirstOrDefault() != null ? result.data.translations.FirstOrDefault().translatedText : "";
            }
        }

        public string SentanceDetect(string inputText)
        {
            
            string googleNaturalLanguageApiKey = ConfigurationManager.AppSettings["GoogleNaturalLanguageApiKey"];
            var obj = new NaturalLanguagePostRequestcs();
            obj.encodingType = "UTF8";
            obj.document.type = "PLAIN_TEXT";
            obj.document.content = inputText;


            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                client.Timeout = new TimeSpan(0, 0, 60);
                HttpResponseMessage googleApiSytaxResponse = client.PostAsync("https://language.googleapis.com/v1/documents:analyzeSyntax?key=" + googleNaturalLanguageApiKey, httpContent).Result;
                googleApiSytaxResponse.EnsureSuccessStatusCode();
                var resultSytax = JsonConvert.DeserializeObject<GoogleNaturalLanguageOutputSyntax>(googleApiSytaxResponse.Content.ReadAsStringAsync().Result);

                var data = string.Join(",", resultSytax.tokens.Where(x => x.lemma.Length >= 3).Select(x => x.lemma).ToList());
                return data;
            }
        }

        public Location GetLocationFromAddress(string address)
        {
            string googleMapsApiKey = ConfigurationManager.AppSettings["GoogleMapLocationApiKey"];

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
                client.Timeout = new TimeSpan(0, 0, 60);
                HttpResponseMessage googleApiResponse = client.GetAsync("json?address=" + address + "&key="+ googleMapsApiKey).Result;
                googleApiResponse.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<GoogleMapLocationModel>(googleApiResponse.Content.ReadAsStringAsync().Result);
                return result.results.FirstOrDefault().geometry.location;
            }
        }
    }
}
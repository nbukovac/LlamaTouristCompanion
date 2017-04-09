using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using TestBotApplication.Services;

namespace TestBotApplication.Models
{
    [Serializable]
    public class LocationSelection
    {
        public string City;
        public string Address;

        public static IForm<LocationSelection> BuildForm()
        {
            return new FormBuilder<LocationSelection>()
                    .AddRemainingFields()
                    .Field(nameof(City), GetCityMessage(Thread.CurrentThread.CurrentUICulture))
                    .Field(nameof(Address), GetAddressMessage(Thread.CurrentThread.CurrentUICulture))
                    .Confirm("Verify data! Type yes for confirmation")
                    .Build();
        }


        private static string GetCityMessage(CultureInfo culture)
        {
            var googleTranslateService = new GoogleTranslateService();
            var context = new Model1();
            var selectedLanguage = context.Languages.FirstOrDefault(x => x.Code == culture.Name);
            var languageCode = selectedLanguage != null ? selectedLanguage.Code : "en";
            return googleTranslateService.TranslateToSelectedLanguage("Enter city", languageCode.Trim());
        }

        private static string GetAddressMessage(CultureInfo culture)
        {
            var googleTranslateService = new GoogleTranslateService();
            var context = new Model1();
            var selectedLanguage = context.Languages.FirstOrDefault(x => x.Code == culture.Name);
            var languageCode = selectedLanguage != null ? selectedLanguage.Code : "en";
            return googleTranslateService.TranslateToSelectedLanguage("Enter address", languageCode.Trim());
        }
    }
}
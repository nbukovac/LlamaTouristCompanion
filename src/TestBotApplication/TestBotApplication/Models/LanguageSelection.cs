using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TestBotApplication.Services;

namespace TestBotApplication.Models
{
    [Serializable]
    public class LanguageSelection
    {
        public LanguageOptions Language;
        public string City;
        public string Address;
        public static IForm<LanguageSelection> BuildForm()
        {
            return new FormBuilder<LanguageSelection>()
                .Message("Welcome to Apartmanko! :)")
                .Field(nameof(Language), "Select your language {||}")
                .Field(nameof(City), "Enter a {&} of an apartment you are staying in")
                .Field(nameof(Address), "Enter a {&} of a apartment you are staying in")
                    .OnCompletion(async (context, profileForm) =>
                    {
                        context.PrivateConversationData.SetValue<LanguageOptions>(
                       "Language", (LanguageOptions)profileForm.Language);

                        context.PrivateConversationData.SetValue<string>(
                      "City", (string)profileForm.City);

                        context.PrivateConversationData.SetValue<string>(
                      "Address", (string)profileForm.Address);

                        await context.PostAsync(GetProfileCOnfirmationMessage(profileForm));

                    })
                    .AddRemainingFields()
                    .Confirm("Hide validation", state => false)
                    .Build();
        }

        private static string GetCityMessage(LanguageSelection profile)
        {
            var googleTranslateService = new GoogleTranslateService();
            var context = new Model1();
            var selectedLanguage = context.Languages.FirstOrDefault(x => x.Id == (int)profile.Language);
            var languageCode = selectedLanguage != null ? selectedLanguage.Code : "en";
            return googleTranslateService.TranslateToSelectedLanguage("Enter city", languageCode.Trim());
        }

        private static string GetAddressMessage(LanguageSelection profile)
        {
            var googleTranslateService = new GoogleTranslateService();
            var context = new Model1();
            var selectedLanguage = context.Languages.FirstOrDefault(x => x.Id == (int)profile.Language);
            var languageCode = selectedLanguage != null ? selectedLanguage.Code : "en";
            return googleTranslateService.TranslateToSelectedLanguage("Enter address", languageCode.Trim());
        }

        private static string GetProfileCOnfirmationMessage(LanguageSelection profile)
        {
            var googleTranslateService = new GoogleTranslateService();
            var context = new Model1();
            var selectedLanguage = context.Languages.FirstOrDefault(x => x.Id == (int)profile.Language);
            var languageCode = selectedLanguage != null ? selectedLanguage.Code : "en";
            return googleTranslateService.TranslateToSelectedLanguage("Your's profile is set. You can ask question on your own language", languageCode.Trim());
        }

    }
    public enum LanguageOptions
    {
        Hrvatski = 1,
        Engleski = 2,
        Njemački = 3
    };
}
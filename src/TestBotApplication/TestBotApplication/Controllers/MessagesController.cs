using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using TestBotApplication.Services;
using Microsoft.Bot.Builder.FormFlow;
using System.Collections.Generic;
using TestBotApplication.Models;
using Microsoft.Bot.Builder.Dialogs;
using System.Globalization;
using System.Threading;

namespace TestBotApplication
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        LanguageSelection informationSelection = new LanguageSelection();

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {

            if (activity.Type == ActivityTypes.Message)
            {
                var dbContext = new Model1();

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                StateClient sc = activity.GetStateClient();
                BotData userData = sc.BotState.GetPrivateConversationData(activity.ChannelId, activity.Conversation.Id, activity.From.Id);
                var userSelectedLanguage = userData.GetProperty<string>("Language");
                var userSelectedCity = userData.GetProperty<string>("City");
                if (string.IsNullOrEmpty(userSelectedLanguage))
                {
                    await Conversation.SendAsync(activity, MakeRootDialog);
                }
                else if (activity.Text == "/deleteprofile")
                {
                    activity.GetStateClient().BotState.DeleteStateForUser(activity.ChannelId, activity.From.Id);
                }
                else
                {
                    var userCity = userData.GetProperty<string>("City");
                    var userAddress = userData.GetProperty<string>("Address");
                    var googleApi = new GoogleTranslateService();
                    var location = googleApi.GetLocationFromAddress(userAddress + "," + userCity);



                    var tokenizedText = googleApi.SentanceDetect(GetTextInEng(activity.Text, userSelectedLanguage));
                    Activity reply = activity.CreateReply($"{tokenizedText}");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                // calculate something for us to return


                //var googleTranslateService = new GoogleTranslateService();
                //var translatedToEngString = googleTranslateService.TranslateToEnglish(activity.Text, selectedLanguage);
                //// return our reply to the user

                //Activity reply1 = activity.CreateReply($"{translatedToEngString}");

                //// Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
                //

            }
            else
            {
                HandleSystemMessage(activity);
            }

            return new HttpResponseMessage();
        }

        internal static IDialog<LanguageSelection> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(LanguageSelection.BuildForm));
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        private string GetTextInEng(string text, string userSelectedLanguage)
        {
            var googleApi = new GoogleTranslateService();
            var languages = new LanguagesDict();
            var parsedLanguageTag = Convert.ToInt32(userSelectedLanguage);
            var selectedLanguage = languages.LanguageCodes[parsedLanguageTag];
            string textinEn = text;
            if (selectedLanguage != "en")
                textinEn = googleApi.TranslateToEnglish(text, selectedLanguage);

            return textinEn;
        }

        private string GetTextInSelectedLanguage(string text,  string userSelectedLanguage)
        {
            var googleApi = new GoogleTranslateService();
            var languages = new LanguagesDict();
            var parsedLanguageTag = Convert.ToInt32(userSelectedLanguage);
            var selectedLanguage = languages.LanguageCodes[parsedLanguageTag];
            string textinEn = text;
            if (selectedLanguage != "en")
                textinEn = googleApi.TranslateToEnglish(text, selectedLanguage);

            return textinEn;
        }
    }
}
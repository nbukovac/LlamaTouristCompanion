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
               
                var guid = userData.GetProperty<string>("ApartmantId");
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
                    var administrationService = new AdministrationService();
                    var possibleLocations =  administrationService.GetPossibleLocations(location);
                    if (possibleLocations.FirstOrDefault(x => x.ApartmentId.ToString() == activity.Text) == null && string.IsNullOrEmpty(guid))
                    {
                        if (possibleLocations.Count > 0)
                        {
                            userData.SetProperty<string>("ApartmantId", "IdComming");
                            sc.BotState.SetPrivateConversationData(activity.ChannelId, activity.Conversation.Id, activity.From.Id, userData);

                            Activity replyToConversation = activity.CreateReply(GetTextInSelectedLanguage("Please select your place", userSelectedLanguage));
                            replyToConversation.Recipient = activity.From;
                            replyToConversation.Type = "message";
                            replyToConversation.Attachments = new List<Attachment>();

                            List<CardAction> cardButtons = new List<CardAction>();
                            foreach (var button in possibleLocations)
                            {
                                CardAction plButton = new CardAction()
                                {
                                    Value = button.ApartmentId.ToString(),
                                    Type = "imBack",
                                    Title = button.Name
                                };
                                cardButtons.Add(plButton);
                            }

                            HeroCard plCard = new HeroCard()
                            {
                                Title = GetTextInSelectedLanguage("Press the button with your accomodation", userSelectedLanguage),
                                Buttons = cardButtons
                            };
                            Attachment plAttachment = plCard.ToAttachment();
                            replyToConversation.Attachments.Add(plAttachment);
                            await connector.Conversations.SendToConversationAsync(replyToConversation);

                           
                            await sc.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);                      
                        }
                        else
                        {
                            var responseText = GetTextInSelectedLanguage("Sorry, havn't found anything. Try again", userSelectedLanguage);
                            Activity replyToSelectedApartmen = activity.CreateReply($"{responseText}");
                            await connector.Conversations.ReplyToActivityAsync(replyToSelectedApartmen);
                        }
                    }
                    else if (guid == "IdComming")
                    {
                        userData.SetProperty<string>("ApartmantId", activity.Text);
                        guid = activity.Text;
                        sc.BotState.SetPrivateConversationData(activity.ChannelId, activity.Conversation.Id, activity.From.Id, userData);
                        //    await sc.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
                        var responseText = GetTextInSelectedLanguage("Go ahed and ask me something", userSelectedLanguage);
                        Activity replyToSelectedApartmen = activity.CreateReply($"{responseText}");
                        await connector.Conversations.ReplyToActivityAsync(replyToSelectedApartmen);
                    }
                    else
                    {
                        var tokenizedText = googleApi.SentanceDetect(GetTextInEng(activity.Text, userSelectedLanguage));
                        var resText = administrationService.GetResponeseToQuery(tokenizedText, guid);
                        if (resText != null)
                        {
                            Activity reply = activity.CreateReply($"{resText.Answer}");
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                        else
                        {
                            var responseText = GetTextInSelectedLanguage("Sorry, have not found anything. Try again", userSelectedLanguage);
                            Activity reply = activity.CreateReply($"{responseText}");
                            await connector.Conversations.ReplyToActivityAsync(reply);
                        }
                            
                    }
                   
                }
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

        private string GetTextInSelectedLanguage(string text, string userSelectedLanguage)
        {
            var googleApi = new GoogleTranslateService();
            var languages = new LanguagesDict();
            var parsedLanguageTag = Convert.ToInt32(userSelectedLanguage);
            var selectedLanguage = languages.LanguageCodes[parsedLanguageTag];
            string textinEn = text;
            if (selectedLanguage != "en")
                textinEn = googleApi.TranslateToSelectedLanguage(text, selectedLanguage);

            return textinEn;
        }
    }
}
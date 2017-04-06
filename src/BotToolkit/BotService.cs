using BotToolkit;
using BotToolkit.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit
{
    [Serializable]
    public class BotService
    {
        public Bot Bot { get; private set; }

        public IAuthenticator Authenticator { get; set; }

        /// <summary>
        /// When true, will always send the typing indicator prior to the bot responding
        /// </summary>
        public bool SendTypingIndicator { get; set; }

        public BotService(Bot bot)
        {
            this.Bot = bot;
            this.SendTypingIndicator = true;
        }

        public async Task RespondAsync(IMessageActivity activity)
        {
            if (activity.Type == ActivityTypes.Message
                && this.SendTypingIndicator)
            {
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                var reply = (activity as Activity).CreateReply();
                reply.Type = ActivityTypes.Typing;

                await connector.Conversations.ReplyToActivityAsync(reply);
            }

            await Conversation.SendAsync(activity, () => Chain.From(() => new MenuDialog(this.Bot.RootMenu, this.Authenticator)));
        }
    }
}

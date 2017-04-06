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

        public BotService(Bot bot)
        {
            this.Bot = bot;
        }

        public Task RespondAsync(IMessageActivity activity)
        {
            return Conversation.SendAsync(activity, () => Chain.From(() => new MenuDialog(this.Bot.RootMenu, this.Authenticator)));
        }
    }
}

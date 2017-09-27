using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    [Serializable]
    public class Bot
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public IEnumerable<Command> Commands { get; set; }

        public Menu RootMenu { get; set; }

        public Task GoHome(IDialogContext context, IAwaitable<object> result = null)
        {
            context.Call(new MenuDialog(this.RootMenu, null), GoHome);

            return Task.FromResult(0);
        }

        public virtual Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> message)
        {
            return Task.FromResult(0);
        }
    }
}

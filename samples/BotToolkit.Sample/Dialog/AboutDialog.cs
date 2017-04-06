using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace BotToolkit.Sample.Dialog
{
    [Serializable]
    public class AboutDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("This is the about dialog");

            context.Wait(MessageReceivedAsync);            
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var msgText = (await result).Text;

            context.Done<object>(null);
        }
    }
}
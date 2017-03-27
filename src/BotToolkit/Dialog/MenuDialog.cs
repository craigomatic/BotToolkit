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
    public class MenuDialog : IDialog
    {
        public Menu Menu { get; private set; }
        
        public MenuDialog(Menu menu)
        {
            this.Menu = menu;
        }

        public Task StartAsync(IDialogContext context)
        {
            var choices = this.Menu.MenuItems.Select(m => m.Label);
            PromptDialog.Choice<string>(context, _SelectionMade, choices, this.Menu.Prompt);

            return Task.Delay(0);
        }

        private async Task _SelectionMade(IDialogContext context, IAwaitable<string> result)
        {
            var selection = await result;

            _HandleCommand(context, selection);
        }

        private void _HandleCommand(IDialogContext context, string selection)
        {
            //does this map to existing menu or global command?

            //favour local menu options if conflicts exist
            var foundMenuItem = this.Menu.MenuItems.Where(m => m.Label == selection).FirstOrDefault();


            if (foundMenuItem != null)
            {
                context.PrivateConversationData.SetValue<MenuItem>("CurrentMenuItem", foundMenuItem);
                foundMenuItem.Action.Display(context);
            }

            //TODO: look for command
        }

        private async Task _ChildDialogCompleted(IDialogContext context, IAwaitable<object> result)
        {
            var param = await result;

            var menuItem = context.PrivateConversationData.Get<MenuItem>("CurrentMenuItem");
            //menuItem.Action.Complete(result);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var command = await result;

            _HandleCommand(context, command.Text);
        }
    }
}

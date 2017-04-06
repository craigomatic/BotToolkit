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

        public IAuthenticator Authenticator { get; private set; }

        public MenuDialog(Menu menu, IAuthenticator authenticator)
        {
            this.Menu = menu;
            this.Authenticator = authenticator;
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (this.Authenticator != null &&
                (await this.Authenticator.CheckAuthAsync(context) == AuthenticationStatus.NotAuthenticated))
            {
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                var choices = this.Menu.MenuItems.Select(m => m.Label);
                PromptDialog.Choice<string>(context, _SelectionMade, choices, this.Menu.Prompt);
            }
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
                context.PrivateConversationData.SetValue("CurrentMenuItem", foundMenuItem.Label);
                foundMenuItem.Action.Display(context);
            }

            //TODO: look for command
        }

        private async Task _ChildDialogCompleted(IDialogContext context, IAwaitable<object> result)
        {
            var param = await result;

            var menuItemLabel = context.PrivateConversationData.Get<string>("CurrentMenuItem");
            var foundMenuItem = this.Menu.MenuItems.Where(m => m.Label == menuItemLabel).FirstOrDefault();
            //foundMenuItem.Action.Complete(result);
        }

        protected virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var command = await result;

            //test for authentication before proceeding
            if (this.Authenticator != null &&
                (await this.Authenticator.CheckAuthAsync(context) == AuthenticationStatus.NotAuthenticated))
            {
                await this.Authenticator.RequestAuth(context, command);

            }
            else
            {
                _HandleCommand(context, command.Text);
            }
        }        
    }
}

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using BotToolkit.Model;
using BotToolkit.Actions;
using BotToolkit.Sample.Dialog;
using Microsoft.Bot.Builder.Dialogs;

namespace BotToolkit.Sample
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private static BotService _BotService;

        static MessagesController()
        {
            var bot = new Bot();

            //global commands
            bot.Commands = new[] { new Command { Label = "help", Action = new DialogMenuAction<object>(new HelpDialog(), _MenuFinished) } };
            
            //menu structure
            bot.RootMenu = new Menu
            {
                Prompt = "Hi there, this is the main menu",
                MenuItems = new[] 
                {
                    new MenuItem { Label = "about", Action = new DialogMenuAction<object>(new AboutDialog(), _MenuFinished) },
                    new MenuItem { Label = "help", Action = new DialogMenuAction<object>(new HelpDialog(), _MenuFinished) }
                }
            };

            _BotService = new BotService(bot);
        }

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await _BotService.RespondAsync(activity);
            }
            
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private static Task _MenuFinished(IDialogContext context, IAwaitable<object> result)
        {
            return Task.Delay(0);
        }
    }
}
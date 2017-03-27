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
    public static class ConversationExtensions
    {
        public static IMessageActivity ToThumbnailCarousel(this IDialogContext context, IEnumerable<ICarouselItem> items)
        {
            var msg = context.MakeMessage();

            foreach (var item in items)
            {               
                msg.Attachments.Add(new ThumbnailCard
                {
                    Title = item.Title,
                    Subtitle = item.Subtitle,
                    Text = item.Text,
                    Buttons = new List<CardAction> { new CardAction(item.ActionType, item.ActionTitle, null, item.ActionValue) }
                }.ToAttachment());
            }

            return msg;
        }
    }
}

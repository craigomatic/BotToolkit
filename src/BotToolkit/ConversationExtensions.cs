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
        public static IMessageActivity ToThumbnailCarousel(this IDialogContext context, IEnumerable<ICarouselItem> items, string messageText = null)
        {
            var msg = _BuildMessage(context, items, messageText);
            msg.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            
            return msg;
        }

        public static IMessageActivity ToThumbnailList(this IDialogContext context, IEnumerable<ICarouselItem> items, string messageText = null)
        {
            var msg = _BuildMessage(context, items, messageText);
            msg.AttachmentLayout = AttachmentLayoutTypes.List;
            return msg;
        }

        private static IMessageActivity _BuildMessage(IDialogContext context, IEnumerable<ICarouselItem> items, string messageText)
        {
            var msg = context.MakeMessage();
            msg.Text = messageText;

            foreach (var item in items)
            {
                msg.Attachments.Add(new ThumbnailCard
                {
                    Title = item.Title,
                    Images = new List<CardImage> { new CardImage { Url = item.CardImage } },
                    Subtitle = item.Subtitle,
                    Text = item.Text,
                    Buttons = new List<CardAction> { new CardAction(item.ActionType, item.ActionTitle, null, item.ActionValue) }
                }.ToAttachment());
            }

            return msg;
        }
    }
}

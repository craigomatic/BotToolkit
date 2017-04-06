using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    public interface ICarouselItem
    {
        string Title { get; set; }

        string Subtitle { get; set; }

        string ActionType { get; set; }

        string ActionTitle { get; set; }

        string Text { get; set; }

        object ActionValue { get; set; }
    }
}

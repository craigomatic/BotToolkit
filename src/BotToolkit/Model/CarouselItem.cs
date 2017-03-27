using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    public class CarouselItem : ICarouselItem
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ActionType { get; set; }

        public string ActionTitle { get; set; }

        public object ActionValue { get; set; }

        public string Text { get; set; }
    }
}

using BotToolkit.Actions;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    [Serializable]
    public class MenuItem
    {
        public IMenuAction Action { get; set; }

        public string Label { get; set; }        
    }
}

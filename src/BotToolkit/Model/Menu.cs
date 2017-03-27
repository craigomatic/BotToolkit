using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    [Serializable]
    public class Menu
    {
        public string Name { get; set; }

        /// <summary>
        /// The string to display when prompting for selection of the menu items
        /// </summary>
        public string Prompt { get; set; }
        
        public IEnumerable<MenuItem> MenuItems { get; set; }
        
        public IEnumerable<Command> GlobalCommands { get; set; }
    }
}

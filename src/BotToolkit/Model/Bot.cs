using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    [Serializable]
    public class Bot
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public IEnumerable<Command> Commands { get; set; }

        public Menu RootMenu { get; set; }
    }
}

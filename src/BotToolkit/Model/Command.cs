using BotToolkit.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    [Serializable]
    public class Command
    {
        /// <summary>
        /// The text that activates the command
        /// </summary>
        public string Label { get; set; }

        public IMenuAction Action { get; set; }
    }
}

using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Actions
{
    public interface IMenuAction
    {
        Task Display(IDialogContext context);
    }
}

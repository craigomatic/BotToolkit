using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Actions
{
    [Serializable]
    public class DialogMenuAction<T> : IMenuAction
    {
        public IDialog<T> Dialog { get; private set; }

        public ResumeAfter<T> ResumeHandler { get; private set; }

        public DialogMenuAction(IDialog<T> dialog, ResumeAfter<T> resumeHandler)
        {
            this.Dialog = dialog;
            this.ResumeHandler = resumeHandler;
        }
        
        public Task Display(IDialogContext context)
        {
            context.Call<T>(this.Dialog, this.ResumeHandler);
            return Task.Delay(0);
        }
    }
}

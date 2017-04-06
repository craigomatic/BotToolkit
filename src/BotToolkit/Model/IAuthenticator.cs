using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace BotToolkit.Model
{
    /// <summary>
    /// Implementations of this interface can be used to enforce authentication at the bot layer.
    /// </summary>
    public interface IAuthenticator
    {
        Task<AuthenticationStatus> CheckAuthAsync(IDialogContext context);

        Task RequestAuth(IDialogContext context, IMessageActivity activity);
    }    
}
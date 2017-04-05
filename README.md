# BotToolkit
Toolkit that makes life easier when working with the Bot Framework in C#

## Usage

The toolkit provides a BotService that simplifies the way you wire up a bot that presents the users with one or more menus.

In MessagesController.cs:

```csharp
[BotAuthentication]
public class MessagesController : ApiController
{
	private static BotService _BotService;

	static MessagesController()
	{
		var bot = new Bot();
		
		bot.RootMenu = new Menu
		{
			Prompt = "Hi there, this is the main menu",
			MenuItems = new[] 
			{
				new MenuItem { Label = "about", Action = new DialogMenuAction<object>(new AboutDialog(), _MenuFinished) },
				new MenuItem { Label = "help", Action = new DialogMenuAction<object>(new HelpDialog(), _MenuFinished) }
			}
		};

		_BotService = new BotService(bot);
	}

	public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
	{
		if (activity.Type == ActivityTypes.Message)
		{
			await _BotService.RespondAsync(activity);
		}
		
		var response = Request.CreateResponse(HttpStatusCode.OK);
		return response;
	}

	private static Task _MenuFinished(IDialogContext context, IAwaitable<object> result)
	{
		return Task.Delay(0);
	}
}
```
This will render a menu when the bot receives a message.

## Authentication

If your bot requires the user to login, you'll most likely want to make use of the excellent [AuthBot](https://github.com/MicrosoftDX/AuthBot) project. 

The BotService has an Authenticator property that you can use to integrate with AuthBot (or some other authentication mechanism of your choosing): 

```csharp
//Implementation of IAuthenticator
[Serializable]
public class AzureAuthenticator : IAuthenticator
{
	public async Task<AuthenticationStatus> CheckAuthAsync(IDialogContext context)
	{
		var hasToken = !string.IsNullOrEmpty(await context.GetAccessToken(AuthSettings.Scopes));

		return hasToken ?
			AuthenticationStatus.Authenticated :
			AuthenticationStatus.NotAuthenticated;
	}

	public Task RequestAuth(IDialogContext context, IMessageActivity activity)
	{
		return context.Forward(new AzureAuthDialog(AuthSettings.Scopes), _AuthComplete, activity, CancellationToken.None);
	}

	private async Task _AuthComplete(IDialogContext context, IAwaitable<object> result)
	{
		var auth = await result;
		context.Done<object>(null);
	}
}
```

```csharp
//In MessagesController.cs
_BotService = new BotService(bot);
_BotService.Authenticator = new AzureAuthenticator();
```

The BotService will then enforce authentication prior to displaying the menu.

## Helpers

Let's say you've stored an object into the IBotDataBag, ie: PrivateConversationData and you want to progressively update it. 

This doesn't work:

```csharp
var myObj = context.PrivateConversationData.Get<MyObject>("TheKey");
myObj.SomeProperty = "new value";
```

...as the object needs to be saved again to the IBotDataBag.

Instead, use the DataHelper like this to take care of the save once it leaves the using block:

```csharp
using(var myObj = new DataHelper<MyObject>(context.PrivateConversationData, "TheKey"))
{
    myObj.Value.SomeProperty = "new value";
}
```

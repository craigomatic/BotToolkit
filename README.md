# BotToolkit
Toolkit that makes life easier when working with the Bot Framework in C#

## Usage

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

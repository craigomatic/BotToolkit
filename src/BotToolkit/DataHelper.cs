using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotToolkit
{
    public class DataHelper<T> : IDisposable
    {
        public IBotDataBag BotDataBag { get; private set; }

        public string Key { get; private set; }

        public T Value
        {
            get { return this.BotDataBag.Get<T>(this.Key); }
        }

        public DataHelper(IBotDataBag botDataBag, string key)
        {
            this.BotDataBag = botDataBag;
            this.Key = key;
        }        

        public void Dispose()
        {
            //save the context
            this.BotDataBag.SetValue<T>(this.Key, this.Value);
        }
    }
}
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

        private T _Value;

        public T Value
        {
            get
            {
                if (_Value == null)
                {
                    _Value = this.BotDataBag.Get<T>(this.Key);
                }

                return _Value;
            }
        }

        public DataHelper(IBotDataBag botDataBag, string key)
        {
            this.BotDataBag = botDataBag;
            this.Key = key;
        }        

        public void Dispose()
        {
            //save the context
            this.BotDataBag.SetValue<T>(this.Key, _Value);
            _Value = default(T);
        }
    }
}
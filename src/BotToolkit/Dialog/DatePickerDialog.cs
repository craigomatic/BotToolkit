using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotToolkit.Dialog
{
    [Serializable]
    public class DatePickerDialog : IDialog<DateTime>
    {
        public string YearPrompt { get; set; }

        public string MonthPrompt { get; set; }

        public string DayOfMonthPrompt { get; set; }

        public string TimePrompt { get; set; }

        public DatePickerMode Mode { get; set; }

        private readonly string _ConversationDataKey = "__Date";

        public DatePickerDialog()
        {
            this.YearPrompt = "Ok, can I please have the year?";
            this.MonthPrompt = "And the month?";
            this.DayOfMonthPrompt = "And which day of the month?";

            this.TimePrompt = "Which time (hh:mm:ss)?";

            this.Mode = DatePickerMode.Date;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.PrivateConversationData.SetValue<DateTime>(_ConversationDataKey, DateTime.MinValue);

            if (this.Mode == DatePickerMode.Time)
            {
                PromptDialog.Text(context, _TimeReceived, this.TimePrompt);
            }
            else
            {
                PromptDialog.Number(context, _YearReceived, this.YearPrompt);
            }

            return Task.Delay(0);
        }

        private async Task _TimeReceived(IDialogContext context, IAwaitable<string> result)
        {
            var time = await result;
            var timeSpan = TimeSpan.Zero;

            if (TimeSpan.TryParse(time, out timeSpan))
            {
                var date = context.PrivateConversationData.Get<DateTime>(_ConversationDataKey);
                context.PrivateConversationData.RemoveValue(_ConversationDataKey);
                context.Done(new DateTime(date.Year, date.Month, date.Day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
            }
            else
            {
                //problem, need to retry
                
            }
        }

        private async Task _YearReceived(IDialogContext context, IAwaitable<long> result)
        {
            var year = await result;

            context.PrivateConversationData.SetValue<DateTime>(_ConversationDataKey, new DateTime((int)year, 1, 1));

            var monthInts = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dev" };
            PromptDialog.Choice(context, _MonthReceived, monthInts, this.MonthPrompt, null, 3, PromptStyle.Auto, months);
        }

        private async Task _MonthReceived(IDialogContext context, IAwaitable<int> result)
        {
            var month = await result;

            var date = context.PrivateConversationData.Get<DateTime>(_ConversationDataKey);
            context.PrivateConversationData.SetValue<DateTime>(_ConversationDataKey, new DateTime(date.Year, month, 1));

            PromptDialog.Number(context, _DayReceived, this.DayOfMonthPrompt);
        }

        private async Task _DayReceived(IDialogContext context, IAwaitable<long> result)
        {
            var dayOfMonth = await result;

            var date = context.PrivateConversationData.Get<DateTime>(_ConversationDataKey);

            if (this.Mode == DatePickerMode.Date)
            {
                context.PrivateConversationData.RemoveValue(_ConversationDataKey);
                context.Done<DateTime>(new DateTime(date.Year, date.Month, (int)dayOfMonth));
            }
            else
            {
                context.PrivateConversationData.SetValue<DateTime>(_ConversationDataKey, new DateTime(date.Year, date.Month, (int)dayOfMonth));

                PromptDialog.Text(context, _TimeReceived, this.TimePrompt);
            }
        }
    }
}

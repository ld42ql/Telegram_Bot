using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace Test_Telegram_Bot
{
    class ModeClass : UserSet
    {
        private static string messageText = String.Empty;
        private static string AnswerBots = String.Empty;

        public ModeClass(long messageFromId, string messageText) : base(messageFromId, messageText)
        {
        }
        
    }
}

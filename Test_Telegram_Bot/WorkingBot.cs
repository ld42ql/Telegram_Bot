using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace Test_Telegram_Bot
{

    class WorkingBot : UserSet
    {
        public static Telegram.Bot.TelegramBotClient Bot;
        private static long update_id;
        private static string messageText = String.Empty;
        private static string AnswerBots;
        static ReplyKeyboardMarkup keyboardMarkup;

        public WorkingBot(long messageFromId, string messageText) : base(messageFromId, messageText)
        {
        }

        /// <summary>
        /// Начала работы бота
        /// </summary>
        public static void StartBot()
        {
            Bot = new Telegram.Bot.TelegramBotClient("451203706:AAEXY_QgsHxwwxyqu3t92Lybv0RM2LkirlY");
            Bot.OnMessage += BotOnMessageReceived;
            Bot.StartReceiving(new UpdateType[] { UpdateType.MessageUpdate });
        }

        /// <summary>
        /// Работа бота
        /// </summary>
        public static void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            update_id = message.Chat.Id;
            messageText = message.Text;

            SelectMode(messageText);
        }

        /// <summary>
        /// Выбор и отправка сообщения
        /// </summary>
        /// <param name="msgTxt"></param>
        static async void SelectMode(string msgTxt)
        {
            AnswerBots = String.Empty;

            switch (msgTxt)
            {
                case "/test":
                    await Bot.SendTextMessageAsync(update_id, "ТЕСТ!!!!", replyMarkup: keyboardMarkup);
                    break;
                default:
                    DefaultModeBot user = new DefaultModeBot(update_id, messageText);
                    user.AnswerDefaultBotTest();
                    break;
            }
        }
    }
}
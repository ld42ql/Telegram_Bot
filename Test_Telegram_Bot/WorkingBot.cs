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
        private static Telegram.Bot.TelegramBotClient Bot;
        private static long update_id;
        private static string messageText = String.Empty;
        private static ReplyKeyboardMarkup keyboard;

        public WorkingBot(long messageFromId, string messageText) : base(messageFromId, messageText)
        {
        }

        private string answerBots = String.Empty;

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
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            update_id = message.Chat.Id;
            messageText = message.Text;

            UsersGreateKeyboard();

            WorkingBot user = new WorkingBot(update_id, messageText);
            try
            {
                await Bot.SendTextMessageAsync(update_id, user.AnswerBots, replyMarkup: keyboard);
                Console.WriteLine($"\n****************\n{update_id} {messageText}" +
                    $" {DateTime.Now}\n****************\n");
            }
            catch (Exception e)
            {
            }
        }
        /// <summary>
        /// Ответ бота
        /// </summary>
        public string AnswerBots
        {
            get
            {
                SelectOperations();
                return answerBots;
            }
        }

        /// <summary>
        /// Формируем ответ
        /// </summary>
        /// <param name="operations">Для какого метода</param>
        string AnswerMessage(PossibleOperations operations)
        {
            string str = $"{operations()}";
            return str;
        }

        /// <summary>
        /// Выбор ответа
        /// </summary>
        void SelectOperations()
        {
            if (!Char.IsLetter(MessageText, 0))
            {
                switch (MessageText.Substring(1, MessageText.Length - 1))
                {
                    case "travel":
                    case "Путешествие":
                        answerBots = AnswerMessage(operations: Operations.EventRandom); break;
                    case "dice":
                    case "Бросок кубика":
                        answerBots = AnswerMessage(operations: Operations.ResultDice); break;
                    default: answerBots = AnswerMessage(operations: Operations.ErrorAnswer); break;
                }
            }
        }

        /// <summary>
        /// Создание клавиатуры для пользователя
        /// </summary>
        static void UsersGreateKeyboard()
        {
            keyboard = new ReplyKeyboardMarkup(new[]
                               {
                    new [] // first row
                    {
                        new KeyboardButton("*Путешествие"),
                        new KeyboardButton("*Бросок кубика"),
                    },
                }, true, false);
        }
    }
}
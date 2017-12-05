using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Test_Telegram_Bot
{
    delegate ReplyKeyboardMarkup UserKeyboardInput();

    class DefaultModeBot : ModeClass
    {
        public DefaultModeBot(long messageFromId, string messageText) : base(messageFromId, messageText)
        {
        }

        private string answerBots = String.Empty;

        /// <summary>
        /// Ответ бота
        /// </summary>
        public string DefaultAnswerBots
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
       static ReplyKeyboardMarkup UsersDefaultKeyboard()
        {
             ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                               {
                    new [] // first row
                    {
                        new KeyboardButton("*Путешествие"),
                        new KeyboardButton("*Бросок кубика"),
                    },
                }, true, false);

            return keyboard;
        }

        /// <summary>
        /// Отправка ответа в телеграмм
        /// </summary>
        public async void AnswerDefaultBotTest()
        {
            await WorkingBot.Bot.SendTextMessageAsync(MessageFromId, DefaultAnswerBots,
                replyMarkup: UsersDefaultKeyboard());
        }
    }
}

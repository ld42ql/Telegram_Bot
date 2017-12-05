using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Telegram_Bot
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    class UserSet
    {
        public static Telegram.Bot.TelegramBotClient Bot;

        /// <summary>
        /// От кого сообщения
        /// </summary>
        public string MessageText { get => messageText; }
        private long messageFromId = 0;

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public long MessageFromId { get => messageFromId; }

        private string messageText = String.Empty;

        /// <summary>
        /// Объект с данными от пользователя
        /// </summary>
        /// <param name="messageFromId">От кого сообщение</param>
        /// <param name="messageText">Текст сообщения</param>
        public UserSet(long messageFromId, string messageText)
        {
            this.messageFromId = messageFromId;
            this.messageText = messageText;
        }


    }
}

using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace Telegram.Bot
{
    public class TelegramBot
    {

        /// <summary>
        /// Is bot successfully authorized
        /// </summary>
        public bool Authorized { get; private set; }

        /// <summary>
        /// Is bot working
        /// </summary>
        public bool Enable { get; private set; }

        //------------------------------------
        private bool Buzy;
        private bool OneTimeKeyboard;
        private TelegramBotClient TgApi;
        private Action<long, string> Handler;
        //------------------------------------

        /// <summary>
        /// Authorization in the Telegram group
        /// </summary>
        /// <param name="Token">Telegram group api token</param>
        /// <returns></returns>
        public bool Authorization(string Token)
        {
            TgApi = new TelegramBotClient(Token);
            TgApi.StartReceiving(Receiving, Errors);
            try
            {
                TgApi.GetMeAsync().Wait();
                return Authorized = true;
            }
            catch
            {
                return Authorized = false;
            }
        }

        /// <summary>
        /// Start bot handler
        /// </summary>
        /// <param name="Handler">Function to which received messages will be sent</param>
        public void Start(Action<long, string> Handler)
        {
            if (Enable) throw new Exception("The current bot instance is already running !"); ;
            if (!Authorized) throw new Exception("The current bot instance is not authorized !");
            this.Handler = Handler;
            Enable = true;
        }

        /// <summary>
        /// Stop bot handler
        /// </summary>
        public void Stop()
        {
            Enable = false;
        }

        /// <summary>
        /// Send Message for user by id
        /// </summary>
        /// <param name="Id">User id for send message</param>
        /// <param name="Text">Text for Message</param>
        /// <param name="Keyboard">Regular expression generating keyboard</param>
        /// <param name="Inline">Set Inline keyboard</param>
        /// <param name="OneTime">Keyboard open only for one message</param>
        /// <param name="Large">Large keyboard display view</param>
        public void Send(long id, string text, string Keyboard = null, bool Inline = false, bool OneTime = false, bool Large = false)
        {
            while (Buzy);Buzy = true;
            IReplyMarkup reply = OneTimeKeyboard ? new ReplyKeyboardRemove() : null;OneTimeKeyboard = false;
            if (Keyboard != null)
            {
                InlineKeyboardButton[][] InlineKeyboardBuilder = new InlineKeyboardButton[Keyboard.Split(ks.a).Length][];
                InlineKeyboardMarkup InlineKeyboardMarkup = new InlineKeyboardMarkup(InlineKeyboardBuilder);
                KeyboardButton[][] KeyboardBuilder = new KeyboardButton[Keyboard.Split(ks.a).Length][];
                ReplyKeyboardMarkup KeyboardMarkup = new ReplyKeyboardMarkup(KeyboardBuilder);
                KeyboardMarkup.ResizeKeyboard = !Large;
                OneTimeKeyboard = OneTime;

                int i = 0;
                foreach (string ButtonLine in Keyboard.Split(ks.a))
                {
                    InlineKeyboardBuilder[i] = new InlineKeyboardButton[ButtonLine.Split(ks.b).Length];
                    KeyboardBuilder[i] = new KeyboardButton[ButtonLine.Split(ks.b).Length];

                    int j = 0;
                    foreach (string Button in ButtonLine.Split(ks.b))
                    {
                        string ButtonText = Button.Split(ks.c)[0];
                        InlineKeyboardBuilder[i][j] = new InlineKeyboardButton(ButtonText);
                        InlineKeyboardBuilder[i][j].CallbackData = ButtonText;

                        if (Button.Split(ks.c).Length > 1)
                        {
                            switch (Button.Split(ks.c)[1])
                            {
                                case "CONTACT":
                                    KeyboardBuilder[i][j] = KeyboardButton.WithRequestContact(ButtonText);
                                    break;
                                case "LOCATION":
                                    KeyboardBuilder[i][j] = KeyboardButton.WithRequestLocation(ButtonText);
                                    break;
                                case "POLL":
                                    KeyboardBuilder[i][j] = KeyboardButton.WithRequestPoll(ButtonText);
                                    break;
                                default:
                                    KeyboardBuilder[i][j] = new KeyboardButton(ButtonText);
                                    break;
                            }
                        }
                        else
                            KeyboardBuilder[i][j] = new KeyboardButton(ButtonText);

                        j++;
                    }
                    i++;
                }
                reply = Inline ? InlineKeyboardMarkup : KeyboardMarkup;
            }
            TgApi.SendTextMessageAsync(chatId: id, text: text,replyMarkup: reply);
            Buzy = false;
        }

        /// <summary>
        /// Symbols used to create a keyboard
        /// </summary>
        /// <param name="a">Button lines splitter</param>
        /// <param name="b">Button columns splitter</param>
        /// <param name="c">Button parameter splitter</param>
        public void SetKeyboardSplitters(char a, char b, char c) { ks = (a, b, c); }
        private (char a, char b, char c) ks = (';', ',', '/');

        
        private Task Errors(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) => Task.CompletedTask; 
        private Task Receiving(ITelegramBotClient botClient, Update data, CancellationToken cancellationToken)
        {
            if (Enable)
            {
                if (data.Message != null)
                {
                    Handler(data.Message.From.Id, data.Message.Text);
                }
                if (data.CallbackQuery != null)
                {
                    Handler(data.CallbackQuery.From.Id, data.CallbackQuery.Data); 
                    TgApi.AnswerCallbackQueryAsync(data.CallbackQuery.Id);
                }
            }
            return Task.CompletedTask;
        }
    }
}
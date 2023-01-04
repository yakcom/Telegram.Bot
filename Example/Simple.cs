using Telegram.Bot;

namespace Example
{
    internal class Simple
    {
        static TelegramBot TgBot;
        static void Main(string[] args)
        {

            TgBot = new TelegramBot();
            TgBot.Authorization("YOUR_TG_GROUP_API_TOKEN");
            TgBot.Start(Handle);

            Console.Read();

        }

        static void Handle(long id,string text)
        {
            TgBot.Send(id, $"Hello Id: {id}\nYou Say: {text}");
            TgBot.Send(id, "Test Inline Keyboard", "Button11;Button21,Button22;Button31,Button32,Button33", true);
            TgBot.Send(id, "Test Outline Keyboard", "Contact/CONTACT;Location/LOCATION;Poll/POLL");
        }
    }
}
<a href="https://github.com/yakcom/Telegram.Bot/releases/">
<p align="center"><img  width="200" src="https://github.com/yakcom/Telegram.Bot/blob/master/.github/Tg.png"/></p>
<h1 align="center">Telegram.Bot</h1></a>
<h3 align="center">Library shell <a href="https://github.com/TelegramBots/Telegram.Bot" target="_blank">TgNet</a> for easy creation of group chat bots Telegram</h3><br>
<a href="https://www.nuget.org/packages/Telegrame.Bot"><img src="https://readme-typing-svg.herokuapp.com?font=Fira+Code&size=25&pause=100000&duration=3000&color=2EB3EC&center=true&vCenter=true&width=1000&lines=Download+NuGet+Release" alt="Typing SVG" /></a>

# Using
```c#
using Telegram.Bot;
```
# Quick Start
```c#
using Telegram.Bot;
void Main()
{
    TgBot = new TelegramBot();
    TgBot.Authorization("YOUR_TG_GROUP_API_TOKEN");
    TgBot.Start(Handle);
}
void Handle(long id, string text)
{
    TgBot.Send(id, "Hello World");
}
```
# Console Example
```C#
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
```
<br><br><br>

# Keyboard general
### Keyboard generated from string with 3 main separators:
> Symbol [ ; ] separates the vertical lines of the buttons
> 
> Symbol [ , ] separates buttons on a line
> 
> Symbol [ / ] separates the text of the button and its options
> 
## Example regular keyboard
```c#
TgBot.Send(id, "Example Test", "Button1Line1;Button1Line2,Button2Line2;Button1Line3,Button2Line3,Button3Line3");
```
<img src="https://github.com/yakcom/Telegram.Bot/blob/master/.github/Buttons.png"/><br><br><br>

# Keyboard button options

| Expression |     Button     |
| ---------- | -------------- |
|  /CONTACT  | Share Contact  |
|  /LOCATION | Share Location |
|  /POLL     | Create Poll    |

## Example keyboard with button options
```c#
TgBot.Send(id, "Test", "Share Contact/CONTACT;Share Location/LOCATION;Create Poll/POLL");
```
<img src="https://github.com/yakcom/Telegram.Bot/blob/master/.github/Buttons2.png"/><br><br><br>

# Additional functions arguments
### Send function has 2 additional arguments
> Inline - keyboard embedded in message

> OneTime - keyboard hides on next message

> Large - big keyboard buttons

## Example inline keyboard
```c#
TgBot.Send(id, "Example Test", "Button1Line1;Button1Line2,Button2Line2;Button1Line3,Button2Line3,Button3Line3",true);
```
<img src="https://github.com/yakcom/Telegram.Bot/blob/master/.github/ButtonsInline.png"/><br><br><br>

# Changing keyboard split characters
### Function ***SetKeyboardSplitters()*** allows you to specify your own characters as separators
## Example
```c#
TgBot.SetKeyboardSplitters(':', '.', '|');
TgBot.Send(id, "Test keyboard", "Button1|CONTACT.Button2|LOCATION:Button3|POLL");
```
<img src="https://github.com/yakcom/Telegram.Bot/blob/master/.github/Buttons3.png"/>

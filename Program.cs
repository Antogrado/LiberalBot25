using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

class Program
{
    
    private static readonly string BotToken = "8452279034:AAHGVXoFuoITOMDBFjBOfArCNS4-tEB1S5s";

    private static readonly string CorrectPhrase = "я согласен с тобой";

    static async Task Main()
    {
        var botClient = new TelegramBotClient(BotToken);

        var me = await botClient.GetMe();
        Console.WriteLine($"Бот запущен @{me.Username}");

        botClient.StartReceiving(UpdateHandler, ErrorHandler);

        Console.WriteLine("Нажмите Enter для выхода");
        Console.ReadLine();
    }

    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
            return;

        var message = update.Message;
        if (message.Text == null)
            return;

        string userMessage = message.Text.ToLower();

        if (userMessage.Contains(CorrectPhrase))
        {
            await botClient.SendMessage(message.Chat.Id, "Вы правы!", cancellationToken: cancellationToken);
        }
        else
        {
            await botClient.SendMessage(message.Chat.Id, "Вы не правы.", cancellationToken: cancellationToken);
        }
    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        return Task.CompletedTask;
    }
}
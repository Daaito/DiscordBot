using System; 
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

class Program
{
    
    private DiscordSocketClient? _client;
    private IConfigurationRoot? _config;

    static async Task Main(string[] args) => await new Program().RunBotAsync();

    public async Task RunBotAsync()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.MessageReceived += HandleCommandAsync;
        _client.MessageReceived += OnMessageRecived;

        string ?token = _config["DiscordToken"];    
        await _client.LoginAsync(TokenType.Bot, token);
        
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private Task Log(LogMessage arg)
    {
        Console.WriteLine(arg);
        return Task.CompletedTask;
    }

    private async Task HandleCommandAsync(SocketMessage message)
    {

        if(message.Author.IsBot) return;

        if(message.Content.ToLower() == "!Ping")
        {
            await message.Channel.SendMessageAsync("Pong!");
        }
        else
        {
            Console.WriteLine("HAT HALT NICHT FUNKTIONIERT");
        }
    }
    private async Task OnMessageRecived(SocketMessage message)
    {
        var author = message.Author.Username;  // Verfasser der Nachricht
        var messageType = message.GetType().Name;  // Typ der Nachricht (SocketMessage oder SocketUserMessage)
        var content = message.Content;        // Inhalt der Nachricht
        Console.WriteLine($"Verfasser: {author}, Typ: {messageType}, Inhalt: {content}");
        
    }
}   
using System; 
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Micosoft.Extensions.Configuration;

class Program
{
    
    private DiscordSocketClient _client;
    private IConfigurationRoot _config;

    static async Task Main(string[] args) => await new Programm().RunBotAsync();

    public async Task RunBotAsync()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("cappsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        _client = new DiscordSocketClient();
        _client.Log += Log;

        string token = _config["DiscordToken"];    
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        _client.NessageReceived += HandleCommandAsync;
        await Task.Delay(-1);
    }

    private Task Log(LogMessage arg)
    {
        Console.WriteLine(arg);
        return Task.CompletedTask;
    }

    private async Task HandleCommandAsync(SocketMessage arg)
    {
        if(massage.Author.IsBot) return;

        if(massage.Content.ToLower() == "!Ping")
        {
            await massage.Channel.SendMessageAsync("Pong!");
        }
    }
}   
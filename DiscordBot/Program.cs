using System;
using System.Text.Json;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus;
using DotNetEnv;

internal class Program
{
    // Load choices from JSON file
    public static readonly List<string> Choices = LoadChoicesFromJson();

    private static List<string> LoadChoicesFromJson()
    {
        try
        {
            string json = File.ReadAllText("choices.json");
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading choices from JSON: {ex.Message}");
            return new List<string> { "Default Option" };
        }
    }

    static async Task Main(string[] args)
    {
        // Load environment variables from .env file
        Env.Load(".env");

        // Get bot token from environment variable
        string? token = Environment.GetEnvironmentVariable("TOKEN");
        if (token == null)
        {
            //use dotnet run --project name
            Console.WriteLine("Error: TOKEN environment variable is not set.");
            return;
        }

        var discord = new DiscordClient(new DiscordConfiguration()
        {
            Token = token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        });

        var slashCommands = discord.UseSlashCommands();
        slashCommands.RegisterCommands<Choose>();

        await discord.ConnectAsync();
        await Task.Delay(-1);
    }
}

public class Choose : ApplicationCommandModule
{
    [SlashCommand("random_variant", "chooses_the_random_variant")]
    public async Task RandomChoice(InteractionContext ctx)
    {
        var random = new Random();
        var choice = Program.Choices[random.Next(Program.Choices.Count)];
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"variant: {choice}"));
    }
    [SlashCommand("randgroup", "Chooses a group of up to 5 unique variants to pick/ban from")]
    public async Task RandomChoiceGroup(InteractionContext ctx)
    {
        var random = new Random();
        var choices = new List<string>();
        var available = new List<string>(Program.Choices);

        while (choices.Count < 5 && available.Count > 0)
        {
            int idx = random.Next(available.Count);
            choices.Add(available[idx]);
            available.RemoveAt(idx);
        }

        var content = choices.Count > 0
            ? $"variants: {string.Join(", ", choices)}"
            : "No variants available.";

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
            new DiscordInteractionResponseBuilder().WithContent(content));
}
    }
}

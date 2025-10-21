using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus;

internal class Program
{
    // Constant list of strings for random choice
    public static readonly List<string> Choices = new List<string>
    {
        "Option 1",
        "Option 2",
        "Option 3",
        "Option 4",
        "Option 5"
    };

    static async Task Main(string[] args)
    {
        // Replace with your bot token
        string token = "MTQzMDI1MjYwMDQzMDk1NjcxOQ.GqS3mx.Rg5SKjezjdM3iR_9igBxAW-HWhcOJ0XDOEdhPQ";

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
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Random choice: {choice}"));
    }
}
# DiscordBot

A Discord bot that provides random choices from a predefined list.

## Setup

1. Ensure you have .NET 8.0 installed.
2. Clone or navigate to the project directory.
3. In the `DiscordBot/.env` file, set your Discord bot token:
   ```
   TOKEN=your_discord_bot_token_here
   ```

## Running the Bot

From the project root directory, run:

```
cd DiscordBot; dotnet run --project DiscordBot.csproj
```

The bot will connect to Discord and register the `/random_variant` slash command.

## Features

- `/random_variant`: Selects a random choice from `choices.json`.

## Dependencies

- DSharpPlus
- DotNetEnv

Restore packages with `dotnet restore`.

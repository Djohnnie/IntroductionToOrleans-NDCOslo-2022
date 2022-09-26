﻿using CSharpWars.Common.Extensions;
using CSharpWars.Enums;

namespace CSharpWars.Orleans.Contracts.Model;

public class BotProperties
{
    public Guid BotId { get; private set; }
    public string Name { get; private set; }
    public string PlayerName { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public Orientation Orientation { get; private set; }
    public Move LastMove { get; private set; }
    public int MaximumHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MaximumStamina { get; private set; }
    public int CurrentStamina { get; private set; }
    public Dictionary<string, string> Memory { get; private set; }
    public List<Bot> Bots { get; set; }
    public Move CurrentMove { get; set; }
    public int MoveDestinationX { get; set; }
    public int MoveDestinationY { get; set; }
    public string Message { get; set; }

    private BotProperties() { }

    public void Update(BotDto bot)
    {
        CurrentHealth = bot.CurrentHealth;
        X = bot.X;
        Y = bot.Y;
    }

    public static BotProperties Build(BotDto bot, ArenaDto arena, IList<BotDto> bots)
    {
        return new BotProperties
        {
            BotId = bot.BotId,
            Name = bot.BotName,
            PlayerName = bot.PlayerName,
            Width = arena.Width,
            Height = arena.Height,
            X = bot.X,
            Y = bot.Y,
            Orientation = bot.Orientation,
            LastMove = bot.Move,
            MaximumHealth = bot.MaximumHealth,
            CurrentHealth = bot.CurrentHealth,
            MaximumStamina = bot.MaximumStamina,
            CurrentStamina = bot.CurrentStamina,
            Memory = bot.Memory.Deserialize<Dictionary<string, string>>() ?? new Dictionary<string, string>(),
            Bots = BuildBots(bots),
            CurrentMove = Move.Idling
        };
    }

    private static List<Bot> BuildBots(IList<BotDto> bots)
    {
        return bots.Select(Bot.Build).ToList();
    }
}
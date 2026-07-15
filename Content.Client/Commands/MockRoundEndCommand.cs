using Content.Client.RoundEnd;
using Content.Shared.Administration;
using JetBrains.Annotations;
using Robust.Shared.Console;
using RoundEndPlayerInfo = Content.Shared.GameTicking.RoundEndMessageEvent.RoundEndPlayerInfo;

namespace Content.Client.Commands;

/// <summary>
/// Opens the round end summary window with generated player data,
/// so the manifest can be tested without playing out a full round.
/// </summary>
[UsedImplicitly, AnyCommand]
public sealed class MockRoundEndCommand : LocalizedCommands
{
    public override string Command => "mockroundend";

    public override string Help => LocalizationManager.GetString($"cmd-{Command}-help", ("command", Command));

    private static readonly string[] FirstNames =
    {
        "Urist", "Zoe", "Bartholomew", "Pax", "Evangelina", "Bob", "Maximilian", "Io",
        "Cassandra", "Jim", "Wilhelmina", "Rex", "Anastasia", "Ed", "Montgomery", "Sue",
    };

    private static readonly string[] LastNames =
    {
        "McHands", "Ng", "Featherstonehaugh-Cholmondeley", "Fo", "Wojciechowski", "Day",
        "Baggins-Took of the Shire", "Ito", "Vanderbilt-Rockefeller III", "Kim",
        "Schwarzenegger", "Ash", "Bonaparte-Habsburg", "Oh", "Pumpernickel", "Le",
    };

    private static readonly string[] Roles =
    {
        "Captain", "Head of Security", "Senior Station Engineer", "Passenger", "Clown",
        "Chief Medical Officer", "Janitor", "Research Director", "Salvage Specialist",
        "Bartender", "Atmospheric Technician", "Quartermaster", "Chaplain", "Botanist",
    };

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        var count = 40;
        if (args.Length > 0 && (!int.TryParse(args[0], out count) || count < 1))
        {
            shell.WriteLine(Help);
            return;
        }

        var random = new Random(1337);
        var players = new RoundEndPlayerInfo[count];
        for (var i = 0; i < count; i++)
        {
            var icName = $"{FirstNames[random.Next(FirstNames.Length)]} {LastNames[random.Next(LastNames.Length)]}";
            var observer = random.Next(5) == 0;
            var antag = !observer && random.Next(6) == 0;

            players[i] = new RoundEndPlayerInfo
            {
                PlayerOOCName = $"player{i:D2}_{(random.Next(3) == 0 ? "with_a_very_long_ooc_name" : "ok")}",
                PlayerICName = icName,
                Role = observer ? "Observer" : Roles[random.Next(Roles.Length)],
                JobPrototypes = [],
                AntagPrototypes = [],
                Antag = antag,
                Observer = observer,
                Connected = true,
            };
        }

        new RoundEndSummaryWindow(
            "Mock Gamemode",
            "This is a mock round end summary for UI testing.",
            TimeSpan.FromMinutes(42.5),
            random.Next(1000, 100000),
            players);
    }
}

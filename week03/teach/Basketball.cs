/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("/Users/tjnelson/dev/cse212-hw/week03/teach/basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            if (fields.Length <= 8)
                continue;

            var playerId = fields[0];
            if (!int.TryParse(fields[8], out var points))
                continue;

            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        // Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        var topPlayers = players.ToArray();
        Array.Sort(topPlayers, (p1, p2) => p2.Value.CompareTo(p1.Value));

        Console.WriteLine();
        var count = Math.Min(10, topPlayers.Length);
        Console.WriteLine($"Top {count} players by total points:\n");
        Console.WriteLine($"{"#",2}  {"Player",-12} {"Points",10}");
        Console.WriteLine(new string('-', 2 + 2 + 12 + 1 + 10));

        for (var i = 0; i < count; ++i)
        {
            var (playerId, totalPoints) = (topPlayers[i].Key, topPlayers[i].Value);
            Console.WriteLine($"{i + 1,2}  {playerId,-12} {totalPoints,10}");
        }
    }
}
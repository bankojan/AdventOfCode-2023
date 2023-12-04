var lines = File.ReadAllLines(".\\Input.txt");

SolvePart1(lines);
SolvePart2(lines);

void SolvePart1(string[] lines)
{
    var maxRed = 12;
    var maxGreen = 13;
    var maxBlue = 14;

    var gameSum = 0;
    foreach (var line in lines)
    {
        var (gameId, red, green, blue) = ParseGame(line);
        if(red <= maxRed && green <= maxGreen && blue <= maxBlue)
        {
            gameSum += gameId;
        }
    }

    Console.WriteLine($"Part 1 solution: {gameSum}");
}

void SolvePart2(string[] lines)
{
    var gameSum = 0;
    foreach (var line in lines)
    {
        var (_, red, green, blue) = ParseGame(line);
        gameSum += red * green * blue;
    }

    Console.WriteLine($"Part 2 solution: {gameSum}");
}

(int gameId, int red, int green, int blue) ParseGame(string line)
{
    var gameSplit = line.Split(":");
    var gameId = int.Parse(gameSplit[0].Split(" ").Last());
    var sets = gameSplit[1].Trim().Split(";");

    var maxRed = 0;
    var maxGreen = 0;
    var maxBlue = 0;

    foreach (var set in sets)
    {
        var colourSets = set.Split(", ");

        foreach (var colourSet in colourSets)
        {
            var colourSplit = colourSet.Trim().Split(" ");
            var count = int.Parse(colourSplit[0]);
            var colour = colourSplit[1];

            switch(colour)
            {
                case "red":
                    maxRed = int.Max(maxRed, count);
                    break;
                case "green":
                    maxGreen = int.Max(maxGreen, count);
                    break;
                case "blue":
                    maxBlue = int.Max(maxBlue, count);
                    break;
                default:
                    break;
            }
        }
    }

    return (gameId, maxRed, maxGreen, maxBlue);
}
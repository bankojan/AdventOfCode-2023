using Common;
using System.Numerics;

var lines = File.ReadAllLines(".\\Input.txt");

SolvePart1();
SolvePart2();

void SolvePart1()
{
    var directions = lines[0].Trim().ToList();
    var positionLines = lines.Skip(2).ToList();

    var positions = ParsePositions(positionLines);

    var startPosition = "AAA";
    var endPosition = "ZZZ";
    var i = 0;

    var count = 0;

    Position currentPosition = positions[startPosition];

    while (true)
    {
        var direction = directions[i];

        if (direction == 'L')
        {
            currentPosition = positions[currentPosition.Left];
        }
        else
        {
            currentPosition = positions[currentPosition.Right];
        }

        if (currentPosition.Current == endPosition)
        {
            break;
        }

        i = (i + 1) % directions.Count;
        count++;
    }

    Console.WriteLine($"Part 1 solution: {count + 1}");
}

void SolvePart2()
{
    var directions = lines[0].Trim().ToList();
    var positionLines = lines.Skip(2).ToList();

    var positions = ParsePositions(positionLines);

    var currentPositions = positions
        .Where(p => p.Key[^1] == 'A')
        .Select(p => p.Value)
        .ToList();

    var scores = new List<long>();

    foreach (var position1 in currentPositions)
    {
        var position = position1;
        var i = 0;
        var count = 0;

        while (true)
        {
            var direction = directions[i];

            if (direction == 'L')
            {
                position = positions[position.Left];
            }
            else
            {
                position = positions[position.Right];
            }

            if (position.Current[^1] == 'Z')
            {
                scores.Add(count + 1);
                break;
            }

            i = (i + 1) % directions.Count;
            count++;
        }
    }

    long score = MathHelpers.LeastCommonMultiple(scores);

    Console.WriteLine($"Part 2 solution: {score}");
}


Dictionary<string, Position> ParsePositions(List<string> pLines)
{
    var positions = new Dictionary<string, Position>();

    foreach (var line in pLines)
    {
        var split1 = line.Split(" = ");

        var current = split1[0].Trim();
        var split2 = split1[1].Trim().Split(", ");

        var left = split2[0][1..];
        var right = split2[1][..(split2[1].Length - 1)];

        positions.Add(current, new(current, left, right));
    }

    return positions;
}

record Position(string Current, string Left, string Right);
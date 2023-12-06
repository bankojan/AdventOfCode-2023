var lines = File.ReadAllLines(".\\Input.txt");

SolvePart1();
SolvePart2();

void SolvePart1()
{
    var times = lines[0]
        .Split(":")[1].Trim()
        .Split(" ")
        .Where(s => !string.IsNullOrEmpty(s))
        .Select(int.Parse)
        .ToList();

    var distances = lines[1]
        .Split(":")[1].Trim()
        .Split(" ")
        .Where(s => !string.IsNullOrEmpty(s))
        .Select(int.Parse)
        .ToList();

    var i = 0;
    var score = 0;

    foreach (var time in times)
    {
        var min = 0;
        var max = 0;

        for (var j = 1; j < time; j++)
        {
            if(j * (time - j) > distances[i])
            {
                min = j;
                break;
            }
        }
        for (var j = time - 1; j >= min; j--)
        {
            if (j * (time - j) > distances[i])
            {
                max = j;
                break;
            }
        }

        var wins = max - min + 1;
        score = score == 0 ? wins : score * wins;

        i++;
    }

    Console.WriteLine($"Part 1 solution: {score}");
}

void SolvePart2()
{
    var timeString = lines[0]
        .Split(":")[1].Trim()
        .Split(" ")
        .Where(s => !string.IsNullOrEmpty(s));
    var time2 = long.Parse(string.Join(string.Empty, timeString));

    var distanceString = lines[1]
        .Split(":")[1].Trim()
        .Split(" ")
        .Where(s => !string.IsNullOrEmpty(s));
    var distance2 = long.Parse(string.Join(string.Empty, distanceString));

    long score = 0;

    long min = 0;
    long max = 0;

    for (var j = 1; j < time2; j++)
    {
        if (j * (time2 - j) > distance2)
        {
            min = j;
            break;
        }
    }
    for (long j = time2 - 1; j >= min; j--)
    {
        if (j * (time2 - j) > distance2)
        {
            max = j;
            break;
        }
    }

    long wins = max - min + 1;
    score = score == 0 ? wins : score * wins;
    
    Console.WriteLine($"Part 2 solution: {score}");
}
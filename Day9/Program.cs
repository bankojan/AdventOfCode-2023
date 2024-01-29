var lines = File.ReadAllLines(".\\Input.txt")
    .Select(l => l.Trim())
    .ToList();



SolvePart1(lines);
SolvePart2(lines);

void SolvePart1(List<string> sequences)
{
    var score = 0;

    foreach (var sequenceString in sequences)
    {
        var sequence = sequenceString.Split(" ")
            .Select(l => int.Parse(l.Trim()))
            .ToList();

        var subSequences = new List<List<int>>
        {
            sequence
        };

        var currentSubsequence = 0;

        while (true)
        {
            var subSequence = new List<int>();

            for (int i = 0; i < subSequences[currentSubsequence].Count - 1; i++)
            {
                subSequence.Add(subSequences[currentSubsequence][i + 1] - subSequences[currentSubsequence][i]);
            }

            subSequences.Add(subSequence);

            if(!subSequence.Any(s => s != 0))
            {
                break;
            }

            currentSubsequence++;
        }

        var extrapolatedValue = 0;
        for (int i = subSequences.Count - 1; i >= 0; i--)
        {
            extrapolatedValue += subSequences[i].Last();
        }

        score += extrapolatedValue;
    }

    Console.WriteLine($"Part 1 solution: {score}");
}

void SolvePart2(List<string> sequences)
{
    var score = 0;

    foreach (var sequenceString in sequences)
    {
        var sequence = sequenceString.Split(" ")
            .Select(l => int.Parse(l.Trim()))
            .ToList();

        var subSequences = new List<List<int>>
        {
            sequence
        };

        var currentSubsequence = 0;

        while (true)
        {
            var subSequence = new List<int>();

            for (int i = 0; i < subSequences[currentSubsequence].Count - 1; i++)
            {
                subSequence.Add(subSequences[currentSubsequence][i + 1] - subSequences[currentSubsequence][i]);
            }

            subSequences.Add(subSequence);

            if (!subSequence.Any(s => s != 0))
            {
                break;
            }

            currentSubsequence++;
        }

        var extrapolatedValue = 0;
        for (int i = subSequences.Count - 2; i >= 0; i--)
        {
            extrapolatedValue = subSequences[i].First() - extrapolatedValue;
        }

        score += extrapolatedValue;
    }

    Console.WriteLine($"Part 2 solution: {score}");
}
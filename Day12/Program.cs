var lines = File.ReadAllLines(".\\Input.txt");

var records = lines
    .Select(l => l.Split(" ")[0])
    .ToList();

var damageRecords = lines
    .Select(l => l.Split(" ")[1].Split(",").Select(c => int.Parse(c.Trim())).ToList())
    .ToList();

Dictionary<(string line, string), HashSet<string>> visited = new();

SolvePart2();
void SolvePart1()
{
    var lineIndex = 0;
    var damageCharacters = new char[] { '?', '#' };
    var arrangmentCount = 0;

    foreach (var line in records)
    {
        var lineDamageRecords = damageRecords[lineIndex++]
            .ToList();

        Stack<(string line, List<int> remainingDamages)> solutionStack = new();
        solutionStack.Push((line, lineDamageRecords));

        var possibleSolutions = new HashSet<string>();

        while(solutionStack.TryPop(out var solution))
        {
            if(solution.remainingDamages.Count == 0)
            {
                if (!solution.line.Contains('#'))
                {
                    possibleSolutions.Add(solution.line);
                }
                
                continue;
            }

            var damageRecord = solution.remainingDamages.First();
            var newRemainingDamages = new List<int>(solution.remainingDamages);
            newRemainingDamages.Remove(damageRecord);

            var possibleIndexes = new List<int>();
            var actualIndexes = new List<int>();

            var lastAssignedIndex = solution.line.Select((l, i) => (l, i))
                .Where(l => l.l == 'D')
                .OrderByDescending(l => l.i)
                .FirstOrDefault().i;

            if (lastAssignedIndex == default)
            {
                lastAssignedIndex = 0;
            }

            for (int i = lastAssignedIndex; i <= solution.line.Length - damageRecord; i++)
            {
                var subLine = solution.line.Substring(i, damageRecord);
                var isViable = (i - 1) >= 0 ?
                    solution.line[i - 1] != '#' &&
                    solution.line[i - 1] != 'D' : true;

                isViable &= (i + damageRecord) < solution.line.Length ?
                    solution.line[i + damageRecord] != '#' &&
                    solution.line[i + damageRecord] != 'D' : true;

                isViable &= solution.line[i] != 'D';

                if (!isViable)
                {
                    continue;
                }

                if(!subLine.Any(c => c != '#'))
                {
                    actualIndexes.Add(i);
                }
                if(!subLine.Any(c => !damageCharacters.Contains(c)))
                {
                    possibleIndexes.Add(i);
                }
            }

            

            if (actualIndexes.Count == 1 && possibleIndexes.Count == 1)
            {
                var newLine = solution.line.ToList();

                for (int j = actualIndexes.First(); j < actualIndexes.First() + damageRecord; j++)
                {
                    newLine[j] = 'D';
                }

                solutionStack.Push((string.Join("", newLine), newRemainingDamages));
            }
            else
            {
                foreach (var possibleIndex in possibleIndexes)
                {
                    var newLine = solution.line.ToList();

                    for (int j = possibleIndex; j < possibleIndex + damageRecord; j++)
                    {
                        newLine[j] = 'D';
                    }

                    solutionStack.Push((string.Join("", newLine), newRemainingDamages));
                }
            }
        }
        arrangmentCount += possibleSolutions.Count;
    }

    Console.WriteLine($"Part 1 solution: {arrangmentCount}");
}


HashSet<string> FindSolution(string line, List<int> remainingDamages)
{
    if (remainingDamages.Count == 0)
    {
        if (!line.Contains('#'))
        {
            return [line];
        }

        return new();
    }

    var damageRecord = remainingDamages.First();
    var newRemainingDamages = new List<int>(remainingDamages);
    newRemainingDamages.Remove(damageRecord);

    var possibleIndexes = new List<int>();
    var actualIndexes = new List<int>();

    var lastAssignedIndex = line.Select((l, i) => (l, i))
        .Where(l => l.l == 'D')
        .OrderByDescending(l => l.i)
        .FirstOrDefault().i;

    if (lastAssignedIndex == default)
    {
        lastAssignedIndex = 0;
    }

    var substring = line.Substring(lastAssignedIndex);
    if (visited.TryGetValue((substring, string.Join("", remainingDamages)), out var s))
    {
        Console.WriteLine("HIT");
        return s;
    }

    for (int i = lastAssignedIndex; i <= line.Length - damageRecord; i++)
    {
        var subLine = line.Substring(i, damageRecord);
        var isViable = (i - 1) >= 0 ?
            line[i - 1] != '#' &&
            line[i - 1] != 'D' : true;

        isViable &= (i + damageRecord) < line.Length ?
            line[i + damageRecord] != '#' &&
            line[i + damageRecord] != 'D' : true;

        isViable &= line[i] != 'D';

        if (!isViable)
        {
            continue;
        }

        if (!subLine.Any(c => c != '#'))
        {
            actualIndexes.Add(i);
        }
        if (!subLine.Any(c => !new char[] { '#', '?' }.Contains(c)))
        {
            possibleIndexes.Add(i);
        }
    }

    if (actualIndexes.Count == 1 && possibleIndexes.Count == 1)
    {
        var newLine = line.ToList();

        for (int j = actualIndexes.First(); j < actualIndexes.First() + damageRecord; j++)
        {
            newLine[j] = 'D';
        }

        var newLineString = string.Join("", newLine);
        var set = FindSolution(newLineString, newRemainingDamages);

        var newSubstring = newLineString.Substring(actualIndexes.First() + damageRecord);
        visited[(newSubstring, string.Join("", newRemainingDamages))] = set;

        return set;
    }
    else
    {
        var set = new HashSet<string>();
        foreach (var possibleIndex in possibleIndexes)
        {
            var newLine = line.ToList();

            for (int j = possibleIndex; j < possibleIndex + damageRecord; j++)
            {
                newLine[j] = 'D';
            }

            var newLineString = string.Join("", newLine);
            var newSet = FindSolution(newLineString, newRemainingDamages);

            var newSubstring = newLineString.Substring(possibleIndex + damageRecord);
            visited[(newSubstring, string.Join("", newRemainingDamages))] = newSet;

            set = [..set, ..newSet];
        }

        return set;
    }
}

void SolvePart2()
{
    var lineIndex = 0;
    var damageCharacters = new char[] { '?', '#' };
    var arrangmentCount = 0;

    var expandedRecords = new List<string>();
    var expandedDamageRecords = new List<List<int>>();

    foreach (var line in records)
    {
        var expandedRecord = new List<char>();
        var lineList = line.ToList();

        for (int i = 0; i < 5; i++)
        {
            expandedRecord.AddRange(lineList);
            expandedRecord.Add('?');
        }

        expandedRecords.Add(string.Join("", expandedRecord));
    }

    foreach (var damageRecord in damageRecords)
    {
        var expandedDamageRecord = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            expandedDamageRecord.AddRange(damageRecord);
        }

        expandedDamageRecords.Add(expandedDamageRecord);
    }

    foreach (var line in expandedRecords)
    {
        var lineDamageRecords = expandedDamageRecords[lineIndex++]
            .ToList();

        var possibleSolutions = FindSolution(line, lineDamageRecords);

        Console.WriteLine(possibleSolutions.Count);
        arrangmentCount += possibleSolutions.Count;
    }

    Console.WriteLine($"Part 1 solution: {arrangmentCount}");
}

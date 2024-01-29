var lines = File.ReadAllLines(".\\Input.txt");

SolvePart1();
SolvePart2();

void SolvePart1()
{
	var galaxies = ParsePoints(lines, 2);

    var totalDistance = CalculateTotalDistance(galaxies);
    Console.WriteLine($"Part 1 solution: {totalDistance}");
}

void SolvePart2()
{
    var galaxies = ParsePoints(lines, 1_000_000);

    var totalDistance = CalculateTotalDistance(galaxies);
    Console.WriteLine($"Part 2 solution: {totalDistance}");
}

long CalculateTotalDistance(List<(long x, long y, char c)> galaxies)
{ 
    var distances = new List<long>();

    for (int i = 0; i < galaxies.Count - 1; i++)
    {
        for (int k = i + 1; k < galaxies.Count; k++)
        {
            var g1 = galaxies[i];
            var g2 = galaxies[k];

            distances.Add(ManhattanDistance((g1.x, g1.y), (g2.x, g2.y)));
        }
    }

    var totalDistance = distances.Sum();
    return totalDistance;

    long ManhattanDistance((long x, long y) p1, (long x, long y) p2)
        => Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
}

List<(long x, long y, char c)> ParsePoints(string[] lines, long expandBy = 1)
{
    var points = new List<(long x, long y, char c)>();

    var j = 0;
    foreach (var line in lines)
    {
        for (var i = 0; i < line.Length; i++)
        {
            points.Add((j, i, line[i]));
        }
        j++;
    }

    var emptyRows = points
        .GroupBy(x => x.x)
        .Where(x => !x.Any(c => c.c == '#'))
        .Select(x => x.Key)
        .ToHashSet();

    var emptyColumns = points
        .GroupBy(x => x.y)
        .Where(x => !x.Any(c => c.c == '#'))
        .Select(x => x.Key)
        .ToHashSet();

    var initialGalaxies = points
        .Where(p => p.c == '#')
        .ToList();

    return initialGalaxies
        .Select(g => (g.x + (emptyRows.Count(r => r < g.x) * (expandBy - 1)), g.y + (emptyColumns.Count(r => r < g.y) * (expandBy - 1)), '#'))
        .ToList();
}

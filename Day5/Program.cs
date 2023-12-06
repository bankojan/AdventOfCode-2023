using System.Diagnostics;

var lines = File.ReadAllLines(".\\Input.txt");

var seedLine = lines[0];

var emptyLineIndexes = lines
    .Select((l, i) => (l, i))
    .Where(l => string.IsNullOrEmpty(l.l))
    .Select(l => l.i)
    .ToList();

var seedToSoilLines = lines
    .Skip(emptyLineIndexes[0] + 2)
    .Take(emptyLineIndexes[1] - emptyLineIndexes[0] - 2)
    .ToList();

var soilToFertilizerLines = lines
    .Skip(emptyLineIndexes[1] + 2)
    .Take(emptyLineIndexes[2] - emptyLineIndexes[1] - 2)
    .ToList();

var fertilizerToWaterLines = lines
    .Skip(emptyLineIndexes[2] + 2)
    .Take(emptyLineIndexes[3] - emptyLineIndexes[2] - 2)
    .ToList();

var waterToLightLines = lines
    .Skip(emptyLineIndexes[3] + 2)
    .Take(emptyLineIndexes[4] - emptyLineIndexes[3] - 2)
    .ToList();

var lightToTemperatureLines = lines
    .Skip(emptyLineIndexes[4] + 2)
    .Take(emptyLineIndexes[5] - emptyLineIndexes[4] - 2)
    .ToList();

var temperatureToHumidityLines = lines
    .Skip(emptyLineIndexes[5] + 2)
    .Take(emptyLineIndexes[6] - emptyLineIndexes[5] - 2)
    .ToList();

var humidityToLocationLines = lines
    .Skip(emptyLineIndexes[6] + 2)
    .ToList();

List<(long destinationStart, long sourceStart, long count)> seedToSoilMap = GenerateMapFromLines(seedToSoilLines);
List<(long destinationStart, long sourceStart, long count)> soilToFertilizerMap = GenerateMapFromLines(soilToFertilizerLines);
List<(long destinationStart, long sourceStart, long count)> fertilizerToWaterMap = GenerateMapFromLines(fertilizerToWaterLines);
List<(long destinationStart, long sourceStart, long count)> waterToLightMap = GenerateMapFromLines(waterToLightLines);
List<(long destinationStart, long sourceStart, long count)> lightToTemperatureMap = GenerateMapFromLines(lightToTemperatureLines);
List<(long destinationStart, long sourceStart, long count)> temperatureToHumidityMap = GenerateMapFromLines(temperatureToHumidityLines);
List<(long destinationStart, long sourceStart, long count)> humidityToLocationMap = GenerateMapFromLines(humidityToLocationLines);

//SolvePart1(seedLine);
SolvePart2(seedLine);

long TransformSourceToDestination(List<(long destinationStart, long sourceStart, long count)> map, long source)
{
    (long destinationStart, long sourceStart, long count) hit = default;

    foreach (var m in map)
    {
        if (m.sourceStart <= source && (m.sourceStart + m.count) > source)
        {
            hit = m;
            break;
        }
    }

    if (hit == default)
    {
        return source;
    }

    return hit.destinationStart + (source - hit.sourceStart);
}

void SolvePart1(string seedString)
{
    var seeds = seedString
        .Split(":")[1]
        .Trim()
        .Split(" ")
        .Select(long.Parse)
        .ToList();

    var minLocation = long.MaxValue;

    foreach (var seed in seeds)
    {
        var soil = TransformSourceToDestination(seedToSoilMap, seed);
        var fertilizer = TransformSourceToDestination(soilToFertilizerMap, soil);
        var water = TransformSourceToDestination(fertilizerToWaterMap, fertilizer);
        var light = TransformSourceToDestination(waterToLightMap, water);
        var temperature = TransformSourceToDestination(lightToTemperatureMap, light);
        var humidity = TransformSourceToDestination(temperatureToHumidityMap, temperature);
        var location = TransformSourceToDestination(humidityToLocationMap, humidity);

        minLocation = long.Min(minLocation, location);
    }

    Console.WriteLine($"Part 1 solution: {minLocation}");
}

void SolvePart2(string seedString)
{
    object pLock = new();
    var sw = new Stopwatch();
    sw.Start();

    var seedChunks = seedString
        .Split(":")[1]
        .Trim()
        .Split(" ")
        .Select(long.Parse)
        .Chunk(2);

    var seeds = new HashSet<long>();
    var minLocation = long.MaxValue;
    
    foreach (var chunk in seedChunks)
    {
        Parallel.For(0, chunk[1], i =>
        {
            var seed = chunk[0] + i;

            var soil = TransformSourceToDestination(seedToSoilMap, seed);
            var fertilizer = TransformSourceToDestination(soilToFertilizerMap, soil);
            var water = TransformSourceToDestination(fertilizerToWaterMap, fertilizer);
            var light = TransformSourceToDestination(waterToLightMap, water);
            var temperature = TransformSourceToDestination(lightToTemperatureMap, light);
            var humidity = TransformSourceToDestination(temperatureToHumidityMap, temperature);
            var location = TransformSourceToDestination(humidityToLocationMap, humidity);

            lock (pLock)
            {             
                minLocation = long.Min(minLocation, location);
            }
        });
    }

    Console.WriteLine(sw.ElapsedMilliseconds / 1000 + "s");
    Console.WriteLine($"Part 2 solution: {minLocation}");
}

List<(long destinationStart, long sourceStart, long count)> GenerateMapFromLines(List<string> lines)
{
    var map = new HashSet<(long destinationStart, long sourceStart, long count)>();
    foreach (var line in lines)
    {
        var split = line.Split(' ');
        var destinationStart = long.Parse(split[0]);
        var sourceStart = long.Parse(split[1]);
        var count = long.Parse(split[2]);

        map.Add((destinationStart, sourceStart, count));
    }
    return map.OrderBy(x => x.sourceStart).ToList();
}

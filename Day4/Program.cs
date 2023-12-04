var lines = File.ReadAllLines(".\\Input.txt");

SolvePart1(lines);
SolvePart2(lines);

void SolvePart1(string[] lines)
{
    var cards = lines
        .Select(l => l.Split(": ")[1]);
    var winningNumbers = cards
        .Select(c => c.Split(" | ")[0].Split(" ").Where(s => !string.IsNullOrEmpty(s)).ToList())
        .ToList();
    var myNumbers = cards
        .Select(c => c.Split(" | ")[1].Split(" ").Where(s => !string.IsNullOrEmpty(s)).ToList())
        .ToList();

    var score = winningNumbers
        .Select((w, i) => w.Intersect(myNumbers[i]).Count())
        .Select(m => m == 0 ? 0 : Math.Pow(2, m - 1))
        .Sum();

    Console.WriteLine($"Part 1 solution: {score}");
}

void SolvePart2(string[] lines)
{
    var cards = lines
        .Select(l => l.Split(": ")[1]);
    var winningNumbers = cards
        .Select(c => c.Split(" | ")[0].Split(" ").Where(s => !string.IsNullOrEmpty(s)).ToList())
        .ToList();
    var myNumbers = cards
        .Select(c => c.Split(" | ")[1].Split(" ").Where(s => !string.IsNullOrEmpty(s)).ToList())
        .ToList();

    var cardCount = winningNumbers
        .Select((x, i) => (x, i))
        .ToDictionary(x => x.i, x => 1);

    var i = 0;
    foreach (var winningCard in winningNumbers)
    {
        var myCard = myNumbers[i++];

        var cardScore = winningCard
            .Intersect(myCard)
            .Count();

        for (var j = 0; j < cardScore; j++)
        {
            if(cardCount.ContainsKey(i + j))
            {
                cardCount[i + j] += cardCount[i - 1];
            }
        }
    }

    var score = cardCount.Sum(x => x.Value);
    Console.WriteLine($"Part 2 solution: {score}");

}
var lines = File.ReadAllLines(".\\Input.txt");

SolvePart1();
SolvePart2();
void SolvePart1()
{
    var hands = lines
        .Select(l => (Cards: l.Split(" ")[0], Bid: int.Parse(l.Split(" ")[1])))
        .ToList();

    var handStrengths = hands
        .Select(h => (Strength: HandStrength(h.Cards), h.Cards, h.Bid))
        .OrderByDescending(h => h.Strength)
        .ToList();

    var groupedByStrength = handStrengths
        .GroupBy(h => h.Strength);

    var rank = hands.Count;
    var score = 0;

    foreach (var strength in groupedByStrength)
    {
        var sHands = strength.ToList();
        sHands
            .Sort((h1, h2) => CompareHands(h1.Cards, h2.Cards, false));

        var partialScore = sHands
            .Select(h => h.Bid * rank--)
            .Sum();
        score += partialScore;
    }

    Console.WriteLine($"Part 1 solution: {score}");
}

void SolvePart2()
{
    var hands = lines
        .Select(l => (Cards: l.Split(" ")[0], TransformedCards: TransformHand(l.Split(" ")[0]), Bid: int.Parse(l.Split(" ")[1])))
        .ToList();

    var handStrengths = hands
        .Select(h => (Strength: HandStrength(h.TransformedCards), h.Cards, h.Bid))
        .OrderByDescending(h => h.Strength)
        .ToList();

    var groupedByStrength = handStrengths
        .GroupBy(h => h.Strength);

    var rank = hands.Count;
    var score = 0;

    foreach (var strength in groupedByStrength)
    {
        var sHands = strength.ToList();
        sHands
            .Sort((h1, h2) => CompareHands(h1.Cards, h2.Cards, true));

        var partialScore = sHands
            .Select(h => h.Bid * rank--)
            .Sum();
        score += partialScore;
    }

    Console.WriteLine($"Part 2 solution: {score}");
}

string TransformHand(string hand)
{
    var possibleCards = hand
        .Where(c => c != 'J')
        .Distinct();

    if (!possibleCards.Any())
    {
        return hand;
    }

    var bestCard = possibleCards
        .Select(card => (card, Strength: HandStrength(hand.Replace('J', card))))
        .OrderByDescending(h => h.Strength)
        .First().card;

    return hand.Replace('J', bestCard);
}

int CompareHands(string hand1, string hand2, bool wildCard)
{
    for (int i = 0; i < hand1.Length; i++)
    {
        var strength1 = CardStrength(hand1[i], wildCard);
        var strength2 = CardStrength(hand2[i], wildCard);

        if (strength1 > strength2)
        {
            return -1;
        }
        else if (strength1 < strength2)
        {
            return 1;
        }
    }

    return 1;

    int CardStrength(char card, bool wildCard) => card switch
    {
        'A' => 13,
        'K' => 12,
        'Q' => 11,
        'J' => wildCard ? 0 : 10,
        'T' => 9,
        '9' => 8,
        '8' => 7,
        '7' => 6,
        '6' => 5,
        '5' => 4,
        '4' => 3,
        '3' => 2,
        '2' => 1
    };
}

int HandStrength(string cards)
{
    var grouping = cards
        .GroupBy(c => c);

    if (grouping.Any(g => g.Count() == 5))
    {
        return 7;
    }
    if (grouping.Any(g => g.Count() == 4))
    {
        return 6;
    }
    if (grouping.Any(g => g.Count() == 3))
    {
        if (grouping.Any(g => g.Count() == 2))
        {
            return 5;
        }
        return 4;
    }
    if (grouping.Where(g => g.Count() == 2).Count() == 2)
    {
        return 3;
    }
    if (grouping.Any(g => g.Count() == 2))
    {
        return 2;
    }
    return 1;
}
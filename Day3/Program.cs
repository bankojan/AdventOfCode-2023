var lines = File.ReadAllLines(".\\Input.txt");

HashSet<Sign> signs = new();
HashSet<Number> numbers = new();

for (int row = 0; row < lines.Length; row++)
{
    for (int column = 0; column < lines[row].Length;)
    {
        var (number, length) = SeekNumber(column, lines[row]);

        if (number is not null)
        {
            numbers.Add(new(row, column, length, number.Value));
        }
        else if (lines[row][column] != '.')
        {
            signs.Add(new(row, column, lines[row][column]));
        }

        column += length;
    }
}

SolvePart1();
SolvePart2();

void SolvePart1()
{
    var numberSum = numbers
        .Where(n => IsSignAdjacent(lines, n, signs) is not null)
        .Sum(n => n.Value);

    Console.WriteLine($"Part 1 solution: {numberSum}");
}

void SolvePart2()
{
    Dictionary<Sign, List<int>> possibleGears = new();

    foreach (var number in numbers)
    {
        var sign = IsSignAdjacent(lines, number, signs);
        if(sign is not null && sign.Value == '*')
        {
            if (possibleGears.ContainsKey(sign))
            {
                possibleGears[sign].Add(number.Value);
            }
            else
            {
                possibleGears.Add(sign, [number.Value]);
            }
        }
    }

    var gearSum = possibleGears
        .Where(g => g.Value?.Count == 2)
        .Select(g => g.Value[0] * g.Value[1])
        .Sum();

    Console.WriteLine($"Part 2 solution: {gearSum}");
}

Sign? IsSignAdjacent(string[] lines, Number number, HashSet<Sign> signs)
{
    var sign = signs.FirstOrDefault(s => s.Row == number.Row && s.Column == number.Column - 1);
    if (sign is not null)
    {
        return sign;
    }
    sign = signs.FirstOrDefault(s => s.Row == number.Row && s.Column == number.Column + number.Length);
    if (sign is not null)
    {
        return sign;
    }

    if (number.Row > 0)
    {
        for(int i = number.Column - 1; i <= number.Column + number.Length; i++)
        {
            sign = signs.FirstOrDefault(s => s.Row == number.Row - 1 && s.Column == i);
            if (sign is not null)
            {
                return sign;
            }
        }
    }
    if ((number.Row + 1) < lines.Length)
    {
        for (int i = number.Column - 1; i <= number.Column + number.Length; i++)
        {
            sign = signs.FirstOrDefault(s => s.Row == number.Row + 1 && s.Column == i);
            if (sign is not null)
            {
                return sign;
            }
        }
    }

    return null;
}

(int? number, int length) SeekNumber(int column, string line)
{
    var number = "";
    while (column < line.Length && int.TryParse(line[column].ToString(), out var n))
    {
        number += n;
        column++;
    }

    return string.IsNullOrEmpty(number) ? (null, 1) :
        (int.Parse(number), number.Length);
}

public record Number(int Row, int Column, int Length, int Value);
public record Sign(int Row, int Column, char Value);


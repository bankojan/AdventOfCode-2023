var lines = File.ReadAllLines(".\\Input.txt");

SolvePart1(lines);
SolvePart2(lines);

void SolvePart1(string[] lines)
{
	var totalCalibrationValue = 0;
	foreach (var line in lines)
	{
		var numbers = line
			.Where(x => int.TryParse(x.ToString(), out var _))
			.Select(x => x.ToString());

		var calibrationValue = numbers.First() + numbers.Last();
		totalCalibrationValue += int.Parse(calibrationValue);
	}

	Console.WriteLine($"Part1: Calibration value is {totalCalibrationValue}");
}

void SolvePart2(string[] lines)
{
	List<string> numberNames = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

	var totalCalibrationValue = 0;
	foreach (var line in lines)
	{
		var numbers = new List<string>();
		for (int i = 0; i < line.Length; i++)
		{
			if (int.TryParse(line[i].ToString(), out var number))
			{
				numbers.Add(line[i].ToString());
			}
			else
			{
				var found = numberNames
					.FirstOrDefault(n => (i + n.Length) <= line.Length && n == line.Substring(i, n.Length));
				if(found is not null)
				{
					numbers.Add((numberNames.IndexOf(found)+ 1).ToString());
				}
			}
        }
        var calibrationValue = numbers.First() + numbers.Last();
        totalCalibrationValue += int.Parse(calibrationValue);
    }

    Console.WriteLine($"Part2: Calibration value is {totalCalibrationValue}");
}
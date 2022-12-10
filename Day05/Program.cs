
Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

class Solution
{
    static string _inputData = File.ReadAllText("..//..//..//input.txt");

    public static string PartOne()
    {
        var stacks = GetStacksFromInput();
        var instructions = GetIntructionFromInput();
       
        foreach (var instruction in instructions)
        {
            var qtyOfMoves = instruction[0];
            var origin = instruction[1];
            var destination = instruction[2];

            for (int i = 1; i <= qtyOfMoves; i++)
            {
                var originCrates = stacks[origin];
                stacks[origin] = originCrates.Substring(0, originCrates.Length - 1);

                stacks[destination] = stacks[destination] + originCrates.Substring(originCrates.Length - 1, 1);
            }
        }

        var cratesOnTopOfStacks = string.Empty;
        for (int i = 1; i <= stacks.Count; i++)
            cratesOnTopOfStacks += stacks[i].Substring(stacks[i].Length - 1, 1);

        return cratesOnTopOfStacks;
    }

    public static string PartTwo()
    {
        var stacks = GetStacksFromInput();
        var instructions = GetIntructionFromInput();

        foreach (var instruction in instructions)
        {
            var qtyOfMoves = instruction[0];
            var origin = instruction[1];
            var destination = instruction[2];
           
            var originCrates = stacks[origin];
            stacks[origin] = originCrates.Substring(0, originCrates.Length - qtyOfMoves);

            stacks[destination] = stacks[destination] + originCrates.Substring(originCrates.Length - qtyOfMoves, qtyOfMoves);           
        }

        var cratesOnTopOfStacks = string.Empty;
        for (int i = 1; i <= stacks.Count; i++)
            cratesOnTopOfStacks += stacks[i].Substring(stacks[i].Length - 1, 1);

        return cratesOnTopOfStacks;
    }

    public static Dictionary<int, string> GetStacksFromInput()
    {
        var stacks = new Dictionary<int, string>();
        foreach (var cratesInStack in _inputData.Split("\r\n"))
        {
            if (cratesInStack.Any(x => x.Equals('1'))) break;

            var sizeOfCrate = 3;
            var qtyOfCrates = cratesInStack.Length / sizeOfCrate;
            for (int i = 1; i <= qtyOfCrates; i++)
            {
                var crate = cratesInStack.Skip((i - 1) * (sizeOfCrate + 1)).Take(sizeOfCrate).ToArray();
                if (crate.Any(x => x.Equals('[')))
                {
                    if (stacks.ContainsKey(i))
                        stacks[i] = crate[1] + stacks[i];
                    else
                        stacks.Add(i, crate[1].ToString());
                }
            }
        }

        return stacks;
    }

    public static List<int[]> GetIntructionFromInput()
    {
        var listOfInstructions = new List<int[]>();
        foreach (var instruction in _inputData.Split("\r\n"))
        {
            if (!instruction.Contains("move")) continue;

            listOfInstructions.Add(ConvertTextToArrayOfDigits(instruction));
        }

        return listOfInstructions;
    }

    public static int[] ConvertTextToArrayOfDigits(string text)
    {
        var words = text.Split(' ');
        var digits = words
            .Select(word => int.TryParse(word, out var digit) ? digit : (int?)null)
            .Where(x => x.HasValue)
            .Select(x => x.Value)
            .ToArray();

        return digits;
    }
}
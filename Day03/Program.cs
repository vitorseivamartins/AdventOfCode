
var inputData = File.ReadAllText("..//..//..//input.txt");
Console.WriteLine("PartOne: " + Solution.PartOne(inputData));
Console.WriteLine("PartTwo: " + Solution.PartTwo(inputData));

static class Solution
{
    public static int PartOne(string inputData)
    {
        var sumOfPriorities = 0;
        foreach (var rucksack in inputData.Split("\r\n"))
        {
            var firstHalfOfRucksack = rucksack.Take(rucksack.Length/2);
            var secondHalfOfRucksack = rucksack.Skip(rucksack.Length/2);

            var letterInCommon = firstHalfOfRucksack.Where(f => secondHalfOfRucksack.Any(s => s == f)).First();
            sumOfPriorities += ((int)letterInCommon) - (Char.IsUpper(letterInCommon) ? 38 : 96);
        }

        return sumOfPriorities;
    }

    public static int PartTwo(string inputData)
    {
        var sumOfPriorities = 0;
        foreach (var rucksack in inputData.Split("\r\n").Chunk(3))
        {
            var letterInCommon = rucksack[0].Where(f => rucksack[1].Any(s => s == f) && rucksack[2].Any(s => s == f)).First();           
            sumOfPriorities += ((int)letterInCommon) - (Char.IsUpper(letterInCommon) ? 38 : 96);
        }

        return sumOfPriorities;
    }
}

Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

static class Solution
{
    public static int PartOne()
    {
        var inputData = File.ReadAllText("..//..//..//input.txt");

        var inputByElf = inputData.Split("\r\n\r\n").ToList();
        var totalCaloriesFoundInTop1Elf = 0;
        foreach(var elf in inputByElf)
        {
            var totalCaloriesOfCurrentElf = elf.Split("\r\n").Sum(x => int.Parse(x));
            if (totalCaloriesOfCurrentElf > totalCaloriesFoundInTop1Elf)
                totalCaloriesFoundInTop1Elf = elf.Split("\r\n").Sum(x => int.Parse(x));
        }

        return totalCaloriesFoundInTop1Elf;
    }

    public static int PartTwo()
    {
        var inputData = File.ReadAllText("..//..//..//input.txt");

        var totalByElf = new List<int>();
        foreach (var elf in inputData.Split("\r\n\r\n"))
            totalByElf.Add(elf.Split("\r\n").Sum(x => int.Parse(x)));

        return totalByElf.OrderByDescending(x => x).Take(3).Sum();
    }
}
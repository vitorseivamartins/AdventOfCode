
var inputData = File.ReadAllText("..//..//..//input.txt");
Console.WriteLine("PartOne: " + Solution.PartOne(inputData));
Console.WriteLine("PartTwo: " + Solution.PartTwo(inputData));

static class Solution
{
    public static int PartOne(string inputData)
    {
        var quantityOfPairsInsideTheOtherFully = 0;
        foreach (var pair in inputData.Split("\r\n"))
        {
            var elfsAssignments = pair.Split(",");
            var firstElfAssignment = elfsAssignments[0].Split("-").Select(x => Convert.ToInt32(x)).ToArray();
            var secondElfAssignment = elfsAssignments[1].Split("-").Select(x => Convert.ToInt32(x)).ToArray();

            if (firstElfAssignment[0] >= secondElfAssignment[0] && firstElfAssignment[1] <= secondElfAssignment[1])
                quantityOfPairsInsideTheOtherFully += 1;
            else if (secondElfAssignment[0] >= firstElfAssignment[0] && secondElfAssignment[1] <= firstElfAssignment[1])
                quantityOfPairsInsideTheOtherFully += 1;
        }

        return quantityOfPairsInsideTheOtherFully;
    }

    public static int PartTwo(string inputData)
    {
        var quantityOfPairsOverlaping = 0;
        foreach (var pair in inputData.Split("\r\n"))
        {
            var elfsAssignments = pair.Split(",");
            var firstElfAssignment = elfsAssignments[0].Split("-").Select(x => Convert.ToInt32(x)).ToArray();
            var secondElfAssignment = elfsAssignments[1].Split("-").Select(x => Convert.ToInt32(x)).ToArray();

            if (IsSomeOfTheValuesBetweenRange(firstElfAssignment,secondElfAssignment[0], secondElfAssignment[1]))
                quantityOfPairsOverlaping += 1;
            else if (IsSomeOfTheValuesBetweenRange(secondElfAssignment, firstElfAssignment[0], firstElfAssignment[1]))
                quantityOfPairsOverlaping += 1;
        }

        return quantityOfPairsOverlaping;
    }

    public static bool IsSomeOfTheValuesBetweenRange(int[] values, int min, int max)
    {
        foreach (var value in values)
            if(value >= min && value <= max) return true;

        return false;
    }
}
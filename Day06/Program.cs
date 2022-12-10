
Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

class Solution
{
    static string _inputData = File.ReadAllText("..//..//..//input.txt");

    public static int PartOne()
    {
        var numberOfCharactersNeeded = 0;
        var buffer = string.Empty;
        for (int i = 1; i <= _inputData.Length; i++)
        {
            if (buffer.Length != 4)
            {
                buffer += _inputData[i-1];
                continue;
            }

            buffer = buffer.Substring(1, buffer.Length - 1) + _inputData[i-1];
            if (buffer.ToArray().GroupBy(x => x).Count() == 4)
            {
                numberOfCharactersNeeded = i;
                break;
            }
        }

        return numberOfCharactersNeeded;
    }

    public static int PartTwo()
    {
        var numberOfCharactersNeeded = 0;
        var buffer = string.Empty;
        for (int i = 1; i <= _inputData.Length; i++)
        {
            if (buffer.Length != 14)
            {
                buffer += _inputData[i - 1];
                continue;
            }

            buffer = buffer.Substring(1, buffer.Length - 1) + _inputData[i - 1];
            if (buffer.ToArray().GroupBy(x => x).Count() == 14)
            {
                numberOfCharactersNeeded = i;
                break;
            }
        }

        return numberOfCharactersNeeded;
    }
}
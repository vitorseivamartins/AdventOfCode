
using System.Linq;

Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

class Solution
{
    static string _inputData = File.ReadAllText("..//..//..//input.txt");

    public static int PartOne()
    {
        var grid = GetGridFromInput();
        var qtyOfTreesVisible = (grid.GetLength(0) + grid.GetLength(1)) * 2 - 4;

        for (int i = 1; i < grid.GetLength(0) - 1; i++)
            for (int j = 1; j < grid.GetLength(1) - 1; j++)
                qtyOfTreesVisible += IsTreeVisible(grid, i, j) ? 1 : 0;

        return qtyOfTreesVisible;
    }

    private static int[,] GetGridFromInput()
    {
        var lines = _inputData.Split("\r\n");
        var grid = new int[lines.First().Length, lines.Count()];

        for (int i = 0; i < lines.Count() ; i++)
            for (int j = 0; j < lines[i].Length; j++)
                grid[i, j] = int.Parse(lines[i][j].ToString());

        return grid;
    }

    private static bool IsTreeVisible(int[,] grid, int xIndex, int yIndex)
    {
        var valuesFromAxisX = GetValuesFromAxis(grid, xIndex, "x");
        var valuesFromAxisY = GetValuesFromAxis(grid, yIndex, "y");
        var value = grid[xIndex, yIndex];

        if (!valuesFromAxisX.Take(yIndex).Any(t => t >= value))
            return true;
        else if (!valuesFromAxisX.Skip(yIndex+1).Any(t => t >= value))
            return true;
        else if (!valuesFromAxisY.Take(xIndex).Any(t => t >= value))
            return true;
        else if (!valuesFromAxisY.Skip(xIndex+1).Any(t => t >= value))
            return true;

        return false;
    }

    private static List<int> GetValuesFromAxis(int[,] grid, int value, string axis)
    {
        var lenghtOfaxis = grid.GetLength(axis.Equals("x") ? 0 : 1);
        var valuesFromAxis = new List<int>();

        for (int i = 0; i < lenghtOfaxis; i++)
            valuesFromAxis.Add(axis.Equals("x") ? grid[value, i] : grid[i, value]);

        return valuesFromAxis;
    } 

    public static int PartTwo()
    {
        var grid = GetGridFromInput();
        var highestScenicScore = 0;

        for (int i = 1; i < grid.GetLength(0) - 1; i++)
            for (int j = 1; j < grid.GetLength(1) - 1; j++)
            {
                var scenicScore = GetScenicScore(grid, i, j);
                if (scenicScore > highestScenicScore) highestScenicScore = scenicScore;
            }

        return highestScenicScore;
    }

    private static int GetScenicScore(int[,] grid, int xIndex, int yIndex)
    {
        var valuesFromAxisX = GetValuesFromAxis(grid, xIndex, "x");
        var valuesFromAxisY = GetValuesFromAxis(grid, yIndex, "y");
        var value = grid[xIndex, yIndex];
        var scenicScore = 1;

        scenicScore *= CalculateScenicScore(valuesFromAxisX.Take(yIndex).Reverse().ToList(), value);
        scenicScore *= CalculateScenicScore(valuesFromAxisX.Skip(yIndex + 1).ToList(), value);
        scenicScore *= CalculateScenicScore(valuesFromAxisY.Take(xIndex).Reverse().ToList(), value);
        scenicScore *= CalculateScenicScore(valuesFromAxisY.Skip(xIndex + 1).ToList(), value);

        return scenicScore;
    }

    private static int CalculateScenicScore(List<int> trees, int valueOfTree)
    {
        if (!trees.Any(t => t >= valueOfTree))
            return trees.Count();
        else
        {
            var qtyVisibleTrees = 0;
            foreach (var tree in trees)
            {
                qtyVisibleTrees++;
                if (tree >= valueOfTree) break;
            }

            return qtyVisibleTrees;
        }
    }
}
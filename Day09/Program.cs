using System.Drawing;

Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

class Solution
{
    static string[] _inputData = File.ReadAllLines("..//..//..//input.txt");

    public static int PartOne()
    {
        return SimulateRope(Enumerable.Range(1, 2).Select(x => new List<Point> { new Point(0, 0) }).ToList());
    }

    public static int PartTwo()
    {
        return SimulateRope(Enumerable.Range(1, 10).Select(x => new List<Point> { new Point(0, 0) }).ToList());
    }

    public static int SimulateRope(List<List<Point>> knots)
    {
        _inputData.Select(line => line.Split(' ')).ToList().ForEach(input => MoveKnot(knots, input[0], int.Parse(input[1])));
        return knots.Last().ToHashSet().Count();
    }

    public static void MoveKnot(List<List<Point>> knots, string direction, int qty)
    {
        var currentPositionOfHead = knots.First().Last();
        for (int i = 1; i <= qty; i++)
        {
            var newPosition = new Point(
                x: currentPositionOfHead.X + (direction == "R" ? 1 : direction == "L" ? -1 : 0),
                y: currentPositionOfHead.Y + (direction == "U" ? 1 : direction == "D" ? -1 : 0)
            );

            knots.First().Add(newPosition);
            currentPositionOfHead = knots.First().Last();

            MoveAdjacentKnot(currentPositionOfHead, knots);
        }
    }

    public static void MoveAdjacentKnot(Point currentPositionOfKnot, List<List<Point>> knots, int knotNumber = 0)
    {
        var adjacentKnot = knotNumber + 1;
        if (adjacentKnot == knots.Count()) return;

        var currentPositionOfAdjacent = knots[adjacentKnot].Last();
        (int x, int y) = (currentPositionOfKnot.X - currentPositionOfAdjacent.X, currentPositionOfKnot.Y - currentPositionOfAdjacent.Y);
        double distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

        if ((int)distance > 1)
            knots[adjacentKnot].Add(new Point(currentPositionOfAdjacent.X + Math.Sign(x), currentPositionOfAdjacent.Y + Math.Sign(y)));

        if (knots.Count() > adjacentKnot && (int)distance > 1)
            MoveAdjacentKnot(knots[adjacentKnot].Last(), knots, knotNumber + 1);
    }
}
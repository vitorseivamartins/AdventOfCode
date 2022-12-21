
using System.Drawing;
using System.IO;
using System.Linq;

Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

class Solution
{
    static string[] _inputData = File.ReadAllLines("..//..//..//input.txt");

    public static int PartOne()
    {
        var terrainData = GetTerrainData();
        return FindShortestPath(
            terrainData.terrainMap,
            terrainData.distanceMap,
            new Point(terrainData.startPosition.X, terrainData.startPosition.Y),
            new Point(terrainData.goalPosition.X, terrainData.goalPosition.Y)
        );
    }
    static ((int X, int Y) startPosition, (int X, int Y) goalPosition, char[,] terrainMap, int[,] distanceMap) GetTerrainData()
    {
        var (startPositionX, startPositionY, goalPositionX, goalPositionY) = (0, 0, 0, 0);

        var terrainMap = new char[_inputData.Length, _inputData[0].Length];
        var distanceMap = new int[_inputData.Length, _inputData[0].Length];

        for (int i = 0; i < terrainMap.GetLength(0); i++)
            for (int j = 0; j < terrainMap.GetLength(1); j++)
            {
                var value = _inputData[_inputData.Length - (1 + i)][j];
                if (value.ToString().Equals("S")) (startPositionX, startPositionY) = (i, j);
                else if (value.ToString().Equals("E")) (goalPositionX, goalPositionY) = (i, j);

                terrainMap[i, j] = value;
                distanceMap[i, j] = -1;

            }

        return ((startPositionX, startPositionY), (goalPositionX, goalPositionY), terrainMap, distanceMap);
    }

    static int FindShortestPath(char[,] terrainMap, int[,] distanceMap, Point currentPosition, Point destinationPosition)
    {
        distanceMap[currentPosition.X, currentPosition.Y] = 0;
        Queue<Point> explorationQueue = new Queue<Point>();
        explorationQueue.Enqueue(currentPosition);
      
        while (currentPosition != destinationPosition)
            currentPosition = ExplorePosition(terrainMap, distanceMap, explorationQueue);

        return distanceMap[currentPosition.X, currentPosition.Y];
    }

    static Point ExplorePosition(char[,] terrainMap, int[,] distanceMap, Queue<Point> explorationQueue)
    {
        if (explorationQueue.Count == 0)
            throw new Exception("No exploration left!");

        var currentPosition = explorationQueue.Dequeue();

        var valueOfCurrentPosition = (int)terrainMap[currentPosition.X, currentPosition.Y];
        if (valueOfCurrentPosition == (int)'S') valueOfCurrentPosition = (int)'a';
        if (valueOfCurrentPosition == (int)'E') valueOfCurrentPosition = (int)'z';
        
        var walkableNeighbors = GetneighborsFromLocation(terrainMap, currentPosition).Where(n =>
                    (terrainMap[n.X, n.Y].Equals('E') ? 
                        (int)'z' :
                        (int)terrainMap[n.X, n.Y]) <= valueOfCurrentPosition + 1
                    && (int)distanceMap[n.X, n.Y] == -1
                );

        foreach (var neighbor in walkableNeighbors)
        {
            distanceMap[neighbor.X, neighbor.Y] = distanceMap[currentPosition.X, currentPosition.Y] + 1;
            explorationQueue.Enqueue(neighbor);
        } 

        return currentPosition;
    }

    static IEnumerable<Point> GetneighborsFromLocation(char[,] map, Point point)
    {
        var neighbors = new List<Point>
        {
            new Point(point.X, point.Y - 1),
            new Point(point.X - 1, point.Y),
            new Point(point.X + 1, point.Y),
            new Point(point.X, point.Y + 1),
        };

        return neighbors.Where(n => n.X >= 0 && n.X < map.GetLength(0) && n.Y >= 0 && n.Y < map.GetLength(1));
    }

    static void PrintDistanceMap(int[,] map)
    {
        Console.Clear();
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
            {
                var value = map[map.GetLength(0) - (1 + i), j];
                Console.SetCursorPosition(j * 3, i);
                Console.WriteLine(value);
            }
    }

    public static int PartTwo()
    {
        var terrainData = GetTerrainData();
        var shortestDistanceToACharacter = 0;

        for (int i = 0; i < terrainData.terrainMap.GetLength(0); i++)
            for (int j = 0; j < terrainData.terrainMap.GetLength(1); j++)
            {
                if (terrainData.terrainMap[i, j].Equals('a'))
                {
                    try
                    {
                        var distance = FindShortestPath(
                            terrainData.terrainMap,
                            (int[,])terrainData.distanceMap.Clone(),
                            new Point(i, j),
                            new Point(terrainData.goalPosition.X, terrainData.goalPosition.Y)
                        );

                        if (shortestDistanceToACharacter > distance || shortestDistanceToACharacter == 0)
                            shortestDistanceToACharacter = distance;
                    }
                    catch (Exception) { }
                }
            };

        return shortestDistanceToACharacter;
    }
}
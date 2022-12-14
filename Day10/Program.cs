
Console.WriteLine("PartOne: " + Solution.PartOne());
Solution.PartTwo();

class Solution
{
    static string[] _inputData = File.ReadAllLines("..//..//..//input.txt");

    public static int PartOne()
    {
        int xValue = 1;
        int instructionNumber = 0;
        bool calculateInstruction = true;
        int sumIfSignalStrenghts = 0;
        int[] cyclesToCalculate = new int[] { 20, 60, 100, 140, 180, 220 };

        foreach (var cycle in Enumerable.Range(1, 220))
        {
            if (cyclesToCalculate.Contains(cycle))
                sumIfSignalStrenghts += cycle * xValue;

            var instructionData = _inputData[instructionNumber];
            if (instructionData.Equals("noop"))
                instructionNumber = instructionNumber + 1;
            else
            {
                calculateInstruction = !calculateInstruction;
                if(calculateInstruction)
                {
                    xValue += int.Parse(instructionData.Split(' ').Last());
                    instructionNumber = instructionNumber + 1;
                }
            }
        }

        return sumIfSignalStrenghts;
    }

    public static void PartTwo()
    {
        Console.Clear();

        int xValue = 1;
        int instructionNumber = 0;
        bool calculateInstruction = true;
        int sumIfSignalStrenghts = 0;
        int[] cyclesToCalculate = new int[] { 20, 60, 100, 140, 180, 220 };

        int crtXPosition = 0;
        int crtYPosition = 0;
        
        foreach (var cycle in Enumerable.Range(1, 240))
        {
            Console.SetCursorPosition(crtXPosition, crtYPosition);
            if(Enumerable.Range(xValue-1,3).Contains(crtXPosition))
                Console.Write("#");
            else
                Console.Write(".");

            if (crtXPosition == 39)
            {
                crtXPosition = 0;
                crtYPosition++;
            }
            else
                crtXPosition++;

            if (cyclesToCalculate.Contains(cycle))
                sumIfSignalStrenghts += cycle * xValue;

            var instructionData = _inputData[instructionNumber];
            if (instructionData.Equals("noop"))
                instructionNumber = instructionNumber + 1;
            else
            {
                calculateInstruction = !calculateInstruction;
                if (calculateInstruction)
                {
                    xValue += int.Parse(instructionData.Split(' ').Last());
                    instructionNumber = instructionNumber + 1;
                }
            }
        }

        Console.Write("\n\r\n\r");
    }
}
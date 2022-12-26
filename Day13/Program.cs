Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

class Solution
{
    static string _inputData = File.ReadAllText("..//..//..//input.txt");

    public static int PartOne()
    {
        var inputList = _inputData.Split("\r\n\r\n").ToList();
        var indicesInRightOrder =  from i in Enumerable.Range(0, inputList.Count)
                                   let pair = inputList[i].Split("\r\n")
                                   where (bool) IsPairInRightOrder(pair[0][1..^1], pair[1][1..^1])
                                   select i + 1;

        return indicesInRightOrder.Sum();
    }

    static bool? IsPairInRightOrder(string leftSide, string rightSide)
    {
        bool? isInRightOrder = null;
        int currentLeftIndex = 0, currentRightIndex = 0;

        if (leftSide.Length == 0 && rightSide.Length == 0)
            return null;
        else if (leftSide.Length == 0 || rightSide.Length == 0)
            return rightSide.Length >= leftSide.Length;

        while (isInRightOrder is null)
        {
            char leftChar, rightChar;
            int leftValue = 0, rightValue = 0;
            try
            {     
                leftChar = leftSide[currentLeftIndex];
                if (char.IsDigit(leftChar))
                    (leftValue, currentLeftIndex) = GetNumber(leftSide, currentLeftIndex);
            }
            catch (Exception)
            {
                return true;
            }

            try
            {
                rightChar = rightSide[currentRightIndex];
                if (char.IsDigit(rightChar))
                    (rightValue, currentRightIndex) = GetNumber(rightSide, currentRightIndex);
            }
            catch (Exception)
            { 
                return false;
            }

            if (!leftChar.Equals(',')) 
            {
                var leftIsList = leftChar.Equals('[');
                var rightIsList = rightChar.Equals('[');

                if (char.IsDigit(leftChar) && char.IsDigit(rightChar))
                {
                    if (leftValue < rightValue)
                        isInRightOrder = true;
                    else if (leftValue > rightValue)
                        isInRightOrder = false;
                    else if(leftSide.Length.Equals(rightSide.Length))
                        return null;
                }
                else if(leftIsList || rightIsList)
                {
                    var leftString = leftSide[currentLeftIndex..];
                    var rightString = rightSide[currentRightIndex..];

                    isInRightOrder = IsPairInRightOrder(
                        leftIsList ? leftString[..GetTailIndexOfLists(leftString)][1..] : leftValue.ToString(),
                        rightIsList ? rightString[..GetTailIndexOfLists(rightString)][1..] : rightValue.ToString());

                    currentLeftIndex += 
                        leftIsList ? GetTailIndexOfLists(leftString) : GetNumber(leftSide, currentLeftIndex).index;

                    currentRightIndex +=
                        rightIsList ? GetTailIndexOfLists(rightString) : GetNumber(rightSide, currentRightIndex).index;
                }
            }

            currentLeftIndex++;
            currentRightIndex++;
        }

        return isInRightOrder;
    }

    static (int value, int index) GetNumber(string line, int index)
    {
        var value = string.Empty;
        while (index < line.Length)
        {
            if (line[index].Equals(',')) break;
            else
                value += line[index];

            index++;
        }

        return (int.Parse(value), index - 1);
    }

    static int GetTailIndexOfLists(string line)
    {
        int counterLeftBracket, counterRightBracket, index;
        counterLeftBracket = counterRightBracket = index = 0;

        while (index < line.Length && (counterLeftBracket != counterRightBracket || counterLeftBracket == 0))
        {
            if (line[index].Equals('[')) counterLeftBracket++;
            else if (line[index].Equals(']')) counterRightBracket++;           
            index++;
        }

        return index - 1;
    }

    public static int PartTwo()
    {
        var dividerPackets = new List<string>() { "[[2]]", "[[6]]" }; 
        var dividerPacketsIndices = new List<int>();
        var inputList = _inputData.Split("\r\n").Where(i => !string.IsNullOrEmpty(i)).ToList();

        foreach (var dividerPacket in dividerPackets)
        {
            dividerPacketsIndices.Add(
                                    (from i in Enumerable.Range(0, inputList.Count)
                                    let list = inputList[i]
                                    where (bool)IsPairInRightOrder(list[1..^1], dividerPacket[1..^1])
                                    select 1).Count() + 1
                                      );
        }

        return dividerPacketsIndices.Aggregate(1, (a,b) => a*b);
    }
}
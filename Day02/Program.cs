
Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

static class Solution
{
    public static int PartOne()
    {
        var inputData = File.ReadAllText("..//..//..//input.txt");

        Dictionary<string, int> possibleMoves = new Dictionary<string, int>();
        possibleMoves.Add("AX", 1); //Rock
        possibleMoves.Add("BY", 2); //Paper
        possibleMoves.Add("CZ", 3); //Scissors

        var totalScore = 0;
        foreach (var round in inputData.Split("\r\n"))
        {
            var opponentChoice = possibleMoves.Where(x => x.Key.Contains(round[0])).First();
            var playerChoice = possibleMoves.Where(x => x.Key.Contains(round[2])).First();

            var resultOfRound = playerChoice.Value - opponentChoice.Value;

            if (resultOfRound == 1 || resultOfRound == -2) //Winning
                totalScore += 6;
            else if (resultOfRound == 0) //Draw
                totalScore += 3;

            totalScore += playerChoice.Value;
        }

        return totalScore;
    }

    public static int PartTwo()
    {
        var inputData = File.ReadAllText("..//..//..//input.txt");

        Dictionary<string, int> possibleMoves = new Dictionary<string, int>();
        possibleMoves.Add("A", 1); //Rock
        possibleMoves.Add("B", 2); //Paper
        possibleMoves.Add("C", 3); //Scissors

        Dictionary<string, int> strategy = new Dictionary<string, int>();
        strategy.Add("X", -1); //Need to lose
        strategy.Add("Y", 0); //Need to draw
        strategy.Add("Z", 1); //Need to win

        var totalScore = 0;
        foreach (var round in inputData.Split("\r\n"))
        {
            var opponentChoice = possibleMoves.Where(x => x.Key.Contains(round[0])).First();

            var playerStrategy = strategy.Where(x => x.Key.Contains(round[2])).First();
            var playerChoice = possibleMoves.Where(x => x.Value.Equals(
                opponentChoice.Value + playerStrategy.Value == 0 ? 3 : //Bound check
                opponentChoice.Value + playerStrategy.Value == 4 ? 1 : //Bound check
                opponentChoice.Value + playerStrategy.Value)
            ).First();

            var resultOfRound = playerChoice.Value - opponentChoice.Value;

            if (resultOfRound == 1 || resultOfRound == -2) //Winning
                totalScore += 6;
            else if (resultOfRound == 0) //Draw
                totalScore += 3;

            totalScore += playerChoice.Value;
        }

        return totalScore;
    }
}
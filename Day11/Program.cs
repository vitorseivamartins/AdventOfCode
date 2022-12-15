
using static Solution;

Console.WriteLine("PartOne: " + PartOne());
Console.WriteLine("PartTwo: " + PartTwo());

class Solution
{
    static string[] _inputData = File.ReadAllText("..//..//..//input.txt").Split("Monkey");

    public static long PartOne()
    {
        var listMonkeys = GetMonkeyList();
        return GetMonkeyBusinessLevel(rounds: 20, listMonkeys, w => w / 3);
    }  

    public static long PartTwo()
    {
        var listMonkeys = GetMonkeyList();
        var mod = listMonkeys.Aggregate(1, (mod, monkey) => mod * monkey.DivisibleBy);
        return GetMonkeyBusinessLevel(rounds: 10_000, listMonkeys, w => w % mod);
    }

    public static long GetMonkeyBusinessLevel(int rounds, List<Monkey> listMonkeys, Func<long, long> releaseStressMethod)
    {
        foreach (var round in Enumerable.Range(1, rounds))
        {
            foreach (var monkey in listMonkeys)
            {
                var monkeyItems = new List<long>(monkey.Items);
                foreach (var item in monkeyItems)
                {
                    var worrieness = item;
                    if (monkey.OperatorOfMonkey.Equals("*"))
                        worrieness *= monkey.FixedOperand.Any(char.IsDigit) ? int.Parse(monkey.FixedOperand) : worrieness;
                    else
                        worrieness += monkey.FixedOperand.Any(char.IsDigit) ? int.Parse(monkey.FixedOperand) : worrieness;

                    worrieness = releaseStressMethod(worrieness);

                    var targetMonkey = worrieness % monkey.DivisibleBy == 0 ? monkey.MonkeyIfTrue : monkey.MonkeyIfFalse;
                    listMonkeys[targetMonkey].Items.Add(worrieness);
                }

                monkey.QtyItemsInspected += monkey.Items.Count();
                monkey.Items.Clear();
            }
        }

        return listMonkeys.OrderByDescending(m => m.QtyItemsInspected).Take(2)
            .Select(s => s.QtyItemsInspected).Aggregate(1L, (a, b) => a * b);
    }

    public static List<Monkey> GetMonkeyList()
    {
        List<Monkey> listMonkeys = new List<Monkey>();
        foreach (var monkeyData in _inputData.Skip(1))
        {
            int monkeyId = 0;
            string fixedOperand = string.Empty;
            string operatorOfMonkey = string.Empty;
            int divisibleBy = 0;
            int monkeyIfTrue = 0;
            int monkeyIfFalse = 0;
            var items = new List<long>();

            int propNumber = 1;
            foreach (var monkeyAtribute in monkeyData.Split("\r\n"))
            {
                switch (propNumber)
                {
                    case 1:
                        monkeyId = int.Parse(monkeyAtribute.Replace(":", ""));
                        break;
                    case 2:
                        items.AddRange(monkeyAtribute.Split(":")[1].Split(", ").Select(i => long.Parse(i.Replace(" ", ""))));
                        break;
                    case 3:
                        var dataCase3 = monkeyAtribute.Split(" ");
                        fixedOperand = dataCase3[dataCase3.Length - 1];
                        operatorOfMonkey = dataCase3[dataCase3.Length - 2];
                        break;
                    case 4:
                        var dataCase4 = monkeyAtribute.Split(" ");
                        divisibleBy = int.Parse(dataCase4[dataCase4.Length - 1]);
                        break;
                    case 5:
                        var dataCase5 = monkeyAtribute.Split(" ");
                        monkeyIfTrue = int.Parse(dataCase5[dataCase5.Length - 1]);
                        break;
                    case 6:
                        var dataCase6 = monkeyAtribute.Split(" ");
                        monkeyIfFalse = int.Parse(dataCase6[dataCase6.Length - 1]);
                        break;
                    default:
                        break;
                }

                propNumber++;
            }

            listMonkeys.Add(new Monkey(monkeyId, fixedOperand, operatorOfMonkey, divisibleBy, monkeyIfTrue, monkeyIfFalse, items));
        }

        return listMonkeys;
    }

    public class Monkey
    {
        public int MonkeyId { get; set; }
        public string FixedOperand { get; set; }
        public string OperatorOfMonkey { get; set; }
        public int DivisibleBy { get; set; }
        public int MonkeyIfTrue { get; set; }
        public int MonkeyIfFalse { get; set; }
        public List<long> Items { get; set; }
        public int QtyItemsInspected { get; set; }

        public Monkey(int monkeyId, string fixedOperand, string operatorOfMonkey, int divisibleBy, int monkeyIfTrue, int monkeyIfFalse, List<long> items)
        {
            MonkeyId = monkeyId;
            FixedOperand = fixedOperand;
            OperatorOfMonkey = operatorOfMonkey;
            DivisibleBy = divisibleBy;
            MonkeyIfTrue = monkeyIfTrue;
            MonkeyIfFalse = monkeyIfFalse;
            Items = items;
        }
    }
}
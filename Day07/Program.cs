
Console.WriteLine("PartOne: " + Solution.PartOne());
Console.WriteLine("PartTwo: " + Solution.PartTwo());

class Solution
{
    static string _inputData = File.ReadAllText("..//..//..//input.txt");

    public static int PartOne()
    {
        var foldersSize = GetFoldersSize();

        var sumOfTotalSizesDirectory = 0;       
        foreach (var folderSize in foldersSize)
        {
            if (folderSize.Value <= 100000)
                sumOfTotalSizesDirectory += folderSize.Value;
        }

        return sumOfTotalSizesDirectory;
    }

    private static Dictionary<string, int> GetFoldersSize()
    {
        var foldersSize = new Dictionary<string, int>();
        var currentPath = string.Empty;

        foreach (var line in _inputData.Split("\r\n"))
        {
            if (line.Contains("$ cd"))
            {
                if (line.Equals("$ cd .."))
                {
                    var paths = currentPath.Split('/');
                    currentPath = string.Join("/", paths.Take(paths.Length - 1));
                }
                else
                {
                    if (line.Equals("$ cd /"))
                        currentPath = ".";
                    else
                        currentPath += "/" + line.Replace("$ cd ", "");

                    if (!foldersSize.ContainsKey(currentPath))
                        foldersSize.Add(currentPath, 0);
                }
            }
            else if (line.Any(l => char.IsDigit(l)))
            {
                var sizeOfFile = int.Parse(string.Join("", line.Where(c => char.IsDigit(c))));
                foldersSize[currentPath] = foldersSize[currentPath] + sizeOfFile;

                AddSizeOfFileToParentStructure(foldersSize, sizeOfFile, currentPath);
            }
        }

        return foldersSize;
    }


    private static void AddSizeOfFileToParentStructure(Dictionary<string, int> foldersSize, int sizeOfFile, string currentFolder)
    {
        if (currentFolder.Equals(".")) return;

        var paths = currentFolder.Split('/');
        var pathToAddSize = string.Join("/", paths.Take(paths.Length - 1));
        foldersSize[pathToAddSize] = foldersSize[pathToAddSize] + sizeOfFile;

        if(pathToAddSize.Split("/").Length > 1)
            AddSizeOfFileToParentStructure(foldersSize, sizeOfFile, pathToAddSize);  
    }

    public static int PartTwo()
    {
        var foldersSize = GetFoldersSize();
        var totalDiskSize = 70000000;
        var totalDiskSizeUsed = foldersSize["."];
        var totalDiskSizeAvailable = totalDiskSize - totalDiskSizeUsed;
        var totalSizeOfUpdateNecessary = 30000000;
        var totalSizeNecessaryToFree = totalSizeOfUpdateNecessary - totalDiskSizeAvailable;

        var candidatesToBeDeleted = new List<int>();
        foreach (var folderSize in foldersSize)
            if (folderSize.Value >= totalSizeNecessaryToFree)
                candidatesToBeDeleted.Add(folderSize.Value);

        return candidatesToBeDeleted.OrderBy(x => x).First();
    }
}
using Domain.Files;
using Lab3;

if (!File.Exists(FileUtil.ReadPath))
{
    const string defaultValue = "4 2\n1 2\n3 4";
    FileUtil.Write(FileUtil.ReadPath, defaultValue);
}

var input = FileUtil.Read(FileUtil.ReadPath);
var firstLine = input[0].Split();
        
var N = int.Parse(firstLine[0]);
var M = int.Parse(firstLine[1]);

List<int> firstTarget = [];
List<int> secondTarget = [];

for (var i = 1; i <= M; i++)
{
    var pair = input[i].Split(); 
    firstTarget.Add(int.Parse(pair[0]));
    secondTarget.Add(int.Parse(pair[1]));
}

var task = new ThirdTask(N, firstTarget, secondTarget);

var res = task.Handle();
Console.WriteLine($"Result = {res}");

FileUtil.Write(FileUtil.WritePath, res);
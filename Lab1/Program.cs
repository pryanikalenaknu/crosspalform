using Domain;
using Domain.Files;
using Lab1;

if (!File.Exists(FileUtil.ReadPath))
{
    const string defaultValue = "4\n2 1 1 1";
    FileUtil.Write(FileUtil.ReadPath, defaultValue);
}

var input = FileUtil.Read(FileUtil.ReadPath);

Validator.IsTrue(input.Length == 2, "Lines of InputFile Should be equel 2");
var N = int.Parse(input[0]);
var splitedM = input[1].Split();

Validator.IsTrue(splitedM.Length == N, "Values of second line should be equal " + N);
var M = Array.ConvertAll(splitedM, int.Parse);
var firstTask = new FirstTask(N, M);

var res = firstTask.Calculate();

Console.WriteLine($"Result = {res}");

FileUtil.Write(FileUtil.WritePath, res);
using Domain;
using Domain.Files;
using Lab2;

if (!File.Exists(FileUtil.ReadPath))
{
    const string defaultValue = "6\n63 29 5 5 28 6";
    FileUtil.Write(FileUtil.ReadPath, defaultValue);
}

var input = FileUtil.Read(FileUtil.ReadPath);

Validator.IsTrue(input.Length == 2, "Lines of InputFile Should be equel 2");
var N = int.Parse(input[0]);
var splitedM = input[1].Split();

Validator.IsTrue(splitedM.Length == N, "Values of second line should be equal " + N);
var M = Array.ConvertAll(splitedM, int.Parse);
var secondTask = new SecondTask(M);

var res = secondTask.Calculate();

Console.WriteLine($"Result = {res}");

FileUtil.Write(FileUtil.WritePath, res);
using Domain;
using Domain.Files;
using Lab1;
using Lab2;
using Lab3;

namespace Lab4;

public class LabSelector(string? labNumber, string input, string output)
{
    
    public void SelectAndExecute()
    {
        switch (labNumber)
        {
            case "lab1":
            {
                ExecuteLab1();
                break;
            }

            case "lab2":
            {
                ExecuteLab2();
                break;
            }

            case "lab3":
            {
                ExecuteLab3();
                break;
            }

            default:
            {
                throw new Exception($"Cannot found lab {labNumber}");
            }
        }
        
        Console.WriteLine($"Result was written into {output}");
    }
    
    private void ExecuteLab1()
    {
        var inputarr = FileUtil.Read(input);

        Validator.IsTrue(inputarr.Length == 2, "Lines of InputFile Should be equel 2");
        var N = int.Parse(inputarr[0]);
        var splitedM = inputarr[1].Split();

        Validator.IsTrue(splitedM.Length == N, "Values of second line should be equal " + N);
        var M = Array.ConvertAll(splitedM, int.Parse);
        var firstTask = new FirstTask(N, M);

        var res = firstTask.Calculate();
        Console.WriteLine($"Result = {res}");
        FileUtil.Write(output, res);
    }
    
    private void ExecuteLab2()
    {
        var inputarr = FileUtil.Read(input);

        Validator.IsTrue(inputarr.Length == 2, "Lines of InputFile Should be equel 2");
        var N = int.Parse(inputarr[0]);
        var splitedM = inputarr[1].Split();

        Validator.IsTrue(splitedM.Length == N, "Values of second line should be equal " + N);
        var M = Array.ConvertAll(splitedM, int.Parse);
        var secondTask = new SecondTask(M);

        var res = secondTask.Calculate();

        Console.WriteLine($"Result = {res}");

        FileUtil.Write(output, res);
    }
    
    private void ExecuteLab3()
    {
        var inputarr = FileUtil.Read(input);
        var firstLine = inputarr[0].Split();
        
        var N = int.Parse(firstLine[0]);
        var M = int.Parse(firstLine[1]);

        List<int> firstTarget = [];
        List<int> secondTarget = [];

        for (var i = 1; i <= M; i++)
        {
            var pair = inputarr[i].Split(); 
            firstTarget.Add(int.Parse(pair[0]));
            secondTarget.Add(int.Parse(pair[1]));
        }

        var task = new ThirdTask(N, firstTarget, secondTarget);

        var res = task.Handle();
        Console.WriteLine($"Result = {res}");

        FileUtil.Write(output, res);
    }
    
}
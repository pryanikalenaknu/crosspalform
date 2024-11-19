using Lab1;
using Lab2;
using Lab3;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class TasksController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Task1()
        {
            return View(new TaskModel());
        }

        [HttpPost]
        public IActionResult Task1(TaskModel input)
        {
            var inputarr = input.InputData.Split("\n").ToList();

            var N = int.Parse(inputarr[0]);
            var splitedM = inputarr[1].Split();

            var M = Array.ConvertAll(splitedM, int.Parse);
            var firstTask = new FirstTask(N, M);

            var res = firstTask.Calculate();
            input.OutputData = res.ToString();
            return View(input);
        }

        [HttpGet]
        public IActionResult Task2()
        {
            return View(new TaskModel());
        }

        [HttpPost]
        public IActionResult Task2(TaskModel input)
        {
            Console.WriteLine(input.InputData);
            var inputarr = input.InputData.Split("\n").ToList();
            
            var splitedM = inputarr[1].Split();

            var M = Array.ConvertAll(splitedM, int.Parse);
            var secondTask = new SecondTask(M);

            var res = secondTask.Calculate();
            input.OutputData = res.ToString();
            return View(input);
        }

        [HttpGet]
        public IActionResult Task3()
        {
            return View(new TaskModel());
        }

        [HttpPost]
        public IActionResult Task3(TaskModel input)
        {
            Console.WriteLine(input.InputData);
            var inputarr = input.InputData.Split("\n").ToList();
            
            var firstLine = inputarr[0].Split(" ");
        
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
            input.OutputData = res.ToString();
            return View(input);
        }
    }
}
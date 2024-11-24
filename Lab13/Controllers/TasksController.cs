using Lab1;
using Lab2;
using Lab3;
using Lab13.Models;
using Lab13.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Controllers
{
    public class TasksController (IAuthService service): Controller
    {
        [HttpPost]
        public async Task<IActionResult> Task1([FromBody]TaskModel input)
        {
            if (!await service.CheckToken(Request.Headers.Authorization.ToString()))
            {
                return Unauthorized();
            }
            
            var inputarr = input.InputData.Split("\n").ToList();

            var N = int.Parse(inputarr[0]);
            Console.WriteLine(N);
            var splitedM = inputarr[1].Split();

            var M = Array.ConvertAll(splitedM, int.Parse);
            var firstTask = new FirstTask(N, M);

            var res = firstTask.Calculate();
            input.OutputData = res.ToString();
            return Json(new
            {
                Output = res.ToString()
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Task2([FromBody]TaskModel input)
        {
            if (!await service.CheckToken(Request.Headers.Authorization.ToString()))
            {
                return Unauthorized();
            }
            Console.WriteLine(input.InputData);
            var inputarr = input.InputData.Split("\n").ToList();
            
            var splitedM = inputarr[1].Split();

            var M = Array.ConvertAll(splitedM, int.Parse);
            var secondTask = new SecondTask(M);

            var res = secondTask.Calculate();
            input.OutputData = res.ToString();
            return Json(new
            {
                Output = res.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Task3([FromBody]TaskModel input)
        {
            if (!await service.CheckToken(Request.Headers.Authorization.ToString()))
            {
                return Unauthorized();
            }
            Console.WriteLine(input.InputData);
            var inputarr = input.InputData.Split("\n").ToList();
            
            var firstLine = inputarr[0].Split(" ");
        
            var N = int.Parse(firstLine[0]);
            var M = int.Parse(firstLine[1]);

            List<int> firstTarget = new List<int>();
            List<int> secondTarget = new List<int>();

            for (var i = 1; i <= M; i++)
            {
                var pair = inputarr[i].Split(); 
                firstTarget.Add(int.Parse(pair[0]));
                secondTarget.Add(int.Parse(pair[1]));
            }

            var task = new ThirdTask(N, firstTarget, secondTarget);

            var res = task.Handle();
            input.OutputData = res.ToString();
            return Json(new
                        {
                            Output = res.ToString()
                        });
        }
    }
}
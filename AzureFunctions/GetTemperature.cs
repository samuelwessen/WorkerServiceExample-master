using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.Models;

namespace AzureFunctions
{
    public static class GetTemperature
    {
        private static Random rnd = new Random();

        [FunctionName("GetTemperature")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log
        )
        {


            return new OkObjectResult(await Task.Run(() => {
                return JsonConvert.SerializeObject(new TemperatureModel(rnd.Next(20, 30), rnd.Next(30, 40)));

            }));

            return new OkObjectResult(await Task.Run(() => {
                return JsonConvert.SerializeObject(new TemperatureModel()
                {
                    Temperature = rnd.Next(20, 30),
                    Humidity = rnd.Next(30, 40),
                    Timestamp = DateTime.Now
                });






                var result = await Task.Run(() =>
            {
                var data = new TemperatureModel()
                {
                    Temperature = rnd.Next(20, 30),
                    Humidity = rnd.Next(30, 40)
                };

                var json = JsonConvert.SerializeObject(data);

                return json;
            });


            return new OkObjectResult(result);
        }
    }
}

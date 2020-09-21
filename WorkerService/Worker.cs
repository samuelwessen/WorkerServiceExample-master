using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.Models;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> log;
        private HttpClient client;

        public Worker(ILogger<Worker> logger)
        {
            log = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();

            log.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();

            log.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await client.GetAsync("localhost:7071/api/GetTemperature");

                if (response.IsSuccessStatusCode)
                {

                    var data = JsonConvert.DeserializeObject<TemperatureModel>(await response.Content.ReadAsStringAsync());

                    log.LogInformation($"Temperature: {data.Temperature}, Humidity: {data.Humidity}, Time: {data.Timestamp}");
                }
                else
                {
                    log.LogInformation("Fail to get data from API at: {time}", DateTimeOffset.Now);
                }


                log.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}

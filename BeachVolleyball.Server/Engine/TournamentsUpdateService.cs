using Engine;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeachVolleyball.Server.Engine
{
    public class TournamentsUpdateService : BackgroundService
    {
        private readonly ITournamentsService tournamentsService;
        private readonly TimeSpan UpdateTime = TimeSpan.FromMinutes(30);

        public TournamentsUpdateService(ITournamentsService tournamentsService)
        {
            this.tournamentsService = tournamentsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Update service is starting.");
            stoppingToken.Register(() =>
                Console.WriteLine("Update background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await this.tournamentsService.UpdateTournamentsAsync(stoppingToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update tournaments in the background. " + e.Message);
                }

                await Task.Delay(UpdateTime, stoppingToken);
            }

            Console.WriteLine("Update service background task is stopping.");
        }
    }
}

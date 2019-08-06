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
        private readonly TimeSpan UpdateTime = TimeSpan.FromSeconds(30);

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
                await this.tournamentsService.UpdateTournamentsAsync(stoppingToken);

                await Task.Delay(UpdateTime, stoppingToken);
            }

            Console.WriteLine("Update service background task is stopping.");
        }
    }
}

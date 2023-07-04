using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using System;

namespace Travel_project.BackGroundServices
{
    public class DateTimeLogWriter : IHostedService
    {
        private IServiceProvider _serviceProvider;

        private Timer timer;
        public DateTimeLogWriter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(DateTimeLogWriter)}Service started....");
            timer = new Timer(writeDateTimeOnLog, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }
        private async void writeDateTimeOnLog(object state)
        {
             //Console.WriteLine("Salam");
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                UserManager<User> scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var today = DateTime.Today;
                var usersWithBirthday = await scopedProcessingService.Users
                    .Where(u => u.BirthDate.HasValue && u.BirthDate.Value.Day == today.Day && u.BirthDate.Value.Month == today.Month)
                    .ToListAsync();
                foreach (var user in usersWithBirthday)
                {
                    SendBirthdayEmail(user);
                }

                Console.WriteLine($"DateTime is {DateTime.Now.ToLongTimeString()}");
            }
        }

        private void SendBirthdayEmail(User user)
        {
            Console.WriteLine($"Sending birthday email to {user.Email}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine($"{nameof(DateTimeLogWriter)}Service stopped....");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer = null!;
        }



    }
}

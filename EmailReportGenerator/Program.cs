using EmailReportGenerator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailReportGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            ServiceCollection serviceCollection = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
                .AddJsonFile("appsettings.json")
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);

            string host = configuration["host"]!;
            int port = int.Parse(configuration["port"]!);
            string email = Environment.GetEnvironmentVariable("EMAIL")!;
            string password = Environment.GetEnvironmentVariable("PASSWORD")!;

            serviceCollection.AddSingleton<IEmailService>(
                new EmailService(host, port, email, password)
            );

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var emailService = serviceProvider.GetService<IEmailService>();
            
            await emailService.Connect();

            var messages = emailService.GetEmailsBySubjectAndSender("Sales Report","jaquelinesantosbraga@gmail.com");
            foreach (var message in messages)
            {
                Console.WriteLine(message.Subject);
            }
        }
    }
}

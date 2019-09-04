﻿namespace Jambo.Consumer.Infrastructure
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;

    class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("autofac.json")
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            IServiceCollection serviceCollection = new ServiceCollection();

            Startup startup = new Startup(configuration);
            startup.ConfigureServices(serviceCollection);
            startup.Run();
        }
    }
}

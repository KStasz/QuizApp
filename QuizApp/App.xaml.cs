using DataAccess.Repositories;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizApp.AdminPages;
using QuizApp.AdminPages.Questions;
using QuizApp.AdminPages.Users;
using QuizApp.UserWindows;
using QuizApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private readonly IHost _host;
        public static IServiceProvider ServiceProvider { get; private set;  }

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.AddJsonFile("appsettings.json", optional: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        ConfigureServices(context.Configuration, services);
                    })
                    .Build();
            ServiceProvider = _host.Services;
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddTransient<ISqlDataAccess, SqlDataAccessRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IQuestionsRepository, QuestionsRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IResultsRepository, ResultRepository>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<NewGameConfig>();
            services.AddSingleton<QuestionsViewModel>();
            services.AddTransient<QuestionsSettings>();
            services.AddTransient<GameViewModel>();
            services.AddTransient<GameWindow>();
            services.AddTransient<UserResultWindow>();
            services.AddSingleton<UserViewModel>();
            services.AddTransient<UsersPage>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
            base.OnExit(e);
        }
    }
}

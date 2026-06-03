using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using TodoList.ViewModel;

namespace TodoList
{
    public partial class App : Application
    {
        // アプリケーション全体で使うサービスプロバイダー（コンテナ）
        public static IServiceProvider ServiceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            // コンテナから MainWindow を取得
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<TodolistRepository>();
            services.AddTransient<TodoListService>();
            services.AddTransient<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }
    }
}

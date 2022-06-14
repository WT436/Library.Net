using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WriteLogExtention.Application;

namespace ServerAutoRuntimesBasic.Service
{
    public class Coordinator
    {
        private ILog4NetManager _logger;
        private readonly string Key = String.Empty;
        private readonly int Sleep = 0;

        private Thread Thread_One;

        public Coordinator()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                               .SetBasePath(Directory.GetCurrentDirectory())
                                               .AddJsonFile("appsettings.json")
                                               .Build();

            Key = configuration.GetValue<string>("BankInfo:SecrecyKey");

            StartThreads();
        }

        static ServiceProvider SetupStartUp()
        {
            //setup our DI
            return new ServiceCollection()
                  .AddSingleton<ILog4NetManager, Log4NetManager>()
                  .BuildServiceProvider();
        }

        public async void StartThreads()
        {
            // cấu hình DI
            ServiceProvider serviceProvider = SetupStartUp();
            _logger = serviceProvider.GetService<ILog4NetManager>();

            try
            {
                _logger.LogInformation("Khởi động Thread");
                // phân bổ nguồn lực thread
                Thread_One = new Thread(TaskProcessThread);
                Thread_One.Start();
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, exception: ex);
            }
        }

        async void TaskProcessThread()
        {
            _logger.LogInformation("Thread one đang chạy...!");

            while (true)
            {
                try
                {
                    await ServiceSpamRoot.Processor();
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: "Thread One Lỗi : " + ex.Message, exception: ex);
                }
                finally
                {
                    Thread.Sleep(Sleep);
                }
            }
        }

    }
}

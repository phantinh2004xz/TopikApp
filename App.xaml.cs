using System.Windows;
using Microsoft.Extensions.Configuration; // Thêm dòng này
using System.IO; // Thêm dòng này

namespace TopikApp
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Cấu hình đọc file appsettings.json
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Sửa từ System.IO.Directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            base.OnStartup(e);
        }
    }
}
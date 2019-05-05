using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Meblex.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
//                .UseKestrel()
//                .UseIISIntegration()
//                .UseUrls("http://+:5555")
                .UseStartup<Startup>();
    }
}

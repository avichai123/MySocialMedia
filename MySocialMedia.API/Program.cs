
namespace MySocialMedia.API
{
    public class Program
    {
        //private const string URL = "https://localhost:7089";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                //webBuilder.UseUrls(URL);
            });
        }

    }
}

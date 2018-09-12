using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Easy.Commerce.Services.Web.Api
{
    /// <summary>
    /// Startup program to run APIs.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main configuration.
        /// </summary>
        /// <param name="args">Settings parameters.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Build web host.
        /// </summary>
        /// <param name="args">Settings parameters.</param>
        /// <returns>Web host.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MuskyMulisha
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if (!DEBUG)
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
#else
            var currentDirectory = Directory.GetCurrentDirectory();
            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 80);
                    options.Listen(IPAddress.Any, 443, listenOptions =>
                    {
                        listenOptions.UseHttps("www.muskymulisha.com.pfx", Environment.GetEnvironmentVariable("MYSECRET_PASS"));
                    });
                })
                .UseContentRoot(currentDirectory)
                .UseUrls("http://*:80", "https://*:443")
                .UseSetting("https_port", "443")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
#endif
            host.Run();
        }
    }
}

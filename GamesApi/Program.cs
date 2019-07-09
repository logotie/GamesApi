using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GamesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //The host is responsible for start up and the lifetime of the web app
            CreateWebHostBuilder(args).Build().Run();
        }

        //Builds the host with certain config.
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

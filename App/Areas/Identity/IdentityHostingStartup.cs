﻿[assembly: HostingStartup(typeof(ALIMS.Areas.Identity.IdentityHostingStartup))]
namespace ALIMS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
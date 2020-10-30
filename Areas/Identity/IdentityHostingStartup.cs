using System.Collections.Generic;
using aspcore_watchshop.Hepler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

[assembly : HostingStartup (typeof (aspcore_watchshop.Areas.Identity.IdentityHostingStartup))]
namespace aspcore_watchshop.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {

        public void Configure (IWebHostBuilder builder) {
            builder.ConfigureServices ((context, services) => { });
        }
    }
}
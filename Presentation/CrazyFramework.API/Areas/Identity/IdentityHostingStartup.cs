using System;
using CrazyFramework.Infrastructure.AspNetIdentityRepos;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CrazyFramework.API.Areas.Identity.IdentityHostingStartup))]
namespace CrazyFramework.API.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
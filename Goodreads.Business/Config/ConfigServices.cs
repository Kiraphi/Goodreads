using Business.Data.Interfaces;
using Business.Data.Repositories;
using Goodreads.Business.Business;
using Goodreads.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Goodreads.Business.Config;

public static class ConfigServices
{
    public static void AddCustomServices(this IServiceCollection service)
    {
        service.AddScoped<IBookBusiness, BookBusiness>();
        service.AddScoped<IBookRepo, BookRepo>();
    }
}
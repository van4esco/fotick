using Fotick.Api.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotick.Api.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IImageRepository, ImageRepository>();
            service.AddTransient<ITagRepository, TagRepository>();
            return service;
        }
    }
}

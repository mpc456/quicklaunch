using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Services;
using QuickLaunch.Data.Access.File.Interface;
using QuickLaunch.Data.Access.File.Strategies.Ini;
using QuickLaunch.Data.Access.File.Strategies.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLaunch.Data.Access.File.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFileDataAccess(this IServiceCollection services, [NotNull] IConfiguration config)
        {
            var fileDataAccesConfig = config.GetSection(nameof(DataAccessFileConfig)).Get<DataAccessFileConfig>();
            services.AddFileDataAccess(fileDataAccesConfig);
        }

        public static void AddFileDataAccess(this IServiceCollection services, [NotNull] DataAccessFileConfig config)
        {
            services.AddSingleton<IDataAccessFileConfig>(sp => config);

            services.AddTransient<IDataAccess, FileDataAcessFacade>();

            services.AddTransient<IFileDataAccess, JsonFileDataAccess>();
            services.AddTransient<IFileDataAccess, QrsFileDataAccess>();
        }
    }
}

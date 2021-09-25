using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using QuickLaunch.Data.Access.Abstractions.Interfaces.Services;
using QuickLaunch.Operations.Abstractions.Interfaces.Services;
using QuickLaunch.Operations.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickLaunch.Data.Access.File.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOperationsLibrary(this IServiceCollection services)
        {
            services.AddTransient<IProcessRunner, ProcessRunner>();
        }
    }
}

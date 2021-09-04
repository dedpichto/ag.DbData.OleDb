using ag.DbData.Abstraction.Services;
using ag.DbData.OleDb.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace ag.DbData.OleDb.Extensions
{
    /// <summary>
    /// Represents <see cref="ag.DbData.OleDb"/> extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Appends the registration of <see cref="OleDbDataFactory"/> and <see cref="OleDbDataObject"/> services to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAgOleDb(this IServiceCollection services)
        {
            services.TryAddTransient<IDbDataStringProvider, DbDataStringProvider>();
            services.AddSingleton<IOleDbDbDataFactory, OleDbDataFactory>();
            services.AddTransient<OleDbDataObject>();
            return services;
        }

        /// <summary>
        /// Appends the registration of <see cref="OleDbDataFactory"/> and <see cref="OleDbDataObject"/> services to <see cref="IServiceCollection"/> and registers a configuration instance.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configurationSection">The <see cref="IConfigurationSection"/> being bound.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAgOleDb(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.AddAgOleDb();
            services.Configure<OleDbDataSettings>(opts =>
            {
                opts.AllowExceptionLogging = configurationSection.GetValue<bool>("AllowExceptionLogging");
                opts.ConnectionString = configurationSection.GetValue<string>("ConnectionString");
            });
            return services;
        }

        /// <summary>
        /// Appends the registration of <see cref="OleDbDataFactory"/> and <see cref="OleDbDataObject"/> services to <see cref="IServiceCollection"/> and configures the options.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAgOleDb(this IServiceCollection services,
            Action<OleDbDataSettings> configureOptions)
        {
            services.AddAgOleDb();
            services.Configure(configureOptions);
            return services;
        }
    }
}

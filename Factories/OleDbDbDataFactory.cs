using ag.DbData.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.OleDb;

namespace ag.DbData.OleDb.Factories
{
    /// <summary>
    /// Represents OleDbDataFactory object.
    /// </summary>
    internal class OleDbDataFactory: IOleDbDbDataFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates object of type <see cref="OleDbDataObject"/>.
        /// </summary>
        /// <returns><see cref="OleDbDataObject"/> implementation of <see cref="IDbDataObject"/> interface.</returns>
        public IDbDataObject Create()
        {
            var dbObject = _serviceProvider.GetService<OleDbDataObject>();
            return dbObject;
        }

        /// <summary>
        /// Creates object of type <see cref="OleDbDataObject"/>.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns><see cref="OleDbDataObject"/> implementation of <see cref="IDbDataObject"/> interface.</returns>
        public IDbDataObject Create(string connectionString)
        {
            var dbObject = _serviceProvider.GetService<OleDbDataObject>();
            dbObject.Connection = new OleDbConnection(connectionString);
            return dbObject;
        }

        /// <summary>
        /// Creates object of type <see cref="OleDbDataObject"/>.
        /// </summary>
        /// <param name="defaultCommandTimeOut">Replaces default coommand timeout of provider</param>
        /// <returns></returns>
        public IDbDataObject Create(int defaultCommandTimeOut)
        {
            var dbObject = _serviceProvider.GetService<OleDbDataObject>();
            dbObject.DefaultCommandTimeout = defaultCommandTimeOut;
            return dbObject;
        }

        /// <summary>
        /// Creates new OleDbDataFactory object.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/>.</param>
        public OleDbDataFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}

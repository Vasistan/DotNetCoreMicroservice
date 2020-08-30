using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApplicationService.Configuration;

namespace ToDoApplicationService.Database
{
    public interface IMongoDbConnection
    {
        MongoClient Connection { get; }
        ConnectionConfiguration ConnectionConfiguration { get; set; }
        IMongoDatabase Database { get; set; }
    }

    public class MongoDbConnection : IMongoDbConnection
    {
        public ConnectionConfiguration ConnectionConfiguration { get; set; }

        public MongoDbConnection(ConnectionConfiguration connectionConfiguration)
        {
            this.ConnectionConfiguration = connectionConfiguration;
            Setup(this.ConnectionConfiguration);

        }

        protected virtual void Setup(ConnectionConfiguration connectionSettings)
        {
            if (connectionSettings.ConnectionString.IndexOf("{0}", StringComparison.OrdinalIgnoreCase) > -1)
            {
                connectionSettings.ConnectionString = string.Format(connectionSettings.ConnectionString, connectionSettings.Database);
            }

            Connection = new MongoClient(connectionSettings.ConnectionString);

            Database = Connection.GetDatabase(connectionSettings.Database);
        }

        public MongoClient Connection { get; protected set; }

        public IMongoDatabase Database { get; set; }
    }
}

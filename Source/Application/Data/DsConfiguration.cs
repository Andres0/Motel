using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data
{
    public class DsConfiguration : DbConfiguration
    {
        public DsConfiguration()
        {
            //this.SetManifestTokenResolver(new MyManifestTokenResolver());
           this.SetDatabaseInitializer<DsContext>(new DropCreateDatabaseAlways<DsContext>()); // for recreate database (comment this line when you need recreate the database)
        }






        public class MyManifestTokenResolver : IManifestTokenResolver
        {
            private readonly IManifestTokenResolver _defaultResolver = new DefaultManifestTokenResolver();

            public string ResolveManifestToken(DbConnection connection)
            {
                var sqlConnection = connection as SqlConnection;
                if (sqlConnection != null && sqlConnection.DataSource == @".\SQLEXPRESS")
                {
                    return "2012";
                }
                else
                {
                    return _defaultResolver.ResolveManifestToken(connection);
                }
            }
        }
    }
}

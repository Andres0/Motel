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
            this.SetDatabaseInitializer<DsContext>(new DropCreateDatabaseIfModelChanges<DsContext>()); // for recreate database (comment this line when you need recreate the database)
            //this.SetDatabaseInitializer<DsContext>(new DropCreateDatabaseAlways<DsContext>()); // for recreate database (comment this line when you need recreate the database)

            //Database.SetInitializer<SchoolDBContext>(new CreateDatabaseIfNotExists<SchoolDBContext>()); este es el por defecto
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
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

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLTestApp
{
    class DBTools
    {
        public static SqlConnection GetDBConnection(string datasource, string database, string username, string password)
        {
            return new SqlConnection(
                string.Format(
                    @"Server={0};Database={1};User Id={2};Password={3}",
                    datasource, database, username, password
                )
            );
        }
    }
}

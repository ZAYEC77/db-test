using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BD.Schemas
{
    abstract class SchemaType
    {
        public MySqlCommand CreateCommand()
        {
            return App.Instance.Connection.CreateCommand();
        }

        public void ExecuteNonQuery(String sql)
        {
            var  cmd = CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void ClearTable(String tableName)
        {
            ExecuteNonQuery(String.Format("DELETE FROM `{0}`;", tableName));
        }
        
        public abstract void GenerateData(int amount);

        public abstract void CreateTables();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BD.Schemas
{
    class OneTableSchema: SchemaType
    {
        public override void CreateTables()
        {
            ExecuteNonQuery("DROP TABLE IF EXISTS `c1_students`");

            ExecuteNonQuery("CREATE TABLE `c1_students` (  `id` int(11) NOT NULL AUTO_INCREMENT,  `name` varchar(63) NOT NULL, `mark` int(11) NOT NULL, `group_name` varchar(63) NOT NULL, `curator` varchar(63) NOT NULL,  PRIMARY KEY (`id`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;");
        }

        public override void GenerateData(int amount)
        {
            var r = new Random();
            ClearTable("c1_students");
            for (int i = 0; i < amount; i++)
            {
                var sql = String.Format("INSERT INTO `c1_students` (name, mark, group_name, curator) VALUES('{0}','{1}','{2}','{3}')", "Name" + r.Next(1, 10), r.Next(1, 6), "Group_" + r.Next(0, 6), "Curator_" + r.Next(0, 6));
                ExecuteNonQuery(sql);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Schemas
{
    class TwoTablesSchema: SchemaType
    {
        public override void CreateTables()
        {
            ExecuteNonQuery("DROP TABLE IF EXISTS `c2_marks`");

            ExecuteNonQuery("DROP TABLE IF EXISTS `c2_students`");

            ExecuteNonQuery("CREATE TABLE `c2_students` (`id` int(11) NOT NULL AUTO_INCREMENT,  `name` varchar(63) NOT NULL, `name_group` varchar(63) NOT NULL, `curator` varchar(63) NOT NULL, PRIMARY KEY (`id`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;");

            ExecuteNonQuery("CREATE TABLE `c2_marks` (`id` int(11) NOT NULL AUTO_INCREMENT, `mark` int(11) NOT NULL,  `student_id` int(11) NOT NULL,  PRIMARY KEY (`id`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;");
       
        }

        public override void GenerateData(int amount)
        {
            String sql;
            var table1 = "c2_students";
            var table2 = "c2_marks";

       
            ClearTable(table2);
            ClearTable(table1);

            int groupAmount = amount / 100;

            int studentsInGroupAmount = amount / groupAmount;
            var mark = new Random();
            for (var j = 1; j <= groupAmount; j++)
            {
                var groupId = j;
                var groupName = "Group " + j;
                var curatorName = "Curator " + j;
                for (var i = 0; i < 100; i++)
                {
                    var name = "Name_" + i;
                    sql = String.Format("INSERT INTO {0}  SET name = '{1}', name_group = '{3}', curator = '{4}';", table1, name, groupId, groupName, curatorName);
                    var cmd = CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    var studentId = cmd.LastInsertedId;
                    cmd.Dispose();

                    sql = String.Format("INSERT INTO {0}  SET mark = {1}, student_id = {2};", table2, mark.Next(1, 6), studentId);
                    ExecuteNonQuery(sql);
                }
            }
        }
    }
}

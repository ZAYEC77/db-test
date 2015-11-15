using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BD.Schemas
{
    class ThreeTablesSchema: SchemaType
    {
        public override void CreateTables()
        {
            ExecuteNonQuery("DROP TABLE IF EXISTS `c3_marks`");

            ExecuteNonQuery("DROP TABLE IF EXISTS `c3_students`");

            ExecuteNonQuery("DROP TABLE IF EXISTS `c3_groups`");

            ExecuteNonQuery("CREATE TABLE `c3_groups` (`id` int(11) NOT NULL AUTO_INCREMENT, `name` varchar(63) NOT NULL, `curator` varchar(63) NOT NULL,  PRIMARY KEY (`id`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;");

            ExecuteNonQuery("CREATE TABLE `c3_students` (  `id` int(11) NOT NULL AUTO_INCREMENT,  `name` varchar(63) NOT NULL,  `group_id` int(11) NOT NULL,  PRIMARY KEY (`id`),  KEY `fk_students_1_idx` (`group_id`),  CONSTRAINT `fk_students_1` FOREIGN KEY (`group_id`) REFERENCES `c3_groups` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;");

            ExecuteNonQuery("CREATE TABLE `c3_marks` (`id` int(11) NOT NULL AUTO_INCREMENT, `mark` int(11) NOT NULL,  `student_id` int(11) NOT NULL,  PRIMARY KEY (`id`),  KEY `fk_marks_1_idx` (`student_id`),  CONSTRAINT `fk_marks_1` FOREIGN KEY (`student_id`) REFERENCES `c3_students` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;");
        }

        public override void GenerateData(int amount)
        {
            String sql;
            var table1 = "c3_groups";
            var table2 = "c3_students";
            var table3 = "c3_marks";

            ClearTable(table3);
            ClearTable(table2);
            ClearTable(table1);

            int groupAmount = amount/100;
            
            int studentsInGroupAmount = amount/groupAmount;
            var mark = new Random();
            for (var j = 1; j <= groupAmount; j++){
                var groupId = j;
                var groupName = "Group " + j;
                var curatorName = "Curator " + j;
                sql = String.Format("INSERT INTO {0}  SET id = {1}, name = '{2}', curator = '{3}';", table1, groupId, groupName, curatorName);
                ExecuteNonQuery(sql);
                for (var i = 0; i < 100; i++) {
                    var name = "Name_"+i;
                    sql = String.Format("INSERT INTO {0}  SET name = '{1}', group_id = {2};",table2,name,groupId);
                    var cmd = CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    var studentId = cmd.LastInsertedId;
                    cmd.Dispose();
                   
                    sql = String.Format("INSERT INTO {0}  SET mark = {1}, student_id = {2};",table3, mark.Next(1, 6), studentId);
                    ExecuteNonQuery(sql);
                }
            }
        }
    }
}

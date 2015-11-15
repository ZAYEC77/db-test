using System;
using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BD.Schemas;


namespace BD
{
    public partial class Form1 : Form
    {
        MySqlConnection connection;
        string server;
        string database;
        string uid;
        string password;
        public string connectionString;

        public Form1()
        {
            InitializeComponent();
            server = "localhost";
            database = "TEST";
            uid = "root";
            password = "1";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";charset=cp1251;";
            connection = new MySqlConnection(connectionString);

        }
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                string query = "SET NAMES 'cp1251'";
                string query1 = "SET CHARACTER SET 'cp1251'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlCommand cmd1 = new MySqlCommand(query1, connection);
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        System.Windows.Forms.MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        System.Windows.Forms.MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            double sT1 = 0;
            double sT2 = 0;


            OpenConnection();
            string CommandText1 = textBox2.Text;
            string CommandText2 = textBox3.Text;
            int q = int.Parse(textBox4.Text);
            textBox1.AppendText("Кiлькiсть запитiв: " + q + "\n");
            textBox1.AppendText("Тест\tT1\tT2\tT2-T1\t%\n");
            for (int h = 1; h <= q; h++)
            {
                Stopwatch stopWatch1 = new Stopwatch();

                stopWatch1.Start();
              
                    MySqlCommand myCommand1 = new MySqlCommand(CommandText1, connection);
                    MySqlDataReader reader1 = myCommand1.ExecuteReader();
                    //while (reader1.Read())
                    //{
                    //    textBox1.AppendText(String.Format("{0}, {1}\n", reader1[0], reader1[1]));
                    //}
                    reader1.Dispose();
                    myCommand1.Dispose();
                

                stopWatch1.Stop();

                TimeSpan ts1 = stopWatch1.Elapsed;
                sT1 += ts1.TotalMilliseconds;
                textBox1.AppendText(h + "\t");
                textBox1.AppendText(Math.Round(ts1.TotalMilliseconds, 0) + "\t");


                Stopwatch stopWatch2 = new Stopwatch();

                stopWatch2.Start();
              
                    MySqlCommand myCommand2 = new MySqlCommand(CommandText2, connection);
                    MySqlDataReader reader2 = myCommand2.ExecuteReader();
                    //while (reader2.Read())
                    //{
                    //    textBox1.AppendText(String.Format("{0}, {1}\n", reader2[0], reader2[1]));
                    //}
                    reader2.Dispose();
                    myCommand2.Dispose();
               

                stopWatch2.Stop();

                TimeSpan ts2 = stopWatch2.Elapsed;
                sT2 += ts2.TotalMilliseconds;


                textBox1.AppendText(Math.Round(ts2.TotalMilliseconds, 0) + "\t");
                textBox1.AppendText(Math.Round((ts2.TotalMilliseconds - ts1.TotalMilliseconds), 0) + "\t");
                textBox1.AppendText(Math.Round((ts2.TotalMilliseconds - ts1.TotalMilliseconds) / (ts1.TotalMilliseconds / 100), 2) + "\n");


            }
            textBox1.AppendText("сер \t");
            textBox1.AppendText(Math.Round(sT1 / q) + "\t");
            textBox1.AppendText(Math.Round(sT2 / q) + "\t");
            textBox1.AppendText(Math.Round((sT2 - sT1) / q) + "\t");
            textBox1.AppendText(Math.Round((sT2 - sT1) / (sT1 / 100), 2) + "\n");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //OpenConnection();

            ////   INSERT INTO studentu1000(id, grup, number, name, grade) VALUES("+(h+1)+ ", 'PI-12-1', 13, 'Скоропадський', 4);
            //MySqlCommand myCommand1;
            //MySqlDataReader reader;
            //for (int h = 130; h <= 990; h+=10)
            //{
            //    string CommandText1 = "INSERT INTO studentu1000 (id,grup,number, name, grade) VALUES(" + (h + 1) + ", 'PI-12-1', 13, 'Скоропадський', 4); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 2) + ", 'PI-14-1', 20, 'Мазепа', 5); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 3) + ", 'PI-14-1', 20, 'Шевченко', 4); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 4) + ", 'PI-12-1', 13, 'Українка', 5); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 5) + ", 'PI-12-1', 13, 'Франко', 5); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 6) + ", 'PI-14-1', 20, 'Сірко', 3); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 7) + ", 'PI-12-1', 13, 'Вишневецький', 4); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 8) + ", 'PI-14-1', 20, 'Лісовський', 5); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 9) + ", 'PI-14-1', 20, 'Кириленко', 2); INSERT INTO studentu1000(id, grup, number, name, grade) VALUES(" + (h + 10) + ", 'PI-12-1', 13, 'Хмельницький', 5); ";
            //     myCommand1 = new MySqlCommand(CommandText1, connection);
            //     myCommand1.ExecuteNonQuery();


            //    myCommand1.Dispose();
            //}




            //connection.Close();
            //textBox1.AppendText("STOP");


            OpenConnection();

            //   INSERT INTO studentu1000(id, grup, number, name, grade) VALUES("+(h+1)+ ", 'PI-12-1', 13, 'Скоропадський', 4);
            MySqlCommand myCommand1;
            MySqlDataReader reader;
            for (int h = 10; h <= 99990; h += 10)
            {
                //  string CommandText1 = "INSERT INTO studentu100001 (id,grup, name, grade) VALUES(" + (h + 1) + ", 1, 'Скоропадський', 4); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 2) + ", 2, 'Мазепа', 5); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 3) + ", 2,  'Шевченко', 4); INSERT INTO studentu100001(id, grup, name, grade) VALUES(" + (h + 4) + ", 1, 'Українка', 5); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 5) + ", 1,  'Франко', 5); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 6) + ", 2, 'Сірко', 3); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 7) + ", 1, 'Вишневецький', 4); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 8) + ", 2,  'Лісовський', 5); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 9) + ", 2, 'Кириленко', 2); INSERT INTO studentu100001(id, grup,  name, grade) VALUES(" + (h + 10) + ", 1, 'Хмельницький', 5); ";

                string CommandText1 = "INSERT INTO studentu100000 (id,grup,number, name, grade) VALUES(" + (h + 1) + ", 'PI-12-1', 13, 'Скоропадський', 4); INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 2) + ", 'PI-14-1', 20, 'Мазепа', 5);  INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 3) + ", 'PI-14-1', 20, 'Шевченко', 4);  INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 4) + ", 'PI-12-1', 13, 'Українка', 5); INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 5) + ", 'PI-12-1', 13, 'Франко', 5); INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 6) + ", 'PI-14-1', 20, 'Сірко', 3);  INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 7) + ", 'PI-12-1', 13, 'Вишневецький', 4); INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 8) + ", 'PI-14-1', 20, 'Лісовський', 5); INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 9) + ", 'PI-14-1', 20, 'Кириленко', 2); INSERT INTO studentu100000(id, grup, number, name, grade) VALUES(" + (h + 10) + ", 'PI-12-1', 13, 'Хмельницький', 5); ";
                myCommand1 = new MySqlCommand(CommandText1, connection);
                myCommand1.ExecuteNonQuery();
                myCommand1.Dispose();
            }

            connection.Close();
            textBox1.AppendText("STOP");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var schema = new OneTableSchema();
                schema.CreateTables();
                MessageBox.Show("Ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void налаштуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Settings()).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var schema = new OneTableSchema();
                schema.GenerateData(1000);
                MessageBox.Show("Ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var connection = App.Instance.Connection;

                double sT1 = 0;
                double sT2 = 0;

                string CommandText1 = textBox2.Text;
                string CommandText2 = textBox3.Text;
                int q = int.Parse(textBox4.Text);
                textBox1.AppendText("Кiлькiсть запитiв: " + q + "\n");
                textBox1.AppendText("Тест\tT1\tT2\tT2-T1\t%\n");
                for (int h = 1; h <= q; h++)
                {
                    Stopwatch stopWatch1 = new Stopwatch();

                    stopWatch1.Start();

                    MySqlCommand myCommand1 = new MySqlCommand(CommandText1, connection);
                    MySqlDataReader reader1 = myCommand1.ExecuteReader();


                    stopWatch1.Stop();

                    reader1.Dispose();
                    myCommand1.Dispose();


                    TimeSpan ts1 = stopWatch1.Elapsed;
                    sT1 += ts1.TotalMilliseconds;
                 //   textBox1.AppendText(h + "\t");
                   // textBox1.AppendText(Math.Round(ts1.TotalMilliseconds, 0) + "\t");


                    Stopwatch stopWatch2 = new Stopwatch();

                    stopWatch2.Start();

                    MySqlCommand myCommand2 = new MySqlCommand(CommandText2, connection);
                    MySqlDataReader reader2 = myCommand2.ExecuteReader();

                    stopWatch2.Stop();

                    reader2.Dispose();
                    myCommand2.Dispose();

                    TimeSpan ts2 = stopWatch2.Elapsed;
                    sT2 += ts2.TotalMilliseconds;


                  //  textBox1.AppendText(Math.Round(ts2.TotalMilliseconds, 0) + "\t");
                  //  textBox1.AppendText(Math.Round((ts2.TotalMilliseconds - ts1.TotalMilliseconds), 0) + "\t");
                  //  textBox1.AppendText(Math.Round((ts2.TotalMilliseconds - ts1.TotalMilliseconds) / (ts1.TotalMilliseconds / 100), 2) + "\n");


                }
                textBox1.AppendText("сер \t");
                textBox1.AppendText(sT1 / q + "\t");
                textBox1.AppendText(sT2 / q + "\t");
                textBox1.AppendText(sT2 - sT1 / q + "\t");
                textBox1.AppendText(Math.Round((sT2 - sT1) / (sT1 / 100), 2) + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var schema = new ThreeTablesSchema();
                schema.GenerateData(1000);
                MessageBox.Show("Ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var schema = new ThreeTablesSchema();
                schema.CreateTables();
                MessageBox.Show("Ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

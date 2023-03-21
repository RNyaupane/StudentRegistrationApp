using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StudentRegistrationApp
{
    public partial class Form1 : Form
    {
        static string constr = "SERVER = localhost;" +
                                "DATABASE = school;" +
                                "USERNAME = root;" +
                                "PASSWORD = '';";
        MySqlConnection conn = new MySqlConnection(constr);
        int rollno = 0;
        public Form1()
        {
            InitializeComponent();

            DisplayData();
        }
        public void DisplayData()
        {
            conn.Open();
            string query = "SELECT * FROM student";
            MySqlDataAdapter adr = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adr.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                conn.Open();
                string name = textBox2.Text;
                string address = textBox3.Text;
                string phone = textBox4.Text;
                string query = "UPDATE student SET name ='" + name + "' , address = '" + address + "' WHERE rollno = " + rollno + "";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int rows = cmd.ExecuteNonQuery();
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
                if (rows > 0)
                {
                    MessageBox.Show("Updated Successfully");
                }
                else
                {
                    MessageBox.Show("Failed to Update");
                }
                conn.Close();
                DisplayData();
            }
            else
            {
                MessageBox.Show("Data not Filled");
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text!="" && textBox3.Text != "" && textBox4.Text != "" ) {
                /* COnnection lai 4 step ma complete garxam
               Step 1:- Connection String
               Step 2:- MySQL connection object
               Step3 :- Open the connection
               Step 4:- Close the connection */

                conn.Open();

                //Insert INTO DATABASE
                //step 1: creating sql string
                //Create the mysql command object
                //ExecuteNonQuery()

                int rollno = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                string address = textBox3.Text;
                string phone = textBox4.Text;
                string query = "INSERT INTO student VALUES("+rollno+",'"+name+"','"+address+"','"+phone+"')" ;
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int rows = cmd.ExecuteNonQuery();//Return a integer value(4 ota data vako case ma 4 return garxa)
                if(rows > 0)
                {
                    MessageBox.Show("Data Inserted Successfully");
                }
                else
                {
                    MessageBox.Show("Failed to insert Data");
                }

                conn.Close();
                DisplayData();
            }
            else
            {
                MessageBox.Show("Please fill the form!!!");
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rollno = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string address = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string phone = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            
            textBox1.Text = rollno.ToString();
            textBox2.Text = name;
            textBox3.Text = address;
            textBox4.Text = phone;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(rollno != 0)
            {
                conn.Open();
                string query = "DELETE FROM student WHERE rollno = " + rollno + "";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int row = cmd.ExecuteNonQuery();
                if(row > 0)
                {
                    MessageBox.Show("Delete SUccessfully");
                }
                else
                {
                    MessageBox.Show("Failed to delete data");
                }
                conn.Close();
                DisplayData();
            }
        }
    }
}

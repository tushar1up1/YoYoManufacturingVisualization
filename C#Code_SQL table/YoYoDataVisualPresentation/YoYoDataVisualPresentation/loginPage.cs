using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting.Utilities;
using System.Configuration;

namespace YoYoDataVisualPresentation
{
    public partial class loginPage : Form
    {
        string connString;
        ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
        public loginPage()
        {
            InitializeComponent();
            foreach (ConnectionStringSettings cs in settings)
            {
                connString = cs.ConnectionString;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                object ret;

                if ((!String.IsNullOrWhiteSpace(textBox1.Text)) && (!String.IsNullOrWhiteSpace(textBox2.Text)))
                {
                    //No of yoyo inspection at Station_1
                    comm.CommandText = "select count(*) from userTable where (username='" + textBox1.Text + "' AND userpassword='" + textBox2.Text + "')";
                    ret = comm.ExecuteScalar();
                    if (Convert.ToInt32(ret) == 1)
                    {
                        comm.CommandText = "select usertype from userTable where (username='" + textBox1.Text + "')";
                        ret = comm.ExecuteScalar();
                        if (ret.ToString().Trim() == "admin")
                        {
                            MyStaticClass.UserType = "admin";
                        }
                        else if (ret.ToString().Trim() == "normal")
                        {
                            MyStaticClass.UserType = "normal";
                        }
                        this.Hide();
                        Form1 fm = new Form1();
                        fm.Show();
                    }
                    else
                    {
                        MessageBox.Show("No user in the database OR Username/password mismatch");
                    }
                }
                else
                {
                    MessageBox.Show("Username/password cannot be NULL/ Empty/ Whitespace");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
      
    }
}

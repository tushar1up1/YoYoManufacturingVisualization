using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SampleQueueReader
{
    public partial class frmMain : Form
    {
        MessageQueue msmq = new MessageQueue();
        Boolean bRead = false;
        String queueName = "\\private$\\yoyo";
//        String yoyoConn = @"Data Source=TUSHAR-PC\MSSQL2014;Initial Catalog=YoYo;User ID=sa;Password=***********";
        String yoyoConn = @"Data Source=TUSHAR-PC\MSSQL2014;Initial Catalog=YoYo;Integrated Security=True";
//        SqlConnection conn = new SqlConnection(@"Data Source=TUSHAR-PC\MSSQL2014;Initial Catalog=YoYo;User ID=sa;Password=***********");
//        SqlConnection conn = new SqlConnection(@"Data Source=TUSHAR-PC\MSSQL2014;Initial Catalog=YoYo;Integrated Security=True");
        
        SqlCommand cmdData = new SqlCommand();
        SqlCommand cmdSchl = new SqlCommand();
        DataAccessLayer dal = new DataAccessLayer();

        SqlConnection connData;
        SqlConnection connSchl;
       
        public frmMain()
        {
            InitializeComponent();
            msmq.Formatter = new ActiveXMessageFormatter();
            msmq.MessageReadPropertyFilter.LookupId = true;
            msmq.SynchronizingObject = this;
            msmq.ReceiveCompleted += new ReceiveCompletedEventHandler(msmq_ReceiveCompleted);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstQueueData.Items.Clear();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtQueueServer.Text = System.Windows.Forms.SystemInformation.ComputerName;
            IsRunning(false);

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                connData = new SqlConnection(yoyoConn);
                connSchl = new SqlConnection(yoyoConn);
                cmdData.Connection = connData;
                cmdSchl.Connection = connSchl;
                connData.Open();
                connSchl.Open();
            }
            catch (Exception ex)
            {

                connData.Close();
                connSchl.Close();
                MessageBox.Show(ex.ToString());
            }

            if (txtQueueServer.Text == "")
            {
                MessageBox.Show("Message Queue Server required");
            }
            else
            {
                msmq.Path = "Formatname:Direct=os:" + txtQueueServer.Text + queueName;
                bRead = true;
                msmq.BeginReceive();
                IsRunning(true);
                ///To ASK: What seperator to use to seperate message as field
            }

        }

        void msmq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
           try
            {
                lstQueueData.Items.Insert(0, e.Message.Body.ToString());
                string[] field = e.Message.Body.ToString().Split(',');
                dal.workArea = field[0];
                dal.keyData = field[1];
                dal.lineNo = field[2];
                dal.stationName = field[3];
                dal.processName = field[4];
                dal.dateTime = Convert.ToDateTime(field[5]);

                cmdSchl.CommandText = "Select ALL SKU,scheduleID, SKUBegindate, SKUEnddate from yoyoSchedule where '" + dal.dateTime + "' between SKUBegindate AND SKUEnddate";

                SqlDataReader rdr;
                rdr = cmdSchl.ExecuteReader();
                while (rdr.Read())
                {
                    dal.scheduleID = rdr[1].ToString();
                    cmdData.CommandText = "Insert into yoyoData (scheduleID, workarea, keydata, line_no, station_name, process_name, process_date) values ('"+ dal.scheduleID + "', '" + dal.workArea + "', '" + dal.keyData + "', '" + dal.lineNo + "', '" + dal.stationName + "', '" + dal.processName + "', '" + dal.dateTime + "')";
                    int recCount = (int)cmdData.ExecuteNonQuery();
                }
                rdr.Close();
                msmq.EndReceive(e.AsyncResult);
                if (chkCount.Checked)
                {
                    txtRemaining.Text = GetMessageCount(msmq).ToString();
                    Application.DoEvents();
                }
                System.Threading.Thread.Sleep(100);
                if (bRead)
                {
                    msmq.BeginReceive();
                }
            }
           catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
/*
            try
            {
                cmdData.CommandText = "Insert into yoyoNewData (workarea, keydata, line_no, station_name, process_name, process_date) values ('" + dal.workArea + "', '" + dal.keyData + "', '" + dal.lineNo + "', '" + dal.stationName + "', '" + dal.processName + "', '" + dal.dateTime + "')";
                int recCount = (int)cmdData.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
*/

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (connData.State == ConnectionState.Open)
            {
                connData.Close();
                
            }
            if (connData.State == ConnectionState.Open)
            {
                connSchl.Close();
            }
            bRead = false;
            IsRunning(false);

        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            lstQueueData.Items.Clear();
        }

        private int GetMessageCount(MessageQueue m)
        {
            Int32 count = 0;
            MessageEnumerator msgEnum = m.GetMessageEnumerator2();
            while (msgEnum.MoveNext(new TimeSpan(0, 0, 0)))
            {
                count++;
            }
            return count;
        }

        private void IsRunning(Boolean state)
        {
            if (state == true)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnSingleRead.Enabled = false;
            }
            else
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnSingleRead.Enabled = true;
            }
        }

        private void btnSingleRead_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(yoyoConn);
            cmdData.Connection = conn;
                        
            if (txtQueueServer.Text == "")
            {
                MessageBox.Show("Message Queue Server required");
            }
            else
            {
                msmq.Path = "Formatname:Direct=os:" + txtQueueServer.Text + queueName;
                try
                {
                    System.Messaging.Message msg = msmq.Receive(new TimeSpan(0));
                    if (msg != null)
                    {
                        lstQueueData.Items.Insert(0, msg.Body.ToString());
                        string[] field = msg.Body.ToString().Split(',');
                        dal.workArea = field[0];
                        dal.keyData = field[1];
                        dal.lineNo = field[2];
                        dal.stationName = field[3];
                        dal.processName = field[4];
                        dal.dateTime = Convert.ToDateTime(field[5]);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "Cannot read - probably empty queue or queue non existent");
                }

                try
                {
                    cmdData.CommandText = "Insert into yoyo_sim_data (workarea, keydata, line_no, station_name, process_name, process_date) values ('" + dal.workArea + "', '" + dal.keyData + "', '" + dal.lineNo + "', '" + dal.stationName + "', '" + dal.processName + "', '" + dal.dateTime + "')";
                    conn.Open();
                    int recCount = (int)cmdData.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show(recCount.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show (ex.ToString());
                }
            }
           
        }

        private void btnPurgeQ_Click(object sender, EventArgs e)
        {
            msmq.Purge();
        }

    }
}

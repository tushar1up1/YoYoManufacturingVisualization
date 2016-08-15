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
    public partial class Form1 : Form
    {
        string connString;// = @"Data Source=TUSHAR-PC\MSSQL2014;Initial Catalog=YoYo;Integrated Security=True";
        ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
        DalScrapDefect dalScprDeft = new DalScrapDefect();
        DalScrapNoAtStation dalScrpStation = new DalScrapNoAtStation();
        DalStationStatus dalStnStatus = new DalStationStatus();

        Chart pieChart1 = new Chart();
        Chart pieChart2 = new Chart();
        Chart pieChart3 = new Chart();


        Chart scrpChtStation = new Chart();

        Chart paretoChart = new Chart();


        public Form1()
        {
            InitializeComponent();
            if (MyStaticClass.UserType == "admin")
            {

            }
            else if (MyStaticClass.UserType == "normal")
            {
                this.button1.Enabled = false;
                this.dateTimePicker1.Enabled = false;
                this.dateTimePicker2.Enabled = false;
                this.listBox3.Enabled = false;
                this.label7.Enabled = false;
                this.label8.Enabled = false;
                this.label9.Enabled = false;
                this.label10.Enabled = false;
                this.textBox1.Enabled = false;
                this.textBox2.Enabled = false;
                this.textBox3.Enabled = false;
                this.button5.Enabled = false;
            }
            foreach (ConnectionStringSettings cs in settings)
            {
                connString = cs.ConnectionString;
            }
        }
        
 
        //Adding records to yoyoSchedule table
        private void button1_Click(object sender, EventArgs e) // Add to schedule button click event
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO yoyoSchedule (SKU, SKUBegindate, SKUEnddate) values ('" + listBox3.SelectedItem.ToString() + "','" + dateTimePicker1.Value.Date.ToString() + "', '" + dateTimePicker2.Value.Date.ToString() + "')";
                conn.Open();
                object ret = comm.ExecuteScalar();
                int record = Convert.ToInt32(ret);
                conn.Close();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                object ret;

                if ((listBox1.SelectedIndex >= 0) || (listBox2.SelectedIndex >= 0) || (listBox4.SelectedIndex >= 0))
                {
                    //No of yoyo inspection at Station_1
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where (yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND yoyoData.station_name='INSPECTION_1' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "')";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_1 = Convert.ToInt32(ret);
                    MessageBox.Show(dalStnStatus.yoyoAtStn_1.ToString());
                    //No of yoyo at next station (PAINT, QUEUE_PAINT)
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='PAINT' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoGoodSt_1 = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoGoodSt_1.ToString());
                    //No of yoyo scrap from station # 1
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='INSPECTION_1_SCRAP' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_1_Scrp = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_1_Scrp.ToString());

                    //No of yoyo inspection at Station_2
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where (yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND yoyoData.station_name='INSPECTION_2' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "')";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_2 = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_2.ToString());
                    //No of yoyo at next station (ASSEMBLY)
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='ASSEMBLY' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoGoodSt_2 = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoGoodSt_2.ToString());
                    //No of yoyo scrap from station # 2
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='INSPECTION_2_SCRAP' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_2_Scrp = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_2_Scrp.ToString());
                    //No of yoyo Rework from station # 2
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='INSPECTION_2_REWORK' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_2_Rework = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_2_Rework.ToString());


                    //No of yoyo inspection at Station_3
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where (yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND yoyoData.station_name='INSPECTION_3'  AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "')";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_3 = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_3.ToString());
                    //No of yoyo at next station (ASSEMBLY)
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='PACKAGE' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoGoodSt_3 = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoGoodSt_3.ToString());
                    //No of yoyo scrap from station # 2
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='INSPECTION_3_SCRAP' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_3_Scrp = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_3_Scrp.ToString());
                    //No of yoyo Rework from station # 2
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox1.SelectedItem.ToString() + "' AND station_name='INSPECTION_3_REWORK' AND yoyoData.line_no='" + listBox2.SelectedItem.ToString() + "'  AND yoyoData.workarea='" + listBox4.SelectedItem.ToString() + "' AND process_date between '" + dateTimePicker3.Value.Date.ToString() + "' AND '" + dateTimePicker4.Value.Date.ToString() + "'";
                    ret = comm.ExecuteScalar();
                    dalStnStatus.yoyoAtStn_3_Rework = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_3_Rework.ToString());
                    StnStatusPicChart(dalStnStatus);

                }
                else
                {
                    MessageBox.Show("To save mylife please select SKU#, LINE#, WORKAREA and DATE from the list, Not too fancy yet");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

         private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                object ret;
                //String.IsNullOrEmpty(listBox5.SelectedItem.ToString())
                if (listBox5.SelectedIndex >= 0)
                {
                    //No of yoyo scrap from station # 1
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox5.SelectedItem.ToString() + "' AND station_name='INSPECTION_1_SCRAP'";
                    ret = comm.ExecuteScalar();
                    dalScrpStation.yoyoAtStn_1_Scrp = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_1_Scrp.ToString());

                    //No of yoyo scrap from station # 2
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox5.SelectedItem.ToString() + "' AND station_name='INSPECTION_2_SCRAP'";
                    ret = comm.ExecuteScalar();
                    dalScrpStation.yoyoAtStn_2_Scrp = Convert.ToInt32(ret);
                    //                MessageBox.Show(yoyoAtStn_2_Scrp.ToString());
                    //No of yoyo scrap from station # 3
                    comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox5.SelectedItem.ToString() + "' AND station_name='INSPECTION_3_SCRAP'";
                    ret = comm.ExecuteScalar();
                    dalScrpStation.yoyoAtStn_3_Scrp = Convert.ToInt32(ret);

                    DrawPieChrtScrpAtStation(dalScrpStation);
                }
                else
                {
                    MessageBox.Show("You must select #SKU from the list");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void button4_Click(object sender, EventArgs e)
         {
             try
             {
                 SqlConnection conn = new SqlConnection(connString);
                 SqlCommand comm = new SqlCommand();
                 comm.Connection = conn;
                 conn.Open();
                 object ret;
                 if (listBox6.SelectedIndex >= 0)
                 {
                     //No of yoyo scrap from station # 1
                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='WARPING'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.warping = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='INCONSISTENT_THICKNESS'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.inconst_thickness = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='PITTING'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.pitting = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='DRIP_MARK'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.drip_mrk = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='PRIMER_DEFECT'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.prim_dfct = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='FINAL_COAT_FLAW' AND station_name='INSPECTION_2_REWORK'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.coat_flw_st2 = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='FINAL_COAT_FLAW' AND station_name='INSPECTION_3_REWORK'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.coat_flw_st3 = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='TANGLED_STRING'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.tngl_strng = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='BROKEN_AXLE'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.brkn_axl = Convert.ToInt32(ret);

                     comm.CommandText = "select count(*) from yoyoData INNER JOIN yoyoSchedule on yoyoData.scheduleID=yoyoSchedule.scheduleID INNER JOIN yoyoSKU on yoyoSchedule.SKU=yoyoSKU.SKU where yoyoSKU.SKU='" + listBox6.SelectedItem.ToString() + "' AND process_name='BROKEN_SHELL'";
                     ret = comm.ExecuteScalar();
                     dalScprDeft.brkn_shell = Convert.ToInt32(ret);

                     DrawParetoChart(dalScprDeft);
                 }

                 //  NOTE:       In the below query date is as below: 04-10-2016 (mm:dd:year)
                 //  WORKS:      comm.CommandText = "Select ALL serialNo, workarea from yoyoNewData where station_name='INSPECTION_1'";


                 /*                comm.CommandText = "Select count(*) from yoyoData where process_date between '" + dateTimePicker1.Value.Date.ToString() + "' AND '" + dateTimePicker1.Value.Date.AddDays(1) + "' AND station_name='INSPECTION_1'";
                 //                object ret = comm.ExecuteScalar();
                 //                int record =  Convert.ToInt32(ret);
                                 comm.CommandText = "Select ALL serialNo, workarea, keydata, line_no, station_name, process_name, process_date from yoyoData where process_date between '" + dateTimePicker1.Value.Date.ToString() + "' AND '" + dateTimePicker1.Value.Date.AddDays(1) + "' AND station_name='INSPECTION_1'"; 
                                 MessageBox.Show(dateTimePicker1.Value.Date.ToString());
               
                                 rdr = comm.ExecuteReader();
                                 while (rdr.Read())
                                 {
                                      tbl.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7]) ;
                                 } 
                                 rdr.Close();  */
                 else
                 {
                     MessageBox.Show("You must select #SKU from the list");
                 }
                 conn.Close();
                 
              }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
             //                comm.CommandText = "Select ALL serialNo, workarea, keydata, line_no, station_name, process_name, process_date from yoyoNewData where process_date>= convert(datetime, '" + dateTimePicker1.Value.Date.ToString() + "', 112) AND process_date < DATEADD(DAY, 1, convert(datetime, '" + dateTimePicker1.Value.Date.ToString() + "', 112)"; 
             //               corrected date: 2016-04-11 : yy:mm:dd // AND DATEPART(hh, process_date) between '" + listBox2.SelectedItem.ToString() + "' AND '" + listBox1.SelectedItem.ToString() + "'
             //               comm.CommandText = "Select ALL serialNo, workarea, keydata, line_no, station_name, process_name, process_date from yoyoNewData where process_date between '2016-04-09' AND '2016-04-11' AND DATEPART(hh, process_date)>'2:00 PM' AND station_name='INSPECTION_1'";// AND DATEPART(hh, process_date) >='" + listBox2.SelectedItem.ToString() + "' AND DATEPART(hh, process_date) <'" + listBox1.SelectedItem.ToString() + "'";
             //                comm.CommandText = "Select ALL serialNo, workarea, keydata, line_no, station_name, process_name, process_date from yoyoNewData where station_name='INSPECTION_1' AND process_date=> '2016-09-03' AND process_date <= '2016-09-05'";// AND DATEPART(hh, process_date) >='" + listBox2.SelectedItem.ToString() + "' AND DATEPART(hh, process_date) <'" + listBox1.SelectedItem.ToString() + "'";


         }
        public void StnStatusPicChart(DalStationStatus dalStnStatus)
        {
            try
            {
                DataTable tbl_1 = new DataTable();
                DataColumn tbl1_dc1 = new DataColumn("YoYo State", System.Type.GetType("System.String"));
                DataColumn tbl1_dc2 = new DataColumn("Total Yoyo", System.Type.GetType("System.Int32"));
                tbl_1.Columns.Add(tbl1_dc1);
                tbl_1.Columns.Add(tbl1_dc2);

                tbl_1.Rows.Add("Good YoYo St_1", dalStnStatus.yoyoGoodSt_1);
                tbl_1.Rows.Add("Scraped YoYo St_1", dalStnStatus.yoyoAtStn_1_Scrp);
                tbl_1.Rows.Add("Rework YoYo St_1", 0);              // At station # 1 we don't have any yoyo rework
                tbl_1.Rows.Add("Next/New Station", 0);

                tbl_1.Rows.Add("Good YoYo St_2", dalStnStatus.yoyoGoodSt_2);
                tbl_1.Rows.Add("Scraped YoYo St_2", dalStnStatus.yoyoAtStn_2_Scrp);
                tbl_1.Rows.Add("Rework YoYo St_2", dalStnStatus.yoyoAtStn_2_Rework);
                tbl_1.Rows.Add("Next/New Station", 0);

                tbl_1.Rows.Add("Good YoYo St_3", dalStnStatus.yoyoGoodSt_3);
                tbl_1.Rows.Add("Scraped YoYo St_3", dalStnStatus.yoyoAtStn_3_Scrp);
                tbl_1.Rows.Add("Rework YoYo St_3", dalStnStatus.yoyoAtStn_3_Rework);


                //Create pie chart here station # 1
                pieChart1.ChartAreas.Clear();
                pieChart1.Titles.Clear();
                pieChart1.Series.Clear();

                pieChart1.ChartAreas.Add(new ChartArea());
                pieChart1.Titles.Add("Station#1 Outcome -> Pie chart");
                Series series1 = new Series
                {
                    Name = "series1",
                    IsVisibleInLegend = true,
                    Color = System.Drawing.Color.Green,
                    ChartType = SeriesChartType.Pie
                };
                pieChart1.Location = new System.Drawing.Point(300, 227);
                pieChart1.Series.Add(series1);
                series1.Points.Add(dalStnStatus.yoyoGoodSt_1);
                series1.Points.Add(dalStnStatus.yoyoAtStn_1_Scrp);
                series1.Points.Add(0);
                var p1_1 = series1.Points[0];
                p1_1.AxisLabel = dalStnStatus.yoyoGoodSt_1.ToString() + "(Good YoYo)";
                p1_1.LegendText = "Good YoYo";
                var p1_2 = series1.Points[1];
                p1_2.AxisLabel = dalStnStatus.yoyoAtStn_1_Scrp.ToString() + "(Scraped)";
                p1_2.LegendText = "Scraped";
                pieChart1.Invalidate();
                pieChart1.Update();
                this.Controls.Add(pieChart1);

                //Create pie chart here station # 2
                pieChart2.ChartAreas.Clear();
                pieChart2.Titles.Clear();
                pieChart2.Series.Clear();

                pieChart2.ChartAreas.Add(new ChartArea());
                pieChart2.Titles.Add("Station#2 Outcome -> Pie chart");
                Series series2 = new Series
                {
                    Name = "series2",
                    IsVisibleInLegend = true,
                    Color = System.Drawing.Color.Green,
                    ChartType = SeriesChartType.Pie
                };
                pieChart2.Location = new System.Drawing.Point(650, 227);
                pieChart2.Series.Add(series2);
                series2.Points.Add(dalStnStatus.yoyoGoodSt_2);
                series2.Points.Add(dalStnStatus.yoyoAtStn_2_Scrp);
                series2.Points.Add(dalStnStatus.yoyoAtStn_2_Rework);
                var p2_1 = series2.Points[0];
                p2_1.AxisLabel = dalStnStatus.yoyoGoodSt_2.ToString() + "(Good YoYo)";
                p2_1.LegendText = "Good YoYo";
                var p2_2 = series2.Points[1];
                p2_2.AxisLabel = dalStnStatus.yoyoAtStn_2_Scrp.ToString() + "(Scraped)";
                p2_2.LegendText = "Scraped";
                var p2_3 = series2.Points[2];
                p2_3.AxisLabel = dalStnStatus.yoyoAtStn_2_Rework.ToString() + "(Rework)";
                p2_3.LegendText = "Reworked";
                pieChart2.Invalidate();
                pieChart2.Refresh();
                this.Controls.Add(pieChart2);

                //Create pie chart here station # 3
                pieChart3.ChartAreas.Clear();
                pieChart3.Titles.Clear();
                pieChart3.Series.Clear();


                pieChart3.ChartAreas.Add(new ChartArea());
                pieChart3.Titles.Add("Station#3 Outcome -> Pie chart");
                Series series3 = new Series
                {
                    Name = "series3",
                    IsVisibleInLegend = true,
                    Color = System.Drawing.Color.Green,
                    ChartType = SeriesChartType.Pie
                };
                pieChart3.Location = new System.Drawing.Point(1000, 227);
                pieChart3.Series.Add(series3);
                series3.Points.Add(dalStnStatus.yoyoGoodSt_3);
                series3.Points.Add(dalStnStatus.yoyoAtStn_3_Scrp);
                series3.Points.Add(dalStnStatus.yoyoAtStn_3_Rework);
                var p3_1 = series3.Points[0];
                p3_1.AxisLabel = dalStnStatus.yoyoGoodSt_3.ToString() + "(Good YoYo)";
                p3_1.LegendText = "Good YoYo";
                var p3_2 = series3.Points[1];
                p3_2.AxisLabel = dalStnStatus.yoyoAtStn_3_Scrp.ToString() + "(Scraped)";
                p3_2.LegendText = "Scraped";
                var p3_3 = series3.Points[2];
                p3_3.AxisLabel = dalStnStatus.yoyoAtStn_3_Rework.ToString() + "(Rework)";
                p3_3.LegendText = "Reworked";
                pieChart3.Invalidate();
                pieChart3.Refresh();
                this.Controls.Add(pieChart3);
                dataGridView1.DataSource = tbl_1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        public void DrawPieChrtScrpAtStation(DalScrapNoAtStation dalScrpStation)
        {
            try
            {
                DataTable tbl_1 = new DataTable();
                DataColumn tbl1_dc1 = new DataColumn("YoYo Station", System.Type.GetType("System.String"));
                DataColumn tbl1_dc2 = new DataColumn("Rejected Yoyo", System.Type.GetType("System.Int32"));
                tbl_1.Columns.Add(tbl1_dc1);
                tbl_1.Columns.Add(tbl1_dc2);

                tbl_1.Rows.Add("Scraped YoYo St_1", dalScrpStation.yoyoAtStn_1_Scrp);
                tbl_1.Rows.Add("Scraped YoYo St_2", dalScrpStation.yoyoAtStn_2_Scrp);
                tbl_1.Rows.Add("Scraped YoYo St_3", dalScrpStation.yoyoAtStn_3_Scrp);


                //Create pie chart here


                scrpChtStation.Series.Clear();
                scrpChtStation.ChartAreas.Clear();
                scrpChtStation.Titles.Clear();
                scrpChtStation.ChartAreas.Add(new ChartArea());
                scrpChtStation.Titles.Add("Station# 1, 2, 3 Rejected -> Pie chart");

                Series series1 = new Series
                {
                    Name = "series1",
                    IsVisibleInLegend = true,
                    Color = System.Drawing.Color.Green,
                    ChartType = SeriesChartType.Pie
                };
                scrpChtStation.Location = new System.Drawing.Point(300, 227);
                scrpChtStation.Series.Add(series1);

                int[] array = { dalScrpStation.yoyoAtStn_1_Scrp, dalScrpStation.yoyoAtStn_2_Scrp, dalScrpStation.yoyoAtStn_3_Scrp };

                series1.Points.DataBindY(array);
                var p1_1 = series1.Points[0];
                p1_1.AxisLabel = dalScrpStation.yoyoAtStn_1_Scrp.ToString() + "(St # 1 Scrap)";
                p1_1.LegendText = "St # 1 Scrap";
                var p1_2 = series1.Points[1];
                p1_2.AxisLabel = dalScrpStation.yoyoAtStn_2_Scrp.ToString() + "(St # 2 Scrap)";
                p1_2.LegendText = "St # 2 Scrap";
                var p1_3 = series1.Points[2];
                p1_3.AxisLabel = dalScrpStation.yoyoAtStn_3_Scrp.ToString() + "(St # 3 Scrap)";
                p1_3.LegendText = "St # 3 Scrap";

                scrpChtStation.Invalidate();
                scrpChtStation.Refresh();
                scrpChtStation.Update();
                this.Controls.Add(scrpChtStation);
                dataGridView1.DataSource = tbl_1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void DrawParetoChart(DalScrapDefect dalScprDeft)
        {
            try
            {
                
                ChartArea chartArea1 = new ChartArea();
                DataTable tbl_1 = new DataTable();
                DataColumn tbl1_dc1 = new DataColumn("Defect Type", System.Type.GetType("System.String"));
                DataColumn tbl1_dc2 = new DataColumn("Scraped #", System.Type.GetType("System.Int32"));
                tbl_1.Columns.Add(tbl1_dc1);
                tbl_1.Columns.Add(tbl1_dc2);

                tbl_1.Rows.Add("WARPING", dalScprDeft.warping);
                tbl_1.Rows.Add("INCONSISTENT_THICKNESS", dalScprDeft.inconst_thickness);
                tbl_1.Rows.Add("PITTING", dalScprDeft.pitting);

                tbl_1.Rows.Add("DRIP_MARK", dalScprDeft.drip_mrk);
                tbl_1.Rows.Add("PRIMER_DEFECT", dalScprDeft.prim_dfct);
                tbl_1.Rows.Add("COAT_FLAW_St2", dalScprDeft.coat_flw_st2);

                tbl_1.Rows.Add("COAT_FLAW_St3", dalScprDeft.coat_flw_st3);
                tbl_1.Rows.Add("TANGEL_STRING", dalScprDeft.tngl_strng);
                tbl_1.Rows.Add("BROKEN_AXEL", dalScprDeft.brkn_axl);
                tbl_1.Rows.Add("BROKEN_SHELL", dalScprDeft.brkn_shell);

                paretoChart.Series.Clear();
                paretoChart.ChartAreas.Clear();
                paretoChart.Titles.Clear();

                //Create pie chart here
                
                paretoChart.Size = new System.Drawing.Size(1000, 500);
                chartArea1.BackColor = Color.Transparent;
                chartArea1.BackColor = System.Drawing.Color.Gainsboro;
                chartArea1.BackSecondaryColor = System.Drawing.Color.White;
                chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64))))); 
                chartArea1.AxisX.MajorGrid.Enabled = false;
                chartArea1.AxisY.MajorGrid.Enabled = false;
                chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
                chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
                chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));


                paretoChart.Titles.Add("Reject type -> Pareto chart");
                paretoChart.BackColor = Color.LightYellow;
                paretoChart.Palette = ChartColorPalette.Fire;
                paretoChart.ChartAreas.Add(chartArea1);
                Series series1 = new Series
                {
                    Name = "series1",
                    IsVisibleInLegend = false,
                    Color = System.Drawing.Color.Green,
                    ChartType = SeriesChartType.Column

                };

                paretoChart.Location = new System.Drawing.Point(300, 227);
                paretoChart.Series.Add(series1);
                series1.Points.AddY(dalScprDeft.warping);
                series1.Points.AddY(dalScprDeft.inconst_thickness);
                series1.Points.AddY(dalScprDeft.pitting);

                series1.Points.AddY(dalScprDeft.drip_mrk);
                series1.Points.AddY(dalScprDeft.prim_dfct);
                series1.Points.AddY(dalScprDeft.coat_flw_st2);

                series1.Points.AddY(dalScprDeft.coat_flw_st3);
                series1.Points.AddY(dalScprDeft.tngl_strng);
                series1.Points.AddY(dalScprDeft.brkn_axl);
                series1.Points.AddY(dalScprDeft.brkn_shell);

                var p1_0 = series1.Points[0];
                p1_0.AxisLabel = dalScprDeft.warping.ToString();
                p1_0.Label = "warping";
                p1_0.Color = Color.Aqua;
                var p1_1 = series1.Points[1];
                p1_1.AxisLabel = dalScprDeft.inconst_thickness.ToString();
                p1_1.Label = "inconst_thickness";
                var p1_2 = series1.Points[2];
                p1_2.AxisLabel = dalScprDeft.pitting.ToString();
                p1_2.Label = "pitting";
                p1_2.Color = Color.Aqua;

                var p1_3 = series1.Points[3];
                p1_3.AxisLabel = dalScprDeft.drip_mrk.ToString();
                p1_3.Label = "drip_mrk";
                var p1_4 = series1.Points[4];
                p1_4.AxisLabel = dalScprDeft.prim_dfct.ToString();
                p1_4.Label = "tngl_strng";
                p1_4.Color = Color.Aqua;
                var p1_5 = series1.Points[5];
                p1_5.AxisLabel = dalScprDeft.coat_flw_st2.ToString();
                p1_5.Label = "coat_flw_st2";

                var p1_6 = series1.Points[6];
                p1_6.AxisLabel = dalScprDeft.coat_flw_st3.ToString();
                p1_6.Label = "coat_flw_st3";
                p1_6.Color = Color.Aqua;
                var p1_7 = series1.Points[7];
                p1_7.AxisLabel = dalScprDeft.tngl_strng.ToString();
                p1_7.Label = "inconst_thickness";
                var p1_8 = series1.Points[8];
                p1_8.AxisLabel = dalScprDeft.brkn_axl.ToString();
                p1_8.Label = "brkn_axl";
                p1_8.Color = Color.Aqua;
                var p1_9 = series1.Points[9];
                p1_9.AxisLabel = dalScprDeft.brkn_shell.ToString();
                p1_9.Label = "brkn_shell";

                paretoChart.DataManipulator.Sort(PointSortOrder.Descending, series1);

                Series series2 = new Series
                {
                    Name = "Pareto",
                    IsVisibleInLegend = false,
                    Color = System.Drawing.Color.Green,
                    ChartType = SeriesChartType.Line,
                    MarkerColor = Color.Red,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerBorderColor = Color.MidnightBlue,
                    IsValueShownAsLabel = true
                };
                // find the total of all points in the source series 
                double total = 0.0;
                foreach (DataPoint pt in paretoChart.Series["series1"].Points)
                    total += pt.YValues[0];

                //get the chart area name form series1 
                string strChartArea = paretoChart.Series["series1"].ChartArea; 
                // set the max value on the primary axis to total 
                paretoChart.ChartAreas[strChartArea].AxisY.Maximum = total; 
                //Now second series to the chart
                paretoChart.Series.Add(series2);

                // assign the series to the same chart area as the column chart 
                series2.ChartArea = paretoChart.Series["series1"].ChartArea;

                // assign this series to use the secondary axis and set it maximum to be 100% 
                series2.YAxisType = AxisType.Secondary;
                paretoChart.ChartAreas[strChartArea].AxisY2.Maximum = 100;  


                // locale specific percentage format with no decimals 
                paretoChart.ChartAreas[strChartArea].AxisY2.LabelStyle.Format = "P0";

                // turn off the end point values of the primary X axis 
                paretoChart.ChartAreas[strChartArea].AxisX.LabelStyle.IsEndLabelVisible = false;

                // for each point in the source series find % of total and assign to series 
                double percentage = 0.0;

                foreach (DataPoint pt in paretoChart.Series["series1"].Points)
                {
                    percentage += (pt.YValues[0] / total * 100.0);
                    series2.Points.Add(Math.Round(percentage, 2));
                }

                paretoChart.Invalidate();
                paretoChart.Refresh();
                this.Controls.Add(paretoChart);
                paretoChart.Refresh();
                dataGridView1.DataSource = tbl_1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;

                if ((!String.IsNullOrWhiteSpace(textBox1.Text)) && (!String.IsNullOrWhiteSpace(textBox2.Text)) && (!String.IsNullOrWhiteSpace(textBox3.Text)))
                {
                    if ((textBox3.Text.Trim() == "admin") || (textBox3.Text.Trim() == "normal"))
                    {
                        comm.CommandText = "INSERT INTO userTable (username, userpassword, usertype) values ('" + textBox1.Text + "','" + textBox2.Text + "', '" + textBox3.Text + "')";
                        conn.Open();
                        object ret = comm.ExecuteScalar();
                        int record = Convert.ToInt32(ret);
                    }
                    else
                    {
                        MessageBox.Show("Username/password/ type cannot be NULL/ Empty/ Whitespace");
                    }
                }
                else
                {
                    MessageBox.Show("Username/password/ type cannot be NULL/ Empty/ Whitespace");
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


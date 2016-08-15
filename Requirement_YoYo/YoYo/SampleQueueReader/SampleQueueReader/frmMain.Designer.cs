namespace SampleQueueReader
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.lstQueueData = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQueueServer = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemaining = new System.Windows.Forms.TextBox();
            this.chkCount = new System.Windows.Forms.CheckBox();
            this.btnSingleRead = new System.Windows.Forms.Button();
            this.btnPurgeQ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(565, 53);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Read";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lstQueueData
            // 
            this.lstQueueData.FormattingEnabled = true;
            this.lstQueueData.Location = new System.Drawing.Point(12, 53);
            this.lstQueueData.Name = "lstQueueData";
            this.lstQueueData.Size = new System.Drawing.Size(524, 342);
            this.lstQueueData.TabIndex = 1;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(565, 82);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop Read";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message Queue Server";
            // 
            // txtQueueServer
            // 
            this.txtQueueServer.Location = new System.Drawing.Point(151, 10);
            this.txtQueueServer.Name = "txtQueueServer";
            this.txtQueueServer.Size = new System.Drawing.Size(173, 20);
            this.txtQueueServer.TabIndex = 5;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(565, 219);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear List";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Remaining";
            // 
            // txtRemaining
            // 
            this.txtRemaining.Location = new System.Drawing.Point(436, 10);
            this.txtRemaining.Name = "txtRemaining";
            this.txtRemaining.Size = new System.Drawing.Size(100, 20);
            this.txtRemaining.TabIndex = 8;
            this.txtRemaining.Text = "0";
            this.txtRemaining.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkCount
            // 
            this.chkCount.AutoSize = true;
            this.chkCount.Location = new System.Drawing.Point(565, 257);
            this.chkCount.Name = "chkCount";
            this.chkCount.Size = new System.Drawing.Size(54, 17);
            this.chkCount.TabIndex = 9;
            this.chkCount.Text = "Count";
            this.chkCount.UseVisualStyleBackColor = true;
            // 
            // btnSingleRead
            // 
            this.btnSingleRead.Location = new System.Drawing.Point(565, 142);
            this.btnSingleRead.Name = "btnSingleRead";
            this.btnSingleRead.Size = new System.Drawing.Size(75, 23);
            this.btnSingleRead.TabIndex = 10;
            this.btnSingleRead.Text = "Single Read";
            this.btnSingleRead.UseVisualStyleBackColor = true;
            this.btnSingleRead.Click += new System.EventHandler(this.btnSingleRead_Click);
            // 
            // btnPurgeQ
            // 
            this.btnPurgeQ.Location = new System.Drawing.Point(565, 312);
            this.btnPurgeQ.Name = "btnPurgeQ";
            this.btnPurgeQ.Size = new System.Drawing.Size(75, 23);
            this.btnPurgeQ.TabIndex = 11;
            this.btnPurgeQ.Text = "Purge Q";
            this.btnPurgeQ.UseVisualStyleBackColor = true;
            this.btnPurgeQ.Click += new System.EventHandler(this.btnPurgeQ_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 405);
            this.Controls.Add(this.btnPurgeQ);
            this.Controls.Add(this.btnSingleRead);
            this.Controls.Add(this.chkCount);
            this.Controls.Add(this.txtRemaining);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtQueueServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lstQueueData);
            this.Controls.Add(this.btnStart);
            this.Name = "frmMain";
            this.Text = "Sample Queue Reader";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstQueueData;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQueueServer;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRemaining;
        private System.Windows.Forms.CheckBox chkCount;
        private System.Windows.Forms.Button btnSingleRead;
        private System.Windows.Forms.Button btnPurgeQ;
    }
}


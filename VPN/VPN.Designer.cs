namespace VPN
{
    partial class VPN
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VPN));
            this.PicBtnSwitch = new System.Windows.Forms.PictureBox();
            this.CountriesList = new System.Windows.Forms.ComboBox();
            this.StatusTxt = new System.Windows.Forms.Label();
            this.LblLocation = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LblStatus = new System.Windows.Forms.Label();
            this.rasDialer = new DotRas.RasDialer(this.components);
            this.PhBook = new DotRas.RasPhoneBook(this.components);
            this.LblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicBtnSwitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PicBtnSwitch
            // 
            this.PicBtnSwitch.BackgroundImage = global::VPN.Properties.Resources.SwitchOffBlk;
            this.PicBtnSwitch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PicBtnSwitch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PicBtnSwitch.Location = new System.Drawing.Point(12, 116);
            this.PicBtnSwitch.Name = "PicBtnSwitch";
            this.PicBtnSwitch.Size = new System.Drawing.Size(244, 94);
            this.PicBtnSwitch.TabIndex = 3;
            this.PicBtnSwitch.TabStop = false;
            this.PicBtnSwitch.Click += new System.EventHandler(this.PicBtnSwitch_Click);
            // 
            // CountriesList
            // 
            this.CountriesList.BackColor = System.Drawing.SystemColors.Control;
            this.CountriesList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CountriesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CountriesList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CountriesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountriesList.FormattingEnabled = true;
            this.CountriesList.Location = new System.Drawing.Point(276, 103);
            this.CountriesList.Name = "CountriesList";
            this.CountriesList.Size = new System.Drawing.Size(266, 37);
            this.CountriesList.TabIndex = 4;
            // 
            // StatusTxt
            // 
            this.StatusTxt.AutoSize = true;
            this.StatusTxt.Font = new System.Drawing.Font("Trebuchet MS", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusTxt.Location = new System.Drawing.Point(271, 197);
            this.StatusTxt.Name = "StatusTxt";
            this.StatusTxt.Size = new System.Drawing.Size(230, 33);
            this.StatusTxt.TabIndex = 5;
            this.StatusTxt.Text = "Choose a location";
            // 
            // LblLocation
            // 
            this.LblLocation.AutoSize = true;
            this.LblLocation.Font = new System.Drawing.Font("Webdings", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLocation.Location = new System.Drawing.Point(271, 70);
            this.LblLocation.Name = "LblLocation";
            this.LblLocation.Size = new System.Drawing.Size(132, 30);
            this.LblLocation.TabIndex = 6;
            this.LblLocation.Text = "Location:";
            // 
            // BtnExit
            // 
            this.BtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnExit.BackgroundImage")));
            this.BtnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Location = new System.Drawing.Point(514, 12);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(40, 40);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.TabStop = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::VPN.Properties.Resources.Minimise;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(468, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Font = new System.Drawing.Font("Webdings", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatus.Location = new System.Drawing.Point(271, 158);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(102, 30);
            this.LblStatus.TabIndex = 9;
            this.LblStatus.Text = "Status:";
            // 
            // rasDialer
            // 
// TODO: Code generation for '' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.rasDialer.Credentials = null;
            this.rasDialer.EapOptions = new DotRas.RasEapOptions(false, false, false);
            this.rasDialer.Options = new DotRas.RasDialOptions(false, false, false, false, false, false, false, false, false, false);
            this.rasDialer.DialCompleted += new System.EventHandler<DotRas.DialCompletedEventArgs>(this.RasDialer_DialCompleted);
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.Location = new System.Drawing.Point(14, 26);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(210, 42);
            this.LblTitle.TabIndex = 10;
            this.LblTitle.Text = "IKEv2 Client";
            // 
            // VPN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LawnGreen;
            this.ClientSize = new System.Drawing.Size(566, 256);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.LblLocation);
            this.Controls.Add(this.StatusTxt);
            this.Controls.Add(this.CountriesList);
            this.Controls.Add(this.PicBtnSwitch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VPN";
            this.Text = "VPN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VPN_FormClosing);
            this.Load += new System.EventHandler(this.VPN_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VPN_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VPN_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VPN_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.PicBtnSwitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox PicBtnSwitch;
        private System.Windows.Forms.ComboBox CountriesList;
        private System.Windows.Forms.Label StatusTxt;
        private System.Windows.Forms.Label LblLocation;
        private System.Windows.Forms.PictureBox BtnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LblStatus;
        private DotRas.RasDialer rasDialer;
        private DotRas.RasPhoneBook PhBook;
        private System.Windows.Forms.Label LblTitle;
    }
}


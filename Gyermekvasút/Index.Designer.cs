namespace Gyermekvasút
{
    partial class Index
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Index));
            this.status = new System.Windows.Forms.StatusStrip();
            this.ido = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.owl = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnI = new System.Windows.Forms.Button();
            this.btnO = new System.Windows.Forms.Button();
            this.btnS = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.btnH = new System.Windows.Forms.Button();
            this.btnU = new System.Windows.Forms.Button();
            this.btnL = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.owl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(104)))), ((int)(((byte)(57)))));
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ido,
            this.toolStripStatusLabel3});
            this.status.Location = new System.Drawing.Point(0, 255);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(636, 22);
            this.status.TabIndex = 2;
            this.status.Text = "statusStrip1";
            // 
            // ido
            // 
            this.ido.BackColor = System.Drawing.SystemColors.Control;
            this.ido.ForeColor = System.Drawing.Color.White;
            this.ido.Name = "ido";
            this.ido.Size = new System.Drawing.Size(58, 17);
            this.ido.Text = "Betöltés...";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(563, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 718);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "© Szkupien Péter 2014.";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Gyermekvasút";
            this.notifyIcon1.Visible = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Széchenyi-hegy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(115, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Csillebérc";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            this.label3.DoubleClick += new System.EventHandler(this.label3_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(201, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Virágvölgy";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(287, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "János-hegy";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(369, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Szépjuhászné";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(466, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Hárs-hegy";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(550, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Hűvösvölgy";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(104)))), ((int)(((byte)(57)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(131, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(231, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Gyermekvasút – Rendelkezői szimulátor";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(104)))), ((int)(((byte)(57)))));
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(146, 238);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(303, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Szkupien Péter 2014-2015. – Minden jog fenntartva!";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(104)))), ((int)(((byte)(57)))));
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(129, 236);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 17);
            this.label13.TabIndex = 20;
            this.label13.Text = "©";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(104)))), ((int)(((byte)(57)))));
            this.linkLabel1.Location = new System.Drawing.Point(406, 217);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(18, 13);
            this.linkLabel1.TabIndex = 24;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "itt.";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(104)))), ((int)(((byte)(57)))));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(129, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(369, 26);
            this.label10.TabIndex = 25;
            this.label10.Text = "Köszönet mindenkinek, aki munkájával hozzájárult a szimulátor elkészültéhez\r\nés a" +
    " rendelkező oktatás megújításához. Tételes felsorolás ";
            // 
            // owl
            // 
            this.owl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.owl.Image = global::Gyermekvasút.Properties.Resources.BAGOLY;
            this.owl.Location = new System.Drawing.Point(122, 127);
            this.owl.Name = "owl";
            this.owl.Size = new System.Drawing.Size(34, 31);
            this.owl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.owl.TabIndex = 23;
            this.owl.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(104)))), ((int)(((byte)(57)))));
            this.pictureBox1.BackgroundImage = global::Gyermekvasút.Properties.Resources.mávgyv;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(4, 186);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 65);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // btnI
            // 
            this.btnI.BackgroundImage = global::Gyermekvasút.Properties.Resources.I_icon_kép;
            this.btnI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnI.Location = new System.Drawing.Point(276, 12);
            this.btnI.Name = "btnI";
            this.btnI.Size = new System.Drawing.Size(82, 82);
            this.btnI.TabIndex = 4;
            this.btnI.UseVisualStyleBackColor = true;
            this.btnI.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnO
            // 
            this.btnO.BackgroundImage = global::Gyermekvasút.Properties.Resources.O_icon_kép;
            this.btnO.Location = new System.Drawing.Point(540, 12);
            this.btnO.Name = "btnO";
            this.btnO.Size = new System.Drawing.Size(82, 82);
            this.btnO.TabIndex = 2;
            this.btnO.UseVisualStyleBackColor = true;
            this.btnO.Click += new System.EventHandler(this.btnO_Click);
            // 
            // btnS
            // 
            this.btnS.BackgroundImage = global::Gyermekvasút.Properties.Resources.S_icon_kép2Z;
            this.btnS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnS.Location = new System.Drawing.Point(364, 12);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(82, 82);
            this.btnS.TabIndex = 0;
            this.btnS.UseVisualStyleBackColor = true;
            this.btnS.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnA
            // 
            this.btnA.BackgroundImage = global::Gyermekvasút.Properties.Resources.A_icon_kép;
            this.btnA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnA.Location = new System.Drawing.Point(12, 12);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(82, 82);
            this.btnA.TabIndex = 7;
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnH
            // 
            this.btnH.BackgroundImage = global::Gyermekvasút.Properties.Resources.H_icon_kép;
            this.btnH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnH.Location = new System.Drawing.Point(452, 12);
            this.btnH.Name = "btnH";
            this.btnH.Size = new System.Drawing.Size(82, 82);
            this.btnH.TabIndex = 3;
            this.btnH.UseVisualStyleBackColor = true;
            this.btnH.Click += new System.EventHandler(this.btnH_Click);
            // 
            // btnU
            // 
            this.btnU.BackgroundImage = global::Gyermekvasút.Properties.Resources.U_icon_kép;
            this.btnU.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnU.Location = new System.Drawing.Point(100, 12);
            this.btnU.Name = "btnU";
            this.btnU.Size = new System.Drawing.Size(82, 82);
            this.btnU.TabIndex = 6;
            this.btnU.UseVisualStyleBackColor = true;
            this.btnU.Click += new System.EventHandler(this.btnU_Click);
            // 
            // btnL
            // 
            this.btnL.BackgroundImage = global::Gyermekvasút.Properties.Resources.L_icon_kép2;
            this.btnL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnL.Location = new System.Drawing.Point(188, 12);
            this.btnL.Name = "btnL";
            this.btnL.Size = new System.Drawing.Size(82, 82);
            this.btnL.TabIndex = 5;
            this.btnL.UseVisualStyleBackColor = true;
            this.btnL.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::Gyermekvasút.Properties.Resources.jóláblác;
            this.pictureBox2.Location = new System.Drawing.Point(0, 115);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(636, 140);
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(253, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(252, 22);
            this.toolStripMenuItem1.Text = "Pillanatnyi értékelés megtekintése";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(636, 277);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.owl);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.status);
            this.Controls.Add(this.btnI);
            this.Controls.Add(this.btnO);
            this.Controls.Add(this.btnS);
            this.Controls.Add(this.btnA);
            this.Controls.Add(this.btnH);
            this.Controls.Add(this.btnU);
            this.Controls.Add(this.btnL);
            this.Controls.Add(this.pictureBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(652, 315);
            this.MinimumSize = new System.Drawing.Size(652, 315);
            this.Name = "Index";
            this.Text = "Gyermekvasút – Rendelkezői szimulátor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Index_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Index_FormClosed);
            this.Load += new System.EventHandler(this.Index_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Index_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Index_KeyDown);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.owl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button btnH;
        private System.Windows.Forms.Button btnO;
        private System.Windows.Forms.Button btnL;
        private System.Windows.Forms.Button btnI;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnU;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel ido;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox owl;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
namespace Gyermekvasút.Telefon
{
    partial class EngedelyKeres
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngedelyKeres));
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbAzonos = new System.Windows.Forms.RadioButton();
            this.rbVolt = new System.Windows.Forms.RadioButton();
            this.rbVan = new System.Windows.Forms.RadioButton();
            this.gbAzonos = new System.Windows.Forms.GroupBox();
            this.azonosAllomas = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.azonosPerc = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.azonosOra = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.azonosVonat = new System.Windows.Forms.ComboBox();
            this.gbVolt = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.voltVonat = new System.Windows.Forms.ComboBox();
            this.voltAll = new System.Windows.Forms.Label();
            this.voltPerc = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.voltOra = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.voltEllenv = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.gbVan = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.vanVonat = new System.Windows.Forms.ComboBox();
            this.vanAll = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.vanOra = new System.Windows.Forms.NumericUpDown();
            this.vanPerc = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.vanEllenv = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.kimenet = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.gbAzonos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.azonosPerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.azonosOra)).BeginInit();
            this.gbVolt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voltPerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltOra)).BeginInit();
            this.gbVan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vanOra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vanPerc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(429, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ebben az ablakban lehetőséged van engedélyt kérni a szomszéd állomás rendelkezőjé" +
    "től.";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 430);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(473, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabel1.Text = "Fogadó állomás: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rendelkező pajtás!";
            // 
            // rbAzonos
            // 
            this.rbAzonos.AutoSize = true;
            this.rbAzonos.Checked = true;
            this.rbAzonos.Location = new System.Drawing.Point(15, 62);
            this.rbAzonos.Name = "rbAzonos";
            this.rbAzonos.Size = new System.Drawing.Size(171, 17);
            this.rbAzonos.TabIndex = 3;
            this.rbAzonos.TabStop = true;
            this.rbAzonos.Text = "Azonos irányú vonat volt útban";
            this.rbAzonos.UseVisualStyleBackColor = true;
            this.rbAzonos.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbVolt
            // 
            this.rbVolt.AutoSize = true;
            this.rbVolt.Location = new System.Drawing.Point(15, 108);
            this.rbVolt.Name = "rbVolt";
            this.rbVolt.Size = new System.Drawing.Size(182, 17);
            this.rbVolt.TabIndex = 4;
            this.rbVolt.Text = "Ellenkező irányú vonat volt útban";
            this.rbVolt.UseVisualStyleBackColor = true;
            this.rbVolt.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbVan
            // 
            this.rbVan.AutoSize = true;
            this.rbVan.Location = new System.Drawing.Point(15, 85);
            this.rbVan.Name = "rbVan";
            this.rbVan.Size = new System.Drawing.Size(183, 17);
            this.rbVan.TabIndex = 5;
            this.rbVan.Text = "Ellenkező irányú vonat van útban";
            this.rbVan.UseVisualStyleBackColor = true;
            this.rbVan.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // gbAzonos
            // 
            this.gbAzonos.Controls.Add(this.azonosAllomas);
            this.gbAzonos.Controls.Add(this.label6);
            this.gbAzonos.Controls.Add(this.azonosPerc);
            this.gbAzonos.Controls.Add(this.label5);
            this.gbAzonos.Controls.Add(this.azonosOra);
            this.gbAzonos.Controls.Add(this.label4);
            this.gbAzonos.Controls.Add(this.label3);
            this.gbAzonos.Controls.Add(this.azonosVonat);
            this.gbAzonos.Location = new System.Drawing.Point(12, 140);
            this.gbAzonos.Name = "gbAzonos";
            this.gbAzonos.Size = new System.Drawing.Size(451, 70);
            this.gbAzonos.TabIndex = 6;
            this.gbAzonos.TabStop = false;
            this.gbAzonos.Text = "Azonos irányú vonat volt útban";
            // 
            // azonosAllomas
            // 
            this.azonosAllomas.AutoSize = true;
            this.azonosAllomas.Location = new System.Drawing.Point(6, 45);
            this.azonosAllomas.Name = "azonosAllomas";
            this.azonosAllomas.Size = new System.Drawing.Size(132, 13);
            this.azonosAllomas.TabIndex = 7;
            this.azonosAllomas.Text = "FogadoAllomas állomásra?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "perckor a(z)";
            // 
            // azonosPerc
            // 
            this.azonosPerc.Location = new System.Drawing.Point(155, 20);
            this.azonosPerc.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.azonosPerc.Name = "azonosPerc";
            this.azonosPerc.Size = new System.Drawing.Size(46, 20);
            this.azonosPerc.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "óra";
            // 
            // azonosOra
            // 
            this.azonosOra.Location = new System.Drawing.Point(75, 20);
            this.azonosOra.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.azonosOra.Name = "azonosOra";
            this.azonosOra.Size = new System.Drawing.Size(46, 20);
            this.azonosOra.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(377, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "számú vonat";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mehet-e kb.";
            // 
            // azonosVonat
            // 
            this.azonosVonat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.azonosVonat.FormattingEnabled = true;
            this.azonosVonat.Location = new System.Drawing.Point(276, 19);
            this.azonosVonat.Name = "azonosVonat";
            this.azonosVonat.Size = new System.Drawing.Size(95, 21);
            this.azonosVonat.TabIndex = 8;
            // 
            // gbVolt
            // 
            this.gbVolt.Controls.Add(this.label13);
            this.gbVolt.Controls.Add(this.voltVonat);
            this.gbVolt.Controls.Add(this.voltAll);
            this.gbVolt.Controls.Add(this.voltPerc);
            this.gbVolt.Controls.Add(this.label10);
            this.gbVolt.Controls.Add(this.voltOra);
            this.gbVolt.Controls.Add(this.label11);
            this.gbVolt.Controls.Add(this.voltEllenv);
            this.gbVolt.Controls.Add(this.label12);
            this.gbVolt.Enabled = false;
            this.gbVolt.Location = new System.Drawing.Point(12, 298);
            this.gbVolt.Name = "gbVolt";
            this.gbVolt.Size = new System.Drawing.Size(451, 76);
            this.gbVolt.TabIndex = 13;
            this.gbVolt.TabStop = false;
            this.gbVolt.Text = "Ellenkező irányú vonat volt útban";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(64, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "perckor a(z)";
            // 
            // voltVonat
            // 
            this.voltVonat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.voltVonat.FormattingEnabled = true;
            this.voltVonat.Location = new System.Drawing.Point(133, 46);
            this.voltVonat.Name = "voltVonat";
            this.voltVonat.Size = new System.Drawing.Size(95, 21);
            this.voltVonat.TabIndex = 13;
            // 
            // voltAll
            // 
            this.voltAll.AutoSize = true;
            this.voltAll.Location = new System.Drawing.Point(234, 50);
            this.voltAll.Name = "voltAll";
            this.voltAll.Size = new System.Drawing.Size(195, 13);
            this.voltAll.TabIndex = 12;
            this.voltAll.Text = "számú vonat FogadoAllomas állomásra?";
            // 
            // voltPerc
            // 
            this.voltPerc.Location = new System.Drawing.Point(12, 46);
            this.voltPerc.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.voltPerc.Name = "voltPerc";
            this.voltPerc.Size = new System.Drawing.Size(46, 20);
            this.voltPerc.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(388, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "óra";
            // 
            // voltOra
            // 
            this.voltOra.Location = new System.Drawing.Point(336, 20);
            this.voltOra.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.voltOra.Name = "voltOra";
            this.voltOra.Size = new System.Drawing.Size(46, 20);
            this.voltOra.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(207, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "számú vonat. Mehet-e kb.";
            // 
            // voltEllenv
            // 
            this.voltEllenv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.voltEllenv.FormattingEnabled = true;
            this.voltEllenv.Location = new System.Drawing.Point(105, 19);
            this.voltEllenv.Name = "voltEllenv";
            this.voltEllenv.Size = new System.Drawing.Size(95, 21);
            this.voltEllenv.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Utolsó vonat a(z)";
            // 
            // gbVan
            // 
            this.gbVan.Controls.Add(this.label8);
            this.gbVan.Controls.Add(this.vanVonat);
            this.gbVan.Controls.Add(this.vanAll);
            this.gbVan.Controls.Add(this.label15);
            this.gbVan.Controls.Add(this.vanOra);
            this.gbVan.Controls.Add(this.vanPerc);
            this.gbVan.Controls.Add(this.label16);
            this.gbVan.Controls.Add(this.vanEllenv);
            this.gbVan.Controls.Add(this.label17);
            this.gbVan.Enabled = false;
            this.gbVan.Location = new System.Drawing.Point(12, 216);
            this.gbVan.Name = "gbVan";
            this.gbVan.Size = new System.Drawing.Size(451, 76);
            this.gbVan.TabIndex = 15;
            this.gbVan.TabStop = false;
            this.gbVan.Text = "Ellenkező irányú vonat van útban";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "perckor a(z)";
            // 
            // vanVonat
            // 
            this.vanVonat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vanVonat.FormattingEnabled = true;
            this.vanVonat.Location = new System.Drawing.Point(127, 41);
            this.vanVonat.Name = "vanVonat";
            this.vanVonat.Size = new System.Drawing.Size(95, 21);
            this.vanVonat.TabIndex = 13;
            // 
            // vanAll
            // 
            this.vanAll.AutoSize = true;
            this.vanAll.Location = new System.Drawing.Point(228, 45);
            this.vanAll.Name = "vanAll";
            this.vanAll.Size = new System.Drawing.Size(195, 13);
            this.vanAll.TabIndex = 12;
            this.vanAll.Text = "számú vonat FogadoAllomas állomásra?";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(352, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "óra";
            // 
            // vanOra
            // 
            this.vanOra.Location = new System.Drawing.Point(300, 18);
            this.vanOra.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.vanOra.Name = "vanOra";
            this.vanOra.Size = new System.Drawing.Size(46, 20);
            this.vanOra.TabIndex = 9;
            // 
            // vanPerc
            // 
            this.vanPerc.Location = new System.Drawing.Point(6, 41);
            this.vanPerc.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.vanPerc.Name = "vanPerc";
            this.vanPerc.Size = new System.Drawing.Size(46, 20);
            this.vanPerc.TabIndex = 11;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(165, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(129, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "számú vonat, mehet-e kb.";
            // 
            // vanEllenv
            // 
            this.vanEllenv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vanEllenv.FormattingEnabled = true;
            this.vanEllenv.Location = new System.Drawing.Point(64, 18);
            this.vanEllenv.Name = "vanEllenv";
            this.vanEllenv.Size = new System.Drawing.Size(95, 21);
            this.vanEllenv.TabIndex = 8;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Ha itt a(z)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(451, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Engedélykérés";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // kimenet
            // 
            this.kimenet.AutoSize = true;
            this.kimenet.ForeColor = System.Drawing.Color.White;
            this.kimenet.Location = new System.Drawing.Point(12, 406);
            this.kimenet.Name = "kimenet";
            this.kimenet.Size = new System.Drawing.Size(10, 13);
            this.kimenet.TabIndex = 17;
            this.kimenet.Text = " ";
            // 
            // EngedelyKeres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 452);
            this.Controls.Add(this.kimenet);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbVan);
            this.Controls.Add(this.gbVolt);
            this.Controls.Add(this.gbAzonos);
            this.Controls.Add(this.rbVan);
            this.Controls.Add(this.rbVolt);
            this.Controls.Add(this.rbAzonos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EngedelyKeres";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Engedélykérés";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EngedelyKeres_FormClosing);
            this.Load += new System.EventHandler(this.EngedelyKeres_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbAzonos.ResumeLayout(false);
            this.gbAzonos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.azonosPerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.azonosOra)).EndInit();
            this.gbVolt.ResumeLayout(false);
            this.gbVolt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voltPerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltOra)).EndInit();
            this.gbVan.ResumeLayout(false);
            this.gbVan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vanOra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vanPerc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbAzonos;
        private System.Windows.Forms.RadioButton rbVolt;
        private System.Windows.Forms.RadioButton rbVan;
        private System.Windows.Forms.GroupBox gbAzonos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox azonosVonat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label azonosAllomas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown azonosPerc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown azonosOra;
        private System.Windows.Forms.GroupBox gbVolt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox voltVonat;
        private System.Windows.Forms.Label voltAll;
        private System.Windows.Forms.NumericUpDown voltPerc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown voltOra;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox voltEllenv;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox gbVan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox vanVonat;
        private System.Windows.Forms.Label vanAll;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown vanOra;
        private System.Windows.Forms.NumericUpDown vanPerc;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox vanEllenv;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label kimenet;
    }
}
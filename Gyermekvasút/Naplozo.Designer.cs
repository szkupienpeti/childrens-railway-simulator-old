namespace Gyermekvasút
{
    partial class Naplozo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Naplozo));
            this.label1 = new System.Windows.Forms.Label();
            this.vonat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.vg = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.kimenet = new System.Windows.Forms.Label();
            this.visszamondas = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Betöltés...";
            // 
            // vonat
            // 
            this.vonat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vonat.FormattingEnabled = true;
            this.vonat.Location = new System.Drawing.Point(43, 73);
            this.vonat.Name = "vonat";
            this.vonat.Size = new System.Drawing.Size(95, 21);
            this.vonat.TabIndex = 1;
            this.vonat.SelectedIndexChanged += new System.EventHandler(this.vonat_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Naplózó pajtás!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "A(z)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "számú vonat meneszthető";
            // 
            // vg
            // 
            this.vg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vg.FormattingEnabled = true;
            this.vg.Location = new System.Drawing.Point(43, 100);
            this.vg.Name = "vg";
            this.vg.Size = new System.Drawing.Size(95, 21);
            this.vg.TabIndex = 5;
            this.vg.SelectedIndexChanged += new System.EventHandler(this.vonat_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "a(z)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "vágányról!";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(15, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(273, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Felhatalmazás";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // kimenet
            // 
            this.kimenet.AutoSize = true;
            this.kimenet.ForeColor = System.Drawing.Color.White;
            this.kimenet.Location = new System.Drawing.Point(12, 172);
            this.kimenet.Name = "kimenet";
            this.kimenet.Size = new System.Drawing.Size(10, 13);
            this.kimenet.TabIndex = 9;
            this.kimenet.Text = " ";
            // 
            // visszamondas
            // 
            this.visszamondas.AutoSize = true;
            this.visszamondas.Location = new System.Drawing.Point(12, 202);
            this.visszamondas.Name = "visszamondas";
            this.visszamondas.Size = new System.Drawing.Size(10, 13);
            this.visszamondas.TabIndex = 11;
            this.visszamondas.Text = " ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 269);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(273, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Bezárás";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(240, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 60);
            this.button2.TabIndex = 13;
            this.button2.Text = "Nem találok valamit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Naplozo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 305);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.visszamondas);
            this.Controls.Add(this.kimenet);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.vg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.vonat);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(316, 343);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(316, 343);
            this.Name = "Naplozo";
            this.Text = "Naplózó";
            this.Load += new System.EventHandler(this.Naplozo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox vonat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox vg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label kimenet;
        private System.Windows.Forms.Label visszamondas;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}
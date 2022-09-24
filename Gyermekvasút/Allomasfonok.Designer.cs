namespace Gyermekvasút
{
    partial class Allomasfonok
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Allomasfonok));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.kp = new System.Windows.Forms.Label();
            this.vp = new System.Windows.Forms.Label();
            this.kpallas = new System.Windows.Forms.Label();
            this.ko = new System.Windows.Forms.Button();
            this.kimenet = new System.Windows.Forms.Label();
            this.bezar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(672, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ebben az ablakban lehetőséged van olyan vágányutak visszavételére,\\namelyekhez sz" +
    "ámlálóval ellátott nyomógombok kezelése szükséges.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(975, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Válaszd ki a legördülő listából, hogy melyik vágányutat szeretnéd visszavenni. Cs" +
    "ak azok a vágányutak jelennek meg, amelyek visszavételéhez valóban számlálóval e" +
    "llátott nyomógomb kezelése szükséges.";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 123);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(329, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "A kiválasztott vágányút adatai";
            // 
            // kp
            // 
            this.kp.AutoSize = true;
            this.kp.Location = new System.Drawing.Point(12, 174);
            this.kp.Name = "kp";
            this.kp.Size = new System.Drawing.Size(64, 13);
            this.kp.TabIndex = 4;
            this.kp.Text = "Kezdőpont: ";
            // 
            // vp
            // 
            this.vp.AutoSize = true;
            this.vp.Location = new System.Drawing.Point(12, 188);
            this.vp.Name = "vp";
            this.vp.Size = new System.Drawing.Size(53, 13);
            this.vp.TabIndex = 5;
            this.vp.Text = "Végpont: ";
            // 
            // kpallas
            // 
            this.kpallas.AutoSize = true;
            this.kpallas.Location = new System.Drawing.Point(12, 204);
            this.kpallas.Name = "kpallas";
            this.kpallas.Size = new System.Drawing.Size(97, 13);
            this.kpallas.TabIndex = 6;
            this.kpallas.Text = "Kezdő jelző állása: ";
            // 
            // ko
            // 
            this.ko.Enabled = false;
            this.ko.Image = global::Gyermekvasút.Properties.Resources.d55_ko;
            this.ko.Location = new System.Drawing.Point(144, 237);
            this.ko.Name = "ko";
            this.ko.Size = new System.Drawing.Size(70, 70);
            this.ko.TabIndex = 7;
            this.ko.UseVisualStyleBackColor = true;
            this.ko.Click += new System.EventHandler(this.ko_Click);
            // 
            // kimenet
            // 
            this.kimenet.AutoSize = true;
            this.kimenet.ForeColor = System.Drawing.Color.White;
            this.kimenet.Location = new System.Drawing.Point(12, 319);
            this.kimenet.Name = "kimenet";
            this.kimenet.Size = new System.Drawing.Size(10, 13);
            this.kimenet.TabIndex = 8;
            this.kimenet.Text = " ";
            // 
            // bezar
            // 
            this.bezar.Location = new System.Drawing.Point(15, 375);
            this.bezar.Name = "bezar";
            this.bezar.Size = new System.Drawing.Size(329, 23);
            this.bezar.TabIndex = 9;
            this.bezar.Text = "Bezárás";
            this.bezar.UseVisualStyleBackColor = true;
            this.bezar.Click += new System.EventHandler(this.bezar_Click);
            // 
            // Allomasfonok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 411);
            this.ControlBox = false;
            this.Controls.Add(this.ko);
            this.Controls.Add(this.bezar);
            this.Controls.Add(this.kimenet);
            this.Controls.Add(this.kpallas);
            this.Controls.Add(this.vp);
            this.Controls.Add(this.kp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(372, 449);
            this.MinimumSize = new System.Drawing.Size(372, 449);
            this.Name = "Allomasfonok";
            this.Text = "Állomásfőnök";
            this.Load += new System.EventHandler(this.Allomasfonok_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label kp;
        private System.Windows.Forms.Label vp;
        private System.Windows.Forms.Label kpallas;
        private System.Windows.Forms.Button ko;
        private System.Windows.Forms.Label kimenet;
        private System.Windows.Forms.Button bezar;
    }
}
namespace Gyermekvasút
{
    partial class Koruljaras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Koruljaras));
            this.label1 = new System.Windows.Forms.Label();
            this.elrendeles = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.kimenet = new System.Windows.Forms.Label();
            this.visszamondas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Váltókezelő pajtás!";
            // 
            // elrendeles
            // 
            this.elrendeles.AutoSize = true;
            this.elrendeles.Location = new System.Drawing.Point(12, 27);
            this.elrendeles.Name = "elrendeles";
            this.elrendeles.Size = new System.Drawing.Size(67, 13);
            this.elrendeles.TabIndex = 1;
            this.elrendeles.Text = "Dolgozunk...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(357, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Elrendelés";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(15, 172);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(357, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Bezárás";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // kimenet
            // 
            this.kimenet.AutoSize = true;
            this.kimenet.ForeColor = System.Drawing.Color.White;
            this.kimenet.Location = new System.Drawing.Point(12, 101);
            this.kimenet.Name = "kimenet";
            this.kimenet.Size = new System.Drawing.Size(0, 13);
            this.kimenet.TabIndex = 4;
            // 
            // visszamondas
            // 
            this.visszamondas.AutoSize = true;
            this.visszamondas.Location = new System.Drawing.Point(12, 114);
            this.visszamondas.Name = "visszamondas";
            this.visszamondas.Size = new System.Drawing.Size(0, 13);
            this.visszamondas.TabIndex = 5;
            // 
            // Koruljaras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 207);
            this.ControlBox = false;
            this.Controls.Add(this.visszamondas);
            this.Controls.Add(this.kimenet);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.elrendeles);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(400, 245);
            this.MinimumSize = new System.Drawing.Size(400, 245);
            this.Name = "Koruljaras";
            this.Text = "Körüljárás";
            this.Load += new System.EventHandler(this.Koruljaras_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label elrendeles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label kimenet;
        private System.Windows.Forms.Label visszamondas;

    }
}
namespace Gyermekvasút
{
    partial class Halozat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Halozat));
            this.progbar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.clientWorker = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progbar
            // 
            this.progbar.Location = new System.Drawing.Point(12, 105);
            this.progbar.Name = "progbar";
            this.progbar.Size = new System.Drawing.Size(493, 23);
            this.progbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progbar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(491, 52);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(221, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Mégse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // clientWorker
            // 
            this.clientWorker.WorkerReportsProgress = true;
            this.clientWorker.WorkerSupportsCancellation = true;
            this.clientWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.clientWorker_DoWork);
            this.clientWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.clientWorker_ProgressChanged);
            this.clientWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.clientWorker_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Blue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Dolgozunk...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "© Szkupien Péter 2014-2015. – Minden jog fenntartva!";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.status.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.status.Location = new System.Drawing.Point(445, 166);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(54, 13);
            this.status.TabIndex = 9;
            this.status.Text = "Semmi jel.";
            this.status.Click += new System.EventHandler(this.label4_Click);
            // 
            // Halozat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 188);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Halozat";
            this.ShowIcon = false;
            this.Text = "Hálózati csatlakozás";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Halozat_FormClosing);
            this.Load += new System.EventHandler(this.Halozat_Load);
            this.Shown += new System.EventHandler(this.Halozat_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Halozat_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker clientWorker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label status;
    }
}
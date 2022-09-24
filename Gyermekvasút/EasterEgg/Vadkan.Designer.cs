namespace Gyermekvasút
{
    partial class Vadkan
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gameover = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ido = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gameover
            // 
            this.gameover.AutoSize = true;
            this.gameover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameover.Font = new System.Drawing.Font("Courier New", 80F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gameover.Location = new System.Drawing.Point(121, 277);
            this.gameover.Name = "gameover";
            this.gameover.Size = new System.Drawing.Size(694, 123);
            this.gameover.TabIndex = 0;
            this.gameover.Text = "GAME OVER!";
            this.gameover.Click += new System.EventHandler(this.gameover_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(842, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mozgás: WASD, szedd fel a makkokat, de a mérgezettekkel vigyázz!";
            // 
            // ido
            // 
            this.ido.AutoSize = true;
            this.ido.Font = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ido.Location = new System.Drawing.Point(11, 39);
            this.ido.Name = "ido";
            this.ido.Size = new System.Drawing.Size(190, 31);
            this.ido.TabIndex = 2;
            this.ido.Text = "Idő: 30 sec";
            // 
            // Vadkan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(870, 570);
            this.Controls.Add(this.ido);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameover);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Vadkan";
            this.Text = "Vadkan";
            this.Load += new System.EventHandler(this.Vadkan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Vadkan_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Vadkan_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label gameover;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ido;


    }
}
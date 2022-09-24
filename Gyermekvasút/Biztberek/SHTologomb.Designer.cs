namespace Gyermekvasút.Biztberek
{
    partial class SHTologomb
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SHTologomb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(48)))), ((int)(((byte)(24)))));
            this.BackgroundImage = global::Gyermekvasút.Properties.Resources.shTologombAlap;
            this.Name = "SHTologomb";
            this.Size = new System.Drawing.Size(40, 71);
            this.Click += new System.EventHandler(this.SHTologomb_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SHTologomb_MouseDown);
            this.MouseLeave += new System.EventHandler(this.SHTologomb_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SHTologomb_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

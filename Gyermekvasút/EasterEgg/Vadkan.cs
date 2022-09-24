using Gyermekvasút.EasterEgg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút
{
    public partial class Vadkan : Form
    {
        public Vadkan()
        {
            InitializeComponent();
        }

        Random rnd = new Random();

        int VADKAN = 1;

        void NewMakk(bool poison)
        {
            Makk makk = new Makk(poison);            
            Random r = new Random();

            int x = 0, y = 0;
            bool xJo = false, yJo = false;            

            do
            {
                x = r.Next(51, this.Width - 51);
                y = r.Next(75, this.Height - 75);
                xJo = true;
                yJo = true;

                foreach (Control item in this.Controls)
                {
                    if ((item.Location.X - x) * (item.Location.X + item.Width - x) < 0)
                    {
                        xJo = false;
                    }
                    if ((x - item.Location.X) * (x + makk.Width - item.Location.X) < 0)
                    {
                        xJo = false;
                    }

                    if ((item.Location.Y - y) * (item.Location.Y + item.Height - y) < 0)
                    {
                        yJo = false;
                    }
                    if ((y - item.Location.Y) * (y + makk.Height - item.Location.Y) < 0)
                    {
                        yJo = false;
                    }
                }
            }
            while (xJo == false || yJo == false) ;

            makk.Location = new Point(x, y);
            this.Controls.Add(makk);
        }

        void NewVadkan()
        {
            Vaddiszno vd = new Vaddiszno();
            Random r = new Random();

            int x = 0, y = 0;
            bool xJo = false, yJo = false;

            do
            {
                //x = r.Next(136, this.Width - 136);
                //y = r.Next(147, this.Height - 147);

                x = (r.Next(0, this.Width / 135 - 2) + 1) * 135;
                y = (r.Next(0, this.Height / 146 - 2) + 1) * 146;

                xJo = true;
                yJo = true;

                foreach (Control item in this.Controls)
                {
                    if ((item.Location.X - x) * (item.Location.X + item.Width - x) < 0)
                    {
                        xJo = false;
                    }
                    if ((x - item.Location.X) * (x + vd.Width - item.Location.X) < 0)
                    {
                        xJo = false;
                    }

                    if ((item.Location.Y - y) * (item.Location.Y + item.Height - y) < 0)
                    {
                        yJo = false;
                    }
                    if ((y - item.Location.Y) * (y + vd.Height - item.Location.Y) < 0)
                    {
                        yJo = false;
                    }
                }
            }
            while (xJo == false && yJo == false);

            vd.Location = new Point(x, y);
            this.Controls.Add(vd);
        }

        private void Vadkan_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Controls.Remove(gameover);

            this.WindowState = FormWindowState.Maximized;
            Vaddiszno vd = new Vaddiszno();
            vd.Location = new Point(this.Width / 2 - vd.Width / 2, this.Height / 2 - vd.Height / 2);
            this.Controls.Add(vd);
        }

        private void Vadkan_KeyDown(object sender, KeyEventArgs e)
        {
            if (ended) return;

            int x = 0, y = 0;
            foreach (Control item in this.Controls)
            {
                if ((item as Vaddiszno) != null)
                {
                    x = item.Location.X;
                    y = item.Location.Y;

                    if (e.KeyCode == Keys.W && item.Location.Y > 6) y -= 10;
                    else if (e.KeyCode == Keys.S && item.Location.Y + item.Height < this.Height - 6) y += 10;
                    else if (e.KeyCode == Keys.D && item.Location.X + item.Width < this.Width - 6) x += 10;
                    else if (e.KeyCode == Keys.A && item.Location.X > 6) x -= 10;

                    bool crashX = false, crashY = false;
                    foreach (Control masik in this.Controls)
                    {
                        if ((masik as Vaddiszno) != null)
	                    {
                            if ((item.Location.X - masik.Location.X) * (item.Location.X + item.Width - masik.Location.X) < 0)
                            {
                                crashX = true;
                                if (crashX && crashY) break;
                            }
                            if ((masik.Location.X - item.Location.X) * (masik.Location.X + masik.Width - item.Location.X) < 0)
                            {
                                crashX = true;
                                if (crashX && crashY) break;
                            }

                            if ((item.Location.Y - masik.Location.Y) * (item.Location.Y + item.Height - masik.Location.Y) < 0)
                            {
                                crashY = true;
                                if (crashX && crashY) break;
                            }
                            if ((masik.Location.Y - item.Location.Y) * (masik.Location.Y + masik.Height - item.Location.Y) < 0)
                            {
                                crashY = true;
                                if (crashX && crashY) break;
                            }		                    
	                    }
                    }
                    if (crashX && crashY)
                    {
                        if (e.KeyCode == Keys.W) y += 10;
                        else if (e.KeyCode == Keys.S) y -= 10;
                        else if (e.KeyCode == Keys.D) x -= 10;
                        else if (e.KeyCode == Keys.A) x += 10;
                    }

                    item.Location = new Point(x, y);
                }
            }

            //találkozott-e vaddisznó makkal
            bool vadkanMakkX = false, vadkanMakkY = false;
            foreach (Control item in this.Controls)
            {
                if ((item as Vaddiszno) != null)
                {
                    foreach (Control makk in this.Controls)
                    {
                        if ((makk as Makk) != null)
                        {
                            vadkanMakkX = false;
                            vadkanMakkY = false;
                            if ((item.Location.X - makk.Location.X) * (item.Location.X + item.Width - makk.Location.X) < 0)
                            {
                                vadkanMakkX = true;
                            }
                            if ((makk.Location.X - item.Location.X) * (makk.Location.X + makk.Width - item.Location.X) < 0)
                            {
                                vadkanMakkX = true;
                            }

                            if ((item.Location.Y - makk.Location.Y) * (item.Location.Y + item.Height - makk.Location.Y) < 0)
                            {
                                vadkanMakkY = true;
                            }
                            if ((makk.Location.Y - item.Location.Y) * (makk.Location.Y + makk.Height - item.Location.Y) < 0)
                            {
                                vadkanMakkY = true;
                            }

                            if (vadkanMakkX && vadkanMakkY)
                            {
                                if ((makk as Makk).Poisoned)
                                {//die
                                    this.Controls.Remove(makk);
                                    this.Controls.Remove(item);
                                    VADKAN--;
                                    if (VADKAN == 0)
                                    {
                                        timer1.Stop();
                                        ended = true;
                                        gameover.Text += "\nVesztettél!";
                                        GameOver(true);

                                    }
                                }
                                else
                                {//new
                                    this.Controls.Remove(makk);
                                    NewVadkan();
                                    VADKAN++;
                                }
                            }
                        }
                    }
                }
            }

        }

        private void Vadkan_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        int ticks = 0;
        bool ended = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ended)
            {
                timer1.Stop();
                this.Close();
            }
            
            ticks++;
            ido.Text = "Idő: " + Convert.ToString(30 - ticks * 2) + " sec";
            if (ticks == 15)
            {
                gameover.Text += "\nGyőztél!";
                ended = true;
                GameOver();
                return;
            }
            if (rnd.Next(0, 5) == 0) NewMakk(true);
            else NewMakk(false);
        }

        private void gameover_Click(object sender, EventArgs e)
        {

        }

        void GameOver(bool loose = false)
        {
            gameover.Location = new Point(this.Width / 2 - gameover.Width / 2, this.Height / 2 - gameover.Height / 2);
            this.Controls.Add(gameover);
            gameover.BringToFront();
            timer1.Start();
        }
    }
}

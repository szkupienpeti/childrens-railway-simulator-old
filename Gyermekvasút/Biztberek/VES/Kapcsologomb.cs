using Gyermekvasút.AllomasFormok;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gyermekvasút.Biztberek
{
    public class Kapcsologomb : PictureBox
    {
        public Kapcsologomb()
        {
            MouseDown += Kapcsologomb_MouseDown;
        }

        void Kapcsologomb_MouseDown(object sender, MouseEventArgs e)
        {
            AllomasForm.Allitas = this;
        }

        Image img;
        public Image Img
        {
            get { return img; }
            set
            {
                img = value;
                Image = Img;
                BackColor = Color.FromArgb(26, 48, 24);
            }
        }

        double alpha;
        virtual public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        double elozoAlpha;
        public double ElozoAlpha
        {
            get { return elozoAlpha; }
            set { elozoAlpha = value; }
        }

        double kpX;
        public double KpX
        {
            get { return kpX; }
            set { kpX = value; }
        }

        double kpY;
        public double KpY
        {
            get { return kpY; }
            set { kpY = value; }
        }
        
        public VES VES { get; set; }

        ICsilleberc allomasForm;
        public ICsilleberc AllomasForm
        {
            get { return allomasForm; }
            set { allomasForm = value; }
        }

        int celAllas;
        public int CelAllas
        {
            get { return celAllas; }
            set { celAllas = value; }
        }

        int minimumAllas;
        public int MinimumAllas
        {
            get { return minimumAllas; }
            set { minimumAllas = value; }
        }

        public static Image RotateImage(Image img, float rotationAngle, double KpY)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)KpY);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)KpY);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }        
    }
}

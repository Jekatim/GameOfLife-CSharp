using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameOfLife_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static bool resume = false;

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resume = false;
            Application.Exit();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int picw = pictureBox1.Width;
			int pich = pictureBox1.Height;

			Bitmap bmp = new Bitmap(picw, pich);
			Graphics g = Graphics.FromImage(bmp);

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

			int wndw = Config.getWidth();
			int wndh = Config.getHeight();

			float stepw = picw / wndw;
			float steph = pich / wndh;

			for (int i = 0; i < wndw; i++)
			{
				for (int j = 0; j < wndh; j++)
				{
					g.DrawRectangle(Pens.Aqua, i*stepw + 1, j*steph + 1, stepw - 1, steph - 1);
					/*if (frontBuffer[i][j] == 0)
					{
						g.FillRectangle(Brushes.White, i*stepw + 1, j*steph + 1, stepw - 1, steph - 1);
					} 
					else
					{
						g.FillRectangle(Brushes.Black, i*stepw + 1, j*steph + 1, stepw - 1, steph - 1);
					}*/
				}
			}

			pictureBox1.Image = bmp;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            menuStrip1.Refresh();
        }
    }
}

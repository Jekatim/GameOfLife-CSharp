using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GameOfLife_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            buf1 = new Buffer();
            buf2 = new Buffer();
            buf1.FillRandom();
            SwitchBuffer();
            pictureBox1.Invalidate();
        }

        static bool resume = false;

        static int bufFlag = 0;
        Buffer buf1, buf2;

        Thread calcthr;

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
					if (CurrentBuffer().is_live(i,j))
					{
                        g.FillRectangle(Brushes.Black, i*stepw + 1, j*steph + 1, stepw - 1, steph - 1);
					} 
					else
					{
						g.FillRectangle(Brushes.White, i*stepw + 1, j*steph + 1, stepw - 1, steph - 1);
					}
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

        void CalcThread()
		{
			while (resume)
			{
				this.CalculateGeneration();

				Thread.Sleep(30);
			}
		}

        void SwitchBuffer()
        {
            switch (bufFlag)
            {
                case 1:
                    bufFlag = 2;
                    break;
                case 0:
                case 2:
                    bufFlag = 1;
                    break;
            }
        }

        Buffer CurrentBuffer()
        {
            if (bufFlag == 1)
                return buf1;
            else
                return buf2;
        }

        Buffer PrevBuffer()
        {
            if (bufFlag == 1)
                return buf2;
            else
                return buf1;
        }

        int CountNeighbours(int x, int y)
		{
			int counter = 0;
			int[,] nb = new int[2,8];
			int k = 0;

			for (int i = x - 1; i <= x + 1; i++) {
				for (int j = y - 1; j <= y + 1; j++) {

					if (i == x && j == y)
						continue;

					if (i < 0)
                        nb[0, k] = Config.getWidth() - 1;
					else
                        if (i >= Config.getWidth()) 
							nb[0,k] = 0;
						else
							nb[0,k] = i;

					if (j < 0)
                        nb[1, k] = Config.getHeight() - 1;
					else
                        if (j >= Config.getHeight()) 
							nb[1,k] = 0;
						else
							nb[1,k] = j;

					k++;
				}
			}

			for (int i = 0; i < 8; i++)
			{
                if (CurrentBuffer().is_live(nb[0,i], nb[1,i]))
					counter++;
			}
			return counter;
		}

        public void CalculateGeneration()
        {
            int neighbours = 0;

            for (int i = 0; i < Config.getWidth(); i++)
            {
                for (int j = 0; j < Config.getHeight(); j++)
                {
                    neighbours = CountNeighbours(i, j);

                    if (CurrentBuffer().is_live(i,j))
                    {
                        switch (neighbours)
                        {
                            case 3:
                                PrevBuffer().born(i,j);
                                break;
                            default:
                                PrevBuffer().die(i, j);
                                break;
                        }
                    }
                    else
                    {
                        switch (neighbours)
                        {
                            case 2:
                            case 3:
                                PrevBuffer().born(i, j);
                                break;
                            default:
                                PrevBuffer().die(i, j);
                                break;
                        }
                    }
                }
            }

            SwitchBuffer();

            pictureBox1.Invalidate();
        }

        private void stepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalculateGeneration();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!resume)
			 {
				 resume = true;
                 calcthr = new Thread(new ThreadStart(CalcThread));
                 calcthr.Start();
			 }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resume)
            {
                resume = false;
                calcthr.Join();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
			 {
			 case Keys.Space:
				 stepToolStripMenuItem_Click(sender, e);
				 break;
			 case Keys.R:
				 startToolStripMenuItem_Click(sender, e);
				 break;
			 case Keys.S:
				 stopToolStripMenuItem_Click(sender, e);
				 break;
			 case Keys.Escape:
				 exitToolStripMenuItem_Click(sender, e);
				 break;
			 default:
				 break;
			 }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigDialog cfgdlg = new ConfigDialog();
			cfgdlg.ShowDialog();

            bufFlag = 0;

            buf1 = new Buffer();
            buf2 = new Buffer();
            buf1.FillRandom();
            SwitchBuffer();

			pictureBox1.Invalidate();
        }
    }
}

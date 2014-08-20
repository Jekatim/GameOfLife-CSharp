using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GameOfLife_CSharp
{
    public partial class ConfigDialog : Form
    {
        public ConfigDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
				 {
					 Config.setWidth(Convert.ToInt32(textBox1.Text));
					 Config.setHeight(Convert.ToInt32(textBox2.Text));
					 Config.setSeed(Convert.ToInt32(textBox3.Text));

					 Close();
				 }
				 catch (Exception ex)
				 {
				 	Debug.WriteLine("Wrong config saving");
				 }	
        }

        private void ConfigDialog_Shown(object sender, EventArgs e)
        {
            try
			 {
				 textBox1.Text = Convert.ToString(Config.getWidth());
				 textBox2.Text = Convert.ToString(Config.getHeight());
				 textBox3.Text = Convert.ToString(Config.getSeed());
				 trackBar1.Value = Config.getSeed();
			 }
			 catch (Exception ex)
			 {
				 Debug.WriteLine("Wrong config reading");
			 }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(trackBar1.Value);
        }
    }
}

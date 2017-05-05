using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wolves
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		public static Form nineForm = new Nine();
		public static Form twelveForm = new twelve();
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				Games.playerNumbers = 9;
				nineForm.ShowDialog();
			}
			else
			{
				Games.playerNumbers = 12;
				twelveForm.ShowDialog();
			}
		}
		/*
* System.Media.SoundPlayer player = new System.Media.SoundPlayer();
player.SoundLocation = @"F:\VS2017\Wolves\Sound\tianhei.wav";
player.Load();
player.PlaySync();*/

	}

}


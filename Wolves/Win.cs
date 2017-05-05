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
	public partial class Win : Form
	{
		public Win()
		{
			InitializeComponent();
		}
		private void Win_Load(object sender, EventArgs e)
		{
			Nine ne = new Nine();
			label2.Text= ne.winLabelInformation();
			if (Games.isWolvesWin)
			{
				label1.Text = "狼人阵营胜利！";
				Nine.winForm.BackColor = Color.Black;
				label1.ForeColor = Color.White;
				label2.ForeColor = Color.White;
				groupBox1.ForeColor = Color.White;
				Games.playSound(@"F:\VS2017\Wolves\Sound\end1.wav");
			}
			else
			{
				label1.Text = "好人阵营胜利！";
				Nine.winForm.BackColor = Color.White;
				label1.ForeColor = Color.Red;
				label2.ForeColor = Color.Black;
				groupBox1.ForeColor = Color.Black;
				Games.playSound(@"F:\VS2017\Wolves\Sound\end2.wav");
			}
		}
	}
}

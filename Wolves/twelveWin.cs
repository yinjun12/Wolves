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
	public partial class twelveWin : Form
	{
		public twelveWin()
		{
			InitializeComponent();
		}

		private void twelveWin_Load(object sender, EventArgs e)
		{
			twelve ne = new twelve();
			label2.Text = ne.winLabelInformation();
			if (Games.isWolvesWin)
			{
				label1.Text = "狼人阵营胜利！";
				twelve.winForm.BackColor = Color.Black;
				label1.ForeColor = Color.White;
				label2.ForeColor = Color.White;
				groupBox1.ForeColor = Color.White;
				Games.playSound(@"F:\VS2017\Wolves\Sound\end1.wav");
			}
			else
			{
				label1.Text = "好人阵营胜利！";
				twelve.winForm.BackColor = Color.White;
				label1.ForeColor = Color.Red;
				label2.ForeColor = Color.Black;
				groupBox1.ForeColor = Color.Black;
				Games.playSound(@"F:\VS2017\Wolves\Sound\end2.wav");
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}

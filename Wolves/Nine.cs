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
	public partial class Nine : Form
	{
		static Games wolvesGame;
		public static Form winForm = new Win();
		public Nine()
		{
			InitializeComponent();
		}

		
		private void printHeadImage()
		{
			PictureBox[] pic = new PictureBox[] { pictureBox1,pictureBox2,pictureBox3
			,pictureBox4,pictureBox5,pictureBox6,pictureBox7,pictureBox8,pictureBox9};
			for (int i = 0; i < pic.Length; i++)
				pic[i].Image = Image.FromFile(wolvesGame.getWhichImage(i));
		}
		private void labelForeColorChange(Color co)
		{
			Label[] allLabel = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11 };
			for (int i = 0; i < allLabel.Length; i++)
				allLabel[i].ForeColor = co;
		}
		private RadioButton[] getTenAllRadioButton()
		{
			RadioButton[] allRadioButton = new RadioButton[] { radioButton1,radioButton2,radioButton3,
			radioButton4,radioButton5,radioButton6,radioButton7,radioButton8,radioButton9,radioButton10};
			return allRadioButton;
		}
		private void radioForeColorChange(Color co)//改变颜色10个radio都要改，改变文字9个跟另一个单独起来
		{
			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < allRadioButton.Length; i++)
				allRadioButton[i].ForeColor = co;
		}
		private void radioCheckedChange(bool flag)
		{
			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < allRadioButton.Length; i++)
				allRadioButton[i].Checked = flag;
		}

		private void radioVisibleChange(bool flag)
		{
			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < Games.playerNumbers; i++)
			{
				if (wolvesGame.allPlayerInformation[i].isDeath != 0)
					allRadioButton[i].Visible = false;
				else
					allRadioButton[i].Visible = flag;
			}
		}
		private void radioTextChange(String str)
		{
			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < allRadioButton.Length; i++)
				allRadioButton[i].Text = str;
		}
		public void changeColorInNight()
		{
			labelForeColorChange(Color.White);
			radioVisibleChange(true);
			radioForeColorChange(Color.White);
			Form1.nineForm.BackColor = Color.Black;
		}
		public void changeColorInDays()
		{
			radioForeColorChange(Color.Black);
			labelForeColorChange(Color.Black);
			radioVisibleChange(false);
			Form1.nineForm.BackColor = Color.White;
		}
		private int getCheckedRadioIndex(RadioButton[] all)//返回值是-1代表没选，返回值是playerNumber代表选的是最右边一个
		{
			int index = -1;
			for (int i = 0; i < all.Length; i++)
			{
				if (all[i].Checked == true)
				{
					index = i;
					break;
				}
			}
			return index;
		}
		public String winLabelInformation()
		{
			return wolvesGame.getAllPlayerInformation();
		}

		private void label10TextChange()
		{
			String label10Str = String.Format("第{0}天  ", wolvesGame.days);
			if (wolvesGame.isNight)
				label10Str += "夜晚";
			else
				label10Str += "白天";
			label10.Text = label10Str;
		}
		private void Nine_Load(object sender, EventArgs e)
		{
			wolvesGame = new Games();
			wolvesGame.setAllPlayersIdentity();
			wolvesGame.randAllocateIdentity();
			label10.Text = "请上帝根据身份发牌！";
			printHeadImage();
			button1.Visible = true;
			button2.Visible = false;
			button3.Visible = false;
			button4.Visible = false;
			button5.Visible = false;
			button6.Visible = false;
			button7.Visible = false;
			label11.Visible = false;
			radioVisibleChange(false);
			radioButton10.Visible = false;
			changeColorInDays();

		}

		private void button1_Click(object sender, EventArgs e)
		{ //点击进入天黑，为天黑相关操作进行准备;
			Games.playSound(@"F:\VS2017\Wolves\Sound\tianhei.wav");
			wolvesGame.isNight = true;
			if (wolvesGame.judgeGamesIsOver())//首先判断游戏是否结束,具体是谁赢，在Win窗体里的加载函数中判断
			{
				winForm.ShowDialog();
				this.Close();
				return;
			}
			wolvesGame.days++;//天黑加一天
			label10TextChange();
			//为天黑做环境准备
			changeColorInNight();
			button1.Visible = false;
			label11.Visible = true;
			radioVisibleChange(false);
			radioButton10.Visible = false;
			label11.Text = "◆预言家请睁眼，并选择你验人的序号\n";
			if (wolvesGame.allPlayerInformation[wolvesGame.findIndexByIndentity('Y')].isDeath != 0)
			{//预言家在当夜死亡还是能验人，在不是当夜死亡不能验人
				label11.Text = "◆【预言家已死亡!】\n" + label11.Text;
			}
			button7.Visible = true;
			wolvesGame.deadNumberInNight[0] = -1;//每次把前一天晚上杀人情况刷新
			wolvesGame.deadNumberInNight[1] = -1;
			printHeadImage();
		}

		private void button5_Click(object sender, EventArgs e)//白天投票死亡和可能猎人发动技能
		{
			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton = getTenAllRadioButton();
			int deadNumber = getCheckedRadioIndex(allRadioButton);
			if (deadNumber < 0 || deadNumber > Games.playerNumbers)
			{
				label11.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			if (wolvesGame.deadNumberInNight[0] ==wolvesGame. findIndexByIndentity('H'))//如果晚上死的是猎人，发动技能按钮不消失
			{
				wolvesGame.deadNumberInNight[0] = -1;//不会在下次点击按钮再进入这个事件
				if (deadNumber >= 0 && deadNumber < Games.playerNumbers)
				{
					wolvesGame.allPlayerInformation[deadNumber].isDeath = 1;
				}
				radioVisibleChange(true);
				radioTextChange("是否公投TA");
				radioButton10.Visible = false;
				button5.Visible = true;
				button5.Text = "白天出局";
				label11.Text = "◆请所有活着玩家发言，并在发言过后进行投票！\n";
				printHeadImage();
				return;

			}//不是猎人，则正常投票
			button5.Visible = false;
			button1.Visible = true;
			radioVisibleChange(false);
			radioButton10.Visible = false;
			int deadNumberInDay = getCheckedRadioIndex(allRadioButton);
			if (deadNumberInDay < 0 || deadNumberInDay > Games.playerNumbers)
			{
				label11.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			if (deadNumberInDay >= 0 && deadNumberInDay < Games.playerNumbers)
			{
				wolvesGame.allPlayerInformation[deadNumberInDay].isDeath = 2;
				label11.Text = String.Format("◆{0}号玩家请发表遗言\n", deadNumberInDay + 1);
				if (wolvesGame.allPlayerInformation[deadNumberInDay].identity == 'H')//猎人白天被投，发动技能后按钮不消失
				{
					radioVisibleChange(true);
					label11.Text = label11.Text + String.Format("◆【技能触发】\n{0}号玩家是否发动技能？", deadNumberInDay + 1);
					button5.Visible = true;//白天死亡按钮不消失，天黑按钮也不出现
					button1.Visible = false;
					button5.Text = "是否发动技能";
					radioButton10.Visible = true;
					radioButton10.Text = "不发动技能";
					allRadioButton[deadNumberInDay].Visible = false;
				}
			}
			printHeadImage();
		}

		private void button2_Click(object sender, EventArgs e)
		{

			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton =getTenAllRadioButton();
			int deadNumberInNightByLangren = getCheckedRadioIndex(allRadioButton);
			if (deadNumberInNightByLangren < 0 )//有10号按钮
			{
				label11.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			radioVisibleChange(false);//先不出现选择解药
			radioButton10.Visible = true;
			radioButton10.Text = "不使用解药";
			if (deadNumberInNightByLangren < Games.playerNumbers)//配置女巫使用解药的环境
			{
				label11.Text = String.Format("◆【死亡的是{0}号玩家！】\n◆你有一瓶解药要用吗？（提示：女巫解药用完后上帝不再向女巫透露晚上死亡玩家信息!）", deadNumberInNightByLangren + 1);
				wolvesGame.allPlayerInformation[deadNumberInNightByLangren].isDeath = 1;
				wolvesGame.deadNumberInNight[0] = deadNumberInNightByLangren;
				int nuwuAbility = wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability;
				if (wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].isDeath == 0/*女巫活着*/||
					wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].isDeath==1&&
					 wolvesGame.days == 1 && deadNumberInNightByLangren ==wolvesGame . findIndexByIndentity('N')/*女巫第一天可以自救*/)
				{
					if (nuwuAbility == Games.BOATHHAVE || nuwuAbility == Games.ONLYANTIDOTE)//女巫有解药，只有有解药的情况下，才能选取是否救TA
					{
						if (deadNumberInNightByLangren !=wolvesGame. findIndexByIndentity('N')
						|| wolvesGame.days == 1 && deadNumberInNightByLangren ==wolvesGame. findIndexByIndentity('N'))//只有在第一夜能自救
						{
							wolvesGame.nightInformation += String.Format("{0}号玩家 ", deadNumberInNightByLangren + 1);
							allRadioButton[deadNumberInNightByLangren].Visible = true;
							allRadioButton[deadNumberInNightByLangren].Text = "是否救TA";
						}
						else
						{
							label11.Text = "◆【解药无法使用!】\n（女巫只有第一天夜晚能自救）";
						}
					}
					else
					{
						radioButton10.Text = "解药已用完 ";
						label11.Text = String.Format("◆【解药已用完！】\n") + label11.Text;
					}
				}
				else
				{
					radioButton10.Text = "女巫已死亡 ";
					label11.Text = "◆【女巫死亡!】\n不能使用药！" + label11.Text;
				}
			}
			else
			{
				label11.Text = String.Format("◆【无人死亡！】\n（提示：女巫解药用完后上帝不再向女巫透露晚上死亡玩家信息！）");

			}
			button2.Visible = false;
			button3.Visible = true;
			printHeadImage();
			radioCheckedChange(false);//在每个按钮的最后把选择取消
		}

		private void button3_Click(object sender, EventArgs e)
		{
			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton = getTenAllRadioButton();
			int checkedRadioNumber = getCheckedRadioIndex(allRadioButton);
			if (checkedRadioNumber < 0 || checkedRadioNumber > Games.playerNumbers)
			{
				label11.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			if (wolvesGame.deadNumberInNight[0] == checkedRadioNumber)
			{//要能选中，则说明女巫有解药
				if (wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability == Games.BOATHHAVE)
					wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability = Games.ONLYPOISION;
				else
					wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability = Games.NONABILITY;
				wolvesGame.allPlayerInformation[wolvesGame.deadNumberInNight[0]].isDeath = 0;
				wolvesGame.deadNumberInNight[0] = -1;//昨晚没有死人
				wolvesGame.nightInformation = "";
				wolvesGame.nuwuUseAntidoteDay = wolvesGame.days;//女巫使用解药的天数
			}
			//配置女巫使用毒药的环境
			int nuwuAbility = wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability;
			radioButton10.Visible = true;

			if (wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].isDeath == 0 /*女巫活着*/||
					wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].isDeath == 1 && wolvesGame.deadNumberInNight[0] ==wolvesGame. findIndexByIndentity('N'))
			{//女巫活着，或女巫当晚死亡
				if (nuwuAbility == Games.BOATHHAVE || nuwuAbility == Games.ONLYPOISION)
				{
					if (wolvesGame.days == wolvesGame.nuwuUseAntidoteDay)
					{
						radioVisibleChange(false);
						label11.Text = "◆【不能在一天夜里同时使用毒药和解药！】（选择不使用毒药）\n";
					}
					else
					{
						radioVisibleChange(true);
						if (wolvesGame.deadNumberInNight[0] >= 0 && wolvesGame.deadNumberInNight[0] < Games.playerNumbers)
							allRadioButton[wolvesGame.deadNumberInNight[0]].Visible = true;
						radioTextChange("是否毒TA");
						label11.Text = "◆你有一瓶毒药你要用吗？\n";
					}
				}
				else
				{
					radioVisibleChange(false);
					label11.Text = "◆【用过毒药！】\n（点击：选择用毒药）";
				}
			}
			else
			{
				radioVisibleChange(false); 
				radioButton10.Visible = true;
				label11.Text = "◆【女巫已死亡!】\n不能使用毒药！";
			}
			radioButton10.Text = "不使用毒药";
			button3.Visible = false;
			button6.Visible = true;
			printHeadImage();
			radioCheckedChange(false);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			RadioButton[] allRadioButton = new RadioButton[10];
			allRadioButton = getTenAllRadioButton();
			int deadNumberByNuwuPoision = getCheckedRadioIndex(allRadioButton);
			if (deadNumberByNuwuPoision < 0 || deadNumberByNuwuPoision > Games.playerNumbers)
			{
				label11.Text = "◆没有选择！！请选择一项";
				return;
			}
			if (deadNumberByNuwuPoision >= 0 &&deadNumberByNuwuPoision < Games.playerNumbers)//女巫使用完毒药
			{
				wolvesGame.deadNumberInNight[1] = deadNumberByNuwuPoision;
				wolvesGame.allPlayerInformation[deadNumberByNuwuPoision].isDeath = 1;
				if (wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability == Games.BOATHHAVE)
					wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability = Games.ONLYANTIDOTE;
				else
					wolvesGame.allPlayerInformation[wolvesGame. findIndexByIndentity('N')].ability = Games.NONABILITY;
			}
			radioVisibleChange(false);
			radioButton10.Visible = false;
			button6.Visible = false;
			button4.Visible = true;
			label11.Text = "◆猎人请睁眼……\n◆猎人请闭眼\n◆天亮了~~ ";
			printHeadImage();
			radioCheckedChange(false);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Games.playSound(@"F:\VS2017\Wolves\Sound\tianliangle.wav");
			if (wolvesGame.judgeGamesIsOver())//首先判断游戏是否结束,具体是谁赢，在Win窗体里的加载函数中判断
			{
				winForm.ShowDialog();
				this.Close();
				return;
			}
			wolvesGame.isNight = false;//进入白天
			changeColorInDays();
			label10TextChange();
			if (wolvesGame.deadNumberInNight[0] == -1 && wolvesGame.deadNumberInNight[1] == -1)//平安夜
			{
				label11.Text = "◆【昨晚平安夜，无人死亡】\n";
			}
			else if (wolvesGame.deadNumberInNight[0] == -1 || wolvesGame.deadNumberInNight[1] == -1)
			{//只刀或只毒，即只死亡一个
				int deadInformation = wolvesGame.deadNumberInNight[0] == -1 ? wolvesGame.deadNumberInNight[1] : wolvesGame.deadNumberInNight[0];
				label11.Text = String.Format("◆【昨晚死亡的是{0}号玩家】\n", deadInformation+1);
				if (wolvesGame.days == 1 && wolvesGame.deadNumberInNight[0] != -1)
					label11.Text += String.Format("◆请{0}号玩家发表遗言\n", wolvesGame.deadNumberInNight[0]+1);
			}
			else
			{
				if (wolvesGame.deadNumberInNight[0] == wolvesGame.deadNumberInNight[1])//刀和毒给了同一个人
					label11.Text = String.Format("◆【昨晚死亡的是{0}号玩家】\n", wolvesGame.deadNumberInNight[0] + 1);
				else//刀和毒给了不同人
				{
					label11.Text = String.Format("◆【昨晚死亡的是{0}号玩家,{1}号玩家】\n", wolvesGame.deadNumberInNight[0] + 1, wolvesGame.deadNumberInNight[1] + 1);
				}
				if (wolvesGame.days == 1 && wolvesGame.deadNumberInNight[0] != -1)//第一天夜晚死亡可以发表遗言
					label11.Text += String.Format("◆请{0}号玩家发表遗言\n", wolvesGame.deadNumberInNight[0] + 1);

			}
			if (wolvesGame.deadNumberInNight[0] ==wolvesGame. findIndexByIndentity('H'))//死亡的是猎人，白天发动技能
			{
				radioVisibleChange(true);
				radioTextChange("对TA使用技能");
				radioButton10.Visible = true;
				radioButton10.Text = "不使用技能";
				button5.Visible = true;
				button5.Text = "是否发动";
				label11.Text += String.Format("\n◆【技能触发】:{0}号玩家是否发动技能？", wolvesGame.deadNumberInNight[0]+1);
			}
			else//否则直接进入投票环节
			{
				label11.Text += String.Format("\n◆请所有活着玩家发言，并在发言过后进行投票！");
				radioVisibleChange(true);
				radioTextChange("是否公投TA");
				radioButton10.Visible = false;
				button5.Visible = true;
				button5.Text = "白天出局";
			}
			button4.Visible = false;
			printHeadImage();
			radioCheckedChange(false);
		}
		private void button7_Click(object sender, EventArgs e)
		{
			radioVisibleChange(true);
			radioTextChange("是否击杀");
			radioButton10.Visible = true;
			radioButton10.Text = "今晚空刀";
			label11.Text = "◆狼人请睁眼，并确定今晚的击杀目标";
			button7.Visible = false;
			button2.Visible = true;
			radioCheckedChange(false);
		}

		private void label10_Click(object sender, EventArgs e)
		{

		}
	}
}

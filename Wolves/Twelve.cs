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
	public partial class twelve : Form
	{
		public twelve()
		{
			InitializeComponent();
		}
		static Games twelveGames;
		public static Form winForm = new twelveWin();
		private void printHeadImage()
		{
			PictureBox[] pic = new PictureBox[] { pictureBox1,pictureBox2,pictureBox3
			,pictureBox4,pictureBox5,pictureBox6,pictureBox7,pictureBox8,pictureBox9,pictureBox10,pictureBox11,pictureBox12};
			for (int i = 0; i < pic.Length; i++)
				pic[i].Image = Image.FromFile(twelveGames.getWhichImage(i));
		}
		private void labelForeColorChange(Color co)
		{
			Label[] allLabel = new Label[] { label1, label2, label3, label4,
				label5, label6, label7, label8, label9, label10, label11,label12,label13,label14 };
			for (int i = 0; i < allLabel.Length; i++)
				allLabel[i].ForeColor = co;
		}
		private RadioButton[] getTenAllRadioButton()
		{
			RadioButton[] allRadioButton = new RadioButton[] { radioButton1,radioButton2,radioButton3,
			radioButton4,radioButton5,radioButton6,radioButton7,radioButton8,radioButton9,radioButton10,
			radioButton11,radioButton12,radioButton13};
			return allRadioButton;
		}
		private void radioForeColorChange(Color co)//改变颜色13个radio都要改，改变文字9个跟另一个单独起来
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < allRadioButton.Length; i++)
				allRadioButton[i].ForeColor = co;
		}
		private void radioVisibleChange(bool flag)
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < Games.playerNumbers; i++)
			{
				if (twelveGames .allPlayerInformation[i].isDeath != 0)
					allRadioButton[i].Visible = false;
				else
					allRadioButton[i].Visible = flag;
			}
		}
		private void radioCheckedChange(bool flag)
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < allRadioButton.Length; i++)
				allRadioButton[i].Checked = flag;
		}
		private void radioTextChange(String str)
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			for (int i = 0; i < allRadioButton.Length; i++)
				allRadioButton[i].Text = str;
		}
		public void changeColorInNight()
		{
			labelForeColorChange(Color.White);
			radioVisibleChange(true);
			radioForeColorChange(Color.White);
			Form1.twelveForm.BackColor = Color.Black;
		}
		public void changeColorInDays()
		{
			radioForeColorChange(Color.Black);
			labelForeColorChange(Color.Black);
			radioVisibleChange(false);
			Form1.twelveForm.BackColor = Color.White;
		}
		private int getCheckedRadioIndex(RadioButton[] all)
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
			return twelveGames.getAllPlayerInformation();
		}
		private void label13TextChange()
		{
			String label13Str = String.Format("第{0}天  ", twelveGames.days);
			if (twelveGames.isNight)
				label13Str += "夜晚";
			else
				label13Str += "白天";
			label13.Text = label13Str;
		}
		private void twelve_Load(object sender, EventArgs e)
		{
			twelveGames = new Games();
			twelveGames.setAllPlayersIdentity();
			twelveGames.randAllocateIdentity();
			label13.Text = "请上帝根据身份发牌";
			printHeadImage();
			button1.Text = "天黑了";
			button1.Visible = true;
			button2.Visible = false;
			button3.Visible = false;
			button4.Visible = false;
			button5.Visible = false;
			button6.Visible = false;
			button7.Visible = false;
			button8.Visible = false;
			button9.Visible = false;
			button10.Visible = false;
			radioVisibleChange(false);
			radioButton13.Visible = false;
			label14.Visible = false;
			changeColorInDays();
		}

		private void button1_Click(object sender, EventArgs e)//天黑
		{
			//Games.playSound(@"F:\VS2017\Wolves\Sound\tianhei.wav");
			twelveGames.isNight = true;
			if (twelveGames.judgeGamesIsOver())//首先判断游戏是否结束,具体是谁赢，在Win窗体里的加载函数中判断
			{
		
				winForm.ShowDialog();
				this.Close();
				return;
			}
			twelveGames.days++;//天黑加一天
			label13TextChange();
			changeColorInNight();
		
			radioVisibleChange(true);
			radioTextChange("守护TA");
			radioButton13.Visible = true;
			radioButton13.Text = "今晚空守";
			if (twelveGames.GuardNumber >= 0 && twelveGames.GuardNumber < Games.playerNumbers)
			{//守卫不能连续两天守同一个人
				RadioButton[] allRadioButton = new RadioButton[13];
				allRadioButton = getTenAllRadioButton();
				allRadioButton[twelveGames.GuardNumber].Visible = false;
			}
			label14.Visible = true;
			label14.Text = "◆守卫请睁眼，并选择一位玩家，并守护TA（提示：不能连续守护同一名玩家）\n";
			if (twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].isDeath != 0)
			{
				radioVisibleChange(false);
				radioButton13.Visible = true;
				radioButton13.Text = "守卫已死亡";
				label14.Text = "◆【守卫已死亡！】\n"+label14.Text;
			}
			button1.Visible = false;
			button2.Visible = true;
			button2.Text = "守卫请闭眼";
			radioCheckedChange(false);
		}
		private void button2_Click(object sender, EventArgs e)//守卫请闭眼
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			int guardNumber = getCheckedRadioIndex(allRadioButton);
			if (guardNumber == -1)
			{
				label14.Text = "【没有选择！！】\n请选择一项！";
				return;
			}
			if (guardNumber >= 0 && guardNumber < Games.playerNumbers)
				twelveGames.GuardNumber = guardNumber;
			else
				twelveGames.GuardNumber = -1;
			radioVisibleChange(false);
			radioButton13.Visible = false;
			label14.Text = "◆预言家请睁眼，请选择一名玩家来验明TA的身份";
			if (twelveGames .allPlayerInformation[twelveGames .findIndexByIndentity('Y')].isDeath != 0)
			{//预言家在当夜死亡还是能验人，在不是当夜死亡不能验人
				label14.Text = "◆【预言家已死亡!】\n" + label14.Text;
			}
			button2.Visible = false;
			button3.Visible = true;
			button3.Text = "预言家请闭眼";
			radioCheckedChange(false);
		}
		private void button3_Click(object sender, EventArgs e)//预言家请闭眼
		{
			radioVisibleChange(true);
			radioTextChange("是否杀死TA");
			radioButton13.Visible = true;
			radioButton13.Text = "今晚空刀";
			label14.Text = "◆狼人请睁眼，请确定今晚的击杀目标\n";
			button3.Visible = false;
			button4.Visible = true;
			button4.Text = "狼人请闭眼";
		}
		private void button4_Click(object sender, EventArgs e)//狼人请闭眼
		{

			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			int deadNumberInNightByLangren = getCheckedRadioIndex(allRadioButton);
			if (deadNumberInNightByLangren < 0)//有13号按钮
			{
				label14.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			radioVisibleChange(false);//先不出现选择解药
			radioButton13.Visible = true;
			radioButton13.Text = "不使用解药";
			if (deadNumberInNightByLangren < Games.playerNumbers)//配置女巫使用解药的环境
			{
				label14.Text = String.Format("◆【死亡的是{0}号玩家！】\n◆你有一瓶解药要用吗？（提示：女巫解药用完后上帝不再向女巫透露晚上死亡玩家信息!）", deadNumberInNightByLangren + 1);
				twelveGames.allPlayerInformation[deadNumberInNightByLangren].isDeath = 1;
				twelveGames.deadNumberInNight[0] = deadNumberInNightByLangren;
				int nuwuAbility = twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability;
				if (twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].isDeath == 0/*女巫活着*/||
					twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].isDeath == 1 &&
					 twelveGames.days == 1 && deadNumberInNightByLangren == twelveGames.findIndexByIndentity('N')/*女巫第一天可以自救*/)
				{
					if (nuwuAbility == Games.BOATHHAVE || nuwuAbility == Games.ONLYANTIDOTE)//女巫有解药，只有有解药的情况下，才能选取是否救TA
					{
						if (deadNumberInNightByLangren != twelveGames.findIndexByIndentity('N')
						|| twelveGames.days == 1 && deadNumberInNightByLangren == twelveGames.findIndexByIndentity('N'))//只有在第一夜能自救
						{
							allRadioButton[deadNumberInNightByLangren].Visible = true;
							allRadioButton[deadNumberInNightByLangren].Text = "是否救TA";
						}
						else
						{
							label14.Text = "◆【解药无法使用!】（女巫只有第一天夜晚能自救）\n";
						}
					}
					else
					{
						radioButton13.Text = "解药已用完 ";
						label14.Text = String.Format("◆【解药已用完！】\n") + label14.Text;
					}
				}
				else
				{
					radioButton13.Text = "女巫已死亡 ";
					label14.Text = "◆【女巫死亡!】\n不能使用药！" + label14.Text;
				}
			}
			else
			{
				label14.Text = String.Format("◆【无人死亡！】\n（提示：女巫解药用完后上帝不再向女巫透露晚上死亡玩家信息！）");

			}
			button4.Visible = false;
			button5.Visible = true;
			button5.Text = "女巫用解药";
			printHeadImage();
			radioCheckedChange(false);//在每个按钮的最后把选择取消
		}
		private void button5_Click(object sender, EventArgs e)//女巫用解药和守卫的守护结果（狼人杀过人了）
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			int checkedRadioNumber = getCheckedRadioIndex(allRadioButton);
			if (checkedRadioNumber < 0)
			{
				label14.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			int tempDeadNumberInNight = twelveGames.deadNumberInNight[0];//用来保存狼人刀人的序号，防止守卫守护后改掉deadNumberInNight[0]
			if (twelveGames.GuardNumber == twelveGames.deadNumberInNight[0]&&twelveGames.GuardNumber!=-1)//当晚守卫守中人了,也有可能是没守人和没救人
			{
				twelveGames.allPlayerInformation[twelveGames.deadNumberInNight[0]].isDeath = 0;
				twelveGames.deadNumberInNight[0] = -1;//昨晚没有死人
				
			}
			if (tempDeadNumberInNight == checkedRadioNumber)
			{//要能选中，则说明女巫有解药
				if (twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability == Games.BOATHHAVE)
					twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability = Games.ONLYPOISION;
				else
					twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability = Games.NONABILITY;
				twelveGames.allPlayerInformation[tempDeadNumberInNight].isDeath = 0;//女巫救人了
				twelveGames.deadNumberInNight[0] = -1;//昨晚没有死人
				
				twelveGames.nuwuUseAntidoteDay = twelveGames.days;//女巫使用解药的天数
				if (twelveGames.GuardNumber == checkedRadioNumber)
				{
					twelveGames.allPlayerInformation[tempDeadNumberInNight].isDeath = 1;//同守同救
					twelveGames.deadNumberInNight[0] = checkedRadioNumber;
				}
			}
			//配置女巫使用毒药的环境
			int nuwuAbility = twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability;
			radioButton13.Visible = true;

			if (twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].isDeath == 0 /*女巫活着*/||
					twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].isDeath == 1 && twelveGames.deadNumberInNight[0] == twelveGames.findIndexByIndentity('N'))
			{//女巫活着，或女巫当晚死亡
				if (nuwuAbility == Games.BOATHHAVE || nuwuAbility == Games.ONLYPOISION)
				{
					if (twelveGames.days == twelveGames.nuwuUseAntidoteDay)
					{
						radioVisibleChange(false);
						label14.Text = "◆【不能在一天夜里同时使用毒药和解药！】（选择不使用毒药）";
					}
					else
					{
						radioVisibleChange(true);
						if (twelveGames.deadNumberInNight[0] >= 0 && twelveGames.deadNumberInNight[0] < Games.playerNumbers)
							allRadioButton[twelveGames.deadNumberInNight[0]].Visible = true;
						radioTextChange("是否毒TA");
						label14.Text = "◆你有一瓶毒药你要用吗？\n";
					}
				}
				else
				{
					radioVisibleChange(false);
					label14.Text = "◆【用过毒药！】（点击：选择用毒药）\n";
				}
			}
			else
			{
				radioVisibleChange(false);
				radioButton13.Visible = true;
				label14.Text = "◆【女巫已死亡!】\n不能使用毒药！";
			}
			radioButton13.Text = "不使用毒药";
			button5.Visible = false;
			button6.Visible = true;
			button6.Text = "女巫用毒药";
			printHeadImage();
			radioCheckedChange(false);
		}
		private void button6_Click(object sender, EventArgs e)
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			int deadNumberByNuwuPoision = getCheckedRadioIndex(allRadioButton);
			if (deadNumberByNuwuPoision < 0 || deadNumberByNuwuPoision > Games.playerNumbers)
			{
				label14.Text = "◆没有选择！！请选择一项";
				return;
			}
			if (deadNumberByNuwuPoision >= 0 && deadNumberByNuwuPoision < Games.playerNumbers)//女巫使用完毒药
			{
				twelveGames.deadNumberInNight[1] = deadNumberByNuwuPoision;
				twelveGames.allPlayerInformation[deadNumberByNuwuPoision].isDeath = 1;
				if (twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability == Games.BOATHHAVE)
					twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability = Games.ONLYANTIDOTE;
				else
					twelveGames.allPlayerInformation[twelveGames.findIndexByIndentity('N')].ability = Games.NONABILITY;
			}
			radioVisibleChange(false);
			radioButton13.Visible = false;
			button6.Visible = false;
			button7.Visible = true;
			button7.Text = "天亮了";
			label14.Text = "◆猎人请睁眼……\n◆猎人请闭眼。\n◆天亮了~~ ";
			printHeadImage();
			radioCheckedChange(false);
		}
		private void setNightToDayEnvironment()//配置天亮的环境
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			label14.Text = twelveGames.getNightInformation();//输出夜晚信息
			int deadNumber = twelveGames.deadNumberInNight[0];
			if (twelveGames.deadNumberInNight[0] == twelveGames.findIndexByIndentity('H'))//死亡的是猎人，白天发动技能
			{
				radioVisibleChange(true);
				radioTextChange("对TA使用技能");
				radioButton13.Visible = true;
				radioButton13.Text = "不使用技能";
				button8.Visible = true;
				button8.Text = "是否发动";
				label14.Text += String.Format("\n◆【技能触发】:{0}号玩家是否发动技能？\n", twelveGames.deadNumberInNight[0] + 1);
			}
			else//否则直接进入投票环节
			{
				label14.Text += String.Format("\n◆请所有活着玩家发言，并在发言过后进行投票！（警长指定从警左或警右）\n");
				radioVisibleChange(true);
				radioTextChange("是否公投TA");
				radioButton13.Visible = true;
				radioButton13.Text = "狼人自爆";
				button8.Visible = true;
				button8.Text = "白天出局";
			}
		}
		private void button7_Click(object sender, EventArgs e)//天亮了
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			//Games.playSound(@"F:\VS2017\Wolves\Sound\tianliangle.wav");
			if (twelveGames.judgeGamesIsOver())//首先判断游戏是否结束,具体是谁赢，在Win窗体里的加载函数中判断
			{
				winForm.ShowDialog();
				this.Close();
				return;
			}
			twelveGames.isNight = false;//进入白天
			changeColorInDays();
			label13TextChange();
			int deadNumber = twelveGames.deadNumberInNight[0];
			if (twelveGames.days == 1)//第一天竞选警长
			{

				button9.Visible = true;
				button9.Text = "竞选警长";
				radioVisibleChange(true);
				if(deadNumber>=0&&deadNumber<Games.playerNumbers)//有人死亡
					allRadioButton[deadNumber].Visible = true;//第一夜死亡也可以竞选警长
				radioTextChange("选TA为警长");
				label14.Text = "◆【开始竞选警长！】";
				radioButton13.Visible = true;
				radioButton13.Text = "狼人自爆";
			}
			1:在三处移交警徽：【竞选警长后，天亮了，白天投票】。
			2：白天投票要有狼人自爆！自爆之后进入天黑，且自爆的狼人死亡
			//else if(deadNumber >= 0 && deadNumber < Games.playerNumbers&&twelveGames.allPlayerInformation[deadNumber].isSergeant==true)
			//{//有人死亡
			//	radioVisibleChange(true);
			//	radioTextChange("警徽交给TA");
			//	allRadioButton[].Visible = false;
			//	radioButton13.Visible = true;
			//	radioButton13.Text = "撕掉警徽";
			//	button10.Visible = true;
			//	button10.Text = "移交警徽";

			//}
			else
				setNightToDayEnvironment();
			button7.Visible = false;
			printHeadImage();
			radioCheckedChange(false);
		}

		private void button8_Click(object sender, EventArgs e)//白天出局
		{
			RadioButton[] allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			int deadNumber = getCheckedRadioIndex(allRadioButton);
			if (deadNumber < 0)
			{
				label14.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			//if (twelveGames.allPlayerInformation[deadNumber].isSergeant == true)
			//{
			//	radioVisibleChange(true);
			//	radioTextChange("警徽交给TA");
			//	allRadioButton[deadNumber].Visible = false;
			//	radioButton13.Visible = true;
			//	radioButton13.Text = "撕掉警徽";
			//	button10.Visible = true;
			//	button10.Text = "移交警徽";
			//}
			if (twelveGames.deadNumberInNight[0] == twelveGames.findIndexByIndentity('H'))//如果晚上死的是猎人，发动技能按钮不消失
			{
				twelveGames.deadNumberInNight[0] = -1;//不会在下次点击按钮再进入这个事件
				if (deadNumber >= 0 && deadNumber < Games.playerNumbers)
				{
					twelveGames.allPlayerInformation[deadNumber].isDeath = 1;
				}
				radioVisibleChange(true);
				radioTextChange("是否公投TA");
				radioButton13.Visible = true;
				radioButton13.Text = "狼人自爆";
				button8.Visible = true;
				button8.Text = "白天出局";
				label14.Text = "◆请所有活着玩家发言，并在发言过后进行投票！\n";
				printHeadImage();
				radioCheckedChange(false);
				return;

			}//不是猎人，则正常投票
			button8.Visible = false;
			button1.Visible = true;
			radioVisibleChange(false);
			radioButton13.Visible = false;
			if (deadNumber >= 0 && deadNumber < Games.playerNumbers)
			{
				twelveGames.allPlayerInformation[deadNumber].isDeath = 2;
				label14.Text = String.Format("◆{0}号玩家请发表遗言\n", deadNumber + 1);
				if (twelveGames.allPlayerInformation[deadNumber].identity == 'H')//猎人白天被投，发动技能后按钮不消失
				{
					radioVisibleChange(true);
					label14.Text = label14.Text + String.Format("◆【技能触发】:{0}号玩家是否发动技能？", deadNumber + 1);
					button8.Visible = true;//白天死亡按钮不消失，天黑按钮也不出现
					button1.Visible = false;
					button8.Text = "是否发动技能";
					radioButton13.Visible = true;
					radioButton13.Text = "不发动技能";
					allRadioButton[deadNumber].Visible = false;
				}
			}
			printHeadImage();
			radioCheckedChange(false);
		}

		private void button9_Click(object sender, EventArgs e)//警长竞选
		{
			RadioButton []allRadioButton = new RadioButton[13];
			allRadioButton = getTenAllRadioButton();
			int checkedNumber = getCheckedRadioIndex(allRadioButton);
			if (checkedNumber < 0)
			{
				label14.Text = "◆【没有选择!】\n请选择一项!";
				return;
			}
			if (checkedNumber >= 0 && checkedNumber < Games.playerNumbers)
			{
				PictureBox[] allPicture = { pictureBox1,pictureBox2
				,pictureBox3,pictureBox4,pictureBox5,pictureBox6,pictureBox7,pictureBox8,pictureBox9
				,pictureBox10,pictureBox11,pictureBox12};
				twelveGames.allPlayerInformation[checkedNumber].isSergeant = true;
				allPicture[checkedNumber].Size = new Size(160, 130);
				allPicture[checkedNumber].BackColor = Color.Red;
			}
			else if (checkedNumber == Games.playerNumbers)
			{
				radioVisibleChange(false);
				radioButton13.Visible = false;
				label14.Text = "◆直接进入天黑！";
				button1.Visible = true;
				button9.Visible = false;
				return;
			}
			setNightToDayEnvironment();//为白天配置环境
			button9.Visible = false;
			button8.Visible = true;
			radioCheckedChange(false);
		}
	}
}

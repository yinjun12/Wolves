using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//0代表没有能力，1代表预言家，2代表解药毒药都在，-1代表只有解药，-2代表只有毒药，
//3代表猎人，4代表守卫守护，5代表狼人杀人
namespace Wolves
{
	class PlayerInfo
	{
		public char identity;//身份:狼人，平民，神
		public int ability;//能力
		public int isDeath;//1是夜晚死亡，2是白天死亡，0是活着
		public bool isAttendElection;//是否竞选
		public bool isSergeant;//是不是警长
		public PlayerInfo(char id, int ab)
		{
			identity = id;
			ability = ab;
			isDeath =0 ;
			isAttendElection = false;
			isSergeant = false;
		}
	}
	class Games
	{
		public static int ONLYPOISION = -2, ONLYANTIDOTE = -1, NONABILITY = 0, CHECKIDENTITY = 1;
		public static int BOATHHAVE = 2, GUN = 3, GUARD = 4, KILLINNIGHT = 5;
		public static int playerNumbers = 0;//游戏人数
		public PlayerInfo[] allPlayerInformation;//玩家身份信息
		public int days = 0;//天数
		public bool isNight = true;//白天还是夜晚
		public static bool isWolvesWin = true;//狼人赢还是好人赢
		public int nuwuUseAntidoteDay = 0;//女巫用解药的天数
		public int GuardNumber = -1;//守卫守护的序号,-1代表空守
		public int []deadNumberInNight = new int[] { -1,-1};//第一个代表当晚被狼人人杀的编号，第二个代表当晚被毒的编号
		private void setWolvesIdentity()
		{
			allPlayerInformation[0] = new PlayerInfo('L', KILLINNIGHT);
			allPlayerInformation[1] = new PlayerInfo('L', KILLINNIGHT);
			allPlayerInformation[2] = new PlayerInfo('L', KILLINNIGHT);
		}
		private void setCivialIdentity()
		{
			allPlayerInformation[3] = new PlayerInfo('P', NONABILITY);
			allPlayerInformation[4] = new PlayerInfo('P', NONABILITY);
			allPlayerInformation[5] = new PlayerInfo('P', NONABILITY);
		}
		private void setGodIdentity()
		{
			allPlayerInformation[6] = new PlayerInfo('Y', CHECKIDENTITY);//预言家
			allPlayerInformation[7] = new PlayerInfo('N', BOATHHAVE);//女巫
			allPlayerInformation[8] = new PlayerInfo('H', GUN);//猎人
		}
		private void setMoreNinePlayerIdentity()//设置超过9人的身份，即12人
		{
			allPlayerInformation[9] = new PlayerInfo('L', KILLINNIGHT);
			allPlayerInformation[10] = new PlayerInfo('P', NONABILITY);
			allPlayerInformation[11] = new PlayerInfo('S', GUARD);//守卫

		}
		public void setAllPlayersIdentity()
		{
			allPlayerInformation = new PlayerInfo[playerNumbers];
			setCivialIdentity();
			setWolvesIdentity();
			setGodIdentity();
			if (playerNumbers > 9)
				setMoreNinePlayerIdentity();
		}
		public  bool judgeGamesIsOver()
		{
			int wolvesNumber = 0, godNumber = 0, civilNumber = 0;//计算活着的神民狼的个数
			for (int i = 0; i < allPlayerInformation.Length; i++)
			{
				if (allPlayerInformation[i].isDeath ==0)
				{
					if (allPlayerInformation[i].identity == 'L')
						wolvesNumber++;
					else if (allPlayerInformation[i].identity == 'P')
						civilNumber++;
					else
						godNumber++;
				}
			}
			if (wolvesNumber == 0)
			{
				isWolvesWin = false;
				return true;
			}
			else if (civilNumber == 0 || godNumber == 0)
			{
				isWolvesWin = true;
				return true;
			}
			if (wolvesNumber >= civilNumber + godNumber)
			{
				isWolvesWin = true;
				return true;
			}
			return false;
		}
		public void randAllocateIdentity()
		{
			Random rand = new Random(DateTime.Now.Millisecond);
			for (int i = 0; i < allPlayerInformation.Length; i++)
			{
				int x, y; PlayerInfo t;
				x = rand.Next(0, allPlayerInformation.Length);
				do { y = rand.Next(0, allPlayerInformation.Length); }
				while (y == x);
				t = allPlayerInformation[x];
				allPlayerInformation[x] = allPlayerInformation[y];
				allPlayerInformation[y] = t;
			}
		}
		public static void playSound(String str)
		{
			System.Media.SoundPlayer player = new System.Media.SoundPlayer();
			player.SoundLocation = str;
			player.Load();
			player.PlaySync();
		}
		public  String selectDeathImage(int i, String str)
		{
			if (allPlayerInformation[i].isDeath == 0)
				return String.Format(@"F:\VS2017\Wolves\image\{0}.jpg", str);
			if (allPlayerInformation[i].isDeath == 1)
				return String.Format(@"F:\VS2017\Wolves\image\{0}deadinnight.jpg", str);
			if (allPlayerInformation[i].isDeath == 2)
				return String.Format(@"F:\VS2017\Wolves\image\{0}deadinday.jpg", str);
			return "";
		}
		public String getWhichImage(int i)
		{
			if (allPlayerInformation[i].identity == 'L')
			{
				return selectDeathImage(i, "langren");
			}
			if (allPlayerInformation[i].identity == 'P')
			{
				return selectDeathImage(i, "cunming");
			}
			if (allPlayerInformation[i].identity == 'Y')
				return selectDeathImage(i, "yuyanjia");
			if (allPlayerInformation[i].identity == 'N')
				return selectDeathImage(i, "nuwu");
			if (allPlayerInformation[i].identity == 'H')
				return selectDeathImage(i, "lieren"); ;
			if (allPlayerInformation[i].identity == 'S')
				return selectDeathImage(i, "shouwei"); ;
			return "";
		}
		public  String getAllPlayerInformation()
		{
			String resString = "";
			for (int i = 0; i < allPlayerInformation.Length; i++)
			{
				String tempString = "";
				char identity = allPlayerInformation[i].identity;
				switch (identity)
				{
					case 'P':
						tempString = "平民";
						break;
					case 'L':
						tempString = "狼人";
						break;
					case 'N':
						tempString = "女巫";
						break;
					case 'Y':
						tempString = "预言家";
						break;
					case 'H':
						tempString = "猎人";
						break;
					case 'S':
						tempString = "守卫";
						break;
				}
				resString += String.Format("{0}号玩家：{1}\n\n", i + 1, tempString);
			}
			return resString;
		}
		public int findIndexByIndentity(char ch)
		{
			int index = 0;
			for (; index < allPlayerInformation.Length; index++)
			{
				if (allPlayerInformation[index].identity == ch)
					break;
			}
			return index;
		}
		public String getNightInformation()
		{
			String returnedString = "";
			if (deadNumberInNight[0] == -1 && deadNumberInNight[1] == -1)//平安夜
			{
				returnedString = "◆【昨晚平安夜，无人死亡】\n";
			}
			else if (deadNumberInNight[0] == -1 || deadNumberInNight[1] == -1)
			{//只刀或只毒，即只死亡一个
				int deadInformation = deadNumberInNight[0] == -1 ? deadNumberInNight[1] : deadNumberInNight[0];
				returnedString = String.Format("◆【昨晚死亡的是{0}号玩家】\n", deadInformation + 1);
				if (days == 1 && deadNumberInNight[0] != -1)
					returnedString += String.Format("◆请{0}号玩家发表遗言\n",deadNumberInNight[0] + 1);
			}
			else
			{
				if (deadNumberInNight[0] == deadNumberInNight[1])//刀和毒给了同一个人
					returnedString = String.Format("◆【昨晚死亡的是{0}号玩家】\n", deadNumberInNight[0] + 1);
				else//刀和毒给了不同人
				{
					returnedString = String.Format("◆【昨晚死亡的是{0}号玩家,{1}号玩家】\n", deadNumberInNight[0] + 1, deadNumberInNight[1] + 1);
				}
				if (days == 1 && deadNumberInNight[0] != -1)//第一天夜晚死亡可以发表遗言
					returnedString += String.Format("◆请{0}号玩家发表遗言\n", deadNumberInNight[0] + 1);
			}
			return returnedString;
		}
	}
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
	
}

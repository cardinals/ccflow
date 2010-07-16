using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BP.CTI
{
	public class tw16vid
	{
		#region TV
		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <returns>����ͨ����</returns>
		[DllImport("tw16vid.dll")]
		public static extern int TV_Installed();
		/// <summary>
		/// ��ʼ������
		/// </summary>
		[DllImport("tw16vid.dll")]
		public static extern void TV_Initialize();

		/// <summary>
		/// �õ����кź���
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		[DllImport("tw16vid.dll")]
		public static extern int TV_GetSerial(byte[] s);
		#endregion
		 
		#region Ӧ��
		/// <summary>
		/// �õ����к�
		/// </summary>
		public static void GetSerial()
		{
			byte[] bs = new byte[20];
			char[] cs = new char[20];
			string s;

			if (TV_Installed() > 0)
			{
				TV_Initialize();
				TV_GetSerial(bs);
				for(int i = 0; i < cs.Length ; i++)
					cs[i] = Convert.ToChar(bs[i]);					;
				s = new string(cs);
				//return s;
				//MessageBox.Show(s);
				//Console.WriteLine(s);
			}
			else
			{
				throw new Exception("δ��װ����!!!");
			}
		}
		#endregion

	}
}
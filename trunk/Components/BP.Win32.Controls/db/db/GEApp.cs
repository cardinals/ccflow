using System;

namespace BP.CTI
{
	/// <summary>
	/// App ��ժҪ˵����
	/// </summary>
	public class GEApp
	{
		


		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public static void StartCall()
		{

			Card.Initialize();
			
			pcmn7api.PCM7_CompressRatio(64);
			pcmn7api.PCM7_CallOutPara(1, "0x18", "0x400", 0);

			while(true)
			{
				// �õ�һ�����е�ͨ����




			}
			// api PCM7_CompressRatio (RATE_64K);
		}
		/// <summary>
		/// ֹͣ
		/// </summary>
		public static void Disable()
		{
		}
		#endregion

		#region ����
		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="ch"></param>
		/// <param name="tel"></param>
		/// <param name="file"></param>
		public static void Call(int ch, string tel,string file)
		{
		}
		#endregion

		
	}
}

using System;
using System.Runtime.InteropServices;
using BP.CTI.App;
using BP.DA;


namespace BP.CTI
{
	/// <summary>
	/// ����
	/// </summary>
	public class TVCard : Card
	{
		/// <summary>
		/// �Ƿ������˼��Է�ת
		/// һ��Ķ�û����������
		/// </summary>
		public static bool  ReverseFlag=false;
		/// <summary>
		/// /
		/// </summary>
		public static int  SIG_SILENCE=64;
		/// <summary>
		/// �Ⲧ����
		/// </summary>
		public static int  SIG_DIAL=65;
		/// <summary>
		/// �Ƿ�ȫ���Ķ��ǿ��е�
		/// </summary>
		public static bool IsAllCHFree
		{
			get
			{
				foreach(TVCH ch in TVCard.HisCHs)
				{
					if ( ch.CHState==TVCHState.Dialing)
						return false;
					if ( ch.CHState==TVCHState.Connect)
						return false;
					if ( ch.CHState==TVCHState.Play)
						return false;
				}
				return true;
			}
		}

		#region ϵͳ����
		 
		/// <summary>
		/// ͨ��
		/// </summary>
		public static TVCHs HisCHs=null;
		
		#region count
		/// <summary>
		/// �õ�ch״̬�ĸ�.
		/// </summary>
		/// <param name="state">CHState</param>
		/// <returns>num</returns>
		public static int GetCHsCount(TVCHState state)
		{
			if (HisCHs==null)
				return 0;
			int i=0;
			foreach(TVCH ch in HisCHs)
			{
				if (ch.CHState==state)
					i++;
			}
			return i;
		}
		#endregion
		 
		#endregion

		#region ���Ʒ���	
		
		 
		/// <summary>
		/// ��ʹ��������.
		/// </summary>
		/// <returns></returns>
		public static void Initialize()
		{
			Card.beforeInitialize(); //���û����ʼ��

			Card.ChCount=pcmn7api.TV_Installed();

			if ( Card.ChCount <= 0)
			{
				throw new Exception("δ��װ����!!!");
			}

			 
			if (pcmn7api.TV_Initialize(0) < 0)
			{
				string msg="����TV_Initialize errornum = "+pcmn7api.TV_Initialize(0);
				Log.DefaultLogWriteLineError(msg);
				Card.ChCount=0;
				throw new Exception(msg);
			}

			
			// init chs.
			TVCard.HisCHs = new TVCHs();			 

			for(int i=0;  i<  Card.ChCount; i++ )
			{
				TVCard.HisCHs.Add(new TVCH( i ));
			}

			Card.HisCardWorkState =CardWorkState.Runing;


			// ���ֿ���ģ�⿨����ͬһ��
			Card.CompressRatio(0); // ��������ѹ��������

			//  ����Ƿ���¼�˴˰忨�ɣ�

//			if (Card.MySerial.IndexOf( Card.Serial )==-1)
//			{
//				//TVCard.HisCHs =null;
//				Log.DefaultLogWriteLineInfo("����������"+Card.Serial);
//			}

			Log.DefaultLogWriteLineInfo("��������ʼ���ɹ�!Serial:"+Card.Serial);

		 




			
		}
		
		#endregion

	 
		public static TVCH GetHangUpCH()
		{
			foreach(TVCH ch in TVCard.HisCHs )
			{
				if (ch.CHState==TVCHState.HangUp)
					return ch;
			}
			return null;
		}
		#region ����
		public static void DoCall(int chNum, CallList cl)
		{	
			Initialize(); // ��ʼ������.

			CH ch =new CH(chNum);
			ch.Call(cl); // ������.

			DoCallRemainder();

			Card.Disable();
		}
	  
		/// <summary>
		/// ����
		/// </summary>
		public static void DoCall()
		{	
			TVCard.Initialize(); // ��ʼ������.

		   Card.GetCurrentContextByTelType(0); //  GetCurrentContextByTelType(int teltype)

			//���빤��״̬
			while(true)
			{
				if (Card.HisCardWorkState ==CardWorkState.Pause)
				{
					DoCallRemainder(); //ִ�к���ʣ��ģ�
				}
				while (Card.HisCardWorkState ==CardWorkState.Pause)
				{
					/* �������ͣ״̬����������ѭ����*/
					DoCallRemainder(); //ִ�к���ʣ��ģ�	
					System.Threading.Thread.Sleep(1000); // ���������˷���Դ�����������ߡ�
				}
				if (Card.HisCardWorkState ==CardWorkState.Stop)
				{
					/* �����ֹͣ���� */
					DoCallRemainder(); //ִ�к���ʣ��ģ�
					break;
				}

				TVCH ch =TVCard.GetHangUpCH(); //ȡ��һ�����е�ͨ��.
				if (ch!=null)
				{
					/* ����п��е�ͨ�� */
					CallList cl= CallLists.GetCall(); // �õ�һ��������
					if (cl==null)
					{
						/*  ���û�п��Ժ����ĵ绰 */
						DoCallRemainder(); //ִ�к���ʣ��ģ�
						System.Threading.Thread.Sleep(3000); // ֹͣ1��,�������ж��Ƿ��п��Ժ�������Ϣ��
					}
					else
					{
						//cl.DoCalling(Card.CurrentCallContext.Context);  // ����״̬Ϊ����װ̬.
						ch.Call(cl); // ������.
						//Log.DebugWriteInfo("ʹ��CH="+ch.No+"����Tel="+cl.Tel);
					}
				}
				// ִ�к�������.
				TVCard.CallOn();
			}

			// ����������,�������ں���������.			
			Card.HisCardWorkState=CardWorkState.Stop;
			Card.Disable(); // ֹͣ�忨�Ĺ���.
		}
		
		#endregion

	}

}
using System;
using System.Runtime.InteropServices;
using BP.CTI.App;
using BP.DA;


namespace BP.CTI
{
	/// <summary>
	/// �忨����״̬
	/// </summary>
	public enum CardWorkState
	{
		/// <summary>
		/// ����
		/// </summary>
		Runing,
		/// <summary>
		/// ֹͣ
		/// </summary>
		Stop,
		/// <summary>
		/// ������ͣ
		/// </summary>
		Stoping,
		/// <summary>
		/// ��ͣ
		/// </summary>
		Pause,
	}
	/// <summary>
	/// �忨����
	/// </summary>
	public enum CardType
	{
		/// <summary>
		/// δ֪
		/// </summary>
		unknown,
		/// <summary>
		/// pcmn7api
		/// </summary>
		pcmn7api,
		/// <summary>
		/// tw16vid
		/// </summary>
		tw16vid,
		/// <summary>
		/// Usbid
		/// </summary>
		Usbid
		
	}
	/// <summary>
	/// ����
	/// </summary>
	public class Card
	{
		/// <summary>
		/// ���µ���ϵͳ����
		/// </summary>
		public static void ResetParas()
		{
			Sys.SysConfig config = new BP.Sys.SysConfig("CallTel");
			DefaultCalledTel=config.Val;

			config = new BP.Sys.SysConfig("MinJE"); // ��С�ĺ������
			DefaultMinJE=float.Parse(config.Val);

			Log.DebugWriteInfo("Card ������ʼ���ɹ�...");

		}

		#region ϵͳ����
		/// <summary>
		/// �忨�Ĺ���״̬
		/// </summary>
		public static CardWorkState HisCardWorkState=CardWorkState.Stop;
		/// <summary>
		/// Ĭ�ϵ����к���
		/// </summary>
		public static string DefaultCalledTel="2251000";
		/// <summary>
		/// ��С�ĺ������
		/// </summary>
		public static float DefaultMinJE=0;  
		/// <summary>
		/// �õ���ǰ�ĺ�������
		/// </summary>
		private static CallContext _CurrentCallContext=null;
		/// <summary>
		/// �õ���ǰ�ĺ�������
		/// </summary>
		public static CallContext CurrentCallContext
		{
			get
			{
				if (_CurrentCallContext==null)
				{
					_CurrentCallContext=CallContexts.GetCurrentCallContext;
				}

				if ( !(_CurrentCallContext.DateFrom < DateTime.Now.Day && 
					_CurrentCallContext.DateTo > DateTime.Now.Day))
				{ 
					/* �����Ѿ�ȷ��Ҫ�任.*/
					_CurrentCallContext= CallContexts.GetCurrentCallContext;
				}
				return _CurrentCallContext;
			}
		}
		/// <summary>
		/// ͨ��
		/// </summary>
		public static CHs HisCHs=null;
		
		#region count
		/// <summary>
		/// �õ�ch״̬�ĸ�.
		/// </summary>
		/// <param name="state">CHState</param>
		/// <returns>num</returns>
		public static int GetCHsCount(CHState state)
		{
			if (HisCHs==null)
				return 0;
			int i=0;
			foreach(CH ch in HisCHs)
			{
				if (ch.CHState==state)
					i++;
			}
			return i;
		}
		#endregion
		/// <summary>
		/// �忨���к���
		/// </summary>
		public static string Serial
		{
			get
			{
				// ��ȡ���кš�
				byte[] bs = new byte[20];
				//char[] cs = new char[20];
				//string s;

				switch(Card.HisCardType)
				{
					case CardType.pcmn7api:
						pcmn7api.PCM7_GetSerial(bs);
						break;
					case CardType.Usbid:
						pcmn7api.TV_GetSerial(bs);
						break;
					default:
						throw new Exception("unknow card type");
				}
				return DataType.ByteToString(bs);				
			}
		}
		/// <summary>
		/// ͨ������
		/// </summary>
		public static int ChCount=0;
		/// <summary>
		/// �忨����
		/// </summary>
		public static CardType HisCardType=CardType.unknown;
		#endregion

		#region ���Ʒ���	
		
		/// <summary>
		/// ����¼������ѹ����
		/// </summary>
		/// <param name="bye">1,2,3, �ֱ���64,32,16ͨ��Ϊ 64. </param>
		public static void CompressRatio(int bye)
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					pcmn7api.PCM7_CompressRatio(bye);					
					break;
				case CardType.Usbid:
					pcmn7api.TV_CompressRatio(bye);
					break;
				default:
					throw new Exception("un know card type");
			}			
		}
		/// <summary>
		/// ��ʹ��������.
		/// </summary>
		/// <returns></returns>
		public static void Initialize()
		{
			if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
				Card.HisCardType=CardType.pcmn7api;
			else
				Card.HisCardType=CardType.Usbid;

			#region ���ݿ������л������á�
			// ��û�к����ɹ������ݺ�����ȥ.��Ӧ�ô�������������.
			DBAccess.RunSQL("update CTI_CallList set CallingState=0 where CallingState=1 or CallingState is null");
			// clear free list.
			DBAccess.RunSQL("delete CTI_CallList where tel in (select tel from CTI_Release)");

			//����ϵͳ���� 
			ResetParas(); 
			#endregion

			// ����ϵͳ���������ڵõ����кš�

			// ��ʹ��ͨ���� ���ؿ�ͨ��������
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					Card.ChCount=pcmn7api.PCM7_Installed();
					break;
				case CardType.Usbid:
					Card.ChCount=pcmn7api.TV_Installed();
					break;
				default:
					throw new Exception("unknow card type");
			}

			if ( Card.ChCount <= 0)
				throw new Exception("δ��װ����!!!");

			 
			// ˵�������ɹ���
			int i =-1;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i=pcmn7api.PCM7_Initialize();
					break;
				case CardType.Usbid:
					i=pcmn7api.TV_Initialize(0);
					break;
				default:
					throw new Exception("un know card type");
			}

			if (i < 0)
			{
				string msg="����Initialize errornum = "+i.ToString();
				Log.DefaultLogWriteLineError(msg);
				throw new Exception(msg);
			}

			
			// init chs.
			Card.HisCHs = new CHs();
			//CH ch8 =new CH(8);
			//CH ch9 =new CH(9);
			//Card.HisCHs.Add( ch8 );
			//Card.HisCHs.Add( ch9 );

			
			//for(int ch=0; ch < Card.ChCount; ch++)
			Card.HisCHs.Add(new CH(1));

			Card.HisCardWorkState =CardWorkState.Runing;


			// ���ֿ���ģ�⿨����ͬһ��
			Card.CompressRatio(0); // ��������ѹ��������

			Log.DefaultLogWriteLineInfo("��������ʼ���ɹ�!Serial:"+Card.Serial);
		}
		/// <summary>
		/// ֹͣ����������
		/// </summary>		
		public static void Disable()
		{
			// ��ֵ
			Card.ChCount=0;
			Card.HisCHs=null;

			Card.HisCardWorkState=CardWorkState.Stop;

			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					pcmn7api.PCM7_Disable();
					break;
				case CardType.Usbid:
					pcmn7api.TV_Disable();
					break;
				default:					
					return;
			}
		}
		#endregion

		#region ͨ������
		public static CH GetCallOutChn()
		{
			
			foreach(CH ch in Card.HisCHs)
			{
				if (ch.CHState==CHState.HangUp)
					return ch;		
			}
			return null;
		}

		#endregion

		#region ����
		/// <summary>
		/// ִ����ͣ
		/// </summary>
		public static void DoStop()
		{
			if (HisCardWorkState==CardWorkState.Pause
				|| HisCardWorkState==CardWorkState.Runing)
			{			
				HisCardWorkState =CardWorkState.Stop;
				DoCallRemainder(); // ����ʣ���
			}
		}
		/// <summary>
		/// ִ����ͣ
		/// </summary>
		public static void DoPause()
		{
			HisCardWorkState =CardWorkState.Pause;
			DoCallRemainder();
		}
		/// <summary>
		/// ����
		/// </summary>
		public static void DoCall()
		{	
			Card.Initialize(); // ��ʼ������.
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
					System.Threading.Thread.Sleep(10); // ���������˷���Դ�����������ߡ�
				}
				if (Card.HisCardWorkState ==CardWorkState.Stop)
				{
					/* �����ֹͣ���� */
					DoCallRemainder(); //ִ�к���ʣ��ģ�
					break;
				}

				CH ch =Card.GetCallOutChn(); //ȡ��һ�����е�ͨ��.
				if (ch!=null)
				{
					/* ����п��е�ͨ�� */
					CallList cl= CallLists.GetCall(); // �õ�һ��������
					if (cl==null)
					{
						/*  ���û�п��Ժ����ĵ绰 */
						DoCallRemainder(); //ִ�к���ʣ��ģ�
						if (SystemConfig.IsDebug)
						{
							System.Threading.Thread.Sleep(0); // ֹͣ1��,�������ж��Ƿ��п��Ժ�������Ϣ��
						}
						else
						{
							System.Threading.Thread.Sleep(0); // ֹͣ100��,�������ж��Ƿ��п��Ժ�������Ϣ��
						}
					}
					else
					{
						Log.DebugWriteInfo("doCallʹ��CH="+ch.No+"����Tel="+cl.Tel);

						cl.CallingStateOfEnum=CallingState.Calling;
						cl.Note="��ʼ����";
						cl.Update(); // �����´���ȡ����.
						ch.Call(cl); // ������.
					}
				}
				// ִ�к�������.
				Card.CallOn();
			}

			// ����������,�������ں���������.			
			Card.HisCardWorkState=CardWorkState.Stop;
			Card.Disable(); // ֹͣ�忨�Ĺ���.
		}
		/// <summary>
		/// ����
		/// </summary>
		public static void Run()
		{
			Log.DefaultLogWriteLineInfo("******before card run "+HisCardWorkState.ToString());
			while(true)
			{
				if (HisCardWorkState==CardWorkState.Runing)
				{
					/* �����ǰ��״̬�����е�. */
					DoCall();
				}				
				System.Threading.Thread.Sleep(1000);
				//Log.DefaultLogWriteLineInfo("************* end run");
			}
			Log.DefaultLogWriteLineInfo("************* end card run");
		}
		/// <summary>
		/// ִ�к���ʣ���
		/// </summary>
		public static void DoCallRemainder()
		{
			while(true)
			{
				Card.CallOn();
				if (Card.IsAllCHFree) //������ǿ��еľ�������.
					break;
			}
			System.Threading.Thread.Sleep(0); // ���������˷���Դ�����������ߡ�

		}
		public static bool IsAllCHFree
		{
			get
			{
				foreach(CH ch in Card.HisCHs)
				{
					if (ch.CHState==CHState.Connect || ch.CHState==CHState.Play)
						return false;
				}
				return true;
			}
		}
		/// <summary>
		/// ��Card��������
		/// </summary>
		public static void CallOn()
		{
			foreach(CH ch in Card.HisCHs)
				ch.CallOn();
		}
		/// <summary>
		/// ֹͣ
		/// </summary>
		public static void Stop()
		{
			Card.HisCardWorkState=CardWorkState.Stop;
		}
		/// <summary>
		/// ��ͣ
		/// </summary>
		public static void Pause()
		{
			Card.HisCardWorkState=CardWorkState.Pause;
		}		 
		#endregion

	}

}
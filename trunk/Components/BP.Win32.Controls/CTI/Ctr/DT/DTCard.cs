using System;
using System.Runtime.InteropServices;
using BP.CTI.App;
using BP.DA;


namespace BP.CTI
{ 
	/// <summary>
	/// ���ֿ�
	/// </summary>
	public class DTCard:Card
	{
		#region ϵͳ���� 
		/// <summary>
		/// �Ƿ�ȫ���Ķ��ǿ��е�
		/// </summary>
		public static bool IsAllCHFree
		{
			get
			{
				foreach(DTCH ch in DTCard.HisCHs)
				{
					if (ch.CHState==DTCHState.Connect || ch.CHState==DTCHState.Play)
						return false;
				}
				return true;
			}
		}
		
		public static DTCHs HisCHs=null;

		#region count
		/// <summary>
		/// �õ�ch״̬�ĸ�.
		/// </summary>
		/// <param name="state">CHState</param>
		/// <returns>num</returns>
		public static int GetCHsCount(DTCHState state)
		{
			if (HisCHs==null)
				return 0;
			int i=0;
			foreach(DTCH ch in DTCard.HisCHs)
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
			Card.beforeInitialize(); 

			 
			Card.ChCount=pcmn7api.PCM7_Installed();
			 

			if ( Card.ChCount <= 0)
				return;
			//throw new Exception("δ��װ����!!!");

			 
			// ˵�������ɹ���
			int i =pcmn7api.PCM7_Initialize();				
		 

			if (i < 0)
			{
				string msg="����Initialize errornum = "+i.ToString();
				Log.DefaultLogWriteLineError(msg);
				Card.ChCount=0;
				throw new Exception(msg);
			}			
		 
			DTCard.HisCHs = new DTCHs();	 

			// �����1��ʼʹ��.
			for(int ch=0; ch < Card.ChCount-1; ch++)
			{
				if (ch==32 || ch==0 || ch==1 )
					continue;
				DTCard.HisCHs.Add(new DTCH(ch));
			}

			Card.HisCardWorkState =CardWorkState.Runing;

			// ���ֿ���ģ�⿨����ͬһ��
			Card.CompressRatio(0); // ��������ѹ��������

			Log.DefaultLogWriteLineInfo("��������ʼ���ɹ�!Serial:"+Card.Serial);
		}
		 
		#endregion

		#region ͨ������
		public static DTCH GetCallOutChn()
		{
			
			foreach(DTCH ch in DTCard.HisCHs)
			{
				if (ch.CHState==DTCHState.HangUp)
					return ch;
			}
			return null;
		}

		#endregion

		#region ����
		public static void DoCall(int chNum, CallList cl)
		{	
			Initialize(); // ��ʼ������.

			CH ch =new CH(chNum);
			ch.Call(cl);  // ������.

			DoCallRemainder();

			Card.Disable();
		}

		  
		/// <summary>
		/// ����
		/// </summary>
		public static void DoCall()
		{	
			Initialize(); // ��ʼ������.

			//���빤��״̬
			while(true)
			{
				if (Card.dtOfContext==null)
				{
					Card.GetCurrentContextByTelType(0);
				}
				if ( Card.dtOfContext.Rows.Count <=0 )
				{
					/* û�е�ǰ���Ժ��������ݡ�*/
					DoCallRemainder(); //ִ�к���ʣ��ģ�
					System.Threading.Thread.Sleep(30000); // ���������˷���Դ�����������ߡ�
					continue;
				}
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

				DTCH ch = DTCard.GetCallOutChn(); //ȡ��һ�����е�ͨ��.
				if (ch!=null)
				{
					/* ����п��е�ͨ�� */
					CallList cl= CallLists.GetCall(); // �õ�һ��������
					if (cl==null)
					{
						/*  ���û�п��Ժ����ĵ绰 */
						Card.CallOn();
						//DoCallRemainder(); //ִ�к���ʣ��ģ�
						//System.Threading.Thread.Sleep(10); // ֹͣ1��,�������ж��Ƿ��п��Ժ�������Ϣ��
					}
					else
					{
						//cl.DoCalling(Card.CurrentCallContext.Context);  // ����״̬Ϊ����װ̬.
						ch.Call(cl); // ������...
					}
				}
				// ִ�к�������.
				Card.CallOn();
			}

			// ����������,�������ں���������.			
			Card.HisCardWorkState=CardWorkState.Stop;
			Card.Disable(); // ֹͣ�忨�Ĺ���.
		}
		 
		 
	   
		#endregion

	}

}
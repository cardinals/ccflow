using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	/// <summary>
	/// ͨ��״̬
	/// </summary>
	public enum CHState
	{
		/// <summary>
		/// �һ�
		/// </summary>
		HangUp,
		/// <summary>
		/// ����
		/// </summary>
		Connect,
		/// <summary>
		/// ����
		/// </summary>
		Play,
		/// <summary>
		/// ������(�������ֿ�û������.)
		/// </summary>
		Dialing,
	}
	/// <summary>
	/// CH ��ժҪ˵����
	/// </summary>
	public class CH
	{
		#region ͨ������
		/// <summary>
		/// ͨ��״̬
		/// </summary>
		public CHState CHState=CHState.HangUp;
		/// <summary>
		/// ͨ�����
		/// </summary>
		public int No=-1;
		/// <summary>
		/// ���ĺ�������
		/// </summary>
		public CallList HisCallList=null;
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public CH()
		{
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="no">���</param>
		public CH(int no)
		{
			
			this.No=no;
		}
		#endregion

		#region ͨ������

		#region ʱ�亭��		
		/// <summary>
		/// ��ʼ��ʱ
		/// </summary>
		/// <param name="mm">Ҫ�����ʱ��</param>
		protected void StartTimer(int mm)
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					pcmn7api.PCM7_StartTimer(this.No,mm);
					break;
				case CardType.Usbid:
					pcmn7api.TV_StartTimer(this.No,mm);
					break;
				default:
					throw new Exception("un know card type");
			}
		}
		/// <summary>
		/// �����Ƿ���ʱ��
		/// </summary>
		/// <returns></returns>
		protected bool TimerElapsed()
		{
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_TimerElapsed(this.No);
					Log.DebugWriteInfo("pcmn7api.PCM7_TimerElapsed(this.No)="+pcmn7api.PCM7_TimerElapsed(this.No) );
					 
					break;
				case CardType.Usbid:
					i= pcmn7api.TV_TimerElapsed(this.No);
					if (i<=0)
						return true;
					else
						return false;
					//Log.DebugWriteInfo("pcmn7api.TV_TimerElapsed(this.No)="+pcmn7api.TV_TimerElapsed(this.No) );
				default:
					throw new Exception("unknow card type");
			}
			if  (i >= this.RingLong )
				return true;
			else
				return false;
		}
		#endregion

		/// <summary>
		/// ����һ���绰
		/// </summary>
		/// <param name="tel">�绰</param>		
		/// <returns></returns>
		protected int CallOut(string tel)
		{
			// ��ʼ����һ��ͨ��
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_StartCallOut(this.No, DataType.StringToByte(tel), DataType.StringToByte(Card.DefaultCalledTel) ) ; 				 
					this.CHState=CHState.Connect; // ��������״̬, Ϊ��������״̬.
					this.StartTimer(500000); // ��ʼ���Ӽ���ʱ��.
					break;
				case CardType.Usbid:
					this.StartTimer(500000); // ��ʼ���Ӽ���ʱ��.

					pcmn7api.TV_HangUpCtrl(this.No); // ��߹һ�.

					pcmn7api.TV_OffHookCtrl(this.No); // ���з� ժ��.
					Log.DebugWriteInfo("��ʼ���� ch="+this.No+" Tel="+this.HisCallList.Tel);
					i= pcmn7api.TV_StartDial(this.No, DataType.StringToByte(tel) ) ; // ��ʼ��������绰����.
					this.CHState=CHState.Dialing ; // ��ǰ��״̬������.
					Log.DebugWriteInfo("���벦��״̬ ch="+this.No+" Tel="+this.HisCallList.Tel);
					break;
				default:
					throw new Exception("unknow card type");
			}
 
			return i;
		}
		/// <summary>
		/// ���Է��Ƿ�һ�
		/// �����Է�ռ�ߣ��պŵ�
		/// </summary>
		/// <returns>0,1</returns>
		protected bool HangUpDetect()
		{
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					if (pcmn7api.PCM7_HangUpDetect(this.No)==1)
						return true;
					else
						return false;
					 
				case CardType.Usbid:
					if (pcmn7api.TV_MonitorBusy(this.No, 1, 5) ==0
						|| pcmn7api.TV_MonitorBusy (this.No, 2, 5)==0 ) 
						return false;
					else
						return true;
				default:
					throw new Exception("unknow card type");
			}
			 
//			if (i==1)
//				return true;
//			else if (i==0)
//				return false;
//			else
//			{
//				Log.DefaultLogWriteLineError("PCM7_HangUpDetect error num"+i);
//				return true;
//				//throw new Exception("PCM7_HangUpDetect error num"+i);
//			}
		}
		/// <summary>
		/// ��ȡ�Է��һ�ԭ��
		/// </summary>
		/// <returns></returns>
		public int GetHangUpReason()
		{
			 				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					return pcmn7api.PCM7_GetHangUpReason(this.No);					 
				case CardType.Usbid:
					return pcmn7api.TV_GetHangUpReason(this.No);					 
				default:
					throw new Exception("unknow card type");
			}

		 
		}
		/// <summary>
		/// �һ�ԭ��
		/// </summary>
		/// <returns>����������ԭ��</returns>
		public string GetHangUpReasonStr_del()
		{
			return this.GetHangUpReasonStr(pcmn7api.PCM7_GetHangUpReason(this.No));
		}
		/// <summary>
		/// ȡ���һ�ԭ����ִ�.
		/// </summary>
		/// <param name="i">�һ����.</param>
		/// <returns>����������ԭ��</returns>
		public string GetHangUpReasonStr(int i)
		{
			switch(i)
			{
				case 21: //0x15
					return "�����豸ӵ���źš����=21";
				case 37: // 0x25
					return "��·Ⱥӵ���źš����=37";
				case 53: //0x35
					return "������ӵ���źš����=53";
				case 69: //0x45
					return "��ַ��ȫ�źš����=69";
				case 85: //0x55
					return "����ʧ���źš����=85";
				case 101: //0x65
					return "�û�æ�ź�(���)�����=101";
				case 117: //0x75
					return "�պ��źš����=117";
				case 133: //0x85
					return "��·�������źš����=133";
				case 149: //0x95
					return "����ר����Ϣ���źš����=149";
				case 165: //0xa5
					return "����ܾ��źš����=165";
				case 181: //0xb5
					return "���ṩ����ͨ·�źš����=181";
				case 197: //0xc5 
					return "���м�ǰ׺�����=197";
				default:
					return "û�д˴����ŵ���Ϣ"+i.ToString();
			}
		}
		/// <summary>
		/// ���µ���
		/// </summary>
		/// <returns>���ŵ��ֽ�</returns>
		public int PlayRest()
		{
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_PlayFileRest(this.No);					 
					break;
				case CardType.Usbid:
					i= pcmn7api.TV_PlayFileRest(this.No);					 
					break;
				default:
					throw new Exception("unknow card type");
			}
			 

			if( i==0 )
			{
				/* �����ǰ���ļ��Ѿ��������, ��ʼ������һ���ļ� */
				this.CurrPlayIndex++;
				if (this.Playfiles.Length <= this.CurrPlayIndex)
				{
					/*����������һ���ļ���*/
					return 0;
				}
				else
				{

					/*������һ���ļ�*/
					return this.StartPlay( this.CurrPlayIndex);
				}
			}
			else
			{
				return i;
			}
		}

			
		/// <summary>
		/// �һ�
		/// </summary>
		protected void HangUpCtrl()
		{

			this.CHState=CHState.HangUp;

			int i = -10;
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_HangUpCtrl(this.No);					 
					break;
				case CardType.Usbid:
					pcmn7api.TV_HangUpCtrl(this.No);
					//pcmn7api.TV_StopDial(this.No);
					//pcmn7api.TV_OffHookCtrl(this.No);
					break;
				default:
					throw new Exception("unknow card type");
			}
 
		}
		/// <summary>
		/// ֹͣ�����ļ�
		/// </summary>
		public double StopPlay()
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					return pcmn7api.PCM7_StopPlayFile(this.No);					 
				case CardType.Usbid:
					return pcmn7api.TV_StopPlayFile(this.No);
				default:
					throw new Exception("unknow card type");
			}
		}
		/// <summary>
		/// ��ʼ����һ���ļ�
		/// </summary>
		/// <param name="fileIndex">�ļ�</param>
		/// <returns></returns>
		protected int StartPlay(int fileIndex)
		{
			Log.DebugWriteInfo("��ʼ�����ļ�:"+this.Playfiles[fileIndex] +" ch="+this.No);
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					return pcmn7api.PCM7_StartPlayFile(this.No,DataType.StringToByte(this.Playfiles[fileIndex]),0,0);					 
				case CardType.Usbid:
					return pcmn7api.TV_StartPlayFile(this.No,DataType.StringToByte(this.Playfiles[fileIndex]),0,0);					 
				default:
					throw new Exception("unknow card type");
			}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		/// <returns></returns>
		public int GetCONNECT_STATUS()
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api: 
					return pcmn7api.PCM7_CallOutStatus(this.No);
				case CardType.Usbid:
					return pcmn7api.TV_CallOutStatus(this.No); 
				default:
					throw new Exception("unknow card type");
			}
		}		 
		#endregion

		#region �����ļ�
		/// <summary>
		/// ���ŵ��ļ�s
		/// </summary>
		public string[] Playfiles=null;
		/// <summary>
		/// ��ǰ�ļ���Index
		/// </summary>
		public int CurrPlayIndex=0;
		#endregion

		

		#region ͨ������
		/// <summary>
		/// ������ӳ���.Ĭ 50 
		/// </summary>
		public int RingLong=50;
		//public string[] 
		/// <summary>
		/// ����һ��ʵ��
		/// </summary>
		/// <param name="cl">ʵ��</param>
		/// <returns></returns>
		public void Call(CallList cl)
		{
			// set call list
			this.HisCallList = cl;

			// ch, ����
			this.CurrPlayIndex=0;
			// play files
			string context =Card.CurrentCallContext.Context;
			if (context.IndexOf(",")!=-1)
			{
				/*˵���Ƕ��ļ�����*/
				context=context.Replace("@YF@", "D"+DateTime.Now.AddMonths(-1).Month+".TW");
				context=context.Replace("@Tel@", this.HisCallList.TelOfFile);
				context=context.Replace("@JE@", this.HisCallList.JEOfFile);
			}
			context=context.Replace(",,", ",");
			Log.DebugWriteInfo("Will call fils ="+context);

			this.RingLong=cl.HisTelType.RingLong;
			this.Playfiles=context.Split(',');
			this.CallOut(cl.Tel);  // ������绰��ʼ��������.
			this.CallOn(); // ������.
		}
		public void CallOn()
		{
			if (this.CHState==CHState.HangUp)
				return;

			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					this.CallOnOfPCM();
					break;
				case CardType.Usbid:
					this.CallOnOfTV();
					break;
				default:
					break;
					
			}

		}
		/// <summary>
		/// �Է��Ƿ�æ
		/// </summary>
		public bool IsBusyOfTV
		{
			get
			{
				if (pcmn7api.TV_MonitorBusy(this.No, 1, 5) >0) 
					return true;
				if ( pcmn7api.TV_MonitorBusy (this.No,2,5)>0)
					return true;
				return false;
			}
		}
		 
		private void CallOnOfTV()
		{
			switch(this.CHState)
			{
				case BP.CTI.CHState.Dialing:  // ���ڲ��ŵ�״̬��
					while (pcmn7api.TV_DialRest(this.No) > 0)
					{
						//Log.DebugWriteInfo("����ch="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);
					}

					pcmn7api.TV_StopDial(this.No);

					Log.DebugWriteInfo("������� ch="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);

					CHState=CHState.Connect ; // ��������״̬.

					pcmn7api.TV_StartTimer(this.No,40); //����ʱ�䡣
					pcmn7api.TV_StartMonitor(this.No); //���ü�����

					Log.DebugWriteInfo("������ʱ�䶼�Ѿ�����,�ȴ��û����� ch="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);
					break; 
				case BP.CTI.CHState.Connect:  // ������������״̬��

					int SigCount=0,SigLen=0;
					int Sig = pcmn7api.TV_CheckSignal (this.No, ref SigCount, ref SigLen);

					if (((Sig == 1 || Sig == 2) && SigCount >= 3) || pcmn7api.TV_TimerElapsed (this.No) < 0) 
					{
						Log.DebugWriteInfo("�û��һ�");
						

						/* �������ʧ�� */
						this.HisCallList.Note="�û��һ�"+this.No;
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						this.HisCallList.Update();
						//this.HangUpCtrl(); // �һ���
						this.CHState = CHState.HangUp; //���ùһ�״̬.
						pcmn7api.TV_HangUpCtrl (this.No);
						return;
					}

					if ( pcmn7api.TV_ListenerOffHook (this.No) !=0 )
					{
						int bys = StartPlay(0);  //��ʼ���ŵ�һ���ļ�.
						if ( bys > 0)
						{
							this.CHState = CHState.Play; //�������״̬��
							break;
						}
						else
						{
							/* �������ʧ�� */
							this.HisCallList.Note="����ʧ��"+this.No;
							this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
							this.HisCallList.CallingStateOfEnum=CallingState.Init;
							this.HisCallList.Update();
							this.HangUpCtrl(); // �һ���
							this.CHState = CHState.HangUp; //���ùһ�״̬.
							return;
						}
					}

					if (this.TimerElapsed())
					{
						Log.DebugWriteInfo("�Է�û�н���,���г�ʱ ch="+this.No+"    "+this.HisCallList.Tel);

						/* ������˹涨��ʱ�� */
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						this.HisCallList.Note="���г�ʱ"+this.No;
						this.HisCallList.Update(); //��ʱ������´��ٺ�������
						//this.StopPlay();
						this.HangUpCtrl(); // �һ���
						this.CHState = CHState.HangUp; // ����
						return;
					}

					
					break;
				case BP.CTI.CHState.Play:

					//Log.DebugWriteInfo("�����ļ�..ch="+this.No+" Tel="+this.HisCallList.Tel);

					int bye=this.PlayRest(); // ֻ��ʼ�յ��������ܲ��ų���.
					Log.DebugWriteInfo(" CHState.Play, ch="+this.No+" PlayRest="+bye);
					if ( bye < 0 )
					{
						/* ����ʧ�� */
						this.StopPlay(); //ֹͣ������
						this.HangUpCtrl(); //  ���ƹһ���
						this.CHState=CHState.HangUp;
						this.HisCallList.CallingStateOfEnum=CallingState.Error;
						this.HisCallList.Note="�����ļ�����ʧ��.";  //delete record. 
						this.HisCallList.Update();
						return;
					}
					else if (bye==0 )
					{
						Log.DebugWriteInfo("����OK..ch="+this.No+" Tel="+this.HisCallList.Tel);

						/* ������� */
						this.CHState=CHState.HangUp;
						this.HisCallList.Note="�ɹ���ȫ����, ������ȫ������ϡ�";  //delete record. 
						this.HisCallList.CallingStateOfEnum=CallingState.OK;						
						this.HisCallList.Update();

						this.HangUpCtrl(); //���ƹһ���

						return;
					}
					else if (bye>0)
					{
						/*��������״̬�Ͳ�����.*/
					}
					if (this.HangUpDetect()) 
					{  
						Log.DebugWriteInfo("HangUpDetect ch="+this.No );
						/* ���Է��Ƿ�һ� */
						this.HisCallList.Note="�Է��һ�";
						this.HisCallList.CallingStateOfEnum=CallingState.OK;
						this.HisCallList.Update();
						this.StopPlay(); // ֹͣ������
						this.HangUpCtrl(); // �һ���
						this.CHState=CHState.HangUp;
						return ;
					}
					break;
			 
			}

		}
		/// <summary>
		/// ִ�к���.
		/// </summary>
		private void CallOnOfPCM()
		{
			 

			switch(this.CHState)
			{
				case BP.CTI.CHState.Connect:
					int state= this.GetCONNECT_STATUS();
					Log.DebugWriteInfo("Connect 1  ch="+this.No+" GetCONNECT_STATUS="+state);
					if (state==CONNECT_STATUS.SO_TALK )
					{
						Log.DebugWriteInfo("Connect 2 ch="+this.No+" GetCONNECT_STATUS="+state);

						/* ����Է��Ѿ������˵绰�������ڿ�ʼ�����ͨ��������� */
						//int bys = StartPlayFile(this.HisCallList.FK_ContextText);
						int bys = StartPlay(0);
						if ( bys > 0)
						{
							this.CHState = CHState.Play; //���÷�����
						}
						else
						{
							/* �������ʧ�� */
							this.HisCallList.Note="����ʧ��"+this.No;
							this.HisCallList.Update();
							this.HangUpCtrl(); // �һ���
							this.CHState = CHState.HangUp; //���ùһ�״̬
							return;
						}
					}					 

					if (this.TimerElapsed())
					{
						/* ������˹涨��ʱ�� */
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						this.HisCallList.Note="���г�ʱ"+this.No;
						this.HisCallList.Update(); //��ʱ������´��ٺ�������
						//this.StopPlay();
						this.HangUpCtrl(); // �һ���
						this.CHState = CHState.HangUp; // w
						return;
					}

					if ( this.HangUpDetect() )
					{
						Log.DebugWriteInfo(" Connect 3  HangUpDetect ch="+this.No+"  GetCONNECT_STATUS="+state);

						/* �����鵽,�Է��һ�, ���벻��. �����Է�ռ�ߣ��պ� */
						int reason=this.GetHangUpReason();
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;						
						this.HisCallList.Note="��⵽�Է��һ�";
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						//this.HisCallList.CallingState=0;
						this.HisCallList.Update();
						this.StopPlay();
						this.HangUpCtrl(); //�ر�ͨ��.
						this.CHState=CHState.HangUp;
					}
					break;
				case BP.CTI.CHState.Play:

					int bye=this.PlayRest(); // ֻ��ʼ�յ��������ܲ��ų���.
					Log.DebugWriteInfo(" CHState.Play, ch="+this.No+" PlayRest="+bye);
					if ( bye < 0 )
					{
						/* ����ʧ�� */
						this.StopPlay(); //ֹͣ������
						this.HangUpCtrl(); //  ���ƹһ���
						this.CHState=CHState.HangUp;
						this.HisCallList.CallingStateOfEnum=CallingState.Error;
						this.HisCallList.Note="�����ļ�����ʧ��.";  //delete record. 
						this.HisCallList.Update();
						return;
					}
					else if (bye==0 )
					{
						/* ������� */
						this.CHState=CHState.HangUp;
						this.HangUpCtrl(); //���ƹһ���
						this.HisCallList.Note="�ɹ���ȫ����, ������ȫ������ϡ�";  //delete record. 
						this.HisCallList.CallingStateOfEnum=CallingState.OK;						
						this.HisCallList.Update();
						return;
					}
					else if (bye>0)
					{
						/*��������״̬�Ͳ�����.*/
					}


					if (this.HangUpDetect()) 
					{  
						Log.DebugWriteInfo("HangUpDetect ch="+this.No+"  GetCONNECT_STATUS="+this.GetCONNECT_STATUS());

						/* ���Է��Ƿ�һ� */
						this.HisCallList.Note="��ʼ���ź�,�Է��һ�ԭ��:"+this.GetHangUpReason();
						this.HisCallList.CallingStateOfEnum=CallingState.OK;
						this.HisCallList.Update();
						this.StopPlay(); // ֹͣ������
						this.HangUpCtrl(); // �һ���
						this.CHState=CHState.HangUp;
						return ;
					}
					break;
			 
			}
		}
		#endregion

	}
	/// <summary>
	/// ͨ������
	/// </summary>
	public class CHs: CollectionBase
	{
		
		/// <summary>
		/// ͨ������
		/// </summary>
		public CHs()
		{
		}
		/// <summary>
		/// ����һ��ͨ��
		/// </summary>
		/// <param name="en"></param>
		public void Add(CH en)
		{
			this.InnerList.Add(en);
		}
		/// <summary>
		/// ����λ��ȡ������
		/// </summary>
		public CH this[int index]
		{
			get 
			{
				return (CH)this.InnerList[index];
			}
		}
	}
}

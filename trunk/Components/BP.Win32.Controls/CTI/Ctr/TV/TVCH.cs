using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	/// <summary>
	/// ͨ��״̬
	/// </summary>
	public enum TVCHState
	{
		/// <summary>
		/// �һ�
		/// </summary>
		HangUp,
		/// <summary>
		/// ��Ⲧ����
		/// </summary>
		CheckSign,
		/// <summary>
		/// �ȴ��Ⲧ����
		/// </summary>
		Waiting,
		/// <summary>
		/// ����
		/// </summary>
		Dialing,
		/// <summary>
		/// ����
		/// </summary>
		Connect,
		/// <summary>
		/// ����
		/// </summary>
		Play
	}
	/// <summary>
	/// TVCH ��ժҪ˵����
	/// </summary>
	public class TVCH:CH
	{
		#region ͨ������
		/// <summary>
		/// ͨ��״̬
		/// </summary>
		public TVCHState CHState=TVCHState.HangUp;
		 
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public TVCH()
		{
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="no">���</param>
		public TVCH(int no)
		{
			
			this.No=no;
		}
		#endregion

		#region ͨ������

		 
		 
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
		/// �һ�
		/// </summary>
		protected void HangUpCtrl()
		{

			this.CHState=TVCHState.HangUp;

			pcmn7api.TV_HangUpCtrl (this.No);
 
		}

		#region ͨ������
	 
		  
		/// <summary>
		/// ����һ���绰
		/// </summary>
		/// <param name="tel">�绰</param>		
		/// <returns></returns>
		public  override void  CallOut()
		{
			pcmn7api.TV_OffHookCtrl(this.No); // ������ժ��.

			this.CHState=TVCHState.CheckSign ; // ���벦��״̬.

			//this.CHState=TVCHState.CheckSign ; // ��鲦����.
			pcmn7api.TV_StartTimer(this.No,4); // ���ò��ŵĳ���..
			this.CallOn();
		}
		 
		public override void CallOn()
		{
			switch(this.CHState )
			{
				case BP.CTI.TVCHState.HangUp: //����ǹһ�״̬��
					return ;

				case BP.CTI.TVCHState.CheckSign: //����Ǽ���Ⲧ����
					if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
					{
						/* �����Ⲧ������ʱ */
						pcmn7api.TV_HangUpCtrl(this.No); // �һ�
						pcmn7api.TV_StartTimer(this.No,4); // �����´μ���ʱ��Ρ�
						this.CHState=TVCHState.Waiting; // ����״̬Ϊ�ȴ��´β���.
					}
					 
					// �ж��Ƿ����Ⲧ����
					int sigpara1=0,sigpara2=0;
					if (Usbid.TV_CheckSignal(this.No, ref sigpara1,ref sigpara2) ==TVCard.SIG_DIAL)
					{
						/* ����������ź�, �Ϳ�ʼִ���Ⲧ.*/
						Log.DebugWriteInfo("�Ѿ���ȡ����������ʼ����. ");
						pcmn7api.TV_StartTimer(this.No,4); // ���ò���ʱ�䳤�ȡ�
						Usbid.TV_StartDial(this.No, DataType.StringToByte(this.HisCallList.Tel) ); //��ʼ����.
						this.CHState=TVCHState.Dialing ; // ���벦��״̬.
					}
					break ;
				case BP.CTI.TVCHState.Waiting: //����ǵȴ��Ⲧ������
					if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
					{
						/*����ȴ��������õ�ʱ�䣬 �ٴμ���Ⲧ���� */
						pcmn7api.TV_OffHookCtrl(this.No); // ������ժ��.
						this.CHState=TVCHState.CheckSign ; // ��鲦����.
						pcmn7api.TV_StartTimer(this.No,4); // ���ò��ŵĳ���..
					}
					return ;
				case BP.CTI.TVCHState.Dialing:  // ���ڲ��ŵ�״̬��

					if (pcmn7api.TV_DialRest(this.No) <= 0)
					{
						pcmn7api.TV_StopDial(this.No); //ֹͣ����
						this.CHState=TVCHState.Connect ; // ��������״̬.
						pcmn7api.TV_StartMonitor(this.No); //���ü�����

						pcmn7api.TV_StartTimer(this.No,this.RingLong); //�Է��ӵ绰��ʱ�䡣
						Log.DebugWriteInfo("�������,������ʱ�䶼�Ѿ�����,�ȴ��û����� TVCH="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);
					}
					if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
					{
						/* ������ų�����ָ����ʱ�� */
						this.CallOut();
					}					  
					break; 
				case BP.CTI.TVCHState.Connect:  // ������������״̬��

					//Log.DebugWriteInfo("Usbid.TV_ListenerOffHook (this.No):=" +Usbid.TV_ListenerOffHook (this.No) );

					if ( Usbid.TV_ListenerOffHook (this.No) != 0 )
					{
						/* ����Է�ժ�� */
						Log.DebugWriteInfo("�Է������߿�ʼ����.TV_ListenerOffHook ��ʼ���������ļ�" );

						/* ���Է��Ƿ�ժ�� 0 �� �����з�û��ժ��; �� 0 �� �����з��Ѿ�ժ�� 						 
						 * ���ժ������ play ״̬.
						 * */
						int bys = StartPlay(0);  //��ʼ���ŵ�һ���ļ�.
						if ( bys > 0)
						{
							this.CHState = TVCHState.Play; //�������״̬��
							break;
						}
						else
						{
							/* �������ʧ�� */
							//							this.HisCallList.Note="����ʧ��"+this.No;
							//							this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
							//							this.HisCallList.CallingStateOfEnum=CallingState.Init;

							this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
								CallListAttr.CallingState,(int)CallingState.Init,
								"Note","����ʧ��"+this.No);

							this.HangUpCtrl(); // �һ���
							this.CHState = TVCHState.HangUp; //���ùһ�״̬.
							return;
						}
					}

					if (!TVCard.ReverseFlag) 
					{
						/* ����Ǽ��Է�ת */
						if (Usbid.TV_MonitorOffHook(this.No, 25) != 0 )	/* 1 Second */
						{
							/* 1, ������ֻ��ж��Ƿ��������
							 * ��Լ�ڲ����3��󣬶Է�û�н�������Ҳ��Ϊ�����ˡ�
							 * 
							 * 2, ���ڿպ��жϴ���,����һ�������ڵĵ绰����.��Լ����3-4���,�ж��ǽ���,
							 * ��ִ���˲����ļ�.ʵ�������Ǹ������ܺ����ĵ绰����.
							 * 
							 * */
							Log.DebugWriteInfo("�Է������߿�ʼ����.TV_MonitorOffHook., ��ʼ���������ļ�" );

							int bys = StartPlay(0);  //��ʼ���ŵ�һ���ļ�.
							if ( bys > 0)
							{
								this.CHState = TVCHState.Play; //�������״̬��
								break;
							}
							else
							{
								/* �������ʧ�� */
								
								this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
									CallListAttr.CallingState,(int)CallingState.Init,
									"Note","����ʧ��"+this.No);


								this.HangUpCtrl(); // �һ���
								this.CHState = TVCHState.HangUp; //���ùһ�״̬.
								return;
							}
						}
						//Log.DebugWriteInfo("TV_MonitorOffHook (ch, 25)="+Usbid.TV_MonitorOffHook (ch, 25));
					}

					// ���æ��
					int	Sig=0, SigCount=0, SigLen=0;
					Sig = Usbid.TV_CheckSignal (this.No, ref SigCount, ref SigLen);

					//Log.DebugWriteInfo("Sig="+Sig+",SigCount="+SigCount+",SigLen="+SigLen+", TV_ListenerOffHook"+Usbid.TV_ListenerOffHook (this.No)+" TV_MonitorOffHook (Channel, 25)= "+Usbid.TV_MonitorOffHook (this.No, 25));
					if (  (Sig == 1 || Sig == 2  )  && SigCount >= 3  ) 
					{
						/* ������Է�æ��   */
					 						
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.Init,
							"Note","�Է�æ,�����Ǻ���ʱ�����"+this.No);
 
						Usbid.TV_HangUpCtrl (this.No); // �һ�
						this.CHState=TVCHState.HangUp; // ����
						Log.DebugWriteInfo("�Է�æ��");
						break;
					}
					
					// ��ⳬʱ
					if (Usbid.TV_TimerElapsed (this.No) < 0)
					{
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.Init,
							"Note","���г�ʱ"+this.No);
 
						 
						Usbid.TV_HangUpCtrl (this.No); // ����ժ��
						this.CHState=TVCHState.HangUp; // ����
						Log.DebugWriteInfo("���г�ʱ");
						break;												 
					}					
					break;
				case BP.CTI.TVCHState.Play:
					int bye=this.PlayRest(); // ֻ��ʼ�յ��������ܲ��ų���.
					if ( bye < 0 )
					{
						/* ����ʧ�� */
						this.StopPlay(); //ֹͣ������
						this.HangUpCtrl(); //  ���ƹһ���
						this.CHState=TVCHState.HangUp;

						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.Error,
							"Note","�����ļ�����ʧ��"+this.No);
						return;
					}
					else if (bye==0 )
					{
						Log.DebugWriteInfo("����OK..TVCH="+this.No+" Tel="+this.HisCallList.Tel);

						/* ������� */
						this.CHState=TVCHState.HangUp;
						pcmn7api.TV_HangUpCtrl(this.No); // �һ�	
						this.StopPlay();					 
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.OK,
							"Note","�ɹ���ȫ����,������ȫ�������ch="+this.No+"");
						return;
					}
					else if (bye>0)
					{
						/*��������״̬�Ͳ�����.*/
					}

					if (pcmn7api.TV_MonitorBusy (this.No, 1, 5)!=0
						|| pcmn7api.TV_MonitorBusy (this.No, 2, 5)!=0) 
					{
						
						Log.DebugWriteInfo("�����ڼ�Է��һ�,����OK..TVCH="+this.No+" Tel="+this.HisCallList.Tel);

						/* ������� */
						this.CHState=TVCHState.HangUp;
						pcmn7api.TV_HangUpCtrl(this.No); // �һ�
						this.StopPlay();
						
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.OK,
							"Note","�����ڼ�Է��һ�,����OK"+this.No);
						return;
					}
					break;
				default:
					throw new Exception("stat error .");
			}
		}
		#endregion

	}
	/// <summary>
	/// ͨ������
	/// </summary>
	public class TVCHs: CollectionBase
	{
		
		/// <summary>
		/// ͨ������
		/// </summary>
		public TVCHs()
		{
		}
		/// <summary>
		/// ����һ��ͨ��
		/// </summary>
		/// <param name="en"></param>
		public void Add(TVCH en)
		{
			this.InnerList.Add(en);
		}
		/// <summary>
		/// ����λ��ȡ������
		/// </summary>
		public TVCH this[int index]
		{
			get 
			{
				return (TVCH)this.InnerList[index];
			}
		}
	}
}

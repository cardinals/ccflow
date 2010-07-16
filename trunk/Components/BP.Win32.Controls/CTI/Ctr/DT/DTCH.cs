using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	/// <summary>
	/// ͨ��״̬
	/// </summary>
	public enum DTCHState
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
		Play
	}
	/// <summary>
	/// CH ��ժҪ˵����
	/// </summary>
	public class DTCH:CH
	{
		#region ͨ������
		/// <summary>
		/// ͨ��״̬
		/// </summary>
		public DTCHState CHState=DTCHState.HangUp;
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public DTCH()
		{
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="no">���</param>
		public DTCH(int no)
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
			pcmn7api.PCM7_StartTimer(this.No,mm);
		}
		/// <summary>
		/// �����Ƿ���ʱ��
		/// </summary>
		/// <returns></returns>
		protected bool TimerElapsed()
		{
			if  ( pcmn7api.PCM7_TimerElapsed(this.No) >= this.RingLong )
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
		protected bool HangUpDetect_del()
		{
			if (pcmn7api.PCM7_HangUpDetect(this.No)==1)
				return true;
			else
				return false;
		}
		/// <summary>
		/// ����һ���绰
		/// </summary>
		/// <param name="tel">�绰</param>		
		/// <returns></returns>
		public override void CallOut()
		{
			int i= pcmn7api.PCM7_StartCallOut(this.No, DataType.StringToByte(this.HisCallList.Tel), DataType.StringToByte(Card.DefaultCalledTel) ) ;
			this.CHState=DTCHState.Connect; // ��������״̬, Ϊ��������״̬.
			pcmn7api.PCM7_StartTimer(this.No,300);
			//return i;
		}
		/// <summary>
		/// ��ȡ�Է��һ�ԭ��
		/// </summary>
		/// <returns></returns>
		public int GetHangUpReason()
		{
			return pcmn7api.PCM7_GetHangUpReason(this.No);					 
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
		/// �һ�
		/// </summary>
		protected void HangUpCtrl()
		{
			this.CHState=DTCHState.HangUp;
			pcmn7api.PCM7_HangUpCtrl(this.No);
		}
		#endregion

	 
		/// <summary>
		/// ִ�к���.
		/// </summary>
		public override  void CallOn()
		{
			switch(this.CHState)
			{
				case BP.CTI.DTCHState.HangUp:
					return;
				case BP.CTI.DTCHState.Connect:
					int state= pcmn7api.PCM7_CallOutStatus(this.No);
					if (state==CONNECT_STATUS.SO_TALK )
					{
						//Log.DebugWriteInfo("Connect 2 ch="+this.No+" GetCONNECT_STATUS="+state);
						/* ����Է��Ѿ������˵绰�������ڿ�ʼ�����ͨ��������� */
						//int bys = StartPlayFile(this.HisCallList.FK_ContextText);
						int bys = StartPlay(0);
						if ( bys > 0)
							this.CHState = DTCHState.Play; //���÷�����
						else
						{
							/* �������ʧ�� */		
							this.HisCallList.DoInit("����ʧ��"+this.No);
							this.HangUpCtrl(); // �һ���
							this.CHState = DTCHState.HangUp; //���ùһ�״̬.
							return;
						}
					}

					if (this.TimerElapsed())
					{
						/* ������˹涨��ʱ�� */
						this.HisCallList.DoTimeOut("���г�ʱ"+this.No);
						this.HangUpCtrl(); // �һ���
						this.CHState = DTCHState.HangUp; // 
						return;
					}

					if ( pcmn7api.PCM7_HangUpDetect(this.No)==1 ) 
					{
						/*����Է��һ� 1     �Է��һ�   0     �Է�δ�һ�.*/
						Log.DebugWriteInfo("��⵽�Է��һ� 3  HangUpDetect ch="+this.No+"  GetCONNECT_STATUS="+state);
						/* �����鵽,�Է��һ�, ���벻��. �����Է�ռ�ߣ��պ� */
						this.HisCallList.DoTimeOut("��⵽�Է��һ�,���߿պ���"+this.No);
						this.HangUpCtrl(); //�ر�ͨ��.
						this.CHState=DTCHState.HangUp;
					}
					break;
				case BP.CTI.DTCHState.Play:
					int bye=this.PlayRest(); // ֻ��ʼ�յ��������ܲ��ų���.
					if ( bye < 0 )
					{
						/* ����ʧ�� */
						this.StopPlay(); //ֹͣ������
						this.HangUpCtrl(); //  ���ƹһ���
						this.CHState=DTCHState.HangUp;
						this.HisCallList.DoError("�ļ�����ʧ��");
						return;
					}
					else if (bye==0 )
					{
						/* ������� */
						this.CHState=DTCHState.HangUp;
						this.StopPlay();
						this.HangUpCtrl(); //���ƹһ���

						this.HisCallList.DoOK(); //������ϡ�
						return;
					}
					else if (bye>0)
					{
						/*��������״̬�Ͳ�����.*/
						if ( pcmn7api.PCM7_HangUpDetect(this.No)==1 ) 
						{
							/*����Է��һ� 1     �Է��һ�   0     �Է�δ�һ�.*/
							//Log.DebugWriteInfo("HangUpDetect ch="+this.No+"�� GetCONNECT_STATUS="+ pcmn7api.PCM7_CallOutStatus(this.No) + GetCONNECT_STATUS+" re ="+ pcmn7api.PCM7_GetHangUpReason(this.No));
							/* ���Է��Ƿ�һ� */
							this.HisCallList.Update(CallListAttr.CallingState,(int)CallingState.OK,		
								CallListAttr.CallDate,DataType.CurrentData,
								CallListAttr.CallDateTime,DataType.CurrentTime,
								CallListAttr.Note,"��ʼ���ź�,�Է��һ�");
							this.StopPlay(); // ֹͣ������
							this.HangUpCtrl(); // �һ���
							this.CHState=DTCHState.HangUp;
							return ;
						}
						break;
					}				

					break;
			}
		}
	}
	/// <summary>
	/// ͨ������
	/// </summary>
	public class DTCHs: CHs
	{
		
		/// <summary>
		/// ͨ������
		/// </summary>
		public DTCHs()
		{
		}
		/// <summary>
		/// ����һ��ͨ��
		/// </summary>
		/// <param name="en"></param>
		public void Add(DTCH en)
		{
			this.InnerList.Add(en);
		}
		/// <summary>
		/// ����λ��ȡ������
		/// </summary>
		public DTCH this[int index]
		{
			get 
			{
				return (DTCH)this.InnerList[index];
			}
		}
	}
}

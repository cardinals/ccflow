using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	 
	/// <summary>
	/// ͨ�����ࡣ
	/// </summary>
	public class CH
	{
		#region ͨ������
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
			//Log.DebugWriteInfo("��ʼ�����ļ�:"+this.Playfiles[fileIndex] +" ch="+this.No);
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
		/// <summary>
		/// ����һ��ʵ��
		/// </summary>
		/// <param name="cl">ʵ��</param>
		/// <returns></returns>
		public void Call(CallList cl)
		{
			cl.DoCalling(Card.GetCurrentContextByTelType(cl.FK_TelType) );
			// set call list.
			this.HisCallList = cl;
			// ch, ����
			this.CurrPlayIndex=0;


			//case BP.CTI.TVCHState.CheckSign: //����Ǽ���Ⲧ����
			//if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
			//{
			/* �����Ⲧ������ʱ */
			pcmn7api.TV_HangUpCtrl(this.No); // �һ�
			pcmn7api.TV_StartTimer(this.No,4); // �����´μ���ʱ��Ρ�
			//this.CHState=TVCHState.Waiting; // ����״̬Ϊ�ȴ��´β���.
			//}
					 
			// �ж��Ƿ����Ⲧ����
			//int sigpara1=0,sigpara2=0;
			//if (Usbid.TV_CheckSignal(this.No, ref sigpara1,ref sigpara2) ==TVCard.SIG_DIAL)
			//{
			// ///* ����������ź�, �Ϳ�ʼִ���Ⲧ.*/
			Log.DebugWriteInfo("�Ѿ���ȡ����������ʼ����. ");
			pcmn7api.TV_StartTimer(this.No,4); // ���ò���ʱ�䳤�ȡ�
			Usbid.TV_StartDial(this.No, DataType.StringToByte(this.HisCallList.Tel) ); //��ʼ����.
			//this.CHState=TVCHState.Dialing ; // ���벦��״̬.
			//}
			//break ;
			//Log.DebugWriteInfo("Will call fils ="+context);

			this.RingLong=40; //cl.HisTelType.RingLong;
			this.Playfiles=cl.CallFiles;
			this.CallOut();  // ������绰��ʼ��������.
			this.CallOn(); // ������.
		}
		#endregion



		#region ��������д��
		public virtual  void CallOut()
		{
			

		}
		public virtual  void CallOn()
		{
			

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
		 
		 
	}
}

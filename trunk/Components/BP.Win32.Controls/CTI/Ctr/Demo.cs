using System;
using BP.CTI;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	/// <summary>
	/// TVDemo ��ժҪ˵����
	/// </summary>
	public class Demo
	{
		public static void TVCall(string tel,string file, int je, int ch)
		{
			Log.DebugWriteInfo("���Գ�������������");
			
			int  SIG_SILENCE=64;  //��, �ž�, ��Ĭ, ��Ĭ
			int  SIG_DIAL=65; // ��
			 
			bool ReverseFlag=false;
			//��ʼ��
			Usbid.TV_Installed();
			Usbid.TV_Initialize(0);

			Usbid.TV_OffHookCtrl(ch); //ժ��
			Usbid.TV_StartTimer(ch,4); 

			int sigpara1=0,sigpara2=0;

			// �ȴ���������			 
			Log.DebugWriteInfo("��ʼ�ȴ�������");
			while (Usbid.TV_CheckSignal(ch, ref sigpara1,ref sigpara2) != SIG_DIAL)
			{
				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Usbid.TV_HangUpCtrl (ch);

					Log.DebugWriteInfo("�ȴ���ʱ���޲������������ȷ��������·�������ģ�������һ�Ρ�");
					throw new Exception("�ȴ���ʱ���޲������������ȷ��������·�������ģ�������һ�Ρ�");
				}
			}
			
			Log.DebugWriteInfo("�Ѿ���ȡ����������ʼ���� ");
			Usbid.TV_StartDial(ch, DataType.StringToByte(tel) ); // ����
			while (Usbid.TV_DialRest (ch) != 0)
			{
			}

			Usbid.TV_StopDial(ch);
			Log.DebugWriteInfo("������� ");
			//Usbid.TV_OffHookCtrl(ch);

			while (Usbid.TV_TimerElapsed (ch) <=2 )
			{
			}

			Usbid.TV_StartTimer (ch, 40); // ����ʱ��.
			Usbid.TV_StartMonitor (ch);   // ��ʼ����

			while (true)
			{
				if ( Usbid.TV_ListenerOffHook (ch) > 0 )
				{
					Log.DebugWriteInfo("�ж϶Է�ժ�� TV_ListenerOffHook");
					break;
				}

				if (!ReverseFlag) 
				{
					if (Usbid.TV_MonitorOffHook(ch, 25) != 0 )	/* 1 Second */
					{
						Log.DebugWriteInfo("�ж϶Է�ժ��, ReverseFlag, TV_MonitorOffHook "+Usbid.TV_MonitorOffHook (ch, 25));
						break;
					}
					Log.DebugWriteInfo("TV_MonitorOffHook (ch, 25)="+Usbid.TV_MonitorOffHook (ch, 25));
				}

				int	Sig=0, SigCount=0, SigLen=0;			

				Sig = Usbid.TV_CheckSignal (ch, ref SigCount, ref SigLen);

				Log.DebugWriteInfo("Sig="+Sig+",SigCount="+SigCount+",SigLen="+SigLen+", TV_ListenerOffHook"+Usbid.TV_ListenerOffHook (ch)+" TV_MonitorOffHook (Channel, 25)= "+Usbid.TV_MonitorOffHook (ch, 25));
				if (  (Sig == 1 || Sig == 2  )  && SigCount >= 3  ) 
				{
					Usbid.TV_HangUpCtrl (ch);
					Log.DebugWriteInfo("�Է�æ��");
					throw new Exception("�Է�æ��");
				 
				}
				 
				 
				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Usbid.TV_HangUpCtrl (ch);
					Log.DebugWriteInfo("���г�ʱ");
					throw new Exception("���г�ʱ");
				 
				}
			}

			// �����ļ�			 
			Log.DebugWriteInfo("��ʼ�����ļ�..");

			if (Usbid.TV_StartPlayFile(ch, DataType.StringToByte( file ), 0, 0)== -1) 
			{
				throw new Exception("file error . ");
			}

			while (Usbid.TV_PlayFileRest (ch) > 0) 
			{
			 
				if (Usbid.TV_MonitorBusy (ch, 1, 5)!=0 || Usbid.TV_MonitorBusy (ch, 2, 5)!=0 ) 
				{
					Log.DebugWriteInfo("�Է�æ����ִ�йһ� ");

					Usbid.TV_HangUpCtrl (ch);
				}
			}

			Usbid.TV_HangUpCtrl (ch);

			Log.DebugWriteInfo("play file ok ");

			Usbid.TV_Disable();




		}

		public static void TVCall_old(string tel,string file, int je, int yf)
		{
			
            int  SIG_SILENCE=64;  //��, �ž�, ��Ĭ, ��Ĭ
            int  SIG_DIAL=65; // ��

			int ch=0;
			bool ReverseFlag=false;
			
			//��ʼ��
			Usbid.TV_Installed();
			Usbid.TV_Initialize(0);


			//Usbid.TV_SetChannelFreq(0,450); // Ƶ�ʷ�������450.

			//Usbid.TV_SetSignalParam(3,450,); // Ƶ�ʷ�������450.
		//	Usbid.TV_SetSignalParamEx(3,450,); // Ƶ�ʷ�������450.




			Usbid.TV_OffHookCtrl(ch); //ժ��
			Usbid.TV_StartTimer(ch,4); 

			int sigpara1=0,sigpara2=0;

			// �ȴ���������
			 
			Log.DebugWriteInfo("��ʼ�ȴ�������");

			while (Usbid.TV_CheckSignal(ch, ref sigpara1,ref sigpara2) != SIG_DIAL)
			{
				 

				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Log.DebugWriteInfo("�ȴ���ʱ,�޲�����");
					Usbid.TV_HangUpCtrl (ch);
					return;
				}
			}


			
			Log.DebugWriteInfo("�Ѿ���ȡ����������ʼ���� ");

			Usbid.TV_StartDial(ch, DataType.StringToByte(tel) ); // ����


			while (Usbid.TV_DialRest (ch) != 0)
			{
			}
			Usbid.TV_StopDial(ch);

			Log.DebugWriteInfo("������� ");

			Usbid.TV_OffHookCtrl(ch);




			while (Usbid.TV_TimerElapsed (ch) <=2 )
			{
			}

			Usbid.TV_StartTimer (ch, 40); // ����ʱ��.
			Usbid.TV_StartMonitor (ch);   // ��ʼ����

			while (true)
			{
			
				if ( Usbid.TV_ListenerOffHook (ch) > 0 )
				{
					Log.DebugWriteInfo("�ж϶Է�ժ�� TV_ListenerOffHook");
					break;
				}

				if (!ReverseFlag) 
				{
					if (Usbid.TV_MonitorOffHook(ch, 25) != 0 )	/* 1 Second */
					{
						Log.DebugWriteInfo("�ж϶Է�ժ��, ReverseFlag, TV_MonitorOffHook "+Usbid.TV_MonitorOffHook (ch, 25));
						break;
					}
					Log.DebugWriteInfo("TV_MonitorOffHook (ch, 25)="+Usbid.TV_MonitorOffHook (ch, 25));
				}

				int	Sig=0, SigCount=0, SigLen=0;			

				Sig = Usbid.TV_CheckSignal (ch, ref SigCount, ref SigLen);

				Log.DebugWriteInfo("Sig="+Sig+",SigCount="+SigCount+",SigLen="+SigLen+", TV_ListenerOffHook"+Usbid.TV_ListenerOffHook (ch)+" TV_MonitorOffHook (Channel, 25)= "+Usbid.TV_MonitorOffHook (ch, 25));
				if (  (Sig == 1 || Sig == 2  )  && SigCount >= 3  ) 
				{
					Log.DebugWriteInfo("�Է�æ��");
					Usbid.TV_HangUpCtrl (ch);
					return;
				}
//				if (Sig==64)
//				{
//					Log.DebugWriteInfo("Sig=64");
//					break;
//				}
				 
				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Log.DebugWriteInfo("���г�ʱ");
					Usbid.TV_HangUpCtrl (ch);
					return;
				}

			}

			// �����ļ�

			 
			Log.DebugWriteInfo("��ʼ�����ļ� ");

			if (Usbid.TV_StartPlayFile(ch, DataType.StringToByte( file ), 0, 0)== -1) 
			{
				throw new Exception("file error . ");
			}

			while (Usbid.TV_PlayFileRest (ch) > 0) 
			{
			 
				if (Usbid.TV_MonitorBusy (ch, 1, 5)!=0 || Usbid.TV_MonitorBusy (ch, 2, 5)!=0 ) 
				{
					Log.DebugWriteInfo("�Է�æ����ִ�йһ� ");

					Usbid.TV_HangUpCtrl (ch);
				}
			}

			Usbid.TV_HangUpCtrl (ch);

			Log.DebugWriteInfo("play file ok ");

			Usbid.TV_Disable();




		}
		
	}
}

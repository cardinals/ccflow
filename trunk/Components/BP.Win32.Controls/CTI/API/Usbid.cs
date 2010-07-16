using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BP.CTI
{
//	public enum CONNECT_STATUS
//	{	
//		SI_WORK0,
//		SI_WORK1,
//		SI_WORK2,
//		SI_WAIT_ACM,
//		SI_WORK3_1,
//		SI_WORK3_2,
//		SI_WAIT_ANS,
//		SI_WORK4,
//		SI_TALK,
//		SI_WORK6,
//		SI_WORK7,
//		SI_WORK8,
//		SO_WORK0,
//		SO_WORK1,
//		SO_WORK2,
//		SO_WORK3,
//		SO_CONNECT,
//		SO_TALK,
//		SO_WORK7,
//		SO_WORK8
//	}

	/// <summary>
	/// ͨ��������
	/// </summary>
	public enum TVChannelType
	{
		/// <summary>
		/// ����ͨ��
		/// </summary>
		CT_INTERNAL,
		/// <summary>
		/// ����ͨ��
		/// </summary>
		CT_EXTERNAL,
		/// <summary>
		/// ��ͨ��
		/// </summary>
		CT_EMPTY
	}
	 
	public class Usbid
	{
		#region ���⺯��
		

		/// <summary>
		/// �û��Զ����ź���
		/// </summary>
		/// <param name="ss">�ź�������(0 SIG_RING, 1 SIG_BUSY1, 2 SIG_BUSY2������)�û���3��ʼ���� ֻ����3 ��4 �����Զ���æ��</param>
		/// <param name="hlen">�ź������� ��λ40ms</param>
		/// <param name="llen">�����ĳ��� ��λ40ms  �ò����� ����4��5 Ϊ 0 ��ʱ����Ч</param>
		/// <param name="linterval1">�ź���֮�侲���ļ������(��һ�����)</param>
		/// <param name="linterval2">�ź���֮�侲���ļ������(�ڶ������)</param>
		[DllImport("USBID.dll")]
		public static extern  int TV_SetSignalParamEx( int ss, double hlen, double llen,double linterval1,double linterval2);
		/// <summary>
		/// �û��Զ����ź���
		/// </summary>
		/// <param name="para1">�ź�������(0 SIG_RING, 1 SIG_BUSY1, 2 SIG_BUSY2������)�û���3��ʼ����</param>
		/// <param name="para2">�ź�����ֵ ʱ����С��Χֵ</param>
		/// <param name="para3">�ź�����ֵ ʱ�����Χֵ</param>
		/// <param name="para4">�ź�����ֵ ʱ����С��Χֵ</param>
		/// <param name="para5">�ź�����ֵ ʱ�����Χֵ</param>
		[DllImport("USBID.dll")]
		public static extern  void TV_SetSignalParam(int para1,int para2,int para3,int para4,int para5);
		
		/// <summary>
		/// ����ͨ�����õ��ź���Ƶ�ʣ��ɶ�̬�ı�
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <param name="hz">Ƶ�� ����450HZ��</param>
		/// <returns>�������</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SetChannelFreq(int ch, int hz);
		/// <summary>
		/// ����æ�����ӹһ�״̬
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <param name="sign">æ���ź�������</param>
		/// <param name="num">æ������</param>
		/// <returns></returns> 
		[DllImport("USBID.dll")]
		public static extern int TV_MonitorBusy(int ch, int sign, int num);
		/// <summary>
		/// ��ʼ������
		/// </summary>
		[DllImport("USBID.dll")]
		public static extern int TV_Initialize(int i);

		/// <summary>
		/// ����ĳһ����ͨ��ժ��
		/// </summary>
		/// <returns>ͨ����</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_OffHookCtrl(int ch);
		
		/// <summary>
		///  ĳһͨ�������Զ�����
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <param name="DialNum">�����ַ���</param>
		/// <returns>�ϴβ���ʱû�в�����ַ���</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartDial(int ch,byte[] DialNum);
		
		/// <summary>
		/// ��ѯĳһͨ��������
		/// </summary>
		/// <param name="ch">ͨ����</param>		
		/// <returns>0CT_INTERNAL����ͨ��,1CT_EMPTY����ͨ��,2 CT_EMPTY��ͨ��</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_ChannelType(int ch);


		/// <summary>
		/// ֹͣĳһͨ�����Զ� DTMF ����
		/// </summary>
		/// <param name="ch">ͨ����</param>		
		/// <returns>û�в�����ַ���</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StopDial(int ch);


		/// <summary>
		/// ��ѯĳһͨ���ж����ֽ�û�в���
		/// </summary>
		/// <returns>ͨ����</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CheckSignal(int ch, ref int a, ref int b );

		/// <summary>
		/// ��ѯĳһͨ���ж����ֽ�û�в���
		/// </summary>
		/// <returns>ͨ����</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_DialRest(int ch);


		/// <summary>
		/// ��ʼ���ӱ��з���ժ��״̬
		/// </summary>
		/// <returns>ͨ����</returns>
		[DllImport("USBID.dll")]
		public static extern void TV_StartMonitor(int ch);



		/// <summary>
		/// �ڿ���ĳһ����ͨ��ժ�������� TV_StartDial(...) �Զ�����֮��, 
		/// �˺�������������ѯ�����з��Ƿ��Ѿ�ժ��
		/// </summary>
		/// <returns> 0 false, 1 true</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_ListenerOffHook(int ch);
		
		/// <summary>
		/// TV_MonitorOffHook
		/// </summary>
		/// <param name="ch"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]		 
		public static extern int TV_MonitorOffHook(int ch, int time);


		#endregion

		#region ��ʼ���Ⱥ���
		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <returns>����ͨ����</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_Installed();
		
		/// <summary>
		/// �õ����кź���
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_GetSerial(byte[] s);
		/// <summary>
		/// ��ֹ���ֿ�����
		/// �� PCM7 Ӧ�ó����˳�ǰ, һ��ɵ��ô˺�����TV_Diable()
		///  �ر��������жϳ��򣬹Ҷ������м��ߣ�ֹͣ���� DTMF���շ���ֹͣ����¼���������ñ����������ֿ����жϱ��رգ����ֿ�Ҫ�ٴο�ʼ������
		///  �ɵ��ú���TV_Initialize() ���´��жϡ�
		/// </summary>
		/// <returns>����ʱΪ�����</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_Disable();
		/// <summary>
		/// ���ͨ���Ƿ��д��������
		/// </summary>
		/// <param name="Ch">ͨ������</param>
		/// <returns>0 or 1</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CheckFax(int Ch);

		/// <summary>
		/// ���Link ״̬��
		/// </summary>
		/// <param name="Ch">ͨ������</param>
		/// <returns>0 or 1</returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_GetLinkStatus(int Ch);
		#endregion

		#region ͨ�����Ӻ���
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_ConnectPcm2Pcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>	 
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_DisconnectConPcm2Pcm(int Ch1);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_ConnectPcmPcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_DisConnectPcmPcm(int Ch1, int ch2);
		#endregion

		#region �շ� DTMF�뺯��
		/// <summary>
		/// �����û��İ���
		/// </summary>
		/// <param name="Ch">ͨ������</param>
		/// <returns>���صı���</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_GetDtmfChar(int Ch);

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StopDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StopSendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SendDtmfRest(int Ch);
		#endregion

		#region �绰��������
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SendSig(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_ReceiveSig(byte[]  Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CallOutPara(int  type,byte[] ss, string ss2,int type1  );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_GetCallOutChn();
		[DllImport("USBID.dll")]
		public static extern int TV_ChnCallOutPara();
		
		/// <summary>
		/// �����绰
		/// </summary>
		/// <param name="ch">ͨ��</param>
		/// <param name="calledNo">���к���</param>
		/// <param name="callingNo">���к���</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartCallOut(int ch, byte[] calledNo, byte[] callingNo);

		//public static extern int TV_StartCallOut(int ch,string calledNo,string callingNo);
		/// <summary>
		/// �õ���ǰ��״̬
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CallOutStatus(int ch);
		[DllImport("USBID.dll")]
		public static extern int TV_PhoneInPara();

		[DllImport("USBID.dll")]
		public static extern int TV_ChnPhoneInPara();
		[DllImport("USBID.dll")]
		public static extern int TV_PhoneInDetect();


		[DllImport("USBID.dll")]
		public static extern int TV_PhoneInStatus();


		[DllImport("USBID.dll")]
		public static extern int  TV_GetTeleNo();

		[DllImport("USBID.dll")]
		public static extern int  TV_SendACM();


		[DllImport("USBID.dll")]
		public static extern int  TV_SendUBM();

		[DllImport("USBID.dll")]
		public static extern int  TV_SendAnswer();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_HangUpDetect(int ch);

		/// <summary>
		/// ͨ��
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_HangUpCtrl(int ch);
		[DllImport("USBID.dll")]
		public static extern int  TV_ResetChannel(int ch);
		/// <summary>
		/// �õ��һ���ԭ��
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_GetHangUpReason(int ch);
		[DllImport("USBID.dll")]
		public static extern int  TV_GetChannelStatus(int ch);
		#endregion

		#region ¼��������
		/// <summary>
		/// ����¼������ѹ����
		/// </summary>
		/// <param name="bte"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_CompressRatio(int bte );
		[DllImport("USBID.dll")]
		public static extern int  TV_StartRecord();
		[DllImport("USBID.dll")]
		public static extern int  TV_RecordRest();
		/// <summary>
		/// ֹͣ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopRecord();

		/// <summary>
		/// ��ʼ����.
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StartPlay();

		/// <summary>
		/// ��������
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_PlayRest();

		/// <summary>
		/// ֹͣ����
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopPlay();

		/// <summary>
		/// ��ʼ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StartRecordFile();

		/// <summary>
		/// ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_RecordFileRest();

		/// <summary>
		/// ֹͣ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopRecordFile();

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="ch">ͨ��</param>
		/// <param name="fileName">�ļ�����</param>
		/// <param name="startBye">��ʼ�������ֽ�</param>
		/// <param name="playlen">��������</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartPlayFile(int ch,  byte[] fileName, int startBye,int playlen);
		/// <summary>
		/// �����ļ�����, �����Ͻ������ļ���δ�����Ĳ��ֶ��뻺����
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_PlayFileRest(int ch);

		/// <summary>
		/// ǿ��ֹͣĳһͨ�����ļ�����
		/// </summary>
		/// <param name="ch">ͨ������</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopPlayFile(int ch);

		��
		#region ���ļ�����
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="ch">ͨ��</param>
		/// <param name="files">�Լ�������ļ�����</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartPlaySentence(int ch,  byte[] fls);

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_PlaySentenceRest(int ch);

		/// <summary>
		/// ֹͣ����
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopPlaySentence(int ch);

		/// <summary>
		/// ���
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_MakeSentence();
		#endregion
		
		#endregion

		#region ��ʱ������
		/// <summary>
		/// ��ʼ��ʱ
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <param name="mm">ʱ��</param>
		/// <returns>�������</returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StartTimer(int ch, int mm);

		/// <summary>
		/// ��ʼ��ʱ
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_TimerElapsed(int ch);
		#endregion

		#region ģ�⿨����
		#endregion

		#region ������Ӻ���
		#endregion

	}
	 
}
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
	/// RATE
	/// </summary>
	public class RATE
	{
		public const int RATE_64K=0;
		public const int RATE_32K=1;
		public const int RATE_16K=2;
	}
	public class pcmn7api:Usbid
	{
		#region ��ʼ���Ⱥ���
		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <returns>����ͨ����</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_Installed();
		/// <summary>
		/// ��ʼ������
		/// </summary>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_Initialize();
		/// <summary>
		/// �õ����кź���
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_GetSerial(  byte[] s);
		/// <summary>
		/// ��ֹ���ֿ�����
		/// �� PCM7 Ӧ�ó����˳�ǰ, һ��ɵ��ô˺�����PCM7_Diable()
		///  �ر��������жϳ��򣬹Ҷ������м��ߣ�ֹͣ���� DTMF���շ���ֹͣ����¼���������ñ����������ֿ����жϱ��رգ����ֿ�Ҫ�ٴο�ʼ������
		///  �ɵ��ú���PCM7_Initialize() ���´��жϡ�
		/// </summary>
		/// <returns>����ʱΪ�����</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_Disable();
		/// <summary>
		/// ���ͨ���Ƿ��д��������
		/// </summary>
		/// <param name="Ch">ͨ������</param>
		/// <returns>0 or 1</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_CheckFax(int Ch);

		/// <summary>
		/// ���Link ״̬��
		/// </summary>
		/// <param name="Ch">ͨ������</param>
		/// <returns>0 or 1</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetLinkStatus(int Ch);
		#endregion

		#region ͨ�����Ӻ���
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_ConnectPcm2Pcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>	 
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_DisconnectConPcm2Pcm(int Ch1);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_ConnectPcmPcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_DisConnectPcmPcm(int Ch1, int ch2);
		#endregion

		#region �շ� DTMF�뺯��
		/// <summary>
		/// �����û��İ���
		/// </summary>
		/// <param name="Ch">ͨ������</param>
		/// <returns>���صı���</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_GetDtmfChar(int Ch);

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StopDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_SendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StopSendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_SendDtmfRest(int Ch);
		#endregion

		#region �绰��������
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_SendSig(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_ReceiveSig(byte[]  Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_CallOutPara(int  type,byte[] ss, string ss2,int type1  );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_GetCallOutChn();
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_ChnCallOutPara();
		
		/// <summary>
		/// �����绰
		/// </summary>
		/// <param name="ch">ͨ��</param>
		/// <param name="calledNo">���к���</param>
		/// <param name="callingNo">���к���</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartCallOut(int ch, byte[] calledNo, byte[] callingNo);

		//public static extern int PCM7_StartCallOut(int ch,string calledNo,string callingNo);
		/// <summary>
		/// �õ���ǰ��״̬
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_CallOutStatus(int ch);
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_PhoneInPara();

		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_ChnPhoneInPara();
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_PhoneInDetect();


		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_PhoneInStatus();


		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetTeleNo();

		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_SendACM();


		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_SendUBM();

		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_SendAnswer();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_HangUpDetect(int ch);

		/// <summary>
		/// ͨ��
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_HangUpCtrl(int ch);
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_ResetChannel(int ch);
		/// <summary>
		/// �õ��һ���ԭ��
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetHangUpReason(int ch);
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetChannelStatus(int ch);
		#endregion

		#region ¼��������
		/// <summary>
		/// ����¼������ѹ����
		/// </summary>
		/// <param name="bte"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_CompressRatio(int bte );
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartRecord();
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_RecordRest();
		/// <summary>
		/// ֹͣ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopRecord();

		/// <summary>
		/// ��ʼ����.
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartPlay();

		/// <summary>
		/// ��������
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_PlayRest();

		/// <summary>
		/// ֹͣ����
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopPlay();

		/// <summary>
		/// ��ʼ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartRecordFile();

		/// <summary>
		/// ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_RecordFileRest();

		/// <summary>
		/// ֹͣ¼��
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopRecordFile();

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="ch">ͨ��</param>
		/// <param name="fileName">�ļ�����</param>
		/// <param name="startBye">��ʼ�������ֽ�</param>
		/// <param name="playlen">��������</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartPlayFile(int ch,  byte[] fileName, int startBye,int playlen);
		/// <summary>
		/// �����ļ�����, �����Ͻ������ļ���δ�����Ĳ��ֶ��뻺����
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_PlayFileRest(int ch);

		/// <summary>
		/// ǿ��ֹͣĳһͨ�����ļ�����
		/// </summary>
		/// <param name="ch">ͨ������</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopPlayFile(int ch);

		��
		#region ���ļ�����
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="ch">ͨ��</param>
		/// <param name="files">�Լ�������ļ�����</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartPlaySentence(int ch,  byte[] fls);

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_PlaySentenceRest(int ch);

		/// <summary>
		/// ֹͣ����
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopPlaySentence(int ch);

		/// <summary>
		/// ���
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_MakeSentence();
		#endregion
		
		#endregion

		#region ��ʱ������
		/// <summary>
		/// ��ʼ��ʱ
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <param name="mm">ʱ��</param>
		/// <returns>�������</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartTimer(int ch, int mm);

		/// <summary>
		/// ��ʼ��ʱ
		/// </summary>
		/// <param name="ch">ͨ����</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_TimerElapsed(int ch);
		#endregion

		#region ģ�⿨����
		#endregion

		#region ������Ӻ���
		#endregion

	}
	/// <summary>
	/// ������
	/// </summary>
	public class ErrorCode
	{
		public const int EP_ERR=-1 ;    /* general error */
		public const int EP_DRIVER=-2;  /* PCMN7 driver not installed */
		public const int EP_CHANNEL=-3;   /* Invalid channel number */
		public const int EP_ERR_ARGUMENT= -4;  /* error argument */
		public const int EP_ERR_NOT_INIT=-5; //û�г�ʼ��
		public const int EP_ERR_SYNC=-6;		// synchronization object error
		public const int EP_ERR_HARDWARE_CONFIG=-7;
		public const int EP_TFSYN=-10;
		public const int EP_MFSYN=-11;
		public const int EP_RXAIS=-12;
		public const int EP_RXTS16AIS= -13;
		public const int EP_RCV_OVERFLOW=-20;
		public const int EP_ERR_TELENO=-30;
		public const int EP_DIAL_STRING_TOO_LONG=-31;
		public const int EP_PLAY_RECORD_CONFLICT=-40;
		public const int EP_RECORD_BUSY=-41;
		public const int EP_PLAY_BUSY=-42;
		public const int EP_OUT_OF_MEMORY= -43;
		public const int EP_FILEOPEN  = -44;
		public const int EP_TIMEOUT= -50; /* time out */
		public const int PCM_RP_BUF_SIZE=0x4000;
	}
}
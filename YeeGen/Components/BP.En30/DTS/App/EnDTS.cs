using System;
using BP;
using BP.DA;
using BP.En;

namespace BP.DTS
{
	public class EnDTS:DataIOEn2
	{
		/// <summary>
		/// ��ҵ�Ǽǵ���
		/// </summary>
        public EnDTS()
        {
            this.HisDoType = DoType.Especial;
            this.Title = "ͬ�����ݿ�������Ϣ";
            this.HisRunTimeType = RunTimeType.UnName;
            this.HisUserType = Web.UserType.SysAdmin;
            this.Note = "�ܹ���ϵͳ����Ա����ÿ���ֶε�������";

            this.DefaultEveryMin = "00";
            this.DefaultEveryHH = "00";
            this.DefaultEveryDay = "00";
            this.DefaultEveryMonth = "00";

            //this.DefaultEveryDay="01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31";
            //this.DefaultEveryHH="01,02,03,04,05,06,07,08,09,10,11,12";
            //this.DefaultEveryMin="01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60";
        }
		/// <summary>
		/// ��ҵ�Ǽǵ���
		/// </summary>
		public override void Do()
		{

			Log.DebugWriteInfo("Blank DTS��"); 
		}
		public void DoSQLServer()
		{
			DBAccess.RunSQL("exec sp_configure 'allow updates','1'"); // ���ÿ��Ը���������¡�
			


			DBAccess.RunSQL("exec sp_configure 'allow updates','0'"); // ���ÿ��Ը���������¡�
		}
	}
}

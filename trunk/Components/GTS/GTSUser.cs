using System;
using System.Web;
using System.Data;
using BP.En;
using BP.DA;
using System.Configuration;
using BP.Port;

namespace BP.Web
{
	/// <summary>
	/// User ��ժҪ˵����
	/// </summary>
	public class GTSUser:WebUser
	{
         
		/// <summary>
		/// ���Խ���ʱ��
		/// </summary>
		public static string EndExamTime
		{
			get
			{				
				return GetSessionByKey("EndExamTime", DataType.CurrentDataTime);
			}
			set
			{
				SetSessionByKey("EndExamTime",value);
			}
		}
		/// <summary>
		/// ��ǰ���Ե�
		/// </summary>
		public static string FK_Paper
		{
			get
			{				
				return GetSessionByKey("FK_Paper", "-1");
			}
			set
			{
				SetSessionByKey("FK_Paper",value);
			}
		}
        public static string MyFileExt
        {
            get
            {
                return GetSessionByKey("MyFileExt", "jpg");
            }
            set
            {
                SetSessionByKey("MyFileExt", value);
            }
        }
		/// <summary>
		/// �Ƿ񳬳��˿��Ե�ʱ�䡣
		/// </summary>
        public static int TimeLeft
        {
            get
            {
                return DataType.GetSpanMinute((string)GetSessionByKey("EndExamTime", DataType.CurrentDataTime));
            }
        }
        public static bool IsInTestTime
        {
            get
            {
                DateTime dt = DateTime.Now;
                string dtStr = dt.ToString("yyyy-MM-dd");
                string sql = "SELECT No FROM GTS_PaperFix WHERE   (ValidTimeFrom LIKE '" + dtStr + "%') ";

                DataTable table = DBAccess.RunSQLReturnTable(sql);
                if (table.Rows.Count == 0)
                    return false;

                BP.GTS.PaperFixs pfs = new BP.GTS.PaperFixs();
                pfs.RetrieveInSQL("No", sql);


                string msg = "���¿����ڽ��л��߽�Ҫ���У�<hr>";
                foreach (BP.GTS.PaperFix pf in pfs)
                {
                    msg += "<br>" + pf.Name + " ��Чʱ��ӣ�" + pf.ValidTimeFrom + " �� " + pf.ValidTimeTo;
                }
                msg += "<hr>��ǰʱ�䣺" + DataType.CurrentDataTimess + "  ����<a href='/GTS/App/Paper/Link.aspx?IsExam=1'>���뿼������</a>��";

                throw new Exception(msg);
            }
        }
	}
}

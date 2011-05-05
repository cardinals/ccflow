using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.TA
{
	/// <summary>
	/// ��־����
	/// </summary>
	public class TaLogAttr:EntityOIDAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// ��ע
		/// </summary>
		public const string Note="Note"; 
		/// <summary>
		/// ����
		/// </summary>
		public const string LogDate="LogDate"; 
		/// <summary>
		/// ʱ��
		/// </summary>
		public const string LogTime="LogTime";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const string FK_TimeScope="FK_TimeScope";
		/// <summary>
		/// ��
		/// </summary>
		public const string FK_Year="FK_Year";
		/// <summary>
		/// ��
		/// </summary>
		public const string FK_Month="FK_Month";
		/// <summary>
		/// �������� ˽��,����.
		/// </summary>
		public const string SharingType="SharingType"; 
		/// <summary>
		/// ��¼��
		/// </summary>
		public const string Recorder="Recorder"; 	
	}
	/// <summary>
	/// ��־
	/// </summary> 
    public class TaLog : EntityOID
    {
        #region  ����
        public string MyLogOpStr
        {
            get
            {
                return "<img src='" + this.EnMap.Icon + "' border=0 /><a href=\"javascript:OpenLog('" + this.OID + "')\" >" + this.Title + "</a>" + BP.PubClass.FilesViewStr(this.ToString(), this.OID);
            }
        }
        public DateTime LogDatetime
        {
            get
            {
                return DataType.ParseSysDateTime2DateTime(this.LogDate + " " + this.LogTime);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.Title);
            }
            set
            {
                SetValByKey(TaLogAttr.Title, value);
            }
        }
        public string TitleHtml
        {
            get
            {
                return "<img src='" + this.EnMap.Icon + "' border=0 />" + this.GetValStringByKey(TaLogAttr.Title);
            }
            set
            {
                SetValByKey(TaLogAttr.Title, value);
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.Note);
            }
            set
            {
                SetValByKey(TaLogAttr.Note, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string LogDate
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.LogDate);
            }
            set
            {
                SetValByKey(TaLogAttr.LogDate, value);
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string LogTime
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.LogTime);
            }
            set
            {
                SetValByKey(TaLogAttr.LogTime, value);
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string FK_TimeScope
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.FK_TimeScope);
            }
            set
            {
                SetValByKey(TaLogAttr.FK_TimeScope, value);
            }
        }
        public string FK_TimeScopeText
        {
            get
            {
                return this.GetValRefTextByKey(TaLogAttr.FK_TimeScope);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Recorder
        {
            get
            {
                return this.GetValStringByKey(TaLogAttr.Recorder);
            }
            set
            {
                SetValByKey(TaLogAttr.Recorder, value);
            }
        }
        /// <summary>
        /// �������� 0��˽��, 1������
        /// </summary>
        public int SharingType
        {
            get
            {
                return this.GetValIntByKey(TaLogAttr.SharingType);
            }
            set
            {
                SetValByKey(TaLogAttr.SharingType, value);
            }
        }
        #endregion

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
                //return base.HisUAC;
            }
        }

        /// <summary>
        /// ��־
        /// </summary>
        public TaLog()
        {

        }
        /// <summary>
        /// ��־
        /// </summary>
        /// <param name="_No">No</param>
        public TaLog(int oid)
            : base(oid)
        {
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("TA_Log");
                map.EnDesc = "��־";
                //map.Icon="./images/log_s.ico";
                map.Icon = "../TA/Images/log_s.gif";

                map.AddTBIntPKOID();

                map.AddTBStringDoc(TaLogAttr.Title, null, "����", true, false);
                map.AddTBStringDoc(TaLogAttr.Note, null, "��ע", true, false);

                map.AddTBDate(TaLogAttr.LogDate, DataType.CurrentData, "����", true, false);
                map.AddTBString(TaLogAttr.LogTime, DataType.CurrentTime, "ʱ��", true, false, 0, 500, 60);
                map.AddDDLEntities(TaLogAttr.FK_TimeScope, "1", "����ʱ��", new TimeScopes(), true);
                map.AddDDLSysEnum(TaLogAttr.SharingType, 0, "��������", true, true, TaskAttr.SharingType, "@0=����@1=˽��");
                map.AddDDLEntities(TaLogAttr.FK_Year, DataType.CurrentYear, "��", new BP.Pub.NDs(), false);
                map.AddDDLEntities(TaLogAttr.FK_Month, DataType.CurrentMonth, "��", new BP.Pub.YFs(), false);
                map.AddTBString(TaLogAttr.Recorder, null, "��¼��", false, false, 0, 20, 0);

                map.AddSearchAttr(TaLogAttr.SharingType);
                map.AttrsOfSearch.AddHidden(TaLogAttr.Recorder, "=", Web.WebUser.No);
                map.AddSearchAttr(TaLogAttr.FK_Year);
                map.AddSearchAttr(TaLogAttr.FK_Month);
                //map.AttrsOfSearch.AddFromTo("���ڴ�",TaLogAttr.LogDate,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��־s
	/// </summary> 
	public class TaLogs: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new TaLog();
			}
		}
		public TaLogs()
		{

		}
		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="emp">��Ա</param>
		/// <param name="ny">����</param>
		public TaLogs(string emp,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TaLogAttr.LogDate, " like ", ny+"%");
			qo.addAnd();
			qo.AddWhere(TaLogAttr.Recorder,emp);
			if (emp==Web.WebUser.No)
			{
				
			}
			else
			{
				qo.addAnd();
				qo.AddWhere(TaLogAttr.SharingType,1);
			}
			qo.addOrderBy(TaLogAttr.LogDate,TaLogAttr.LogTime);
			qo.DoQuery();
		}
	}
}
 
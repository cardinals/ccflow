using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.TA
{
	/// <summary>
	/// �����¼�����
	/// </summary>
    public class CycleEventAttr : EntityOIDAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ������
        /// </summary>
        public const string ToEmps = "ToEmps";
        /// <summary>
        /// ToEmpNames
        /// </summary>
        public const string ToEmpNames = "ToEmpNames";
        /// <summary>
        /// ��ʼѭ������
        /// </summary>
        public const string StartDate = "StartDate";
        /// <summary>
        /// ����ѭ������
        /// </summary>
        public const string EndDate = "EndDate";
        /// <summary>
        /// �Ƿ��н�������
        /// </summary>
        public const string NoEndDate = "NoEndDate";
        /// <summary>
        /// �¼���ʼʱ��
        /// </summary>
        public const string StartTime = "StartTime";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string FK_TimeScope = "FK_TimeScope";
        /// <summary>
        /// ӵ����
        /// </summary>
        public const string Recorder = "Recorder";
        /// <summary>
        /// Doc
        /// </summary>
        public const string Doc = "Doc";

        /// <summary>
        /// ��s
        /// </summary>
        public const string Days = "Days";
        /// <summary>
        /// ��s
        /// </summary>
        public const string Weeks = "Weeks";
        /// <summary>
        /// �·�s
        /// </summary>
        public const string Monthes = "Monthes";
        /// <summary>
        /// ѭ����ʽ
        /// </summary>
        public const string CycleWay = "CycleWay";
    }
	/// <summary>
	/// ѭ����ʽ
	/// </summary>
    public enum CycleWay
    {
        /// <summary>
        /// δ����
        /// </summary>
        UnSet,
        /// <summary>
        /// ����
        /// </summary>
        ByWeek,
        /// <summary>
        /// ����
        /// </summary>
        ByMonth,
        /// <summary>
        /// ����
        /// </summary>
        ByYear
    }
	/// <summary>
	/// �����¼�
	/// </summary> 
    public class CycleEvent : EntityOID
    {
        /// <summary>
        /// �������ڵ�����
        /// </summary>
        public string GenerCurrRefDate
        {
            get
            {
                switch (this.MyCycleWay)
                {
                    case CycleWay.ByYear:
                        return DataType.CurrentYear + "-" + this.Monthes.ToString().PadLeft(2,'0') + "-" + this.Days.ToString().PadLeft(2,'0');
                    case CycleWay.ByMonth:
                        return DataType.CurrentYear + "-" + DataType.CurrentMonth + "-" + this.Days.ToString().PadLeft(2,'0');
                    case CycleWay.ByWeek:
                        DayOfWeek week = (DayOfWeek)this.Weeks;
                        DateTime dt = DateTime.Now;
                        while (true)
                        {
                            if (dt.DayOfWeek == week)
                                return dt.ToString("yyyy-MM-dd");
                            dt = dt.AddDays(1);
                        }
                    case CycleWay.UnSet:
                        return "unset";
                    default:
                        return "unset";
                }
            }
        }

        #region  ����
        //		public DateTime LogDatetime
        //		{
        //			get
        //			{
        //				return DataType.ParseSysDateTime2DateTime(this.LogDate+" "+this.LogTime);
        //			}
        //		}
        public string Recorder
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.Recorder);
            }
            set
            {
                SetValByKey(CycleEventAttr.Recorder, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.Title);
            }
            set
            {
                SetValByKey(CycleEventAttr.Title, value);
            }
        }
        public string TitleHtml
        {
            get
            {
                return "<img src='" + this.EnMap.Icon + "' border=0 />" + this.StartTime + "<a href=\"javascript:WinOpen('CycleEventV.aspx?RefOID=" + this.OID + "')\"  >" + this.GetValStringByKey(CycleEventAttr.Title) + "</a>";
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string ToEmps
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.ToEmps);
            }
            set
            {
                string strs = value+",";
                if (strs.Contains("," + BP.Web.WebUser.No + "," ) == false)
                    strs = strs + "," + Web.WebUser.No;

                strs = PubClass.CheckEmps(strs);
            
                SetValByKey(CycleEventAttr.ToEmps, strs);
                SetValByKey(CycleEventAttr.ToEmpNames, Web.WebUser.Tag);
            }
        }
        public string ToEmpNames
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.ToEmpNames);
            }
        }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public string StartDate
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.StartDate);
            }
            set
            {
                SetValByKey(CycleEventAttr.StartDate, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string EndDate
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.EndDate);
            }
            set
            {
                SetValByKey(CycleEventAttr.EndDate, value);
            }
        }
        public DateTime EndDate_S
        {
            get
            {
                return DataType.ParseSysDate2DateTime(this.EndDate);
            }
        }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public string StartTime
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.StartTime);
            }
            set
            {
                SetValByKey(CycleEventAttr.StartTime, value);
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string FK_TimeScope
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.FK_TimeScope);
            }
            set
            {
                SetValByKey(CycleEventAttr.FK_TimeScope, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(CycleEventAttr.Doc);
            }
            set
            {
                SetValByKey(CycleEventAttr.Doc, value);
            }
        }
        public string DocHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(CycleEventAttr.Doc);
            }
        }
        /// <summary>
        /// �Ƿ��н�������
        /// </summary>
        public bool NoEndDate
        {
            get
            {
                return this.GetValBooleanByKey(CycleEventAttr.NoEndDate);
            }
            set
            {
                SetValByKey(CycleEventAttr.NoEndDate, value);
            }
        }
        /// <summary>
        /// ѭ����ʽ
        /// </summary>
        public CycleWay MyCycleWay
        {
            get
            {
                return (CycleWay)this.GetValIntByKey(CycleEventAttr.CycleWay);
            }
            set
            {
                this.SetValByKey(CycleEventAttr.CycleWay, (int)value);
            }
        }
        public string MyCycleWayText
        {
            get
            {
                return this.GetValRefTextByKey(CycleEventAttr.CycleWay);
            }
        }
        public int Weeks
        {
            get
            {
                return this.GetValIntByKey(CycleEventAttr.Weeks);
            }
            set
            {
                SetValByKey(CycleEventAttr.Weeks, value);
            }
        }
        public int Days
        {
            get
            {
                return this.GetValIntByKey(CycleEventAttr.Days);
            }
            set
            {
                SetValByKey(CycleEventAttr.Days, value);
            }
        }
        public int Monthes
        {
            get
            {
                return this.GetValIntByKey(CycleEventAttr.Monthes);
            }
            set
            {
                SetValByKey(CycleEventAttr.Monthes, value);
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
        /// �����¼�
        /// </summary>
        public CycleEvent()
        {
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="_No">No</param>
        public CycleEvent(int oid)
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

                Map map = new Map("TA_CycleEvent");
                map.EnDesc = "�����¼�";
                map.Icon = "./Images/CycleEvent_s.ico";

                map.AddTBIntPKOID();
                map.AddTBString(CycleEventAttr.Title, null, "����", true, false, 0, 500, 15);

                map.AddTBString(CycleEventAttr.ToEmps, null, "������", true, false, 0, 1000, 15);
                map.AddTBString(CycleEventAttr.ToEmpNames, null, "����������", true, false, 0, 1000, 15);


                map.AddTBDate(CycleEventAttr.StartDate, "��ʼ����", false, false);
                map.AddTBDate(CycleEventAttr.EndDate, "2200-12-31", "��������", false, false);

                map.AddBoolean(CycleEventAttr.NoEndDate, true, "�޽�������", false, false);

                map.AddTBString(CycleEventAttr.StartTime, "08:00", "�¼���ʼʱ��", true, false, 5, 5, 5);
                map.AddDDLEntities(CycleEventAttr.FK_TimeScope, "01", "����ʱ���", new TimeScopes(), false);

                map.AddDDLEntities(CycleEventAttr.Recorder, null, "������", new Emps(), false);

                map.AddTBString(CycleEventAttr.Doc, null, "��ע", true, false, 0, 1000, 15);
                map.AddDDLSysEnum(CycleEventAttr.CycleWay, (int)CycleWay.ByMonth, "ѭ����ʽ", true, true, "CycleWay", "@0=δ����@1=����@2=����@3=����");

                map.AddTBString(CycleEventAttr.Days, "1", "��", true, false, 0, 1000, 15);
                map.AddTBString(CycleEventAttr.Weeks, "1", "��", true, false, 0, 1000, 15);
                map.AddTBString(CycleEventAttr.Monthes, "1", "�·�", true, false, 0, 1000, 15);

                map.AddSearchAttr(CycleEventAttr.CycleWay);
                map.AttrsOfSearch.AddHidden(CycleEventAttr.Recorder, "=", Web.WebUser.No);
                map.AttrsOfSearch.AddFromTo("��ʼ����", CycleEventAttr.EndDate, DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat), DataType.CurrentData, 8);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �����¼�s
	/// </summary> 
	public class CycleEvents: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new CycleEvent();
			}
		}
		public CycleEvents()
		{

		}
		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="emp">��Ա</param>
		/// <param name="startDate">��ʼ����</param>
		public CycleEvents(string emp,string ny)
		{
			QueryObject qo = new QueryObject(this);

			qo.addLeftBracket();
			qo.AddWhere(CycleEventAttr.ToEmps, " like ", "%,"+emp+",%");
			qo.addOr();
			qo.AddWhere(CycleEventAttr.Recorder,emp);
			qo.addRightBracket();

			qo.addAnd();


			qo.addLeftBracket();
			qo.AddWhere(CycleEventAttr.StartDate, " <= " , ny+"%" );
			qo.addAnd();
			qo.AddWhere(CycleEventAttr.EndDate, " >= " , ny+"%" );
			qo.addRightBracket();

			qo.addOrderBy(CycleEventAttr.StartTime );
			qo.DoQuery();
		}
		/// <summary>
		/// ��ѯ���ҵ�ȫ�������¼�
		/// </summary>
		/// <param name="emp"></param>
		public CycleEvents(string emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(CycleEventAttr.ToEmps, " like ", "%,"+emp+",%");

			qo.addOrderBy(CycleEventAttr.StartTime );

			qo.DoQuery();
		}
		public int re()
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere(CycleEventAttr.ToEmps, " like ", "%"+Web.WebUser.No+"%");
			return qo.DoQuery();
		}
	}
}
 
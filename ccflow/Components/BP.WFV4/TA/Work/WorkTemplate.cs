using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.TA
{
 	/// <summary>
	/// ����ģ��״̬
	/// </summary>
	public enum FK_WTT
	{
		/// <summary>
		/// ������
		/// </summary>
		Apply,
		/// <summary>
		/// ������
		/// </summary>
		Report,
		/// <summary>
		/// ������
		/// </summary>
		Etc
	}
	/// <summary>
	/// ����ģ������
	/// </summary>
    public class WorkTemplateAttr : WorkDtlBaseAttr
    {
        /// <summary>
        /// �˻�ԭ��
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ���������
        /// </summary>
        public const string PRI = "PRI";
        public const string IsRe = "IsRe";
        public const string SpanDays = "SpanDays";
        /// <summary>
        /// ����ģ��״̬
        /// </summary>
        public const string FK_WTT = "FK_WTT";

        public const string Doc = "Doc";
        public const string CheckWay = "CheckWay";
    }
	/// <summary>
	/// ����ģ��
	/// </summary> 
	public class WorkTemplate : EntityNoName
	{
		#region ��������

        public CheckWay HisCheckWay
        {
            get
            {
                return (CheckWay)this.GetValIntByKey(WorkTemplateAttr.CheckWay);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.CheckWay, value);
            }
        }

        public string Doc
        {
            get
            {
                return this.GetValStrByKey(WorkTemplateAttr.Doc);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.Doc, value);
            }
        }

        public bool IsRe
        {
            get
            {
                return this.GetValBooleanByKey(WorkTemplateAttr.IsRe);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.IsRe, value);
            }
        }


        public string FK_WTT
        {
            get
            {
                return this.GetValStrByKey(WorkTemplateAttr.FK_WTT);
            }
            set
            {
                SetValByKey(WorkTemplateAttr.FK_WTT, value);
            }
        }
     
     
		public string FK_WTTText
		{
			get
			{
				return this.GetValRefTextByKey(WorkTemplateAttr.FK_WTT);
			}
		}
		#endregion
 
		#region ���캯��
		/// <summary>
		/// ����ģ��
		/// </summary>
		public WorkTemplate()
		{
		  
		}
		/// <summary>
		/// ����ģ��
		/// </summary>
		/// <param name="_No">No</param>
		public WorkTemplate(string oid):base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_WorkTemplate");
				map.EnDesc="����ģ��";

                map.AddTBStringPK("No", null, "���", true, true, 3, 3, 3);
                map.AddTBString(WorkTemplateAttr.Name, null, "����", true, false, 0, 500, 15);


                map.AddDDLEntities(WorkTemplateAttr.FK_WTT, "99", "����", new WorkTemplateTypes(), true);

              //  map.AddDDLSysEnum(WorkTemplateAttr.FK_WTT, 2, "����", true, true, "FK_WTT", "@0=������@1=ͨ����@2=��ѯ��@3=�ر���@4=������");
                map.AddDDLSysEnum(WorkTemplateAttr.PRI, 0, "Ĭ�ϵ�PRI", true, true);
                map.AddBoolean(WorkTemplateAttr.IsRe, false, "Ĭ���Ƿ���Ҫ�ظ�", true, true);
                map.AddTBInt(WorkTemplateAttr.SpanDays, 0, "Ĭ���������",true, false);
				map.AddTBStringDoc();
               // map.AddTBStringNote();
                map.AddDDLSysEnum(WorkAttr.CheckWay, 0, "Ĭ�Ͽ�������", true, true);

                RefMethod rm = new RefMethod();
                rm.Title = "��ȡTxtģ��";
                rm.ClassMethodName = this.ToString() + ".DoReadTxt";
                rm.Warning = "��ȷ��Ҫִ����";

                map.AddRefMethod(rm);

                map.AddSearchAttr(WorkTemplateAttr.FK_WTT);
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

        public string DoReadTxt()
        {
            string dir = "D:\\WebApp\\OA\\Data\\TemplateWork\\";

            string[] fls = System.IO.Directory.GetFiles(dir);
            foreach (string f in fls)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(f);
                WorkTemplate en = new WorkTemplate();
                en.Name = fi.Name.Substring(1);
                en.Name = en.Name.Replace(".txt", "");

                if (en.Retrieve(WorkTemplateAttr.Name, fi.Name) == 0)
                {
                    en.No = en.GenerNewNo;
                    en.Insert();
                }

                en.Doc = DataType.ReadTextFile(f);
                en.Update();
                //string pk = 
            }

            return "ִ�гɹ���";
        }
	}
	/// <summary>
	/// ����ģ��s
	/// </summary> 
	public class WorkTemplates: EntitiesNoName
	{
		 
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkTemplate();
			}
		}
		/// <summary>
		/// WorkTemplates
		/// </summary>
		public WorkTemplates()
		{

		}
		public WorkTemplates(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.addLeftBracket();
			qo.AddWhere(WorkTemplateAttr.Executer,userNo);
			qo.addOr();
			qo.AddWhere(WorkTemplateAttr.Sender,userNo);
			qo.addRightBracket();
			qo.addAnd();
			qo.AddWhere(WorkTemplateAttr.PRI, " LIKE ", ny+"%");
			qo.DoQuery();
		}
		
	}
}
 
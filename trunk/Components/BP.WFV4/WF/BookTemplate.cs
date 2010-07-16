
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.En;


namespace BP.WF
{

    public class BookTemplateAttr
    {
        public const string Url = "Url";
        /// <summary>
        /// NodeID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// �Ƿ���Ҫ�ʹ�
        /// </summary>
        public const string IsNeedSend = "IsNeedSend";
        /// <summary>
        /// Ϊ��������ʹ��
        /// </summary>
        public const string IDX = "IDX";

        /// <summary>
        /// Ҫ�ų����ֶ�
        /// </summary>
        public const string ExpField = "ExpField";
        /// <summary>
        /// Ҫ�滻��ֵ
        /// </summary>
        public const string ReplaceVal = "ReplaceVal";
    }
	/// <summary>
	/// ����ģ��
	/// </summary>
	public class BookTemplate : EntityNoName
    {
        #region  ����
        /// <summary>
        /// Ҫ�滻��ֵ
        /// </summary>
        public string ReplaceVal
        {
            get
            {
                return this.GetValStringByKey(BookAttr.ReplaceVal);
            }
            set
            {
                this.SetValByKey(BookAttr.ReplaceVal, value);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        public new string No
        {
            get
            {
                string no = this.GetValStrByKey("No");
                no = no.Replace("\n", "");
                no = no.Replace(" ", "");
                return no;
            }
            set
            {
                this.SetValByKey("No", value);
                this.SetValByKey(BookTemplateAttr.Url, value);
            }
        }
        public string IDX
        {
            get
            {
                return this.GetValStrByKey(BookTemplateAttr.IDX);
            }
            set
            {
                this.SetValByKey(BookTemplateAttr.IDX, value);
            }
        }
        public string Url
        {
            get
            {
                string s= this.GetValStrByKey(BookTemplateAttr.Url);
                if (s == "" || s == null)
                    return this.No;

                return s;
            }
            set
            {
                this.SetValByKey(BookTemplateAttr.Url, value);
            }
        }
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(BookTemplateAttr.NodeID);
            }
            set
            {
                this.SetValByKey(BookTemplateAttr.NodeID, value);
            }
        }
        /// <summary>
        /// �Ƿ���Ҫ�ʹ�
        /// </summary>
        public bool IsNeedSend
        {
            get
            {
                return this.GetValBooleanByKey(BookTemplateAttr.IsNeedSend); 
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
		/// ss
		/// </summary>
		public BookTemplate(){}
        public BookTemplate(string no):base(no.Replace( "\n","" ).Trim() ) 
        {
        }
		/// <summary>
		/// ��д���෽��
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_BookTemplate");
                map.EnDesc = this.ToE("BookTemplate", "����ģ��"); // "����ģ��";
                map.EnType = EnType.Admin;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "6";

                map.AddTBStringPK("No", null, null, true, true, 1, 300, 6);
                map.AddTBString("Name", null, null, true, false, 0, 200, 20);
                map.AddTBString("Url", null, null, true, false, 0, 200, 20);
                map.AddTBString("NodeID", null, "NodeID", true, false, 0, 200, 20);
                map.AddTBString("IDX", null, "IDX", false, false, 0, 200, 20);
                map.AddTBString(BookTemplateAttr.ExpField, null, "Ҫ�ų����ֶ�", false, false, 0, 800, 20);
                map.AddTBString(BookTemplateAttr.ReplaceVal, null, "Ҫ�滻��ֵ", false, false, 0, 3000, 20);
                map.AddBoolean(BookTemplateAttr.IsNeedSend, false, "�Ƿ���Ҫ�ʹ��ִ", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// �ڵ������ģ��
	/// </summary>
	public class BookTemplates: EntitiesNoName
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BookTemplate();
			}
		}
		/// <summary>
		/// ����ģ��
		/// </summary>
        public BookTemplates()
        {
        }
        /// <summary>
        /// BookTemplates
        /// </summary>
        /// <param name="nd"></param>
        public BookTemplates(Node nd)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BookTemplateAttr.NodeID, nd.NodeID);
            if (nd.IsStartNode)
            {
                qo.addOr();
                qo.AddWhere("No", "SLHZ");
            }
            qo.DoQuery();
        }
        public BookTemplates(string fk_flow)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(BookTemplateAttr.NodeID, "select nodeid from wf_node where fk_flow='"+fk_flow+"'" );
            qo.DoQuery();
        }
        public BookTemplates(int nd)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BookTemplateAttr.NodeID, nd);
            qo.DoQuery();
        }
		#endregion
	}
	
}

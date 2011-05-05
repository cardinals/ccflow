
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port; 
using BP.Port; 
using BP.En;
using BP.Web;

 
namespace BP.WF
{
	/// <summary>
	/// ������־
	/// </summary>
    public class DTSLogAttr
    {
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        public const string MsgKey = "MsgKey";
        public const string Note = "Note";
        public const string MyPK = "MyPK";
    }
	/// <summary>
	/// ������־
	/// </summary>
    public class DTSLog : Entity
    {

        #region ��������
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.MyPK);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.MyPK, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// �����Ϣ
        /// </summary>
        public string MsgKey
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.MsgKey);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.MsgKey, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(DTSLogAttr.Note);
            }
            set
            {
                this.SetValByKey(DTSLogAttr.Note, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ������־
        /// </summary>
        public DTSLog()
        {
        }
        public DTSLog(string pk)
        {
            //this.MyPK=pk;
            this.Retrieve();
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
                Map map = new Map("ZF_DTSLog");
                map.EnDesc = "������־";
                map.Icon = "../ZF/Images/Dot_s.gif";

                map.AddTBStringPK(DTSLogAttr.MyPK, null, "����", true, true, 0, 500, 10);
                map.AddDDLEntities(DTSLogAttr.FK_Dept, null, "����", new BP.Port.Depts(), false);
                map.AddTBString(DTSLogAttr.MsgKey, null, "��Ϣ", false, true, 0, 500, 10);
                map.AddTBString(DTSLogAttr.Note, null, "��ע", false, true, 0, 500, 10);

                map.AddSearchAttr(DTSLogAttr.FK_Dept);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeInsert()
        {
            this.MyPK = DateTime.Now.ToString("yy��MM��dd��HHʱmm��ss��");
            return base.beforeInsert();
        }
    }
	/// <summary>
	/// ������־s
	/// </summary>
	public class DTSLogs : Entities
	{	
		#region ���췽��
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new DTSLog();
			}
		}
		/// <summary>
		/// ������־s 
		/// </summary>
		public DTSLogs(){}
		
		#endregion
	}
	
}

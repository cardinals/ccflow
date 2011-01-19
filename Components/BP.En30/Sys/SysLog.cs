using System;
using System.IO;
using System.Collections;
using BP.DA;
using BP.En;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace BP.Sys
{
    public class SysLogAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ������key
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// FK_Emp
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// Title
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// �ļ���С
        /// </summary>
        public const string FK_TypeName = "FK_TypeName";
        /// <summary>
        /// FK_Type
        /// </summary>
        public const string FK_Type = "FK_Type";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Doc = "Doc";
        public const string Time = "Time";
        public const string FK_NY = "FK_NY";
    }
    /// <summary>
    /// ϵͳ��־
    /// </summary>
    public class SysLog : BP.En.EntityMyPK
    {
        #region ʵ�ֻ�������

        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_Dept, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_Emp, value);
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.Title);
            }
            set
            {
                this.SetValByKey(SysLogAttr.Title, value);
            }
        }
        public string FK_TypeName
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_TypeName);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_TypeName, value);
            }
        }
        public string FK_Type
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.FK_Type);
            }
            set
            {
                this.SetValByKey(SysLogAttr.FK_Type, value);
            }
        }

        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.RDT);
            }
            set
            {
                this.SetValByKey(SysLogAttr.RDT, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(SysLogAttr.Doc);
            }
            set
            {
                this.SetValByKey(SysLogAttr.Doc, value);
            }
        }
        #endregion

        #region ���췽��

        public SysLog() { }
        /// <summary>
        /// ϵͳ��־
        /// </summary>
        /// <param name="_OID"></param>
        public SysLog(string _OID)
            : base(_OID)
        {
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_Log");
                map.EnDesc = "ϵͳ��־";
                map.CodeStruct = "3";

                map.AddMyPK();

                map.AddTBString(SysLogAttr.RDT, null, "����", true, false, 0, 50, 20);
                map.AddTBString(SysLogAttr.Time, null, "ʱ��", true, false, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_NY, null, "�·�", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_Type, null, "����", true, false, 0, 500, 20);
                map.AddTBString(SysLogAttr.FK_TypeName, null, "��������", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.Title, null, "����", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.Doc, null, "��ϸ��Ϣ", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_Emp, null, "��Ա", false, true, 0, 50, 20);
                map.AddTBString(SysLogAttr.FK_Dept, null, "����", false, true, 0, 50, 20);

                //   map.AddTBString(SysLogAttr.Doc, null, "��ע", true, false, 0, 200, 30);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// д��־�ķ���
        /// </summary>
        /// <param name="type">����</param>
        /// <param name="typeName">��������</param>
        /// <param name="title">����</param>
        /// <param name="doc">����</param>
        public static void WriteLog(string type, string typeName, string title, string doc)
        {
            SysLog log = new SysLog();
            //log.MyPK = BP.DA.DBAccess.GenerOID();
        }
    }
	/// <summary>
	/// ϵͳ��־ 
	/// </summary>
	public class SysLogs :EntitiesMyPK
	{
        /// <summary>
        /// ϵͳ��־
        /// </summary>
		public SysLogs(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysLog();
			}
		}
	}
}

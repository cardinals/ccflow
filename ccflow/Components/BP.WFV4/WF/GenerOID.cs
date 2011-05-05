

using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.WF;
using BP.Tax; 
using BP.Port; 
using BP.En;


namespace BP.DA
{
	/// <summary>
	/// ת����¼
	/// </summary>
    public class GenerOIDAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string NodeId = "NodeId";
        /// <summary>
        /// ������Ա
        /// </summary>
        public const string Worker = "Worker";
        /// <summary>
        /// �˻�ԭ��
        /// </summary>
        public const string Sort = "Sort";
        public const string FK_Emp = "FK_Emp";
        public const string Emps = "Emps";
        /// <summary>
        /// �Ƿ����ջ�
        /// </summary>
        public const string IsTakeBack = "IsTakeBack";
        #endregion
    }
	/// <summary>
	/// ת����¼
	/// </summary>
	public class GenerOID : Entity
	{
        public int Gener(string sort)
        {
            int val = DBAccess.RunSQLReturnVal("SELECT OID FROM "
        }
		#region ��������
        public int OID
        {
            get
            {
                return this.GetValIntByKey(GenerOIDAttr.OID);
            }
            set
            {
                SetValByKey(GenerOIDAttr.OID, value);
            }
        }
        public string Sort
        {
            get
            {
                return this.GetValStringByKey(GenerOIDAttr.Sort);
            }
            set
            {
                SetValByKey(GenerOIDAttr.Sort, value);
            }
        }
		#endregion 

		#region ���캯��
		/// <summary>
		/// ת����¼
		/// </summary>
		public GenerOID(){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_GenerOID");
                map.EnDesc = "ת����¼";
                map.EnType = EnType.App;

                map.AddTBIntPK(GenerOIDAttr.OID, 0, "OID", true, true);
                map.AddTBStringPK(GenerOIDAttr.Sort, "", "����", true, true, 1, 20, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 
	}
	/// <summary>
	/// ת����¼s 
	/// </summary>
	public class GenerOIDs : Entities
	{	 
		#region ����
		/// <summary>
		/// ת����¼s
		/// </summary>
		public GenerOIDs()
		{

		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GenerOID();
			}
		}
		#endregion
	}
	
}

using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// ��Ա
	/// </summary>
    public class GTSDeptAttr
    {
        #region ��������
        /// <summary>
        /// ѧ��
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ���
        /// </summary>
        public const string Pass = "Pass";
        #endregion
    }
	/// <summary>
	/// ��Ա ��ժҪ˵��
	/// </summary>
    public class GTSDept : EntityNoName
    {
        #region ��������
        /// <summary>
        ///�Ķ���
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(GTSDeptAttr.Addr);
            }
            set
            {
                SetValByKey(GTSDeptAttr.Addr, value);
            }
        }
        /// <summary>
        ///���
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GTSDeptAttr.FK_Dept);
            }
            set
            {
                SetValByKey(GTSDeptAttr.FK_Dept, value);
            }
        }
        public string Pass
        {
            get
            {
                return this.GetValStringByKey(GTSDeptAttr.Pass);
            }
            set
            {
                SetValByKey(GTSDeptAttr.Pass, value);
            }
        }
        #endregion

        #region ���캯��
        public GTSDept()
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

                Map map = new Map("Port_Dept");
                map.EnDesc = "����ά��";
                map.EnType = EnType.Admin;

                #region �ֶ�
                /*�����ֶ����Ե����� */
                map.AddTBStringPK(EmpAttr.No, null, null, true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, null, true, false, 2, 100, 100);
                #endregion �ֶ�����


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override bool beforeDelete()
        {
            if (this.No == "00")
                throw new Exception("������ɾ�� 00 ���š�");

            return base.beforeDelete();
        }

        //protected override bool beforeInsert()
        //{
        //    if (this.No.Length == 2)
        //        throw new Exception("����Ҫ���� 2 λ�������Ĳ��ű�š�");
        //    return base.beforeInsert();
        //}
    }
	/// <summary>
    /// ����ά�� 
	/// </summary>
	public class GTSDepts : Entities
	{
		#region ����
		/// <summary>
		/// ��Ա
		/// </summary>
		public GTSDepts(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GTSDept();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}

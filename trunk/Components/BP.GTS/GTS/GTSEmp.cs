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
    public class GTSEmpAttr
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
    public class GTSEmp : EntityNoName
    {
        #region ��������
        /// <summary>
        ///�Ķ���
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(GTSEmpAttr.Addr);
            }
            set
            {
                SetValByKey(GTSEmpAttr.Addr, value);
            }
        }
        /// <summary>
        ///���
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GTSEmpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(GTSEmpAttr.FK_Dept, value);
            }
        }
        public string Pass
        {
            get
            {
                return this.GetValStringByKey(GTSEmpAttr.Pass);
            }
            set
            {
                SetValByKey(GTSEmpAttr.Pass, value);
            }
        }
        #endregion

        #region ���캯��
        public GTSEmp()
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

                Map map = new Map("Port_Emp");
                map.EnDesc = "����";
                map.EnType = EnType.Admin;

                #region �ֶ�
                /*�����ֶ����Ե����� */
                map.AddTBStringPK(EmpAttr.No, null, null, true, false, 2, 20, 100);
                map.AddTBString(EmpAttr.Name, null, null, true, false, 1, 100, 100);
                map.AddTBString(EmpAttr.Pass, "pub", null, true, false, 0, 20, 10);

                map.AddTBInt("MyFileNum", 0, "MyFileNum ", true, false);
                 

                map.AddDDLEntities(EmpAttr.FK_Dept, null, null, new Port.Depts(), true);
                map.AddMyFile();
                #endregion �ֶ�����

                map.AddSearchAttr(EmpAttr.FK_Dept);

                RefMethod rm = new RefMethod();
                rm.Title = "ͼƬ";
                rm.ClassMethodName = this.ToString() + ".DoOpen";
                map.AddRefMethod(rm);



                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeDelete()
        {
            if (this.No == "admin")
                throw new Exception("@������ɾ��admin.");
            return base.beforeDelete();
        }
        protected override bool beforeUpdateInsertAction()
        {
            if (this.No == "admin")
                this.FK_Dept = "00";
            return base.beforeUpdateInsertAction();
        }
        public string DoOpen()
        {
            BP.PubClass.Open("../Comm/Item3.aspx?EnName=BP.GTS.GTSEmp&No="+this.No);
            return null;
        }
        #endregion
    }
	/// <summary>
	/// ��Ա 
	/// </summary>
	public class GTSEmps : Entities
	{
		#region ����
		/// <summary>
		/// ��Ա
		/// </summary>
		public GTSEmps(){}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GTSEmp();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}

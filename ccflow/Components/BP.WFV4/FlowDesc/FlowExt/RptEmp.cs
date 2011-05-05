using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.DS;


namespace BP.WF
{
	/// <summary>
	/// �������Ա
	/// </summary>
	public class RptEmpAttr  
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Rpt="FK_Rpt";
		/// <summary>
		/// ��������Ա
		/// </summary>
		public const  string FK_Emp="FK_Emp";		 
		#endregion	
	}
	/// <summary>
    /// �������Ա ��ժҪ˵����
	/// </summary>
    public class RptEmp : Entity
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptEmpAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptEmpAttr.FK_Rpt, value);
            }
        }
        /// <summary>
        ///��������Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RptEmpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(RptEmpAttr.FK_Emp, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// �����������Ա
        /// </summary> 
        public RptEmp() { }
        /// <summary>
        /// ������Ա��������Ա��Ӧ
        /// </summary>
        /// <param name="_empoid">����</param>
        /// <param name="wsNo">��������Ա���</param> 	
        public RptEmp(string _empoid, string wsNo)
        {
            this.FK_Rpt = _empoid;
            this.FK_Emp = wsNo;
            if (this.Retrieve() == 0)
                this.Insert();
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

                Map map = new Map("WF_RptEmp");
                map.EnDesc = "�������Ա";
                map.EnType = EnType.Dot2Dot;

                map.AddDDLEntitiesPK(RptEmpAttr.FK_Rpt, null, "����Ա", new WFRpts(), true);
                map.AddDDLEntitiesPK(RptEmpAttr.FK_Emp, null, "��������Ա", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// �������Ա 
	/// </summary>
    public class RptEmps : Entities
    {
        #region ����
        /// <summary>
        /// �����������Ա
        /// </summary>
        public RptEmps()
        {
        }
        /// <summary>
        /// �������Ա
        /// </summary>
        public RptEmps(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptEmpAttr.FK_Emp, stationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �������Ա
        /// </summary>
        /// <param name="empId">RptID</param>
        public RptEmps(int empId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptEmpAttr.FK_Rpt, empId);
            qo.DoQuery();
        }
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new RptEmp();
            }
        }
        #endregion
    }
}

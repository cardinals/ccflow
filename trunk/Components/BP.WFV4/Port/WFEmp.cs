using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 
using BP.Port; 
using BP.En;


namespace BP.WF.Port
{
	/// <summary>
	/// ����Ա
	/// </summary>
    public class WFEmpAttr
    {
        #region ��������
        /// <summary>
        /// No
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ������
        /// </summary>
        public const string Name = "Name";
        public const string LoginData = "LoginData";
        public const string Tel = "Tel";
        /// <summary>
        /// ��Ȩ��
        /// </summary>
        public const string Author = "Author";
        /// <summary>
        /// ��Ȩ����
        /// </summary>
        public const string AuthorDate = "AuthorDate";
        /// <summary>
        /// �Ƿ�����Ȩ״̬
        /// </summary>
        public const string AuthorIsOK = "AuthorIsOK";
        public const string Email = "Email";

        #endregion
    }
	/// <summary>
	/// ����Ա
	/// </summary>
	public class WFEmp : EntityNoName
	{		
		#region ��������
        
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Tel);
            }
            set
            {
                SetValByKey(WFEmpAttr.Tel, value);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Email);
            }
            set
            {
                SetValByKey(WFEmpAttr.Email, value);
            }
        }
        public string Author
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Author);
            }
            set
            {
                SetValByKey(WFEmpAttr.Author, value);
            }
        }
        public string AuthorDate
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.AuthorDate);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorDate, value);
            }
        }
        public bool AuthorIsOK
        {
            get
            {
                return this.GetValBooleanByKey(WFEmpAttr.AuthorIsOK);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorIsOK, value);
            }
        }
          
		#endregion 

		#region ���캯��
		/// <summary>
		/// ����Ա
		/// </summary>
		public WFEmp(){}
        /// <summary>
        /// ����Ա
        /// </summary>
        /// <param name="no"></param>
        public WFEmp(string no) 
        {
            this.No = no;
            try
            {
                if (this.RetrieveFromDBSources() == 0)
                {
                    Emp emp = new Emp(no);
                    this.Copy(emp);
                    this.Insert();
                }
            }
            catch
            {
                this.CheckPhysicsTable();
            }
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

                Map map = new Map("WF_Emp");
                map.EnDesc = "����Ա";
                map.EnType = EnType.App;
                map.AddTBStringPK(WFEmpAttr.No, null, "No", true, true, 1, 50, 20);
                map.AddTBString(WFEmpAttr.Name, null, "Name", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Tel, null, "Tel", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Email, null, "Email", true, true, 0, 50, 20);

                map.AddTBString(WFEmpAttr.Author, null, "��Ȩ��", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.AuthorDate, null, "��Ȩ����", true, true, 0, 50, 20);
                map.AddTBInt(WFEmpAttr.AuthorIsOK, 0, "�Ƿ���Ȩ�ɹ�", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 

        #region ����
        #endregion
    }
	/// <summary>
	/// ����Աs 
	/// </summary>
	public class WFEmps : EntitiesNoName
	{	 
		#region ����
		/// <summary>
		/// ����Աs
		/// </summary>
		public WFEmps()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WFEmp();
			}
		}
		#endregion
	}
	
}

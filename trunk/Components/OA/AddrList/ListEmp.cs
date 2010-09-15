using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ��ԱͨѶ¼����
    /// </summary>
    public class ListEmpAttr : EntityOIDNameAttr
    {
        public const string No = "No";
        /// <summary>
        /// ��ַ
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// �绰
        /// </summary>
        public const string Tel = "Tel";
        /// <summary>
        /// �ʼ�
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// ����
        /// </summary>
        public const string Fax = "Fax";
        /// <summary>
        /// �쵼
        /// </summary>
        public const string Learder = "Learder";
        /// <summary>
        /// ֵ���ֻ�
        /// </summary>
        public const string HandSet = "HandSet";
        /// <summary>
        /// ��ע
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ְ��
        /// </summary>
        public const string FK_Duty = "FK_Duty";
    }
    /// <summary>
    /// ��ԱͨѶ¼
    /// </summary>
    public class ListEmp : BP.En.EntityNoName
    {
        #region ��������
        /// <summary>
        /// ����Ա�绰
        /// </summary>
        public string HandSet
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.HandSet);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.HandSet, value);
            }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public string Learder
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Learder);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.Learder, value);
            }
        }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Addr);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.Addr, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Note);
            }
        }
        public string Fax
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Fax);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Email);
            }
        }

        public string Tel
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Tel);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.Tel, value);
            }
        }
       
        #endregion

        #region ���췽��
        /// <summary>
        /// ��ԱͨѶ¼
        /// </summary>
        public ListEmp()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GE_ListEmp");
                map.EnType = EnType.Sys;
                map.EnDesc = "��ԱͨѶ¼";
                map.DepositaryOfEntity = Depositary.None;
                map.MoveTo = ListEmpAttr.FK_Dept;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.ListDutys' >ְ��ά��</a> - <a href='Ens.aspx?EnsName=BP.GE.ListDepts' >����ά��</a>";

                //map.AddTBIntPKOID();
                map.AddTBStringPK(ListEmpAttr.No, null, "���", true, true, 6, 6, 6);
                map.AddTBString(ListEmpAttr.Name, null, "����", true, false, 0, 100, 100);
                map.AddTBString(ListEmpAttr.HandSet, null, "�ֻ�", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Tel, null, "�칫�绰", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Fax, null, "Fax", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Email, null, "Email", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Note, null, "��ע", true, false, 0, 500, 10);

                map.AddDDLEntities(ListEmpAttr.FK_Dept, null, "��λ", new BP.GE.ListDepts(), true);
                map.AddDDLEntities(ListEmpAttr.FK_Duty, null, "ְ��", new BP.GE.ListDutys(), true);

                map.AddSearchAttr(ListEmpAttr.FK_Dept);
                map.AddSearchAttr(ListEmpAttr.FK_Duty);

                // map.AddTBString(ListEmpAttr.FK_ZJ, null, "ZJ", true, false, 0, 100, 10);
                // map.AddTBString(ListEmpAttr.Addr, null, "��ʱ", true, false, 0, 20, 10);
                // map.AddDDLEntities(ListEmpAttr.Fax, null, "��", new ListEmpWeeks(), true);
                // map.AddDDLEntities(ListEmpAttr.Email, null, "��", new ListEmpWeeks(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��ԱͨѶ¼s
    /// </summary>
    public class ListEmps : EntitiesNoName
    {
        /// <summary>
        /// ��ԱͨѶ¼s
        /// </summary>
        public ListEmps()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ListEmp();
            }
        }
    }
}

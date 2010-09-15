using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ��λͨѶ¼����
    /// </summary>
    public class ListDeptAttr : EntityOIDNameAttr
    {
        /// <summary>
        /// No
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// Addr
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
        public const string Note = "Note";
        public const string WorkFloor = "WorkFloor";
    }
    /// <summary>
    /// ��λͨѶ¼
    /// </summary>
    public class ListDept : BP.En.EntityNoName
    {
        #region ��������
        /// <summary>
        /// ����Ա�绰
        /// </summary>
        public string HandSet
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.HandSet);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.HandSet, value);
            }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public string Learder
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Learder);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.Learder, value);
            }
        }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Addr);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.Addr, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Note);
            }
        }
        public string Fax
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Fax);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Email);
            }
        }

        public string Tel
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Tel);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.Tel, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��λͨѶ¼
        /// </summary>
        public ListDept()
        {
        }
        /// <summary>
        /// ��λͨѶ¼
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GE_ListDept");
                map.EnType = EnType.Sys;
                map.EnDesc = "��λͨѶ¼";
                map.DepositaryOfEntity = Depositary.None;
                map.MoveTo = ListDeptAttr.WorkFloor;

                // map.AddTBIntPKOID();
                map.AddTBStringPK(ListDeptAttr.No, null, "��λ���", true, false, 2, 10, 10);
                map.AddTBString(ListDeptAttr.Name, null, "��λ����", true, false, 0, 100, 100);
                map.AddTBString(ListDeptAttr.Learder, null, "�쵼", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.HandSet, null, "ֵ���ֻ�", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Tel, null, "��ϵ�绰", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Fax, null, "Fax", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Email, null, "Email", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Note, null, "��ע", true, false, 0, 500, 10);

                map.AddDtl(new ListEmps(), ListEmpAttr.FK_Dept);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��λͨѶ¼s
    /// </summary>
    public class ListDepts : EntitiesNoName
    {
        /// <summary>
        /// ��λͨѶ¼s
        /// </summary>
        public ListDepts()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ListDept();
            }
        }
    }
}

using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Port
{	 
	/// <summary>
	/// ��Ȩ����
	/// </summary>
    public class AuthorAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ��Ȩ����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ������
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public const string IsOK = "IsOK";
    }
	/// <summary>
	/// ��Ȩ
	/// </summary>
    public class Author : EntityNo
    {
        #region ʵ�ֻ����ķ�����
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// ��Ȩ��
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStrByKey(AuthorAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(AuthorAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(AuthorAttr.RDT);
            }
            set
            {
                this.SetValByKey(AuthorAttr.RDT, value);
            }
        }
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsOK
        {
            get
            {
                return this.GetValBooleanByKey(AuthorAttr.IsOK);
            }
            set
            {
                this.SetValByKey(AuthorAttr.IsOK, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ȩ
        /// </summary> 
        public Author()
        {
        }
        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="_No"></param>
        public Author(string _No) : base(_No) { }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_Author");
                map.EnDesc = this.ToE("Author", "��Ȩ"); // "��Ȩ";
                map.EnType = EnType.Admin;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAllowRepeatNo = false;

                map.AddTBStringPK(SimpleNoNameAttr.No, null, null, true, false, 2, 20, 4);

                map.AddTBString(AuthorAttr.FK_Emp, null, "��Ȩ��", true, false, 0, 50, 250);
                map.AddTBDate(AuthorAttr.RDT, null, "��¼����", true, true);
                map.AddBoolean(AuthorAttr.IsOK, true, "�Ƿ���Ч", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	 /// <summary>
	 /// ��Ȩs
	 /// </summary>
    public class Authors : EntitiesNoName
    {
        /// <summary>
        /// ��Ȩ
        /// </summary>
        public Authors() { }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Author();
            }
        }
    }
}

using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.En
{
	/// <summary>
    /// ���ڷּ��ε�ά��
	/// </summary>
    public class EntityOIDNoNameAttr : EntityOIDAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
    }
	/// <summary>
    /// ���ڷּ��ε�ά��
	/// </summary>
    abstract public class EntityOIDNoName : EntityOIDNo
    {
        #region ��������
        /// <summary>
        /// ʵ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityOIDNoNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNoNameAttr.Name, value);
            }
        }
        #endregion

        #region ����
        public EntityOIDNoName()
        { }
        protected EntityOIDNoName(string _No)
        {
            this.No = _No;
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(EntityOIDNoAttr.No, this.No);
            if (qo.DoQuery() == 0)
            {
                throw new Exception("@û�б��[" + this.No + "][" + this.EnDesc + "]���ʵ��");
            }
        }
        protected EntityOIDNoName(int _OID) : base(_OID) { }

        public int RetrieveByNo()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(EntityOIDNoAttr.No, this.No);
            return qo.DoQuery();
        }
        #endregion

        #region  ��д����ķ�����
        #endregion
    }
    /// <summary>
    /// EntitiesOIDNoName ���ڷּ��ε�ά��
    /// </summary>
    abstract public class EntitiesOIDNoName : EntitiesOID
    {
        /// <summary>
        /// ���ڷּ��ε�ά��
        /// </summary>
        public EntitiesOIDNoName()
        {
        }
    }
}

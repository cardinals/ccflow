using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// NoEntity ��ժҪ˵����
	/// </summary>
    abstract public class EntityMyPKInt : Entity
    {
        #region ��������
        /// <summary>
        /// ��Ҫ��
        /// </summary>
        public override string PK
        {
            get
            {
                return "MyPK";
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(EntityMyPKAttr.MyPK);
            }
            set
            {
                this.SetValByKey(EntityMyPKAttr.MyPK, value);
            }
        }
        public int NoInt
        {
            get
            {
                return this.GetValIntByKey(EntityMyPKAttr.NoInt);
            }
            set
            {
                this.SetValByKey(EntityMyPKAttr.NoInt, value);
            }
         }
        #endregion

        #region ����
        public EntityMyPKInt()
        {
        }
        /// <summary>
        /// class Name 
        /// </summary>
        /// <param name="_MyPK">_MyPK</param>
        protected EntityMyPKInt(string _MyPK)
        {
            this.MyPK = _MyPK;
            this.Retrieve();
        }
        #endregion
    }
	/// <summary>
	/// ����ʵ�弯��
	/// </summary>
    abstract public class EntitiesMyPKInt : Entities
    {
        /// <summary>
        /// ʵ�弯��
        /// </summary>
        public EntitiesMyPKInt()
        {
        }
    }
}

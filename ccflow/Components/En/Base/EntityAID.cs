using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
    public class EntityAIDAttr
    {
        /// <summary>
        /// AID
        /// </summary>
        public static string AID = "AID";
    }
	/// <summary>
	/// �����б�
	/// </summary>
    public class EntityAIDMyFileAttr : EntityAIDAttr
    {
        /// <summary>
        /// MyFileName
        /// </summary>
        public const string MyFileName = "MyFileName";
        /// <summary>
        /// MyFilePath
        /// </summary>
        public const string MyFilePath = "MyFilePath";
        /// <summary>
        /// MyFileExt
        /// </summary>
        public const string MyFileExt = "MyFileExt";
    }
	/// <summary>
	/// AIDʵ��,ֻ��һ��ʵ�����ʵ��ֻ��һ���������ԡ�
	/// </summary>
	abstract public class EntityAID : Entity
	{		 
		#region ����
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return "AID";
            }
        }
		/// <summary>
		/// AID, ����ǿյľͷ��� 0 . 
		/// </summary>
        public int AID
        {
            get
            {
                return this.GetValIntByKey(EntityAIDAttr.AID);
            }
        }
		#endregion

		#region ���캯��
		/// <summary>
		/// ����һ����ʵ��
		/// </summary>
		protected EntityAID()
		{
		}
		/// <summary>
		/// ����AID����ʵ��
		/// </summary>
		/// <param name="AID">AID</param>
        protected EntityAID(int AID)
        {
            this.SetValByKey(EntityAIDAttr.AID, AID);
            this.Retrieve();
        }
		#endregion
	 
		#region override ����
        public override bool IsExits
        {
            get
            {
                if (this.AID == 0)
                    return false;

                // �������ݿ��ж���䡣
                string selectSQL = "SELECT " + this.PKField + " FROM " + this.EnMap.PhysicsTable + " WHERE AID=" + this.HisDBVarStr + "v";
                Paras ens = new Paras();
                ens.Add("v", this.AID);

                // �����ݿ������ѯ���ж���û�С�
                switch (this.EnMap.EnDBUrl.DBUrlType)
                {
                    case DBUrlType.AppCenterDSN:
                        return DBAccess.IsExits(selectSQL, ens);
                    case DBUrlType.DBAccessOfMSSQL2000:
                        return DBAccessOfMSSQL2000.IsExits(selectSQL);
                    case DBUrlType.DBAccessOfOLE:
                        return DBAccessOfOLE.IsExits(selectSQL);
                    case DBUrlType.DBAccessOfOracle9i:
                        return DBAccessOfOracle9i.IsExits(selectSQL);
                    default:
                        throw new Exception("û����Ƶ���" + this.EnMap.EnDBUrl.DBType);
                }
            }
        }
		/// <summary>
		/// ɾ��֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete() 
		{
			if (base.beforeDelete()==false)
				return false;			
			try 
			{				
				if (this.AID < 0 )
					throw new Exception("@ʵ��["+this.EnDesc+"]û�б�ʵ����������Delete().");
				return true;
			} 
			catch (Exception ex) 
			{
				throw new Exception("@["+this.EnDesc+"].beforeDelete err:"+ex.Message);
			}
		}
		/// <summary>
		/// beforeInsert ֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.AID > 0)
                throw new Exception("@[" + this.EnDesc + "], ʵ���Ѿ���ʵ���� AID=[" + this.AID + "]������Insert.");

            return base.beforeInsert();
        }
		/// <summary>
		/// beforeUpdate
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			if (base.beforeUpdate()==false)
				return false;

			/*
			if (this.AID <= 0 )
				throw new Exception("@ʵ��["+this.EnDesc+"]û�б�ʵ����������Update().");
				*/
			return true;
		}
        protected virtual string SerialKey
        {
            get
            {
                return "AID";
            }
        }
       
		#endregion

		#region public ����
		  
		#endregion
	}
    /// <summary>
    /// EntitiesAID
    /// </summary>
    abstract public class EntitiesAID : Entities
    {
        public EntitiesAID()
        {
        }
    }
}

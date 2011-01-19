using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.En
{
	/// <summary>
	/// EntityOIDNoAttr
	/// </summary>
	public class EntityOIDNoAttr : EntityOIDAttr 
	{	
		/// <summary>
		/// ���
		/// </summary>
		public const string No="No";		
	}
	
	abstract public class EntityOIDNo : EntityOID
	{
		#region ��������
		/// <summary>
		/// ʵ����
		/// </summary>
		public string No
		{
			get
			{
				return this.GetValStringByKey(EntityOIDNoAttr.No);
			}
			set
			{
				this.SetValByKey(EntityOIDNoAttr.No,value);
			}
		}
		#endregion 

		#region ����
		public EntityOIDNo()
		{}
		protected EntityOIDNo(string _No)
		{
			this.No = _No  ; 
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(EntityOIDNoAttr.No , this.No);
			if (qo.DoQuery()==0) 
			{
				throw new Exception("@û�б��["+this.No+"]["+this.EnDesc+"]���ʵ��");
			}
		}		
		protected EntityOIDNo(int _OID) : base(_OID){}		
 
		public int RetrieveByNo()
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(EntityOIDNoAttr.No,this.No) ;
			return qo.DoQuery(); 
		}
		#endregion 	

		#region  ��д����ķ�����
		#endregion

	}
	abstract public class EntitiesOIDNo : EntitiesOID
	{	 
		public EntitiesOIDNo(){}
	}
}

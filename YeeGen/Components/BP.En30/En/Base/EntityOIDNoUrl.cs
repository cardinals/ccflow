using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.En
{
	/// <summary>
	/// EntityOIDNoUrlAttr
	/// </summary>
	public class EntityOIDNoUrlAttr : EntityOIDNoAttr 
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Url="Url";
		/// <summary>
		/// Ŀ��
		/// </summary>
		public const string Target="Target";
	}
	/// <summary>
	/// OID No Url �����ӡ�
	/// </summary>
	abstract public class EntityOIDNoUrl : EntityOIDNo
	{
		#region ��������		 
		/// <summary>
		/// ����
		/// </summary>
		public string Url
		{
			get
			{
				return this.GetValStringByKey(EntityOIDNoUrlAttr.Url);
			}
			set
			{
				this.SetValByKey(EntityOIDNoUrlAttr.Url,value);
			}
		}
		/// <summary>
		/// Ŀ��
		/// </summary>
		public string Target
		{
			get
			{
				return this.GetValStringByKey(EntityOIDNoUrlAttr.Target);
			}
			set
			{
				this.SetValByKey(EntityOIDNoUrlAttr.Target,value);
			}
		}
		#endregion 

		#region ����
		public EntityOIDNoUrl()
		{}
		protected EntityOIDNoUrl(string _No) : base(_No){}		 
		protected EntityOIDNoUrl(int _OID) : base(_OID){} 		 
		#endregion 
	

		#region  ��д����ķ�����
		protected override bool beforeInsert()
		{
			base.beforeInsert();
			return true;
		}
		protected override bool beforeUpdate()
		{
			base.beforeUpdate();
			return true;
		}
		protected override bool beforeDelete()
		{
			base.beforeDelete();
			return true;
		}

		protected override void afterDelete()
		{
			base.afterDelete();
			return ;

		}
		protected override  void afterInsert()
		{
			base.afterInsert();
			return ;
		}
		protected override void afterUpdate()
		{
			base.afterUpdate();
			return ;
		}
		#endregion
	}
	abstract public class EntitiesOIDNoUrl : EntitiesOIDNo
	{	 
		public EntitiesOIDNoUrl(){}
	}
}

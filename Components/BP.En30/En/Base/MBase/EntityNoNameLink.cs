using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// ����ʵ�塣
	/// </summary>
	public class EntityNoNameUrlAttr : EntityNoNameAttr
	{
		public const string Url="Url";
		public const string Target="Target";
	}
	/// <summary>
	/// NoEntity ��ժҪ˵����
	/// </summary>
	abstract public class EntityNoNameUrl : EntityNoName
	{
		/// <summary>
		/// ����ʵ��
		/// </summary>
		public EntityNoNameUrl()
		{
		}
		protected EntityNoNameUrl(string _No) : base(_No){}		 
		/// <summary>
		/// URL
		/// </summary>
		public string Url
		{
			get
			{
				return this.GetValStringByKey(EntityNoNameUrlAttr.Url);
			}
			set
			{
				this.SetValByKey(EntityNoNameUrlAttr.Url,value);
			}
		}	
		/// <summary>
		/// Ŀ��
		/// </summary>
		public string Target
		{
			get
			{
				return this.GetValStringByKey(EntityNoNameUrlAttr.Target);
			}
			set
			{
				this.SetValByKey(EntityNoNameUrlAttr.Target,value);
			}
		}	

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
	abstract public class EntitiesNoNameUrl : EntitiesNoName
	{
		public EntitiesNoNameUrl()
		{
		}
	 
	}
}

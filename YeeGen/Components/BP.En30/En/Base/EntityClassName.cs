using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityEnsNameAttr
	/// </summary>
	public class EntityEnsNameAttr  
	{	
		/// <summary>
		/// className
		/// </summary>
		public const string EnsEnsName="EnsEnsName";
	}
	/// <summary>
	/// NoEntity ��ժҪ˵����
	/// </summary>
	abstract public class EntityEnsName : Entity
	{
		#region ��������
		/// <summary>
		/// ����������
		/// </summary>
		public string EnsEnsName
		{
			get
			{
				return this.GetValStringByKey(EntityEnsNameAttr.EnsEnsName) ; 
			}
			set
			{
				this.SetValByKey(EntityEnsNameAttr.EnsEnsName,value) ; 
			}
		}
		#endregion 

		#region ��չ����
		/// <summary>
		/// ��������
		/// </summary>
		public string HisDesc
		{
			get
			{
				return this.HisEntity.EnDesc;
			}
		}
		/// <summary>
		/// ����ʵ��
		/// </summary>
		public Entity HisEntity
		{
			get
			{
				return this.HisEntities.GetNewEntity;
			}
		}
		/// <summary>
		/// ����ʵ�弯��
		/// </summary>
		public Entities HisEntities
		{
			get
			{
				return ClassFactory.GetEns(this.EnsEnsName) ;
			}
		}
		#endregion 

		#region ����
		public EntityEnsName()
		{
		}
		/// <summary>
		/// class Name 
		/// </summary>
		/// <param name="_EnsEnsName">_EnsEnsName</param>
		protected EntityEnsName(string _EnsEnsName)
		{
			this.EnsEnsName = _EnsEnsName;
			this.Retrieve();
		}	
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
	/// <summary>
	/// ����ʵ�弯��
	/// </summary>
	abstract public class EntitiesEnsName : Entities
	{
		/// <summary>
		/// ʵ�弯��
		/// </summary>
		public EntitiesEnsName()
		{
		}
	}
}

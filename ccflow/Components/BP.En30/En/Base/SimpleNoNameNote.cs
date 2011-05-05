using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.En
{
	/// <summary>
	/// ����
	/// </summary>
	public class SimpleNoNaIEnoteAttr : EntityNoNameAttr
	{
		public const string Note="Note";
	}
	
	abstract public class SimpleNoNaIEnote : EntityNoName
	{
		/// <summary>
		/// Note
		/// </summary>
		public string Note
		{
			get
			{
				return this.GetValStringByKey(SimpleNoNaIEnoteAttr.Note);
			}
			set
			{
				this.SetValByKey(SimpleNoNaIEnoteAttr.Note,value);
			}
		}

		#region ����
		/// <summary>
		/// �򵥱�����ʵ��
		/// </summary>
		public SimpleNoNaIEnote()
		{}
		/// <summary>
		/// �򵥱�����ʵ��
		/// </summary>
		/// <param name="_No">���</param>
		protected SimpleNoNaIEnote(string _No) : base(_No){}
		/// <summary>
		/// ����
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map(this.PhysicsTable);
				map.EnDesc=this.Desc;

				map.DepositaryOfEntity=Depositary.Application;
				map.DepositaryOfMap=Depositary.Application;


				map.AddTBStringPK(SimpleNoNaIEnoteAttr.No,null,null,true,false,1,20,10);
				map.AddTBString(SimpleNoNaIEnoteAttr.Name,null,null,true,false,0,200,10);
				map.AddTBString(SimpleNoNaIEnoteAttr.Note,null,null,true,false,0,500,10);
 
				this._enMap=map;
				return this._enMap; 
			}
		}		 
		#endregion 		

		#region ��Ҫ������д�ķ���
		/// <summary>
		/// ָ����
		/// </summary>
		protected abstract string PhysicsTable{get;}
		/// <summary>
		/// ����
		/// </summary>
		protected abstract string Desc{get;}
		#endregion 

	
		#region  ��д����ķ�����
		/// <summary>
		/// �����߼�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{
			base.beforeInsert();
			return true;
		}
		/// <summary>
		/// �����߼�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			base.beforeUpdate();
			return true;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete()
		{
			base.beforeDelete();
			return true;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		protected override void afterDelete()
		{
			base.afterDelete();
			return ;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		protected override  void afterInsert()
		{
			base.afterInsert();
			return ;
		}
		/// <summary>
		/// �߼�����
		/// </summary>
		protected override void afterUpdate()
		{
			base.afterUpdate();
			return ;
		}
		#endregion
	}
	abstract public class SimpleNoNaIEnotes : EntitiesNoName
	{	 
		public SimpleNoNaIEnotes()
		{}
	}
}

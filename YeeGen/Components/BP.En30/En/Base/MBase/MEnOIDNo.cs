using System;
using CWAI.En;
using CWAI.En.Base ;
using System.Collections;
using CWAI.DA;
using System.Data;
using CWAI.Sys;
using CWAI;
namespace CWAI.En.Base
{
	/// <summary>
	/// ��ʵ��
	/// </summary>
	abstract public class EnOIDNo : Entity
	{
		#region ��������
		/// <summary>
		/// OID
		/// </summary>
		public int OID
		{
			get
			{
				return this.GetValIntByKey(MEnAttr.OID);
			}
			set
			{
				this.SetValByKey(MEnAttr.OID,value);
			}
		}
		/// <summary>
		/// ʵ����
		/// </summary>
		public string No
		{
			get
			{
				return this.GetValStringByKey(MEnAttr.No);
			}
			set
			{
				this.SetValByKey(MEnAttr.No,value);
			}
		}
		#endregion 

		#region ����
		public EnOIDNo()
		{}
 
		protected EnOIDNo(int _OID)
		{
			this.OID = _OID;
			this.Retrieve();
		} 
		public int RetrieveByNo()
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(MEnAttr.No,this.No) ;
			return qo.DoQuery(); 
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
	/// ����һ��EnsOIDNoʵ��
	/// </summary>
	abstract public class EnsOIDNo : EntitiesOIDNo
	{
		/// <summary>
		/// ����һ��EnsOIDNoʵ��
		/// </summary>
		public EnsOIDNo()
		{
		}
		/// <summary>
		/// ����һ��EnsOIDNoʵ��
		/// </summary>
		/// <param name="no"></param>
		public EnsOIDNo(string no)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MEnAttr.No, no);
			qo.DoQuery();
		}
	} 
}

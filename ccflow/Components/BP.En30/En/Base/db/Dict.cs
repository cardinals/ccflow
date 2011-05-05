using System;
using System.Collections;
using BP.DA;

namespace BP.En.Base
{
	/// <summary>
	/// Dict Attr 
	/// </summary>
	public class DictAttr : EntityOIDAttr
	{
		public const string No="No";
		public const string Name="Name";
	}
	/// <summary>
	/// DictEntity ��ժҪ˵����
	/// </summary>
	abstract public class Dict : EntityOID
	{ 

		#region �����йص��߼�����(�������ֻ��dict EntityNo, �����й�ϵ��)
		/// <summary>
		/// beforeInsert
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{
//			if (this.No=="" || this.No==null)
//				throw new Exception("@��Ų�����Ϊ�ա�") ; 
			base.beforeInsert();
			if (this.EnMap.IsAllowRepeatNo==true)
				return true;
			string No = this.GetValStringByKey("No") ; 
			string sql ="SELECT "+this.EnMap.GetFieldByKey("No")+" FROM "+this.EnMap.PhysicsTable + " WHERE "+this.EnMap.GetFieldByKey("No")+ "='"+No+"'";
			if (DBAccess.RunSQLReturnTable(sql).Rows.Count!=0)			 
				throw new Exception("@["+this.EnMap.EnDesc+" , "+this.EnMap.PhysicsTable+"] ���["+No+"]�ظ���");
			if (this.EnMap.IsCheckNoLength)
			{
				if (this.No.Length!=int.Parse(this.EnMap.CodeStruct))
					throw new Exception("@ ["+this.EnMap.EnDesc+"]���["+this.No+"]���󣬳��Ȳ�����ϵͳҪ�󣬱�����["+int.Parse(this.EnMap.CodeStruct).ToString()+"]λ���������г�����["+this.No.Length.ToString()+"]λ��");
			}
			return true; 
		}
		#endregion 
	
		protected Dict(){}
		/// <summary>
		/// ����base �ķ�����
		/// </summary>
		/// <param name="oid"></param>
		protected Dict(int oid) : base(oid){}
		/// <summary>
		/// ����No ����һ��ʵ����
		/// </summary>
		/// <param name="_no">���</param>
		protected Dict(string _no )
		{
			this.No=_no;
			if (this.RetrieveByNo()==0)
				throw new Exception("@û���ҵ����Ϊ["+this.No+"], ["+this.EnDesc+"]��ʵ��.");
				 
		}
		#region �ṩ������
		/// <summary>
		/// No
		/// </summary>
		public virtual string No
		{
			get
			{
				return this.GetValStringByKey(DictAttr.No);
			}
			set
			{
				this.SetValByKey(DictAttr.No,value);
			}
		}
		/// <summary>
		/// Name
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(DictAttr.Name);
			}
			set
			{
				this.SetValByKey(DictAttr.Name,value);
			}
		}
		#endregion

		#region �ṩ�Ĳ�ѯ����
		/// <summary>
		/// ��No ��ѯ��
		/// </summary>
		/// <returns></returns>
		public int RetrieveByNo() 
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DictAttr.No,this.No);
			int i = qo.DoQuery();
			if (i==0)
			  return 0 ; //throw new Exception("@û���ҵ����Ϊ["+this.No+"]��ʵ��.");
			else
			  return i;
			 
		}	
		/// <summary>
		///  ��Name ��ѯ��
		/// </summary>
		/// <returns></returns>	
		public int RetrieveByName()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DictAttr.Name, this.Name);
			return qo.DoQuery();
		}
		#endregion
}
	abstract public class Dicts : EntitiesOID
	{
		public Dicts()
		{		
		}
	}
}

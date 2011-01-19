using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityOIDMIDAttr
	/// </summary>
	public class EntityOIDMIDAttr:EntityOIDAttr
	{
		/// <summary>
		/// ���ID
		/// </summary>
		public const string MID="MID";

	}
	/// <summary>
	/// ����OID MID ����ʵ��̳�
	/// </summary>
	abstract public class EntityOIDMID : EntityOID
	{		 
		#region ����
		/// <summary>
		/// ����
		/// </summary>
		protected EntityOIDMID(){}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="oid">OID</param>
		protected EntityOIDMID(int oid):base(oid){}
		#endregion 

		#region ���Է���
		/// <summary>
		/// ���ID
		/// </summary>
		public int MID 
		{
			get
			{			 
				return this.GetValIntByKey(EntityOIDMIDAttr.MID);			 
			} 
			set
			{			 
				this.SetValByKey(EntityOIDMIDAttr.MID,value);			 
			} 
		}
		/// <summary>
		/// ���ز�ѯ�����ĸ���
		/// </summary>
		/// <param name="mid">mid</param>
		/// <returns>���ز�ѯ�����ĸ���</returns>
		public int RetrieveByMID(int mid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere("MID",mid );
			return qo.DoQuery();			 
		}
		#endregion
		 
	}
	/// <summary>
	/// ����OID MID ����ʵ��̳�
	/// </summary>
	abstract public class EntitiesOIDMID : EntitiesOID
	{
		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public EntitiesOIDMID()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����
		/// </summary>
		public EntitiesOIDMID(int mid)
		{
			this.RetrieveByMID(mid);			
		}
		#endregion

		#region ��ѯ����
		/// <summary>
		/// ����MID��ѯ�����ز�ѯ�����ĸ��������Ѳ�ѯ�������ʵ�弯���ڡ�
		/// </summary>
		/// <param name="mid">MID</param>
		/// <returns>���ز�ѯ�����ĸ���</returns>
		public int RetrieveByMID(int mid)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere("MID",mid);
			return qo.DoQuery();
		}
		#endregion
	}
}

using System;
using System.Collections;

namespace BP.En.Base
{
	/// <summary>
	/// DictSimple ��ժҪ˵����
	/// �򵥱����� Key val 
	/// </summary>
	abstract public class DictSimple : EntityOID
	{
		protected DictSimple() 
		{
		}
		protected DictSimple(int oid) : base(oid){}

		#region �ṩ������
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

		#region ʵ�ֻ���ķ���
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

}

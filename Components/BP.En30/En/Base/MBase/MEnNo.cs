using System;

namespace BP.En.MBase
{
	/// <summary>
	/// ��ʵ�� ��
	/// </summary>
	abstract public class EnNo:EntityNo
	{
		#region ���캭��
		/// <summary>
		/// EnNo
		/// </summary>
		public EnNo(){}
		/// <summary>
		/// EnNo
		/// </summary>
		/// <param name="_no">_no</param>
		public EnNo(string _no)  :base(_no){}		
		#endregion		 
	}
	/// <summary>
	/// ��ʵ�弯�ϡ�
	/// </summary>
	abstract public class EnsNo : Entities
	{
		public EnsNo(){}
	}
}

using System;
using System.Collections;
using BP.DA;
using BP.En.Base;

namespace BP.Tax
{
	/// <summary>
	/// ��ҵ
	/// </summary>
	public class HY :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����		 
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_HY";
			}
		}	 
		public override string  Desc
		{
			get
			{
				return "��ҵ";			
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��ҵ
		/// </summary> 
		public HY(){}
		/// <summary>
		/// ��ҵ
		/// </summary>
		/// <param name="_No"></param>
		public HY(string _No ): base(_No){}
		#endregion 
	}
	 
	public class HYs :SimpleNoNameFixs
	{
		/// <summary>
		/// ��ҵ����
		/// </summary>
		public HYs(){}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new HY();
			}
		}
	}
}

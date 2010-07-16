using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
//using BP.ZHZS.Base;

namespace BP.Tax
{
	/// <summary>
	///���ڹ�˰�����ջ���
	/// </summary>
	public class ZSJG :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_ZSJG";
			}
		}
		 
		public override string  Desc
		{
			get
			{
				return "���ջ���";
			}
		}
		#endregion 

		#region ���췽��
		 
		public ZSJG(){}
		 
		public ZSJG(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// ���ڹ�˰�����ջ���
	/// </summary>
	public class ZSJGs :SimpleNoNameFixs
	{
		 
		public ZSJGs(){}
		 
		public override Entity GetNewEntity
		{
			get
			{
				return new ZSJG();
			}
		}
	}
}

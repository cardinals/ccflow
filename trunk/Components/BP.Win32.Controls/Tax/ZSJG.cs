using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
//using BP.ZHZS.Base;

namespace BP.Tax
{
	/// <summary>
	///���ڹ�˰�ľ�������
	/// </summary>
	public class JJXZ :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_JJXZ";
			}
		}
		 
		public override string  Desc
		{
			get
			{
				return "��������";
			}
		}
		#endregion 

		#region ���췽��
		 
		public JJXZ(){}
		 
		public JJXZ(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// ���ڹ�˰�ľ�������
	/// </summary>
	public class JJXZs :SimpleNoNameFixs
	{
		 
		public JJXZs(){}
		 
		public override Entity GetNewEntity
		{
			get
			{
				return new JJXZ();
			}
		}
	}
}

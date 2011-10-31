using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{	 
	/// <summary>
	/// ��������
	/// </summary>
	public class Operator :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_Operator";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "��������";				
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
        /// ��������
		/// </summary> 
		public Operator(){}		 
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="_No"></param>
		public Operator(string _No ): base(_No){}
		#endregion 
	}
	 /// <summary>
	 /// ��������s
	 /// </summary>
	public class Operators :SimpleNoNameFixs
	{
		/// <summary>
		/// ��������
		/// </summary>
		public Operators(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Operator();
			}
		}
	}
}

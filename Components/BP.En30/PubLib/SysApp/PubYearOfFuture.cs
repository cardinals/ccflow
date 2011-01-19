using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.Tax
{
	/// <summary>
	/// ����
	/// </summary>
	public class PubYearOfFuture :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_YearOfFuture";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "δ�����";
			}
		}
		#endregion 

		#region ���췽��
		 
		public PubYearOfFuture(){}
		 
		public PubYearOfFuture(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// ����s
	/// </summary>
	public class PubYearOfFutures :SimpleNoNameFixs
	{
		#region ����
		/// <summary>
		/// ����s
		/// </summary>
		public PubYearOfFutures(){}
		/// <summary>
		/// ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PubYearOfFuture();
			}
		}
		#endregion
	}
}

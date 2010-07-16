using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.Tax
{
	/// <summary>
	/// ����
	/// </summary>
	public class PubYearOfHistory :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_YearOfHistory";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "��ʷ���";
			}
		}
		#endregion 

		#region ���췽��
		 
		public PubYearOfHistory(){}
		 
		public PubYearOfHistory(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// ����s
	/// </summary>
	public class PubYearOfHistorys :SimpleNoNameFixs
	{
		#region ����
		/// <summary>
		/// ����s
		/// </summary>
		public PubYearOfHistorys(){}
		/// <summary>
		/// ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PubYearOfHistory();
			}
		}
		#endregion
	}
}

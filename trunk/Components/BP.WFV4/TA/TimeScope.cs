using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.TA
{
	/// <summary>
	/// ʱ���
	/// </summary>
	public class TimeScope :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "TA_TimeScope";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "ʱ���";
			}
		}
		#endregion 

		#region ���췽��
		 
		/// <summary>
		/// ʱ���
		/// </summary>
		public TimeScope(){}
		 
		/// <summary>
		/// ʱ���
		/// </summary>
		/// <param name="_No">���</param>
		public TimeScope(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// ʱ���s
	/// </summary>
	public class TimeScopes :SimpleNoNameFixs
	{
		#region ����
		/// <summary>
		/// ʱ���s
		/// </summary>
		public TimeScopes(){}
		/// <summary>
		/// ʱ���
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TimeScope();
			}
		}
		#endregion
	}
}

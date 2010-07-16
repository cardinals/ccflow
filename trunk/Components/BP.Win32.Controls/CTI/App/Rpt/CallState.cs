using System;
using System.Collections;
using BP.DA;
using BP.En.Base;


namespace BP.CTI.App
{
	/// <summary>
	/// ����
	/// </summary>
	public class CallState :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "CTI_CallState";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "����״̬";
			}
		}
		#endregion 

		#region ���췽��
		 
		public CallState(){}
		 
		public CallState(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// ����s
	/// </summary>
	public class CallStates :SimpleNoNameFixs
	{
		#region ����
		/// <summary>
		/// ����s
		/// </summary>
		public CallStates(){}
		/// <summary>
		/// ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallState();
			}
		}
		#endregion
	}
}

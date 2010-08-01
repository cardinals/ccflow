using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.TA
{
	 
	/// <summary>
	/// ����������
	/// </summary>
	public class TaskGroup :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// Pub_TaskGroup
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "TA_TaskGroup";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "�������";
			}
		}
		#endregion

		#region ���췽��
		/// <summary>
		/// ����������
		/// </summary>
		public TaskGroup(){}	
		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="_No">No</param>
		public TaskGroup(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// TaskGroups
	/// </summary>
	public class TaskGroups : SimpleNoNameFixs
	{
		/// <summary>
		/// ����������		
		/// </summary>
		public TaskGroups(){}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TaskGroup();
			}
		}
	}
}

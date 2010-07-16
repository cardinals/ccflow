
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{	 
	/// <summary>
	/// ����������̵�����
	/// </summary>
	public class FlowCompleteConditionAttr : ConditionAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string NodeID="NodeID";
	}
	/// <summary>
	/// �нڵ�� �ڵ������������
	/// </summary>
	public class FlowCompleteCondition :Condition
	{
		#region ��չ����
		 
		#endregion 

		#region ʵ�ֻ���ķ���
		/// <summary>
		/// ����
		/// </summary>
		protected override string Desc
		{
			get
			{
				return "�����������";
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		protected override string PhysicsTable
		{
			get
			{
			   
				return "WF_FlowCompleteCondition";
			}
		} 
		#endregion

		#region ���췽��
		/// <summary>
		/// �ڵ������������
		/// </summary>
		public FlowCompleteCondition(){}
	    
		 
		#endregion

		
	}
	/// <summary>
	/// ����������̵�����
	/// </summary>
	/// <summary>
	/// �ڵ������������s
	/// һ����˵��ֻ��һ������.
	/// ����������֮���� or  ��ϵ.
	/// ����ж������,����,����һ��������ͨ��.
	/// </summary>
	public class FlowCompleteConditions :Conditions
	{
		#region ����
		/// <summary>
		/// �ڵ������������
		/// </summary>
		public FlowCompleteConditions(){}
		/// <summary>
		/// �ڵ����������������
		/// </summary>
		/// <param name="nodeID">�ӽڵ�</param>	
		public FlowCompleteConditions(int nodeID ): base(nodeID){}		 
		#endregion

		#region ʵ�ֻ���ķ���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				FlowCompleteCondition  en = new FlowCompleteCondition();
				en.NodeID=this.NodeID;
				return en;
			}			 
		}
		#endregion
	}
}

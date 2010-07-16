
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{
	/// <summary>
	/// �ڵ�������������
	/// </summary>
	public class NodeCompleteConditionAttr : ConditionAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string NodeID="NodeID";
	}
	/// <summary>
	/// �нڵ�� �ڵ������������
	/// </summary>
	public class NodeCompleteCondition :Condition
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
				return "�ڵ���ɹ���������";
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		protected override string PhysicsTable
		{
			get
			{
				return "WF_NodeCompleteCondition";
			}
		} 
		#endregion

		#region ���췽��
		/// <summary>
		/// �ڵ������������
		/// </summary>
		public NodeCompleteCondition(){}
        /// <summary>
        /// �ڵ������������
        /// </summary>
        /// <param name="mainNodeID"></param>
        public NodeCompleteCondition(string mypk) : base(mypk) { }
		#endregion
	}
	/// <summary>
	/// �ڵ������������s
	/// һ����˵��ֻ��һ������.
	/// ����������֮���� or  ��ϵ.
	/// ����ж������,����,����һ��������ͨ��.
	/// </summary>
	public class NodeCompleteConditions :Conditions
	{
		#region ����
		/// <summary>
		/// �ڵ������������
		/// </summary>
		public NodeCompleteConditions(){}
		/// <summary>
		/// �ڵ����������������
		/// </summary>
		/// <param name="nodeID">�ӽڵ�</param>		
		public NodeCompleteConditions(int nodeID):base(nodeID){}
		#endregion

		#region ʵ�ֻ���ķ���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				NodeCompleteCondition  en = new NodeCompleteCondition();
				en.NodeID=this.NodeID;
				return en;
			}
		}
		#endregion
	}
}

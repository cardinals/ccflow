
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Sys;
//using BP.ZHZS.Base;

namespace BP.WF
{	 
	/// <summary>
	/// �ڵ�������������
	/// </summary>
    public class ConditionNodeAttr : ConditionAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string NodeID = "NodeID";


      
    }
	/// <summary>
	/// �нڵ�� �ڵ������������
	/// </summary>
	abstract public class ConditionNode : Condition
	{
		#region ����������
		#endregion 

		#region ��չ����
		/// <summary>
		/// ���Ľڵ�
		/// </summary>
		public Node HisNode
		{
			get
			{
				return new Node(this.NodeID);
			}
		}		
		#endregion 

		
		#region ���췽��
		/// <summary>
		/// �ڵ������������
		/// </summary>
		public ConditionNode(){}

        public ConditionNode(int mainID)
        {
            this.NodeID = mainID;
            this.Retrieve();
        }
		/// <summary>
		/// ����
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map(this.PhysicsTable);
                map.EnDesc = this.Desc;
                map.AddTBIntPK(ConditionNodeAttr.NodeID, 0, "MainID", true, true);
                map.AddTBInt(ConditionNodeAttr.FK_Node, 0, "�ڵ�ID", true, true);
                map.AddTBInt(ConditionNodeAttr.FK_Attr, 0, "����", true, true);
                map.AddTBString(ConditionNodeAttr.AttrKey, null, "����", true, true, 1, 60, 20);

                map.AddTBString(ConditionNodeAttr.FK_Operator, "=", "�������", true, true, 1, 60, 20);
                map.AddTBString(ConditionNodeAttr.OperatorValue, "", "Ҫ�����ֵ", true, true, 1, 60, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion

		#region ��������
		
		#endregion
	}
	/// <summary>
	/// �ڵ������������s
	/// һ����˵��ֻ��һ������.
	/// ����������֮���� or  ��ϵ.
	/// ����ж������,����,����һ��������ͨ��.
	/// </summary>
	abstract public class ConditionsNode :Conditions
	{
		/// <summary>
		/// nodeid
		/// </summary>
	    protected int NodeId=0;

		#region ����
		/// <summary>
		/// �ڵ������������
		/// </summary>
		public ConditionsNode(){}
		/// <summary>
		/// �ڵ����������������
		/// </summary>
		/// <param name="nodeID">�ӽڵ�</param>		
		public ConditionsNode(int nodeID)
		{
			this.NodeId=nodeID;
			QueryObject qo = new QueryObject(this);
            qo.AddWhere(ConditionNodeAttr.NodeID, nodeID);
			qo.DoQuery();
		}
		#endregion

		#region ʵ�ֻ���ķ���
	 
		#endregion
	}
}

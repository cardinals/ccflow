using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
//using BP.ZHZS.DS;


namespace BP.WF
{

	/// <summary>
	/// �ڵ�����
	/// </summary>
	public enum FlowCheckNodeType
	{
		/// <summary>
		/// ��׼����
		/// </summary>
		GECheckStand,
		/// <summary>
		/// �������
		/// </summary>
		NumCheck
	}
	 
	/// <summary>
	/// ������ÿ���ڵ����Ϣ.	 
	/// </summary>
	public class FlowCheckNode 
	{
		/// <summary>
		/// wsss
		/// </summary>
		public string EnsName
		{
			get
			{
                if (FlowCheckNodeType == FlowCheckNodeType.GECheckStand)
                    return "BP.WF.GECheckStands";
                else
                    return "BP.WF.NumChecks";
			}
		}
        public string EnName
        {
            get
            {
                if (FlowCheckNodeType == FlowCheckNodeType.GECheckStand)
                    return "BP.WF.GECheckStand";
                else
                    return "BP.WF.NumCheck";
            }
        }
		/// <summary>
		/// �ڵ�ID
		/// </summary>
		public int NodeID;
		/// <summary>
		/// ����
		/// </summary>
		public string Name;
		/// <summary>
		/// ddd
		/// </summary>
		public FlowCheckNodeType FlowCheckNodeType=FlowCheckNodeType.GECheckStand;
	}
	/// <summary>
	/// �ڵ㼯��
	/// </summary>
	public class FlowCheckNodes : System.Collections.CollectionBase
	{

		#region ���췽��
		/// <summary>
		/// �ڵ㼯��
		/// </summary>
		public FlowCheckNodes()
		{		
		}		 
		#endregion

		#region 
		/// <summary>
		/// ����λ��ȡ������
		/// </summary>
		public FlowCheckNode this[int index]
		{
			get 
			{
				return (FlowCheckNode)this.InnerList[index];
			}
		}
		/// <summary>
		/// ��������ӵ�����β������������Ѿ����ڣ������
		/// </summary>
		/// <param name="flowNode">Ҫ��ӵĶ���</param>
		/// <returns>������ӵ��ĵط�</returns>
		public virtual int Add(FlowCheckNode flowNode)
		{
			return this.InnerList.Add(flowNode);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="NodeID">�ڵ�ID</param>
		/// <param name="NodeName">�ڵ�����</param>
		/// <param name="type">FlowCheckNodeType</param>
		public void  Add( int NodeID,string NodeName, FlowCheckNodeType type )
		{
			FlowCheckNode fcn = new FlowCheckNode();
			fcn.Name=NodeName;
			fcn.NodeID=NodeID;
			fcn.FlowCheckNodeType = type;
			this.Add(fcn);
		}					
		#endregion 
	}
	
}

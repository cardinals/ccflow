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
	/// ��������
	/// </summary>
	public class DirectionConditionAttr:ConditionAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string NodeID="NodeID";
		/// <summary>
		/// Ҫת���Ľڵ�
		/// </summary>
		public const string ToNodeID="ToNodeID";
		/// <summary>
		/// ��Key
		/// </summary>
		public const string Groupkey="Groupkey";		
	}
	/// <summary>
	/// �ڵ㷽������
	/// </summary>
	public class DirectionCondition :Condition
	{
		#region �߼�����
        protected override string PhysicsTable
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        protected override string Desc
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
		/// <summary>
		/// �ڸ��������֮ǰҪ���ò�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

		#region ��������
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey("MyPK");
            }
            set
            {
                this.SetValByKey("MyPK", value);
            }
        }
		/// <summary>
		/// �ڵ���Ϣ
		/// </summary>
		public int NodeID
		{
			get
			{
			   return this.GetValIntByKey(DirectionConditionAttr.NodeID);
			}
			set
			{
				this.SetValByKey(DirectionConditionAttr.NodeID,value);
			}
		}
		/// <summary>
		/// Ҫת��Ľڵ�
		/// </summary>
		public int ToNodeID
		{
			get
			{
				return this.GetValIntByKey(DirectionConditionAttr.ToNodeID);
			}
			set
			{
				this.SetValByKey(DirectionConditionAttr.ToNodeID,value);
			}
		}
		#endregion 

		#region ʵ�ֻ����ķ�����
		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ڵ㷽������
		/// </summary>
		public DirectionCondition()
		{
		}
		/// <summary>
		/// �ڵ㷽������
		/// </summary>
		/// <param name="nodeID">�ڵ���Ϣ</param>
		/// <param name="toNodeID">Ҫת��Ľڵ�</param>		 
		public DirectionCondition(int nodeID, int toNodeID)
		{
            this.NodeID = nodeID;
            this.ToNodeID = toNodeID;
            this.MyPK = nodeID + "_" + toNodeID;
            this.Retrieve();
		}
		/// <summary>
		/// ����
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("WF_DirectionCondition");
				map.EnDesc="ת������";

                map.AddMyPK();
				map.AddTBInt(DirectionConditionAttr.NodeID,0,"�ڵ�",true,true);
				map.AddTBInt(DirectionConditionAttr.ToNodeID,0,"ת��ڵ�",true,true);


                map.AddTBInt(DirectionConditionAttr.FK_Node, 0, "�ڵ�", true, true);
                map.AddTBInt(DirectionConditionAttr.FK_Attr, 0, "����OID", true, true);
				map.AddTBString(DirectionConditionAttr.AttrKey,null,"����ֵ",true,true,0,60,20);
				map.AddTBString(DirectionConditionAttr.FK_Operator,null,"�������",true,true,0,60,20); 
				map.AddTBString(DirectionConditionAttr.OperatorValue,null,"Ҫ�����ֵ",true,true,0,60,20);

				this._enMap=map;
				return this._enMap;
			}
		}
        protected override bool beforeUpdate()
        {
            this.MyPK = this.NodeID + "_" + this.ToNodeID;
            return base.beforeUpdate();
        }
        protected override bool beforeInsert()
        {
            this.MyPK = this.NodeID + "_" + this.ToNodeID;
            return base.beforeInsert();
        }
		#endregion
	 
	}
	/// <summary>
	/// �ڵ㷽������s
	/// </summary>
	public class DirectionConditions :Conditions
	{
		#region ��������
		private int  fromNode=0;
		private int  toNode=0;
		#endregion 

		#region ����
		/// <summary>
		/// �ڵ㷽������
		/// </summary>
        public DirectionConditions() { }
		/// <summary>
		/// �ڵ㷽������
		/// </summary>
		/// <param name="fromNode">�ӽڵ�</param>
		/// <param name="toNode">���ڵ�</param>
		/// <param name="operatorPK">������PK</param>
        public DirectionConditions(int fromNode, int toNode)
        {
            this.fromNode = fromNode;
            this.toNode = toNode;

            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DirectionConditionAttr.NodeID, fromNode);
            qo.addAnd();
            qo.AddWhere(DirectionConditionAttr.ToNodeID, toNode);
            qo.DoQuery();
        }
		#endregion

		#region ��д����ķ���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				DirectionCondition en = new DirectionCondition();
				en.ToNodeID=this.toNode ;
				en.NodeID = this.fromNode;
                en.MyPK = this.toNode + "_" + this.fromNode;
				return en;
			}
		}
		#endregion
	}
}

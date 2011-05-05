using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
//using BP.ZHZS.Base;

namespace BP.WF
{	
	/// <summary>
	/// ��������
	/// </summary>
    public class ConditionAttr
    {

        /// <summary>
        /// ����Key
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// ����Key
        /// </summary>
        public const string AttrKey = "AttrKey";
        /// <summary>
        /// ����
        /// </summary>
        public const string AttrName = "AttrName";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Attr = "FK_Attr";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_Operator = "FK_Operator";
        /// <summary>
        /// �����ֵ
        /// </summary>
        public const string OperatorValue = "OperatorValue";
        /// <summary>
        /// ����ֵ
        /// </summary>
        public const string OperatorValueT = "OperatorValueT";
        /// <summary>
        /// Node
        /// </summary>
        public const string FK_Node = "FK_Node";
    }
	/// <summary>
	/// ��������
	/// </summary>
	abstract public class Condition :EntityMyPK
	{
        public string ConditionDesc
        {
            get
            {
                return "";
            }
        }
        /// <summary>
        /// Ҫ����Ľڵ�
        /// </summary>
        public Node HisNode
        {
            get
            {
                return new Node(this.NodeID);
            }
        }
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(ConditionAttr.NodeID);
            }
            set
            {
                this.SetValByKey(ConditionAttr.NodeID, value);
            }
        }
        public int FK_Node
        {
            get
            {
                int i= this.GetValIntByKey(ConditionAttr.FK_Node);
                if (i == 0)
                    return this.NodeID;
                return i;
            }
            set
            {
                this.SetValByKey(ConditionAttr.FK_Node, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                Node nd = new Node(this.FK_Node);
                return nd.Name;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int FK_Attr
        {
            get
            {
                return this.GetValIntByKey(ConditionAttr.FK_Attr);
            }
            set
            {
                this.SetValByKey(ConditionAttr.FK_Attr, value);
            }
        }
        public string FK_AttrT
        {
            get
            {
                BP.Sys.MapAttr attr = new BP.Sys.MapAttr(this.FK_Attr);
                return attr.Name;
            }
        }
		/// <summary>
		/// �ڸ��������֮ǰҪ���ò�����
		/// </summary>
		/// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            this.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0");
            this.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_NodeCompleteCondition)");
            this.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_FlowCompleteCondition)");
            return base.beforeUpdateInsertAction();
        }

        #region ��Ҫ������д�ķ���
        /// <summary>
        /// ָ����
        /// </summary>
        protected abstract string PhysicsTable { get;}
        /// <summary>
        /// ����
        /// </summary>
        protected abstract string Desc { get;}
        #endregion 


		#region ʵ�ֻ����ķ�����
	 
		/// <summary>
		/// Ҫ�����ʵ������
		/// </summary>
		public string AttrKey
		{
			get
			{
				return this.GetValStringByKey(ConditionAttr.AttrKey);
			}
			set
			{
				this.SetValByKey(ConditionAttr.AttrKey,value);
			}
		}
        public string AttrName
        {
            get
            {
                return this.GetValStringByKey(ConditionAttr.AttrName);
            }
            set
            {
                this.SetValByKey(ConditionAttr.AttrName, value);
            }
        }
        public string OperatorValueT
        {
            get
            {
                return this.GetValStringByKey(ConditionAttr.OperatorValueT);
            }
            set
            {
                this.SetValByKey(ConditionAttr.OperatorValueT, value);
            }
        }	
		/// <summary>
		/// �������
		/// </summary>
		public string FK_Operator
		{
            get
            {
                string s = this.GetValStringByKey(ConditionAttr.FK_Operator);
                if (s == null || s == "")
                    return "=";
                return s;
            }
			set
			{
				this.SetValByKey(ConditionAttr.FK_Operator,value);
			}
		}
		/// <summary>
		/// ����ֵ
		/// </summary>
		public object OperatorValue
		{
			get
			{
				return this.GetValStringByKey(ConditionAttr.OperatorValue);
			}
			set
			{
				this.SetValByKey(ConditionAttr.OperatorValue,value);
			}
		}
        public string OperatorValueStr
        {
            get
            {
                return this.GetValStringByKey(ConditionAttr.OperatorValue);
            }
        }
        public int OperatorValueInt
        {
            get
            {
                return this.GetValIntByKey(ConditionAttr.OperatorValue);
            }
        }
        public bool OperatorValueBool
        {
            get
            {
                return this.GetValBooleanByKey(ConditionAttr.OperatorValue);
            }
        }
        public Int64 WorkID = 0;
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��������
		/// </summary>
		public Condition(){}
        public Condition(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }

		#endregion 

		#region ��������
		/// <summary>
		/// ��������ܲ���ͨ��
		/// </summary>
		public virtual bool IsPassed
		{
			get
			{

                Work en = this.HisNode.HisWork;
                try
                {
                    en.SetValByKey("OID", this.WorkID);
                    en.Retrieve();
                }
                catch (Exception ex)
                {
                    throw new Exception("@��ȡ���ж�����ʵ��[" + en.EnDesc + "], ���ִ���:" + ex.Message + "@����ԭ���Ƕ������̵��ж��������ִ���,��������ѡ����ж������������ǵ�ǰ�����ڵ����һ���������,ȡ������ʵ���ʵ��.");
                }

				switch( this.FK_Operator.Trim() )
				{
					case "=":  // ����� = 
						if (en.GetValStringByKey(this.AttrKey)==this.OperatorValue.ToString())
							return true;
						else
							return false;
					 
					case ">":
						if ( en.GetValDoubleByKey(this.AttrKey) > Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
					 
					case ">=":
						if (  en.GetValDoubleByKey(this.AttrKey) >= Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
				 
					case "<":
						if (  en.GetValDoubleByKey(this.AttrKey) < Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
					 
					case "<=":
						if (  en.GetValDoubleByKey(this.AttrKey) <= Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
						 
					case "!=":
						if (  en.GetValDoubleByKey(this.AttrKey) != Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
					case "like":
						if (  en.GetValStringByKey(this.AttrKey).IndexOf(this.OperatorValue.ToString()) == -1   )
							return false;
						else
							return true;
					default :
						throw new Exception("@û���ҵ���������..");
				}
				 
			}
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

                map.AddMyPK();

                map.AddTBInt(ConditionAttr.NodeID, 0, "MainID", true, true);
                map.AddTBInt(ConditionAttr.FK_Node, 0, "�ڵ�ID", true, true);
                map.AddTBInt(ConditionAttr.FK_Attr, 0, "����", true, true);
                map.AddTBString(ConditionAttr.AttrKey, null, "����", true, true, 0, 60, 20);
                map.AddTBString(ConditionAttr.AttrName, null, "��������", true, true, 0, 60, 20);

                map.AddTBString(ConditionAttr.FK_Operator, "=", "�������", true, true, 0, 60, 20);
                map.AddTBString(ConditionAttr.OperatorValue, "", "Ҫ�����ֵ", true, true, 0, 60, 20);
                map.AddTBString(ConditionAttr.OperatorValueT, "", "Ҫ�����ֵT", true, true, 0, 60, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// ��������s
	/// </summary>
	abstract public class Conditions :En.Base.Entities
	{
		#region public����
		/// <summary>
		/// ������������������ǲ��Ƕ�����.
		/// </summary>
		public bool IsAllPassed
		{
            get
            {
                if (this.Count == 0)
                    throw new Exception("@û��Ҫ�жϵļ���.");

                foreach (Condition en in this)
                {
                    if (en.IsPassed == false)
                        return false;
                }
                return true;
            }
		}
		/// <summary>
		/// �ǲ������е�һ��passed. 
		/// </summary>
		public bool IsOneOfConditionPassed
		{
            get
            {
                foreach (Condition en in this)
                {
                    if (en.IsPassed == true)
                        return true;
                }
                return false;
            }
		}
		/// <summary>
		/// ȡ������һ�������������. 
		/// </summary>
		public Condition GetOneOfConditionPassed
		{
			get
			{				 
				foreach(Condition en in this)
				{
					if (en.IsPassed==true)
						return en;
				}
				throw new Exception("@û�����������");
			}
		}
        public int NodeID = 0;
		#endregion 

		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		public Conditions(){}
        public Conditions(int nodeID) 
        {
            this.NodeID = nodeID;
            this.Retrieve(ConditionAttr.NodeID, nodeID);
        }		 

		#endregion

	}
}

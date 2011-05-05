using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port; 
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// ��Ϣ
	/// </summary>
	public class CHOfNodeAttr 
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// �ؾ�
		/// </summary>
		public const  string FK_XJ="FK_XJ";

		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_NY="FK_NY";
		/// <summary>
		/// ���������ֶ�
		/// </summary>
		public const  string Spec="Spec";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_AP_del="FK_AP";
		/// <summary>
		/// empid
		/// </summary>
		public const  string EmpID="EmpID";

		/// <summary>
		/// ����ID
		/// </summary>
		public const  string WorkID="WorkID";
		/// <summary>
		/// ���˵Ľڵ�
		/// </summary>
		public const  string FK_Node="FK_Node";
		/// <summary>
		/// �ڵ�״̬
		/// </summary>
		public const  string NodeState="NodeState";
		 
		/// <summary>
		/// ִ��Ա
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// ���˵Ĺ�����λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		/// <summary>
		/// �����ķ���ʱ��
		/// </summary>
		public const  string RDT="RDT";		 	 
		/// <summary>
		/// �������
		/// </summary>
		public const  string CDT="CDT";
		/// <summary>
		/// Ӧ���������
		/// </summary>
		public const  string SDT="SDT";
		/// <summary>
		/// ��ɹ���������
		/// </summary>
		public const  string NodeSwinkCent="NodeSwinkCent";

		/// <summary>
		/// ��Ҫ���������ڣ�
		/// </summary>
		public const  string NodeDeductDays="NodeDeductDays";

		/// <summary>
		/// �۷��ʣ���/�죩
		/// </summary>
		public const  string NodeDeductCent="NodeDeductCent";
		/// <summary>
		/// ��߿۷�
		/// </summary>
		public const  string NodeMaxDeductCent="NodeMaxDeductCent";

		/// <summary>
		/// ʵ�ʵ÷�
		/// </summary>
		public const  string Cent="Cent";
		/// <summary>
		/// �ӷ�
		/// </summary>
		public const  string CentOfAdd="CentOfAdd";
		/// <summary>
		/// �۷�
		/// </summary>
		public const  string CentOfCut="CentOfCut";
		/// <summary>
		/// �������˵÷�
		/// </summary>
		public const  string CentOfQU="CentOfQU";

		/// <summary>
		/// ׷���˴�
		/// </summary>
		public const  string ZJRC="ZJRC";
		/// <summary>
		/// SpanDays
		/// </summary>
		public const  string SpanDays="SpanDays";
		/// <summary>
		/// Note
		/// </summary>
		public const  string Note="Note";
        /// <summary>
        /// ������Ա
        /// </summary>
		public const  string Emps="Emps";
		#endregion
	}
	/// <summary>
	/// ��Ϣ
	/// </summary>
	public class CHOfNode : Entity
	{
		#region ��������
		/// <summary>
		/// ׷���˴�
		/// </summary>
		public float ZJRC
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.ZJRC);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.ZJRC,value);
			}
		}

		public float CentOfAdd
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.CentOfAdd);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.CentOfAdd,value);
			}
		}
        public Int64 WorkID
		{
			get
			{
				return this.GetValInt64ByKey(CHOfNodeAttr.WorkID);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.WorkID,value);
			}
		}
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Node,value);
			}
		}
		public NodeState NodeState
		{
			get
			{
				return (NodeState)this.GetValIntByKey(CHOfNodeAttr.NodeState);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeState,(int)value);
			}
		}
		public float CentOfCut
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.CentOfCut);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.CentOfCut,value);
			}
		}
		public float Cent
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.Cent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Cent,value);
			}
		}
		public float NodeDeductCent
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.NodeDeductCent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeDeductCent,value);
			}
		}
		public int SpanDays
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.SpanDays);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.SpanDays,value);
			}
		}
		public string  Emps
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.Emps);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Emps,value);
			}
		}
		public string  Spec
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.Spec);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Spec,value);
			}
		}
		 

		public string  RDT
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.RDT);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.RDT,value);
			}
		}
		public string  SDT
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.SDT);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.SDT,value);
			}
		}
      
		 
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Dept,value);
			}
		}
		public string  FK_Station
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Station);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Station,value);
			}
		}
		public string  FK_StationText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Station);
			}
		}
		public string  FK_DeptText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Dept);
			}
		}
		public string  FK_NY
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_NY);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_NY,value);
			}
		}
		
		public string  CDT
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.CDT);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.CDT,value);
			}
		}
		public string  FK_Flow
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Flow);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Flow,value);
			}
		}
		public string  FK_FlowText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Flow);
			}
		}
		public string  FK_NodeText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Node);
			}
		}
	
		public string  FK_Emp
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Emp,value);
			}
		}
		public string  FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Emp);
			}
		}
		public string  NodeStateText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.NodeState);
			}
		}
		public string  Note
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.Note);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Note,value);
			}
		}
		 
		#endregion 

		#region ���캯��
		/// <summary>
		/// ��Ϣ
		/// </summary>
		public CHOfNode(){}
		/// <summary>
		/// 
		/// </summary>
		public CHOfNode(int oid)
		{
			string sql="SELECT WorkID,FK_Node  FROM WF_CHOfNode WHERE WorkID+FK_Node="+oid;
			DataTable dt  = DBAccess.RunSQLReturnTable(sql);
			if (dt.Rows.Count==0)
				throw new Exception("error CHOfNode oid= "+oid);

			this.WorkID =int.Parse( dt.Rows[0][0].ToString()) ;
			this.FK_Node=int.Parse( dt.Rows[0][1].ToString()) ;
			this.Retrieve();

		}
		public CHOfNode(int workid, int FK_Node,string fk_emp)
		{
			this.WorkID=workid;
			this.FK_Node=FK_Node;
            this.FK_Emp = fk_emp;
			this.Retrieve();
		}
        /// <summary>
        /// �Ƿ��Ҵ����
        /// </summary>
        public bool IsMyDeal
        {
            get
            {
                return this.GetValBooleanByKey("IsMyDeal");
            }
            set
            {
                this.SetValByKey("IsMyDeal", value);
            }
        }

		public int NodeSwinkCent
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.NodeSwinkCent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeSwinkCent,value);
			}
		}
		public float NodeMaxDeductCent
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.NodeMaxDeductCent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeMaxDeductCent,value);
			}
		}

		public int NodeDeductDays
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.NodeDeductDays);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeDeductDays,value);
			}
		}

		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
               // Map map = new Map("WF_CHOfNode");
                 Map map = new Map("WF_CHOfNode");

                map.EnDesc = "����";
                map.EnType = EnType.App;

                map.AddDDLEntities(CHOfNodeAttr.FK_Flow, null, "����", new Flows(), false);

                map.AddTBIntPK(CHOfNodeAttr.WorkID, 0, "WorkID", false, true);
                map.AddDDLEntitiesPK(CHOfNodeAttr.FK_Node, 0, DataType.AppInt, "�ڵ�", new Nodes(), NodeAttr.NodeID, NodeAttr.Name, false);
                map.AddDDLEntitiesPK(CHOfNodeAttr.FK_Emp, BP.Web.WebUser.No, "��Ա", new BP.Port.Emps(), false);

                map.AddDDLSysEnum(CHOfNodeAttr.NodeState, 0, "�ڵ�״̬", true, false);

                map.AddDDLEntities(CHOfNodeAttr.FK_Station, BP.Web.WebUser.FK_Station, "��λ", new Stations(), false);
                map.AddDDLEntities(CHOfNodeAttr.FK_Dept, null, "����", new Depts(), false);
                map.AddDDLEntities(CHOfNodeAttr.FK_Dept, BP.Web.WebUser.FK_Dept, "����", new Depts(), false);
                //map.AddDDLEntities(CHOfNodeAttr.FK_XJ,BP.Web.WebUser.FK_DeptOfXJ,"�ؾ�",new BP.Port.XJs(),false);
                map.AddDDLEntities(CHOfNodeAttr.FK_NY, DataType.CurrentYearMonth, "����", new BP.Pub.NYs(), true);

                map.AddTBDateTime(CHOfNodeAttr.RDT, "����ʱ��", true, true);
                map.AddTBDateTime(CHOfNodeAttr.CDT, "ʵ�����ʱ��", true, true);
                map.AddTBDateTime(CHOfNodeAttr.SDT, "Ӧ�������", true, true);

                map.AddTBInt(CHOfNodeAttr.SpanDays, 0, "�����", false, false);
                map.AddTBInt(CHOfNodeAttr.NodeDeductDays, 0, "����", true, true);
                map.AddTBFloat(CHOfNodeAttr.NodeDeductCent, 0, "�۷��ʣ���/�죩", true, true);
                map.AddTBFloat(CHOfNodeAttr.NodeMaxDeductCent, 0, "��߿۷�", true, true);

                map.AddTBFloat(CHOfNodeAttr.NodeSwinkCent, 0, "��ɹ���������", false, true);

                map.AddTBFloat(CHOfNodeAttr.CentOfAdd, 0, "�ӷ�", true, true);
                map.AddTBFloat(CHOfNodeAttr.CentOfCut, 0, "�۷�", true, true);

                // map.AddTBFloat(CHOfNodeAttr.CentOfQU, 0, "�������˵÷�", false, false);

                map.AddTBFloat(CHOfNodeAttr.Cent, 0, "�÷�", false, false);
                map.AddTBString(CHOfNodeAttr.Spec, null, "���������ֶ�", false, true, 0, 200, 10);
                map.AddTBString(CHOfNodeAttr.Emps, null, "Emps", false, true, 0, 500, 10);
                map.AddTBString(CHOfNodeAttr.Note, "��", "�۷�ԭ��", false, true, 0, 2000, 20);

                map.AddTBInt("ZJRC", 0, "׷���˴�", false, false);
                map.AddTBInt("IsMyDeal", 1, "�Ƿ��Ҵ����", false, false);

                //��Ӳ�ѯ����
                //map.AttrsOfSearch.AddFromTo("����",CHOfNodeAttr.RDT,DateTime.Now.AddDays(-7).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
                map.AddSearchAttr(CHOfNodeAttr.FK_Dept);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Flow);
                map.AddSearchAttr(CHOfNodeAttr.FK_NY);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Dept);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Station);
                map.AddSearchAttr(CHOfNodeAttr.FK_Emp);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Node);
                map.AddSearchAttr(CHOfNodeAttr.NodeState);

                //�Ҽ���ӹ������湦��
                //				RefMethod rm = new RefMethod();
                //				rm.Title="���ڵ�";
                //				rm.ClassMethodName=this.ToString()+".DoShowWorkRpt()";
                //				rm.Icon="/Images/Btn/Rpt.gif";
                //				rm.Width=0;
                //				rm.Height=0;
                //				rm.HisAttrs=null;
                //				rm.Target=null;
                //				map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
		}
		//�Ҽ���ӹ������湦������
        public string DoShowWorkRpt()
        {
            PubClass.WinOpen("../../" + SystemConfig.AppName + "/WF/WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID);
            return null;
        }
		#endregion

		#region ���ػ��෽��
        protected override bool beforeUpdateInsertAction()
        {
            if (this.CDT == "��")
                this.NodeState = NodeState.Init;
            else
                this.NodeState = NodeState.Complete;

            return base.beforeUpdateInsertAction();
        }
		#endregion	
	}
	/// <summary>
	/// ��Ϣs BP.Port.FK.CHOfNodes
	/// </summary>
	public class CHOfNodes : Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CHOfNode();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public CHOfNodes(){}
		#endregion
	}
	
}

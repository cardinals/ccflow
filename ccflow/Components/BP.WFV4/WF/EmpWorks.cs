
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port ; 
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// ��Ա����
	/// </summary>
	public class EmpWorkAttr 
	{
		#region ��������
        public const string FK_Emp = "FK_Emp";

		/// <summary>
		/// ��˰���
		/// </summary>
		public const  string WorkID="WorkID";
		/// <summary>
		/// ������
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const  string WFState="WFState";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Title="Title";
		/// <summary>
		/// ��¼��
		/// </summary>
		public const  string Rec="Rec";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const  string RDT="RDT";
        /// <summary>
        /// ��ǰ��Ӧ�������
        /// </summary>
        public const string SDT = "SDT";
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public const  string CDT="CDT";		
		/// <summary>
		/// �÷�
		/// </summary>
		public const  string Cent="Cent";		
		/// <summary>
		/// note
		/// </summary>
		public const  string FlowNote="FlowNote";
		/// <summary>
		/// ��ǰ�������Ľڵ�.
		/// </summary>
		public const  string FK_Node="FK_Node";
		/// <summary>
		/// ��ǰ������λ
		/// </summary>
		public const  string FK_Station="FK_Station";

		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// ˰�������
		/// </summary>
		public const  string FK_Taxpayer="FK_Taxpayer";
		/// <summary>
		/// ��˰������
		/// </summary>
		public const  string TaxpayerName="TaxpayerName";


		#endregion
	}
	/// <summary>
	/// ��Ա����
	/// </summary>
	public class EmpWork : Entity
	{	
		#region ��������
		/// <summary>
		/// HisFlow
		/// </summary>
		public Flow HisFlow
		{
			get
			{
				return new Flow(this.FK_Flow); 
			}
		}
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.FK_Flow);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Flow,value);
			}
		}
		public string  FK_FlowText
		{
			get
			{
                //Flow fl = new Flow(this.FK_Flow);
                //return fl.Name;
				return this.GetValRefTextByKey(EmpWorkAttr.FK_Flow);
			}
		}
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(EmpWorkAttr.WorkID);
            }
            set
            {
                SetValByKey(EmpWorkAttr.WorkID, value);
            }
        }
		/// <summary>
		/// ��ǰ�Ĺ�����λ
		/// </summary>
		public string  FK_Station
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.FK_Station);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Station,value);
			}
		}
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.FK_Dept);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.Title);
			}
			set
			{
				SetValByKey(EmpWorkAttr.Title,value);
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.RDT);
			}
			set
			{
				SetValByKey(EmpWorkAttr.RDT,value);
			}
		}
        /// <summary>
        /// /Ӧ���������
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(EmpWorkAttr.SDT);
            }
            set
            {
                SetValByKey(EmpWorkAttr.SDT, value);
            }
        }
//		/// <summary>
//		/// ���̱�ע
//		/// </summary>
//		public string  FlowNote
//		{
//			get
//			{
//				return this.GetValStringByKey(EmpWorkAttr.FlowNote);
//			}
//			set
//			{
//				SetValByKey(EmpWorkAttr.FlowNote,value);
//			}
//		}		
		 
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(EmpWorkAttr.Rec);
            }
            set
            {
                this.SetValByKey(EmpWorkAttr.Rec, value);
            }
        }
		public string RecText
		{
			get
			{
              //  return this.Rec;
				return this.GetValRefTextByKey(EmpWorkAttr.Rec );
			}
		}
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(EmpWorkAttr.FK_Node);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Node,value);
			}
		}		
		public string  FK_NodeText
		{
            get
            {
                return this.GetValRefTextByKey(EmpWorkAttr.FK_Node);
            }
		}
		/// <summary>
		/// ��������״̬( 0, δ���,1 ���, 2 ǿ����ֹ 3, ɾ��״̬,) 
		/// </summary>
		public int  WFState
		{
			get
			{
				return this.GetValIntByKey(EmpWorkAttr.WFState);
			}
			set
			{
				SetValByKey(EmpWorkAttr.WFState,value);
			}
		}
		public string  WFStateText
		{
			get
			{
				return this.GetValRefTextByKey(EmpWorkAttr.WFState);
			}
		} 
		/// <summary>
		/// ����״̬
		/// </summary>
		public string WFStateLab
		{
			get
			{
				return this.GetValRefTextByKey(EmpWorkAttr.WFState);
			}
		}
		#endregion

		#region ���캯��
		/// <summary>
		/// ��Ա��������
		/// </summary>
		public EmpWork()
		{
		}		 
		public EmpWork(int workId)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EmpWorkAttr.WorkID, workId);
			if (qo.DoQuery()==0)
				throw new Exception("����["+workId+"]�����ڣ��������Ѿ���ɡ�");
		}
		/// <summary>
		/// ��Ա��������
		/// </summary>
		/// <param name="workId">��������ID</param>
		/// <param name="flowNo">���̱��</param>
		public EmpWork(Int64 workId, string flowNo)
		{
            try
            {
                this.WorkID = workId;
                this.FK_Flow = flowNo;
                this.Retrieve();
            }
            catch (Exception ex)
            {
                WorkFlow wf = new WorkFlow(new Flow(flowNo), workId, this.FID);
                StartWork wk = wf.HisStartWork;
                if (wk.WFState == BP.WF.WFState.Complete)
                {
                    throw new Exception("@�Ѿ�������̣��������ڵ�ǰ������������Ҫ�õ������̵���ϸ����鿴��ʷ������������Ϣ:" + ex.Message);
                }
                else
                {
                    this.Copy(wk);
                    //string msg = "@�����ڲ����󣬸��������Ĳ��㣬���ʾ��Ǹ����Ѵ����֪ͨ��ϵͳ����Ա��error code:0001�������Ϣ:" + ex.Message;
                    string msg = "@�����ڲ����󣬸��������Ĳ��㣬���ʾ��Ǹ����Ѵ����֪ͨ��ϵͳ����Ա��error code:0001�������Ϣ:" + ex.Message;
                    Log.DefaultLogWriteLine(LogType.Error, "@������ɺ���ʹ�����׳����쳣��" + msg);
                    //throw new Exception(msg);
                }
            }
		}
        /// <summary>
        /// ִ���޸�
        /// </summary>
        public void DoRepair()
        { 
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
                Map map = new Map("WF_GenerEmpWorkDtls");
                map.EnDesc = this.ToE("UnOvFlow", "δ�������"); // "δ�������";
                map.EnType = EnType.View;

                map.AddTBIntPK(EmpWorkAttr.WorkID, 0, "WorkID", true, true);

               // map.AddTBInt(EmpWorkAttr.WFState, 0, "WFState", true, true);


                map.AddDDLEntities(EmpWorkAttr.FK_Flow, null, null, new Flows(), false);
                map.AddDDLEntities(EmpWorkAttr.FK_Emp, null, null, new Emps(), false);

                map.AddTBString(EmpWorkAttr.Title, null, null, true, false, 0, 500, 10);

                map.AddTBInt(EmpWorkAttr.WFState, 0, "WFState", true, true);

                map.AddTBDateTime(EmpWorkAttr.RDT, null, true, true);
                map.AddTBDate(EmpWorkAttr.SDT, null, null, true, true);

                map.AddDDLEntities(EmpWorkAttr.FK_Node, 0, DataType.AppInt, null, new Nodes(), NodeAttr.NodeID, NodeAttr.Name, false);

                ////  map.AddTBString(EmpWorkAttr.FK_Station, null,null, true, false, 0, 500, 10);
                ////  map.AddDDLEntities(EmpWorkAttr.FK_Dept, null, null, new Port.Depts(), false);
                ////  map.AddDtl(new WorkerLists(), EmpWorkAttr.WorkID); //���Ĺ�����

                ////map.AddSearchAttr(EmpWorkAttr.FK_Dept);
                ////map.AddSearchAttr(EmpWorkAttr.FK_Flow);
                ////map.AddSearchAttr(EmpWorkAttr.Rec);

                ////map.AttrsOfSearch.AddFromTo("��¼��",EmpWorkAttr.RDT,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);

                //RefMethod rm = new RefMethod();
                //rm.Title = this.ToE("WorkRpt", "��������"); // "��������";
                //rm.ClassMethodName = this.ToString() + ".DoRpt";
                //rm.Icon = "../Images/Btn/Word.gif";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowSelfTest", "�����Լ�"); // "�����Լ�";
                //rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowRepare", "�����Լ첢�޸�");  //"�����Լ첢�޸�";
                //rm.ClassMethodName = this.ToString() + ".DoRepare";
                //rm.Warning = "��ȷ��Ҫִ�д˹����� \t\n 1)����Ƕ����̣�����ͣ���ڵ�һ���ڵ��ϣ�ϵͳΪִ��ɾ������\t\n 2)����Ƿǵص�һ���ڵ㣬ϵͳ�᷵�ص��ϴη����λ�á�";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region ���ػ��෽��
		/// <summary>
		/// ɾ����,��Ҫ�ѹ������б�ҲҪɾ��.
		/// </summary>
        protected override void afterDelete()
        {
            // . clear bad worker .  
            DBAccess.RunSQLReturnTable("delete WF_GenerWorkerList where WorkID in  ( select WorkID from WF_GenerWorkerList where WorkID not in (select WorkID from WF_EmpWork) )");

            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID);
            wf.DoDeleteWorkFlowByReal(); /* ɾ������Ĺ�����*/

            base.afterDelete();
        }
		#endregion 

		#region ִ�����
        public string DoRpt()
        {
            PubClass.WinOpen("WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Flow"+this.FK_Flow);
            return null;
        }
		/// <summary>
		/// ִ���޸�
		/// </summary>
		/// <returns></returns>
        public string DoRepare()
        {
            if (this.DoSelfTestInfo() == "û�з����쳣��")
                return "û�з����쳣��";

            string sql = "SELECT FK_NODE FROM WF_GENERWORKERLIST WHERE WORKID='" + this.WorkID + "' ORDER BY FK_Node desc";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                /*����ǿ�ʼ�����ڵ㣬��ɾ������*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID);
                wf.DoDeleteWorkFlowByReal();
                return "����������Ϊ������ʧ�ܱ�ϵͳɾ����";
            }

            int fk_node = int.Parse(dt.Rows[0][0].ToString());

            Node nd = new Node(fk_node);
            if (nd.IsStartNode)
            {
                /*����ǿ�ʼ�����ڵ㣬��ɾ������*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID);
                wf.DoDeleteWorkFlowByReal();
                return "����������Ϊ������ʧ�ܱ�ϵͳɾ����";
            }

            this.FK_Node = fk_node;
            this.Update();

            string str = "";
            WorkerLists wls = new WorkerLists();
            wls.Retrieve(WorkerListAttr.FK_Node, fk_node, WorkerListAttr.WorkID, this.WorkID);
            foreach (WorkerList wl in wls)
            {
                str += wl.FK_Emp + wl.FK_EmpText + ",";
            }

            return "����������Ϊ[" + nd.Name + "]��������ʧ�ܱ��ع�����ǰλ�ã���ת��[" + str + "]�����޸��ɹ���";
        }
		public string DoSelfTestInfo()
		{
			WorkerLists wls = new WorkerLists(this.WorkID,this.FK_Flow );

			#region  �鿴һ�µ�ǰ�Ľڵ��Ƿ�ʼ�����ڵ㡣
			Node nd = new Node(this.FK_Node);
			if (nd.IsStartNode)
			{
				/* �ж��Ƿ����˻صĽڵ� */
				Work wk = nd.HisWork;
				wk.OID = this.WorkID;
				wk.Retrieve();

				if (wk.NodeState!=NodeState.Back)
				{
					return "��ǰ�Ĺ����ڵ� �����ڿ�ʼ�����ڵ��� �������˻����� ��Ӧ�ó��ֵ����������ɾ����ǰ�Ĺ����ڵ㡣 ";
				}
			}
			#endregion


			#region  �鿴һ���Ƿ��е�ǰ�Ĺ����ڵ���Ϣ��
			bool isHave=false;
			foreach(WorkerList wl in wls)
			{
				if (wl.FK_Node==this.FK_Node)
					isHave=true;
			}

			if (isHave==false)
			{
				/*  */
				return "�Ѿ������ڵ�ǰ�Ĺ����ڵ���Ϣ����ɴ����̵�ԭ�������û�в����ϵͳ�쳣������ɾ�������̻��߽���ϵͳ�Զ��޸�����";
			}
			#endregion

			return "û�з����쳣��";
		}
		#endregion
	}
	/// <summary>
	/// ��Ա����s
	/// </summary>
	public class EmpWorks : Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new EmpWork();
			}
		}
		/// <summary>
		/// �����������̼���
		/// </summary>
		public EmpWorks(){}
		#endregion
	}
	
}

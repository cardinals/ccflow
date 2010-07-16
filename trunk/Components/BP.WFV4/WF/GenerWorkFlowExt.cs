
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.En;


namespace BP.WF
{
    /// <summary>
    /// �����Ĺ���
    /// </summary>
    public class GenerWorkFlowExtAttr
    {
        #region ��������
        /// <summary>
        /// ��˰���
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ������
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��ǰ��Ӧ�������
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// �÷�
        /// </summary>
        public const string Cent = "Cent";
        /// <summary>
        /// note
        /// </summary>
        public const string FlowNote = "FlowNote";
        /// <summary>
        /// ��ǰ�������Ľڵ�.
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ��ǰ������λ
        /// </summary>
        public const string FK_Station = "FK_Station";

        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
        #endregion
    }
    /// <summary>
    /// �����Ĺ���
    /// </summary>
    public class GenerWorkFlowExt : Entity
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
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(GenerWorkFlowExtAttr.FK_Flow);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.FK_Flow, value);
            }
        }
        public string FK_FlowText
        {
            get
            {
                //Flow fl = new Flow(this.FK_Flow);
                //return fl.Name;
                return this.GetValRefTextByKey(GenerWorkFlowExtAttr.FK_Flow);
            }
        }
        /// <summary>
        /// ��ǰ�Ĺ�����λ
        /// </summary>
        public string FK_Station_del
        {
            get
            {
                return this.GetValStringByKey(GenerWorkFlowExtAttr.FK_Station);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.FK_Station, value);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GenerWorkFlowExtAttr.FK_Dept);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(GenerWorkFlowExtAttr.Title);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.Title, value);
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(GenerWorkFlowExtAttr.RDT);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.RDT, value);
            }
        }
        /// <summary>
        /// /Ӧ���������
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(GenerWorkFlowExtAttr.SDT);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.SDT, value);
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowExtAttr.WorkID);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.WorkID, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(GenerWorkFlowExtAttr.Rec);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowExtAttr.Rec, value);
            }
        }
        public string RecText
        {
            get
            {
                return this.GetValRefTextByKey(GenerWorkFlowExtAttr.Rec);
            }
        }
        /// <summary>
        /// ��ǰ�������Ľڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowExtAttr.FK_Node);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.FK_Node, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowExtAttr.FID);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.FID, value);
            }
        }
        public string FK_NodeText
        {
            get
            {
                return this.GetValRefTextByKey(GenerWorkFlowExtAttr.FK_Node);
            }
        }
        /// <summary>
        /// ��������״̬( 0, δ���,1 ���, 2 ǿ����ֹ 3, ɾ��״̬,) 
        /// </summary>
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowExtAttr.WFState);
            }
            set
            {
                SetValByKey(GenerWorkFlowExtAttr.WFState, value);
            }
        }
        public string WFStateText
        {
            get
            {
                return this.GetValRefTextByKey(GenerWorkFlowExtAttr.WFState);
            }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public string WFStateLab
        {
            get
            {
                return this.GetValRefTextByKey(GenerWorkFlowExtAttr.WFState);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �����Ĺ�������
        /// </summary>
        public GenerWorkFlowExt()
        {
        }
        public GenerWorkFlowExt(int workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkFlowExtAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("����[" + workId + "]�����ڣ��������Ѿ���ɡ�");
        }
        /// <summary>
        /// �����Ĺ�������
        /// </summary>
        /// <param name="workId">��������ID</param>
        /// <param name="flowNo">���̱��</param>
        public GenerWorkFlowExt(Int64 workId, string flowNo)
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
                Map map = new Map("WF_GenerWorkFlow");
                map.EnDesc = this.ToE("UnOvFlow", "δ�������"); // "δ�������";

                map.AddTBIntPK(GenerWorkFlowExtAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(GenerWorkFlowExtAttr.FID, 0, "FID", true, true);

                // map.AddTBString(GenerWorkFlowExtAttr.FK_Flow, null, "����", true, false, 0, 500, 10);
                map.AddDDLEntities(GenerWorkFlowExtAttr.FK_Flow, null, null, new Flows(), false);

                map.AddTBString(GenerWorkFlowExtAttr.Title, null, null, true, false, 0, 500, 10);
                map.AddDDLSysEnum(GenerWorkFlowExtAttr.WFState, 0, null, true, false, "WFState");

                //map.AddTBString(GenerWorkFlowExtAttr.Rec, null, "������", true, false, 0, 500, 10);

                map.AddDDLEntities(GenerWorkFlowExtAttr.Rec, null, "������", new Emps(), false);
                map.AddTBDateTime(GenerWorkFlowExtAttr.RDT, null, true, true);
                map.AddTBDate(GenerWorkFlowExtAttr.SDT, null, null, true, true);

                // map.AddTBInt(GenerWorkFlowExtAttr.FK_Node, 0, "FK_Node", true, false);

                map.AddDDLEntities(GenerWorkFlowExtAttr.FK_Node, 0, DataType.AppInt, null, new Nodes(), NodeAttr.NodeID, NodeAttr.Name, false);
               // map.AddTBString(GenerWorkFlowExtAttr.FK_Station, null, null, true, false, 0, 500, 10);
                // map.AddDDLEntities(GenerWorkFlowExtAttr.FK_Station, null, "��ǰ��λ", new Stations(), false);

                map.AddDDLEntities(GenerWorkFlowExtAttr.FK_Dept, null, null, new Port.Depts(), false);

                map.AddDtl(new WorkerLists(), GenerWorkFlowExtAttr.WorkID); //���Ĺ�����

                map.AddSearchAttr(GenerWorkFlowExtAttr.FK_Dept);
                map.AddSearchAttr(GenerWorkFlowExtAttr.FK_Flow);
                map.AddSearchAttr(GenerWorkFlowExtAttr.Rec);

                //map.AttrsOfSearch.AddFromTo("��¼��",GenerWorkFlowExtAttr.RDT,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "��������"); // "��������";
                rm.ClassMethodName = this.ToString() + ".DoRpt";
                rm.Icon = "../Images/Btn/Word.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowSelfTest", "�����Լ�"); // "�����Լ�";
                rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowRepare", "�����Լ첢�޸�");  //"�����Լ첢�޸�";
                rm.ClassMethodName = this.ToString() + ".DoRepare";
                rm.Warning = "��ȷ��Ҫִ�д˹����� \t\n 1)����Ƕ����̣�����ͣ���ڵ�һ���ڵ��ϣ�ϵͳΪִ��ɾ������\t\n 2)����Ƿǵص�һ���ڵ㣬ϵͳ�᷵�ص��ϴη����λ�á�";
                map.AddRefMethod(rm);

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
            DBAccess.RunSQLReturnTable("DELETE WF_GenerWorkerList WHERE WorkID IN ( select WorkID from WF_GenerWorkerList where WorkID not in (select WorkID from WF_GenerWorkFlowExt) )");

            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
            wf.DoDeleteWorkFlowByReal(); /* ɾ������Ĺ�����*/
            base.afterDelete();
        }
        #endregion

        #region ִ�����
        public string DoRpt()
        {
            PubClass.WinOpen("WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Flow" + this.FK_Flow);
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
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
                wf.DoDeleteWorkFlowByReal();
                return "����������Ϊ������ʧ�ܱ�ϵͳɾ����";
            }

            int fk_node = int.Parse(dt.Rows[0][0].ToString());

            Node nd = new Node(fk_node);
            if (nd.IsStartNode)
            {
                /*����ǿ�ʼ�����ڵ㣬��ɾ������*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
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
            WorkerLists wls = new WorkerLists(this.WorkID, this.FK_Flow);

            #region  �鿴һ�µ�ǰ�Ľڵ��Ƿ�ʼ�����ڵ㡣
            Node nd = new Node(this.FK_Node);
            if (nd.IsStartNode)
            {
                /* �ж��Ƿ����˻صĽڵ� */
                Work wk = nd.HisWork;
                wk.OID = this.WorkID;
                wk.Retrieve();

                if (wk.NodeState != NodeState.Back)
                {
                    return "��ǰ�Ĺ����ڵ� �����ڿ�ʼ�����ڵ��� �������˻����� ��Ӧ�ó��ֵ����������ɾ����ǰ�Ĺ����ڵ㡣 ";
                }
            }
            #endregion


            #region  �鿴һ���Ƿ��е�ǰ�Ĺ����ڵ���Ϣ��
            bool isHave = false;
            foreach (WorkerList wl in wls)
            {
                if (wl.FK_Node == this.FK_Node)
                    isHave = true;
            }

            if (isHave == false)
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
    /// �����Ĺ���s
    /// </summary>
    public class GenerWorkFlowExts : Entities
    {
        /// <summary>
        /// ���ݹ�������,������ԱID ��ѯ��������ǰ�������Ĺ���.
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <param name="empId">������ԱID</param>
        /// <returns></returns>
        public static DataTable QuByFlowAndEmp(string flowNo, int empId)
        {
            string sql = "SELECT a.WorkID FROM WF_GenerWorkFlowExt a, WF_GenerWorkerList b WHERE a.WorkID=b.WorkID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='" + empId.ToString() + "' AND a.FK_Flow='" + flowNo + "'";
            return DBAccess.RunSQLReturnTable(sql);
        }

        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GenerWorkFlowExt();
            }
        }
        /// <summary>
        /// �����������̼���
        /// </summary>
        public GenerWorkFlowExts() { }
        #endregion
    }

}

using System;
using System.Data;
using BP.DA;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// ������������
    /// </summary>
    public enum FJOpen
    {
        /// <summary>
        /// ������
        /// </summary>
        None,
        /// <summary>
        /// �Բ���Ա����
        /// </summary>
        ForEmp,
        /// <summary>
        /// �Թ���ID����
        /// </summary>
        ForWorkID,
        /// <summary>
        /// ������ID����
        /// </summary>
        ForFID
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum FLRole
    {
        /// <summary>
        /// ���ս�����
        /// </summary>
        ByEmp,
        /// <summary>
        /// ���ղ���
        /// </summary>
        ByDept,
        /// <summary>
        /// ���ո�λ
        /// </summary>
        ByStation
    }
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public enum RunModel
    {
        /// <summary>
        /// ��ͨ
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// ����
        /// </summary>
        HL = 1,
        /// <summary>
        /// ����
        /// </summary>
        FL = 2,
        /// <summary>
        /// �ֺ���
        /// </summary>
        FHL
    }
    /// <summary>
    /// �ڵ�ǩ������
    /// </summary>
    public enum SignType
    {
        /// <summary>
        /// ��ǩ
        /// </summary>
        OneSign,
        /// <summary>
        /// ��ǩ
        /// </summary>
        Countersign
    }
    /// <summary>
    /// �ڵ�״̬
    /// </summary>
    public enum NodeState
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        Init,
        /// <summary>
        /// �Ѿ����
        /// </summary>
        Complete,
        /// <summary>
        /// �۷�״̬
        /// </summary>
        CutCent,
        /// <summary>
        /// ǿ����ֹ
        /// </summary>
        Stop,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete,
        /// <summary>
        /// �˻�
        /// </summary>
        Back
    }
    /// <summary>
    /// �ڵ㹤������
    /// �ڵ㹤������( 0, ��˽ڵ�, 1 ��Ϣ�ɼ��ڵ�,  2, ��ʼ�ڵ�)
    /// </summary>
    public enum NodeWorkType
    {
        /// <summary>
        /// ��ʼ�ڵ�
        /// </summary>
        StartWork = 0,
        /// <summary>
        /// ��ʼ�ڵ����
        /// </summary>
        StartWorkFL = 1,
        /// <summary>
        /// ��׼��˽ڵ�
        /// </summary>
        GECheckStands = 2,
        /// <summary>
        /// ������˽ڵ�
        /// </summary>
        NumChecks = 3,
        /// <summary>
        /// ��ǩ(����˵Ĺ���)
        /// </summary>
        GECheckMuls = 4,
        /// <summary>
        /// �����ڵ�
        /// </summary>
        WorkHL = 5,
        /// <summary>
        /// �����ڵ�
        /// </summary>
        WorkFL = 6,
        /// <summary>
        /// �ֺ���
        /// </summary>
        WorkFHL = 7,
        /// <summary>
        /// ��ͨ����
        /// </summary>
        Work = 8
    }
    /// <summary>
    /// ���̽ڵ�����
    /// </summary>
    public enum FNType
    {
        /// <summary>
        /// ƽ��ڵ�
        /// </summary>
        Plane = 0,
        /// <summary>
        /// �ֺ���
        /// </summary>
        River = 1,
        /// <summary>
        /// ֧��
        /// </summary>
        Branch = 2
    }
    /// <summary>
    /// 
    /// </summary>
    public enum NodePosType
    {
        Start,
        Mid,
        End
    }
    /// <summary>
    /// �ڵ����ݲɼ�����
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// system form.
        /// </summary>
        SysForm,
        /// <summary>
        /// self form.
        /// </summary>
        SelfForm
    }
    /// <summary>
    /// �ڵ�����
    /// </summary>
    public class NodeAttr
    {
        #region ������
        public const string IsCCNode = "IsCCNode";
        public const string IsCCFlow = "IsCCFlow";
        public const string HisStas = "HisStas";
        public const string HisToNDs = "HisToNDs";
        public const string HisBookIDs = "HisBookIDs";
        public const string NodePosType = "NodePosType";
        public const string HisDeptStrs = "HisDeptStrs";
        public const string HisEmps = "HisEmps";
        public const string GroupStaNDs = "GroupStaNDs";
        public const string FJOpen = "FJOpen";
        public const string IsCanReturn = "IsCanReturn";
        public const string IsCanCC = "IsCanCC";
        public const string FormType = "FormType";
        public const string FormUrl = "FormUrl";

        /// <summary>
        /// ����֮ǰ����Ϣ��ʾ
        /// </summary>
        public const string BeforeSendAlert = "BeforeSendAlert";
        #endregion

        #region ��������
        /// <summary>
        /// OID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// �ڵ������
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ������
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// �Ƿ���乤��
        /// </summary>
        public const string IsTask = "IsTask";
        /// <summary>
        /// �ڵ㹤������
        /// </summary>
        public const string NodeWorkType = "NodeWorkType";
        /// <summary>
        /// �ڵ������
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// x
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// WarningDays(��������)
        /// </summary>
        public const string WarningDays_del = "WarningDays";
        /// <summary>
        /// DeductDays(�۷�����)
        /// </summary>
        public const string DeductDays = "DeductDays";
        /// <summary>
        /// ������
        /// </summary>
        public const string WarningDays = "WarningDays";
        /// <summary>
        /// �۷�
        /// </summary>
        public const string DeductCent = "DeductCent";
        /// <summary>
        /// ��߿۷�
        /// </summary>
        public const string MaxDeductCent = "MaxDeductCent";
        /// <summary>
        /// ����ӷ�
        /// </summary>
        public const string SwinkCent = "SwinkCent";
        /// <summary>
        /// ��������ӷ�
        /// </summary>
        public const string MaxSwinkCent = "MaxSwinkCent";
        /// <summary>
        /// ���̲���
        /// </summary>
        public const string Step = "Step";
        /// <summary>
        /// ��������
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        ///  �������
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// ǩ������
        /// </summary>
        public const string SignType = "SignType";
        /// <summary>
        /// �Ƿ����ѡ�������Ա
        /// </summary>
        public const string IsSelectEmp = "IsSelectEmp";
        public const string DoWhat = "DoWhat";
        /// <summary>
        /// ��ʾ�ı�
        /// </summary>
        public const string ShowSheets = "ShowSheets";
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public const string RunModel = "RunModel";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FLRole = "FLRole";
        /// <summary>
        /// �Ƿ��Ǹ���
        /// </summary>
        public const string FNType = "FNType";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ڵ����Ϣ.	 
    /// </summary>
    public class Node : Entity, IDTS
    {
        #region ���Ի�ȫ�ֵ� Nod
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// ���Ի�ȫ�ֵ�
        /// </summary>
        /// <returns></returns>
        public NodePosType GetHisNodePosType()
        {
            string nodeid = this.NodeID.ToString();
            if (nodeid.Substring(nodeid.Length - 2) == "01")
                return NodePosType.Start;

            if (this.HisFromNodes.Count == 0)
                return NodePosType.Mid;

            if (this.HisToNodes.Count == 0)
                return NodePosType.End;

            return NodePosType.Mid;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="fl"></param>
        /// <returns></returns>
        public static string CheckFlow(Flow fl)
        {
            Nodes nds = new Nodes();
            nds.Retrieve(NodeAttr.FK_Flow, fl.No);

            if (nds.Count == 0)
                return "����[" + fl.No + fl.Name + "]��û�нڵ����ݣ�����Ҫע��һ��������̡�";

 


            // �����Ƿ�������������Ľڵ㡣
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_NodeCompleteCondition)");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_FlowCompleteCondition)");

            DA.DBAccess.RunSQL("DELETE WF_Direction WHERE WF_Direction.Node=0 or WF_Direction.ToNode=0");
            DA.DBAccess.RunSQL("DELETE WF_Direction WHERE Node  NOT IN (SELECT NODEID FROM WF_NODE )");
            DA.DBAccess.RunSQL("DELETE WF_Direction WHERE ToNode  NOT IN (SELECT NODEID FROM WF_NODE) ");

            //�����������
            DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSort=(SELECT FK_FlowSort FROM WF_Flow WHERE NO=WF_NODE.FK_FLOW)");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSortT=(SELECT Name FROM WF_FlowSort WHERE NO=WF_NODE.FK_FlowSort)");


            // ������Ϣ����λ���ڵ���Ϣ��
            foreach (Node nd in nds)
            {
                BP.Sys.MapData md = new BP.Sys.MapData();
                md.No = "ND" + nd.NodeID;
                if (md.IsExits == false)
                {
                    nd.CreateMap();
                }


                // ������λ��
                NodeStations stas = new NodeStations(nd.NodeID);
                string strs = "";
                foreach (NodeStation sta in stas)
                    strs += "@" + sta.FK_Station;

                nd.HisStas = strs;

                // �������š�
                NodeDepts ndpts = new NodeDepts(nd.NodeID);
                strs = "";
                foreach (NodeDept ndp in ndpts)
                    strs += "@" + ndp.FK_Dept;

                nd.HisDeptStrs = strs;

                // ��ִ����Ա��
                NodeEmps ndemps = new NodeEmps(nd.NodeID);
                strs = "";
                foreach (NodeEmp ndp in ndemps)
                    strs += "@" + ndp.FK_Emp;
                nd.HisEmps = strs;

                // �ڵ�
                strs = "";
                Directions dirs = new Directions(nd.NodeID);
                foreach (Direction dir in dirs)
                    strs += "@" + dir.ToNode;
                nd.HisToNDs = strs;

                // ����
                strs = "";
                BookTemplates temps = new BookTemplates(nd);
                foreach (BookTemplate temp in temps)
                    strs += "@" + temp.No;
                nd.HisBookIDs = strs;

                // ���ڵ��λ�����ԡ�
                nd.HisNodePosType = nd.GetHisNodePosType();
                nd.DirectUpdate();
            }

            // ��������Ա����
            string sql = "select FK_Station from WF_FlowStation where fk_flow='" + fl.No + "'";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            string mystas = "";
            foreach (DataRow dr in dt.Rows)
            {
                mystas += dr[0].ToString() + ",";
            }
            fl.CCStas = mystas;


            // �����λ����.
            sql = "SELECT HisStas, COUNT(*) NUM FROM WF_Node WHERE FK_Flow='" + fl.No + "' GROUP BY HisStas";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string stas = dr[0].ToString();
                string nodes = "";
                foreach (Node nd in nds)
                {
                    if (nd.HisStas == stas)
                        nodes += "@" + nd.NodeID;
                }

                foreach (Node nd in nds)
                {
                    if (nodes.Contains("@" + nd.NodeID.ToString()) == false)
                        continue;

                    nd.GroupStaNDs = nodes;
                    nd.DirectUpdate();
                }
            }


            /* �ж����̵����� */

            sql = "SELECT Name FROM WF_Node WHERE (NodeWorkType=" + (int)NodeWorkType.StartWorkFL + " OR NodeWorkType=" + (int)NodeWorkType.WorkFHL + " OR NodeWorkType=" + (int)NodeWorkType.WorkFL + " OR NodeWorkType=" + (int)NodeWorkType.WorkHL + ") AND (FK_Flow='" + fl.No + "')";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                fl.HisFlowType = FlowType.Panel;
            else
                fl.HisFlowType = FlowType.FHL;

            //fl.EnsName = fl.HisStartNode.EnsName;
            //fl.EnName = fl.HisStartNode.EnName;
            //fl.StartNodeID = fl.HisStartNode.NodeID;
            fl.DirectUpdate();
            return null;
        }
        protected override bool beforeUpdate()
        {
            Flow fl = new Flow(this.FK_Flow);
            Node.CheckFlow(fl);

            DBAccess.RunSQL("UPDATE Sys_MapData SET Name='"+ this.Name+"' WHERE No='ND"+this.NodeID+"'");

            if (this.IsCheckNode)
            {
                if (this.HisSignType == SignType.OneSign)
                    this.HisNodeWorkType = NodeWorkType.GECheckStands;
                else
                    this.HisNodeWorkType = NodeWorkType.GECheckMuls;
            }
            else
            {
                switch (this.HisRunModel)
                {
                    case RunModel.Ordinary:
                        if (this.IsStartNode)
                            this.HisNodeWorkType = NodeWorkType.StartWork;
                        else
                            this.HisNodeWorkType = NodeWorkType.Work;
                        break;
                    case RunModel.FL:
                        if (this.IsStartNode)
                            this.HisNodeWorkType = NodeWorkType.StartWorkFL;
                        else
                            this.HisNodeWorkType = NodeWorkType.WorkFL;
                        break;
                    case RunModel.HL:
                        if (this.IsStartNode)
                            throw new Exception("@���������ÿ�ʼ�ڵ�Ϊ�����ڵ㡣");
                        else
                            this.HisNodeWorkType = NodeWorkType.WorkHL;
                        break;
                }
            }

            #region  �ж����̽ڵ����͡�
            while (true)
            {
                if (this.IsFLHL)
                {
                    /*����Ƿֺ�������ô��һ���Ǹ�����*/
                    this.HisFNType = FNType.River;
                    break;
                }

                if (fl.HisFlowType == FlowType.Panel)
                {
                    /*˵����û�з����̺���֮˵��ֻ��ƽ��ڵ㡣*/
                    this.HisFNType = FNType.Plane;
                    break;
                }



                if (this.HisNodeWorkType == NodeWorkType.StartWork)
                {
                    /* ����ǿ�ʼ�ڵ㣬�ж������ڵ����Ƿ��з��������ڵ㣬�����˵������һ�� */
                    this.HisFNType = FNType.Branch;
                    break;
                }



                // �鿴�ܹ�ת�����Ľڵ㡣
                Nodes fNDs = this.HisFromNodes;
                if (fNDs.Count == 0)
                {
                    /*˵������һ�������ڵ㣬�Ͳ��������������*/
                    break;
                }

                bool isOk = false;
                foreach (Node nd in fNDs)
                {
                    if (nd.IsFL)
                    {
                        /*������һ���ڵ��Ƿ���*/
                        this.HisFNType = FNType.Branch;
                        isOk = true;
                        break;
                    }

                    if (nd.HisNodeWorkType == NodeWorkType.WorkHL)
                    {
                        /*������һ���ڵ��Ǻ���*/
                        this.HisFNType = FNType.River;
                        isOk = true;
                        break;
                    }

                    if (nd.HisFNType == FNType.Plane)
                        break;

                    this.HisFNType = nd.HisFNType;
                    isOk = false;
                    break;
                }

                ////����ж��ˣ��ͷų�������
                //if (isOk)
                //    break;
                //else
                //    throw new Exception("@û���жϵ����Ƿ������Ǻ����ڵ㡣");
                break;
            }
            #endregion  �ж����̽ڵ����͡�


            //Nodes nds = fl.HisNodes;
            //foreach (Node nd in nds)
            //{

            //}

            return base.beforeUpdate();
        }
        /// <summary>
        /// ע������
        /// </summary>
        /// <param name="fl"></param>
        /// <returns></returns>
        public static string RegFlow(Flow fl)
        {

            #region ȡ�������̽ڵ�
            string msg = "";
            // ȡ�������̽ڵ�.
            Nodes alNodes = new Nodes();
            ArrayList alWorks = BP.DA.ClassFactory.GetObjects("BP.WF.Works");
            #endregion


            #region ���������ж������ı�ǡ�
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_NodeCompleteCondition)");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_FlowCompleteCondition)");
            #endregion


            #region ɾ������ڵ�
            DBAccess.RunSQL("DELETE WF_FLOWCOMPLETECONDITION where nodeid not in ( select nodeid from wf_node)");
            //   DBAccess.RunSQL("DELETE WF_NODECOMPLETECONDITION where enclassName not in (select EnsName from wf_node)");
            //  DBAccess.RunSQL("DELETE WF_FLOWCOMPLETECONDITION where enclassName not in (select EnsName from wf_node)");
            DBAccess.RunSQL("DELETE WF_GenerWorkerList where fk_node not in( select NodeId from wf_node)");


            //���±�׼��˽ڵ��������˽ڵ�ı�����WF_Node��ȥ
            // DBAccess.RunSQL("update wf_node set PTable='WF_GECheckStand' WHERE ensname='BP.WF.GECheckStands'");
            // DBAccess.RunSQL("update wf_node set PTable='WF_NumCheck' WHERE ensname='BP.WF.NumChecks'");
            #endregion


            #region ���Ƚڵ�Ļ�����Ϣ
            // ��鿪ʼ�ڵ��������
            //string sql = "SELECT COUNT(*) FROM (SELECT NODEID, FK_FLOW, COUNT(FK_FLOW) AS NUM  FROM WF_NODE  WHERE WF_NODE.IsStartNode=1 GROUP BY NODEID ,FK_FLOW) WHERE NUM >1";
            //if (DBAccess.RunSQLReturnValInt(sql) > 0)
            //    msg += "���̵Ŀ�ʼ�ڵ㲻Ψһ��" + sql;

            //sql = "UPDATE WF_FLOW SET (StartNODEID ,EnsName,EnName )=( SELECT NODEID, EnsName,EnName  FROM WF_NODE WHERE WF_NODE.IsStartNode=1 AND WF_NODE.FK_FLOW=WF_FLOW.NO )";
            //DBAccess.RunSQL(sql);
            #endregion

            return msg;
        }
        #endregion

        #region ����
        private Conds _HisNodeCompleteConditions = null;
        /// <summary>
        /// �ڵ�������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,���������Ա������ڵ��ϵ�����������.
        /// </summary>
        public Conds HisNodeCompleteConditions
        {
            get
            {
                if (this._HisNodeCompleteConditions == null)
                {
                    _HisNodeCompleteConditions = new Conds(CondType.Node, this.NodeID, this.HisWork.OID);
                }
                return _HisNodeCompleteConditions;
            }
        }
        private Conds _HisFlowCompleteConditions = null;
        /// <summary>
        /// ����������������,�˽ڵ�������������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,�������������.
        /// </summary>
        public Conds HisFlowCompleteConditions
        {
            get
            {
                if (this._HisFlowCompleteConditions == null)
                    _HisFlowCompleteConditions = new Conds(CondType.Flow, this.NodeID, this.HisWork.OID);
                return _HisFlowCompleteConditions;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// �ڲ����
        /// </summary>
        public string No
        {
            get
            {
                return this.NodeID.ToString().Substring(this.NodeID.ToString().Length - 2);
            }
        }
        /// <summary>
        /// ���̽ڵ�����
        /// </summary>
        public FNType HisFNType
        {
            get
            {
                return (FNType)this.GetValIntByKey(NodeAttr.FNType);
            }
            set
            {
                this.SetValByKey(NodeAttr.FNType, (int)value);
            }
        }

        public FormType HisFormType
        {
            get
            {
                return (FormType)this.GetValIntByKey(NodeAttr.FormType);
            }
            set
            {
                this.SetValByKey(NodeAttr.FormType, (int)value);
            }
        }
        
        /// <summary>
        /// OID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
         /// <summary>
        /// FormUrl 
        /// </summary>
        public string FormUrl
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FormUrl);
            }
            set
            {
                this.SetValByKey(NodeAttr.FormUrl, value);
            }
        }
        
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityOIDNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNameAttr.Name, value);
            }
        }
        /// <summary>
        /// ��Ҫ���������ڣ�
        /// </summary>
        public int DeductDays
        {
            get
            {
                int i = this.GetValIntByKey(NodeAttr.DeductDays);
                if (i == 0)
                    return 1;
                return i;
            }
            set
            {
                this.SetValByKey(NodeAttr.DeductDays, value);
            }
        }
        /// <summary>
        /// ��߿۷�
        /// </summary>
        public float MaxDeductCent
        {
            get
            {
                return this.GetValFloatByKey(NodeAttr.MaxDeductCent);
            }
            set
            {
                this.SetValByKey(NodeAttr.MaxDeductCent, value);
            }
        }
        /// <summary>
        /// �������ӷ�
        /// </summary>
        public float SwinkCent
        {
            get
            {
                return this.GetValFloatByKey(NodeAttr.SwinkCent);
            }
            set
            {
                this.SetValByKey(NodeAttr.SwinkCent, value);
            }
        }
        /// <summary>
        /// ���̲���
        /// </summary>
        public int Step
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Step);
            }
            set
            {
                this.SetValByKey(NodeAttr.Step, value);
            }
        }
       
        /// <summary>
        /// ��������( ��Ҫ���������ڣ�+��������)
        /// </summary>
        public int NeedCompleteDays
        {
            get
            {
                return this.DeductDays;
            }
        }
        /// <summary>
        /// �۷��ʣ���/�죩
        /// </summary>
        public float DeductCent
        {
            get
            {
                return this.GetValFloatByKey(NodeAttr.DeductCent);
            }
            set
            {
                this.SetValByKey(NodeAttr.DeductCent, value);
            }
        }
        /// <summary>
        /// �Ƿ��ǿ�ʼ�ڵ�
        /// </summary>
        public bool IsStartNode
        {
            get
            {
                if (this.HisNodePosType == NodePosType.Start)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// x
        /// </summary>
        public int X
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.X);
            }
            set
            {
                this.SetValByKey(NodeAttr.X, value);
            }
        }
        public int WarningDays
        {
            get
            {
                if (this.GetValIntByKey(NodeAttr.WarningDays) == 0)
                    return this.DeductDays;
                else
                    return this.DeductDays - this.GetValIntByKey(NodeAttr.WarningDays);
            }
        }
        /// <summary>
        /// y
        /// </summary>
        public int Y
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Y);
            }
            set
            {
                this.SetValByKey(NodeAttr.Y, value);
            }
        }
        /// <summary>
        /// λ��
        /// </summary>
        public NodePosType NodePosType
        {
            get
            {
                return (NodePosType)this.GetValIntByKey(NodeAttr.NodePosType);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodePosType, (int)value);
            }
        }
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public RunModel HisRunModel
        {
            get
            {
                return (RunModel)this.GetValIntByKey(NodeAttr.RunModel);
            }
        }
        /// <summary>
        /// �ڵ��������
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FK_Flow);
            }
            set
            {
                SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        public string DoWhat
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.DoWhat);
            }
            set
            {
                SetValByKey(NodeAttr.DoWhat, value);
            }
        }
        public string FlowName
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FlowName);
            }
            set
            {
                SetValByKey(NodeAttr.FlowName, value);
            }
        }
        //public string EnsName
        //{
        //    get
        //    {
        //        return this.GetValStringByKey(NodeAttr.EnsName);
        //    }
        //    set
        //    {
        //        SetValByKey(NodeAttr.EnsName, value);
        //    }
        //}
        //public string EnName
        //{
        //    get
        //    {
        //        string ms = this.GetValStringByKey(NodeAttr.EnName);
        //        if (ms == null || ms.Trim() == "")
        //        {
        //            ms = this.HisWorks.GetNewEntity.ToString();
        //            this.SetValByKey(NodeAttr.EnName, ms);
        //            this.Update(NodeAttr.EnName, ms);
        //        }
        //        return ms;
        //    }
        //    set
        //    {
        //        SetValByKey(NodeAttr.EnName, value);
        //    }
        //}
        public string PTable
        {
            get
            {
                if (this.IsCheckNode)
                    return "WF_GECheckStand";
                return "ND" + this.NodeID;
            }
            set
            {
                SetValByKey(NodeAttr.PTable, value);
            }
        }
        /// <summary>
        /// Ҫ��ʾ�ں���ı�
        /// </summary>
        public string ShowSheets
        {
            get
            {
                string s = this.GetValStringByKey(NodeAttr.ShowSheets);
                if (s == "")
                    return "@";
                return s;
            }
            set
            {
                SetValByKey(NodeAttr.ShowSheets, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary> 
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.Doc);
            }
            set
            {
                SetValByKey(NodeAttr.Doc, value);
            }
        }
        public string GroupStaNDs
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.GroupStaNDs);
            }
            set
            {
                this.SetValByKey(NodeAttr.GroupStaNDs, value);
            }
        }

        public string HisToNDs
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.HisToNDs);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisToNDs, value);
            }
        }
        /// <summary>
        /// ǩ������
        /// </summary>
        public SignType HisSignType
        {
            get
            {
                return (SignType)this.GetValIntByKey(NodeAttr.SignType);
            }
            set
            {
                this.SetValByKey(NodeAttr.SignType, (int)value);
            }
        }
        public string HisDeptStrs
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.HisDeptStrs);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisDeptStrs, value);
            }
        }
        public string HisStas
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.HisStas);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisStas, value);
            }
        }
        public string HisEmps
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.HisEmps);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisEmps, value);
            }
        }
        public string HisBookIDs
        {
            get
            {
                string str = this.GetValStringByKey(NodeAttr.HisBookIDs);
                if (this.IsStartNode)
                    if (str.Contains("@SLHZ") == false)
                        str += "@SLHZ";
                return str;
            }
            set
            {
                this.SetValByKey(NodeAttr.HisBookIDs, value);
            }
        }
        #endregion

        #region ��չ����
        private BookTemplates _BookTemplates = null;
        /// <summary>
        /// HisNodeRefFuncs
        /// </summary>
        public BookTemplates HisBookTemplates
        {
            get
            {
                if (_BookTemplates == null)
                {
                    _BookTemplates = new BookTemplates();
                    _BookTemplates.Retrieve(BookTemplateAttr.NodeID, this.NodeID);
                    //  _BookTemplates.AddEntities(this.HisBookIDs);
                }
                return _BookTemplates;
            }
        }
        private Flow _Flow = null;
        /// <summary>
        /// �˽ڵ����ڵ�����.
        /// </summary>
        public Flow HisFlow
        {
            get
            {
                if (this._Flow == null)
                    _Flow = new Flow(this.FK_Flow);
                return _Flow;
            }
        }
        /// <summary>
        /// �ǲ��Ƕ��λ�����ڵ�.
        /// </summary>
        public bool IsMultiStations
        {
            get
            {
                if (this.HisStations.Count > 1)
                    return true;
                return false;
            }
        }
        private Stations _HisStations = null;
        /// <summary>
        /// �˽ڵ����ڵĹ�����λ
        /// </summary>
        public Stations HisStations
        {
            get
            {
                if (this._HisStations == null)
                {
                    _HisStations = new Stations();
                    _HisStations.AddEntities(this.HisStas);
                }
                return _HisStations;
            }
        }
        private Depts _HisDepts = null;
        /// <summary>
        /// �˽ڵ����ڵĹ�����λ
        /// </summary>
        public Depts HisDepts
        {
            get
            {
                if (this._HisDepts == null)
                {
                    _HisDepts = new Depts();
                    _HisDepts.AddEntities(this.HisDeptStrs);
                }
                return _HisDepts;
            }
        }
        /// <summary>
        /// HisStationsStr
        /// </summary>
        public string HisStationsStr
        {
            get
            {
                string str = "";
                foreach (Station st in this.HisStations)
                {
                    str += st.Name + "��";
                }
                return str;
            }
        }

        #region ת���Ĺ���
        private Work _Work = null;
        /// <summary>
        /// �õ�����һ������ʵ��
        /// </summary>
        public Work HisWork
        {
            get
            {
                if (_Work == null)
                {
                    if (this.IsCheckNode)
                    {
                        _Work = new BP.WF.GECheckStand(this.NodeID);
                        _Work.SetValByKey(GECheckStandAttr.NodeID, this.NodeID);

                        //  _Work.SetValByKey("MyPK", nd.NodeID + "_" + workId);

                        return _Work;
                    }

                    if (this.IsStartNode)
                    {
                        _Work = new BP.WF.GEStartWork(this.NodeID);
                        return _Work;
                    }

                    _Work = new BP.WF.GEWork(this.NodeID);
                    return _Work;
                }
                return _Work;
            }
        }
        private Works _HisWorks = null;
        /// <summary>
        /// ���Ĺ���s
        /// </summary>
        public Works HisWorks
        {
            get
            {
                if (_HisWorks == null)
                    _HisWorks = (Works)this.HisWork.GetNewEntities;

                return _HisWorks;
            }
        }
        //public GEStartWork HisStartWork
        //{
        //    get
        //    {
        //        return new GEStartWork(this.NodeID);
        //    }
        //}
        #endregion
        /// <summary>
        /// ���Ĺ�������
        /// </summary>
        public string HisWorksDesc
        {
            get
            {
                return this.Name;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// �õ�һ������dataʵ��
        /// </summary>
        /// <param name="workId">����ID</param>
        /// <returns>���û�оͷ���null</returns>
        public Work GetWork(Int64 workId)
        {
            Work wk = this.HisWork;
            wk.SetValByKey("OID", workId);
            if (this.IsCheckNode)
            {
                wk.SetValByKey("NodeID", this.NodeID);
                wk.SetValByKey("MyPK", this.NodeID + "_" + workId);
            }

            if (wk.RetrieveFromDBSources() == 0)
                return null;
            else
                return wk;
            return wk;
        }
        #endregion

        #region �ڵ�Ĺ�������
        /// <summary>
        /// ������������
        /// </summary>
        public FJOpen HisFJOpen
        {
            get
            {
                return (FJOpen)this.GetValIntByKey(NodeAttr.FJOpen);
            }
            set
            {
                this.SetValByKey(NodeAttr.FJOpen, (int)value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FLRole HisFLRole
        {
            get
            {
                return (FLRole)this.GetValIntByKey(NodeAttr.FLRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.FLRole, (int)value);
            }
        }
        /// <summary>
        /// Int type
        /// </summary>
        public NodeWorkType HisNodeWorkType
        {
            get
            {
                return (NodeWorkType)this.GetValIntByKey(NodeAttr.NodeWorkType);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeWorkType, (int)value);
            }
        }
        #endregion

        #region �������� (���ڽڵ�λ�õ��ж�)
        /// <summary>
        /// ����
        /// </summary>
        public NodePosType HisNodePosType
        {
            get
            {
                if (SystemConfig.IsDebug)
                {
                    this.SetValByKey(NodeAttr.NodePosType, (int)this.GetHisNodePosType());
                    return this.GetHisNodePosType();
                }


                //if (this.HisNodeWorkType == NodeWorkType.StartWork || this.HisNodeWorkType == NodeWorkType.)
                //    return NodePosType.Start;

                return (NodePosType)this.GetValIntByKey(NodeAttr.NodePosType);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodePosType, (int)value);
            }

        }
        /// <summary>
        /// �ǲ��ǽ����ڵ�
        /// </summary>
        public bool IsEndNode
        {
            get
            {
                if (this.HisNodePosType == NodePosType.End)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �Ƿ���Գ���
        /// </summary>
        public bool IsCanCC
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanCC);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanCC, value);
            }
        }
        /// <summary>
        /// �ǲ����м�ڵ�
        /// </summary>
        public bool IsMiddleNode
        {
            get
            {
                if (this.HisNodePosType == NodePosType.Mid)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �ǲ�����˽ڵ�
        /// </summary>
        public bool IsCheckNode
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.GECheckStands:
                    case NodeWorkType.GECheckMuls:
                    case NodeWorkType.NumChecks:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��нڵ����������
        /// </summary>
        public bool IsCCNode
        {
            get
            {
                return false;
                // return this.GetValBooleanByKey(NodeAttr.IsCCNode);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCCNode, value);
            }
        }
        public bool IsSelectEmp
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsSelectEmp);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsSelectEmp, value);
            }
        }
        public bool IsSetDept
        {
            get
            {
                if (this.HisDeptStrs.Length > 3)
                    return true;
                else
                    return false;
            }
        }
        public bool IsHL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��Ƿ���
        /// </summary>
        public bool IsFL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsFLHL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��������������
        /// </summary>
        public bool IsCCFlow
        {
            get
            {
                return false;
                //return this.GetValBooleanByKey(NodeAttr.IsCCFlow);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCCFlow, value);
            }
        }
        /// <summary>
        /// �ǲ��Ǳ�׼��˽ڵ�
        /// </summary>
        public bool IsGECheckStandNode
        {
            get
            {
                if (this.HisNodeWorkType == NodeWorkType.GECheckStands)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �ǲ���PC�����ڵ�
        /// </summary>
        public bool IsPCNode
        {
            get
            {
                return false;
                //switch (this.HisNodeWorkType)
                //{
                //    case NodeWorkType.PCWork:
                //    case NodeWorkType.PCStartWork:
                //        return true;
                //    default:
                //        return false;
                //}
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string NodeWorkTypeText
        {
            get
            {
                return this.HisNodeWorkType.ToString();
                //return this.GetValRefTextByKey(NodeAttr.NodeWorkType);
            }
        }
        #endregion

        #region �ڵ�ķ��� (from nodes and to nodes , ע���жϽڵ���������ڵ�����)
        /// <summary>
        /// ���Ľ�Ҫ����ת��Ľڵ㷽�򼯺�.
        /// û���������ڵĸ���,ȫ���Ľڵ�..
        /// </summary>
        private Nodes _HisToNodes = null;
        /// <summary>
        /// ���Ľ�Ҫת��ķ��򼯺�
        /// �����û�е�ת����,�����ǽ����ڵ�.
        /// û���������ڵĸ���,ȫ���Ľڵ�.
        /// </summary>
        public Nodes HisToNodes
        {
            get
            {
                if (this._HisToNodes == null)
                {
                    _HisToNodes = new Nodes();
                    _HisToNodes.AddEntities(this.HisToNDs);
                }
                return this._HisToNodes;
            }
        }
        private Nodes _HisFromNodes = null;
        /// <summary>
        /// ���Ľ�Ҫ���Եķ��򼯺�
        /// �����û�е����ķ���,�����ǿ�ʼ�ڵ�.
        /// </summary>
        public Nodes HisFromNodes
        {
            get
            {
                if (this._HisFromNodes == null || this._HisFromNodes.Count == 0)
                {
                    Directions ens = new Directions();
                    this._HisFromNodes = ens.GetHisFromNodes(this.NodeID);
                }
                return _HisFromNodes;
            }
        }
        #endregion

        #region �������� (�û�ִ�ж���֮��,��Ҫ���Ĺ���)
        /// <summary>
        /// �û�ִ�ж���֮��,��Ҫ���Ĺ���		 
        /// </summary>
        /// <returns>������Ϣ,���е���Ϣ</returns>
        public string AfterDoTask()
        {
            return "";
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Node() { }
        /// <summary>
        /// �ڵ�
        /// </summary>
        /// <param name="_oid">�ڵ�ID</param>	
        public Node(int _oid)
        {
            this.NodeID = _oid;
            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + _oid);
            }
            else
            {
                if (this.Retrieve() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + _oid);
            }
        }
        public Node(string ndName)
        {
            ndName = ndName.Replace("ND", "");
            this.NodeID = int.Parse(ndName);

            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + ndName);
            }
            else
            {
                if (this.Retrieve() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + ndName);
            }
        }
        public string EnName
        {
            get
            {
                return "ND" + this.NodeID;
            }
        }
        public string EnsName
        {
            get
            {
                return "ND" + this.NodeID + "s";
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

                Map map = new Map("WF_Node");
                map.EnDesc = this.ToE("Node", "�ڵ�"); // "�ڵ�";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeAttr.NodeID, 0, this.ToE("NodeID", "�ڵ�ID"), true, true);
                map.AddTBString(NodeAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 100, 10);
                map.AddTBInt(NodeAttr.Step, (int)NodeWorkType.Work, this.ToE("FlowStep", "���̲���"), true, false);

                map.AddTBInt(NodeAttr.NodeWorkType, 0, "�ڵ�����", false, false);

                //  map.AddDDLSysEnum(NodeAttr.NodeWorkType, 0, "�ڵ�����", true, true, NodeAttr.NodeWorkType,
                //    "@0=��ʼ�ڵ�@1=��ʼ�����ڵ�@2=��׼��˽ڵ�@3=������˽ڵ�@4=��ǩ@5=�����ڵ�@6=�����ڵ�@7=�ֺ����ڵ�@8=��ͨ�ڵ�");

                // map.AddTBInt(NodeAttr.NodeWorkType, 0, "�ڵ�����", false, false);

                map.AddTBString(NodeAttr.FK_Flow, null, null, false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FlowName, null, null, false, true, 0, 100, 10);

                //map.AddTBString(NodeAttr.EnsName, null, "����s", false, false, 0, 100, 10);
                //map.AddTBString(NodeAttr.EnName, null, "����", false, false, 0, 100, 10);

                map.AddTBInt(NodeAttr.WarningDays, 0, this.ToE(NodeAttr.WarningDays, "��������(0������)"), false, false); // "��������(0������)"
                map.AddTBInt(NodeAttr.DeductDays, 1, this.ToE(NodeAttr.DeductDays, "����(��)"), false, false); //"����(��)"
                map.AddTBFloat(NodeAttr.DeductCent, 2, this.ToE(NodeAttr.DeductCent, "�۷�(ÿ����1���)"), false, false); //"�۷�(ÿ����1���)"
                map.AddTBFloat(NodeAttr.MaxDeductCent, 10, this.ToE(NodeAttr.MaxDeductCent, "��߿۷�"), false, false); //"��߿۷�"
                map.AddTBFloat(NodeAttr.SwinkCent, float.Parse("0.1"), this.ToE("SwinkCent", "�����÷�"), false, false); //"�����÷�"


                //map.AddDDLSysEnum(NodeAttr.DoWhere, 0, "�����ﴦ��", true, true);
                //map.AddDDLSysEnum(NodeAttr.RunType, 0, "ִ������", true, true);
                //map.AddTBStringDoc(NodeAttr.DoWhat, null, "������ɺ���ʲô", true, false);
                //map.AddTBStringDoc(NodeAttr.DoWhatMsg, null, "��ʾִ����Ϣ", true, false);


                map.AddTBString(NodeAttr.DoWhat, null, "��ɺ���SQL", true, false, 0, 500, 10);

                map.AddTBString(NodeAttr.Doc, null, BP.Sys.Language.GetValByUserLang("Desc", "����"), true, false, 0, 100, 10);

                map.AddBoolean(NodeAttr.IsTask, true, "������乤����?", true, true);
                map.AddBoolean(NodeAttr.IsSelectEmp, false, "�ɷ�ѡ�������?", true, true);
                
                map.AddBoolean(NodeAttr.IsCanReturn, false, "�Ƿ�����˻�", true, true);
                map.AddBoolean(NodeAttr.IsCanCC, true, "�Ƿ���Գ���", true, true);

                map.AddDDLSysEnum(NodeAttr.SignType, 0, "���ģʽ(����˽ڵ���Ч)", true, true, NodeAttr.SignType, "@0=��ǩ@1=��ǩ");
                map.AddDDLSysEnum(NodeAttr.RunModel, 0, "����ģʽ(����ͨ�ڵ���Ч)", true, true, NodeAttr.RunModel, "@0=��ͨ@1=����@2=����@3=�ֺ���");


                map.AddDDLSysEnum(NodeAttr.FLRole, 0, "��������", true, true, NodeAttr.FLRole, "@0=��������@1=������@2=����λ");

                map.AddDDLSysEnum(NodeAttr.FJOpen, 0, "����Ȩ��", true, true, NodeAttr.FJOpen, "@0=�رո���@1=����Ա@2=����ID@3=����ID");

                // ���̵Ľڵ��Ϊ����֧��. FNType  @0=ƽ��ڵ�@1=����@2=֧��.
                map.AddTBInt(NodeAttr.FNType, (int)FNType.Plane, "���̽ڵ�����", false, false);

                map.AddDDLSysEnum(NodeAttr.FJOpen, 0, "����Ȩ��", true, true, NodeAttr.FJOpen, "@0=�رո���@1=����Ա@2=����ID@3=����ID");

                map.AddDDLSysEnum(NodeAttr.FormType, 0, "������", true, true, NodeAttr.FormType, "@0=ϵͳ��@1=�Զ����");
                map.AddTBString(NodeAttr.FormUrl,  "http://","�Զ����URL", true, false, 0, 500, 10);



                

                //map.AddBoolean(NodeAttr.IsFL, false, "�Ƿ��Ƿ����ڵ�(��ͨ�ڵ���Ч)", true, true);
                //  map.AddDDLSysEnum(NodeAttr.NodePosType, 1, "λ��", true, false);


                map.AddTBInt(NodeAttr.NodePosType, 0, "λ��", false, false);


                map.AddTBInt(NodeAttr.IsCCNode, 0, "�Ƿ��нڵ��������", false, false);
                map.AddTBInt(NodeAttr.IsCCFlow, 0, "�Ƿ��������������", false, false);

                map.AddTBString(NodeAttr.HisStas, null, "��λ", false, false, 0, 4000, 10);
                map.AddTBString(NodeAttr.HisDeptStrs, null, "����", false, false, 0, 4000, 10);

                map.AddTBString(NodeAttr.HisToNDs, null, "ת���Ľڵ�", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.HisBookIDs, null, "����IDs", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.HisEmps, null, "HisEmps", false, false, 0, 600, 10);


                map.AddTBString(NodeAttr.PTable, null, "�����", false, false, 0, 100, 10);

                map.AddTBString(NodeAttr.ShowSheets, null, "��ʾ�ı�", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.GroupStaNDs, null, "��λ����ڵ�", false, false, 0, 200, 10);
                map.AddTBInt(NodeAttr.X, 0, "X����", false, false);
                map.AddTBInt(NodeAttr.Y, 0, "Y����", false, false);


                //map.AddTBDate(FlowAttr.LifeCycleFrom, BP.DA.DataType.CurrentData, "�������ڴ�", true, false);
                //map.AddTBDate(FlowAttr.LifeCycleTo, DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"), "��", true, false);

                map.AttrsOfOneVSM.Add(new NodeEmps(), new Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name, DeptAttr.No, "������Ա");
                map.AttrsOfOneVSM.Add(new NodeDepts(), new Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "���ܲ���");
                map.AttrsOfOneVSM.Add(new NodeStations(), new Stations(), NodeStationAttr.FK_Node, NodeStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "��λ");


             

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignSheet", "��Ʊ�"); // "��Ʊ�";
                rm.ClassMethodName = this.ToString() + ".DoMapData";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("BillBook", "����&����"); //"����&����";
                rm.ClassMethodName = this.ToString() + ".DoBook";
                rm.Icon = "/Images/Btn/Word.gif";
               

                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoFAppSet", "�����ⲿ����ӿ�"); // "�����ⲿ����ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoFAppSet";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoAction", "�����¼��ӿ�"); // "�����¼��ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoAction";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "����ʾ"; // this.ToE("DoAction", "�����¼��ӿ�"); // "�����¼��ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoShowSheets";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoCond", "�ڵ��������"); // "�ڵ��������";
                rm.ClassMethodName = this.ToString() + ".DoCond";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("DoCondFL", "������ɹ���"); // "������ɹ���";
                //rm.ClassMethodName = this.ToString() + ".DoCondFL";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoShowSheets()
        {
            PubClass.WinOpen("./../WF/Admin/ShowSheets.aspx?CondType=0&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "����", "cdn", 400, 400, 200, 300);
            return null;
        }
        public string DoCond()
        {
            //if (this.IsCheckNode)
            //    return "��˽ڵ㲻�����ýڵ����������";

            PubClass.WinOpen("./../WF/Admin/Cond.aspx?CondType=" + (int)CondType.Flow + "&FK_Flow=" + this.FK_Flow + "&FK_MainNode=" + this.NodeID + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "����", "cdn", 400, 400, 200, 300);
            return null;
        }
        public string DoCondFL()
        {
            //if (this.IsCheckNode)
            //    return "��˽ڵ㲻������ ������ɹ��� ��";

            PubClass.WinOpen("./../WF/Admin/Cond.aspx?CondType=" + (int)CondType.FLRole + "&FK_Flow=" + this.FK_Flow + "&FK_MainNode=" + this.NodeID + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=", "������ɹ���", "cdn", 400, 400, 200, 300);
            return null;
        }
        public string DoMapData()
        {
            //if (this.IsCheckNode)
            //    return "��˽ڵ��ϲ�����Ʊ���";

            PubClass.WinOpen("./../WF/MapDef/MapDef.aspx?PK=ND" + this.NodeID, "��Ƶ�", "sheet", 800, 500, 210, 300);
            return null;
        }
        public string DoAction()
        {
            PubClass.WinOpen("./../WF/Admin/Action.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "����", "book", 800, 500, 200, 300);
            return null;
        }
        public string DoBook()
        {
            PubClass.WinOpen("./../WF/Admin/Book.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "����", "book", 800, 500, 200, 300);
            return null;
        }
        public string DoFAppSet()
        {
            PubClass.WinOpen("./../WF/Admin/FAppSet.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "����", "sd", 800, 500, 200, 200);
            //  PubClass.WinOpen("./../WF/Admin/FAppSet.aspx?NodeID=" + this.NodeID, 400, 500);
            return null;
        }
        #endregion


        protected override bool beforeDelete()
        {

            BP.DA.DBAccess.RunSQL("UPDATE WF_Node Set ShowSheets=replace(ShowSheets,'@" + this.NodeID + "','') WHERE FK_Flow='" + this.FK_Flow + "'");

            //  BP.DA.DBAccess.RunSQL("UPDATE WF_Node Set ShowSheets=replace(ShowSheets,'@" + this.NodeID + "','')");

            if (this.IsCheckNode)
                return base.beforeDelete();

            // ɾ�����Ľڵ㡣
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            md.Delete();

            //ɾ��������ϸ��
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls(md.No);
            foreach (BP.Sys.MapDtl dtl in dtls)
            {
                dtl.Delete();
            }
            return base.beforeDelete();
        }
        public void RepariMap()
        {
            if (this.IsCheckNode)
                return;

            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            if (md.RetrieveFromDBSources() == 1)
            {
            }
            else
            {
                md.Name = this.Name;
                md.Insert();
            }


            Map map = md.GenerHisMap();
            if (md.No == "ND101")
            {
                int Y = 0;
            }
            if (map.Attrs.Contains(WorkAttr.FID) == false)
            {
                BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.Readonly;
                attr.KeyOfEn = "FID";
                attr.Name = "FID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }
        }
        private void AddDocAttr(BP.Sys.MapData md)
        {
            /*������������̣� */

            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;

            attr.HisEditType = BP.Sys.EditType.UnDel;

            attr.KeyOfEn = "Title";
            attr.Name = "����";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.UIIsLine = true;
            attr.IDX = -100;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;


            attr.KeyOfEn = "KeyWord";
            attr.Name = "�����";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.UIIsLine = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = -99;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;

            attr.KeyOfEn = "FZ";
            attr.Name = "��ע";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.UIIsLine = true;
            attr.IDX = 1;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.IDX = -98;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.Sys.EditType.UnDel;

            attr.KeyOfEn = "DW_SW";
            attr.Name = "���ĵ�λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.UIIsLine = true;

            attr.IDX = 1;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;

            attr.KeyOfEn = "DW_FW";
            attr.Name = "���ĵ�λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;

            attr.KeyOfEn = "DW_BS";
            attr.Name = "����(��)��λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;

            attr.KeyOfEn = "DW_CS";
            attr.Name = "����(��)��λ";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;

            attr.KeyOfEn = "NumPrint";
            attr.Name = "ӡ�Ʒ���";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 10;
            attr.IDX = 1;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.UIIsLine = false;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;

            attr.KeyOfEn = "JMCD";
            attr.Name = "���̶ܳ�";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.Enum;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.UIIsLine = false;
            attr.UIBindKey = "JMCD";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.KeyOfEn = "PRI";
            attr.Name = "�����̶�";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.Enum;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.UIIsLine = false;
            attr.UIBindKey = "PRI";
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "GWWH";
            attr.Name = "�����ĺ�";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.UIIsLine = false;
            attr.Insert();
        }
        /// <summary>
        /// ����map
        /// </summary>
        public void CreateMap()
        {
           

            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            if (md.RetrieveFromDBSources() == 1)
                return;
            md.Name = this.Name;
            md.Insert();


            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
        
            attr.KeyOfEn = "OID";
            attr.Name = "WorkID";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.DefVal = "0";
            attr.HisEditType = BP.Sys.EditType.Readonly;
            attr.Insert();


            bool isDocFlow = false;
            if (this.HisFlow.HisFlowSheetType == FlowSheetType.DocFlow)
                isDocFlow = true;

            if (isDocFlow)
            {
                this.AddDocAttr(md);
            }


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
           
            attr.KeyOfEn = "FID";
            attr.Name = "FID";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.DefVal = "0";
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.Sys.EditType.Readonly;
            attr.KeyOfEn = WorkAttr.RDT;
            attr.Name = BP.Sys.Language.GetValByUserLang("AcceptTime", "����ʱ��");  //"����ʱ��";
            attr.MyDataType = BP.DA.DataType.AppDateTime;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;

            if (this.IsStartNode)
                attr.UIVisible = false;
            else
                attr.UIVisible = true;

            attr.UIIsEnable = false;
            attr.Tag = "1";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            
            attr.KeyOfEn = WorkAttr.CDT;
            if (this.IsStartNode)
                attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "����ʱ��"); //"����ʱ��";
            else
                attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "���ʱ��"); //"���ʱ��";

            attr.MyDataType = BP.DA.DataType.AppDateTime;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = false;
            attr.DefVal = "@RDT";
            attr.Tag = "1";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.Rec;
            if (this.IsStartNode == false)
                attr.Name = BP.Sys.Language.GetValByUserLang("Rec", "��¼��"); // "��¼��";
            else
                attr.Name = BP.Sys.Language.GetValByUserLang("Sponsor", "������"); //"������";

            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = false;
            attr.MaxLen = 20;
            attr.MinLen = 0;
            attr.DefVal = "@WebUser.No";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.Sys.EditType.Readonly;
            attr.KeyOfEn = WorkAttr.Emps;
            attr.Name = "Emps";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MaxLen = 400;
            attr.MinLen = 0;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
           
            attr.KeyOfEn = WorkAttr.NodeState;
            attr.Name = BP.Sys.Language.GetValByUserLang("NodeState", "�ڵ�״̬"); //"�ڵ�״̬";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.DefVal = "0";
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.HisEditType = BP.Sys.EditType.UnDel;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.Sys.EditType.UnDel;

            attr.KeyOfEn = StartWorkAttr.FK_Dept;
            attr.Name = BP.Sys.Language.GetValByUserLang("OperDept", "����Ա����"); //"����Ա����";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.FK;
            attr.UIBindKey = "BP.Port.Depts";
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MinLen = 0;
            attr.MaxLen = 20;
            attr.Insert();

            if (this.NodePosType == NodePosType.Start)
            {
                //��ʼ�ڵ���Ϣ��
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.Readonly;
                attr.KeyOfEn = StartWorkAttr.WFState;
                attr.DefVal = "0";
                attr.Name = BP.Sys.Language.GetValByUserLang("FlowState", "����״̬"); //"����״̬";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.LGType = FieldTypeS.Normal;
                attr.UIBindKey = attr.KeyOfEn;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.Readonly;
                attr.KeyOfEn = StartWorkAttr.WFLog;
                attr.Name = BP.Sys.Language.GetValByUserLang("Log", "��־"); //"��־";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = true;
                attr.MinLen = 0;
                attr.MaxLen = 3000;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = "BillNo";
                attr.Name = BP.Sys.Language.GetValByUserLang("BillNo", "�����ĺ�"); //"�����ĺ�";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 10;
                attr.Insert();
                if (isDocFlow == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.Sys.EditType.UnDel;
                    attr.KeyOfEn = "Title";
                    attr.Name = BP.Sys.Language.GetValByUserLang("Title", "���̱���"); // "���̱���";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = true;
                    attr.UIIsEnable = true;
                    attr.UIIsLine = true;
                    attr.MinLen = 0;
                    attr.MaxLen = 200;
                    attr.IDX = -100;
                    attr.Insert();
                }

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = "FK_NY";
                attr.Name = BP.Sys.Language.GetValByUserLang("YearMonth", "����"); //"����";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.LGType = FieldTypeS.Normal;
                //attr.UIBindKey = "BP.Pub.NYs";
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 7;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = "MyNum";
                attr.Name = BP.Sys.Language.GetValByUserLang("Num", "����"); // "����";
                attr.DefVal = "1";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = "FK_Dept";
                attr.Name = BP.Sys.Language.GetValByUserLang("SponsorDept", "�����˲���"); //"�����˲���";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 7;
                attr.DefVal = "@WebUser.FK_Dept";
                attr.Insert();
            }

            if (this.IsCheckNode)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = GECheckStandAttr.Note; // "CheckState";
                attr.Name = "������"; // BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 4000;
                attr.IDX = -100;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = GECheckStandAttr.RefMsg; // "CheckState";
                attr.Name = "������Ϣ"; //BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 4000;
                attr.IDX = -100;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = GECheckStandAttr.CheckState;
                attr.Name = "���״̬"; // BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.IDX = -100;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.Sys.EditType.UnDel;
                attr.KeyOfEn = GECheckStandAttr.Sender;
                attr.Name = "������"; // BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.IDX = -100;
                attr.Insert();
            }
        }


         



    }
    /// <summary>
    /// �ڵ㼯��
    /// </summary>
    public class Nodes : EntitiesOID
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Node();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ڵ㼯��
        /// </summary>
        public Nodes()
        {
        }
        /// <summary>
        /// �ڵ㼯��.
        /// </summary>
        /// <param name="FlowNo"></param>
        public Nodes(string fk_flow)
        {
            //   Nodes nds = new Nodes();
            this.Retrieve(NodeAttr.FK_Flow, fk_flow, NodeAttr.Step);
            //this.AddEntities(NodesCash.GetNodes(fk_flow));
            return;
        }
        #endregion

        #region ��ѯ����
        /// <summary>
        /// RetrieveAll
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            Nodes nds = Cash.GetObj(this.ToString(), Depositary.Application) as Nodes;
            if (nds == null)
            {
                nds = new Nodes();
                QueryObject qo = new QueryObject(nds);
                qo.AddWhereInSQL(NodeAttr.NodeID, " SELECT Node FROM WF_Direction ");
                qo.addOr();
                qo.AddWhereInSQL(NodeAttr.NodeID, " SELECT ToNode FROM WF_Direction ");
                qo.DoQuery();

                Cash.AddObj(this.ToString(), Depositary.Application, nds);
                Cash.AddObj(this.GetNewEntity.ToString(), Depositary.Application, nds);
            }

            this.Clear();
            this.AddEntities(nds);
            return this.Count;
        }
        /// <summary>
        /// ��ʼ�ڵ�
        /// </summary>
        public void RetrieveStartNode()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeAttr.NodePosType, (int)NodePosType.Start);
            qo.addAnd();
            qo.AddWhereInSQL(NodeAttr.NodeID, "SELECT FK_NODE FROM WF_NODESTATION WHERE FK_STATION IN (SELECT FK_STATION FROM PORT_EMPSTATION WHERE FK_EMP='" + Web.WebUser.No + "')");

            qo.addOrderBy(NodeAttr.FK_Flow);
            qo.DoQuery();
        }
        #endregion
    }
}

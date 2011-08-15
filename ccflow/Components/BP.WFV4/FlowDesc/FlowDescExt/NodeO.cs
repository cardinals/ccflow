using System;
using System.Data;
using BP.DA;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF.Ext
{
    /// <summary>
    /// ������ÿ���ڵ����Ϣ.
    /// </summary>
    public class NodeO : Entity, IDTS
    {
        /// <summary>
        /// Ͷ�ݹ���
        /// </summary>
        public ReturnRole HisReturnRole
        {
            get
            {
                return (ReturnRole)this.GetValIntByKey(NodeAttr.ReturnRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.ReturnRole, (int)value);
            }
        }
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
        /// Ͷ�ݹ���
        /// </summary>
        public DeliveryWay HisDeliveryWay
        {
            get
            {
                return (DeliveryWay)this.GetValIntByKey(NodeAttr.DeliveryWay);
            }
            set
            {
                this.SetValByKey(NodeAttr.DeliveryWay, (int)value);
            }
        }
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
        public string Name
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.Name);
            }
            set
            {
                this.SetValByKey(NodeAttr.Name, value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(NodeAttr.FK_Flow, value);
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
                this.SetValByKey(NodeAttr.FlowName, value);
            }
        }
        /// <summary>
        /// ������sql
        /// </summary>
        public string RecipientSQL
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.RecipientSQL);
            }
            set
            {
                this.SetValByKey(NodeAttr.RecipientSQL, value);
            }
        }
        /// <summary>
        /// �Ƿ�����˻�
        /// </summary>
        public bool ReturnEnable
        {
            get
            {
                return this.GetValBooleanByKey(BtnAttr.ReturnRole);
            }
        }
        public bool AthEnable
        {
            get
            {
                if (HisFJOpen == FJOpen.None)
                    return false;
                return true;
            }
        }
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        protected override bool beforeUpdate()
        {
            //if (this.HisReturnRole == ReturnRole.CanNotReturn)
            //    this.ReturnEnable = false;
            //else
            //    this.ReturnEnable = true;

            #region ���������ж������ı��
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0  WHERE FK_Flow='" + this.FK_Flow + "'");
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=0) AND FK_Flow='" + this.FK_Flow + "'");
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=1) AND FK_Flow='" + this.FK_Flow + "'");
            #endregion ���������ж������ı��

            Flow fl = new Flow();
            fl.No = this.FK_Flow;
            fl.RetrieveFromDBSources();

            this.FlowName = fl.Name;
            //  Node.CheckFlow(fl);
            DBAccess.RunSQL("UPDATE Sys_MapData SET Name='" + this.Name + "' WHERE No='ND" + this.NodeID + "'");
            return base.beforeUpdate();
        }


        #region ���Ի�ȫ�ֵ� Node
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
        #endregion

        #region ���캯��
        /// <summary>
        /// �ڵ�
        /// </summary>
        public NodeO() { }
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
                map.EnDesc = this.ToE("Node", "�ڵ�");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                // ��������
                map.AddTBIntPK(NodeAttr.NodeID, 0, this.ToE("NodeID", "�ڵ�ID"), true, true);
                map.AddTBInt(NodeAttr.Step, (int)NodeWorkType.Work, "����", true, false);

                map.AddTBString(NodeAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 100, 10, true);
                map.AddBoolean(NodeAttr.IsTask, true, this.ToE("IsTask", "������乤����?"), true, true, false);

                 
                map.AddBoolean(NodeAttr.IsForceKill, false, "�Ƿ����ǿ��ɾ��������(�Ժ�������Ч)", true, true, false);
               

                // map.AddTBInt(NodeAttr.PassRate, 100, "ͨ����(���ں����ڵ���Ч)", true, true);
                map.AddTBDecimal(NodeAttr.PassRate, 0, "���ͨ����", true, false);

                map.AddDDLSysEnum(NodeAttr.RunModel, 0, this.ToE("RunModel", "����ģʽ"),
                    true, true, NodeAttr.RunModel, "@0=��ͨ@1=����@2=����@3=�ֺ���");

                //map.AddDDLSysEnum(NodeAttr.FLRole, 0, this.ToE("FLRole", "��������"), true, true, NodeAttr.FLRole,
                //    "@0=��������@1=������@2=����λ");


                map.AddDDLSysEnum(NodeAttr.DeliveryWay, 0, "Ͷ�ݹ���", true, true);
                map.AddTBString(NodeAttr.RecipientSQL, null, "������SQL", true, false, 0, 500, 10, true);

                map.AddDDLSysEnum(NodeAttr.FormType, 0, this.ToE("FormType", "������"), true, true);

                map.AddTBString(NodeAttr.FormUrl, null, this.ToE("FormUrl", "��URL"), true, false, 0, 500, 10, true);
                map.AddTBString(NodeAttr.DoWhat, null, this.ToE("DoWhat", "��ɺ���SQL"), false, false, 0, 500, 10, false);

                map.AddTBString(NodeAttr.MsgSend, null, "���ͳɹ�����ʾ��Ϣ", true, false, 0, 2000, 10, true);

                //map.AddBoolean("IsSkipReturn", false, "�Ƿ���Կ缶����", true, true, true);

                map.AddTBDateTime("DTFrom", "�������ڴ�", true, true);
                map.AddTBDateTime("DTTo", "�������ڵ�", true, true);


                #region  ���ܰ�ť״̬
                map.AddTBString(BtnAttr.SendLab, "����", "���Ͱ�ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SendEnable, true, "�Ƿ�����", true, false);

                map.AddTBString(BtnAttr.SaveLab, "����", "���水ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.SaveEnable, true, "�Ƿ�����", true, true);

                map.AddTBString(BtnAttr.ReturnLab, "�˻�", "�˻ذ�ť��ǩ", true, false, 0, 50, 10);
                map.AddDDLSysEnum(NodeAttr.ReturnRole, 0, this.ToE("ReturnRole", "�˻ع���"),
           true, true, NodeAttr.ReturnRole);

                map.AddTBString(BtnAttr.CCLab, "����", "���Ͱ�ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.CCEnable, true, "�Ƿ�����", true, true);

                map.AddTBString(BtnAttr.ShiftLab, "�ƽ�", "�ƽ���ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.ShiftEnable, true, "�Ƿ�����", true, true);

                map.AddTBString(BtnAttr.DelLab, "ɾ��", "ɾ����ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.DelEnable, true, "�Ƿ�����", true, true);

                map.AddTBString(BtnAttr.EndFlowLab, "��������", "�������̰�ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.EndFlowEnable, false, "�Ƿ�����", true, true);

                map.AddTBString(BtnAttr.RptLab, "����", "���水ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.RptEnable, true, "�Ƿ�����", true, true);

                map.AddTBString(BtnAttr.AthLab, "����", "������ť��ǩ", true, false, 0, 50, 10);
                map.AddDDLSysEnum(NodeAttr.FJOpen, 0, this.ToE("FJOpen", "����Ȩ��"), true, true, 
                    NodeAttr.FJOpen, "@0=�رո���@1=����Ա@2=����ID@3=����ID");

                map.AddTBString(BtnAttr.TrackLab, "�켣", "�켣��ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.TrackEnable, true, "�Ƿ�����", true, true);

                map.AddTBString(BtnAttr.OptLab, "ѡ��", "ѡ�ť��ǩ", true, false, 0, 50, 10);
                map.AddBoolean(BtnAttr.OptEnable, true, "�Ƿ�����", true, true);
                 
                #endregion  ���ܰ�ť״̬


                // ��������
                map.AddTBInt(NodeAttr.WarningDays, 0, this.ToE(NodeAttr.WarningDays, "��������(0������)"), true, false); // "��������(0������)"
                map.AddTBInt(NodeAttr.DeductDays, 1, this.ToE(NodeAttr.DeductDays, "����(��)"), true, false); //"����(��)"
                map.AddTBFloat(NodeAttr.DeductCent, 2, this.ToE(NodeAttr.DeductCent, "�۷�(ÿ����1���)"), true, false); //"�۷�(ÿ����1���)"

                map.AddTBFloat(NodeAttr.MaxDeductCent, 0, this.ToE(NodeAttr.MaxDeductCent, "��߿۷�"), true, false);   //"��߿۷�"
                map.AddTBFloat(NodeAttr.SwinkCent, float.Parse("0.1"), this.ToE("SwinkCent", "�����÷�"), true, false); //"�����÷�"

                map.AddDDLSysEnum(NodeAttr.OutTimeDeal, 0, this.ToE("OutTimeDeal", "��ʱ����"),
                true, true, NodeAttr.OutTimeDeal, "@0=������@1=�Զ�ת����һ��@2=�Զ�ת��ָ������Ա@3=��ָ������Ա������Ϣ@4=ɾ������@5=ִ��SQL");

                map.AddTBString(NodeAttr.DoOutTime, null, "��������", true, false, 0, 500, 10, true);
                map.AddTBString(NodeAttr.FK_Flow, null, "flow", false, false, 0, 100, 10);


                // ��ع��ܡ�
                map.AttrsOfOneVSM.Add(new BP.WF.NodeStations(), new BP.WF.Port.Stations(),
                    NodeStationAttr.FK_Node, NodeStationAttr.FK_Station,
                    DeptAttr.Name, DeptAttr.No, this.ToE("NodeSta", "�ڵ��λ"));

                map.AttrsOfOneVSM.Add(new BP.WF.NodeDepts(), new BP.WF.Port.Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name,
                DeptAttr.No, this.ToE("AccptDept", "�ڵ㲿��"));


                map.AttrsOfOneVSM.Add(new BP.WF.NodeEmps(), new BP.WF.Port.Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name,
                    DeptAttr.No, this.ToE("Accpter", "������Ա"));


                map.AttrsOfOneVSM.Add(new BP.WF.NodeFlows(), new Flows(), NodeFlowAttr.FK_Node, NodeFlowAttr.FK_Flow, DeptAttr.Name, DeptAttr.No,
                    this.ToE("CallSubFlow", "�ɵ��õ�������"));

                map.AttrsOfOneVSM.Add(new BP.WF.NodeReturns(), new BP.WF.Port.Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name,
                  DeptAttr.No, this.ToE("Accpter", "���˻صĽڵ�"));

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignSheet", "��Ʊ�"); // "��Ʊ�";
                rm.ClassMethodName = this.ToString() + ".DoMapData";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("Bill", "���ݴ�ӡ"); //"����&����";
                rm.ClassMethodName = this.ToString() + ".DoBill";
                rm.Icon = "/Images/FileType/doc.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoFAppSet", "�����ⲿ����ӿ�"); // "�����ⲿ����ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoFAppSet";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoAction", "�¼�"); // "�����¼��ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoAction";
                map.AddRefMethod(rm);


                //rm = new RefMethod();
                //rm.Title = "����ʾ"; // this.ToE("DoAction", "�����¼��ӿ�"); // "�����¼��ӿ�";
                //rm.ClassMethodName = this.ToString() + ".DoShowSheets";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoCond", "�ڵ��������"); // "�ڵ��������";
                rm.ClassMethodName = this.ToString() + ".DoCond";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = this.ToE("DoListen", "��Ϣ����"); // "�����¼��ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoListen";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("DoFeatureSet", "���Լ�"); // "�����¼��ӿ�";
                //rm.ClassMethodName = this.ToString() + ".DoFeatureSet";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoListen()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoListen();
        }
        public string DoFeatureSet()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoFeatureSet();
        }
        public string DoShowSheets()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoShowSheets();
        }
        public string DoCond()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoCond();
        }
        //public string DoCondFL()
        //{
        //    BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        //    return nd.DoCondFL();
        //}
        public string DoMapData()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoMapData();
        }
        public string DoAction()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoAction();
        }
        public string DoBill()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoBill();
        }
        public string DoFAppSet()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoFAppSet();
        }
        #endregion
    }
    /// <summary>
    /// �ڵ㼯��
    /// </summary>
    public class NodeOs : EntitiesOID
    {
        #region ���췽��
        /// <summary>
        /// �ڵ㼯��
        /// </summary>
        public NodeOs()
        {
        }
        #endregion

        public override Entity GetNewEntity
        {
            get { return new NodeO(); }
        }
    }
}

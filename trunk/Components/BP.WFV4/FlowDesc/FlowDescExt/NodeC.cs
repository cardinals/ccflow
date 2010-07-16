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
    public class NodeC : Entity, IDTS
    {
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
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        #region ���Ի�ȫ�ֵ� Nod
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
        public NodeC() { }
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

                map.AddTBIntPK(NodeAttr.NodeID, 0, this.ToE("NodeID", "�ڵ�ID"), true, true);
                map.AddTBString(NodeAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 100, 10);
                map.AddTBInt(NodeAttr.Step, (int)NodeWorkType.Work, this.ToE("FlowStep", "���̲���"), true, false);
                map.AddBoolean(NodeAttr.IsTask, true, "������乤����?", true, true);
                map.AddBoolean(NodeAttr.IsSelectEmp, false, "�ɷ�ѡ�������?", true, true);
                map.AddBoolean(NodeAttr.IsCanReturn, false, "�Ƿ�����˻�", true, true);
                map.AddBoolean(NodeAttr.IsCanCC, true, "�Ƿ���Գ���", true, true);
                map.AddDDLSysEnum(NodeAttr.SignType, 0, "���ģʽ", true, true, NodeAttr.SignType, "@0=��ǩ@1=��ǩ");
                map.AddDDLSysEnum(NodeAttr.FLRole, 0, "��������-�Է�����Ч", true, true, NodeAttr.FLRole, "@0=��������@1=������@2=����λ");
                map.AddDDLSysEnum(NodeAttr.FJOpen, 0, "����Ȩ��", true, true, NodeAttr.FJOpen, "@0=�رո���@1=����Ա@2=����ID@3=����ID");

                map.AddDDLSysEnum(NodeAttr.FormType, 0, "������", true, true, NodeAttr.FormType, "@0=ϵͳ��@1=�Զ����");
                map.AddTBString(NodeAttr.FormUrl, "http://", "�Զ����URL", true, false, 0, 500, 10);


                map.AddTBString(NodeAttr.DoWhat, null, "��ɺ���SQL", true, false, 0, 500, 10);
                Attr attr = map.GetAttrByKey(NodeAttr.DoWhat);
                attr.UIIsLine = true;


                // �������á�
                map.AddTBInt(NodeAttr.WarningDays, 0, this.ToE(NodeAttr.WarningDays, "��������(0������)"), true, false); // "��������(0������)"
                map.AddTBInt(NodeAttr.DeductDays, 1, this.ToE(NodeAttr.DeductDays, "����(��)"), true, false); //"����(��)"
                map.AddTBFloat(NodeAttr.DeductCent, 2, this.ToE(NodeAttr.DeductCent, "�۷�(ÿ����1���)"), true, false); //"�۷�(ÿ����1���)"

                map.AddTBFloat(NodeAttr.MaxDeductCent, 0, this.ToE(NodeAttr.MaxDeductCent, "��߿۷�"), true, false);   //"��߿۷�"
                map.AddTBFloat(NodeAttr.SwinkCent, float.Parse("0.1"), this.ToE("SwinkCent", "�����÷�"), true, false); //"�����÷�"


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
                // rm.Target = "_self";
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

                this._enMap = map;
                return this._enMap;
            }
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
        public string DoCondFL()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoCondFL();
        }
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
        public string DoBook()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoBook();
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
    public class NodeCs : EntitiesOID
    {
        #region ���췽��
        /// <summary>
        /// �ڵ㼯��
        /// </summary>
        public NodeCs()
        {
        }
        #endregion

        public override Entity GetNewEntity
        {
            get { return new NodeC(); }
        }
    }
}

using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// ���̱���
    /// </summary>
    public class WFRptAttr:BP.En.EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ִ��
        /// </summary>
        public const string DoWhat = "DoWhat";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ⲿ�������õ���Ϣ.	 
    /// </summary>
    public class WFRpt : EntityNoName
    {
        #region ��������
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsUpdate = true;
                uac.IsDelete = true;
                return uac;
            }
        }
        /// <summary>
        /// �ⲿ�������õ�������
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
        public string FK_FlowT
        {
            get
            {
                return this.GetValRefTextByKey(NodeAttr.FK_Flow);
            }
        }
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(WFRptAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(WFRptAttr.FK_FlowSort, value);
            }
        }
        public string FK_FlowSortT
        {
            get
            {
                return this.GetValRefTextByKey(WFRptAttr.FK_FlowSort);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        public WFRpt() { }
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        /// <param name="_oid">�ⲿ��������ID</param>	
        public WFRpt(string _oid)
        {
            this.No = _oid;
            this.Retrieve();
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
                Map map = new Map("WF_Rpt");

                map.EnDesc = this.ToE("FlowView", "������ͼ"); // "������ͼ(����Ƶı���ϵͳ���Զ�������ǰ̨)";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(WFRptAttr.No, null, this.ToE("View","��ͼ"), true, true, 2, 20, 10);
                map.AddTBString(WFRptAttr.Name, null, this.ToE("RptName" , "��������"), true, false, 0, 400, 10);

                map.AddDDLEntities(WFRptAttr.FK_Flow, null,null, new Flows(), false);
                map.AddDDLEntities(WFRptAttr.FK_FlowSort, null, null, new FlowSorts(), false);

                map.AttrsOfOneVSM.Add(new RptStations(), new Stations(), RptStationAttr.FK_Rpt, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, this.ToE("StaRight", "��λ����Ȩ��"));
                map.AttrsOfOneVSM.Add(new RptEmps(), new Emps(), RptEmpAttr.FK_Rpt, RptEmpAttr.FK_Emp, DeptAttr.Name, DeptAttr.No, this.ToE("EmpRight", "��Ա����Ȩ��"));

                //map.AddTBInt(WFRptAttr.NodeID, 0, "NodeID", false, false);
                //map.AddDDLSysEnum(WFRptAttr.ShowTime, 0, "����ʱ��", true, false, WFRptAttr.ShowTime, "@0=��(��ʾ�ڱ��ײ�)@1=������ѡ��ʱ@2=������ʱ@3=������ʱ");
                //map.AddTBString(WFRptAttr.FK_Flow, null, "���̱��", false, true, 0, 100, 10);
                //map.AddTBString(WFRptAttr.DoWhat, null, "ִ��ʲô��", false, true, 0, 100, 10);
                //map.AddTBInt(WFRptAttr.H, 0, "���ڸ߶�", false, false);
                //map.AddTBInt(WFRptAttr.W, 0, "���ڿ��", false, false);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("ViewDef", "��ͼ����");  //"��ͼ����";
                rm.ClassMethodName = this.ToString() + ".DoField()";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("RptRun", "��������");  //"��������";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("NewRpt", "�½�����");  //"�½�����";
                rm.ClassMethodName = this.ToString() + ".DoNew()";
                rm.Icon = "/Images/Btn/New.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("OperVideo", "¼�����"); // "¼�����";
                rm.ClassMethodName = this.ToString() + ".DoHelp()";
                rm.Icon = "/Images/FileType/rm.gif";
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoNew()
        {
            WFRpt rpt = new WFRpt();
            rpt.No = "V" + this.FK_Flow;
            int i = 0;
            while (rpt.IsExits)
            {
                rpt.No = "V" + this.FK_Flow + "_" + i;
            }
            rpt.FK_Flow = this.FK_Flow;
            rpt.FK_FlowSort = this.FK_FlowSort;
            rpt.Name = this.ToE("NewView", "�½�������ͼ");  //"�½�������ͼ";
            rpt.Insert();

            BP.PubClass.WinOpen("../Comm/UIEn.aspx?EnsName=BP.WF.WFRpts&MyPK=" + rpt.No, 500, 600);
            return null;
        }
        public string DoHelp()
        {
            BP.PubClass.WinOpen("www.ccflow.cn" , 500, 600);
            return null;
        }
       
        
        public string DoPanelEns()
        {
            BP.PubClass.WinOpen("../Comm/PanelEns.aspx?EnsName=" + this.No,500,600);
            return null;
        }
        public string DoGroup()
        {
            BP.PubClass.WinOpen("../Comm/GroupEnsMNum.aspx?EnsName=" + this.No, 500, 600);
            return null;
        }
        public string DoPower()
        {
            BP.PubClass.WinOpen("");
            return null;
        }
        public string DoField()
        {
            BP.PubClass.WinOpen("../WF/Admin/WFRptD.aspx?FK_Flow=" + this.FK_Flow + "&DoType=Edit&RefNo=" + this.No, 700 , 400);
            return null;
        }

        public string DoOpenRpt()
        {
            BP.PubClass.WinOpen("../WF/SelfWFRpt.aspx?FK_Flow=" + this.FK_Flow + "&DoType=Edit&RefNo=" + this.No, 700, 400);
            return null;
        }
        
        /// <summary>
        /// ���Ի�����
        /// </summary>
        /// <returns></returns>
        public void DoInitAttrs()
        {
            MapData md = new MapData();
            md.No = this.No;
            md.RetrieveFromDBSources();
            if (md.SearchKeys == "")
                md.SearchKeys = "@FK_Dept@FK_NY@";

            md.No = this.Name;
            md.Save();

            RptAttrs attrs = new RptAttrs(this.No);
            RptAttr rptAttr = new RptAttr();

            MapAttr mattrN = new MapAttr(); //��ʱ������

            Flow fl = new Flow(this.FK_Flow);
            MapAttrs mAttrsStartNode = new MapAttrs("ND" + int.Parse(this.FK_Flow) + "01"); // 
            string startNodeID = int.Parse(this.FK_Flow).ToString() + "01";
            if (attrs.Contains(RptAttrAttr.Field, "OID") == false)
            {
                /* �Ƿ��������״̬ */
                MapAttr mattr = mAttrsStartNode.GetEntityByKey(MapAttrAttr.KeyOfEn, "OID") as MapAttr;
                rptAttr.MyPK = this.No + "_OID";

                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;
                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "OID";
                rptAttr.RefFieldName = "����ID";

                rptAttr.Field = "OID";
                rptAttr.RefFieldName = "����ID";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }
            mattrN = new MapAttr(this.No, "OID");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
               // mattrN.OID = 0;
                mattrN.Name = "����ID";
                mattrN.KeyOfEn = "OID";
                mattrN.MyDataType = BP.DA.DataType.AppInt;
                mattrN.DefVal="0" ; 
                mattrN.UIVisible=true;
                mattrN.UIContralType= UIContralType.TB;
                mattrN.Insert();
            }

            if (attrs.Contains(RptAttrAttr.Field, "WFState") == false)
            {
                /* �Ƿ��������״̬ */
                MapAttr mattr = mAttrsStartNode.GetEntityByKey(MapAttrAttr.KeyOfEn, "WFState") as MapAttr;

                rptAttr.MyPK = this.No + "_WFState";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "WFState";
                rptAttr.RefFieldName = this.ToE("FlowState", "����״̬"); //"����״̬";

                rptAttr.Field = "WFState";
                rptAttr.RefFieldName = this.ToE("FlowState", "����״̬");  //"����״̬";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }
            mattrN = new MapAttr(this.No, "WFState");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
                //mattrN.OID = 0;
                mattrN.Name = this.ToE("FlowState", "����״̬"); // "����״̬";
                mattrN.KeyOfEn = "WFState";
                mattrN.MyDataType = BP.DA.DataType.AppInt;
                mattrN.DefVal = "0";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.DDL;
                mattrN.UIBindKey = "WFState";
                mattrN.LGType = FieldTypeS.Enum;
                mattrN.Insert();
            }


            if (attrs.Contains(RptAttrAttr.Field, "RDT") == false)
            {
                /* �Ƿ��������״̬ */
                MapAttr mattr = mAttrsStartNode.GetEntityByKey(MapAttrAttr.KeyOfEn, "RDT") as MapAttr;

                rptAttr.MyPK = this.No + "_RDT";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "RDT";
                rptAttr.RefFieldName = this.ToE("StartDate", "��������");  //"��������";

                rptAttr.Field = "RDT";
                rptAttr.RefFieldName = this.ToE("StartDate","��������");  //"��������";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }

            mattrN = new MapAttr(this.No, "RDT");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
                //mattrN.OID = 0;
                mattrN.Name = this.ToE("StartDate", "��������");  //"��������";
                mattrN.KeyOfEn = "RDT";
                mattrN.MyDataType = BP.DA.DataType.AppDate;
                mattrN.DefVal = "";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.TB;
                mattrN.Insert();
            }


            if (attrs.Contains(RptAttrAttr.Field, "FK_NY") == false)
            {
                /* �Ƿ��������״̬ */
                rptAttr.MyPK = this.No + "_FK_NY";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "FK_NY";
                rptAttr.RefFieldName = this.ToE("BNY", "��������"); //"��������";

                rptAttr.Field = "FK_NY";
                rptAttr.RefFieldName = this.ToE("BNY", "��������");  //"��������";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }

            mattrN = new MapAttr(this.No, "FK_NY");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
              //  mattrN.OID = 0;
                mattrN.Name = "����";
                mattrN.KeyOfEn = "FK_NY";
                mattrN.MyDataType = BP.DA.DataType.AppString;
                mattrN.DefVal = "";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.DDL;
                mattrN.LGType = FieldTypeS.FK;

                mattrN.UIBindKey = "BP.Pub.NYs";
                mattrN.Insert();
            }

            if (attrs.Contains(RptAttrAttr.Field, "FK_Dept") == false)
            {
                /* �Ƿ��������״̬ */
                rptAttr.MyPK = this.No + "FK_Dept";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "FK_Dept";
                rptAttr.RefFieldName = this.ToE("Dept", "����"); // "����";

                rptAttr.Field = "FK_Dept";
                rptAttr.RefFieldName = this.ToE("Dept", "����"); // "����";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }

            mattrN = new MapAttr(this.No, "FK_Dept");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
               // mattrN.OID = 0;
                mattrN.Name = this.ToE("Dept", "����"); // "����";
                mattrN.KeyOfEn = "FK_Dept";
                mattrN.MyDataType = BP.DA.DataType.AppString;
                mattrN.DefVal = "";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.DDL;
                mattrN.LGType = FieldTypeS.FK;

                mattrN.UIBindKey = "BP.Port.Depts";
                mattrN.Insert();
            }

            if (attrs.Contains(RptAttrAttr.Field, "MyNum") == false)
            {
                /* �Ƿ��������״̬ */
                rptAttr.MyPK = this.No + "_MyNum";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "MyNum";
                rptAttr.RefFieldName = this.ToE("Num","����"); // "����";

                rptAttr.Field = "MyNum";
                rptAttr.RefFieldName = this.ToE("Num", "����");  //"����";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }
            MapAttr attrMyNum = new MapAttr(this.No, "MyNum");
            if (attrMyNum.MyPK.Length == 0)
            {
                attrMyNum.MyDataType = BP.DA.DataType.AppInt;
                attrMyNum.KeyOfEn = "MyNum";
                attrMyNum.Name = this.ToE("Num", "����"); // "����";
                attrMyNum.HisEditType = BP.En.EditType.UnDel;
                attrMyNum.DefVal = "0";
                attrMyNum.UIContralType = UIContralType.TB;
                attrMyNum.UIVisible = true;
                attrMyNum.LGType = FieldTypeS.Normal;
                attrMyNum.IDX = -1000;
                attrMyNum.Insert();
            }
            this.DoGenerView();
        }
        public string DoGenerView()
        {
            RptAttrs attrs = new RptAttrs(this.No);
            Nodes nds = new Nodes(this.FK_Flow);

            #region ɾ��Ŀ�����
            try
            {
                BP.DA.DBAccess.RunSQL("drop view " + this.No);
            }
            catch
            {
            }

            try
            {
                BP.DA.DBAccess.RunSQL("drop table " + this.No);
            }
            catch
            {
            }
            #endregion ɾ��Ŀ�����

            string nodeid = int.Parse(this.FK_Flow) + "01";

            try
            {
                BP.DA.DBAccess.RunSQL("drop view " + this.No);
            }
            catch
            {
                try
                {
                    BP.DA.DBAccess.RunSQL("drop table " + this.No);
                }
                catch
                {
                }
            }

            string sql = "CREATE  VIEW " + this.No + " AS SELECT @";
            foreach (RptAttr attr in attrs)
            {
                Node nd = nds.GetEntityByKey(attr.FK_Node) as Node;
                if (nd == null)
                {
                    attr.Delete(); //ɾ���Ѿ������ڵĽڵ㡣
                    continue;
                }


                sql += "," + attr.RefTable + "." + attr.RefField + " AS " + attr.Field;
            }
            attrs = new RptAttrs(this.No);

            sql += " FROM @";

            DataTable dt = DBAccess.RunSQLReturnTable(" SELECT DISTINCT RefTable, FK_Node FROM WF_RptAttr WHERE WF_RptAttr.FK_RPT='" + this.No + "'");
            foreach (DataRow dr in dt.Rows)
            {
                int mynodeid = int.Parse(dr[1].ToString());
                Node nd = nds.GetEntityByKey(mynodeid) as Node;

                
                    sql += "," + dr[0].ToString() + "";
            }
            sql += " WHERE @";

            foreach (DataRow dr in dt.Rows)
            {
                int mynodeid = int.Parse(dr[1].ToString());
                Node nd = nds.GetEntityByKey(mynodeid) as Node;

                
                    sql += "AND ND" + nodeid + ".OID=" + dr[0].ToString() + ".OID ";
            }
            sql = sql.Replace("@,", "");
            sql = sql.Replace("@AND", "");

            #region ������ݱ��Ƿ���ڣ������ھ�CREATE.
            Flow fl = new Flow(this.FK_Flow);
            nds = fl.HisNodes;
            foreach (Node nd in nds)
            {
                
                //nd.HisWork.CheckPhysicsTable();
            }
            #endregion ������ݱ��Ƿ���ڣ������ھ�CREATE.

            DBAccess.RunSQL(sql);
            BP.SystemConfig.DoClearCash();
            return null;
        }
        protected override void afterInsert()
        {
            this.DoInitAttrs();
            base.afterInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            MapData md = new MapData();
            md.No = this.No;
            md.RetrieveFromDBSources();
            md.Name = this.Name;
            md.Save();
            return base.beforeUpdateInsertAction();
        }
        protected override void afterDelete()
        {
            RptAttrs attrs = new RptAttrs();
            attrs.Delete(RptAttrAttr.FK_Rpt, this.No);
            

            MapData md = new MapData();
            md.No = this.No;
            md.Delete();
            try
            {
                BP.DA.DBAccess.RunSQL("drop view " + this.No);
            }
            catch
            {
                try
                {
                    BP.DA.DBAccess.RunSQL("drop table " + this.No);
                }
                catch
                {
                }
            }
            base.afterDelete();
        }
        #endregion
    }
    /// <summary>
    /// �ⲿ�������ü���
    /// </summary>
    public class WFRpts : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new WFRpt();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ⲿ�������ü���
        /// </summary>
        public WFRpts()
        {
        }
        /// <summary>
        /// �ⲿ�������ü���.
        /// </summary>
        /// <param name="FlowNo"></param>
        public WFRpts(string fk_flow)
        {
            this.Retrieve(NodeAttr.FK_Flow, fk_flow);
        }
        public WFRpts(int nodeid)
        {
            this.Retrieve(NodeAttr.NodeID, nodeid);
        }
        #endregion
    }
}

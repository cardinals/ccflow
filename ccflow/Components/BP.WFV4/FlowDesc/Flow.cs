using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using BP.DA;
using BP.Sys;
using BP.Port;
using BP.En;
using BP.Web;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Win32;  

namespace BP.WF
{
    /// <summary>
    /// ����Ӧ������
    /// </summary>
    public enum FlowAppType
    {
        /// <summary>
        /// ��ͨ��
        /// </summary>
        Normal,
        /// <summary>
        /// ������
        /// </summary>
        PRJ 
    }
    /// <summary>
    /// ���ͷ�ʽ
    /// </summary>
    public enum CCWay
    {
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        ByMsg,
        /// <summary>
        /// ����e-mail
        /// </summary>
        ByEmail,
        /// <summary>
        /// ���յ绰
        /// </summary>
        ByPhone,
        /// <summary>
        /// �������ݿ⹦��
        /// </summary>
        ByDBFunc
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum CCType
    {
        /// <summary>
        /// ������
        /// </summary>
        None,
        /// <summary>
        /// ����Ա
        /// </summary>
        AsEmps,
        /// <summary>
        /// ����λ
        /// </summary>
        AsStation,
        /// <summary>
        /// ���ڵ�
        /// </summary>
        AsNode,
        /// <summary>
        /// ������
        /// </summary>
        AsDept,
        /// <summary>
        /// ���ղ������λ
        /// </summary>
        AsDeptAndStation
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum XWType
    {
        /// <summary>
        /// ������
        /// </summary>
        Up,
        /// <summary>
        /// ƽ����
        /// </summary>
        Line,
        /// <summary>
        /// ������
        /// </summary>
        Down
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum DocType
    {
        /// <summary>
        /// ��ʽ��
        /// </summary>
        OfficialDoc,
        /// <summary>
        /// �㺯
        /// </summary>
        UnOfficialDoc,
        /// <summary>
        /// ����
        /// </summary>
        Etc
    }
    /// <summary>
    /// ���̱�����
    /// </summary>
    public enum FlowSheetType
    {
        /// <summary>
        /// ������
        /// </summary>
        SheetFlow,
        /// <summary>
        /// ��������
        /// </summary>
        DocFlow
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum FlowType
    {
        /// <summary>
        /// ƽ������
        /// </summary>
        Panel,
        /// <summary>
        /// �ֺ���
        /// </summary>
        FHL
    }
    /// <summary>
    /// ����״̬
    /// </summary>
    public enum WFState
    {
        /// <summary>
        /// ����״̬
        /// </summary>
        Runing,
        /// <summary>
        /// ���״̬
        /// </summary>
        Complete,
        /// <summary>
        /// ǿ����ֹ
        /// </summary>
        Stop,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete
    }
    /// <summary>
    /// ������������
    /// </summary>
    public enum FlowRunWay
    {
        /// <summary>
        /// �ֹ�����
        /// </summary>
        HandWork,
        /// <summary>
        /// �����
        /// </summary>
        ByYear,
        /// <summary>
        /// �����·�
        /// </summary>
        ByMonth,
        /// <summary>
        /// ������
        /// </summary>
        ByWeek,
        /// <summary>
        /// ������
        /// </summary>
        ByDay,
        /// <summary>
        /// ��Сʱ
        /// </summary>
        ByHours
    }
    /// <summary>
    /// ��������
    /// </summary>
    public class FlowAttr : EntityNoNameAttr
    {
        #region base attrs
        /// <summary>
        /// CCType
        /// </summary>
        public const string CCType = "CCType";
        /// <summary>
        /// ���ͷ�ʽ
        /// </summary>
        public const string CCWay = "CCWay";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// ���������ڡ�
        /// </summary>
        public const string CreateDate = "CreateDate";
        /// <summary>
        /// BillTable
        /// </summary>
        public const string BillTable = "BillTable";
        /// <summary>
        /// ��ʼ�����ڵ�����
        /// </summary>
        public const string StartNodeType = "StartNodeType";
        /// <summary>
        /// StartNodeID
        /// </summary>
        public const string StartNodeID = "StartNodeID";
        /// <summary>
        /// �ܲ�������Num���ˡ�
        /// </summary>
        public const string IsCanNumCheck = "IsCanNumCheck";
        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        public const string IsFJ = "IsFJ";
        #endregion base attrs

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsOK = "IsOK";
        public const string CCStas = "CCStas";
        public const string IsCCAll = "IsCCAll";
        public const string Note = "Note";
        public const string RunSQL = "RunSQL";
        public const string StartListUrl = "StartListUrl";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FlowType = "FlowType";
        /// <summary>
        /// ƽ������
        /// </summary>
        public const string AvgDay = "AvgDay";
        public const string FlowSheetType = "FlowSheetType";
        /// <summary>
        /// �ĵ�����
        /// </summary>
        public const string DocType = "DocType";
        /// <summary>
        /// ��������
        /// </summary>
        public const string XWType = "XWType";
        /// <summary>
        /// ������������
        /// </summary>
        public const string FlowRunWay = "FlowRunWay";
        /// <summary>
        /// ���е�����
        /// </summary>
        public const string FlowRunObj = "FlowRunObj";
        /// <summary>
        /// �Ƿ���Bill
        /// </summary>
        public const string NumOfBill = "NumOfBill";
        /// <summary>
        /// 
        /// </summary>
        public const string NumOfDtl = "NumOfDtl";
        /// <summary>
        /// �Ƿ����������
        /// </summary>
        public const string IsCanStart = "IsCanStart";
        /// <summary>
        /// �켣�ֶ�
        /// </summary>
        public const string AppType = "AppType";
    }
    /// <summary>
    /// ����
    /// ��¼�����̵���Ϣ��
    /// ���̵ı�ţ����ƣ�����ʱ�䣮
    /// </summary>
    public class Flow : EntityNoName
    {
        #region ҵ����
        /// <summary>
        /// �켣�ֶ�
        /// </summary>
        public string AppType
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.AppType);
            }
            set
            {
                this.SetValByKey(FlowAttr.AppType, value);
            }
        }
        /// <summary>
        /// ����һ���¹���
        /// </summary>
        /// <returns></returns>
        public Work NewWork()
        {
            BP.WF.Node nd = new BP.WF.Node(this.StartNodeID);

            //�Ӳݸ��￴���Ƿ����¹�����
            StartWork wk = (StartWork)nd.HisWork;
            int num = wk.Retrieve(StartWorkAttr.NodeState, 0,
                StartWorkAttr.Rec, WebUser.No);

            try
            {
                if (num == 0)
                {
                    wk.Rec = WebUser.No;
                    wk.SetValByKey("RecText", WebUser.Name);
                    wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
                    wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
                    wk.WFState = 0;
                    wk.NodeState = 0;
                    wk.OID = DBAccess.GenerOID("WID");
                    wk.DirectInsert();
                }

                wk.Rec = WebUser.No;
                wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
                wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
                wk.WFState = 0;
                wk.NodeState = 0;
                wk.FK_Dept = WebUser.FK_Dept;
                wk.SetValByKey("FK_DeptName", WebUser.FK_DeptName);
                wk.SetValByKey("FK_DeptText", WebUser.FK_DeptName);
                wk.FID = 0;
                wk.SetValByKey("RecText", WebUser.Name);

                string msg = "";
                if (WebUser.SysLang == "CH")
                    msg = WebUser.Name + "��" + DateTime.Now.ToString("MM��dd��HH:mm") + "����";
                else
                    msg = WebUser.Name + " Date " + DateTime.Now.ToString("MM-dd HH:mm") + " " + this.ToE("Start", "����");

                string title = wk.GetValStringByKey("Title");
                if (string.IsNullOrEmpty(title))
                    wk.Title = msg;
                else if (title.Contains("��") == true)
                    wk.Title = msg;
            }
            catch (Exception ex)
            {
                //if (ex.Message.Contains("key=["))
            }
            return wk;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="fk_node"></param>
        /// <returns></returns>
        public Work GenerWork(Int64 workid, Node nd)
        {
            Work wk = nd.HisWork;
            wk.OID = workid;
            if (wk.RetrieveFromDBSources() == 0)
                throw new Exception("@����[" + nd.No  + " : " + wk.EnDesc + "],����WorkID=" + workid + " ��ʧ,����ϵ����Ա��");

            // ���õ�ǰ����Ա�Ѽ�¼�ˡ�
            wk.Rec = WebUser.No;
            wk.RecText = WebUser.Name;
            wk.Rec = WebUser.No;
            wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
            wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
     //       wk.NodeState = 0; �����ڵ�״̬�ſ���ʾ����
            wk.SetValByKey("FK_Dept", WebUser.FK_Dept);
            wk.SetValByKey("FK_DeptName", WebUser.FK_DeptName);
            wk.SetValByKey("FK_DeptText", WebUser.FK_DeptName);
            wk.FID = 0;
            wk.SetValByKey("RecText", WebUser.Name);

            if (nd.IsStartNode)
            {
                string msg = "";
                if (WebUser.SysLang == "CH")
                    msg = WebUser.Name + "��" + DateTime.Now.ToString("MM��dd��HH:mm") + "����";
                else
                    msg = WebUser.Name + " Date " + DateTime.Now.ToString("MM-dd HH:mm") + " " + this.ToE("Start", "����");

                wk.SetValByKey("Title", msg);
            }
            return wk;
        }
        #endregion ҵ����

        public string Tag = null;
        /// <summary>
        /// ��������
        /// </summary>
        public FlowRunWay HisFlowRunWay
        {
            get
            {
                return (FlowRunWay)this.GetValIntByKey(FlowAttr.FlowRunWay);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowRunWay, (int)value);
            }
        }
        /// <summary>
        /// ���ж���
        /// </summary>
        public string FlowRunObj
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.FlowRunObj);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowRunObj, value);
            }
        }
        public string StartListUrl
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.StartListUrl);
            }
            set
            {
                this.SetValByKey(FlowAttr.StartListUrl, value);
            }
        }
        /// <summary>
        /// ���̱�����
        /// </summary>
        public FlowSheetType HisFlowSheetType
        {
            get
            {
                return (FlowSheetType)this.GetValIntByKey(FlowAttr.FlowSheetType);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowSheetType, (int)value);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// �޲�����������ͼ
        /// </summary>
        /// <returns></returns>
        public string RepareV_FlowData_View()
        {
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            if (fls.Count == 1)
                return null;

            string sql = "";
            try
            {
                sql = "DROP VIEW V_FlowData ";
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }

            string flowDataViewExtFields = BP.SystemConfig.AppSettings["FlowDataViewExtFields"];
            if (flowDataViewExtFields == null)
                flowDataViewExtFields = "";

            sql = "CREATE VIEW V_FlowData  ";
            sql += "\t\n /*  WorkFlow Data " + DateTime.Now.ToString("yyyy-MM-dd") + " */ ";
            sql += " AS ";
            foreach (Flow fl in fls)
            {
                string mysql = "\t\n SELECT " + flowDataViewExtFields + " '" + fl.FK_FlowSort + "' AS FK_FlowSort,'" + fl.No + "' AS FK_Flow,OID,FID,Title,BillNo,WFState,CDT,Emps,FK_Dept,FK_NY,FlowDaySpan,FlowEmps,FlowEnder,FlowEnderRDT,FlowStartRDT FROM ND" + int.Parse(fl.No) + "Rpt";
                try
                {
                    DBAccess.RunSQLReturnTable(mysql);
                }
                catch
                {
                    continue;
                    fl.DoCheck();
                }
                sql += mysql;
                sql += "\t\n UNION ";
            }
            sql = sql.Substring(0, sql.Length - 6);
            DBAccess.RunSQL(sql);
            return null;
        }
        /// <summary>
        /// У������
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            DBAccess.RunSQL("UPDATE WF_Node SET FlowName = (SELECT Name FROM WF_Flow WHERE NO=WF_Node.FK_Flow)");
            this.NumOfBill = DBAccess.RunSQLReturnValInt("SELECT count(*) FROM WF_BillTemplate WHERE NodeID IN (select NodeID from WF_Flow where no='" + this.No + "')");
            this.NumOfDtl = DBAccess.RunSQLReturnValInt("SELECT count(*) FROM Sys_MapDtl WHERE FK_MapData='ND" + int.Parse(this.No) + "Rpt'");

            this.DirectUpdate();

            string msg = "<font color=blue>" + this.ToE("About", "����") + "��" + this.Name + " �� " + this.ToE("FlowCheckInfo", "���̼����Ϣ") + "</font><hr>";
            Nodes nds = new Nodes(this.No);
            Emps emps = new Emps();

            string rpt = "<html>\r\n<title>" + this.Name + "-" + this.ToE("DesignRpt", "������ϱ���") + "</title>";
            rpt += "\r\n<body>\r\n<table width='70%' align=center border=1>\r\n<tr>\r\n<td>";
            rpt += "\r\n<div aligen=center><h1><b>" + this.Name + " - " + this.ToE("FlowDRpt", "��������ĵ�") + "</b></h1></div>";
            rpt += "<b>" + this.ToE("FlowDesc", "�������") + ":</b><br>";
            rpt += "&nbsp;&nbsp;" + this.Note;
            rpt += "\r\n<br><b>" + this.ToE("Info", "��Ϣ") + ":</b><br>";

            BillTemplates bks = new BillTemplates(this.No);

            rpt += "&nbsp;&nbsp;" + this.ToE("Step", "����") + ":" + nds.Count + "��" + this.ToE("Bill", "����") + ":" + bks.Count + "����";

            #region �Խڵ���м��
            foreach (Node nd in nds)
            {
                DBAccess.RunSQL("UPDATE Sys_MapData SET Name='" + nd.Name + "' WHERE No='ND" + nd.NodeID + "'");
                try
                {
                    nd.HisWork.CheckPhysicsTable();
                }
                catch (Exception ex)
                {
                    rpt += "@" + nd.Name + " , �ڵ�����NodeWorkTypeText=" + nd.NodeWorkTypeText + "���ִ���.@err=" + ex.Message;
                }

                rpt += "<hr><b>" + this.ToEP1("NStep", "@��{0}��", nd.Step.ToString()) + "��" + nd.Name + "��</b><br>";
                rpt += this.ToE("Info", "��Ϣ") + "��";

                BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs("ND" + nd.NodeID);
                foreach (BP.Sys.MapAttr attr in attrs)
                {
                    rpt += attr.KeyOfEn + " " + attr.Name + "��";
                }
                // ��ϸ���顣
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                foreach (Sys.MapDtl dtl in dtls)
                {
                    dtl.HisGEDtl.CheckPhysicsTable();

                    rpt += "<br>" + this.ToE("Dtl", "��ϸ") + "��" + dtl.Name;
                    BP.Sys.MapAttrs attrs1 = new BP.Sys.MapAttrs(dtl.No);
                    foreach (BP.Sys.MapAttr attr in attrs1)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "��";
                    }
                }

                rpt += "<br>" + this.ToE("Station", "��λ") + "��";

                // ��λ�Ƿ�������ȷ��
                msg += "<b>��" + nd.Name + "����</b>" + this.ToE("NodeWorkType", "�ڵ�����") + "��" + nd.HisNodeWorkType + "<hr>";

                if (nd.HisStations.Count == 0)
                {
                    string infoAccept = "";
                    if (nd.HisDeliveryWay == DeliveryWay.BySelected)
                    {
                        if (nd.HisToNodes.Count == 0)
                            infoAccept = "<font color=red>@��ִ�е���Ա���û��ڱ����,��Աѡ������ѡ�������.���ǵ�ǰ�ڵ��ж����֧��������ƴ���</font>";
                        else
                            infoAccept = "@��ִ�е���Ա���û��ڱ����,��Աѡ������ѡ�������.";
                    }

                    if (nd.HisNodeEmps.Count != 0)
                    {
                        infoAccept = "@�ڵ���ָ�������¿�ִ�е�������Ա.";
                        foreach (NodeEmp ne in nd.HisNodeEmps)
                            infoAccept += "@" + ne.FK_Emp + "," + ne.FK_EmpT;
                    }

                    if (nd.HisWork.EnMap.Attrs.Contains("FK_Emp") == false)
                        infoAccept = "��ִ�е���Ա���û��ڱ���ѡ������ġ�";

                    if (infoAccept == "")
                        msg += "<font color=red>" + this.ToE("Error", "����") + "��" + nd.Name + " " + this.ToE("NoSetSta", "û�����ø�λ") + "��</font>";
                    else
                        msg += infoAccept;
                }
                else
                {
                    msg += this.ToE("Station", "��λ") + "��";
                    foreach (BP.Port.Station st in nd.HisStations)
                    {
                        msg += st.Name + "��";
                        rpt += st.Name + "��";
                    }

                    emps.RetrieveInSQL("select fk_emp from Port_Empstation WHERE fk_station in (select fk_station from WF_NodeStation WHERE FK_Node=" + nd.NodeID + " )");
                    if (emps.Count == 0)
                    {
                        msg += "<font color=red>" + this.ToE("F0", "��λ��Ա���ô�����Ȼ�������˸�λ�����Ǹ�λ��û�������Աִ�У����̵��˲��費���������У������û�ά����ά����λ��") + "</font>";
                    }
                    else
                    {
                        msg += this.ToE("F1", "���ڵ��ִ�еĹ�����Ա����");
                        foreach (Emp emp in emps)
                        {
                            msg += emp.No + emp.Name + "��";
                        }
                    }
                }
                msg += "<br>";
                //�Ե��ݽ��м�顣
                BillTemplates Bills = nd.HisBillTemplates;
                if (Bills.Count == 0)
                {
                    msg += "";
                }
                else
                {
                    msg += this.ToE("Bill", "����");
                    rpt += "<br>" + this.ToE("Bill", "����") + "��";
                    foreach (BillTemplate Bill in Bills)
                    {
                        msg += Bill.Name + "��";
                        rpt += Bill.Name + "��";
                    }
                }
                msg += "<br>";
                //�ⲿ������ýӿڼ��
                FAppSets sets = new FAppSets(nd.NodeID);
                if (sets.Count == 0)
                {
                    //msg += " ��";
                }
                else
                {
                    msg += ":";
                    foreach (FAppSet set in sets)
                    {
                        msg += set.Name + "," + this.ToE("DoWhat", "ִ������") + ":" + set.DoWhat + " ��";
                    }
                }
                msg += "<br>";
                // �ڵ���������Ķ���.
                Conds conds = new Conds(CondType.Node, nd.NodeID, 1);
                if (conds.Count == 0)
                {
                    //msg += " ��";
                }
                else
                {
                    msg += " " + this.ToE("DirCond", "��������") + ":";
                    rpt += "<br>" + this.ToE("DirCond", "��������") + "��";
                    foreach (Cond cond in conds)
                    {
                        Node ndOfCond = new Node();
                        ndOfCond.NodeID = ndOfCond.NodeID;
                        if (ndOfCond.RetrieveFromDBSources() == 0)
                        {
                            //msg += "<font color=red>�趨�ķ��������Ľڵ��Ѿ���ɾ���ˣ�����ϵͳ�Զ�ɾ�������������</font>";
                            //cond.Delete();
                            continue;
                        }

                        try
                        {

                            if (cond.AttrKey.Length < 2)
                                continue;

                            if (ndOfCond.HisWork.EnMap.Attrs.Contains(cond.AttrKey) == false)
                                throw new Exception("����:" + cond.AttrKey + " , " + cond.AttrName + " �����ڡ�");
                        }
                        catch (Exception ex)
                        {
                            msg += "<font color=red>" + ex.Message + "</font>";
                            ndOfCond.Delete();
                        }

                        msg += cond.AttrKey + cond.AttrName + cond.OperatorValue + "��";
                        rpt += cond.AttrKey + cond.AttrName + cond.OperatorValue + "��";
                    }
                }
                msg += "<br>";

                #region ����Ƿ��з�������
                if (nd.HisToNodes.Count >= 2 && nd.HisRunModel== RunModel.Ordinary)
                {
                    foreach (Node cND in nd.HisToNodes)
                    {
                        string sql = "SELECT COUNT(*) FROM WF_Cond WHERE NodeID='" + nd.NodeID + "' AND ToNodeID='" + cND.NodeID + "'";
                        if (DBAccess.RunSQLReturnValInt(sql) == 0)
                        {
                            msg += "<br><font color=red>@�ӽڵ�{" + nd.Name + "}���ڵ�{" + cND.Name + "},û�����÷������������ڷ������ϵ��Ҽ����÷���������</font>";
                        }
                    }
                }
                #endregion ����Ƿ��з�������
            }
            #endregion

            msg += "<br><a href='../../DataUser/FlowDesc/" + this.Name + ".htm' target=_s>" + this.ToE("DesignRpt", "��Ʊ���") + "</a>";
            msg += "<hr><b> </b> &nbsp; <br>" + DataType.CurrentDataTime + "<br><br><br>";
            rpt += "\r\n</td></tr>\r\n</table>\r\n</body>\r\n</html>";

            string path = BP.SystemConfig.PathOfDataUser + "\\FlowDesc\\" + this.Name + "\\";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            DataType.SaveAsFile(path + this.Name + "_��Ʊ���.htm", rpt);

            msg += "<hr>" + this.ToE("CheckBaseData", "��ʼ�����������Ƿ�����") + "<hr>";

            emps = new Emps();
            emps.RetrieveAllFromDBSource();
            //foreach (Emp emp in emps)
            //{
            //    Dept dept = new Dept();
            //    dept.No = emp.FK_Dept;
            //    if (dept.IsExits == false)
            //        msg += "@��Ա:" + emp.Name + "�����ű��{" + dept.Name + "}����ȷ.";
            //    EmpStations ess = new EmpStations();
            //    ess.Retrieve(EmpStationAttr.FK_Emp, emp.No);
            //    if (ess.Count == 0)
            //        msg += "@��Ա:" + emp.No + "," + emp.Name + ",û�й�����λ��";
            //    EmpDepts eds = new EmpDepts();
            //    eds.Retrieve(EmpStationAttr.FK_Emp, emp.No);
            //    if (eds.Count == 0)
            //        msg += "@��Ա:" + emp.No + "," + emp.Name + ",û�й������š�";
            //}
            //msg = msg.Replace("@", "<BR>@");
            //string emp = this.ToE("Emp", "��Ա");
            //string dept = this.ToE("Dept", "���ű��");
            //string error = this.ToE("Dept", "����ȷ");

            CheckRpt();

            ////Depts depts =new Depts();
            ////depts.RetrieveAll();
            //string sql = "SELECT * FROM Port_Emp WHERE FK_DEPT NOT IN (SELECT NO FROM PORT_DEPT)";
            DBAccess.RunSQL("DELETE FROM WF_NodeEmp WHERE FK_EMP  not in (select no from port_emp)");
            DBAccess.RunSQL("DELETE FROM WF_Emp WHERE NO not in (select no from port_emp )");
            try
            {
#warning ���´���
                DBAccess.RunSQL("UPDATE WF_Emp set Name=(select name from port_emp where port_emp.no=wf_emp.no),FK_Dept=(select fk_dept from port_emp where port_emp.no=wf_emp.no)");
            }
            catch
            {
            }

            Node.CheckFlow(this);
            return msg;
        }

        #region ��������ģ�塣
        public void GenerHisHtmlDocRpt()
        {
            string path = SystemConfig.PathOfWorkDir + "\\VisualFlow\\DataUser\\FlowDesc\\" + this.No + "." + this.Name + "\\";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            Nodes nds = this.HisNodes;

            string msg = "";

            #region �������б�
            //����һ���б�.
            msg = "<html>";
            msg += "\r\n<title>" + this.Name + "��ģ��</title>";
            msg += "\r\n<META http-equiv=Content-Type content='text/html; charset=utf-8'>";
            msg += "\r\n<body>";
            msg += "\r\n<h1>" + this.Name + "</h1>";

            msg += "\r\n<h3>�۳ҹ���������,�������̹���ϵͳ�Զ����ɣ������뵽<a href='http://doc.ccFlow.org' target=_blank >����ģ�彻������������ء�</a>��ϵ:QQ:793719823,Tel:18660153393</h3>";
            msg += "\r\n<hr>";

            msg += "\r\n<h3><a href='" + this.Name + ".xml' target=_blank>ccflow��ʶ���(" + this.Name + ")����ģ��(xml��ʽ)</a> </h3>";

            //  <a href='" + this.Name + "_��Ʊ���.htm' target=_blank>" + this.Name + "��Ʊ���</a> - 
            // DataType.SaveAsFile(path + this.Name + "_��Ʊ���.htm", rpt);

            msg += "\r\n<table border=1>";
            foreach (Node nd in nds)
            {
                msg += "\r\n<tr>";
                msg += "\r\n<TD><a href='" + nd.FlowName + "_" + nd.Name + ".doc' target=_blank >" + nd.Name + "</a></TD>";
                msg += "\r\n<TD><a href='" + nd.FlowName + "_" + nd.Name + ".pdf' target=_blank >" + nd.Name + "</a></TD>";
                msg += "\r\n<TD><a href='" + nd.FlowName + "_" + nd.Name + ".htm' target=_blank >" + nd.Name + "</a></TD>";
                msg += "\r\n</tr>";
            }
            msg += "\r\n</table>";

            msg += "\r\n<img src='./../" + this.No + ".gif' alt='" + this.Name + "����ͼ' border=0 />";

            msg += "\r\n<hr>";
            msg += "\r\n<h3>�ر�˵�������������б�ģ����ccflow��ccflow�Ŀͻ�����������Ͷ����ã�����ת�ء����á�ʹ����ע����������������ccflowͬ�⡣</h3>";

            msg += "\r\n<ul>";
            msg += "\r\n<li><a href='http://ccFlow.org' target=_blank >�۳ҹ���������ٷ���վ http://ccFlow.org </a></li>";
            msg += "\r\n<li><a href='http://doc.ccFlow.org' target=_blank >��׼����ģ���� http://doc.ccFlow.org </a></li>";
            msg += "\r\n</ul>";

            msg += "\r\n</body>";
            msg += "\r\n</html>";

            DataType.SaveAsFile(path + "index.htm", msg);
            #endregion �������б�

            foreach (Node nd in nds)
            {
                Work wk = nd.HisWork;
                this.GenerWorkTempleteDoc(nd.HisWork, "ND" + nd.NodeID, nd); // ����word, pdf..
                msg = "<html>";
                msg += "\r\n<title>" + this.Name + ",��ģ��</Title>";
                msg += "\r\n<META http-equiv=Content-Type content='text/html; charset=utf-8'>";
                msg += "\r\n<body>";

                msg += "\r\n<h1>" + this.Name + " - " + nd.Name + "</h1>";

                msg += "\r\n<h3>���أ�<a href='Index.htm' >" + this.Name + "</a>����ģ����ccflow�Զ����ɣ�����ĵ���ģ�� <a href='http://doc.ccFlow.org' target=_blank >�۳�����ģ�����������</a>��</h3>";
                msg += "\r\n<hr>";

                msg += this.GenerWorkTempleteHtml(nd.HisWork, "ND" + nd.NodeID);

                msg += "\r\n<hr>";
                msg += "\r\n<h3>�ر�˵�������������б�ģ����ccflow��ccflow�Ŀͻ�����������Ͷ����ã�����ת�ء����á�ʹ����ע����������������ccflowͬ�⡣</h3>";
                msg += "\r\n<ul>";
                msg += "\r\n<li><a href='http://ccFlow.org' target=_blank >�۳ҹ���������ٷ���վ http://ccFlow.org </a></li>";
                msg += "\r\n<li><a href='http://doc.ccFlow.org' target=_blank >��׼����ģ���� http://doc.ccFlow.org </a></li>";
                msg += "\r\n</ul>";
                msg += "\r\n</body>";
                msg += "\r\n</html>";
                DataType.SaveAsFile(path + this.Name + "_" + nd.Name + ".htm", msg);
            }
        }

        public string GenerFlowXmlTemplete()
        {
            string path = SystemConfig.PathOfWorkDir + @"\VisualFlow\DataUser\FlowDesc\" + this.No + "." + this.Name + "\\";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            DataSet ds = new DataSet();

            // ��������Ϣ����һ���ļ���
            Flows fls = new Flows();
            fls.AddEntity(this);

            // fls.SaveToXml(path + "Flow.xml");
            ds.Tables.Add(fls.ToDataTableField("Flow"));

            // �ڵ���Ϣ
            Nodes nds = this.HisNodes;
            //   nds.SaveToXml(path + "Nodes.xml");
            ds.Tables.Add(nds.ToDataTableField("Nodes"));

            // ������Ϣ
            BillTemplates tmps = new BillTemplates(this.No);
            //tmps.SaveToXml(path + "BillTemplates.xml");
            string pks = "";
            foreach (BillTemplate tmp in tmps)
            {
                try
                {
                    System.IO.File.Copy(SystemConfig.PathOfDataUser + @"\CyclostyleFile\" + tmp.No + ".rtf", path + "\\" + tmp.No + ".rtf", true);
                }
                catch
                {
                    pks += "@" + tmp.PKVal;
                    tmp.Delete();
                }
            }
            tmps.Remove(pks);
            ds.Tables.Add(tmps.ToDataTableField("BillTemplates"));

            // ������Ϣ
            Conds cds = new Conds(this.No);
            // cds.SaveToXml(path + "Conds.xml");
            ds.Tables.Add(cds.ToDataTableField("Conds"));

            // ����
            string sqlin = "SELECT NodeID FROM WF_Node WHERE fk_flow='" + this.No + "'";
            string sql = "select * from WF_Direction where Node IN (" + sqlin + ") OR ToNode In (" + sqlin + ")";

            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Directions";
            //   ds1.WriteXml(path + "Directions.xml");
            ds.Tables.Add(dt);

            // Ӧ������ FAppSet
            FAppSets sets = new FAppSets(this.No);
            //sets.SaveToXml(path + "FAppSets.xml");
            ds.Tables.Add(sets.ToDataTableField("FAppSets"));


            // ���̷�������͵���λ 
            FlowStations fstas = new FlowStations(this.No);
            // fstas.SaveToXml(path + "FlowStations.xml");
            ds.Tables.Add(fstas.ToDataTableField("FlowStations"));

            // ���̱�ǩ.
            LabNotes labs = new LabNotes(this.No);
            //  labs.SaveToXml(path + "LabNotes.xml");
            ds.Tables.Add(labs.ToDataTableField("LabNotes"));

            // ��Ϣ����.
            Listens lts = new Listens(this.No);
            ds.Tables.Add(lts.ToDataTableField("Listens"));


            // �ڵ��벿�š�
            sql = "SELECT * FROM WF_NodeDept where FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "NodeDept";
            ds.Tables.Add(dt);

            // �ڵ����λȨ�ޡ�
            sql = "SELECT * FROM WF_NodeStation where FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "NodeStations";
            ds.Tables.Add(dt);

            // �ڵ�����Ա��
            sql = "SELECT * FROM WF_NodeEmp where FK_Node IN (" + sqlin + ")";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "WF_NodeEmps";
            ds.Tables.Add(dt);

            //ds = DBAccess.RunSQLReturnDataSet(sql);
            //ds.WriteXml(path + "WF_NodeEmps.xml");

            // ���̱���
            WFRpts rpts = new WFRpts(this.No);
            // rpts.SaveToXml(path + "WFRpts.xml");
            ds.Tables.Add(rpts.ToDataTableField("WFRpts"));

            // ���̱�������
            RptAttrs rptAttrs = new RptAttrs();
            rptAttrs.RetrieveAll();
            ds.Tables.Add(rptAttrs.ToDataTableField("RptAttrs"));


            // ���̱������Ȩ�ޡ�
            RptStations rptStations = new RptStations(this.No);
            rptStations.RetrieveAll();
            //  rptStations.SaveToXml(path + "RptStations.xml");
            ds.Tables.Add(rptStations.ToDataTableField("RptStations"));


            // ���̱�����Ա����Ȩ�ޡ�
            RptEmps rptEmps = new RptEmps(this.No);
            rptEmps.RetrieveAll();
            // rptEmps.SaveToXml(path + "RptEmps.xml");
            ds.Tables.Add(rptEmps.ToDataTableField("RptEmps"));


            int flowID = int.Parse(this.No);
            // Sys_MapData
            sql = "SELECT * FROM Sys_MapData WHERE No LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapData";
            ds.Tables.Add(dt);

            //ds = DBAccess.RunSQLReturnDataSet(sql);
            //ds.WriteXml(path + "Sys_MapData.xml");

            // Sys_MapAttr.
            sql = "SELECT * FROM Sys_MapAttr WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapAttr";
            ds.Tables.Add(dt);


            // Sys_EnumMain
            sql = "SELECT * FROM Sys_EnumMain WHERE No IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%' )";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_EnumMain";
            ds.Tables.Add(dt);

            // Sys_MapDtl
            sql = "SELECT * FROM Sys_MapDtl WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapDtl";
            ds.Tables.Add(dt);


            // Sys_MapExt
            sql = "SELECT * FROM Sys_MapExt WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapExt";
            ds.Tables.Add(dt);


            // Sys_Enum
            sql = "SELECT * from Sys_Enum WHERE EnumKey IN ( SELECT No FROM Sys_EnumMain where NO IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%' ) )";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_Enum";
            ds.Tables.Add(dt);


            // Sys_GroupField
            sql = "SELECT * FROM Sys_GroupField WHERE EnName LIKE 'ND" + flowID + "%' ";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_GroupField";
            ds.Tables.Add(dt);

            // Sys_MapFrame
            sql = "SELECT * FROM Sys_MapFrame WHERE FK_MapData LIKE 'ND" + flowID + "%' ";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapFrame";
            ds.Tables.Add(dt);

            // Sys_MapM2M
            sql = "SELECT * FROM Sys_MapM2M WHERE FK_MapData LIKE 'ND" + flowID + "%' ";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_MapM2M";
            ds.Tables.Add(dt);

            // Sys_FrmLine.
            sql = "SELECT * FROM Sys_FrmLine WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmLine";
            ds.Tables.Add(dt);

            // Sys_FrmLab.
            sql = "SELECT * FROM Sys_FrmLab WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmLab";
            ds.Tables.Add(dt);

            // Sys_FrmLink.
            sql = "SELECT * FROM Sys_FrmLink WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmLink";
            ds.Tables.Add(dt);

            // Sys_FrmRB.
            sql = "SELECT * FROM Sys_FrmRB WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmRB";
            ds.Tables.Add(dt);

            // Sys_FrmImgAth.
            sql = "SELECT * FROM Sys_FrmImgAth WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmImgAth";
            ds.Tables.Add(dt);

            // Sys_FrmImg.
            sql = "SELECT * FROM Sys_FrmImg WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmImg";
            ds.Tables.Add(dt);

            // Sys_FrmAttachment.
            sql = "SELECT * FROM Sys_FrmAttachment WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            dt = DBAccess.RunSQLReturnTable(sql);
            dt.TableName = "Sys_FrmAttachment";
            ds.Tables.Add(dt);


            ds.WriteXml(path + this.Name + ".xml");
            return path;
        }
        /// <summary>
        /// ���� word �ļ�
        /// </summary>
        private void GenerWorkTempleteDoc(Entity en, string enName, Node nd)
        {
            string tempFilePath =  SystemConfig.PathOfWorkDir + @"\VisualFlow\DataUser\FlowDesc\" + this.No + "." + this.Name + "\\";
            if (System.IO.Directory.Exists(tempFilePath) == false)
                System.IO.Directory.CreateDirectory(tempFilePath);
            tempFilePath = tempFilePath + nd.FlowName + "_" + nd.Name + ".doc";
            if (File.Exists(tempFilePath) == true)
                return;

            BP.WF.Glo.KillProcess("WINWORD.EXE");
            try
            {
                RegistryKey delKey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Shared Tools\Text Converters\Import\",
                    true);
                delKey.DeleteValue("MSWord6.wpc");
                delKey.Close();
            }
            catch
            {
            }

            GroupField currGF = new GroupField();
            MapAttrs mattrs = new MapAttrs(enName);
            GroupFields gfs = new GroupFields(enName);
            MapDtls dtls = new MapDtls(enName);
            foreach (MapDtl dtl in dtls)
                dtl.IsUse = false;

            // ���������Ԫ���������
            int rowNum = 0;
            foreach (GroupField gf in gfs)
            {
                rowNum++;
                bool isLeft = true;
                foreach (MapAttr attr in mattrs)
                {
                    if (attr.UIVisible == false)
                        continue;

                    if (attr.GroupID != gf.OID)
                        continue;

                    if (attr.UIIsLine)
                    {
                        rowNum++;
                        isLeft = true;
                        continue;
                    }

                    if (isLeft == false)
                        rowNum++;
                    isLeft = !isLeft;
                }
            }

            rowNum = rowNum + 2 + dtls.Count;


            // ����Word�ĵ�
            string CheckedInfo = "";
            string message = "";
            Object Nothing = System.Reflection.Missing.Value;
            object filename = tempFilePath;

            Word.Application WordApp = new Word.ApplicationClass();
            Word.Document WordDoc = WordApp.Documents.Add(ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing);

            try
            {
                WordApp.ActiveWindow.View.Type = Word.WdViewType.wdOutlineView;
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;

                #region ����ҳü
                // ���ҳü ����ͼƬ
                string pict = SystemConfig.PathOfDataUser + "log.jpg"; // ͼƬ����·��
                if (System.IO.File.Exists(pict))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(pict);
                    object LinkToFile = false;
                    object SaveWithDocument = true;
                    object Anchor = WordDoc.Application.Selection.Range;

                    WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(pict, ref  LinkToFile,
                        ref  SaveWithDocument, ref  Anchor);
                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = img.Width; // ͼƬ���
                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = img.Height; // ͼƬ�߶�
                }
                WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("[�۳�ҵ�����̹���ϵͳ http://ccFlow.org ] - [" + nd.FlowName + "-" + nd.Name + "ģ��]");
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight; // �����Ҷ���
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument; // ����ҳü����
                WordApp.Selection.ParagraphFormat.LineSpacing = 15f; // �����ĵ����м��
                #endregion

                // �ƶ����㲢����
                object count = 14;
                object WdLine = Word.WdUnits.wdLine; // ��һ��;
                WordApp.Selection.MoveDown(ref  WdLine, ref  count, ref  Nothing); // �ƶ�����
                WordApp.Selection.TypeParagraph(); // �������

                // �ĵ��д������
                Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, rowNum, 4, ref  Nothing, ref  Nothing);

                // ���ñ����ʽ
                newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                newTable.Columns[1].Width = 100f;
                newTable.Columns[2].Width = 100f;
                newTable.Columns[3].Width = 100f;
                newTable.Columns[4].Width = 100f;

                // ���������
                newTable.Cell(1, 1).Range.Text = nd.Name + "��";
                newTable.Cell(1, 1).Range.Bold = 2; // ���õ�Ԫ��������Ϊ����

                // �ϲ���Ԫ��
                newTable.Cell(1, 1).Merge(newTable.Cell(1, 4));
                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; // ��ֱ����
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter; // ˮƽ����

                int groupIdx = 1;
                foreach (GroupField gf in gfs)
                {
                    groupIdx++;
                    // ���������
                    newTable.Cell(groupIdx, 1).Range.Text = gf.Lab;
                    newTable.Cell(groupIdx, 1).Range.Font.Color = Word.WdColor.wdColorDarkBlue; // ���õ�Ԫ����������ɫ
                    newTable.Cell(groupIdx, 1).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray25;
                    // �ϲ���Ԫ��
                    newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
                    WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    groupIdx++;

                    bool isLeft = true;
                    bool isColumns2 = false;
                    int currColumnIndex = 0;
                    foreach (MapAttr attr in mattrs)
                    {
                        if (attr.UIVisible == false)
                            continue;

                        if (attr.GroupID != gf.OID)
                            continue;

                        if (newTable.Rows.Count < groupIdx)
                            continue;

                        #region ������ϸ��
                        foreach (MapDtl dtl in dtls)
                        {
                            if (dtl.IsUse)
                                continue;

                            if (dtl.RowIdx != groupIdx - 3)
                                continue;


                            if (gf.OID != dtl.GroupID)
                                continue;

                            newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                            newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));

                            Attrs dtlAttrs = dtl.GenerMap().Attrs;
                            string strs = "";
                            int colNum = 0;
                            foreach (Attr attrDtl in dtlAttrs)
                            {
                                if (attrDtl.UIVisible == false)
                                    continue;
                                strs += "|" + attrDtl.Desc;
                                colNum++;
                            }


                            newTable.Cell(groupIdx, 1).Select();
                            WordApp.Selection.Delete(ref Nothing, ref Nothing);
                            Word.Table newTableDtl = WordDoc.Tables.Add(WordApp.Selection.Range, dtl.RowsOfList, colNum,
                                ref Nothing, ref Nothing);



                            newTableDtl.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            newTableDtl.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                            int colIdx = 1;
                            foreach (Attr attrDtl in dtlAttrs)
                            {
                                if (attrDtl.UIVisible == false)
                                    continue;
                                newTableDtl.Cell(1, colIdx).Range.Text = attrDtl.Desc;
                                colIdx++;
                            }

                            for (int i = 2; i <= dtl.RowsOfList; i++)
                            {
                                newTableDtl.Cell(i, 1).Range.Text = "";
                            }
                            groupIdx++;
                        }
                        #endregion ������ϸ��


                        if (attr.UIIsLine)
                        {
                            currColumnIndex = 0;
                            isLeft = true;
                            if (attr.IsBigDoc)
                            {
                                newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                                newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
                                newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n";
                            }
                            else
                            {
                                newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 4));
                                newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
                            }
                            groupIdx++;
                            continue;
                        }
                        else
                        {
                            if (attr.IsBigDoc)
                            {
                                if (currColumnIndex == 2)
                                {
                                    currColumnIndex = 0;
                                }

                                newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                                if (currColumnIndex == 0)
                                {
                                    newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 2));
                                    newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n";
                                    currColumnIndex = 3;
                                    continue;
                                }
                                else if (currColumnIndex == 3)
                                {
                                    newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 3));
                                    newTable.Cell(groupIdx, 2).Range.Text = attr.Name + ":\r\n";
                                    currColumnIndex = 0;
                                    groupIdx++;
                                    continue;
                                }
                                else
                                {
                                    continue;
                                    //throw new Exception("sdsdsdsd");
                                }
                            }
                            else
                            {
                                if (currColumnIndex == 0)
                                {
                                    newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
                                    currColumnIndex++;
                                    continue;
                                }
                                else if (currColumnIndex == 1)
                                {
                                    newTable.Cell(groupIdx, 3).Range.Text = attr.Name;
                                    currColumnIndex++;
                                    groupIdx++;
                                    continue;
                                }
                                else
                                {
                                    newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
                                    currColumnIndex = 0;
                                    continue;
                                }
                            }
                        }
                    }
                }  //����ѭ��

                #region ���ҳ��
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryFooter;
                WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("ģ����ccflow4.5�Զ����ɣ��Ͻ�ת�ء���ӭʹ�ÿ�Դ,��ѵĹ������̹���ϵͳ http://ccFlow.org.");
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                #endregion


                // �ļ�����
                WordDoc.SaveAs(ref  filename, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing);

                WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
                WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
                try
                {
                    string docFile = filename.ToString();
                    string pdfFile = docFile.Replace(".doc", ".pdf");
                    Glo.Rtf2PDF(docFile, pdfFile);
                }
                catch (Exception ex)
                {
                    Log.DebugWriteInfo("@����pdfʧ��." + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Log.DebugWriteError("@����word���ִ���:" + nd.NodeID + ",@Err:" + ex.Message);
                // throw ex;
                // WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
                WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
                WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
            }
        }
        private string GenerWorkTempleteHtml(Entity en, string enName)
        {
            string msg = "";
            msg += "\r\n<table border=1 width='80%' >";
            GroupField currGF = new GroupField();
            MapAttrs mattrs = new MapAttrs(enName);
            GroupFields gfs = new GroupFields(enName);
            MapDtls dtls = new MapDtls(enName);
            foreach (MapDtl dtl in dtls)
                dtl.IsUse = false;
            foreach (GroupField gf in gfs)
            {
                currGF = gf;
                msg += "<TR>";
                msg += "<TD colspan=4 valign='top' align=left bgcolor=Silver ><div style='text-align:left; float:left'>&nbsp;<b>" + gf.Lab + "</b></div><div style='text-align:right; float:right'></div></TD>";
                msg += "</TR>";

                bool isHaveH = false;
                int i = -1;
                int idx = -1;
                bool isLeftNext = true;
                int rowIdx = 0;
                foreach (MapAttr attr in mattrs)
                {

                    if (attr.GroupID != gf.OID)
                    {
                        if (gf.Idx == 0 && attr.GroupID == 0)
                        {

                        }
                        else
                            continue;
                    }

                    if (attr.HisAttr.IsRefAttr || attr.UIVisible == false)
                        continue;

                    if (isLeftNext == true)
                        this.InsertObjects(dtls, gfs, true, rowIdx, currGF);

                    #region �����ֶ�
                    // ��ʾ��˳���.
                    idx++;
                    if (attr.IsBigDoc && attr.UIIsLine)
                    {
                        if (isLeftNext == false)
                        {
                            msg += "\r\n<TD>&nbsp;</TD>";
                            msg += "\r\n<TD>&nbsp;</TD>";
                            msg += "\r\n</TR>";
                            rowIdx++;
                        }
                        rowIdx++;
                        msg += "\r\n<TR>";
                        if (attr.UIIsEnable)
                            msg += "\r\n<TD  colspan=4 width='100%' valign=top align=left>" + attr.Name;
                        else
                            msg += "\r\n<TD  colspan=4 width='100%' valign=top class=TBReadonly>" + attr.Name;

                        msg += "";
                        msg += "\r\n</TD>";
                        msg += "\r\n</TR>";

                        rowIdx++;
                        isLeftNext = true;
                        continue;
                    }

                    if (attr.IsBigDoc)
                    {
                        if (isLeftNext)
                        {
                            rowIdx++;
                            msg += "<TR>";
                        }

                        msg += "<TD colspan=2>" + attr.Name;
                        //TB mytbLine = new TB();
                        //mytbLine.ID = "TB_" + attr.KeyOfEn;
                        //mytbLine.TextMode = TextBoxMode.MultiLine;
                        //mytbLine.Rows = 8;
                        //mytbLine.Columns = 30;
                        //mytbLine.Attributes["style"] = "width:100%;padding: 0px;margin: 0px;";
                        //mytbLine.Text = en.GetValStrByKey(attr.KeyOfEn);
                        //mytbLine.Enabled = attr.UIIsEnable;
                        //if (mytbLine.Enabled == false)
                        //    mytbLine.Attributes["class"] = "TBReadonly";

                        msg += "\r\n";
                        msg += "\r\n</TD>";

                        if (isLeftNext == false)
                        {
                            msg += "\r\n</TR>";
                            rowIdx++;
                        }

                        isLeftNext = !isLeftNext;
                        continue;
                    }

                    //���� colspanOfCtl .
                    int colspanOfCtl = 1;
                    if (attr.UIIsLine)
                        colspanOfCtl = 3;

                    if (attr.UIIsLine)
                    {
                        if (isLeftNext == false)
                        {
                            msg += "\r\n<TD>&nbsp;</TD>";
                            msg += "\r\n<TD>&nbsp;</TD>";
                            msg += "\r\n</TR>";
                            rowIdx++;
                        }
                        isLeftNext = true;
                    }

                    if (isLeftNext)
                    {
                        rowIdx++;
                        msg += "\r\n<TR>";
                    }

                    #region add contrals.
                    switch (attr.LGType)
                    {
                        case FieldTypeS.Normal:
                            switch (attr.MyDataType)
                            {
                                case DataType.AppString:
                                case DataType.AppDate:
                                case DataType.AppDateTime:
                                    msg += "<TD>" + attr.Name + "</TD>";
                                    msg += "<TD colspan=" + colspanOfCtl + ">&nbsp;</TD>";
                                    break;
                                case DataType.AppBoolean:
                                    msg += "<TD></TD>";
                                    msg += "<TD colspan=" + colspanOfCtl + ">&nbsp;</TD>";
                                    break;
                                case DataType.AppDouble:
                                case DataType.AppFloat:
                                case DataType.AppInt:
                                    msg += "<TD>" + attr.Name + "</TD>";
                                    msg += "<TD colspan=" + colspanOfCtl + ">&nbsp;</TD>";
                                    break;
                                case DataType.AppMoney:
                                case DataType.AppRate:
                                    msg += "<TD>" + attr.Name + "</TD>";
                                    msg += "<TD colspan=" + colspanOfCtl + ">&nbsp;</TD>";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case FieldTypeS.Enum:
                            msg += "<TD>" + attr.Name + "</TD>";
                            msg += "<TD colspan=" + colspanOfCtl + ">&nbsp;</TD>";
                            break;

                            //this.AddTDDesc(attr.Name);
                            //DDL ddle = new DDL();
                            //ddle.ID = "DDL_" + attr.KeyOfEn;
                            //ddle.BindSysEnum(attr.KeyOfEn);
                            //ddle.SetSelectItem(en.GetValStrByKey(attr.KeyOfEn));
                            //ddle.Enabled = attr.UIIsEnable;
                            //this.AddTD("colspan=" + colspanOfCtl, ddle);
                            break;
                        case FieldTypeS.FK:
                            msg += "<TD>" + attr.Name + "</TD>";
                            msg += "<TD colspan=" + colspanOfCtl + ">&nbsp;</TD>";
                            break;
                        default:
                            break;
                    }
                    #endregion add contrals.

                    #endregion �����ֶ�

                    #region β����

                    if (colspanOfCtl == 3)
                    {
                        isLeftNext = true;
                        msg += "\r\n</TR>";
                        continue;
                    }

                    if (isLeftNext == false)
                    {
                        isLeftNext = true;
                        msg += "\r\n</TR>";
                        continue;
                    }
                    isLeftNext = false;
                    #endregion add contrals.

                }
                // �������������
                if (isLeftNext == false)
                {
                    msg += "\r\n<TD>&nbsp;</TD>";
                    msg += "\r\n<TD>&nbsp;</TD>";
                    msg += "\r\n</TR>";
                }
                this.InsertObjects(dtls, gfs, false, rowIdx, currGF);
            }
            msg += "\r\n</Table>";
            msg += "\r\n</div>";
            return msg;
        }
        private void InsertObjects(MapDtls dtls, GroupFields gfs, bool isJudgeRowIdx, int rowIdx, GroupField currGF)
        {
            foreach (MapDtl dtl in dtls)
            {
                if (dtl.IsUse)
                    continue;

                if (isJudgeRowIdx)
                {
                    if (dtl.RowIdx != rowIdx)
                        continue;
                }

                if (dtl.GroupID == 0 && rowIdx == 0)
                {
                    dtl.GroupID = currGF.OID;
                    dtl.RowIdx = 0;
                    dtl.Update();
                }
                else if (dtl.GroupID == currGF.OID)
                {

                }
                else
                {
                    continue;
                }
                dtl.IsUse = true;
                rowIdx++;
                //// myidx++;
                //this.AddTR(" ID='" + currGF.Idx + "_" + rowIdx + "' ");
                //this.Add("<TD colspan=4 ID='TD" + dtl.No + "' height='50px' width='100%'  >");
                //string src = this.Request.ApplicationPath + "/WF/Dtl.aspx?EnsName=" + dtl.No + "&RefPKVal=" + this.HisEn.PKVal;
                //this.Add("<iframe ID='F" + dtl.No + "' frameborder=0 Onblur=\"SaveDtl('" + dtl.No + "');\" style='padding:0px;border:0px;width:100%'  leftMargin='0'  topMargin='0' src='" + src + "' height='10px' scrolling=no  /></iframe>");
                //this.AddTDEnd();
                //this.AddTREnd();
            }
        }
        #endregion ��������ģ�塣

        public void CheckRptOfReset()
        {
            string fk_mapData = "ND" + int.Parse(this.No) + "Rpt";
            string sql = "DELETE FROM Sys_MapAttr WHERE FK_MapData='" + fk_mapData + "'";
            DBAccess.RunSQL(sql);

            sql = "DELETE FROM Sys_MapData WHERE No='" + fk_mapData + "'";
            DBAccess.RunSQL(sql);
            this.CheckRpt();
        }
        public void CheckRpt()
        {
            Nodes nds = this.HisNodes;
            CheckRpt(nds);
            CheckRptDtl(nds);
            this.CheckRptView(nds);
        }
        /// <summary>
        /// ������ϸ������Ϣ
        /// </summary>
        /// <param name="nds"></param>
        private void CheckRptDtl(Nodes nds)
        {
            MapDtls dtlsDtl = new MapDtls();
            dtlsDtl.Retrieve(MapDtlAttr.FK_MapData, "ND" + int.Parse(this.No) + "Rpt");
            foreach (MapDtl dtl in dtlsDtl)
            {
                dtl.Delete();
            }

            //  dtlsDtl.Delete(MapDtlAttr.FK_MapData, "ND" + int.Parse(this.No) + "Rpt");

            foreach (Node nd in nds)
            {
                if (nd.IsEndNode == false)
                    continue;

                // ȡ������ϸ��
                MapDtls dtls = new MapDtls("ND" + nd.NodeID);
                if (dtls.Count == 0)
                    continue;

                string rpt = "ND" + int.Parse(this.No) + "Rpt";

                int i = 0;
                foreach (MapDtl dtl in dtls)
                {
                    i++;
                    string rptDtlNo = "ND" + int.Parse(this.No) + "RptDtl" + i.ToString();
                    MapDtl rtpDtl = new MapDtl();
                    rtpDtl.No = rptDtlNo;
                    if (rtpDtl.RetrieveFromDBSources() == 0)
                    {
                        rtpDtl.Copy(dtl);
                        rtpDtl.No = rptDtlNo;
                        rtpDtl.FK_MapData = rpt;
                        rtpDtl.PTable = rptDtlNo;
                        rtpDtl.GroupID = -1;
                        rtpDtl.Insert();
                    }


                    MapAttrs attrsRptDtl = new MapAttrs(rptDtlNo);
                    MapAttrs attrs = new MapAttrs(dtl.No);
                    foreach (MapAttr attr in attrs)
                    {
                        if (attrsRptDtl.Contains(MapAttrAttr.KeyOfEn, attr.KeyOfEn) == true)
                            continue;

                        MapAttr attrN = new MapAttr();
                        attrN.Copy(attr);
                        attrN.FK_MapData = rptDtlNo;
                        switch (attr.KeyOfEn)
                        {
                            case "FK_NY":
                                attrN.UIVisible = true;
                                attrN.IDX = 100;
                                attrN.UIWidth = 60;
                                break;
                            case "RDT":
                                attrN.UIVisible = true;
                                attrN.IDX = 100;
                                attrN.UIWidth = 60;
                                break;
                            case "Rec":
                                attrN.UIVisible = true;
                                attrN.IDX = 100;
                                attrN.UIWidth = 60;
                                break;
                            default:
                                break;
                        }

                        attrN.Save();
                    }

                    GEDtl geDtl = new GEDtl(rptDtlNo);
                    geDtl.CheckPhysicsTable();
                }
            }
        }
        /// <summary>
        /// �������нڵ���ͼ
        /// </summary>
        /// <param name="nds"></param>
        private void CheckRptView(Nodes nds)
        {
            string viewName = "V" + this.No;
            string sql = "CREATE VIEW " + viewName + " ";
            sql += "/* WorkFlow:" + this.Name + " Date:" + DateTime.Now.ToString("yyyy-MM-dd") + " */ ";
            sql += "\r\n AS ";
            bool is1 = false;
            foreach (Node nd in nds)
            {
                //  nd.HisWork.CheckPhysicsTable();
                if (is1 == false)
                    is1 = true;
                else
                    sql += "\r\n UNION ";

                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.Oracle9i:
                        sql += "\r\n SELECT '" + nd.NodeID + "' || '_'|| OID||'_'|| FID  AS MyPK, '" + nd.NodeID + "' AS FK_Node,OID,FID,RDT,SUBSTR(RDT,1,7) AS FK_NY,CDT,Rec,Emps,NodeState,FK_Dept, 1 AS MyNum FROM ND" + nd.NodeID + " ";
                        break;
                    default:
                        sql += "\r\n SELECT '" + nd.NodeID + "'+'_'+CAST(OID AS varchar(10)) +'_'+CAST(FID AS VARCHAR(10)) AS MyPK, '" + nd.NodeID + "' AS FK_Node,OID,FID,RDT," + BP.SystemConfig.AppCenterDBSubstringStr + "(RDT,1,7) AS FK_NY,CDT,Rec,Emps,NodeState,FK_Dept, 1 AS MyNum FROM ND" + nd.NodeID + " ";
                        break;
                }
            }
            sql += "\r\n GO ";

            if (DBAccess.IsExitsObject(viewName) == true)
                DBAccess.RunSQL("DROP VIEW " + viewName);

            try
            {
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }

        }
        private void CheckRpt(Nodes nds)
        {
            string fk_mapData = "ND" + int.Parse(this.No) + "Rpt";
            string flowId = int.Parse(this.No).ToString();

            #region �����ֶΡ�
            string sql = "SELECT distinct  KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN ( SELECT 'ND' " +SystemConfig.AppCenterDBAddStringStr + " cast(NodeID as varchar(20)) FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            string sql2 = "DELETE FROM Sys_MapAttr WHERE KeyOfEn NOT IN (" + sql + ") AND FK_MapData='ND" + flowId + "Rpt' ";
            DBAccess.RunSQL(sql2); // ɾ�������ڵ��ֶ�.

            // ������û���ֶΡ�
            sql = "SELECT MyPK, KeyOfEn FROM Sys_MapAttr WHERE FK_MapData IN ( SELECT 'ND' " + SystemConfig.AppCenterDBAddStringStr + " cast(NodeID as varchar(20)) FROM WF_Node WHERE FK_Flow='" + this.No + "')";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            sql = "SELECT KeyOfEn FROM Sys_MapAttr WHERE FK_MapData='ND" + flowId + "Rpt'";
            DataTable dtExits = DBAccess.RunSQLReturnTable(sql);
            string pks = "@";
            foreach (DataRow dr in dtExits.Rows)
                pks += dr[0] + "@";
            
            foreach (DataRow dr in dt.Rows)
            {
                string mypk = dr["MyPK"].ToString();
                if (pks.Contains("@" + dr["KeyOfEn"].ToString() + "@"))
                    continue;
                
                pks += dr["KeyOfEn"].ToString() + "@";
                BP.Sys.MapAttr ma = new BP.Sys.MapAttr(mypk);
                ma.MyPK = "ND" + flowId + "Rpt" + "_" + ma.KeyOfEn;
                ma.FK_MapData = "ND" + flowId + "Rpt";
                ma.UIIsEnable = false;
                ma.DefVal = "";
                try
                {
                    ma.Insert();
                }
                catch
                {
                }
            }

            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + flowId + "Rpt";
            if (md.RetrieveFromDBSources() == 0)
            {
                md.Name = this.Name;
                md.Insert();
            }
            else
            {
                md.Name = this.Name;
                md.Update();
            }

            sql = "SELECT DISTINCT GroupID FROM Sys_MapAttr WHERE FK_MapData='ND" + flowId + "Rpt' AND GroupID NOT IN( SELECT OID FROM Sys_GroupField WHERE EnName='ND" + flowId + "Rpt') ";
            Sys.GroupFields gfs = new BP.Sys.GroupFields();
            QueryObject qo = new QueryObject(gfs);
            qo.AddWhereInSQL("OID", sql);
            int i = qo.DoQuery();

            MapAttrs attrs = new MapAttrs(fk_mapData);
            foreach (Sys.GroupField gf in gfs)
            {
                GroupField mygf = new GroupField();
                mygf.Copy(gf);
                mygf.OID = 0;
                mygf.EnName = fk_mapData;
                mygf.Insert();
                i = DBAccess.RunSQL("UPDATE Sys_MapAttr SET GroupID=" + mygf.OID + " WHERE  FK_MapData='" + fk_mapData + "'  AND GroupID=" + gf.OID);
                if (i == 0)
                    mygf.Delete();
            }
            #endregion �����ֶΡ�

            #region �����������ֶΡ�
            int groupID = 0;
            foreach (MapAttr attr in attrs)
            {
                switch (attr.KeyOfEn)
                {
                    case StartWorkAttr.NodeState:
                        attr.Delete();
                        break;
                    case StartWorkAttr.WFState:
                        if (attr.UIBindKey.Length > 3)
                            continue;
                        if (attr.UIContralType != UIContralType.DDL)
                        {
                            attr.UIBindKey = attr.KeyOfEn;
                            attr.UIContralType = UIContralType.DDL;
                            attr.LGType = FieldTypeS.Enum;
                            attr.UIVisible = false;
                            attr.DefVal = "";
                            attr.UIIsEnable = false;
                            attr.Update();
                        }
                        break;
                    case StartWorkAttr.FK_Dept:
                        attr.UIBindKey = "BP.Port.Depts";
                        attr.UIContralType = UIContralType.DDL;
                        attr.LGType = FieldTypeS.FK;
                        attr.UIVisible = true;
                        attr.GroupID = groupID;// gfs[0].GetValIntByKey("OID");
                        attr.UIIsEnable = false;
                        attr.DefVal = "";
                        attr.Update();
                        // }
                        break;
                    case "FK_NY":
                        attr.UIBindKey = "BP.Pub.NYs";
                        attr.UIContralType = UIContralType.DDL;
                        attr.LGType = FieldTypeS.FK;
                        attr.UIVisible = true;
                        attr.UIIsEnable = false;
                        attr.GroupID = groupID; 
                        attr.Update();
                        break;
                    case "FK_Emp":
                        break;
                    default:
                        break;
                }
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowEmps) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEmps; // "FlowEmps";
                attr.Name = "������"; //  
                attr.MyDataType = DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.IDX = -100;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowStarter) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowStarter;
                attr.Name = this.ToE("Starter", "������"); //  
                attr.MyDataType = DataType.AppString;

                attr.UIBindKey = "BP.Port.Emps";
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;

                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.IDX = -1;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowStartRDT) == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowStartRDT; // "FlowStartRDT";
                attr.Name = this.ToE("StartDT", "����ʱ��");
                attr.MyDataType = DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.IDX = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowEnder) == false)
            {
                /* ������ */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEnder;
                attr.Name = this.ToE("Ender", "������"); //  
                attr.MyDataType = DataType.AppString;
                attr.UIBindKey = "BP.Port.Emps";
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 1000;
                attr.IDX = -1;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowEnderRDT) == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowEnderRDT; // "FlowStartRDT";
                attr.Name = this.ToE("EDT", "����ʱ��");
                attr.MyDataType = DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.IDX = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_" + GERptAttr.FlowDaySpan) == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = GERptAttr.FlowDaySpan; // "FlowStartRDT";
                attr.Name = this.ToE("Span", "���(��)");
                attr.MyDataType = DataType.AppMoney;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = true;
                attr.UIIsLine = false;
                attr.IDX = -101;
                attr.Insert();
            }

            if (attrs.Contains(md.No + "_MyNum") == false)
            {
                /* MyNum */
                MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = EditType.UnDel;
                attr.KeyOfEn = "MyNum";
                attr.Name = this.ToE("MyNum", "��");
                attr.MyDataType = DataType.AppInt;
                attr.DefVal = "1";
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.HisEditType = EditType.UnDel;
                attr.IDX = -101;
                attr.Insert();
            }
            #endregion �����������ֶΡ�

            #region Ϊ�����ֶ����÷��顣
            string flowInfo = this.ToE("FlowInfo", "������Ϣ");
            GroupField flowGF = null;
            foreach (GroupField mygf in gfs)
            {
                if (mygf.Lab != flowInfo)
                    continue;
                flowGF = mygf;
            }
            if (flowGF == null)
            {
                flowGF = new GroupField();
                flowGF.Lab = flowInfo;
                flowGF.EnName = fk_mapData;
                flowGF.Idx = -1;
                flowGF.Insert();
            }

            DBAccess.RunSQL("UPDATE Sys_MapAttr SET GroupID=" + flowGF.OID + " WHERE  FK_MapData='" + fk_mapData + "'  AND KeyOfEn IN('" + GERptAttr.MyNum + "','" + GERptAttr.BillNo + "','" + GERptAttr.FK_Dept + "','" + GERptAttr.FK_NY + "','" + GERptAttr.FlowDaySpan + "','" + GERptAttr.FlowEmps + "','" + GERptAttr.FlowEnder + "','" + GERptAttr.FlowEnderRDT + "','" + GERptAttr.FlowStarter + "','" + GERptAttr.FlowStartRDT + "','" + GERptAttr.WFState + "')");
            #endregion Ϊ�����ֶ����÷���

            BP.Sys.GEEntity sw = this.HisFlowData;
            sw.CheckPhysicsTable();

            DBAccess.RunSQL("DELETE FROM Sys_GroupField WHERE EnName='" + fk_mapData + "' AND OID NOT IN (SELECT GroupID FROM sys_mapattr where fk_mapdata like '" + fk_mapData + "')");
            DBAccess.RunSQL("UPDATE ND" + flowId + "Rpt SET MyNum=1");
        }

        #region ��������
        public bool IsCCAll
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsCCAll);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsCCAll, value);
            }
        }
        public bool IsCanStart
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsCanStart);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsCanStart, value);
            }
        }
        public bool IsOK
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsOK);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsOK, value);
            }
        }
        public bool IsFJ_del
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsFJ);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsFJ, value);
            }
        }
        /// <summary>
        /// �Ƿ��е���
        /// </summary>
        public int NumOfBill
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.NumOfBill);
            }
            set
            {
                this.SetValByKey(FlowAttr.NumOfBill, value);
            }
        }
        public int NumOfDtl
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.NumOfDtl);
            }
            set
            {
                this.SetValByKey(FlowAttr.NumOfDtl, value);
            }
        }
        public decimal AvgDay
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.AvgDay);
            }
            set
            {
                this.SetValByKey(FlowAttr.AvgDay, value);
            }
        }
        public int StartNodeID
        {
            get
            {
                return int.Parse(this.No + "01");
                //return this.GetValIntByKey(FlowAttr.StartNodeID);
            }
        }
        public string EnName
        {
            get
            {
                return "ND" + this.StartNodeID;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.FK_FlowSort);
            }
            set
            {
                this.SetValByKey(FlowAttr.FK_FlowSort, value);
            }
        }
        public string FK_FlowSortText
        {
            get
            {
                return this.GetValRefTextByKey(FlowAttr.FK_FlowSort);
            }
        }
        /// <summary>
        /// Ҫ���͵ĸ�λ
        /// </summary>
        public string CCStas
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.CCStas);
            }
            set
            {
                this.SetValByKey(FlowAttr.CCStas, value);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������(�������)
        /// </summary>
        public int FlowType_del
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.FlowType);
            }
        }
        /// <summary>
        /// �Ƿ��Զ�����
        /// </summary>
        public bool IsAutoWorkFlow_del
        {
            get
            {
                return false;
                /*
                if (this.FlowType==1)
                    return true;
                else
                    return false;
                    */
            }
        }
        public string Note
        {
            get
            {
                string s = this.GetValStringByKey("Note");
                if (s.Length == 0)
                {
                    return "��";
                }
                return s;
            }
        }
        public string NoteHtml
        {
            get
            {
                if (this.Note == "��" || this.Note == "")
                    return this.ToE("FlowDescNull",
                        "���������Աû�б�д�����̵İ�����Ϣ����������-���򿪴�����-����ƻ����ϵ���Ҽ�-����������-����д���̰�����Ϣ��");
                else
                    return this.Note;
            }
        }
        /// <summary>
        /// �Ƿ���߳��Զ�����
        /// </summary>
        public bool IsMutiLineWorkFlow_del
        {
            get
            {
                return false;
                /*
                if (this.FlowType==2 || this.FlowType==1 )
                    return true;
                else
                    return false;
                    */
            }
        }
        #endregion

        #region ��չ����
        /// <summary>
        /// Ӧ������
        /// </summary>
        public FlowAppType HisFlowAppType
        {
            get
            {
                return (FlowAppType)this.GetValIntByKey(FlowAttr.AppType);
            }
            set
            {
                this.SetValByKey(FlowAttr.AppType, (int)value);
            }
        }
        public DocType HisDocType
        {
            get
            {
                return (DocType)this.GetValIntByKey(FlowAttr.DocType);
            }
            set
            {
                this.SetValByKey(FlowAttr.DocType, (int)value);
            }
        }
        public string DocTypeT
        {
            get
            {
                return this.GetValRefTextByKey(FlowAttr.DocType);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FlowType HisFlowType
        {
            get
            {
                return (FlowType)this.GetValIntByKey(FlowAttr.FlowType);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowType, (int)value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public XWType HisXWType
        {
            get
            {
                return (XWType)this.GetValIntByKey(FlowAttr.XWType);
            }
            set
            {
                this.SetValByKey(FlowAttr.XWType, (int)value);
            }
        }
        public string HisXWTypeT
        {
            get
            {
                return this.GetValRefTextByKey(FlowAttr.XWType);
            }
        }
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Nodes _HisNodes = null;
        /// <summary>
        /// ���Ľڵ㼯��.
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                if (this._HisNodes == null)
                    _HisNodes = new Nodes(this.No);
                return _HisNodes;
            }
            set
            {
                _HisNodes = value;
            }
        }
        /// <summary>
        /// ���� Start �ڵ�
        /// </summary>
        public Node HisStartNode
        {
            get
            {

                foreach (Node nd in this.HisNodes)
                {
                    if (nd.IsStartNode)
                        return nd;
                }
                throw new Exception("@û���ҵ����Ŀ�ʼ�ڵ�,��������[" + this.Name + "]�������.");
            }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public FlowSort HisFlowSort
        {
            get
            {
                return new FlowSort(this.FK_FlowSort);
            }
        }
        /// <summary>
        /// flow data ����
        /// </summary>
        public BP.Sys.GEEntity HisFlowData
        {
            get
            {
                try
                {
                    BP.Sys.GEEntity wk = new BP.Sys.GEEntity("ND" + int.Parse(this.No) + "Rpt");
                    return wk;
                }
                catch
                {
                    this.DoCheck();
                    BP.Sys.GEEntity wk1 = new BP.Sys.GEEntity("ND" + int.Parse(this.No) + "Rpt");
                    return wk1;
                }
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public Flow()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_No">���</param>
        public Flow(string _No)
        {
            this.No = _No;
            if (SystemConfig.IsDebug)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("���̱�Ų�����");
            }
            else
            {
                this.Retrieve();
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

                Map map = new Map("WF_Flow");

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = this.ToE("Flow", "����");
                map.IsAllowRepeatName = false;
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, null, true, true, 1, 10, 3);
                map.AddTBString(FlowAttr.Name, null, null, true, false, 0, 50, 10);

                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "�������"), new FlowSorts(), false);
                map.AddTBInt(FlowAttr.FlowType, (int)FlowType.Panel, "��������", false, false);

                // @0=ҵ������@1=��������.
                map.AddTBInt(FlowAttr.FlowSheetType, (int)FlowSheetType.SheetFlow, "������", false, false);

                map.AddDDLSysEnum(FlowAttr.DocType, (int)DocType.OfficialDoc, "��������(�Թ�����Ч)", false, false,
                    FlowAttr.DocType, "@0=��ʽ����@1=�㺯");
                map.AddDDLSysEnum(FlowAttr.XWType, (int)XWType.Down, "��������(�Թ�����Ч)", false, false, FlowAttr.XWType,
                    "@0=������@1=ƽ����@2=������");

                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, "���з�ʽ", false,
                    false, FlowAttr.FlowRunWay,
                    "@0=�ֹ�����@1=��������@2=��������@3=��������@4=��������@5=��Сʱ����");
                map.AddTBString(FlowAttr.FlowRunObj, null, "��������", true, false, 0, 3000, 10);

                map.AddTBString(FlowAttr.Note, null, null, true, false, 0, 100, 10);
                map.AddTBString(FlowAttr.RunSQL, null, "���̽���ִ�к�ִ�е�SQL", true, false, 0, 2000, 10);

                map.AddTBInt(FlowAttr.NumOfBill, 0, "�Ƿ��е���", false, false);
                map.AddTBInt(FlowAttr.NumOfDtl, 0, "NumOfDtl", false, false);


                map.AddBoolean(FlowAttr.IsOK, true, this.ToE("IsEnable", "�Ƿ�����"), true, true);
                map.AddBoolean(FlowAttr.IsCCAll, false, "������ɺ��Ͳ�����Ա", true, true);

                map.AddBoolean(FlowAttr.IsCanStart, true, "���Զ���������", true, true, true);

                map.AddTBString(FlowAttr.CCStas, null, "Ҫ���͵ĸ�λ", false, false, 0, 2000, 10);

                map.AddTBDecimal(FlowAttr.AvgDay, 0, "ƽ����������", false, false);



                map.AddTBString(FlowAttr.StartListUrl, null, this.ToE("StartListUrl", "����Url"), true, false, 0, 500, 10, true);


                map.AddTBInt(FlowAttr.AppType, 0, "Ӧ������", false, false);

                map.AddSearchAttr(FlowAttr.FK_FlowSort);
                   map.AddSearchAttr(FlowAttr.FlowRunWay);


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "��Ƽ�鱨��"); // "��Ƽ�鱨��";
                rm.ToolTip = "���������Ƶ����⡣";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                //   rm = new RefMethod();
                //rm.Title = this.ToE("ViewDef", "��ͼ����"); //"��ͼ����";
                //rm.Icon = "/Images/Btn/View.gif";
                //rm.ClassMethodName = this.ToString() + ".DoDRpt";
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("RptRun", "��������"); // "��������";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowDataOut", "����ת������");  //"����ת������";
                ////  rm.Icon = "/Images/Btn/Table.gif";
                //rm.ToolTip = "���������ʱ�䣬��������ת���浽����ϵͳ��Ӧ�á�";
                //rm.ClassMethodName = this.ToString() + ".DoExp";
                //map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "ɾ������";
                rm.Warning = "��ȷ��Ҫִ��ɾ������������";
                rm.ToolTip = "�����ʷ�������ݡ�";
                rm.ClassMethodName = this.ToString() + ".DoExp";
                map.AddRefMethod(rm);


                map.AttrsOfOneVSM.Add(new FlowStations(), new Stations(), FlowStationAttr.FK_Flow,
                    FlowStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "���͸�λ");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeDelete()
        {
            //this.DoDelData();
            //this.DoDelete();
            return base.beforeDelete();
        }

        protected override bool beforeInsert()
        {
            if (this.FK_FlowSort == "01")
                this.HisFlowSheetType = FlowSheetType.DocFlow;
            else
                this.HisFlowSheetType = FlowSheetType.SheetFlow;


            return base.beforeInsert();
        }

        protected override bool beforeUpdateInsertAction()
        {
            DBAccess.RunSQL("UPDATE WF_Node SET FlowName='" + this.Name + "' WHERE FK_Flow='" + this.No + "'");
            DBAccess.RunSQL("UPDATE Sys_MapData SET  Name='" + this.Name + "' WHERE No='ND" + int.Parse(this.No) + "Rpt'");
            return base.beforeUpdateInsertAction();
        }

        #region  ��������
        /// <summary>
        /// �������ת��
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            this.DoCheck();
            PubClass.WinOpen("./../WF/Admin/Exp.aspx?CondType=0&FK_Flow=" + this.No, "����", "cdsn", 800, 500, 210, 300);
            return null;
        }
        /// <summary>
        /// ���屨��
        /// </summary>
        /// <returns></returns>
        public string DoDRpt()
        {
            this.DoCheck();
            PubClass.WinOpen("./../WF/Admin/WFRpt.aspx?CondType=0&FK_Flow=" + this.No, "����", "cdsn", 800, 500, 210, 300);
            return null;
        }
        /// <summary>
        /// ���б���
        /// </summary>
        /// <returns></returns>
        public string DoOpenRpt()
        {
            BP.PubClass.WinOpen("../WF/MapDef/WFRpt.aspx?FK_Flow=" + this.No + "&DoType=Edit&MyPK=ND" + int.Parse(this.No) + "Rpt", 700, 400);
            // BP.PubClass.WinOpen("../WF/SelfWFRpt.aspx?FK_Flow=" + this.No + "&DoType=Edit&RefNo=" + this.No, 700, 400);
            return null;
        }
        public string DoDelData()
        {
            string sql = "  where FK_Node in (SELECT NodeID FROM WF_Node WHERE fk_flow='" + this.No + "')";
            string sql1 = " where NodeID in (SELECT NodeID FROM WF_Node WHERE fk_flow='" + this.No + "')";

            DA.DBAccess.RunSQL("DELETE FROM WF_CHOfFlow WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_Bill WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FK_Flow='" + this.No + "'");

            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_ReturnWork " + sql1);
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FK_Flow='" + this.No + "'");
            DA.DBAccess.RunSQL("DELETE FROM WF_SelectAccper " + sql);
            DA.DBAccess.RunSQL("DELETE FROM WF_FileManager " + sql);
            DA.DBAccess.RunSQL("DELETE FROM WF_RememberMe " + sql);

            DA.DBAccess.RunSQL("DELETE FROM ND" + int.Parse(this.No) + "Rpt ");

            //DA.DBAccess.RunSQL("DELETE FROM WF_WorkList WHERE FK_Flow='" + this.No + "'");
            //DA.DBAccess.RunSQL("DELETE Sys_MapExt WHERE FK_MapData LIKE 'ND"+int.Parse(this.No)+"%'" );

            //ɾ���ڵ����ݡ�
            Nodes nds = new Nodes(this.No);
            foreach (Node nd in nds)
            {
                try
                {
                    Work wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE FROM " + wk.EnMap.PhysicsTable);
                }
                catch
                {
                }

                MapDtls dtls = new MapDtls("ND" + nd.NodeID);
                foreach (MapDtl dtl in dtls)
                {
                    try
                    {
                        DA.DBAccess.RunSQL("DELETE FROM " + dtl.PTable);
                    }
                    catch
                    {
                    }
                }
            }


            DA.DBAccess.RunSQL("DELETE FROM ND" + int.Parse(this.No) + "Rpt ");
            MapDtls mydtls = new MapDtls("ND" + int.Parse(this.No) + "Rpt");
            foreach (MapDtl dtl in mydtls)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE FROM " + dtl.PTable);
                }
                catch
                {
                }
            }
            return "ɾ���ɹ�...";
        }
        /// <summary>
        /// װ������ģ��
        /// </summary>
        /// <param name="fk_flowSort">�������</param>
        /// <param name="path">��������</param>
        /// <returns></returns>
        public static Flow DoLoadFlowTemplate(string fk_flowSort, string path)
        {
            FileInfo info = new FileInfo(path);
            DataSet ds = new DataSet();
            ds.ReadXml(path);

            DataTable dtFlow = ds.Tables["WF_Flow"];
            Flow fl = new Flow();
            string oldFlowNo = dtFlow.Rows[0]["No"].ToString();
            int oldFlowID = int.Parse(oldFlowNo);
            try
            {
                fl.DoNewFlow();

                int flowID = int.Parse(fl.No);

                #region �������̱�����
                foreach (DataColumn dc in dtFlow.Columns)
                {
                    string val = dtFlow.Rows[0][dc.ColumnName] as string;
                    switch (dc.ColumnName)
                    {
                        case "No":
                        case "FK_FlowSort":
                            continue;
                        case "Name":
                            val = "Copy of " + val;
                            break;
                        default:
                            break;
                    }
                    fl.SetValByKey(dc.ColumnName, val);
                }
                fl.FK_FlowSort = fk_flowSort;
                fl.Update();
                #endregion �������̱�����

                string msg = "";

                #region ����OID �����ظ������⡣ Sys_GroupField �� Sys_MapAttr.
                DataTable mydtGF = ds.Tables["Sys_GroupField"];
                DataTable myDTAttr = ds.Tables["Sys_MapAttr"];
                foreach (DataRow dr in mydtGF.Rows)
                {
                    Sys.GroupField gf = new Sys.GroupField();
                    foreach (DataColumn dc in mydtGF.Columns)
                    {
                        string val = dr[dc.ColumnName] as string;
                        gf.SetValByKey(dc.ColumnName, val);
                    }
                    if (gf.IsExits)
                    {
                        int oldID = gf.OID;
                        dr["OID"] = DBAccess.GenerOID();
                        foreach (DataRow dr1 in myDTAttr.Rows)
                        {
                            if (dr1["GroupID"].ToString() == oldID.ToString())
                            {
                                dr1["GroupID"] = gf.OID;
                            }
                        }
                    }
                }
                #endregion ����OID �����ظ������⡣ Sys_GroupField �� Sys_MapAttr.

                foreach (DataTable dt in ds.Tables)
                {
                    switch (dt.TableName)
                    {
                        case "WF_Flow": //ģ���ļ���
                            continue;
                        case "BillTemplates": //ģ���ļ���
                        case "WF_BillTemplate":
                        case "WF_BillTemplates":
                            foreach (DataRow dr in dt.Rows)
                            {
                                BillTemplate bt = new BillTemplate();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Flow":
                                            val = flowID.ToString();
                                            break;
                                        case "NodeID":
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    bt.SetValByKey(dc.ColumnName, val);
                                }
                                int i = 0;
                                string no = bt.No;
                                while (bt.IsExits)
                                {
                                    bt.No = no + i.ToString();
                                    i++;
                                }
                                try
                                {
                                    File.Copy(info.DirectoryName + "\\" + no + ".rtf", BP.SystemConfig.PathOfWebApp + @"\DataUser\CyclostyleFile\" + bt.No + ".rtf", true);
                                }
                                catch (Exception ex)
                                {
                                    msg += "@�ָ�����ģ��ʱ���ִ���" + ex.Message + ",�п��������ڸ�������ģ��ʱû�и���ͬĿ¼�µĵ���ģ���ļ���";
                                }
                                bt.Insert();
                            }
                            break;
                        case "WF_Cond": //Conds.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Cond cd = new Cond();
                                cd.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "ToNodeID":
                                        case "FK_Node":
                                        case "NodeID":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    cd.SetValByKey(dc.ColumnName, val);
                                }
                                cd.MyPK = DA.DBAccess.GenerOID().ToString();
                                cd.Insert();
                            }

                            break;
                        case "Directions": //FAppSets.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Direction dir = new Direction();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "Node":
                                        case "ToNode":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    dir.SetValByKey(dc.ColumnName, val);
                                }
                                dir.Insert();
                            }
                            break;
                        case "WF_FAppSet": //FAppSets.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                FAppSet fs = new FAppSet();
                                fs.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    fs.SetValByKey(dc.ColumnName, val);
                                }
                                fs.OID = DA.DBAccess.GenerOID();
                                fs.Insert();
                            }

                            break;
                        case "FlowStations": //FlowStations.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                FlowStation fs = new FlowStation();
                                fs.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case FlowStationAttr.FK_Flow:
                                            continue;
                                        default:
                                            break;
                                    }
                                    fs.SetValByKey(dc.ColumnName, val);
                                }
                                fs.Insert();
                            }

                            break;
                        case "WF_LabNote": //LabNotes.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                LabNote ln = new LabNote();
                                ln.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case LabNoteAttr.FK_Flow:
                                            continue;
                                        default:
                                            break;
                                    }
                                    ln.SetValByKey(dc.ColumnName, val);
                                }
                                ln.MyPK = ln.FK_Flow + "_" + ln.X + "_" + ln.Y;
                                ln.DirectInsert();
                                //ln.OID = DA.DBAccess.GenerOID();
                                //ln.InsertAsOID(ln.OID);
                            }
                            break;
                        case "NodeDept": //FAppSets.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                NodeDept dir = new NodeDept();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    dir.SetValByKey(dc.ColumnName, val);
                                }
                                dir.Insert();
                            }

                            break;
                        case "WF_Node": //LabNotes.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Node nd = new Node();
                                nd.FK_Flow = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case NodeAttr.NodeID:
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        case NodeAttr.FK_Flow:
                                        case "FK_FlowSort":
                                            continue;
                                        case NodeAttr.ShowSheets:
                                        case NodeAttr.HisToNDs:
                                        case NodeAttr.GroupStaNDs:
                                            string key = "@" + flowID;
                                            val = val.Replace(key, "");
                                            break;
                                        default:
                                            break;
                                    }
                                    nd.SetValByKey(dc.ColumnName, val);
                                }
                                try
                                {
                                    nd.DirectInsert();
                                }
                                catch
                                {
                                    nd.DirectUpdate();
                                }
                            }
                            break;
                        case "NodeStations": //FAppSets.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                NodeStation ns = new NodeStation();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    ns.SetValByKey(dc.ColumnName, val);
                                }
                                ns.Insert();
                            }
                            break;
                        case "Listens": // ��Ϣ������
                            foreach (DataRow dr in dt.Rows)
                            {
                                Listen li = new Listen();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "OID":
                                            continue;
                                            break;
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        case "Nodes":
                                            string[] nds = val.Split('@');
                                            string valExt = "";
                                            foreach (string nd in nds)
                                            {
                                                if (nd == "" || nd == null)
                                                    continue;

                                                string ndExt = nd.Clone() as string;

                                                if (ndExt.Length == 3)
                                                    ndExt = flowID + ndExt.Substring(1);
                                                else if (val.Length == 4)
                                                    ndExt = flowID + ndExt.Substring(2);

                                                ndExt = "@" + ndExt;
                                                valExt += ndExt;
                                            }
                                            val = valExt;
                                            break;
                                        default:
                                            break;
                                    }
                                    li.SetValByKey(dc.ColumnName, val);
                                }
                                li.Insert();
                            }
                            break;
                        case "RptAttrs": //LabNotes.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                RptAttr attr = new RptAttr();
                                attr.FK_Node = fl.No;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case RptAttrAttr.FK_Node:
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    attr.SetValByKey(dc.ColumnName, val);
                                }
                                attr.MyPK = attr.FK_Node + "_" + attr.FK_Rpt + "_" + attr.RefAttrOID + "_" + attr.RefField;
                                attr.Save();
                            }

                            break;
                        case "RptStations": //RptEmps.xml��
                        case "RptEmps": //RptEmps.xml��
                            break;
                        case "Sys_Enum": //RptEmps.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.SysEnum se = new Sys.SysEnum();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case RptAttrAttr.FK_Node:
                                            break;
                                        default:
                                            break;
                                    }
                                    se.SetValByKey(dc.ColumnName, val);
                                }

                                se.MyPK = se.EnumKey + "_" + se.Lang + "_" + se.IntKey;
                                if (se.IsExits)
                                    continue;
                                se.Insert();
                            }
                            break;
                        case "Sys_EnumMain": //RptEmps.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.SysEnumMain sem = new Sys.SysEnumMain();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    sem.SetValByKey(dc.ColumnName, val);
                                }

                                if (sem.IsExits)
                                    continue;
                                sem.Insert();
                            }
                            break;
                        case "Sys_MapAttr": //RptEmps.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.MapAttr ma = new Sys.MapAttr();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.MapAttrAttr.FK_MapData:
                                        case Sys.MapAttrAttr.KeyOfEn:
                                            val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                            break;
                                        default:
                                            break;
                                    }
                                    ma.SetValByKey(dc.ColumnName, val);
                                }
                                bool b = ma.IsExit(Sys.MapAttrAttr.FK_MapData, ma.FK_MapData, Sys.MapAttrAttr.KeyOfEn, ma.KeyOfEn);
                                if (b == true)
                                    ma.Update();
                                else
                                    ma.Insert();
                            }
                            break;
                        case "Sys_MapData": //RptEmps.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.MapData md = new Sys.MapData();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.MapDataAttr.No:
                                            val = val.Replace("ND" + oldFlowID, "ND" + int.Parse(fl.No));
                                            break;
                                        default:
                                            break;
                                    }
                                    md.SetValByKey(dc.ColumnName, val);
                                }
                                md.Save();
                            }
                            break;
                        case "Sys_MapDtl": //RptEmps.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.MapDtl md = new Sys.MapDtl();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.MapDtlAttr.No:
                                        case Sys.MapDtlAttr.FK_MapData:
                                        case Sys.MapDtlAttr.PTable:
                                            val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                            break;
                                        default:
                                            break;
                                    }
                                    md.SetValByKey(dc.ColumnName, val);
                                }
                                md.Save();
                            }
                            break;
                        case "Sys_MapExt":
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.MapExt md = new Sys.MapExt();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.MapDtlAttr.No:
                                        case Sys.MapDtlAttr.FK_MapData:
                                        case Sys.MapDtlAttr.PTable:
                                            val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                            break;
                                        default:
                                            break;
                                    }
                                    md.SetValByKey(dc.ColumnName, val);
                                }
                                md.Save();
                            }
                            break;
                        case "WF_NodeEmps": //FAppSets.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                NodeEmp ne = new NodeEmp();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case "FK_Node":
                                            if (val.Length == 3)
                                                val = flowID + val.Substring(1);
                                            else if (val.Length == 4)
                                                val = flowID + val.Substring(2);
                                            break;
                                        default:
                                            break;
                                    }
                                    ne.SetValByKey(dc.ColumnName, val);
                                }
                                ne.Insert();
                            }
                            break;
                        case "WFRpts": //RptEmps.xml��
                            foreach (DataRow dr in dt.Rows)
                            {
                                WFRpt rpt = new WFRpt();
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case WFRptAttr.FK_FlowSort:
                                            val = fl.FK_FlowSort;
                                            break;
                                        case WFRptAttr.FK_Flow:
                                            val = fl.No;
                                            break;
                                        default:
                                            break;
                                    }
                                    rpt.SetValByKey(dc.ColumnName, val);
                                }
                                try
                                {
                                    rpt.Save();
                                }
                                catch
                                {
                                }
                            }
                            break;
                        case "Sys_GroupField": // 
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sys.GroupField gf = new Sys.GroupField();
                                foreach (DataColumn dc in dt.Columns)
                                {

                                    string val = dr[dc.ColumnName] as string;
                                    switch (dc.ColumnName)
                                    {
                                        case Sys.GroupFieldAttr.EnName:
                                        case Sys.MapAttrAttr.KeyOfEn:
                                            val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                            break;
                                        default:
                                            break;
                                    }
                                    gf.SetValByKey(dc.ColumnName, val);
                                }

                                int oid = DBAccess.GenerOID();
                                DBAccess.RunSQL("UPDATE Sys_MapAttr SET GroupID=" + oid + " WHERE FK_MapData='" + gf.EnName + "' AND GroupID=" + gf.OID);
                                gf.InsertAsOID(oid);
                            }
                            break;
                        default:
                            msg += "Error:" + dt.TableName;
                            break;
                        //    throw new Exception("@unhandle named " + dt.TableName);
                    }
                }

                #region �������������ԡ�
                DBAccess.RunSQL("DELETE FROM WF_Cond WHERE ToNodeID NOT IN (SELECT NodeID FROM WF_Node)");
                DBAccess.RunSQL("DELETE FROM WF_Cond WHERE FK_Node NOT IN (SELECT NodeID FROM WF_Node)");
                DBAccess.RunSQL("DELETE FROM WF_Cond WHERE NodeID NOT IN (SELECT NodeID FROM WF_Node)");
                #endregion

                if (msg == "")
                    return fl; // "��ȫ�ɹ���";

                msg = "ִ���ڼ�������·������Ĵ���\t\r" + msg;
                fl.DoCheck();
                throw new Exception(msg);
            }
            catch (Exception ex)
            {
                fl.DoDelete();
                throw ex;
            }
        }
        public Node DoNewNode(int x, int y)
        {
            Node nd = new Node();
            int idx = this.HisNodes.Count;
            while (true)
            {
                string strID = this.No + idx.ToString().PadLeft(2, '0');
                nd.NodeID = int.Parse(strID);
                if (nd.IsExits == false)
                    break;
                idx++;
            }

            nd.HisNodeWorkType = NodeWorkType.Work;
            nd.Name = this.ToE("Node", "�ڵ�") + idx;
            nd.HisNodePosType = NodePosType.Mid;
            nd.FK_Flow = this.No;
            nd.FlowName = this.Name;
            nd.X = x;
            nd.Y = y;
            nd.Step = idx;
            nd.Insert();

            nd.CreateMap();
            return nd;
        }
        /// <summary>
        /// ִ���½�
        /// </summary>
        public void DoNewFlow()
        {
            this.No = this.GenerNewNoByKey(FlowAttr.No);
            if (string.IsNullOrWhiteSpace(this.Name))
                this.Name = BP.Sys.Language.GetValByUserLang("NewFlow", "�½�����") + this.No; //�½�����

            this.Save();

            #region ɾ���п��ܴ��ڵ���ʷ����.
            Flow fl = new Flow(this.No);
            try
            {
                fl.DoDelData();
            }
            catch
            {
            }
            try
            {
                fl.DoDelete();
            }
            catch
            {
            }
            this.Save();
            #endregion ɾ���п��ܴ��ڵ���ʷ����.


            Node nd = new Node();
            nd.NodeID = int.Parse(this.No + "01");
            nd.Name = BP.Sys.Language.GetValByUserLang("StartNode", "��ʼ�ڵ�");//  "��ʼ�ڵ�"; 
            nd.Step = 1;
            nd.FK_Flow = this.No;
            nd.FlowName = this.Name;
            nd.HisNodePosType = NodePosType.Start;
            nd.HisNodeWorkType = NodeWorkType.StartWork;
            nd.X = 100;
            nd.Y = 100;
            nd.Insert();

            nd.CreateMap();
            nd.HisWork.CheckPhysicsTable();

            nd = new Node();
            nd.NodeID = int.Parse(this.No + "99");
            nd.Name = BP.Sys.Language.GetValByUserLang("EndNode", "�����ڵ�"); // "�����ڵ�";
            nd.Step = 99;
            nd.FK_Flow = this.No;
            nd.FlowName = this.Name;
            nd.HisNodePosType = NodePosType.End;
            nd.HisNodeWorkType = NodeWorkType.Work;
            nd.X = 100;
            nd.Y = 200;
            nd.Insert();
            nd.CreateMap();
            nd.HisWork.CheckPhysicsTable();

            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + int.Parse(this.No) + "Rpt";
            md.Name = this.Name;
            md.Save();

            #region ����freeFrm ��װ��.

            FrmImg img = new FrmImg();
            img.MyPK = "Img" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No;
            img.FK_MapData = "ND" + int.Parse(this.No + "01");
            img.X = (float)577.26;
            img.Y = (float)3.45;

            img.W = (float)137;
            img.H = (float)40;

            img.ImgURL = "/FreeFrm;component/Img/LogoBig.png";
            img.LinkURL = "http://ccflow.org";
            img.LinkTarget = "_blank";
            img.Insert();

            FrmLab lab = new FrmLab();
            lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No;
            lab.Text = this.ToE("Title", "���̱���");
            lab.FK_MapData = "ND" + int.Parse(this.No + "01");
            lab.X = (float)106.5;
            lab.Y = (float)59.22;
            lab.FontSize = 11;
            lab.FontColor = "black";
            lab.FontName = "Portable User Interface";
            lab.FontStyle = "Normal";
            lab.FontWeight = "normal";
            lab.Insert();

            lab = new FrmLab();
            lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 2;
            lab.Text = "������";
            lab.FK_MapData = "ND" + int.Parse(this.No + "01");
            lab.X = (float)106.48;
            lab.Y = (float)96.08;
            lab.FontSize = 11;
            lab.FontColor = "black";
            lab.FontName = "Portable User Interface";
            lab.FontStyle = "Normal";
            lab.FontWeight = "normal";
            lab.Insert();

            lab = new FrmLab();
            lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 3;
            lab.Text = "����ʱ��";
            lab.FK_MapData = "ND" + int.Parse(this.No + "01");
            lab.X = (float)307.64;
            lab.Y = (float)95.17;

            lab.FontSize = 11;
            lab.FontColor = "black";
            lab.FontName = "Portable User Interface";
            lab.FontStyle = "Normal";
            lab.FontWeight = "normal";
            lab.Insert();

            lab = new FrmLab();
            lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 4;
            lab.Text = "�½��ڵ�(���޸ı���)";
            lab.FK_MapData = "ND" + int.Parse(this.No + "01");

            lab.X = (float)294.67;
            lab.Y = (float)8.27;

            lab.FontSize = 23;
            lab.FontColor = "Blue";
            lab.FontName = "Portable User Interface";
            lab.FontStyle = "Normal";
            lab.FontWeight = "normal";
            lab.Insert();

            lab = new FrmLab();
            lab.MyPK = "Lab" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No + 5;
            lab.Text = "˵��:����������ccflow�Զ������ģ��������޸�/ɾ������@Ϊ�˸�����������������Ե�http://ccflow.org�������ر�ģ��.";
            lab.Text += "@��Ϊ��ǰ����������silverlight��������ʹ���ر�˵������:@";
            lab.Text += "@1,�ı�ؼ�λ��: ";
            lab.Text += "@  ���еĿؼ���֧�� wasd, ��Ϊ����������ƶ��ؼ���λ�ã� ���ֿؼ�֧�ַ����. ";
            lab.Text += "@2, ����textbox, �ӱ�, dropdownlistbox, �Ŀ�� shift+ -> ��������ӿ�� shift + <- ��С���.";
            lab.Text += "@3, ���� windows�� + s.  ɾ�� delete.  ���� ctrl+c   ճ��: ctrl+v.";
            lab.Text += "@4, ֧��ȫѡ�������ƶ��� �����Ŵ���С����., �����ı��ߵĿ��.";
            lab.Text += "@5, �ı��ߵĳ��ȣ� ѡ���ߣ�����ɫ��Բ�㣬��������.";
            lab.Text += "@6, �Ŵ������С��label ������ , ѡ��һ�����label , �� A+ ���ߡ�A������ť.";
            lab.Text += "@7, �ı��߻��߱�ǩ����ɫ�� ѡ��������󣬵㹤�����ϵĵ�ɫ��.";

            lab.X = (float)168.24;
            lab.Y = (float)163.7;
            lab.FK_MapData = "ND" + int.Parse(this.No + "01");
            lab.FontSize = 11;
            lab.FontColor = "Red";
            lab.FontName = "Portable User Interface";
            lab.FontStyle = "Normal";
            lab.FontWeight = "normal";
            lab.Insert();

            string key = "L" + DateTime.Now.ToString("yyMMddhhmmss") + WebUser.No;
            FrmLine line = new FrmLine();
            line.MyPK = key + "_1";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)281.82;
            line.Y1 = (float)81.82;
            line.X2 = (float)281.82;
            line.Y2 = (float)121.82;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();


            line.MyPK = key + "_2";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)360;
            line.Y1 = (float)80.91;
            line.X2 = (float)360;
            line.Y2 = (float)120.91;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();

            line.MyPK = key + "_3";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)158.82;
            line.Y1 = (float)41.82;
            line.X2 = (float)158.82;
            line.Y2 = (float)482.73;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();

            line.MyPK = key + "_4";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)81.55;
            line.Y1 = (float)80;
            line.X2 = (float)718.82;
            line.Y2 = (float)80;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();


            line.MyPK = key + "_5";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)81.82;
            line.Y1 = (float)40;
            line.X2 = (float)81.82;
            line.Y2 = (float)480.91;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();

            line.MyPK = key + "_6";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)81.82;
            line.Y1 = (float)481.82;
            line.X2 = (float)720;
            line.Y2 = (float)481.82;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();

            line.MyPK = key + "_7";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)83.36;
            line.Y1 = (float)40.91;
            line.X2 = (float)717.91;
            line.Y2 = (float)40.91;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();

            line.MyPK = key + "_8";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)83.36;
            line.Y1 = (float)120.91;
            line.X2 = (float)717.91;
            line.Y2 = (float)120.91;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();

            line.MyPK = key + "_9";
            line.FK_MapData = "ND" + int.Parse(this.No + "01");
            line.X1 = (float)719.09;
            line.Y1 = (float)40;
            line.X2 = (float)719.09;
            line.Y2 = (float)482.73;
            line.BorderWidth = (float)2;
            line.BorderColor = "Black";
            line.Insert();
            #endregion

            this.CheckRpt();
            this.RepareV_FlowData_View();
        }
        protected override bool beforeUpdate()
        {
            Node.CheckFlow(this);
            return base.beforeUpdate();
        }
        public void DoDelete()
        {
            string sql = "";

            sql = "DELETE FROM WF_chofflow WHERE FK_Flow='" + this.No + "'";

            sql += "@GO DELETE  FROM WF_GenerWorkerlist WHERE FK_Flow='" + this.No + "'";

            sql += "@GO DELETE FROM  WF_GenerWorkFlow WHERE FK_Flow='" + this.No + "'";

            // ɾ����λ�ڵ㡣
            sql += "@GO DELETE  FROM  WF_NodeStation WHERE FK_Node in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            // ɾ������
            sql += "@GO DELETE FROM WF_Direction  WHERE Node in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            sql += "@GO DELETE FROM WF_Direction  WHERE ToNode in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            //ɾ������
            sql += "@GO DELETE FROM WF_NodeEmp  WHERE   FK_Node in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";


            //ɾ������.
            sql += "@GO DELETE WF_Listen WHERE FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

           

            // ɾ��d2d����.
            sql += "@GO DELETE WF_M2M WHERE FK_Node IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            // ɾ������.
            sql += "@GO DELETE WF_FAppSet WHERE NodeID IN (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";


            // ɾ������.
            sql += "@GO DELETE WF_FlowEmp WHERE FK_Flow='" + this.No + "' ";

            // ɾ������
            WFRpts rpts = new WFRpts(this.No);
            rpts.Delete();

            // �ⲿ��������
            sql += "@GO DELETE  FROM  WF_FAppSet WHERE  NodeID in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            // ɾ������
            sql += "@GO DELETE FROM WF_BillTemplate WHERE  NodeID in (SELECT NodeID FROM WF_Node WHERE FK_Flow='" + this.No + "')";

            Nodes nds = new Nodes(this.No);
            foreach (Node nd in nds)
            {
                string ndNo = "ND" + nd.NodeID;

                // ɾ���¼�.
                sql += "@GO DELETE Sys_FrmEvent WHERE FK_MapData='"+ndNo+"'";

                Sys.MapDtls dtls = new BP.Sys.MapDtls(ndNo);
                foreach (MapDtl dtl in dtls)
                {
                    sql += "@GO DELETE FROM Sys_MapAttr WHERE FK_MapData='" + dtl.No + "'";
                    try
                    {
                        DBAccess.RunSQL("DROP TABLE " + dtl.PTable);
                    }
                    catch
                    {
                    }
                }

                sql += "@GO DELETE  FROM  Sys_MapDtl WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM  Sys_FrmLine WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM  Sys_FrmLab WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM  Sys_FrmImg WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM  Sys_MapAttr WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM  Sys_GroupField WHERE EnName='ND" + nd.NodeID + "' OR EnName='' ";
                sql += "@GO DELETE  FROM Sys_MapData WHERE No='ND" + nd.NodeID + "'";


                sql += "@GO DELETE  FROM Sys_FrmBtn WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM Sys_FrmEvent WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM Sys_FrmLink WHERE FK_MapData='ND" + nd.NodeID + "'";

                sql += "@GO DELETE  FROM Sys_FrmAttachmentDB WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM Sys_FrmAttachment WHERE FK_MapData='ND" + nd.NodeID + "'";
                sql += "@GO DELETE  FROM Sys_FrmRB WHERE FK_MapData='ND" + nd.NodeID + "'";
            }

            sql += "@GO DELETE  FROM WF_Node WHERE FK_Flow='" + this.No + "'";
            sql += "@GO DELETE  FROM  WF_LabNote WHERE FK_Flow='" + this.No + "'";

            //ɾ��������Ϣ
            sql += "@GO DELETE FROM Sys_GroupField WHERE EnName NOT IN(SELECT NO FROM Sys_MapData)";

            #region ɾ�����̱���
            string fk_map = "ND" + int.Parse(this.No) + "Rpt";
            sql += "@GO DELETE  FROM  Sys_MapData WHERE No='" + fk_map + "'";
            sql += "@GO DELETE  FROM  Sys_MapAttr WHERE FK_MapData='" + fk_map + "'";
            try
            {
                BP.DA.DBAccess.RunSQL("DROP TABLE " + fk_map);
            }
            catch
            {

            }

            // ɾ����ϸ��
            MapDtls dtl1s = new MapDtls(fk_map);
            foreach (MapDtl dtl in dtl1s)
            {
                sql += "@GO DELETE  FROM  Sys_MapAttr WHERE FK_MapData='" + dtl.No + "'";
                BP.DA.DBAccess.RunSQL("DROP TABLE " + dtl.PTable);
            }
            sql += "@GO DELETE  FROM  Sys_MapDtl WHERE FK_MapData='" + fk_map + "'";
            //ɾ����ͼ.
            try
            {
                BP.DA.DBAccess.RunSQL("DROP VIEW V_" + this.No);
            }
            catch
            {
            }
            #endregion ɾ�����̱���

            // ִ��¼�Ƶ�sql scripts.
            BP.DA.DBAccess.RunSQLs(sql);
            this.Delete();

            this.RepareV_FlowData_View();
        }
        #endregion
    }
    /// <summary>
    /// ���̼���
    /// </summary>
    public class Flows : EntitiesNoName
    {
        #region ��ѯ
        public static void GenerHtmlRpts()
        {
            Flows fls = new Flows();
            fls.RetrieveAll();

            foreach (Flow fl in fls)
            {
                fl.DoCheck();
                fl.GenerHisHtmlDocRpt();
                fl.GenerFlowXmlTemplete();
            }

            // ������������
            string path = SystemConfig.PathOfWorkDir + @"\VisualFlow\DataUser\FlowDesc\";
            string msg = "";
            msg += "<html>";
            msg += "\r\n<title>.net��������������ƣ�����ģ��</title>";

            msg += "\r\n<body>";

            msg += "\r\n<h1>�۳�����ģ����</h1> <br><a href=index.htm >������ҳ</a> - <a href='http://ccFlow.org' >���ʳ۳ҹ������̹���ϵͳ������������ٷ���վ</a> ����ϵͳ��������ϵ:QQ:793719823,Tel:18660153393<hr>";

            foreach (Flow fl in fls)
            {
                msg += "\r\n <h3><b><a href='./" + fl.No + "/index.htm' target=_blank>" + fl.Name + "</a></b> - <a href='" + fl.No + ".gif' target=_blank  >" + fl.Name + "����ͼ</a></h3>";

                msg += "\r\n<UL>";
                Nodes nds = fl.HisNodes;
                foreach (Node nd in nds)
                {
                    msg += "\r\n<li><a href='./" + fl.No + "/" + nd.NodeID + "_" + nd.FlowName + "_" + nd.Name + "��.doc' target=_blank>����" + nd.Step + ", - " + nd.Name + "ģ��</a> -<a href='./" + fl.No + "/" + nd.NodeID + "_" + nd.Name + "_��ģ��.htm' target=_blank>Html��</a></li>";
                }
                msg += "\r\n</UL>";
            }
            msg += "\r\n</body>";
            msg += "\r\n</html>";

            string pathDef = SystemConfig.PathOfWorkDir + "\\VisualFlow\\DataUser\\FlowDesc\\" + SystemConfig.CustomerNo + "_index.htm";
            DataType.WriteFile(pathDef, msg);

            pathDef = SystemConfig.PathOfWorkDir + "\\VisualFlow\\DataUser\\FlowDesc\\index.htm";
            DataType.WriteFile(pathDef, msg);
            System.Diagnostics.Process.Start(SystemConfig.PathOfWorkDir + "\\VisualFlow\\DataUser\\FlowDesc\\");
        }
        #endregion ��ѯ

        #region ��ѯ
        /// <summary>
        /// �����ȫ�����Զ�����
        /// </summary>
        public void RetrieveIsAutoWorkFlow()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowAttr.FlowType, 1);

            qo.addOrderBy(FlowAttr.No);

            qo.DoQuery();
        }
        /// <summary>
        /// ��ѯ����ȫ�����������ڼ��ڵ�����
        /// </summary>
        /// <param name="flowSort">�������</param>
        /// <param name="IsCountInLifeCycle">�ǲ��Ǽ����������ڼ��� true ��ѯ����ȫ���� </param>
        public void Retrieve(string flowSort)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowAttr.FK_FlowSort, flowSort);
            qo.addOrderBy(FlowAttr.No);
            qo.DoQuery();
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public Flows() { }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="fk_sort"></param>
        public Flows(string fk_sort)
        {
            this.Retrieve(FlowAttr.FK_FlowSort, fk_sort);
        }
        #endregion

        #region �õ�ʵ��
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Flow();
            }
        }
        #endregion
    }
}


using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;

namespace BP.WF
{
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
        /// BookTable
        /// </summary>
        public const string BookTable = "BookTable";
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
        /// DateLit
        /// </summary>
        public const string DateLit = "DateLit";
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
    }
    /// <summary>
    /// ����
    /// ��¼�����̵���Ϣ��
    /// ���̵ı�ţ����ƣ�����ʱ�䣮
    /// </summary>
    public class Flow : EntityNoName
    {
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
        /// ���������
        /// </summary>
        public string BookTable_del
        {
            get
            {
                string book = this.GetValStringByKey(FlowAttr.BookTable);
                if (book == null || book == "")
                {
                    book = "BK_" + this.HisStartNode.HisWork.EnMap.PhysicsTable;
                    this.SetValByKey(FlowAttr.BookTable, book);
                    this.Update();
                }
                return book;
            }
        }
       
        /// <summary>
        /// ִ�и���
        /// </summary>
        /// <returns></returns>
        public string DoCopy()
        {
            return null;

            Flow fl = new Flow();
            fl.Copy(this);
            fl.No = this.GenerNewNoByKey(FlowAttr.No);
            fl.Name = "����:" + this.Name;
            fl.Insert();

            Nodes nds = this.HisNodes;
            foreach (Node nd in nds)
            {
                Node nnd = new Node();
                nnd.Copy(nd);
            }
        }
        /// <summary>
        /// У������
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            string msg = "<font color=blue>" + this.ToE("About", "����") + "��" + this.Name + " �� " + this.ToE("FlowCheckInfo", "���̼����Ϣ") + "</font><hr>";
            Nodes nds = new Nodes(this.No);
            Emps emps = new Emps();

            string rpt = "<html><title>" + this.Name + "-" + this.ToE("DesignRpt", "������ϱ���") + "</title></html>";
            rpt += "\t\n<body>\t\n<table width='70%' align=center border=1>\t\n<tr>\t\n<td>";
            rpt += "<div aligen=center><h1><b>" + this.Name + " - " + this.ToE("FlowDRpt", "��������ĵ�") + "</b></h1></div>";
            rpt += "<b>" + this.ToE("FlowDesc", "�������") + ":</b><br>";
            rpt += "&nbsp;&nbsp;" + this.Note;
            rpt += "<br><b>" + this.ToE("Info", "��Ϣ") + ":</b><br>";

            BookTemplates bks = new BookTemplates(this.No);

            rpt += "&nbsp;&nbsp;" + this.ToE("Step", "����") + ":" + nds.Count + "��" + this.ToE("Book", "����") + ":" + bks.Count + "����";

            #region �Խڵ���м��
            foreach (Node nd in nds)
            {
                try
                {
                    if (nd.IsCheckNode == false)
                        nd.HisWork.CheckPhysicsTable();
                }
                catch (Exception ex)
                {
                    rpt += "@" + nd.Name + " , �ڵ�����NodeWorkTypeText=" + nd.NodeWorkTypeText + "���ִ���.@err=" + ex.Message;
                }


                rpt += "<hr><b>" + this.ToEP1("NStep", "@��{0}��", nd.Step.ToString()) + "��" + nd.Name + "��</b><br>";
                rpt += this.ToE("Info", "��Ϣ") + "��";
                if (nd.IsCheckNode)
                {
                    rpt += this.ToE("CHTableDesc", "����ˡ������������ʱ�䡣"); // "����ˡ������������ʱ�䡣";
                }
                else
                {
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs("ND" + nd.NodeID);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "��";
                    }
                }

                // ��ϸ���顣
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                foreach (Sys.MapDtl dtl in dtls)
                {
                    dtl.HisGEDtl.CheckPhysicsTable();

                    rpt += "<br>" + this.ToE("Dtl", "��ϸ") + "��" + dtl.Name;
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs(dtl.No);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "��";
                    }
                }

                rpt += "<br>" + this.ToE("Station", "��λ") + "��";

                // ��λ�Ƿ�������ȷ��
                msg += "<b>��" + nd.Name + "����</b>" + this.ToE("NodeWorkType", "�ڵ�����") + "��" + nd.HisNodeWorkType + "<hr>";
                if (nd.HisStations.Count == 0)
                {
                    msg += "<font color=red>" + this.ToE("Error", "����") + "��" + nd.Name + " " + this.ToE("NoSetSta", "û�����ø�λ") + "��</font>";
                }
                else
                {
                    msg += this.ToE("Station", "��λ") + "��";
                    foreach (BP.Port.Station st in nd.HisStations)
                    {
                        msg += st.Name + "��";
                        rpt += st.Name + "��";
                    }

                    emps.RetrieveInSQL("select fk_emp from port_empstation where fk_station in (select fk_station from wf_nodestation where fk_node='" + nd.NodeID + "' )");
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

                //��������м�顣
                BookTemplates books = nd.HisBookTemplates;
                if (books.Count == 0)
                {
                    msg += "";
                }
                else
                {
                    msg += this.ToE("Book", "����");
                    rpt += "<br>" + this.ToE("Book", "����") + "��";
                    foreach (BookTemplate book in books)
                    {
                        msg += book.Name + "��";
                        rpt += book.Name + "��";
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
                Conds conds = new Conds(CondType.Node, nd.NodeID, 1 );
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
                            msg += "<font color=red>�趨�ķ��������Ľڵ��Ѿ���ɾ���ˣ�����ϵͳ�Զ�ɾ�������������</font>";
                            cond.Delete();
                            continue;
                        }

                        try
                        {
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
            }
            #endregion

            msg += "<br><a href='../../Data/FlowDesc/" + this.Name + ".htm' target=_s>" + this.ToE("DesignRpt", "��Ʊ���") + "</a>";
            msg += "<hr><b> </b> &nbsp; <br>" + DataType.CurrentDataTimeCN + "<br><br><br>";

            rpt += "\t\n</td></tr>\t\n</table>\t\n</body>\t\n</html>";

            try
            {
                BP.DA.DataType.SaveAsFile(BP.SystemConfig.PathOfData + "\\FlowDesc\\" + this.Name + ".htm", rpt);
            }
            catch
            {
            }

            msg += "<hr>��ʼ�����������Ƿ�����<hr>";

            emps = new Emps();
            emps.RetrieveAllFromDBSource();
            foreach (Emp emp in emps)
            {
                Dept dept = new Dept();
                dept.No = emp.FK_Dept;
                if (dept.IsExits == false)
                    msg += "@��Ա:" + emp.Name + "�����ű��{" + dept.Name + "}����ȷ.";

                EmpStations ess = new EmpStations();
                ess.Retrieve(EmpStationAttr.FK_Emp, emp.No);
                if (ess.Count == 0)
                    msg += "@��Ա:" + emp.No + "," + emp.Name + ",û�й�����λ��";


                EmpDepts eds = new EmpDepts();
                eds.Retrieve(EmpStationAttr.FK_Emp, emp.No);
                if (eds.Count == 0)
                    msg += "@��Ա:" + emp.No + "," + emp.Name + ",û�й������š�";
            }

            ////Depts depts =new Depts();
            ////depts.RetrieveAll();
            //string sql = "SELECT * FROM PORT_EMP WHERE FK_DEPT NOT IN (SELECT NO FROM PORT_DEPT)";


            return msg;
        }

        public string DoCheck_del()
        {
            string msg = "<font color=blue>" + this.ToE("About", "����") + "��" + this.Name + " �� " + this.ToE("FlowCheckInfo", "���̼����Ϣ") + "</font><hr>";
            Nodes nds = new Nodes(this.No);
            Emps emps = new Emps();

            string rpt = "<html><title>" + this.Name + "-" + this.ToE("DesignRpt", "������ϱ���") + "</title></html>";
            rpt += "\t\n<body>\t\n<table width='70%' align=center border=1>\t\n<tr>\t\n<td>";
            rpt += "<div aligen=center><h1><b>" + this.Name + " - " + this.ToE("FlowDRpt", "��������ĵ�") + "</b></h1></div>";
            rpt += "<b>" + this.ToE("FlowDesc", "�������") + ":</b><br>";
            rpt += "&nbsp;&nbsp;" + this.Note;
            rpt += "<br><b>" + this.ToE("Info", "��Ϣ") + ":</b><br>";

            BookTemplates bks = new BookTemplates(this.No);
            rpt += "&nbsp;&nbsp;" + this.ToE("Step", "����") + ":" + nds.Count + "��" + this.ToE("Book", "����") + ":" + bks.Count + "����";

            #region �Խڵ���м��
            foreach (Node nd in nds)
            {
                if (nd.IsCheckNode == false)
                    nd.HisWork.CheckPhysicsTable();


                rpt += "<hr><b>" + this.ToEP1("NStep", "@��{0}��", nd.Step.ToString()) + "��" + nd.Name + "��</b><br>";
                rpt += this.ToE("Info", "��Ϣ") + "��";
                if (nd.IsCheckNode)
                {
                    rpt += this.ToE("CHTableDesc", "����ˡ������������ʱ�䡣"); // "����ˡ������������ʱ�䡣";
                }
                else
                {
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs("ND" + nd.NodeID);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "��";
                    }
                }
                // ��ϸ���顣
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.No);
                foreach (Sys.MapDtl dtl in dtls)
                {
                    dtl.HisGEDtl.CheckPhysicsTable();

                    rpt += "<br>" + this.ToE("Dtl", "��ϸ") + "��" + dtl.Name;
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs(dtl.No);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "��";
                    }
                }
                rpt += "<br>" + this.ToE("Station", "��λ") + "��";

                // ��λ�Ƿ�������ȷ��
                msg += "<b>��" + nd.Name + "����</b>" + this.ToE("NodeWorkType", "�ڵ�����") + "��" + nd.HisNodeWorkType + "<hr>";
                if (nd.HisStations.Count == 0)
                {
                    msg += "<font color=red>" + this.ToE("Error", "����") + "��" + nd.Name + " " + this.ToE("NoSetSta", "û�����ø�λ") + "��</font>";
                }
                else
                {
                    msg += this.ToE("Station", "��λ") + "��";
                    foreach (BP.Port.Station st in nd.HisStations)
                    {
                        msg += st.Name + "��";
                        rpt += st.Name + "��";
                    }

                    emps.RetrieveInSQL("select fk_emp from port_empstation where fk_station in (select fk_station from wf_nodestation where fk_node='" + nd.NodeID + "' )");
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

                //��������м�顣
                BookTemplates books = nd.HisBookTemplates;
                if (books.Count == 0)
                {
                    msg += "";
                }
                else
                {
                    msg += this.ToE("Book", "����");
                    rpt += "<br>" + this.ToE("Book", "����") + "��";
                    foreach (BookTemplate book in books)
                    {
                        msg += book.Name + "��";
                        rpt += book.Name + "��";
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
                        msg += cond.AttrKey + cond.AttrName + cond.OperatorValue + "��";
                        rpt += cond.AttrKey + cond.AttrName + cond.OperatorValue + "��";
                    }
                }
                msg += "<br>";
            }
            #endregion

            msg += "<br><a href='../../Data/FlowDesc/" + this.Name + ".htm' target=_s>" + this.ToE("DesignRpt", "��Ʊ���") + "</a>";
            msg += "<hr><b> </b> &nbsp; <br>" + DataType.CurrentDataTimeCN + "<br><br><br>";

            rpt += "\t\n</td></tr>\t\n</table>\t\n</body>\t\n</html>";
            BP.DA.DataType.SaveAsFile(BP.SystemConfig.PathOfData + "\\FlowDesc\\" + this.Name + ".htm", rpt);
            return msg;
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
        /// ��ŵ�������
        /// </summary>
        public int DateLit
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.DateLit);
            }
            set
            {
                this.SetValByKey(FlowAttr.DateLit, value);
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
                return this.GetValStringByKey("Note");
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
                return  this.GetValRefTextByKey(FlowAttr.DocType);
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
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, null, true, true, 1, 10, 3);
                map.AddTBString(FlowAttr.Name, null, null, true, false, 0, 50, 10);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "�������"), new FlowSorts(), false);


                map.AddTBInt(FlowAttr.FlowType, (int)FlowType.Panel, "��������", false, false);

                // @0=ҵ������@1=��������.
                map.AddTBInt(FlowAttr.FlowSheetType, (int)FlowSheetType.SheetFlow, "������", false, false);


                map.AddDDLSysEnum(FlowAttr.DocType, (int)DocType.OfficialDoc, "��������(�Թ�����Ч)", false, false, FlowAttr.DocType,"@0=��ʽ����@1=�㺯");
                map.AddDDLSysEnum(FlowAttr.XWType, (int)XWType.Down, "��������(�Թ�����Ч)", false, false, FlowAttr.XWType, "@0=������@1=ƽ����@2=������");


                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, "���з�ʽ", false, false, FlowAttr.FlowRunWay,
                    "@0=�ֹ�����@1=��������@2=��������@3=��������@4=��������@5=��Сʱ����");
                map.AddTBString(FlowAttr.FlowRunObj, null, "��������", true, false, 0, 100, 10);

                map.AddTBString(FlowAttr.Note, null, null, true, false, 0, 100, 10);
                map.AddTBInt(FlowAttr.DateLit, 30, "��ŵ����(0��ʾ���´�)", false, false);


                map.AddBoolean(FlowAttr.IsOK, true, "�Ƿ�����", true, true);
                map.AddBoolean(FlowAttr.IsCCAll, false, "������ɺ��Ͳ�����Ա", true, true);
                map.AddTBString(FlowAttr.CCStas, null, "Ҫ���͵ĸ�λ", false, false, 0, 2000, 10);

                map.AddTBDecimal(FlowAttr.AvgDay, 0, "ƽ����������", false, false);


                map.AddSearchAttr(FlowAttr.FK_FlowSort);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "��Ƽ�鱨��"); // "��Ƽ�鱨��";
                rm.ToolTip = "���������Ƶ����⡣";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("ViewDef", "��ͼ����"); //"��ͼ����";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoDRpt";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("RptRun", "��������"); // "��������";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowDataOut", "����ת������");  //"����ת������";
                //  rm.Icon = "/Images/Btn/Table.gif";
                rm.ToolTip = "���������ʱ�䣬��������ת���浽����ϵͳ��Ӧ�á�";

                rm.ClassMethodName = this.ToString() + ".DoExp";
                map.AddRefMethod(rm);

                map.AttrsOfOneVSM.Add(new FlowStations(), new Stations(), FlowStationAttr.FK_Flow, FlowStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "���͸�λ");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override bool beforeUpdateInsertAction()
        {
            if (this.FK_FlowSort == "00")
                this.HisFlowSheetType = FlowSheetType.DocFlow;
            else
                this.HisFlowSheetType = FlowSheetType.SheetFlow;

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
            BP.PubClass.WinOpen("../WF/SelfWFRpt.aspx?FK_Flow=" + this.No + "&DoType=Edit&RefNo=" + this.No, 700, 400);
            return null;
        }
        /// <summary>
        /// ִ���½�
        /// </summary>
        public void DoNewFlow()
        {
            this.No = this.GenerNewNoByKey(FlowAttr.No);
            if (this.No.Substring(0, 1) == "1")
                this.No = "100";

            this.Name = BP.Sys.Language.GetValByUserLang("NewFlow", "�½�����") + this.No; //�½�����
            this.Save();


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
            nd.Save();
            nd.CreateMap();


            nd = new Node();
            nd.NodeID = int.Parse(this.No + "99");
            nd.Name = BP.Sys.Language.GetValByUserLang("EndNode", "�����ڵ�"); // "�����ڵ�";
            nd.Step = 1;
            nd.FK_Flow = this.No;
            nd.FlowName = this.Name;
            nd.HisNodePosType = NodePosType.End;
            nd.HisNodeWorkType = NodeWorkType.WorkHL;
            nd.X = 100;
            nd.Y = 200;
            nd.Save();
            nd.CreateMap();
        }
        protected override bool beforeUpdate()
        {
            Node.CheckFlow(this);
            return base.beforeUpdate();
        }
        public void DoDelete()
        {
            string sql = "";
            sql = " DELETE wf_chofflow WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            sql = " DELETE wf_generworkerlist WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            sql = " DELETE wf_generworkflow WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            // ɾ����λ�ڵ㡣
            sql = "  DELETE from wf_NodeStation  where   FK_Node in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            // ɾ������
            sql = "  DELETE from wf_direction  where   node in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);
            sql = "  DELETE from wf_direction  where   tonode in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            //// ɾ������,����.
            //sql = "  DELETE from wf_nodecompletecondition  where   nodeid in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            //DBAccess.RunSQL(sql);
            //sql = "  DELETE from wf_globalcompletecondition  where   fk_flow='" + this.No + "'";
            //DBAccess.RunSQL(sql);
            //sql = "  DELETE from wf_directioncondition  where   nodeid in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            //DBAccess.RunSQL(sql);

            // ɾ������
            WFRpts rpts = new WFRpts(this.No);
            rpts.Delete();

            // �ⲿ��������
            sql = " DELETE WF_FAppSet WHERE  NodeID in (select NodeID from WF_Node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            // ɾ������
            sql = " DELETE WF_BookTemplate WHERE  NodeID in (SELECT NodeID from WF_Node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            Nodes nds = new Nodes(this.No);
            foreach (Node nd in nds)
            {
                sql = " DELETE Sys_MapData WHERE No='ND" + nd.NodeID + "'";
                DBAccess.RunSQL(sql);

                sql = " DELETE Sys_MapAttr WHERE FK_MapData='ND" + nd.NodeID + "'";
                DBAccess.RunSQL(sql);

                if (nd.IsCheckNode == false)
                {
                    try
                    {
                        sql = " DROP TABLE ND" + nd.NodeID;
                        DBAccess.RunSQL(sql);
                    }
                    catch
                    {
                    }
                }


                // ɾ����ϸ��Ϣ��
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                dtls.Delete();
            }

            sql = " DELETE WF_Node WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            sql = " DELETE WF_LabNote WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            this.Delete();
        }
        #endregion
    }
    /// <summary>
    /// ���̼���
    /// </summary>
    public class Flows : EntitiesNoName
    {
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


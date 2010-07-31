using System;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Web;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// WF ��ժҪ˵����
    /// ������.
    /// �����������������
    /// ��������Ϣ��
    /// ���̵���Ϣ��
    /// </summary>
    public class WorkNode
    {
        public string ToE(string s, string chName)
        {
            return BP.Sys.Language.GetValByUserLang(s, chName);
        }
        public string ToEP1(string s, string chName, string v)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v);
        }
        public string ToEP2(string s, string chName, string v, string v1)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v, v1);
        }
        public string ToEP3(string s, string chName, string v, string v1, string v2)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v, v1, v2);
        }

        #region Ȩ���ж�
        /// <summary>
        /// �ж�һ�����ܲ��ܶ���������ڵ���в�����
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        private bool IsCanOpenCurrentWorkNode(string empId)
        {
            NodeState stat = this.HisWork.NodeState;
            if (stat == NodeState.Init)
            {
                if (this.HisNode.IsStartNode)
                {
                    /*����ǿ�ʼ�����ڵ㣬�ӹ�����λ�ж�����û�й�����Ȩ�ޡ�*/
                    return WorkFlow.IsCanDoWorkCheckByEmpStation(this.HisNode.NodeID, empId);
                }
                else
                {
                    /* ����ǳ�ʼ���׶�,�ж����ĳ�ʼ���ڵ� */
                    WorkerList wl = new WorkerList();
                    wl.WorkID = this.HisWork.OID;
                    wl.FK_Emp = empId;
                    wl.FK_Node = this.HisNode.NodeID;
                    return wl.IsExits;
                }
            }
            else
            {
                /* ����ǳ�ʼ���׶� */
                return false;
            }
        }
        #endregion

        //��ѯ��ÿ���ڵ����Ľ����˼��ϣ�Emps����
        public string GenerEmps(Node nd)
        {
            string str = "";
            foreach (WorkerList wl in this.HisWorkerLists)
                str = wl.FK_Emp + ",";

            if (this.HisWorkerLists.Count == 0)
            {
                try
                {
                    Log.DefaultLogWriteLineError("@" + this.ToE("ErrEmpNull", "������Ա����Ϊ��") + "��WorkID=" + this.WorkID + "@FK_Flow=" + this.HisNode.FK_Flow);
                }
                catch
                {
                }
            }
            return str;
        }
        /// <summary>
        ///  �����ݵ��ȵ�����ϵͳ��
        /// </summary>
        /// <param name="wn"></param>
        private CHOfNode DTSDataToChOfNode(WorkNode wn)
        {
            return null;
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == false)
                return null;

            //   return null;
            Work wk = wn.HisWork;
            Emp emp = wk.HisRec;
            Node nd = wn.HisNode;

            CHOfNode cn = new CHOfNode();
            cn.NodeState = NodeState.Complete;
            cn.WorkID = wk.OID;
            cn.FK_Node = wn.HisNode.NodeID;
            cn.RDT = wk.RDT;
            cn.CDT = wk.CDT;
            cn.SDT = DataType.AddDays(wk.RDT, nd.NeedCompleteDays).ToString(DataType.SysDataFormat);
            // cn.FK_NY = wk.Record_FK_NY;
            cn.FK_NY = DataType.CurrentYearMonth;
            cn.FK_Emp = wk.RecOfEmp.No;
            cn.FK_Station = wn.HisStationOfUse.No;
            cn.FK_Dept = wn.HisDeptOfUse.No;
            cn.NodeState = wk.NodeState;
            cn.FK_Dept = emp.FK_Dept;
            cn.FK_Flow = nd.FK_Flow;
            cn.SpanDays = DataType.SpanDays(wk.RDT, wk.CDT);
            cn.Note = "null "; // �۷�ԭ��.

            //cn.Emps = wk.Emps ; //Emps ;
            string emps = "";
            //if (wk.Emps.IndexOf(",") == -1)
            //{
            //    emps = wk.Emps + ",";
            //}
            //if (wk.Emps.IndexOf(",") != -1)
            //{
            //    emps = wk.Emps;
            //}
            //cn.Emps = emps;


            //ֻҪ��ĳ���ڵ�����ִ��ֶΣ�Spec,��ô�Ͱ�Spec�������д�� WF_CHOfNode ����
            if (wk.EnMap.Attrs.Contains("SPEC"))
                cn.Spec = wk.GetValStringByKey("SPEC");

            cn.CentOfAdd = nd.SwinkCent; //�������÷֡�
            cn.NodeDeductCent = nd.DeductCent;
            cn.NodeDeductDays = nd.DeductDays;
            cn.NodeMaxDeductCent = nd.MaxDeductCent;

            if (cn.SpanDays > 0)
            {
                cn.CentOfCut = cn.NodeDeductCent * (cn.SpanDays - nd.NeedCompleteDays);
                if (cn.CentOfCut > cn.NodeMaxDeductCent)
                    cn.CentOfCut = cn.NodeMaxDeductCent;

                cn.Note = this.ToE("WN1", "δ������ɹ�����"); // "δ������ɹ�����"; // �۷�ԭ��
                if (cn.CentOfCut <= 0)
                    cn.CentOfCut = 0;
            }

            cn.Cent = cn.CentOfCut + cn.CentOfAdd;

            //if (cn.NodeState == NodeState.Complete)
            //{
            int i = DBAccess.RunSQLReturnValInt("select count(*) from WF_CHOfNode where fk_emp='" + emp.No + "' AND fk_node='" + nd.NodeID + "'");
            if (i == 0)
                cn.Insert();
            else
                cn.Update();
            //}
            //else
            //{
            //    cn.Save();
            //}
            return cn;
        }
        private string nextStationName = "";
        /// <summary>
        /// ������һ���Ĺ�����
        /// </summary>
        /// <param name="town">WorkNode</param>
        /// <returns></returns>
        public WorkerLists InitWorkerLists_QingHai(WorkNode town)
        {
            DataTable dt = new DataTable();
            // dt = DBAccess.RunSQLReturnTable("SELECT NO,NAME FROM PUB_EMP WHERE 1=2 ");
            dt.Columns.Add("No", typeof(string));

            string sql;
            string fk_emp;

            // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ����������Ϊ�˱������
            DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            // �жϵ�ǰ�ڵ��Ƿ�ɼ���Ŀ����Ա.(����������)
            if (this.HisWork.EnMap.Attrs.Contains("FK_Emp"))
            {
                //sql="SELECT No,Name FROM PUB_EMP WHERE NO='"+this.HisWork.GetValStringByKey("FK_Emp")+"'";
                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                //dt=DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }
            //����־�(��)��
            if (this.HisWork.EnMap.Attrs.Contains("FK_FJZ"))
            {
                fk_emp = this.HisWork.GetValStringByKey("FK_FJZ");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);

                //sql = "SELECT No,Name FROM PUB_EMP WHERE NO='" + this.HisWork.GetValStringByKey("FK_FJZ") + "'";
                //dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            //�ӹ켣�����ѯ.
            sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
               + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') "
               + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + ")";
            dt = DBAccess.RunSQLReturnTable(sql);

            // ����ܹ��ҵ�.
            if (dt.Rows.Count >= 1)
            {
                return WorkerListWayOfDept(town, dt);
            }

            // û�в�ѯ��������¡�
            Stations nextStations = town.HisNode.HisStations;
            // �Ҳ���, ��Ҫ�ж��������⡣
            Station fromSt = this.HisStationOfUse;
            string DeptofUse = this.HisDeptOfUse.No;
            if (nextStations.Count == 0)
                throw new Exception(this.ToEP1("WF2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));  //����Աά��������û�и��ڵ�["+town.HisNode.Name+"]���ù�����λ��

            Station toSt = (Station)nextStations[0];
            if (fromSt.No == toSt.No)
            {
#warning edit 2008 - 05-28 .
                // sql = "SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_Dept FROM PORT_EMPDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM PORT_EMPSTATION WHERE FK_Station='" + toSt.No + "')";
                sql = "SELECT No,Name FROM Port_Emp WHERE NO='" + WebUser.No + "'";  // IN (SELECT FK_Dept FROM PORT_EMPDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM PORT_EMPSTATION WHERE FK_Station='" + toSt.No + "')";
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    fk_emp = this.HisWork.GetValStringByKey("FK_FJZ");
                    DataRow dr = dt.NewRow();
                    dr[0] = fk_emp;
                    dt.Rows.Add(dr);
                    return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    return WorkerListWayOfDept(town, dt);
                }
            }

            if (fromSt.Grade < toSt.Grade)
            {
            }
            else if (fromSt.Grade == toSt.Grade)
            {
            }
            else
            {
            }
            return WorkerListWayOfDept(town, dt);
        }

        public WorkerLists InitWorkerLists_Gener(WorkNode town)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;

            // ���ִ�������η��ͣ���ǰһ�εĹ켣����Ҫ��ɾ����������Ϊ�˱������
            DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            if (this.HisNode.IsSelectEmp)
            {
                sql = "SELECT  FK_Emp  FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            // �жϵ�ǰ�ڵ��Ƿ�ɼ���Ŀ����Ա
            if (this.HisWork.EnMap.Attrs.Contains("FK_Emp"))
            {
                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // �ж� �ڵ���Ա���Ƿ������ã�  ����оͲ����Ǹ�λ�����ˡ��ӽڵ���Ա���������ѯ��
            if (town.HisNode.HisEmps.Length > 2)
            {
                string[] emps = town.HisNode.HisEmps.Split('@');
                foreach (string emp in emps)
                {
                    if (emp == null || emp == "")
                        continue;
                    DataRow dr = dt.NewRow();
                    dr[0] = emp;
                    dt.Rows.Add(dr);
                    return WorkerListWayOfDept(town, dt);
                }
            }



            // �жϽڵ㲿�������Ƿ������˲��ţ���������ˣ��Ͱ������Ĳ��Ŵ���
            if (town.HisNode.IsSetDept)
            {
                if (town.HisNode.HisStations.Count == 0)
                {
                    sql = "SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count > 0)
                        return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    sql = "SELECT NO FROM PORT_EMP WHERE NO IN ";
                    sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                    sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    sql += ")";
                    sql += "AND NO IN ";
                    sql += "(";
                    sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                    sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    sql += ")";
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count > 0)
                        return WorkerListWayOfDept(town, dt);
                }
            }

            if (this.HisNode.IsStartNode == false)
            {
                // �����ǰ�Ľڵ㲻�ǿ�ʼ�ڵ㣬 �ӹ켣�����ѯ��
                sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                   + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') "
                   + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs) + ") )";
                dt = DBAccess.RunSQLReturnTable(sql);

                // ����ܹ��ҵ�.
                if (dt.Rows.Count >= 1)
                {
                    if (dt.Rows.Count == 1)
                    {
                        /*�����Աֻ��һ���������˵��������Ҫ */
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }

            // û�в�ѯ���������, �Ȱ��ձ����ż��㡣
            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception("�ڵ�û�и�λ:" + town.HisNode.NodeID + "  " + town.HisNode.Name);
                // throw new Exception(this.ToEP1("WN2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }


            // û�в�ѯ���������, �������ƥ�������㡣
            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception("�ڵ�û�и�λ:" + town.HisNode.NodeID+"  " + town.HisNode.Name);

                //  throw new Exception(this.ToEP1("WN2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // û�в�ѯ���������, �������ƥ���� ���һ������ ���㡣
            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 2) + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception("�ڵ�û�и�λ:" + town.HisNode.NodeID+"  " + town.HisNode.Name);
                    //throw new Exception(this.ToEP1("WF2", "@��������{0}�Ѿ���ɡ�", town.HisNode.Name));
                else
                {
                    try
                    {
                        sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
                 + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
                 + " AND  NO IN "
                 + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 4) + "%')";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
                + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )";
                            dt = DBAccess.RunSQLReturnTable(sql);

                            if (dt.Rows.Count == 0)
                                throw new Exception(this.ToE("WF3", "û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��") + town.HisNode.Name); //"ά����������[" + town.HisNode.Name + "]ά���ĸ�λ���Ƿ�����Ա��"
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(this.ToE("WF3", "û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��") + town.HisNode.Name); //"ά����������[" + town.HisNode.Name + "]ά���ĸ�λ���Ƿ�����Ա��"
                    }

                    return WorkerListWayOfDept(town, dt);
                }
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backtoNodeID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public WorkNode DoReturnWork(int backtoNodeID, string msg)
        {
            // �ı䵱ǰ�Ĺ����ڵ㣮
            WorkNode wn = new WorkNode(this.WorkID, backtoNodeID);
            // ���� return work ״̬��
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID, this.HisNode.FK_Flow);
            gwf.FK_Node = backtoNodeID;
            gwf.DirectUpdate();

            // ɾ��������.
            WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            wkls.Delete();

            // д����־.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wn.HisWork.OID;
            rw.NodeId = wn.HisNode.NodeID;
            rw.Note = msg;
            rw.Save();

            WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);

            // ɾ���˻�ʱ��ǰ�ڵ�Ĺ�����Ϣ��
            this.HisWork.Delete();

            //ɾ���˻�ʱ��ǰ�ڵ�Ŀ�����Ϣ��
            CHOfNode ch = new CHOfNode();
            int i = ch.Delete(CHOfNodeAttr.FK_Node, wn.HisNode.NodeID, CHOfNodeAttr.WorkID, wn.HisWork.OID);
            return wn;
        }

        /// <summary>
        /// ���ݽڵ�ID��ɾ����������
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="workid">����ID</param>
        public void DeleteNodeData(int nodeid, Int32 workid)
        {
        }
        /// <summary>
        /// ִ���˻�
        /// </summary>
        /// <param name="msg">�˻ع�����ԭ��</param>
        /// <returns></returns>
        public WorkNode DoReturnWork(string msg)
        {
            // �ı䵱ǰ�Ĺ����ڵ㣮
            WorkNode wn = this.GetPreviousWorkNode();
            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID, this.HisNode.FK_Flow);
            gwf.FK_Node = wn.HisNode.NodeID;
            gwf.DirectUpdate();

            // ���� return work ״̬��
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            // ɾ��������.
            WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            wkls.Delete();

            // д����־.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wn.HisWork.OID;
            rw.NodeId = wn.HisNode.NodeID;
            rw.Note = msg;
            rw.Save();

            //			WorkFlow wf = this.HisWorkFlow;
            //			wf.WritLog(msg);
            // ��Ϣ֪ͨ��һ��������Ա����
            WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "�˻صĹ���", wn.HisNode.Name, "�˻صĹ���");

            // ɾ���˻�ʱ��ǰ�ڵ�Ĺ�����Ϣ��
            this.HisWork.Delete();

            //ɾ���˻�ʱ��ǰ�ڵ�Ŀ�����Ϣ��
            CHOfNode ch = new CHOfNode();
            int i = ch.Delete(CHOfNodeAttr.FK_Node, wn.HisNode.NodeID, CHOfNodeAttr.WorkID, wn.HisWork.OID);
            //if (i == 0)
            //    throw new Exception("ɾ������");
            /*
            // ������Ϣ����Ӧ�Ĺ�����Ա��
            Sys.SysMsg sm = new BP.Sys.SysMsg();
            sm.ToEmp=","+wn.HisWork.RecOfEmp.No+",";
            sm.FromEmp=Web.WebUser.No;
            sm.Title="�����˻�֪ͨ";
            sm.Doc=msg;
            sm.PRI=1;
            sm.Save();
            */
            return wn;
        }
        /// <summary>
        /// ִ�й������
        /// </summary>
        public string DoSetThisWorkOver()
        {
            this.HisWork.AfterWorkNodeComplete();

            // ����״̬��
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.SetValByKey("CDT", DataType.CurrentDataTime);
            this.HisWork.Rec = Web.WebUser.No;
            this.HisWork.DirectUpdate();

            // ��������Ĺ�����.
            string sql = "";
            switch (this.HisNode.HisNodeWorkType)
            {
                //case NodeWorkType.MulWorks:
                case NodeWorkType.GECheckMuls:
                    break;
                default:
                    sql = "DELETE WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp!='" + this.HisWork.Rec.ToString() + "'";
                    DBAccess.RunSQL(sql);
                    break;
            }
            return "";
        }
        /// <summary>
        /// ���� "ְ��" �õ����ܹ�ִ��������ļ���
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDuty(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDuty  a, Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty ) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// ���� "����" �õ����ܹ�ִ��������ļ���
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDept(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDept  a, Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }

        #region ���ݹ�����λ���ɹ�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="town"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt)
        {
            if (dt.Rows.Count == 0)
                throw new Exception(this.ToE("WN4", "������Ա�б�Ϊ��")); // ������Ա�б�Ϊ��

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].ToString() == WebUser.No)
                {
                    /*���������*/

                }
            }

            Int64 workID = this.HisWork.OID;
            int toNodeId = town.HisNode.NodeID;

            this.HisWorkerLists = new WorkerLists();

#warning ����ʱ��  town.HisNode.DeductDays-1

            // 2008-01-22 ֮ǰ�Ķ�����
            //int i = town.HisNode.DeductDays;
            //dtOfShould = DataType.AddDays(dtOfShould, i);
            //if (town.HisNode.WarningDays > 0)
            //    dtOfWarning = DataType.AddDays(dtOfWarning, i - town.HisNode.WarningDays);
            // edit at 2008-01-22 , ����Ԥ�����ڵ����⡣

            DateTime dtOfShould = DataType.AddDays(DateTime.Now, town.HisNode.DeductDays);
            DateTime dtOfWarning = DateTime.Now;
            if (town.HisNode.WarningDays > 0)
                dtOfWarning = DataType.AddDays(dtOfShould, -town.HisNode.WarningDays); // dtOfShould.AddDays(-town.HisNode.WarningDays);


            this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node, town.HisNode.NodeID,
                "SDT", dtOfShould.ToString("MM-dd"));

            if (dt.Rows.Count == 1)
            {
                /* ���ֻ��һ����Ա */
                WorkerList wl = new WorkerList();
                wl.WorkID = workID;
                wl.FK_Node = toNodeId;
                wl.FK_Emp = dt.Rows[0][0].ToString();

                Emp emp = new Emp(wl.FK_Emp);
                wl.FK_EmpText = emp.Name;
                wl.FK_Dept = emp.FK_Dept;
                wl.WarningDays = town.HisNode.WarningDays;
                wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);

                wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                wl.RDT = DateTime.Now.ToString(DataType.SysDataFormat);
                wl.FK_Flow = town.HisNode.FK_Flow;
                wl.FID = town.HisWork.FID;
                wl.DirectInsert();

                this.HisWorkerLists.AddEntity(wl);

                RememberMe rm = new RememberMe(); // this.GetHisRememberMe(town.HisNode);
                rm.Objs = "@" + wl.FK_Emp + "@";
                rm.ObjsExt = wl.FK_Emp + "<" + wl.FK_EmpText + ">";
                rm.Emps = "@" + wl.FK_Emp + "@";
                rm.EmpsExt = wl.FK_Emp + "<" + wl.FK_EmpText + ">";
                this._RememberMe = rm;
            }
            else
            {
                /* ����ж����Ա */

                RememberMe rm = this.GetHisRememberMe(town.HisNode);

                // �������Ƿ���ڵ�ǰ����Ա��
                bool isHaveIt = false;
                string emps = "@";
                foreach (DataRow dr in dt.Rows)
                {
                    string fk_emp = dr[0].ToString();
                    if (rm.Objs.IndexOf("@" + fk_emp) != -1)
                        isHaveIt = true;

                    emps += fk_emp + "@";
                }

                if (isHaveIt == false)
                {
                    /* ��������û�е�ǰ���ɵĲ�����Ա */
                    /* �Ѿ���֤��û���ظ�����Ա��*/
                    string myemps = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (myemps.IndexOf(dr[0].ToString()) != -1)
                            continue;

                        myemps += "@" + dr[0].ToString();

                        WorkerList wl = new WorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_Emp = dr[0].ToString();

                        Emp emp = new Emp();
                        try
                        {
                            emp = new Emp(wl.FK_Emp);
                        }
                        catch
                        {
                            continue;
                        }

                        wl.FK_EmpText = emp.Name;

                        wl.FK_Dept = emp.FK_Dept;

                        //    wl.UseDept = this.HisDeptOfUse.No;
                        //    wl.UseStation = this.HisStationOfUse.No;
                        wl.WarningDays = town.HisNode.WarningDays;

                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;

                        try
                        {
                            wl.DirectInsert();
                        }
                        catch (Exception ex)
                        {
                            Log.DefaultLogWriteLineError("��Ӧ�ó��ֵ��쳣��Ϣ��" + ex.Message);
                        }
                        this.HisWorkerLists.AddEntity(wl);
                    }
                }
                else
                {
                    string[] strs = rm.Objs.Split('@');
                    string myemps = "";
                    foreach (string s in strs)
                    {
                        if (s.Length < 3)
                            continue;

                        if (myemps.IndexOf(s) != -1)
                            continue;

                        myemps += "@" + s;

                        WorkerList wl = new WorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_Emp = s;

                        Emp emp = new Emp();
                        try
                        {
                            emp = new Emp(wl.FK_Emp);
                        }
                        catch
                        {
                            continue;
                        }

                        wl.FK_EmpText = emp.Name;

                        wl.FK_Dept = emp.FK_Dept;

                        //wl.UseDept = this.HisDeptOfUse.No;
                        //wl.UseStation = this.HisStationOfUse.No;
                        wl.WarningDays = town.HisNode.WarningDays;
                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;


                        try
                        {
                            if (town.HisNode.IsFLHL == false)
                                wl.DirectInsert();
                        }
                        catch
                        {
                            continue;
                        }
                        this.HisWorkerLists.AddEntity(wl);
                    }
                }

                string objsmy = "@";
                foreach (WorkerList wl in this.HisWorkerLists)
                {
                    objsmy += wl.FK_Emp + "@";
                }

                if (rm.Emps != emps || rm.Objs != objsmy)
                {
                    /* ������Ա�б����˱仯 */
                    rm.Emps = emps;
                    rm.Objs = objsmy;

                    string objExts = "";
                    foreach (WorkerList wl in this.HisWorkerLists)
                    {
                        if (Glo.IsShowUserNoOnly)
                            objExts += wl.FK_Emp + "��";
                        else
                            objExts += wl.FK_Emp + "<" + wl.FK_EmpText + ">��";
                    }
                    rm.ObjsExt = objExts;

                    string empExts = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        Emp emp = new Emp(dr[0].ToString());
                        if (rm.Objs.IndexOf(emp.No) != -1)
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += emp.No + "��";
                            else
                                empExts += emp.No + "<" + emp.Name + ">��";
                        }
                        else
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += "<strike><font color=red>" + emp.No + "</font></strike>��";
                            else
                                empExts += "<strike><font color=red>" + emp.No + "<" + emp.Name + "></font></strike>��";
                        }
                    }
                    rm.EmpsExt = empExts;
                    rm.Save();
                }
            }

            if (this.HisWorkerLists.Count == 0)
                throw new Exception("@���ݲ��Ų���������Ա���ִ�������[" + this.HisWorkFlow.HisFlow.Name + "],�нڵ�[" + town.HisNode.Name + "]�������,û���ҵ����ܴ˹����Ĺ�����Ա.");
            return this.HisWorkerLists;
        }
        #endregion


        #region ����

        #region ������λ
        private Station _HisStationOfUse = null;
        /// <summary>
        /// ȡ����ǰ�����õĸ�λ.
        /// �������������λ�Ľڵ���˵��.
        /// 
        /// </summary>
        /// <returns></returns>
        public Station HisStationOfUse
        {
            get
            {
                if (_HisStationOfUse == null)
                {
                    Stations sts = this.HisNode.HisStations;
                    if (sts.Count == 0)
                        throw new Exception(this.ToE("WN18", this.HisNode.Name));

                    _HisStationOfUse = (Station)this.HisNode.HisStations[0];
                    return _HisStationOfUse;

                    if (this.HisStations.Count == 1)
                    {
                        /* �����������ڵ�ֻ����һ��������λ���ʣ���ĩ��������ڵ������������ڵ��õĽڵ㡣*/
                        _HisStationOfUse = (Station)HisStations[0];
                        return _HisStationOfUse;
                    }

                    // �����ǰ�ڵ�������������λ���ʡ� �ҳ�������Ա�Ĺ�����λ����					
                    Stations mySts = this.HisWork.RecOfEmp.HisStations;
                    if (mySts.Count == 1)
                    {
                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                    }

                    // ȡ�����ڵ�Ĺ�����λ�빤����Ա������λ�Ľ�����
                    Stations gainSts = (Stations)mySts.GainIntersection(this.HisStations);
                    if (gainSts.Count == 0)
                    {
                        if (this.HisStations.Count == 0)
                            throw new Exception("@��û��Ϊ�ڵ�[" + this.HisNode.Name + "],���ù�����λ.");

                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                        ///// //throw new Exception("@��ȡʹ�ù�����λ���ִ���,������Ա["+this.HisWork.RecOfEmp.Name+"]�Ĺ���û�д������,���ı������Ĺ�����λ.���¹���������������ת��ȥ.");
                    }

                    /* �ж�������Ҫ������λ,��Ҫ������λ����. */
                    string mainStatNo = this.HisWork.HisRec.No;
#warning
                    foreach (Station myst in gainSts)
                    {
                        if (myst.No == mainStatNo)
                        {
                            _HisStationOfUse = myst;
                            return _HisStationOfUse;
                        }
                    }

                    /*ɨ�轻��,��������. */
                    foreach (Station myst in gainSts)
                    {

                    }

                    /* û��ɨ�赽��, ���ص�һ��station.*/
                    if (_HisStationOfUse == null)
                        _HisStationOfUse = (Station)gainSts[0];

                }
                return _HisStationOfUse;
            }
        }
        /// <summary>
        /// ������λ
        /// </summary>
        public Stations HisStations
        {
            get
            {
                return this.HisNode.HisStations;
            }
        }
        /// <summary>
        /// ���ص�һ�������ڵ�
        /// </summary>
        public Station HisStationOfFirst
        {
            get
            {
                return (Station)this.HisNode.HisStations[0];
            }
        }
        #endregion

        #region �ж�һ�˶ಿ�ŵ����
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        private Dept _HisDeptOfUse = null;
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        public Dept HisDeptOfUse
        {
            get
            {
                if (_HisDeptOfUse == null)
                {
                    //�ҵ�ȫ���Ĳ��š�
                    Depts depts;
                    if (this.HisWork.Rec == WebUser.No)
                        depts = WebUser.HisDepts;
                    else
                        depts = this.HisWork.RecOfEmp.HisDepts;

                    if (depts.Count == 0)
                        throw new Exception("��û�и�[" + this.HisWork.Rec + "]���ò��š�");

                    if (depts.Count == 1) /* ���ȫ���Ĳ���ֻ��һ�����ͷ�������*/
                    {
                        _HisDeptOfUse = (Dept)depts[0];
                        return _HisDeptOfUse;
                    }

                    if (_HisDeptOfUse == null)
                    {
                        /* �����û�ҵ����ͷ��ص�һ�����š� */
                        _HisDeptOfUse = depts[0] as Dept;
                    }
                }
                return _HisDeptOfUse;
            }
        }
        #endregion

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
                    _HisNodeCompleteConditions = new Conds(CondType.Node, this.HisNode.NodeID, this.WorkID);

                    return _HisNodeCompleteConditions;
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
                {
                    _HisFlowCompleteConditions = new Conds(CondType.Flow, this.HisNode.NodeID, this.WorkID);

                }
                return _HisFlowCompleteConditions;
            }
        }
        #endregion

        #region ������������
        ///// <summary>
        ///// �õ���ǰ���Ѿ���ɵĹ����ڵ�.
        ///// </summary>
        ///// <returns></returns>
        //public WorkNodes GetHadCompleteWorkNodes()
        //{
        //    WorkNodes mywns = new WorkNodes();
        //    WorkNodes wns = new WorkNodes(this.HisNode.HisFlow, this.HisWork.OID);
        //    foreach (WorkNode wn in wns)
        //    {
        //        if (wn.IsComplete)
        //            mywns.Add(wn);
        //    }
        //    return mywns;
        //}
        #endregion

        #region ���̹�������
        /// <summary>
        /// ���ȫ�ֵĹ��������ǲ����������
        /// �������ˣ��ͷ��ز����κδ���
        /// �������������ɵ��������ͷ�����Ӧ����Ϣ��
        /// ������������̵����������return null;  
        /// </summary>
        public string CheckGlobalCompleteCondition()
        {
            if (this.HisWorkFlow.IsComplete)  // ���ȫ�ֵĹ����Ѿ����.
                return this.ToE("FlowOver", "��ǰ�������Ѿ����"); //��ǰ�������Ѿ����

            #region ���ȫ�ֵ��������,���ڲ������õ�,���Ծ���ʱɾ��.
            /*
			GlobalCompleteConditions gcc = new GlobalCompleteConditions(this.HisNode.FK_Flow,this.HisWork.OID);
			if (gcc.IsOneOfConditionPassed)
			{
				this.HisWorkFlow.DoFlowOver();
				return "@��������["+this.HisNode.HisFlow.Name+"]ִ�й����У��ڽڵ�["+this.HisNode.Name+"]�����������������"+gcc.GetOneOfConditionPassed.ConditionDesc+"����������������";
			}
			*/
            #endregion

            return null;
        }
        /// <summary>
        /// ������̻ع�������
        /// </summary>
        /// <param name="TransferPC"></param>
        /// <param name="ByTransfered"></param>
        /// <returns></returns>
        public string AfterNodeSave()
        {
            DateTime dt = DateTime.Now;
            this.HisWork.Rec = Web.WebUser.No;
            this.WorkID = this.HisWork.OID;
            // this.NodeID = this.HisNode.NodeID;
            try
            {
                string msg = AfterNodeSave_Do();

                string doc = this.HisNode.DoWhat;
                if (doc.Length > 10)
                {
                    Attrs attrs = this.HisWork.EnMap.Attrs;
                    foreach (Attr attr in attrs)
                    {
                        if (doc.Contains("@" + attr.Key) == false)
                            continue;

                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:
                                doc = doc.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                                break;
                            default:
                                doc = doc.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                                break;
                        }
                    }

                    BP.DA.DBAccess.RunSQL(doc);
                }
                return msg;
            }
            catch (Exception ex)
            {
                /*���ύ���������£��ع����ݡ�*/
                try
                {

                    // �ѹ�����״̬���û�����
                    this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                    // �����̵�״̬���û�����
                    GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID, this.HisNode.FK_Flow);
                    if (gwf.WFState != 0 || gwf.FK_Node != this.HisNode.NodeID)
                    {
                        /* ���������������һ���б仯��*/
                        gwf.FK_Node = this.HisNode.NodeID;
                        gwf.WFState = 0;
                        gwf.Update();
                    }


                    Node startND = this.HisNode.HisFlow.HisStartNode;
                    StartWork wk = startND.HisWork as StartWork;
                    switch (startND.HisNodeWorkType)
                    {
                        case NodeWorkType.StartWorkFL:
                        case NodeWorkType.WorkFL:

                            break;
                        default:
                            // �ѿ�ʼ�ڵ��װ̬���û�����
                            wk.OID = this.WorkID;
                            wk.Retrieve();
                            if (wk.WFState == WFState.Complete)
                            {
                                wk.Update("WFState", (int)WFState.Runing);
                            }
                            break;
                    }


                    Nodes nds = this.HisNode.HisToNodes;
                    foreach (Node nd in nds)
                    {
                        Work mwk = nd.HisWork;
                        mwk.OID = this.WorkID;
                        if (mwk.IsGECheckStand)
                            mwk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                        mwk.Delete();
                    }

                    throw ex;
                }
                catch (Exception ex1)
                {
                    throw new Exception("@�����ڼ���ִ���:" + ex.Message + " @�ع�����ʧ�����ݳ��ִ���" + ex1.Message);
                }
            }
        }
        #region �û����ı���
        public WorkerLists HisWorkerLists = null;
        public CHOfFlow HisCHOfFlow = null;
        public GenerWorkFlow _HisGenerWorkFlow;
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                if (_HisGenerWorkFlow == null)
                    _HisGenerWorkFlow = new GenerWorkFlow(this.WorkID, this.HisNode.FK_Flow);
                return _HisGenerWorkFlow;
            }
            set
            {
                _HisGenerWorkFlow = value;
            }
        }
        private Int64 _WorkID = 0;
        public Int64 WorkID
        {
            get
            {
                return _WorkID;
            }
            set
            {
                _WorkID = value;
            }
        }
        #endregion


        public WorkerLists GenerWorkerLists(WorkNode town)
        {
            // ���Ի����ǵĹ�����Ա��
            WorkerLists gwls = null;
            switch (SystemConfig.CustomerNo)
            {
                case BP.CustomerNoList.QHDS:
                case BP.CustomerNoList.HBDS:
                    gwls = this.InitWorkerLists_QingHai(town);
                    break;
                case BP.CustomerNoList.HNDS:
                default:
                    gwls = this.InitWorkerLists_Gener(town);
                    break;
            }
            return gwls;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        public string FeiLiuStartUp(Node toNode)
        {
            // ssdd
            Work wk = toNode.HisWork;
            WorkNode town = new WorkNode(wk, toNode);

            // ������һ����Ҫִ�е���Ա��
            WorkerLists gwls = this.GenerWorkerLists(town);
            this.AddIntoWacthDog(gwls);  //@������Ϣ�����


            //�����ǰ�����ݣ��������η��͡�
            wk.Delete(WorkAttr.FID, this.HisWork.OID);

            string msg = "";

            // ���ղ��ŷ��飬�ֱ��������̡�
            switch (this.HisNode.HisFLRole)
            {
                case FLRole.ByStation:
                case FLRole.ByDept:
                case FLRole.ByEmp:
                    foreach (WorkerList wl in gwls)
                    {
                        Work mywk = toNode.HisWork;
                        mywk.Copy(this.HisWork);  //���ƹ�����Ϣ��
                        mywk.FID = this.HisWork.FID;
                        mywk.Rec = wl.FK_Emp;
                        mywk.Emps = wl.FK_Emp;
                        try
                        {
                            mywk.BeforeSave();
                        }
                        catch
                        {
                        }

                        mywk.OID = DBAccess.GenerOID(BP.Web.WebUser.FK_Dept.Substring(2));  //BP.DA.DBAccess.GenerOID();
                        // ��������Ĺ���Լ����
                        mywk.InsertAsOID(mywk.OID);


                        // ������������Ϣ��
                        GenerWorkFlow gwf = new GenerWorkFlow();
                        gwf.FID = this.WorkID;
                        gwf.WorkID = mywk.OID;

                        if (BP.WF.Glo.IsShowUserNoOnly)
                            gwf.Title = WebUser.No + "," + WebUser.Name + " �Զ�����" + toNode.Name + "(�����ڵ�)";
                        else
                            gwf.Title = WebUser.No + " �Զ�����" + toNode.Name + "(�����ڵ�)";

                        gwf.WFState = 0;
                        gwf.RDT = DataType.CurrentData;
                        gwf.Rec = Web.WebUser.No;
                        gwf.FK_Flow = toNode.FK_Flow;
                        gwf.FK_Node = toNode.NodeID;
                        gwf.FK_Dept = wl.FK_Dept;
                        try
                        {
                            gwf.DirectInsert();
                        }
                        catch
                        {
                            gwf.DirectUpdate();
                        }

                        // ����work�е���Ϣ��
                        BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET WorkID=" + mywk.OID + ",FID=" + this.WorkID + " WHERE FK_Emp='" + wl.FK_Emp + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + toNode.NodeID);
                    }
                    break;
                default:
                    throw new Exception("û�д�������ͣ�" + this.HisNode.HisFLRole.ToString());
            }
            // return ��ϵͳ�Զ��´�����¼�λͬ�¡�";

            msg += "@�����ڵ㣺" + toNode.Name + " �Ѿ�����" + string.Format("@�����Զ��´��{0}����{1}λͬ��,{2}.", this.nextStationName, this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);
            msg += this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID);

            //���½ڵ�״̬��
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);

            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            msg += "@<a href='" + path + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";

            return msg;
            // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
            // return this.ToEP3("TaskAutoSendTo", "@�����Զ��´��{0}����{1}λͬ��,{2}.", this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
        }

        public string FeiLiuStartUp()
        {
            GenerFH fh = new GenerFH();
            fh.FID = this.WorkID;
            if (this.HisNode.IsStartNode || fh.IsExits == false)
            {
                fh.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                fh.RDT = DataType.CurrentData;
                fh.FID = this.WorkID;
                fh.FK_Flow = this.HisNode.FK_Flow;
                fh.FK_Node = this.HisNode.NodeID;
                fh.GroupKey = WebUser.No;
                fh.WFState = 0;
                try
                {
                    fh.DirectInsert();
                }
                catch
                {
                    fh.DirectUpdate();
                }
            }


            string msg = "";
            Nodes toNodes = this.HisNode.HisToNodes;

            // ���ֻ��һ��ת��ڵ�, �Ͳ����ж�������,ֱ��ת����.
            if (toNodes.Count == 1)
                return FeiLiuStartUp((Node)toNodes[0]);

            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in toNodes)
            {

                Conds dcs = new Conds();
                QueryObject qo = new QueryObject(dcs);
                qo.AddWhere(CondAttr.NodeID, this.HisNode.NodeID);
                qo.addAnd();
                qo.AddWhere(CondAttr.ToNodeID, nd.NodeID);
                qo.addAnd();
                qo.AddWhere(CondAttr.CondType, (int)CondType.FLRole);
                qo.DoQuery();
                foreach (Cond dc in dcs)
                {
                    dc.WorkID = this.HisWork.OID;
                }


                if (dcs.Count == 0)
                {
                    throw new Exception(string.Format(this.ToE("WN10", "@����ڵ�ķ�����������:û�и���{0}�ڵ㵽{1},����ת������."), this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                    //throw new Exception("@����ڵ�ķ�����������:û�и���[" + this.HisNode.NodeID + this.HisNode.Name + "]�ڵ㵽[" + nd.NodeID + nd.Name + "],����ת������");
                }

                if (dcs.IsPass) // ������ת����������һ������.
                {
                    numOfWay++;
                    toNodeId = nd.NodeID;
                    msg = FeiLiuStartUp(nd);
                }
            }


            if (toNodeId == 0)
            {
                //throw new Exception("ת���������ô���:�ڵ�����" + this.HisNode.Name +" , �������������޷�Ͷ�ݡ�");
                throw new Exception(string.Format(this.ToE("WN11", "@ת���������ô���:�ڵ�����{0}, ϵͳ�޷�Ͷ�ݡ�"), this.HisNode.Name));  // ת���������ô���:�ڵ�����" + this.HisNode.Name + " , �������������޷�Ͷ�ݡ�"
            }
            else
            {
                /* ɾ����������������ϵ������������ݡ�
                 * ����˵�����������������˱仯����ܲ������������ϵ����ݡ���Ϊ�˹������������������������衣 */
                foreach (Node nd in toNodes)
                {
                    if (nd.NodeID == toNodeId)
                        continue;

                    // ɾ�������������Ϊ������������ݲ��������ˡ�
                    Work wk = nd.HisWork;
                    wk.OID = this.HisWork.OID;
                    if (wk.IsGECheckStand)
                        wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);

                    wk.Delete();

                    // ɾ����������Ŀ�����Ϣ��
                    if (SystemConfigOfTax.IsAutoGenerCHOfNode)
                    {
                        CHOfNode cn = new CHOfNode();
                        cn.WorkID = this.HisWork.OID;
                        cn.FK_Node = nd.NodeID;
                        cn.Delete();
                    }
                }
            }
            return msg;
        }
        #endregion

        /// <summary>
        /// �����̽ڵ㱣���Ĳ���.
        /// 1, �жϽڵ�������ǲ������,������,�����ýڵ�����״̬.
        /// 2, �ж��ǲ��Ƿ��Ϲ���������������������, ������,������,���������������. return ;
        /// 3, �жϽڵ�ķ���.
        /// 1, һ��һ�����жϴ˽ڵ�ķ���, ����������ڵ㹤��.
        /// 2, ���û���κ�һ���ڵ�Ĺ���.��ĩ���׳��쳣, ���̵Ľڵ㷽�������������. .
        /// </summary>
        /// <param name="TransferPC">�Ƿ�Ҫִ�л�ȡ�ⲿ��Ϣ����</param>
        /// <param name="ByTransfered">�ǲ��Ǳ����õ�,ֻ���ڿ�ʼ�ڵ�,�������ⲿ���õ��Զ�����������,�������Ϊtrue, �����ڵ�ǰ�Ľڵ����֮��Ͳ�����һ����Ĺ���,�������Ϊfalse, ������һ������.</param>
        /// <returns>ִ�к������</returns>
        private string AfterNodeSave_Do()
        {
            try
            {
                if (this.HisNode.IsStartNode)
                {
                    /* ������ʼ�������̼�¼. */
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = this.HisWork.OID;
                    gwf.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                    gwf.WFState = 0;
                    gwf.RDT = this.HisWork.RDT;
                    gwf.Rec = Web.WebUser.No;
                    gwf.FK_Flow = this.HisNode.FK_Flow;
                    gwf.FK_Node = this.HisNode.NodeID;
                    //  gwf.FK_Station = this.HisStationOfUse.No;
                    gwf.FK_Dept = this.HisWork.RecOfEmp.FK_Dept;
                    try
                    {
                        gwf.DirectInsert();
                    }
                    catch
                    {
                        gwf.DirectUpdate();
                    }

                    #region ����  HisGenerWorkFlow

                    this.HisGenerWorkFlow = gwf;

                    //#warning ȥ���������û���뵽���ȥд��
                    // ��¼������ͳ�Ʒ�����ȥ��
                    this.HisCHOfFlow = new CHOfFlow();
                    this.HisCHOfFlow.Copy(gwf);
                    this.HisCHOfFlow.WorkID = this.HisWork.OID;
                    this.HisCHOfFlow.WFState = (int)WFState.Runing;

                    /* ˵��û�������¼ */
                    this.HisCHOfFlow.FK_Flow = this.HisNode.FK_Flow;
                    this.HisCHOfFlow.WFState = 0;
                    this.HisCHOfFlow.Title = gwf.Title;
                    this.HisCHOfFlow.FK_Emp = this.HisWork.Rec.ToString();
                    this.HisCHOfFlow.RDT = this.HisWork.RDT;
                    this.HisCHOfFlow.CDT = DataType.CurrentDataTime;
                    this.HisCHOfFlow.SpanDays = 0;
                    this.HisCHOfFlow.FK_Dept = this.HisDeptOfUse.No;
                    this.HisCHOfFlow.FK_NY = DataType.CurrentYearMonth;
                    try
                    {
                        this.HisCHOfFlow.Insert();
                    }
                    catch
                    {
                        this.HisCHOfFlow.Update();
                    }
                    #endregion HisCHOfFlow


                    #region  ������ʼ������,�ܹ�ִ�����ǵ���Ա.
                    WorkerList wl = new WorkerList();
                    wl.WorkID = this.HisWork.OID;
                    wl.FK_Node = this.HisNode.NodeID;
                    wl.FK_Emp = this.HisWork.Rec;
                    wl.FK_Flow = this.HisNode.FK_Flow;
                    // wl.UseDept = this.HisDeptOfUse.No;
                    // wl.UseStation = this.HisStationOfUse.No;
                    wl.WarningDays = this.HisNode.WarningDays;
                    wl.SDT = DataType.CurrentData;
                    wl.DTOfWarning = DataType.CurrentData;
                    wl.RDT = DataType.CurrentData;
                    try
                    {
                        wl.Insert(); // �Ȳ��룬����¡�
                    }
                    catch
                    {
                        wl.Update();
                    }
                    #endregion
                }


                string msg = "";
                switch (this.HisNode.HisNodeWorkType)
                {
                    case NodeWorkType.GECheckMuls:
                    case NodeWorkType.GECheckStands:
                    case NodeWorkType.NumChecks: /* ��˵���߼� */
                        msg = this.AfterWorkOfCheckSave();
                        msg += this.DoSetThisWorkOver();
                        break;
                    case NodeWorkType.StartWorkFL:
                    case NodeWorkType.WorkFL:  /* �������� */
                        this.HisWork.FID = this.HisWork.OID;
                        msg = this.FeiLiuStartUp();
                        break;
                    case NodeWorkType.WorkFHL:   /* �������� */
                        this.HisWork.FID = this.HisWork.OID;
                        msg = this.FeiLiuStartUp();
                        break;
                    default: /* �����ĵ���߼� */
                        msg = this.StartupNewNodeWork();
                        msg += this.DoSetThisWorkOver();
                        break;
                }

                #region ��������
                BookTemplates reffunc = this.HisNode.HisBookTemplates;
                if (reffunc.Count > 0)
                {
                    #region ����������Ϣ
                    Int64 workid = this.HisWork.OID;
                    int nodeId = this.HisNode.NodeID;
                    //string bookTable=this.HisNode.HisFlow.BookTable;
                    string flowNo = this.HisNode.FK_Flow;

                    // ɾ�����sql, �ں����Ѿ������쳣�жϡ�
                    //DBAccess.RunSQL("DELETE WF_Book WHERE WorkID='" + workid + "' and (FK_Node='" + nodeId + "') ");
                    #endregion

                    DateTime dt = DateTime.Now;
                    string bookNo = this.HisWorkFlow.HisStartWork.BillNo;
                    Flow fl = new Flow(this.HisNode.FK_Flow);
                    string year = dt.Year.ToString();
                    string bookInfo = "";
                    foreach (BookTemplate func in reffunc)
                    {
                        string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                        BP.Rpt.RTF.RTFEngine rtf = new BP.Rpt.RTF.RTFEngine();

                        Works works;
                        string[] paths;
                        string path;

                        try
                        {
                            #region ��������
                            rtf.HisEns.Clear();
                            rtf.EnsDataDtls.Clear();

                            if (func.NodeID == 0)
                            {
                                // �ж��Ƿ��������ִ
                                if (fl.DateLit == 0)
                                    continue;

                                HisCHOfFlow.DateLitFrom = DateTime.Now.AddDays(fl.DateLit).ToString(DataType.SysDataFormat);
                                HisCHOfFlow.DateLitTo = DateTime.Now.AddDays(fl.DateLit + 10).ToString(DataType.SysDataFormat);
                                HisCHOfFlow.Update();
                                rtf.AddEn(HisCHOfFlow);
                            }
                            else
                            {
                                WorkNodes wns = new WorkNodes();
                                if (this.HisNode.HisFNType == FNType.River)
                                    wns.GenerByFID(this.HisNode.HisFlow, this.WorkID);
                                else
                                    wns.GenerByWorkID(this.HisNode.HisFlow, this.WorkID);


                                works = wns.GetWorks;
                                foreach (Work wk in works)
                                {
                                    if (wk.OID == 0)
                                        continue;

                                    rtf.AddEn(wk);
                                    rtf.ensStrs += ".ND" + wk.NodeID;
                                    ArrayList al = wk.GetDtlsDatasOfArrayList();
                                    foreach (Entities ens in al)
                                        rtf.AddEns(ens);
                                }
                                //    w = new BP.Port.WordNo(WebUser.FK_DeptOfXJ);
                                // rtf.AddEn(w);
                            }

                            paths = file.Split('_');
                            path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";

                            bookInfo += "<img src='/" + SystemConfig.AppName + "/Images/Btn/Word.gif' /><a href='" + System.Web.HttpContext.Current.Request.ApplicationPath + "/FlowFile/Book/" + path + file + "' target=_blank >" + func.Name + "</a>";

                            //  string  = BP.SystemConfig.GetConfig("FtpPath") + file;
                            path = BP.WF.Glo.FlowFile + "\\Book\\" + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";


                            if (System.IO.Directory.Exists(path) == false)
                                System.IO.Directory.CreateDirectory(path);

                            rtf.MakeDoc(func.Url + ".rtf",
                                path, file, func.ReplaceVal, false);

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            BP.WF.DTS.InitBookDir dir = new BP.WF.DTS.InitBookDir();
                            dir.Do();

                            path = BP.WF.Glo.FlowFile + "\\Book\\" + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                            string msgErr = "@" + this.ToE("WN5", "��������ʧ�ܣ����ù���Ա���Ŀ¼����") + "[" + BP.WF.Glo.FlowFile + "]��@Err��" + ex.Message + " @File=" + file + " @Path:" + path;
                            bookInfo += "@<font color=red>" + msgErr + "</font>";
                            throw new Exception(msgErr);
                        }

                        Book bk = new Book();
                        bk.WorkID = this.HisWork.OID;
                        bk.FK_Node = this.HisNode.NodeID;
                        bk.BookNo = bookNo;

                        if (func.IsNeedSend)
                            bk.BookState = BookState.UnSend;
                        else
                            bk.BookState = BookState.Send;


                        bk.FK_NodeRefFunc = func.No;
                        bk.FilePrix = func.No;
                        bk.RDT = DataType.CurrentDataTime;
                        //string fk_flowSort = this.HisNode.HisFlow.FK_FlowSort;
                        //dt = dt.AddDays(func.TimeLimit);
                        bk.ShouldSendDT = dt.ToString("yyyy-MM-dd");
                        bk.FileName = file;
                        bk.BookName = func.Name;
                        try
                        {
                            bk.Insert();
                        }
                        catch
                        {
                            bk.Update();
                        }

                        #region ���������֤
                        if (func.IsNeedSend)
                        {
                            //func.IDX = bookNo;
                            ///* �����Ҫ�� */
                            //file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                            //rtf.AddEn(func);

                            //paths = file.Split('_');
                            //path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";

                            //bookInfo += "<img src='/" + SystemConfig.AppName + "/Images/Btn/Word.gif' /><a href='/FlowFile/" + path + file.Replace(".doc", "HZ.doc") + "' target=_blank >��֤</a>";

                            ////path = BP.WF.Glo.FlowFileBook  + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                            ////rtf.MakeDoc("/��֤.rtf",
                            ////    path, file.Replace(".doc", "HZ.doc"), false);
                        }
                        #endregion

                    } // end ����ѭ�����顣

                    if (bookInfo != "")
                        bookInfo = "@Book:" + bookInfo;
                    msg += bookInfo;
                }
                #endregion


                #region ����ǰ��Ա�Ŀ�������
                if (SystemConfigOfTax.IsAutoGenerCHOfNode == true)
                {
                    this.DTSDataToChOfNode(this);
                    int i = 0;
                    CHOfNodes cns = new CHOfNodes();
                    int num = cns.Retrieve(CHOfNodeAttr.WorkID, this.HisWork.OID,
                        CHOfNodeAttr.FK_Node, this.HisNode.NodeID);

                    if (num == 0)
                    {
                        Log.DebugWriteError("@�����ܳ��ֵ��������Ϊ�Ѿ������˵��ȡ�");
                        //cns.AddEntity(this.DTSDataToChOfNode(this));
                        num = 1;
                        //  throw new Exception("�����ܳ��ֵ������");
                    }

                    if (num > 1)
                    {
                        /* ˵���Ѿ��ӹ���������� �� ��ִ�и���ǰ����Ա�ӷ֡�*/
                        //����������� �� �ڵ�״̬��
                        i = DBAccess.RunSQL("UPDATE WF_CHOfNode SET CDT='" + DataType.CurrentDataTime + "',NodeState=1, CentOfAdd=0 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID='" + this.HisWork.OID + "'");
                        if (i == 0)
                        {
                            Log.DebugWriteError("@��Ӧ�����е����");
                            //  this.DTSDataToChOfNode(this);
                        }

                        //  ����ǰ������Ա�ӷ֡� 
                        i = DBAccess.RunSQL("UPDATE WF_CHOfNode SET NodeSwinkCent=" + this.HisNode.SwinkCent + " WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp='" + Web.WebUser.No + "'");
                        if (i == 0)
                            Log.DebugWriteError("@�����ܳ��ֵ������û�и���ǰ��Ա�ӷ֡�");
                    }

                    //  Node nd = this.HisNode;
                    // ִ�п۷֡� ���Ӧ���ʱ�䳬���˵�ǰϵͳʱ��,��ô����۷֡�
                    // DBAccess.RunSQL("UPDATE WF_CHOfNode SET CentOfCut='" + this.HisNode.DeductCent + "', Cent=CentofCut+CentOfQu+CentOfAdd  WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID='" + this.HisWork.OID + "' AND FK_Node='" + this.HisNode.NodeID + "' AND SUBSTR(SDT,0,11)<'" + DataType.CurrentData + "' ");
                    foreach (CHOfNode cn in cns)
                    {
                        if (cn.SpanDays > 0)
                        {
                            cn.CentOfCut = cn.NodeDeductCent * (cn.SpanDays - this.HisNode.NeedCompleteDays);
                            if (cn.CentOfCut > cn.NodeMaxDeductCent)
                                cn.CentOfCut = cn.NodeMaxDeductCent;

                            cn.Note = this.ToE("WN1", "δ������ɹ�����"); //"δ������ɹ�����"; // �۷�ԭ��
                            if (cn.CentOfCut <= 0)
                                cn.CentOfCut = 0;
                        }
                        cn.Cent = cn.CentOfCut + cn.CentOfAdd;
                        cn.Update();
                    }
                    DBAccess.RunSQL("UPDATE WF_CHOfNode SET CDT='" + DataType.CurrentDataTime + "', FK_NY='" + DataType.CurrentYearMonth + "', NodeState=1, CentOfAdd=" + this.HisNode.SwinkCent + "  WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp='" + WebUser.No + "'");
                }
                #endregion

                this.HisWork.DoCopy(); // copy ���ص����ݵ�ָ����ϵͳ.
                return msg;
            }
            catch (Exception ex)  // ����׳��쳣��˵��û����ȷ��ִ�С���ǰ�Ĺ�����������ɡ��������̲�����ɡ�
            {
                WorkNode wn = this.HisWorkFlow.HisStartWorkNode;
                //wn.HisWork.SetValByKey("WFState",0);
                wn.HisWork.Update("WFState", (int)WFState.Runing);
                this.HisWork.NodeState = NodeState.Init;
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                // ���µ�ǰ�Ľڵ���Ϣ��
                this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node, this.HisNode.NodeID);
                this.RollbackCHOfNode(this.HisNode.HisToNodes);
                throw ex;
            }
        }
        private void RollbackCHOfNode(Nodes toNodes)
        {
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == false)
                return;

            foreach (Node nd in toNodes)
            {
                DBAccess.RunSQL("DELETE WF_CHOFNODE WHERE WorkID=" + this.HisWork.OID + " AND FK_Node=" + nd.NodeID);

             //   DBAccess.RunSQL("DELETE WF_CHOFNODE WHERE WorkID=@wORKid" + this.HisWork.OID + " AND FK_Node=" + nd.NodeID,);

            }
            return;

            // ���µ�ǰ�ڵ�Ŀ������ݡ�
            CHOfNode chn = new CHOfNode();
            chn.WorkID = this.HisWork.OID;
            chn.FK_Node = this.HisNode.NodeID;
            chn.FK_Emp = Web.WebUser.No;

            int num = chn.Retrieve(CHOfNodeAttr.WorkID, this.HisWork.OID,
                CHOfNodeAttr.FK_Node, this.HisNode.NodeID,
                CHOfNodeAttr.FK_Emp, Web.WebUser.No);

            /*
            if (num == 0)
                throw new Exception("�����ܳ��֡�");

            if (num > 1)
                throw new Exception("�����ܳ��֡�num > 1 ");
             * */

            chn.CentOfAdd = 0;
            chn.CDT = " ";

            //if (chn.SDT <DataType.CurrentData)
            //	chn.ch
            chn.CentOfCut = 0;
            chn.Update();

            //���ȵ������ݵ� ����ϵͳ���С�
            //this.DTSDataToChOfNode(this);
        }
        /// <summary>
        /// ���빤����¼
        /// </summary>
        /// <param name="gwls"></param>
        public void AddIntoWacthDog(WorkerLists gwls)
        {
            return;
            /*
            string workers="";
            // ������
            foreach(WorkerList wl in gwls)
            {
                workers+=","+wl.FK_Emp;
            }
            Watchdog wd =new Watchdog();
            wd.InitDateTime=DataType.CurrentDataTime ;
            wd.WorkID=this.HisWork.OID;
            wd.NodeId =this.HisNode.OID;
            wd.Workers = workers+",";
            wd.FK_Dept =this.HisDeptOfUse.No ;
            wd.FK_Station=this.HisStationOfUse.No;
            wd.Save();
            */

        }
        /// <summary>
        /// ��ǩ�ڵ��Ƿ�ȫ����ɣ�
        /// </summary>
        private bool IsOverMGECheckStand = false;
        /// <summary>
        /// ������˽ڵ��������
        /// </summary>		 
        private string AfterWorkOfCheckSave()
        {
            // ���ü��ȫ�ֵĹ���.
            string msg = this.CheckGlobalCompleteCondition();
            if (msg != null)
                return msg;

            GECheckStand en = this.HisWork as GECheckStand;
            if (this.HisNode.HisNodeWorkType == NodeWorkType.GECheckMuls)
            {

            }
            else
            {
                return AfterWorkOfCheckSave_Stand(en);
            }

            WorkerLists ens = new WorkerLists();
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(WorkerListAttr.WorkID, this.WorkID);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, en.NodeID);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.IsEnable, true);
            qo.DoQuery();

            if (ens.Count == 1)
                return AfterWorkOfCheckSave_Stand(en);

            // �����ҵĵ�ǰ״̬��
            foreach (WorkerList wl in ens)
            {
                if (wl.FK_Emp != WebUser.No)
                    continue;

                wl.IsPass = true;
                wl.Update(WorkerListAttr.IsPass, 1);
            }

            string msgInfo = "";
            foreach (WorkerList wl in ens)
            {
                if (wl.IsPass == true)
                    continue;

                msgInfo += wl.FK_EmpText + "��";
            }


            if (msgInfo.Length > 2)
            {
                IsOverMGECheckStand = false;
                msgInfo = "@��ǰ����[" + this.HisNode.Name + "]�Ѿ���ɡ�@�ڴ��ǻ�ǩ�ڵ㣬������Աû�д�������" + msgInfo + "��@��Ҫ�ȴ����Ƕ�������ɺ����ִ����һ���衣";
                return msgInfo;
            }
            else
            {
                IsOverMGECheckStand = true;
                return "@��ǰ����[" + this.HisNode.Name + "]�ǻ�ǩ�ڵ㣬���е���Ա���Ѿ�ǩ��ͨ����ϡ�" + this.StartupNewNodeWork();
            }
        }
        private string AfterWorkOfCheckSave_Stand(GECheckStand en)
        {
            switch (en.CheckState)
            {
                case CheckState.Agree:  //�����ͬ��.
                    if (this.HisNode.IsEndNode) // ��������Ľڵ�
                        return this.HisWorkFlow.DoFlowOver(); // �����������̽���.
                    else // ����������ڵ�.
                        return this.StartupNewNodeWork();   //�����µĽ�����.
                    break;
                case CheckState.Dissent: // ����ǲ�ͬ��, �����ô˹����Ѿ�����.
                    return this.HisWorkFlow.DoFlowOver();
                    break;
                case CheckState.Pause: // ����ǹ���.
                    return "���[" + this.HisNode.Name + "]����.";
                    break;
                default:
                    throw new Exception("@�ڵ�[" + this.HisNode.Name + "]״̬����.");
            }
        }
        /// <summary>
        /// ������̡��ڵ���������
        /// </summary>
        /// <returns></returns>
        private string CheckCompleteCondition()
        {
            // ���ü��ȫ�ֵĹ���.
            string msg = this.CheckGlobalCompleteCondition();
            if (msg != null)
                return msg;

            #region �жϽڵ��������
            try
            {
                // ���û������,��˵����,����Ϊ��ɽڵ����������.
                if (this.HisNode.IsCCNode == false)
                {
                    msg = string.Format(this.ToE("WN6", "��ǰ����[{0}]�Ѿ����"), this.HisNode.Name);
                }
                else
                {
                    int i = this.HisNodeCompleteConditions.Count;
                    if (i == 0)
                    {
                        this.HisNode.IsCCNode = false;
                        this.HisNode.Update();
                    }

                    if (this.HisNodeCompleteConditions.IsPass)
                    {
                        if (SystemConfig.IsDebug)
                            msg = "@��ǰ����[" + this.HisNode.Name + "]�����������[" + this.HisNodeCompleteConditions.ConditionDesc + "],�Ѿ����.";
                        else
                            msg = string.Format(this.ToE("WN6", "��ǰ����{0}�Ѿ����"), this.HisNode.Name);  //"@"; //��ǰ����[" + this.HisNode.Name + "],�Ѿ����.
                    }
                    else
                    {
                        // "@��ǰ����[" + this.HisNode.Name + "]û�����,��һ��������������."
                        throw new Exception(string.Format(this.ToE("WN7", "@��ǰ����{0}û�����,��һ��������������."), this.HisNode.Name));
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN8", "@�жϽڵ�{0}����������ִ���.") + ex.Message, this.HisNode.Name)); //"@�жϽڵ�[" + this.HisNode.Name + "]����������ִ���:" + ex.Message;
            }
            #endregion

            #region �ж���������.
            try
            {
                if (this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass)
                {
                    /* ���������� */
                    string overMsg = this.HisWorkFlow.DoFlowOver();
                    string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    return msg + "@���Ϲ��������������[" + this.HisFlowCompleteConditions.ConditionDesc + "]" + overMsg + " @�鿴<img src='" + path + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + path + "/WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >��������</a>";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN9", "@�ж�����{0}����������ִ���.") + ex.Message, this.HisNode.Name));
            }
            #endregion

            return msg;
        }
        /// <summary>
        /// �����µĽڵ�
        /// </summary>
        /// <returns></returns>
        public string StartupNewNodeWork()
        {
            string msg = this.CheckCompleteCondition();
            // ȡ��ǰ�ڵ�Nodes.
            Nodes toNodes = this.HisNode.HisToNodes;
            if (toNodes.Count == 0)
            {
                /* ��������һ���ڵ㣬���������̽�����*/
                string ovrMsg = this.HisWorkFlow.DoFlowOver();
                return ovrMsg + this.ToE("WN0", "@�˹����������е����һ�����ڣ������ɹ�������") + "<img src='./../../Images/Btn/PrintWorkRpt.gif' ><a href='./../../WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "&FK_Node=" + this.HisNode.NodeID + "'target='_blank' >" + this.ToE("WorkRpt", "��������") + "</a>";
            }

            // ���ֻ��һ��ת��ڵ�, �Ͳ����ж�������,ֱ��ת����.
            if (toNodes.Count == 1)
                return StartNextNode((Node)toNodes[0]);

            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in toNodes)
            {
                Conds dcs = new Conds();
                QueryObject qo = new QueryObject(dcs);
                qo.AddWhere(CondAttr.NodeID, this.HisNode.NodeID);
                qo.addAnd();
                qo.AddWhere(CondAttr.ToNodeID, nd.NodeID );
                //qo.addAnd();
                //qo.AddWhere(CondAttr.CondType, (int)CondType.Node);
                qo.DoQuery();
                foreach (Cond dc in dcs)
                {
                    dc.WorkID = this.HisWork.OID;
                }


                if (dcs.Count == 0)
                {
                    throw new Exception(string.Format(this.ToE("WN10", "@����ڵ�ķ�����������:û�и���{0}�ڵ㵽{1},����ת������."), this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                    //throw new Exception("@����ڵ�ķ�����������:û�и���[" + this.HisNode.NodeID + this.HisNode.Name + "]�ڵ㵽[" + nd.NodeID + nd.Name + "],����ת������");
                }

                if (dcs.IsPass) // ������ת����������һ������.
                {
                    numOfWay++;
                    toNodeId = nd.NodeID;
                    //msg += "@��������ת��ڵ�[<font color=blue>" + nd.Name + "</font>]������[<font color=blue>" + dcs.ConditionDesc + "</font>]" ;
                    msg += StartNextNode(nd);
                    if (SystemConfig.IsDebug)
                    {
                        if (numOfWay > 1)
                            throw new Exception("@���ƶ��ķ���������ì�ܣ���һ�������ϵ�·����Ψһ������ϸ������ķ����������õ�ȡֵ��Χ��");
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (toNodeId == 0)
                throw new Exception(string.Format(this.ToE("WN11", "@ת���������ô���:�ڵ�����{0}, ϵͳ�޷�Ͷ�ݡ�"), this.HisNode.Name));

            /* ɾ����������������ϵ������������ݡ�
             * ����˵�����������������˱仯����ܲ������������ϵ����ݡ���Ϊ�˹������������������������衣 */
            foreach (Node nd in toNodes)
            {
                if (nd.NodeID == toNodeId)
                    continue;

                // ɾ�������������Ϊ������������ݲ��������ˡ�
                Work wk = nd.HisWork;
                wk.OID = this.HisWork.OID;
                if (wk.IsGECheckStand)
                    wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);

                if (wk.Delete() != 0)
                {
                    // ɾ����������Ŀ�����Ϣ��
                    if (SystemConfigOfTax.IsAutoGenerCHOfNode)
                    {
                        CHOfNode cn = new CHOfNode();
                        cn.WorkID = this.HisWork.OID;
                        cn.FK_Node = nd.NodeID;
                        cn.Delete();
                    }
                }
            }
            return msg;
        }

        #region ������˽ڵ�
        /// <summary>
        /// ����ָ������һ���ڵ�...
        /// </summary>
        /// <param name="nd">Ҫ�����Ľڵ�</param>
        public string StartNextNode(Node nd)
        {
            try
            {
                // �������֮��Ҫ�������顣
                switch (nd.HisNodeWorkType)
                {
                    case NodeWorkType.GECheckMuls:
                    case NodeWorkType.NumChecks:
                    case NodeWorkType.GECheckStands:
                        return StartNextCheckNode(nd); /* ��˽ڵ� */
                    case NodeWorkType.WorkHL:
                        return StartNextWorkNodeHeLiu(nd);  /* �����ڵ� */
                    default:
                        return StartNextWorkNodeOrdinary(nd);  /* ��ͨ�ڵ� */
                }
                this.InitEmps(nd);
            }
            catch (Exception ex)
            {
                throw new Exception("@" + this.ToE("StartNextNodeErr", "@������һ���ڵ���ִ���") + ":" + ex.Message); //������һ���ڵ���ִ���
            }
        }
        /// <summary>
        /// InitEmps
        /// </summary>
        /// <param name="nd"></param>
        public void InitEmps(Node nd)
        {
            return;

            #region ����ÿ���ڵ����Ľ����˼��ϣ�Emps����
            try
            {
                int i = 0;
                if (nd.IsCheckNode)
                {
                    //���±�׼��˽ڵ��Emps
                    i = DBAccess.RunSQL("update " + nd.PTable + " set Emps='" + this.GenerEmps(nd) + "' where NodeID='" + nd.NodeID + "'  and oid='" + this.HisWork.OID + "'");
                }
                else
                {
                    //������ͨ�ڵ��Emps
                    i = DBAccess.RunSQL("update " + nd.PTable + " set Emps='" + this.GenerEmps(nd) + "' where oid='" + this.HisWork.OID + "'");
                }
            }
            catch (Exception ex)
            {
                nd.HisWork.CheckPhysicsTable();
                throw new Exception("@����Empsʱ����һ�´���<br>" + ex.Message);
            }
            #endregion

            #region �������ͺ���WF_CHOFNODE����һ������,
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == false)
                return;

            // ɾ�����ܳ��ֵ� �������ݡ�
            //CHOfNodes cns = new CHOfNodes();
            //cns.Delete(CHOfNodeAttr.FK_Node,nd.NodeID.ToString(),
            //    CHOfNodeAttr.WorkID,this.HisWork.OID.ToString());

            Work wk = nd.HisWork;
            Emp emp = wk.HisRec;
            //Node nd = new Node(); //wn.HisNode;
            string msg = "";
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == true)
            {
                CHOfNode cn = new CHOfNode();
                cn.WorkID = wk.OID;
                cn.FK_Node = nd.NodeID;
                cn.RDT = wk.RDT;
                cn.CDT = " ";
                cn.SDT = DataType.AddDays(wk.RDT, nd.NeedCompleteDays).ToString(DataType.SysDataFormat);
                cn.FK_NY = wk.Record_FK_NY;
                cn.FK_Emp = wk.RecOfEmp.No;
                cn.FK_Station = HisStationOfUse.No;//wn.HisStationOfUse.No;
                cn.FK_Dept = HisDeptOfUse.No;
                cn.NodeState = wk.NodeState;
                cn.FK_Dept = emp.FK_Dept;
                cn.FK_Flow = nd.FK_Flow;
                cn.SpanDays = DataType.SpanDays(wk.RDT, wk.CDT);
                cn.Note = " "; //
                //cn.Emps = wk.Emps ; //Emps
                cn.Emps = this.GenerEmps(nd).ToString();

                //ֻҪ��ĳ���ڵ�����ִ��ֶΣ�Spec,��ô�Ͱ�Spec�������д��WF_CHOfNode����
                if (wk.EnMap.Attrs.Contains("SPEC"))
                    cn.Spec = wk.GetValStringByKey("SPEC");

                cn.CentOfAdd = 0; //�������÷֡�
                cn.NodeDeductCent = nd.DeductCent;
                cn.NodeDeductDays = nd.DeductDays;
                cn.NodeMaxDeductCent = nd.MaxDeductCent;
                if (wk.EnMap.Attrs.Contains("FK_Taxpayer"))
                {
                    cn.FK_Taxpayer = wk.GetValStringByKey("FK_Taxpayer");
                    if (wk.EnMap.Attrs.Contains("TaxpayerName"))
                        cn.TaxpayerName = wk.GetValStringByKey("TaxpayerName");
                }

                cn.CentOfCut = 0;
                //cn.CentOfQU=0;
                cn.Cent = 0;
                cn.Insert();
            }
            #endregion
        }
        /// <summary>
        /// �������¸�����Ҫ���Ĺ�����
        /// </summary>
        /// <param name="wk">Ҫ�����Ĺ���</param>
        /// <param name="nd">Ҫ�����Ľڵ㡣</param>
        /// <returns>��������Ϣ</returns>
        private string beforeStartNode(Work wk, Node nd)
        {
            WorkNode town = new WorkNode(wk, nd);

            // ���Ի����ǵĹ�����Ա��
            WorkerLists gwls = this.GenerWorkerLists(town);


            this.AddIntoWacthDog(gwls); //@������Ϣ�����
            string msg = "";
            //msg = "@�����Զ��´��'<font color=blue><b>" + this.nextStationName + "</b></font>'����" + this._RememberMe.NumOfObjs + "λͬ�£�@" + this._RememberMe.EmpsExt;
            //msg = "@" + this.ToE("TaskAutoSendTo") + " <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
            if (nd.HisNodeWorkType == NodeWorkType.GECheckMuls)
            {
                msg = this.ToEP3("TaskAutoSendToM", "@�����Զ��´��{0}����{1}λͬ��,{2}.��Ҫ�ȴ�����ȫ�����������ִ����һ���衣<a href=\"javascript:WinOpen('WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + nd.FK_Flow + "&FID=0')\" >�����ǰ�Ĳ����д��������Գ�������</a>", this.nextStationName, this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
                msg += this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);
            }
            else
            {
                msg = this.ToEP3("TaskAutoSendTo", "@�����Զ��´��{0}����{1}λͬ��,{2}.", this.nextStationName, this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ��@" + this._RememberMe.EmpsExt;
                if (this._RememberMe.NumOfEmps >= 2)
                {
                    msg += "<a href=\"javascript:WinOpen('AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>ָ���ض���ͬ�´���</a>��";
                }
                msg += this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);
            }

            msg += "@<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "');\" ><img src='./Img/SMS.gif' border=0 />���ֻ���������</a>";

            if (this.HisNode.IsStartNode)
                msg += "@<a href='MyFlowInfo.aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src=./Img/UnDo.gif border=0/>�������η���</a>�� <a href='MyFlow.aspx?FK_Flow=" + nd.FK_Flow + "'>�½�����</a>��";
            else
                msg += "@<a href='MyFlowInfo.aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src=./Img/UnDo.gif border=0/>�������η���</a>��";

            if (nd.IsCheckNode)
            {
                foreach (WorkerList wl in gwls)
                {
                    break;
                    /* ȥ���Զ���˵�����*/
                    wk.Rec = wl.FK_Emp;
                    /* �������˽ڵ� */
                    //if (    wk.RecOfEmp.IsAuthorizedTo(this.HisWork.Rec))
                    //{
                    //    /* �����Ҫ�����Ĺ����ڵ���Ա��Ȩ����ǰ�ĵĹ�����Ա����ֱ��pass. */
                    //    //wk.Rec = 
                    //    wk.SetValByKey(GECheckStandAttr.CheckState, (int)CheckState.Agree);
                    //    wk.DirectSave();
                    //    WorkNode mywn = new WorkNode(wk, nd);
                    //    //wk.SetValByKey(GECheckStandAttr.CheckState, (int)CheckState.Agree);
                    //    try
                    //    {
                    //        msg += "@<b>�Զ���Ȩ����</b>��Ϣ����:" + mywn.AfterNodeSave(false, true, DateTime.Now); // ֱ��������һ��������ֱ����ˡ�
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        msg += "@<font color=red>�Զ���Ȩ�����������´���:</font>" + ex.Message;
                    //    }
                    //    break;
                    //}
                }
            }
            else
            {
                /* �γ���Ϣ֪ͨ���� */
                if (nd.IsPCNode == false && SystemConfig.IsBSsystem)
                {
                    MsgsManager.AddMsgs(gwls, town.HisNode.FlowName, nd.Name, "Test");
                }
            }

            string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            string str = "";
            //if (this._RememberMe.NumOfEmps > 1)
            //    str = "<a href='" + appPath + "/WF/AllotTask.aspx?NodeId=" + nd.NodeID + "&WorkID=" + wk.OID + "&FlowNo=" + nd.FK_Flow + "&dt='" + DateTime.Now.ToString("MMddhhmmss") + " target='_self' >" + this.ToE("AllotWork", "���乤��") + "</a>";

            //return msg + "@������" + str + "�鿴<img src='" + appPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + appPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >��������</a>,����<img src='" + appPath + "/Images/Btn/WorkFlowOp.gif' ><a href='" + appPath + "/WF/OpWorkFlow.aspx?WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "' target='_blank' >���̲���</a> ��";
            return msg + "@" + str + " <img src='" + appPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + appPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FID=" + wk.FID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >" + this.ToE("WorkRpt", "��������") + "</a>��";
        }
        /// <summary>
        /// ������һ����˽ڵ�
        /// </summary>
        /// <param name="nd">�����ڵ�</param>
        private string StartNextCheckNode(Node nd)
        {
            GECheckStand wk = (GECheckStand)nd.HisWork;
            try
            {
                //wk.MyPK = nd.NodeID + "_" + this.HisWork.OID;
                wk.OID = this.HisWork.OID;
                wk.NodeID = nd.NodeID;
                wk.NodeState = NodeState.Init;
                wk.CheckState = CheckState.Agree;//Ĭ����ͬ��״̬����Ϊû�в�ͬ��͹���״̬��Ĭ�ϴ���ͬ��״̬
                wk.Rec = BP.Web.WebUser.No;

                try
                {
                    wk.Insert();
                }
                catch
                {
                    wk.Update();
                }

                string msg = this.beforeStartNode(wk, nd);

                if (wk.OID == BP.WF.WFPubClass.DefaultWorkID)
                {
                    /*������ָ�����ݣ�*/
                    string refMsg = BP.WF.WFPubClass.DefaultRefMsg;
                    if (refMsg == "")
                    {
                        /* �����ǰ�ڵ�û�и�����Ϣ����ȡ��һ���ڵ�ĸ�����Ϣ��*/
                        if (this.HisNode.IsCheckNode)
                        {
                            wk.RefMsg = this.HisWork.GetValStringByKey("RefMsg");
                        }
                    }
                    wk.RefMsg = refMsg;

                }

                if (this.HisNode.IsCheckNode)
                {
                    /* �����ǰ�Ľڵ��� checkNode */
                    GECheckStand mych = (GECheckStand)this.HisWork;
                    wk.RefMsg = mych.RefMsg + "\n --- " + this.HisWork.RecText + " , " + this.HisWork.RDT + this.ToE("CheckNoteD", "����������£�") + ":\n" + mych.Note + "\n\n";
                }


                wk.Sender = Web.WebUser.No;
                wk.NodeState = NodeState.Init; //�ڵ�״̬��
                if (nd.HisNodeWorkType == NodeWorkType.GECheckMuls)
                {
                    WorkerLists wls = new WorkerLists();
                    wls.Retrieve(WorkerListAttr.FK_Node, nd.NodeID, WorkerListAttr.WorkID, wk.OID);
                    foreach (WorkerList wl in wls)
                    {
                        GECheckMul mc = new GECheckMul();
                        mc.OID = this.HisWork.OID;
                        mc.NodeID = nd.NodeID;
                        mc.NodeState = NodeState.Init;
                        mc.CheckState = CheckState.Agree;//Ĭ����ͬ��״̬����Ϊû�в�ͬ��͹���״̬��Ĭ�ϴ���ͬ��״̬
                        mc.Rec = wl.FK_Emp;
                        mc.RefMsg = wk.RefMsg;
                        mc.Sender = WebUser.No;
                        try
                        {
                            mc.Insert();
                        }
                        catch
                        {
                            mc.Update();
                        }
                    }

                    wk.Delete();
                }
                else
                {
                    if (wk.Update() == 0)
                        wk.Insert();
                }
                return "@<font color=blue>" + nd.Name + "</font> " + this.ToE("WN12", "��˹����ɹ�����") + "." + msg;
            }
            catch (Exception ex)
            {
                wk.CheckPhysicsTable();
                throw new Exception("@" + this.ToE("WN13", "������˽ڵ�����ʧ��") + ex.Message); //������˽ڵ�����ʧ��
            }
        }
        public string GenerWhySendToThem(int fNodeID, int toNodeID)
        {
            return "@<a href='../../WF/WhySendToThem.aspx?NodeID=" + fNodeID + "&ToNodeID=" + toNodeID + "&WorkID=" + this.WorkID + "' target=_blank >ΪʲôҪ���͸����ǣ�</a>";
        }
        /// <summary>
        /// ��������ID
        /// </summary>
        public static Int64 FID = 0;
        private string StartNextWorkNodeHeLiu(Node nd)
        {
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
            if (this.HisWork.FID == 0 || this.HisWork.FID == this.HisWork.OID)
            {
                /* û��FID */
            }
            else
            {
                /* �Ѿ���FID��˵������ǰ�Ѿ��з������ߺ����ڵ㡣*/
                GenerFH myfh = new GenerFH(this.HisWork.FID);
                if (myfh.FK_Node == nd.NodeID)
                {
                    // �������Ľڵ� worklist ��Ϣ��
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + myfh.FID + " WHERE WorkID=" + this.WorkID);

                    if (nd.HisFJOpen != FJOpen.None)
                        DBAccess.RunSQL("UPDATE WF_FileManager SET FID=" + myfh.FID + " WHERE WorkID=" + this.WorkID);


                    // ��ʼ������ϸ��Ȩ�ޡ�
                    foreach (BP.Sys.MapDtl dtl in dtls)
                    {
                        DBAccess.RunSQL("Update " + dtl.PTable + " SET FID=" + myfh.FID + " WHERE RefPK='" + this.HisWork.OID + "'");
                    }
                    return "@�����Ѿ����е������ڵ�[" + nd.Name + "]����ǰ�����Ѿ���ɡ�@���Ĺ����Ѿ����͸�������Ա[" + myfh.ToEmpsMsg + "]��<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "')\" >����֪ͨ����</a>��" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);
                }
            }


            //�����ҵ��˽ڵ�Ľ�����Ա�ļ��ϡ���Ϊ FID ����������FID��
            WorkNode town = new WorkNode(nd.HisWork, nd);

            // ���Ի����ǵĹ�����Ա��
            WorkerLists gwls = this.GenerWorkerLists(town);

            string groupKeys = "";
            string fk_emp = "";
            string toEmpsStr = "";
            foreach (WorkerList wl in gwls)
            {
                groupKeys += "@" + wl.FK_Emp;
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "��";
                else
                    toEmpsStr += wl.FK_Emp + "<" + wl.FK_EmpText + ">��";
            }

            GenerFH fh = new GenerFH();
            // ���� groupKeys �ܷ��ѯ�����FID ��
            int resu = fh.Retrieve(GenerFHAttr.FK_Node, nd.NodeID, GenerFHAttr.GroupKey, groupKeys);
            if (resu == 0)
            {
                /* �����ǰ�Ľڵ�û�в���FID ������ڵ��һ�ε��*/
                fh.FID = BP.DA.DBAccess.GenerOID();
                fh.WFState = (int)WFState.Runing;
                fh.GroupKey = groupKeys;
                fh.FK_Flow = nd.FK_Flow;
                fh.FK_Node = nd.NodeID;
                fh.ToEmpsMsg = toEmpsStr;
                fh.Title = "��" + DataType.CurrentData + " ���ܵĹ���." + nd.Name;
                fh.RDT = DataType.CurrentData;
                fh.Insert();

                // �����������̼�¼
                GenerWorkFlow gwf = new GenerWorkFlow();
                gwf.WorkID = fh.FID;
                gwf.FID = fh.FID;
                gwf.Title = fh.Title;
                gwf.WFState = 0;
                gwf.RDT = fh.RDT;
                gwf.Rec = Web.WebUser.No;
                gwf.FK_Flow = this.HisNode.FK_Flow;
                gwf.FK_Node = nd.NodeID;
                // gwf.FK_Station = this.HisStationOfUse.No;
                gwf.FK_Dept = this.HisWork.RecOfEmp.FK_Dept;
                try
                {
                    gwf.DirectInsert();
                }
                catch
                {
                    gwf.DirectUpdate();
                }



                /* ����������������Ա */
                // ���´�ǰ���нڵ㹤����FID��

                if (this.HisWork.FID == 0)
                {
                    /*���FID=0��˵����ǰ��N���ڵ㶼û�в����� FID����FID ���µ�ǰ��ȥ��*/
                    this.HisWork.Update(WorkAttr.FID, fh.FID);

                    // ������ǰ�ڵ�� FID��
                    Nodes nds = new Nodes(this.HisNode.FK_Flow);
                    foreach (Node mynd in nds)
                    {
                        if (mynd.NodeID == this.HisNode.NodeID || mynd.NodeID == nd.NodeID)
                            continue;

                        if (mynd.IsEndNode)
                            continue;

                        Work wk1 = mynd.HisWork;
                        wk1.OID = this.WorkID;
                        if (wk1.IsGECheckStand)
                            wk1.SetValByKey(GECheckStandAttr.NodeID, mynd.NodeID);

                        wk1.Update(WorkAttr.FID, fh.FID);
                    }
                }



                /* ���� WF_GenerWorkFlow WF_WorkerList */
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FID=" + fh.FID + "   WHERE WorkID=" + this.WorkID);
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + fh.FID + ",WorkID=" + fh.FID + " WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);

                if (nd.HisFJOpen != FJOpen.None)
                    DBAccess.RunSQL("UPDATE WF_FileManager SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);



                // ������ǰ�ڵ�Ĺ�����¼.
                Work wk = nd.HisWork;
                wk.NodeID = nd.NodeID;
                wk.OID = fh.FID;
                wk.FID = fh.FID;
                wk.Rec = fk_emp;
                try
                {
                    wk.Insert();
                }
                catch
                {

                }


                // ��ʼ������ϸ��Ȩ�����⡣
                foreach (BP.Sys.MapDtl dtl in dtls)
                {
                    DBAccess.RunSQL("Update " + dtl.PTable + " SET FID=" + fh.FID + " WHERE RefPK='"   + this.WorkID + "'");
                }

                return "@��ǰ�����Ѿ���ɣ������Ѿ����е������ڵ�[" + nd.Name + "]�����ǵ�һλ����˽ڵ����Ա��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID="+this.WorkID+"&NodeID="+nd.NodeID+"')\" >����֪ͨ����</a>��";
            }
            else
            {
                /* ɾ���Ѿ���������Ա���ϣ���Ϊ���������Ѿ�û�����ã�ǰ���Ѿ�������ˡ���ɾ���ͻ�����ظ������⡣*/
                BP.DA.DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);


                /* ���� WF_GenerWorkFlow WF_WorkerList */
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FID=" + fh.FID + "   WHERE WorkID=" + this.WorkID);
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);

                if (nd.HisFJOpen != FJOpen.None)
                    DBAccess.RunSQL("UPDATE WF_FileManager SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);



                if (this.HisWork.FID == 0)
                {
                    /*���FID=0��˵����ǰ��N���ڵ㶼û�в����� FID����FID ���µ�ǰ��ȥ��*/
                    this.HisWork.Update(WorkAttr.FID, fh.FID);

                    // ������ǰ�ڵ�� FID��
                    Nodes nds = new Nodes(this.HisNode.FK_Flow);
                    foreach (Node mynd in nds)
                    {
                        if (mynd.NodeID == this.HisNode.NodeID || mynd.NodeID == nd.NodeID)
                            continue;

                        if (mynd.IsEndNode)
                            continue;

                        Work wk = mynd.HisWork;
                        wk.OID = this.WorkID;
                        if (wk.IsGECheckStand)
                            wk.SetValByKey(GECheckStandAttr.NodeID, mynd.NodeID);

                        wk.Update(WorkAttr.FID, fh.FID);
                    }
                }

                /* ��� ������ FID���͸��µ�ǰ��״̬ ���䷵�ء�*/
                //fh.FID = this.HisWork.FID;
                //fh.Retrieve();
                //fh.FK_Node = nd.NodeID;
                //fh.GroupKey = groupKeys;
                //fh.Update();

                GenerWorkFlow mygwf = new GenerWorkFlow();
                mygwf.WorkID = this.WorkID;
                int i = mygwf.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("ϵͳ�쳣");

                mygwf.FK_Node = nd.NodeID;
                mygwf.Update(GenerWorkFlowAttr.FK_Node, nd.NodeID);

                // ��ʼ������ϸ��Ȩ�����⡣
                foreach (BP.Sys.MapDtl dtl in dtls)
                {
                    DBAccess.RunSQL("Update " + dtl.PTable + " SET FID=" + fh.FID + " WHERE RefPK='"  + this.WorkID + "'");
                }

                return "@��ǰ�����Ѿ���ɣ������Ѿ����е������ڵ�[" + nd.Name + "]�������ǵ�һ������˽ڵ����Ա��@���Ĺ����Ѿ����͸�������Ա[" + toEmpsStr + "]��<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "')\" >����֪ͨ����</a>��";
            }
        }
        private void UpdateHeLiuFID()
        {
        }
        /// <summary>
        /// ������һ�������ڵ�
        /// </summary>
        /// <param name="nd">�ڵ�</param>		 
        /// <returns></returns>
        private string StartNextWorkNodeOrdinary(Node nd)
        {
            string sql = "";
            try
            {
                #region  ��ʼ������Ĺ����ڵ㡣
                Work wk = nd.HisWork;
                wk.SetValByKey("OID", this.HisWork.OID); //�趨����ID.
                wk.Copy(this.HisWork); // ִ��copy ��һ���ڵ�����ݡ�
                wk.Rec = "";
                wk.NodeState = NodeState.Init; //�ڵ�״̬��
                wk.Rec = BP.Web.WebUser.No;
                try
                {
                    wk.Insert();
                }
                catch
                {
                }

                // ������ϸ���ݡ�
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + this.HisNode.NodeID);
                Sys.MapDtls toDtls = new BP.Sys.MapDtls("ND" + nd.NodeID);

                int i = -1;
                foreach (Sys.MapDtl dtl in dtls)
                {
                    i++;
                    if (toDtls.Count < i)
                        continue;

                    Sys.MapDtl toDtl = (Sys.MapDtl)toDtls[i];
                    if (toDtl.IsCopyNDData == false)
                        continue;


                    //��ȡ��ϸ���ݡ�
                    GEDtls gedtls = new GEDtls(dtl.No);
                    QueryObject qo = null;
                    qo = new QueryObject(gedtls);
                    switch (dtl.DtlOpenType)
                    {
                        case DtlOpenType.ForEmp:
                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                            //qo.addAnd();
                            //qo.AddWhere(GEDtlAttr.Rec, WebUser.No);
                            break;
                        case DtlOpenType.ForWorkID:
                            qo.AddWhere(GEDtlAttr.RefPK,  this.WorkID);
                            break;
                        case DtlOpenType.ForFID:
                            qo.AddWhere(GEDtlAttr.FID,  this.WorkID);
                            break;
                    }
                    qo.DoQuery();

                    foreach (GEDtl gedtl in gedtls)
                    {
                        BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                        dtCopy.Copy(gedtl);
                        dtCopy.FK_MapDtl = toDtl.No;

                        dtCopy.RefPK = this.WorkID.ToString();
                        try
                        {
                            dtCopy.InsertAsOID(dtCopy.OID);
                        }
                        catch
                        {
                        }
                    }

                }


              //  wk.CopyCellsData("ND" + this.HisNode.NodeID + "_" + this.HisWork.OID);

                #endregion

                try
                {
                    wk.BeforeSave();
                }
                catch
                {
                }

                try
                {
                    wk.DirectSave();
                }
                catch (Exception ex)
                {
                    Log.DebugWriteInfo(this.ToE("SaveWorkErr", "���湤������") + "��" + ex.Message);
                    throw new Exception(this.ToE("SaveWorkErr", "���湤������  ") + wk.EnDesc + ex.Message);
                }
                // ����һ�������ڵ�.
                string msg = this.beforeStartNode(wk, nd);
                return "@" + string.Format(this.ToE("NStep", "@��{0}��"), nd.Step.ToString()) + "<font color=blue>" + nd.Name + "</font>" + this.ToE("WorkStartOK", "�����ɹ�����") + "." + msg;
            }
            catch (Exception ex)
            {
                nd.HisWorks.DoDBCheck(DBLevel.Middle);
                throw new Exception(string.Format("StartGEWorkErr", nd.Name) + "@" + ex.Message + sql);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        private Work _HisWork = null;
        /// <summary>
        /// ����
        /// </summary>
        public Work HisWork
        {
            get
            {
                return this._HisWork;
            }
        }
        /// <summary>
        /// �ڵ�
        /// </summary>
        private Node _HisNode = null;
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Node HisNode
        {
            get
            {
                return this._HisNode;
            }
        }
        private RememberMe _RememberMe = null;
        public RememberMe GetHisRememberMe(Node nd)
        {
            if (_RememberMe == null || _RememberMe.FK_Node != nd.NodeID)
            {
                _RememberMe = new RememberMe();
                _RememberMe.FK_Emp = Web.WebUser.No;
                _RememberMe.FK_Node = nd.NodeID;
                _RememberMe.RetrieveFromDBSources();
            }
            return this._RememberMe;
        }
        private WorkFlow _HisWorkFlow = null;
        /// <summary>
        /// ��������
        /// </summary>
        public WorkFlow HisWorkFlow
        {
            get
            {
                if (_HisWorkFlow == null)
                    _HisWorkFlow = new WorkFlow(this.HisNode.HisFlow, this.HisWork.OID, this.HisWork.FID);
                return _HisWorkFlow;
            }
        }
        /// <summary>
        /// ��׼��check, �����ǰ�Ĺ���������˹���.��throw new exception
        /// </summary>
        /// <returns></returns>
        private GECheckStand GetGECheckStand()
        {
            return (GECheckStand)this.HisWork;
        }
        /// <summary>
        /// ��ǰ�ڵ�Ĺ����ǲ�����ɡ�
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (this.HisWork.NodeState == NodeState.Complete)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����һ�������ڵ�����.
        /// </summary>
        /// <param name="workId">����ID</param>
        /// <param name="nodeId">�ڵ�ID</param>
        public WorkNode(Int64 workId, int nodeId)
        {
            this.WorkID = workId;



            Node nd = new Node(nodeId);
            Work wk = nd.HisWork;
            wk.OID = workId;
            if (nd.IsCheckNode)
            {
                wk.SetValByKey(GECheckStandAttr.NodeID, nodeId);
            }

            wk.Retrieve();

            this._HisWork = wk;
            this._HisNode = nd;
        }
        /// <summary>
        /// ����һ�������ڵ�����
        /// </summary>
        /// <param name="nd">�ڵ�ID</param>
        /// <param name="wk">����</param>
        public WorkNode(Work wk, Node nd)
        {
            this.WorkID = wk.OID;

            //Node nd  = new Node(ndId);
            // if (nd.HisWorks.GetNewEntity.ToString() != wk.ToString())
            //   throw new Exception("@���������ӵ�ʧ��:����ڵ��" + nd.Name + "�ݲɼ���Ϣ��ŵ�ʵ��[" + nd.WorksEnsName + "]��������ʵ��[" + wk.ToString() + "]��һ�£�");
            this._HisWork = wk;
            this._HisNode = nd;
        }
        #endregion

        #region ��������
        private void Repair()
        {


        }
        /// <summary>
        /// �õ�������һ������
        /// 1, �ӵ�ǰ���ҵ�������һ�������Ľڵ㼯��.		 
        /// ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
        /// </summary>
        /// <returns>�õ�������һ������</returns>
        public WorkNode GetPreviousWorkNode()
        {
            // ���û���ҵ�ת�����Ľڵ�,�ͷ���,��ǰ�Ĺ���.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "�˽ڵ��ǿ�ʼ�ڵ�,û����һ������")); //�˽ڵ��ǿ�ʼ�ڵ�,û����һ������.

            WorkNodes wns = new WorkNodes();
            foreach (Node nd in this.HisNode.HisFromNodes)
            {

                Work wk = (Work)nd.HisWorks.GetNewEntity;
                wk.OID = this.HisWork.OID;
                if (nd.IsCheckNode)
                {
                    wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                }

                if (wk.RetrieveFromDBSources() == 0)
                {
                    continue;
                    //if (this.HisNode.HisFromNodes.Count == 1)
                    //{
                    //    throw new Exception("@ϵͳ������δ֪���쳣����֪ͨ����Ա������Workid=[" + wk.OID + "]����������һ��ͬ�³������͡������ñ����ع���Ա�û���½=�����칤��=�����̲�ѯ=���ڹؼ���������["+this.WorkID+"]��������ѡ��ȫ������ѯ��������ɾ������������Ϣ:currNodeID ="+ this.HisNode.NodeID );

                    //    ///*�ָ���*/
                    //    //wk.NodeState = NodeState.Complete;
                    //    //wl.FK_Node = nd.NodeID;
                    //    //wl.RDT = DataType.CurrentData;
                    //    //wl.FK_Flow = nd.FK_Flow;
                    //    //wl.SDT = wl.RDT;
                    //    //wl.DTOfWarning = wl.RDT;
                    //    //wl.IsEnable = true;
                    //    //wl.Insert();
                    //    //Log.DefaultLogWriteLineWarning("@�Զ��޸��ˣ�û���ҵ���һ�������Ĵ���WorkID=" + this.HisWork.OID);
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                }

                //WorkerList wl = new WorkerList();
                //if (wl.Retrieve(WorkerListAttr.FK_Node,nd.NodeID, WorkerListAttr.WorkID, wk.OID,
                WorkNode wn = new WorkNode(wk, nd);
                wns.Add(wn);
            }
            switch (wns.Count)
            {
                case 0:
                    throw new Exception(this.ToE("WN15", "û���ҵ�������һ������,ϵͳ������֪ͨ����Ա��������������һ��ͬ�³������͡������ñ����ع���Ա�û���½=�����칤��=�����̲�ѯ=���ڹؼ���������Workid��������ѡ��ȫ������ѯ��������ɾ������") + "@WorkID=" + this.WorkID);
                case 1:
                    return (WorkNode)wns[0];
                default:
                    break;
            }
            Node nd1 = wns[0].HisNode;
            Node nd2 = wns[1].HisNode;
            if (nd1.HisFromNodes.Contains(NodeAttr.NodeID, nd2.NodeID))
            {
                return wns[0];
            }
            else
            {
                return wns[1];
            }
        }
        /// <summary>
        /// �õ�������һ������.
        /// �����ǰ������û�д������״̬,�ͷ��ص�ǰ�Ĺ���.
        /// </summary>
        /// <returns>�õ�������һ������</returns>
        private WorkNode GetNextWorkNode()
        {
            // �����ǰ������û�д������״̬,�ͷ��ص�ǰ�Ĺ���.
            if (this.HisWork.NodeState != NodeState.Complete)
                throw new Exception(this.ToE("WN16", "@�˽ڵ�Ĺ�������û�����,û����һ������.")); //"@�˽ڵ�Ĺ�������û�����,û����һ������."

            // �������һ�������ڵ�
            if (this.HisNode.IsEndNode)
                throw new Exception(this.ToE("ND17", "@�˽ڵ��ǽ����ڵ�,û����һ������.")); // "@�˽ڵ��ǽ����ڵ�,û����һ������."

            // throw new Exception("@��ǰ�Ĺ���û���������,������");
            Nodes nds = this.HisNode.HisToNodes;
            if (nds.Count == 0)
                throw new Exception("@û���ҵ��ӵ�ǰ�ڵ㡾" + this.HisNode.Name + "����ת��ڵ㣬��������һ�����ڵ㣬����ȡ����һ�������ڵ㡣");

            foreach (Node nd in nds)
            {
                Work wk = (Work)nd.HisWorks.GetNewEntity;
                if (nd.IsCheckNode)
                {
                    wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                }
                wk.OID = this.HisWork.OID;

                if (wk.IsExits == false)
                    continue;

                wk.Retrieve();
                WorkNode wn = new WorkNode(wk, nd);
                return wn;
            }

            if (nds.Count == 1)
            {
                /* ��һ�������,������ļ�¼���Ƿ�ɾ��.
                 * ���Ȱ취��:
                 * 1,�ѵ�ǰ�����̵ĵ�ǰ�����ڵ�����Ϊ��ǰ�Ľڵ�.,
                 * 2,������Ϊ������Ա.
                 * */
                // �ж��ǲ�������һ���Ĺ�����Ա�б�.
                Node nd = (Node)nds[0];
                WorkerLists wls = new WorkerLists(this.HisWork.OID, nd.NodeID);
                if (wls.Count == 0)
                {
                    /*˵��û�в����������б�.*/
                    this.HisWork.NodeState = NodeState.Init;
                    this.HisWork.DirectUpdate();
                    GenerWorkFlow wgf = new GenerWorkFlow(this.HisWork.OID, this.HisNode.FK_Flow);
                    throw new Exception("@��ǰ�Ĺ���û����ȷ�Ĵ���, û����һ���蹤���ڵ�.Ҳ��������Ƿ�ɾ���˹������еļ�¼,��ɵ�û���ҵ�,��һ����������,����ϵͳ�Ѿ��ָ���ǰ�ڵ�ΪΪ���״̬,�����̿���������������ȥ.");
                }
                else
                {
                    /*˵���Ѿ������������б�.*/
                    DBAccess.RunSQL("delete WF_GenerWorkerList where WorkID=" + this.HisWork.OID + " and FK_Node=" + nd.NodeID);
                }
            }
            throw new Exception("@û���ҵ���һ���蹤���ڵ�[" + this.HisNode.Name + "]����һ������,���̴���.��Ѵ����ⷢ�͸�������Ա��");
        }
        #endregion
    }
    /// <summary>
    /// �����ڵ㼯��.
    /// </summary>
    public class WorkNodes : CollectionBase
    {
        #region ����
        /// <summary>
        /// ���Ĺ���s
        /// </summary> 
        public Works GetWorks
        {
            get
            {
                if (this.Count == 0)
                    throw new Exception("@��ʼ��ʧ�ܣ�û���ҵ��κνڵ㡣");

                Works ens = this[0].HisNode.HisWorks;
                ens.Clear();

                foreach (WorkNode wn in this)
                {
                    ens.AddEntity(wn.HisWork);
                }
                return ens;
            }
        }
        /// <summary>
        /// �����ڵ㼯��
        /// </summary>
        public WorkNodes()
        {
        }

        public int GenerByFID(Flow flow, Int64 fid)
        {
            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                if (nd.HisFNType != FNType.River)
                    continue;

                Work wk = nd.GetWork(fid);
                if (wk == null)
                    continue;

                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }

        public int GenerByWorkID(Flow flow, Int64 oid)
        {
            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                if (nd.HisFNType == FNType.River)
                    continue;

                Work wk = nd.GetWork(oid);
                if (wk == null)
                    continue;

                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }
        /// <summary>
        /// ɾ����������
        /// </summary>
        public void DeleteWorks()
        {
            foreach (WorkNode wn in this)
            {
                wn.HisWork.Delete();
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ��WorkNode
        /// </summary>
        /// <param name="wn">���� �ڵ�</param>
        public void Add(WorkNode wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public WorkNode this[int index]
        {
            get
            {
                return (WorkNode)this.InnerList[index];
            }
        }
        #endregion
    }
}

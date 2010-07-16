using System;
using System.Data;
using BP.DA ; 
using System.Collections;
using BP.En;
using BP.WF;
using BP.Port ; 
using BP.En;
using BP.DTS;

namespace BP.WF.DTS
{
    public class CheckNodes : DataIOEn
    {
        /// <summary>
        /// ������Ա,��λ,����
        /// </summary>
        public CheckNodes()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "�޸��ڵ���Ϣ";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {

            MDCheck md = new MDCheck();
            md.Do();

            //ִ�е��Ȳ��š�
            //BP.Port.DTS.GenerDept gd = new BP.Port.DTS.GenerDept();
            //gd.Do();

            // ������Ա��Ϣ��
            // Emp emp = new Emp(Web.WebUser.No);
            // emp.DoDTSEmpDeptStation();
        }
    }


    public class UserPort : DataIOEn2
    {
        /// <summary>
        /// ������Ա,��λ,����
        /// </summary>
        public UserPort()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "�������̲���(������ϵͳ��һ�ΰ�װʱ���߲��ű仯ʱ)";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {

            //ִ�е��Ȳ��š�
            //BP.Port.DTS.GenerDept gd = new BP.Port.DTS.GenerDept();
            //gd.Do();

            // ������Ա��Ϣ��
            // Emp emp = new Emp(Web.WebUser.No);
            // emp.DoDTSEmpDeptStation();
        }
    }
    public class AddEmpLeng : DataIOEn2
    {
        public AddEmpLeng()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "Ϊ����Ա��ų�������";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = "";
            string sql2 = "";
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            foreach (Node nd in nds)
            {
                sql += "\n UPDATE " + nd.PTable + " SET EMPS=','||EMPS  WHERE substr(emps,0,1)='0';";
                sql2 += "\n UPDATE " + nd.PTable + " SET EMPS=REPLACE( EMPS, ',0',',010' )  WHERE EMPS NOT LIKE '%,010%';";
            }

            Log.DebugWriteInfo(sql);
            Log.DebugWriteInfo("===========================" + sql2);
        }

        public void Do1()
        {
            string sql = "";
            string sql2 = "";

            ArrayList al = ClassFactory.GetObjects("BP.En.Entity");
            foreach (object obj in al)
            {
                Entity en = obj as Entity;
                Map map = en.EnMap;

                try
                {
                    if (map.IsView)
                        continue;
                }
                catch
                {
                }

                //en.CheckPhysicsTable

                string table = en.EnMap.PhysicsTable;
                foreach (Attr attr in map.Attrs)
                {
                    if (attr.Key.IndexOf("Text") != -1)
                        continue;

                    if (attr.Key == WorkAttr.Rec || attr.Key == "FK_Emp" || attr.UIBindKey == "BP.Port.Emps")
                    {
                        sql += "\n update " + table + " set " + attr.Key + "='01'||" + attr.Key + " where length(" + attr.Key + ")=6;";
                    }
                    else if (attr.Key == "Checker")
                    {
                        sql2 += "\n update " + table + " set " + attr.Key + "='01'||" + attr.Key + " where length(" + attr.Key + ")=6;";
                    }
                }
            }
            Log.DebugWriteInfo(sql);
            Log.DebugWriteInfo("===========================" + sql2);
        }
    }
    public class DelWorkFlowData : DataIOEn
    {
        public DelWorkFlowData()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "<font color=red><b>�����������</b></font>";
            
            //this.HisRunTimeType = RunTimeType.UnName;
            //this.FromDBUrl = DBUrlType.AppCenterDSN;
            //this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            if (BP.Web.WebUser.No != "admin")
            {
                throw new Exception("�Ƿ��û���");
            }

            DA.DBAccess.RunSQL("delete WF_CHOfNode");
            DA.DBAccess.RunSQL("delete WF_CHOfFlow");
            DA.DBAccess.RunSQL("delete WF_Book");
            DA.DBAccess.RunSQL("delete WF_GENERWORKERLIST");
            DA.DBAccess.RunSQL("delete WF_GENERWORKFLOW");
            DA.DBAccess.RunSQL("delete WF_WORKLIST");
            DA.DBAccess.RunSQL("delete WF_ReturnWork");
            DA.DBAccess.RunSQL("delete WF_GECheckStand");
            DA.DBAccess.RunSQL("delete WF_GECheckMul");
            DA.DBAccess.RunSQL("delete WF_ForwardWork");
            DA.DBAccess.RunSQL("delete WF_SelectAccper");

            Nodes nds = new Nodes();
            nds.RetrieveAll();

            string msg = "";
            foreach (Node nd in nds)
            {
                if (nd.IsCheckNode)
                    continue;
                Work wk =  null;

                try
                {
                    wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE " + wk.EnMap.PhysicsTable);
                }
                catch (Exception ex)
                {
                    wk.CheckPhysicsTable();
                    msg += "@" + ex.Message;
                }
            }

            if (msg != "")
                throw new Exception(msg);
        }
    }
    public class InitBookDir : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public InitBookDir()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "<font color=green><b>��������Ŀ¼(������ÿ�θ��������ĺŻ�ÿ��һ��)</b></font>";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        /// <summary>
        /// ��������Ŀ¼
        /// </summary>
        public override void Do()
        {

            Depts Depts = new Depts();
            QueryObject qo = new QueryObject(Depts);
      //      qo.AddWhere("Grade", " < ", 4);
            qo.DoQuery();

            BookTemplates funcs = new BookTemplates();
            funcs.RetrieveAll();


            string path = BP.WF.Glo.FlowFile + "\\Book\\" ;
            string year = DateTime.Now.Year.ToString();

            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            if (System.IO.Directory.Exists(path + "\\\\" + year) == false)
                System.IO.Directory.CreateDirectory(path + "\\\\" + year);


            foreach (Dept Dept in Depts)
            {
                if (System.IO.Directory.Exists(path + "\\\\" + year + "\\\\" + Dept.No) == false)
                    System.IO.Directory.CreateDirectory(path + "\\\\" + year + "\\\\" + Dept.No);

                foreach (BookTemplate func in funcs)
                {
                    if (System.IO.Directory.Exists(path + "\\\\" + year + "\\\\" + Dept.No + "\\\\" + func.No) == false)
                        System.IO.Directory.CreateDirectory(path + "\\\\" + year + "\\\\" + Dept.No + "\\\\" + func.No);
                }
            }
        }
    }

    public class OutputSQLs : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public OutputSQLs()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "OutputSQLs for produces DTSCHofNode";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = this.GenerSqls();
            PubClass.ResponseWriteBlueMsg(sql.Replace("\n", "<BR>"));
        }
        public string GenerSqls()
        {
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + "��ʼ���ȿ�����Ϣ:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string infoMsg = "", errMsg = "";

            Nodes nds = new Nodes();
            nds.RetrieveAll();

            string fromDateTime = DateTime.Now.Year + "-01-01";
            fromDateTime = "2004-01-01 00:00";
            //string fromDateTime=DateTime.Now.Year+"-01-01 00:00";
            //string fromDateTime=DateTime.Now.Year+"-01-01 00:00";
            string insertSql = "";
            string delSQL = "";
            string updateSQL = "";

            string sqls = "";
            int i = 0;
            foreach (Node nd in nds)
            {
                if (nd.IsPCNode)  /* ����Ǽ�����ڵ�.*/
                    continue;

                if (nd.IsCheckNode)
                    continue;

                i++;

                Map map = nd.HisWork.EnMap;
                delSQL = "\n DELETE " + map.PhysicsTable + " WHERE  OID  NOT IN (SELECT WORKID FROM wf_generworkflow ) AND NODESTATE=0 ";

                if (map.Attrs.Contains("FK_Taxpayer") && map.Attrs.Contains("TaxpayerName"))
                {
                    insertSql = "INSERT INTO WF_CHOfNode (FK_Node,FK_Flow,WorkID, NodeState,FK_Emp,Emps,RDT,CDT,FK_Taxpayer,TaxpayerName )"
                        + " "
                        + "  SELECT " + nd.NodeID + " as FK_Node, '" + nd.FK_Flow + "' as FK_Flow, OID as WorkID, NodeState, Rec,Emps,RDT,CDT,FK_Taxpayer, TaxpayerName "
                        + "  FROM " + map.PhysicsTable
                        + "  WHERE  OID NOT IN ( SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + " ) AND Rec IS NOT NULL ";
                }
                else
                {
                    insertSql = "INSERT INTO WF_CHOfNode (FK_Node,FK_Flow,WorkID,NodeState,FK_Emp,Emps,RDT,CDT  ) "
                + " "
                + "  SELECT " + nd.NodeID + " as FK_Node, '" + nd.FK_Flow + "' as FK_Flow, OID as WorkID, NodeState, Rec,Emps,RDT,CDT "
                + "  FROM " + nd.HisWork.EnMap.PhysicsTable
                + "  WHERE  OID NOT IN ( SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + " )  AND Rec IS NOT NULL ";
                }

                // ����״̬��sql.
                updateSQL = " UPDATE WF_CHOfNode  SET (WF_CHOfNode.NODESTATE,RDT,CDT) = ( SELECT NODESTATE, RDT,CDT FROM " + nd.PTable + "  WHERE WF_CHOFNODE.WorkID=" + nd.PTable + ".OID ) WHERE WF_CHOfNode.FK_NODE='" + nd.NodeID + "'";

                sqls += "\n\n\n -- NO:" + i + "��" + nd.FK_Flow + nd.FlowName + " :  " + map.EnDesc + " \n" + delSQL + "; \n" + insertSql + "; \n" + updateSQL + ";";
            }

            Log.DefaultLogWriteLineInfo(sqls);
            return sqls;
        }
    }
    public class OutputSQLOfDeleteWork : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public OutputSQLOfDeleteWork()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "����ɾ���ڵ����ݵ�sql.";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = this.GenerSqls();
            PubClass.ResponseWriteBlueMsg(sql.Replace("\n", "<BR>"));
        }
        public string GenerSqls()
        {
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            string delSQL = "";
            foreach (Node nd in nds)
            {
                delSQL += "\n DELETE " + nd.PTable + "  ; ";
            }
            return delSQL;
        }
    }
	
    /// <summary>
    /// ������Ӧ�õ��ľ�̬������
    /// </summary>
    public class WFDTS
    {
        
        /// <summary>
        /// ����ͳ�Ʒ���
        /// </summary>
        /// <param name="fromDateTime"></param>
        /// <returns></returns>
        public static string InitFlows(string fromDateTime)
        {
            return null; /* �����������Ӧ�����ˡ�*/
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + " ################# Start ִ��ͳ�� #####################");
            //ɾ�����Ŵ��������
            //DBAccess.RunSQL("DELETE WF_BadWF WHERE BadFlag='FlowDeptBad'");
            fromDateTime = "2004-01-01 00:00";

            Flows fls = new Flows();
            fls.RetrieveAll();

            CHOfFlow fs = new CHOfFlow();
            foreach (Flow fl in fls)
            {
                Node nd = fl.HisStartNode;
                try
                {
                    string sql = "INSERT INTO WF_CHOfFlow SELECT OID WorkID, " + fl.No + " as FK_Flow, WFState, ltrim(rtrim(Title)) as Title,ltrim(rtrim(WFLog)) as WFLog, Rec as FK_Emp,"
                        + " RDT, CDT, 0 as SpanDays,'' FK_Dept,"
                        + "'' as FK_Dept,'' AS FK_NY,'' as FK_AP,'' AS FK_ND, '' AS FK_YF, Rec ,'' as FK_XJ, '' as FK_Station   "
                        + " FROM " + nd.HisWork.EnMap.PhysicsTable + " WHERE RDT>='" + fromDateTime + "' AND OID NOT IN ( SELECT WorkID FROM WF_CHOfFlow  )";
                    DBAccess.RunSQL(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(fl.Name + "   " + nd.Name + "" + ex.Message);
                }
            }
            DBAccess.RunSP("WF_UpdateCHOfFlow");
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + " End ִ��ͳ�Ƶ���");
            return "";
        }
 
       
    }

    
     
}

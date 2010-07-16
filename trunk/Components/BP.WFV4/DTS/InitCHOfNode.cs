using System;
using System.Collections.Generic;
using System;
using System.Data;
using BP.DA;
using System.Collections;
using BP.En.Base;
using BP.WF;
using BP.Port;
using BP.En;
using BP.DTS;
using BP.Tax;

namespace BP.WF.DTS
{
    /// <summary>
    /// ����ʱЧ����
    /// </summary>
    public class InitCHOfNode : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public InitCHOfNode()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "�ڵ�ʱЧ����(1,���ܽڵ㹤����Ϣ��chofNode��2,��Ԥ�ڵĹ���׷�������ˣ�3,ͬ���ڵ�״̬. 4, Ϊ�ӹ�����׼�����ݡ�)��������ֹ�ִ������";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
            this.Note = "ÿ���·�����Ҫִ�еĵ��ȣ�����Ƿ���ִ����������ĵ��ȡ�";
        }
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public override void Do()
        {
            try
            {
                Log.DefaultLogWriteLineInfo("* ��ʼִ�нڵ�����ͬ����Ϊִ������׼�����ݡ� ");

                //DBAccess.RunSQL("delete dszf.zf_dtslog");

                // ִ�����ݵ�ͬ������ȡ�
                DBAccess.RunSP("dswf.DTS_NodeDate2CHofNode");

                this.InitOneToMore();

                DateTime dt = DateTime.Now;
                dt.AddMonths(-1);

                DBAccess.RunSP("dszf.heard", "ny", dt.ToString("yyyy-MM"));

                Log.DefaultLogWriteLineInfo("* Ϊִ������׼������ִ����ɡ�");
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLineError("* ִ�д���" + ex.Message);
                throw ex;
            }
        }
        public void InitOneToMore()
        {
            //DoDelete();
            // return "";

            string noInsql = "SELECT FROMND||','||TOND  FROM DSZF.ZF_RATE WHERE FROMND NOT LIKE '%QJC%'";
            // string noInsql = "SELECT FROMND||'@'||TOND FROM DSZF.ZF_RATE";
            DataTable dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            string zfrates = "";
            foreach (DataRow dr in dt_Rate.Rows)
            {
                zfrates += dr[0].ToString();
            }

            noInsql = "select nodeid from DSZF.Zf_Dot WHERE NODEID NOT LIKE '%QJC%'";
            dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            foreach (DataRow dr in dt_Rate.Rows)
            {
                zfrates += dr[0].ToString() + ",";
            }

            string sql = "";

            #region /* ��ѯ����û���굫�����ڵ���� */
            sql = "SELECT * FROM (SELECT WorkID, FK_Node, EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE FK_NODE IN (SELECT NODEID FROM WF_NODE) AND NodeState=0 AND SUBSTR(SDT,0,11) < '" + DataType.CurrentData + "' AND LENGTH(EMPS) > 10 GROUP BY WORKID,FK_NODE,EMPS) WHERE NUM >1";
            DataTable dt_no = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_no in dt_no.Rows)
            {
                string node = dr_no["FK_Node"].ToString();

                if (IsInit(zfrates, node))
                    continue; /*�����������*/

                string myemps = dr_no["Emps"].ToString();
                string WorkID = dr_no["WorkID"].ToString();
                int fk_node;
                try
                {
                    fk_node = int.Parse(node);
                }
                catch
                {
                    continue;
                }

                // �ҵ����е�һ��Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    continue;

                // throw new Exception("�����ܳ��ֵ������");

                Node nd = new Node();
                nd.NodeID = fk_node;
                nd.RetrieveFromDBSources();

                //NodeExt nd = new NodeExt(fk_node);

                string[] emps = myemps.Split(',');
                foreach (string emp in emps)
                {
                    if (emp == "" || emp == null)
                        continue;

                    if (emp == cn.FK_Emp)
                    {
                        /*������Ѿ������˼�¼��*/
                        continue;
                    }

                    CHOfNode mycn = new CHOfNode();
                    mycn.Copy(cn);
                    mycn.NodeState = 0;
                    mycn.FK_Emp = emp;
                    mycn.CDT = "��";
                    mycn.CentOfAdd = 0;
                    mycn.CentOfCut = nd.DeductCent;
                    mycn.Cent = 0;
                    mycn.IsMyDeal = false;
                    mycn.Save();
                }
                /*ִ�п۷�*/
                DBAccess.RunSQL("UPDATE WF_CHofNode SET CentOfCut=" + nd.DeductCent + " WHERE FK_Node=" + nd.NodeID + " and workid='" + WorkID + "'");
            }
            #endregion

            #region /* ��ѯ���Ѿ����굫�����ڵ���� */ 
            sql = "SELECT * FROM (SELECT WorkID, FK_Node, EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE NodeState=1 AND SUBSTR(CDT,0,11) >= SUBSTR(SDT,0,11 ) GROUP BY WORKID,FK_NODE,EMPS) WHERE NUM>1";
            DataTable dt_yes = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_yes in dt_yes.Rows)
            {
                
                string node = dr_yes["FK_Node"].ToString();
                if (IsInit(zfrates, node))
                    continue; /*�����������*/

                if (zfrates.IndexOf(node) >= 0)
                    continue;

                string myemps = dr_yes["Emps"].ToString();
                string WorkID = dr_yes["WorkID"].ToString();
                int fk_node = int.Parse(dr_yes["FK_Node"].ToString());


                // �ҵ����е�һ��Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    continue;

                Node nd = new Node(fk_node);

                string[] emps = myemps.Split(',');
                foreach (string emp in emps)
                {
                    if (emp == "" || emp == null)
                        continue;

                    if (emp == cn.FK_Emp)
                    {
                        /*������Ѿ������˼�¼��*/
                        continue;
                    }

                    CHOfNode mycn = new CHOfNode();
                    mycn.Copy(cn);
                    mycn.FK_Emp = emp;
                    //��������ڵ�������Ѿ������˴˹�������ô��������
                    int a = mycn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node, CHOfNodeAttr.FK_Emp, mycn.FK_Emp);
                    if (a == 1)
                        continue;

                    mycn.CDT = "��";
                    mycn.NodeState = 0;
                    mycn.CentOfAdd = 0;
                    mycn.CentOfCut = nd.DeductCent;
                    mycn.Cent = 0;
                    mycn.Save();
                }
            }
            #endregion
        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        /// <param name="zfrates"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsInit(string zfrates, string node)
        {
            if (zfrates.IndexOf(node + "@") != -1)
                return true;

            if (zfrates.IndexOf("," + node + ",") != -1)
                return true;

            if (zfrates.IndexOf(node + ";") != -1)
                return true;

            if (zfrates.IndexOf("@" + node) != -1)
                return true;

            return false;
        }
        /// <summary>
        /// ɾ����ֽ����������
        /// </summary>
        public void DoDelete()
        {
            string sql = "SELECT * FROM (SELECT WorkID, FK_Node, EMPS, COUNT(*) AS NUM FROM DSWF.WF_CHOFNODE WHERE LENGTH(EMPS) > 10 GROUP BY WORKID, FK_NODE, EMPS ) WHERE NUM>1 ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            string noInsql = "SELECT FROMND||','||TOND  FROM DSZF.ZF_RATE WHERE FROMND NOT LIKE '%QJC%'";
            DataTable dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            string zfrates = "";
            foreach (DataRow dr in dt_Rate.Rows)
                zfrates += dr[0].ToString();

            noInsql = "select nodeid from DSZF.ZF_Dot WHERE NODEID NOT LIKE '%QJC%'";
            dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            foreach (DataRow dr in dt_Rate.Rows)
            {
                zfrates += dr[0].ToString() + ",";
            }

            foreach (DataRow dr in dt.Rows)
            {
                int workid = int.Parse(dr["WorkID"].ToString());
                string node = dr["FK_Node"].ToString();

                if (IsInit(zfrates, node) == false)
                    continue;

                int fk_node = int.Parse(node);
                Node nd = new Node(fk_node);
                Work wk = nd.HisWork;

                if (wk.IsCheckWork)
                {
                    wk.SetValByKey(CheckWorkAttr.NodeID, fk_node);
                }

                wk.OID = workid;
                try
                {
                    wk.Retrieve();
                }
                catch
                {
                    continue;
                }

                //sql = "SELECT * FROM  DSWF.WF_CHOFNODE WHERE FK_NODE='"+node+"' AND WORKID='"+workid+"' AND FK_EMP='"+wk.Recorder+"'";
                //if (

                sql = "DELETE DSWF.WF_CHOFNODE WHERE FK_NODE='" + node + "' AND WORKID='" + workid + "' AND FK_EMP!='" + wk.Recorder + "' ";
                // Log.DebugWriteInfo(sql);
                DBAccess.RunSQL(sql);
            }
        }
    }
}

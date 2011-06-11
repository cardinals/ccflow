using System;
using System.Data;
using BP.DA ; 
using System.Collections;
using BP.En.Base;
using BP.WF;
using BP.Port ; 
using BP.En;
using BP.DTS;
using BP.Tax;

namespace BP.WF.DTS
{
    public class SelfTest : DataIOEn
    {
        /// <summary>
        /// ����ʱЧ����
        /// </summary>
        public SelfTest()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "�����Զ����";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        /// <summary>
        /// ��������Ŀ¼
        /// </summary>
        public override void Do()
        {
            string msg = "";
            string info = "";
            // --------------- ΢����˰�Զ������Ϣ
            try
            {
                msg += "==========================================================================";
                info = this.CheckCT();
                msg += "@<b>΢����˰�Զ������Ϣ:</b>@" + info;


                Log.DefaultLogWriteLineInfo("΢����˰�����Ϣ��" + info);
            }
            catch (Exception ex1)
            {
                msg += "\n@΢����˰�Զ���������Ϣ:@" + ex1.Message;
                Log.DefaultLogWriteLineError("΢����˰�Զ������Ϣ��" + ex1.Message);
            }

            // --------------- ���̶�ʧ�Զ����
            try
            {
                msg += "==========================================================================";
                info = this.LostFlowTest();

                msg += "@<B>���̶�ʧ�Զ����<B>@" + info;

                Log.DefaultLogWriteLineError("���̶�ʧ�Զ���⣺��ɣ�" + info);
            }
            catch (Exception ex)
            {
                msg += "\n@���̶�ʧ�Զ������ִ���:" + ex.Message;

                Log.DefaultLogWriteLineError("���̶�ʧ�Զ������ִ���" + ex.Message);
            }


            // --------------- �����Զ������Ϣ
            try
            {
                msg += "==========================================================================";
                info = this.FlowTest();
                msg += "@<B>�����Զ������Ϣ</b>@" + info;
            }
            catch (Exception ex)
            {
                msg += "\n@�������Զ����ʱ�����:" + ex.Message;
                Log.DefaultLogWriteLineError("���̶�ʧ�Զ������ִ���" + ex.Message);
            }

            //  Log.DefaultLogWriteLineInfo( msg.Replace("@", "\n@"));

            if (BP.SystemConfig.IsBSsystem)
                PubClass.Alert(msg);
        }
        /// <summary>
        /// �����˻��Զ���⣬ ��ǰ�ڵ㲻�ڱ������С�
        /// </summary>
        /// <returns></returns>
        public string CannotbackTest()
        {
            GenerWorkFlows ens = new GenerWorkFlows();
            ens.RetrieveAll(100000);
            foreach (GenerWorkFlow en in ens)
            {
                


            }

            return "";

        }
        /// <summary>
        /// ���̶�ʧ�Զ����
        /// </summary>
        /// <returns></returns>
        public string LostFlowTest()
        {
            int num = 0;
            string msg = "";
            string sql = "UPDATE dswf.WF_GenerWorkerList SET dswf.WF_GenerWorkerList.IsEnable=1 WHERE WORKID IN (SELECT WORKID FROM ( SELECT WORKID, COUNT(*) AS NUM FROM dswf.WF_GenerWorkerList WHERE dswf.WF_GenerWorkerList.Isenable=1 AND  WORKID IN (SELECT WORKID FROM dswf.WF_GENERWORKFLOW WHERE dswf.WF_GENERWORKFLOW.FK_CURRENTNODE = dswf.WF_GenerWorkerList.Fk_Node)    GROUP BY WORKID)  WHERE  NUM =0 ) AND WF_GenerWorkerList.Fk_Node IN ( SELECT WF_GENERWORKFLOW.FK_CURRENTNODE FROM dswf.WF_GENERWORKFLOW WHERE dswf.WF_GenerWorkerList.Workid = dswf.WF_GENERWORKFLOW.WORKID )";

            num = DBAccess.RunSQL(sql);
            msg += "@��ʧ���̲����Զ��ָ����� " + num + "����";


            // ��ѯ��������ע�����û����ɵ����̣�����Ҳû���������б��С�
            sql = "SELECT WorkID FROM WF_CHOFFLOW WHERE WORKID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW) AND WFSTATE=0";
            BP.WF.CHOfFlows fls = new CHOfFlows();
            fls.RetrieveInSQL(BP.WF.CHOfFlowAttr.WorkId, sql);
            if (fls.Count == 0)
                return  msg;

            msg += "@����ע�������" + fls.Count + "�����������ݣ���ѯ��䣺" + sql + "��@ϵͳ���Զ��޸����ǣ��޸�������£�";
            foreach (CHOfFlow fl in fls)
            {
                msg += "@��" + fl.FK_Taxpayer + fl.TaxpayerName + "����:" + fl.FK_Flow + "����:" + fl.RDT + "��workid=" + fl.WorkId + "��ʼ�ڵ���в�������������׼��ɾ����";
                Flow flow = new Flow(fl.FK_Flow);
                StartWork wk = flow.HisStartNode.HisWork as StartWork;
                wk.OID = fl.WorkId;
                if (wk.RetrieveFromDBSources() == 0)
                {
                    fl.Delete(); //�����ʼ�ڵ��в�����������¼����ɾ������
                    // ɾ�������ڵ��ϵ����ݣ��������в�̫���ܴ��ڡ�
                    Nodes nds = flow.HisNodes;
                    foreach (Node nd in nds)
                    {
                        try
                        {
                            Work mywk = nd.HisWork;
                            mywk.OID = fl.WorkId;
                            if (mywk.IsCheckWork)
                                mywk.SetValByKey(CheckWorkAttr.NodeID, nd.NodeID);
                            mywk.Delete();
                        }
                        catch
                        {

                        }
                    }
                    msg += "@��ʼ�ڵ���в��������������Ѿ�ɾ����";
                }
                else
                {
                    if (wk.WFState == WFState.Complete)
                    {
                        /*����Ѿ����*/
                        fl.WFState = (int)WFState.Complete;
                        fl.DirectUpdate();
                        msg += "@���ڽڵ���е�״̬��1������ע����е�״̬��Ϊ1���Զ�������";
                    }
                }
            }

            Log.DefaultLogWriteLineInfo(msg);
            return msg;
        }
        public string CheckCT()
        {
            string msg = "";
            string sql = "";

            // �ڵ�ͣ��



            sql = " DELETE dswf.WF_GENERWORKFLOW WHERE WORKID IN (SELECT WorkID FROM  DSCT.CT_HD WHERE NO NOT IN ( select qybm from dsbm.djsw ) )";
            int i = DBAccess.RunSQL(sql);
            msg += "@����(" + i + ")��ע��������΢����˰ϵͳ�б�ɾ����";

            sql = " DELETE dswf.wf_generworkerlist WHERE WORKID IN (SELECT WorkID FROM  DSCT.CT_HD WHERE NO NOT IN (SELECT qybm FROM dsbm.djsw) )";
            DBAccess.RunSQL(sql);

            sql = "DELETE DSCT.CT_HD WHERE NO NOT IN (SELECT qybm FROM dsbm.djsw )";
            DBAccess.RunSQL(sql);

            sql = "DELETE FROM DSWF.WF_GENERWORKFLOW WHERE FK_FLOW='220' AND WORKID NOT IN ( SELECT WORKID FROM  DSCT.CT_HD WHERE (STATEOFHD IN ('2','3','4','1') ) )";
            DBAccess.RunSQL(sql);

            try
            {
                // ͬ��˰�����Ա����˰��������Ϣ�� ��ֹ˰�����Ա�仯���������û�б仯��
                sql = "update dsct.ct_hd SET (FK_Emp, Name )  = ( select sgy,qymc from dsbm.djsw where dsbm.djsw.qybm =dsct.ct_hd.no)  ";
                DBAccess.RunSQL(sql);
            }
            catch (Exception ex)
            {
                msg += "@��ȷ�� dsbm.djsw �е���˰�˱������ �Ƿ��ظ�. ���sql = select * from ( select no, count(*) n from dswf.ds_taxpayer group by no) WHERE  N>1 ";
            }
            Log.DefaultLogWriteLineInfo(msg);
            return msg;
        }
        /// <summary>
        /// �����Ѿ���ɵ���������ע����в�����
        /// </summary>
        public string FlowTestV2()
        {
            string msg = "";

            Flows fls = new Flows();
            fls.RetrieveAll();
            int i = 1;
            foreach (Flow fl in fls)
            {
                BP.WF.Node nd = new BP.WF.Node();
                if (fl.No.IndexOf("XN") != -1)
                    continue;

                try
                {
                    nd = fl.HisStartNode;
                }
                catch
                {
                    continue;
                }

                string sql = "SELECT OID FROM " + nd.PTable + " WHERE WFState=1 AND OID NOT IN (SELECT WORKID FROM WF_CHOFFLOW) ";
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    continue;

                StartWorks sws = fl.HisStartNode.HisWorks as StartWorks;
                sws.RetrieveInSQL(StartWorkAttr.OID, sql);
                foreach (StartWork sw in sws)
                {
                    CHOfFlow cf = new CHOfFlow();
                    cf.Copy(sw);
                    cf.WorkId = sw.OID;
                    cf.FK_Flow = fl.No;
                    cf.FK_Emp = sw.Recorder;
                    cf.WFState = (int)WFState.Complete;
                    cf.FK_NY = sw.Record_FK_NY;
                    cf.Insert();
                    msg += "@����ע���:" + fl.Name + " WorkID=" + sw.OID + " �Ѿ��޸���";
                }
            }
            return msg;
        }
        /// <summary>
        /// �����Զ����
        /// </summary>
        public string FlowTest()
        {
            string msg = "";

            Flows fls = new Flows();
            fls.RetrieveAll();

            int i = 1;
            foreach (Flow fl in fls)
            {
                // StartWork sw = fl.HisStartNode.HisWorks;
                BP.WF.Node nd = new BP.WF.Node();
                if (fl.No.IndexOf("XN") != -1)
                    continue;

                try
                {
                    nd = fl.HisStartNode;
                }
                catch
                {
                    continue;
                }

                string delsql = "DELETE  " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";
                DBAccess.RunSQL(delsql);


                //string sql = "SELECT COUNT(*) FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";
                //if (DBAccess.RunSQLReturnValInt(sql) == 0)
                //    continue;

                //string sqlIn = "SELECT OID FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW )  ";
                //string delsql = "DELETE  " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";


                //BP.WF.Nodes nds = fl.HisNodes;
                //foreach (BP.WF.Node mynd in nds)
                //{
                //    if (mynd.IsStartNode)
                //        continue;
                //    sql = "DELETE " + mynd.PTable + " WHERE OID IN ( " + sqlIn + " )";
                //    DBAccess.RunSQL(sql); // ɾ�������ڵ������.
                //}
                //// ɾ����ʼ������ݡ�
                //DBAccess.RunSQL(delsql);
                //msg += "@������:" + fl.Name + "ɾ����ʽ�޸�" + delsql;
            }

            Log.DefaultLogWriteLineInfo(msg);
            return msg;
        }

        public string FlowTest_bak1214()
        {
            string msg = "";

            Flows fls = new Flows();
            fls.RetrieveAll();

            int i = 1;
            foreach (Flow fl in fls)
            {
                // StartWork sw = fl.HisStartNode.HisWorks;
                BP.WF.Node nd = new BP.WF.Node();
                if (fl.No.IndexOf("XN") != -1)
                    continue;

                try
                {
                    nd = fl.HisStartNode;
                }
                catch
                {
                    continue;
                }

                string sql = "SELECT COUNT(*) FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                    continue;


                string sqlIn = "SELECT OID FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW )  ";
                string delsql = "DELETE  " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";


                BP.WF.Nodes nds = fl.HisNodes;
                foreach (BP.WF.Node mynd in nds)
                {
                    if (mynd.IsStartNode)
                        continue;
                    sql = "DELETE " + mynd.PTable + " WHERE OID IN ( " + sqlIn + " )";
                    DBAccess.RunSQL(sql); // ɾ�������ڵ������.
                }
                // ɾ����ʼ������ݡ�
                DBAccess.RunSQL(delsql);
                msg += "@������:" + fl.Name + "ɾ����ʽ�޸�" + delsql;
            }

            return msg;
        }
    }
}

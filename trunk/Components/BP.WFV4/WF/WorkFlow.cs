using System;
using BP.En;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Sys;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// WF ��ժҪ˵����
    /// ������
    /// �����������������
    /// ��������Ϣ��
    /// ���̵���Ϣ��
    /// </summary>
    public class WorkFlow
    {
        public string ToE(string no, string chName)
        {
            return BP.Sys.Language.GetValByUserLang(no, chName);
        }
        public string ToEP1(string no, string chName, string v)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(no, chName), v);
        }
        public string ToEP2(string no, string chName, string v, string v2)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(no, chName), v, v2);
        }

        #region ��ǰ����ͳ����Ϣ
        /// <summary>
        /// ������Χ�����еĸ�����
        /// </summary>
        public static int NumOfRuning(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=0";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// ���뾯�����޵ĸ���
        /// </summary>
        public static int NumOfAlert(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=1";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// ����
        /// </summary>
        public static int NumOfTimeout(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=2";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        #endregion

        #region  Ȩ�޹���
        /// <summary>
        /// �ǲ����ܹ�����ǰ�Ĺ�����
        /// </summary>
        /// <param name="empId">������ԱID</param>
        /// <returns>�ǲ����ܹ�����ǰ�Ĺ���</returns>
        public bool IsCanDoCurrentWork(string empId)
        {
            //return true;
            // �ҵ���ǰ�Ĺ����ڵ�
            WorkNode wn = this.GetCurrentWorkNode();

            // �ж��ǲ��ǿ�ʼ�����ڵ�..
            if (wn.HisNode.IsStartNode)
            {
                // ���������ж��ǲ��������Ȩ�ޡ�
                return WorkFlow.IsCanDoWorkCheckByEmpStation(wn.HisNode.NodeID, empId);
            }

            // �ж����Ĺ������ɵĹ�����.
            WorkerLists gwls = new WorkerLists(this.WorkID, wn.HisNode.NodeID);
            if (gwls.Count == 0)
            {
                //return true;
                //throw new Exception("@�������̶������,û���ҵ��ܹ�ִ�д��������Ա.�����Ϣ:����ID="+this.WorkID+",�ڵ�ID="+wn.HisNode.NodeID );
                throw new Exception("@" + this.ToE("WF0", "�������̶������,û���ҵ��ܹ�ִ�д��������Ա.�����Ϣ:") + " WorkID=" + this.WorkID + ",NodeID=" + wn.HisNode.NodeID);
            }

            foreach (WorkerList en in gwls)
            {
                if (en.FK_Emp == empId)
                    return true;
            }
            return false;
        }
        #endregion

        #region ���̹�������
        /// <summary>
        /// ������ɾ����������
        /// </summary>
        public void DoDeleteWorkFlowByReal()
        {
            BP.DA.Log.DefaultLogWriteLineInfo("@[" + this.HisFlow.Name + "]���̱�[" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "]ɾ����WorkID[" + this.WorkID + "]��");
            string msg = "";
            try
            {
                Int64 workId = this.WorkID;
                string flowNo = this.HisFlow.No;
            }
            catch (Exception ex)
            {
                throw new Exception("��ȡ���̵� ID �����̱�� ���ִ���" + ex.Message);
            }

            try
            {
                // @,ɾ�����������̿�����Ϣ.
                DBAccess.RunSQL("DELETE WF_CHOfNode WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE WF_CHOfFlow WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");

                //  WF_CHOfStation
                //DBAccess.RunSQL("DELETE WF_CHOfStation WHERE WorkID="+this.WorkID+" AND FK_Flow='"+this.HisFlow.No+"'");
                // @,ɾ��������ͳ�Ʒ�����Ϣ.
                // DBAccess.RunSQL("DELETE WF_CHQuality WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");

                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE WF_Book WHERE WorkID=" + this.WorkID);

                // 2, ɾ�������Ĺ���.
                try
                {
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = this.WorkID;
                    gwf.DeleteHisRefEns();
                    gwf.Delete();
                }
                catch
                {
                }

                Nodes nds = this.HisFlow.HisNodes;
                foreach (Node nd in nds)
                {
                    Work wk = nd.HisWork;
                    if (nd.IsCheckNode)
                    {
                        wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                        wk.SetValByKey("MyPK", nd.NodeID + "_" + this.WorkID);
                    }
                    wk.OID = this.WorkID;
                    wk.Delete();
                }

                // 1, ɾ�����������ص�ÿһ�������ڵ���Ϣ��
                WorkNodes wns = this.HisWorkNodesOfWorkID;
                foreach (WorkNode nd in wns)
                {
                    try
                    {
                        //DBAccess.RunSQL("DELETE "+nd.HisWork.EnMap.PhysicsTable+" WHERE OID="+this.WorkID );
                        nd.HisWork.Delete();
                    }
                    catch
                    {
                        //msg+=ex.Message;
                    }
                }
                if (msg != "")
                {
                    Log.DebugWriteInfo(msg);
                    //throw new Exception("@�Ѿ��ӹ������б����������.ɾ���ڵ���Ϣ�����ִ���:" + msg);
                }
            }
            catch (Exception ex)
            {
                string err = "@" + this.ToE("WF1", "ɾ����������") + "[" + this.HisStartWork.OID + "," + this.HisStartWork.Title + "] Err " + ex.Message;
                Log.DefaultLogWriteLine(LogType.Error, err);
                throw new Exception(err);
            }
        }
        /// <summary>
        /// ɾ����ǰ�Ĺ��������ñ��.
        /// </summary>
        public void DoDeleteWorkFlowByFlag(string msg)
        {
            try
            {
                //�������̵�״̬Ϊǿ����ֹ״̬
                WorkNode nd = GetCurrentWorkNode();
                nd.HisWork.NodeState = NodeState.Stop;
                nd.HisWork.DirectUpdate();
                //���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬
                StartWork sw = this.HisStartWork;
                sw.WFState = BP.WF.WFState.Delete;
                sw.WFLog += "\n@" + Web.WebUser.No + " " + Web.WebUser.Name + "��" + DataType.CurrentDataTime + " �߼�ɾ��,ԭ������:" + msg;
                sw.DirectUpdate();
                //���ò����Ĺ�������Ϊ.
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID, this.HisFlow.No);
                gwf.WFState = 3;
                gwf.Update();
                // ɾ����Ϣ.
                BP.WF.MsgsManager.DeleteByWorkID(sw.OID);
                //WorkerLists wls = new WorkerLists(this
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@�߼�ɾ�����ִ���:" + ex.Message);
                throw new Exception("@�߼�ɾ�����ִ���:" + ex.Message);
            }
        }

        #region ��־����
        /// <summary>
        /// д��������־[�Ѿ������˼�¼ʱ��]
        /// </summary>
        /// <param name="doc"></param>
        public void WritLog(string doc)
        {
            StartWork sw = this.HisStartWork;
            sw.WFLog += "\n@" + BP.DA.DataType.CurrentData + "  " + doc;
            sw.DirectUpdate();
        }
        #endregion ��־����

        #region ���̵�ǿ����ֹ\ɾ�� ���߻ָ�ʹ������,
        /// <summary>
        /// ǿ����ֹ����. 
        ///  1, �������̵�״̬Ϊǿ����ֹ״̬.
        ///  2, ���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬. 
        ///  3, ���ò����Ĺ�������Ϊ ǿ����ֹ״̬ .
        ///  4, ��ȥ��ǰ������Ա����Ϣ.
        /// </summary>
        /// <param name="msg"></param>
        public void DoStopWorkFlow(string msg)
        {
            try
            {
                //�������̵�״̬Ϊǿ����ֹ״̬
                //			//	WorkNode nd = GetCurrentWorkNode();
                //				nd.HisWork.NodeState = 4;
                //				nd.HisWork.Update();

                //���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬
                StartWork sw = this.HisStartWork;
                sw.WFState = BP.WF.WFState.Stop;
                //sw.NodeState=4;
                sw.WFLog += "\n@" + Web.WebUser.No + " " + Web.WebUser.Name + "��" + DateTime.Now.ToString(DataType.SysDataTimeFormat) + " ǿ����ֹ����,ԭ������:" + msg;
                sw.DirectUpdate();

                //���ò����Ĺ�������Ϊ
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID, this.HisFlow.No);
                gwf.WFState = 2;
                gwf.DirectUpdate();
                // ɾ����Ϣ.
                BP.WF.MsgsManager.DeleteByWorkID(sw.OID);
                //WorkerLists wls = new WorkerLists(this
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@ǿ����ֹ���̴���." + ex.Message);
                throw new Exception("@ǿ����ֹ���̴���." + ex.Message);
            }
        }
        public void DoDeleteFlow()
        {
            Work wk = this.HisStartWork;
        }
        public string DoSelfTest()
        {
            string msg = "";
            if (this.IsComplete)
                return "�����Ѿ���������������졣";

            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID, this.HisFlow.No);
            if (gwf.WFState == (int)WFState.Complete)
                return "�����Ѿ���������������졣";


            // �ж��Ƿ��ж���ڵ�״̬�ڵȴ������״̬�Ĵ���
            WorkNodes ens = this.HisWorkNodesOfWorkID;
            int i = 0;

            #region �����жϵ�ǰ�Ĺ����ڵ�� �ڵ������Ƿ���ڡ�
            int num = 0;
            foreach (WorkNode wn in ens)
            {
                Work wk = wn.HisWork;
                Node nd = wn.HisNode;
                if (wk.NodeState != NodeState.Complete)
                {
                    /*���û����� */
                    if (nd.NodeID != gwf.FK_Node)
                    {
                        num++; ;
                    }
                }
            }

            if (num == 0)
            {
                return "û���ҵ���ǰ�Ĺ��������̴���";
            }

            foreach (WorkNode wn in ens)
            {
                Work wk = wn.HisWork;
                Node nd = wn.HisNode;

                if (wk.NodeState != NodeState.Complete)
                {
                    /*���û����� */
                    if (nd.NodeID != gwf.FK_Node)
                    {
                        /*������ǵ�ǰ�Ĺ����ڵ㡣*/
                        wk.NodeState = NodeState.Complete;
                        wk.Update();
                        msg += "���̵�����ɣ�������һ���������ж����ǰ��������������Ϊ�ֹ��ĵ���������ɵġ�";
                    }
                }
            }
            #endregion


            if (msg == "")
                return "@����û�����⣬�û����������ַ�ʽɾ������1���˻ص���һ�����ڡ�2�������ص�ϵͳ����Ա����ɾ����";
            else
                return msg;
        }
        /// <summary>
        /// �ָ�����.
        /// </summary>
        /// <param name="msg">�ظ����̵�ԭ��</param>
        public void DoComeBackWrokFlow(string msg)
        {
            try
            {
                // �������̵�״̬Ϊǿ����ֹ״̬
                //				WorkNode nd = GetCurrentWorkNode();
                //				nd.HisWork.NodeState =0;
                //				nd.HisWork.Update();

                // ���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬
                StartWork sw = this.HisStartWork;
                sw.WFState = 0;
                sw.WFLog += "\n@" + Web.WebUser.No + " " + Web.WebUser.Name + "��" + DateTime.Now.ToString(DataType.SysDataTimeFormat) + " �ظ�ʹ������,ԭ������:" + msg;
                sw.DirectUpdate();

                //���ò����Ĺ�������Ϊ
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID, this.HisFlow.No);
                gwf.WFState = 0;
                gwf.DirectUpdate();

                // ������Ϣ 
                WorkNode wn = this.GetCurrentWorkNode();
                WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
                if (wls.Count == 0)
                    throw new Exception("@�ָ����̳��ִ���,�����Ĺ������б�");
                BP.WF.MsgsManager.AddMsgs(wls, "�ָ�������", wn.HisNode.Name, "�ظ�������");
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@�ָ����̳��ִ���." + ex.Message);
                throw new Exception("@�ָ����̳��ִ���." + ex.Message);
            }
        }
        #endregion


        /// <summary>
        /// �õ���ǰ�Ľ����еĹ�����
        /// </summary>
        /// <returns></returns>		 
        public WorkNode GetCurrentWorkNode()
        {
            //if (this.IsComplete)
            //    throw new Exception("@��������[" + this.HisStartWork.Title + "],�Ѿ���ɡ�");

            int currNodeID = 0;
            string sql = "SELECT FK_Node FROM WF_GenerWorkFlow WHERE WorkID=" + this.WorkID;
            currNodeID = DBAccess.RunSQLReturnValInt(sql);
            if (currNodeID == 0)
            {
                this.DoFlowOver();
                throw new Exception("@" + this.ToEP1("WF2", "��������{0}�Ѿ���ɡ�", this.HisStartWork.Title));
            }

            Node nd = new Node(currNodeID);
            Work work = nd.HisWork;
            work.OID = this.WorkID;
            work.NodeID = nd.NodeID;
            if (nd.IsCheckNode)
            {
                work.SetValByKey("NodeID", nd.NodeID);
            }

            work.SetValByKey("FK_Dept", Web.WebUser.FK_Dept);

            if (work.RetrieveFromDBSources() == 0)
            {
                Log.DefaultLogWriteLineError("@" + this.ToE("WF3", "û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��")); // û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��
                work.Rec = Web.WebUser.No;

                try
                {
                    work.Insert();
                }
                catch
                {
                    Log.DefaultLogWriteLineError("@" + this.ToE("WF3", "û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��") + "��"); // û���ҵ���ǰ�Ĺ����ڵ������
                }
            }

            return new WorkNode(work, nd);
        }
        /// <summary>
        /// ����������̽���
        /// </summary>
        /// <param name="sw"></param>
        public string DoDoFlowOverHeLiu_del()
        {
            GenerFH gh = new GenerFH();
            gh.FID = this.WorkID;
            if (gh.RetrieveFromDBSources() == 0)
                throw new Exception("ϵͳ�쳣");
            else
                gh.Delete();

            GenerWorkFlows ens = new GenerWorkFlows();
            ens.Retrieve(GenerWorkFlowAttr.FID, this.WorkID);

            string msg = "";
            foreach (GenerWorkFlow en in ens)
            {
                if (en.WorkID == en.FID)
                    continue;

                /*����ÿһ��������*/
                WorkFlow fl = new WorkFlow(en.FK_Flow, en.WorkID, en.FID);
                // msg += fl.DoFlowOverOrdinary();
            }
            return msg;
        }
        /// <summary>
        /// ���������Ľڵ�
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string DoDoFlowOverFeiLiu(GenerWorkFlow gwf)
        {
            // ��ѯ��������û����ɵ����̡�
            int i = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_GenerWorkflow WHERE FID=" + gwf.FID + " AND WFState!=1");
            switch (i)
            {
                case 0:
                    throw new Exception("@��Ӧ�õĴ���");
                case 1:
                    BP.DA.DBAccess.RunSQL("DELETE WF_GenerWorkflow  WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + gwf.FID);

                    StartWork wk = this.HisFlow.HisStartNode.HisWork as StartWork;
                    wk.OID = gwf.FID;
                    wk.WFState = WFState.Complete;
                    wk.NodeState = NodeState.Complete;
                    wk.Update();

                    return "@��ǰ�Ĺ����Ѿ���ɣ������������еĹ������Ѿ���ɡ�";
                default:
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkflow SET WFState=1 WHERE WorkID=" + this.WorkID);
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=1 WHERE WorkID=" + this.WorkID);
                    return "@��ǰ�Ĺ����Ѿ���ɡ�";
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string DoFlowOver()
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            Node nd = new Node(gwf.FK_Node);

            string msg = this.BeforeFlowOver();

            switch (nd.HisFNType)
            {
                case FNType.Plane:
                    msg += this.DoFlowOverPlane(nd);
                    break;
                case FNType.River:
                    msg += this.DoFlowOverRiver(nd);
                    break;
                case FNType.Branch:
                    msg += this.DoFlowOverBranch(nd);
                    break;
                default:
                    throw new Exception("@û���жϵ������");
                    break;
            }

            return msg;

            //switch (nd.HisNodeWorkType)
            //{
            //    case NodeWorkType.WorkHL: // ����
            //        return this.DoDoFlowOverHeLiu();
            //    case NodeWorkType.WorkFL: // ����
            //        return this.DoDoFlowOverFeiLiu(gwf);
            //    default:
            //}
            //switch (nd.HisNodeWorkType)
            //{
            //    case NodeWorkType.WorkHL:
            //    default:
            //        return DoFlowOverOrdinary();
            //}
        }
        /// <summary>
        /// �ڷ����Ͻ������̡�
        /// </summary>
        /// <returns></returns>
        public string DoFlowOverBranch(Node nd)
        {
            string sql = "";
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);

            if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
            {
                /* ��ʼ�ڵ��Ǹ��� */
            }
            else
            {
                BP.DA.DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE OID=" + this.WorkID); // ���¿�ʼ�ڵ��״̬��
            }

            string msg = "";
            // �ж��������Ƿ�û��û����ɵ�֧����
            sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;

            DataTable dt = DBAccess.RunSQLReturnTable("SELECT Rec FROM ND" + nd.NodeID + " WHERE FID=" + this.FID);
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /* ���ȫ����� */
                if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
                {
                    BP.DA.DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE FID=" + this.FID);
                }

                /*�������̶�������*/
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);

                /* �������������ɵ���Ϣ������ǰ���û���*/
                msg += "@����������ȫ��������{" + dt.Rows.Count + "}����Ա�����˷�֧���̣��������һ����ɴ˹�������Ա��@��֧���̲������������£�";
                foreach (DataRow dr in dt.Rows)
                {
                    msg += dr[0].ToString() + "��";
                }
                return msg;
                //   return "@����������ȫ������" + this.GenerFHStartWorkInfo();
            }
            else
            {
                /* ����������Աû����ɴ˹�����*/

                msg += "@���Ĺ����Ѿ��ꡣ@��������Ŀǰ��û����ȫ��������{" + dt.Rows.Count + "}����Ա�����˷�֧���̣��������£�";
                foreach (DataRow dr in dt.Rows)
                {
                    msg += dr[0].ToString() + "��";
                }
                return msg;
            }
        }
        /// <summary>
        /// �ڷ����Ͻ������̡�
        /// </summary>
        /// <returns></returns>
        public string DoFlowOverBranch_Bak(Node nd)
        {
            string sql = "";
            if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
            {
                /* �����ʼ�ڵ��Ǹ����������ڵ���֧����*/
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);

                // �ж��Ƿ���û�н�����֧����
                sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    StartWork sw = this.HisStartWorkNode.HisWork as StartWork;
                    sw.FID = this.FID;
                    sw.OID = this.FID;
                    int i = sw.RetrieveFromDBSources();
                    if (i == 0)
                    {
                        throw new Exception("@��ʼ�ڵ���Ϣ��ʧ��");
                    }
                    else
                    {
                        sw.Update("WFState", (int)WFState.Complete);
                    }

                    /*�������̶�������*/
                    DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                    DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);
                }
                return "@�����ڸ����Ͻ�����";
            }

            /*��ʼ�ڵ���֧���������ڵ���֧����*/
            StartWork mysw = this.HisStartWorkNode.HisWork as StartWork;
            mysw.OID = this.WorkID;
            mysw.Update("WFState", (int)WFState.Complete);

            //i = sw.RetrieveFromDBSources();
            //if (i == 0)
            //{
            //    throw new Exception("@��ʼ�ڵ���Ϣ��ʧ��");
            //}
            //else
            //{
            //    sw.Update("WFState", (int)WFState.Complete);
            //}

            // ��������״̬��
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);
            //   BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET WFState=0 WHERE WorkID=" + this.WorkID);

            // �ж��Ƿ���û�н�����֧����
            sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /*�������̶�������*/
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);
            }

            return "@�����ڷ����Ͻ�����";

        }
        /// <summary>
        /// �ڸ����Ͻ�������
        /// </summary>
        /// <param name="nd">�����Ľڵ�</param>
        /// <returns>���ص���Ϣ</returns>
        public string DoFlowOverRiver(Node nd)
        {
            try
            {
                string msg = "";



                /* ���¿�ʼ�ڵ��״̬��*/
                DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE FID=" + this.FID);

                /*�������̶�������*/
                DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);

                return msg;
            }
            catch (Exception ex)
            {
                throw new Exception("@��������ʱ������쳣��" + ex.Message);
            }
        }
        public string GenerFHStartWorkInfo()
        {
            string msg = "";
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT Title,RDT,Rec,OID FROM ND" + this.StartNodeID + " WHERE FID=" + this.FID);
            switch (dt.Rows.Count)
            {
                case 0:
                    Node nd = new Node(this.StartNodeID);
                    throw new Exception("@û���ҵ����ǿ�ʼ�ڵ�����ݣ������쳣��FID=" + this.FID + "���ڵ㣺" + nd.Name + "�ڵ�ID��" + nd.NodeID);
                case 1:
                    msg = string.Format("@�����ˣ� {0}  ���ڣ�{1} ��������� ���⣺{2} ���Ѿ��ɹ���ɡ�",
                        dt.Rows[0]["Rec"].ToString(), dt.Rows[0]["RDT"].ToString(), dt.Rows[0]["Title"].ToString());
                    break;
                default:
                    msg = "@����(" + dt.Rows.Count + ")λ��Ա����������Ѿ���ɡ�";
                    foreach (DataRow dr in dt.Rows)
                    {
                        msg += "<br>�����ˣ�" + dr["Rec"] + " �������ڣ�" + dr["RDT"] + " ���⣺" + dr["Title"] + "<a href='./../../WF/WFRpt.aspx?WorkID=" + dr["OID"] + "&FK_Flow=" + this.HisFlow.No + "' target=_blank>��ϸ...</a>";
                    }
                    break;
            }
            return msg;
        }
        public int StartNodeID
        {
            get
            {
                return int.Parse(this.HisFlow.No + "01");
            }
        }
        /// <summary>
        /// ���������̽���
        /// </summary>		 
        public string DoFlowOverPlane(Node nd)
        {
            StartWork sw = this.HisStartWorkNode.HisWork as StartWork;
            sw.OID = this.WorkID;
            sw.Update("WFState", (int)sw.WFState);

            this._IsComplete = 1;

            // ������̡�
            DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE WorkID=" + this.HisStartWork.OID + " AND FK_Flow='" + this.HisFlow.No + "'");

            // ��������Ĺ����ߡ�
            DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.HisStartWork.OID + " AND FK_Node IN (SELECT NodeId FROM WF_Node WHERE FK_Flow='" + this.HisFlow.No + "') ");
            return "";

            //// �޸����̻����е�����״̬��
            //CHOfFlow chf = new CHOfFlow();
            //chf.WorkID = this.WorkID;
            //chf.Update("WFState", (int)sw.WFState);
            // +"@" + this.ToEP2("WF5", "��������{0},{1}������ɡ�", this.HisFlow.Name, this.HisStartWork.Title);  // ��������[" + HisFlow.Name + "] [" + HisStartWork.Title + "]������ɡ�;
        }
        /// <summary>
        /// �������֮��Ҫ���Ĺ�����
        /// </summary>
        private string BeforeFlowOver()
        {
            string ccmsg = "";
            if (this.HisFlow.IsCCAll == true)
            {
                ccmsg += "@�������̲�����Ա��";
                /*��������̽������Զ��ķ��͸�ȫ�������Ա*/
                ccmsg += this.CCTo(BP.DA.DBAccess.RunSQLReturnTable("SELECT DISTINCT FK_Emp FROM WF_GenerWorkerList WHERE WorkID=" + this.WorkID + " AND IsEnable=1"));
            }

            #region ִ�����̳���
            if (this.HisFlow.CCStas.Length > 2)
            {
                ccmsg += "@���ո�λ����������Ա��";
                /* ��������˳�����Ա�ĸ�λ��*/
                string sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 3)
                {
                    sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 2) + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                    dt = DBAccess.RunSQLReturnTable(sql);
                }

                if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 5)
                {
                    sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 4) + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                    dt = DBAccess.RunSQLReturnTable(sql);
                }

                if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 7)
                {
                    sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 6) + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                    dt = DBAccess.RunSQLReturnTable(sql);
                }

                if (dt.Rows.Count == 0)
                {
                    ccmsg += "@ϵͳû�л�ȡ��Ҫ���͵���Ա������Ա���õ���Ϣ���£�" + this.HisFlow.CCStas + "����ȷ���ø�λ���Ƿ��д���Ա��";
                }
                ccmsg += this.CCTo(dt);
            }
            #endregion
            return ccmsg;
        }
        /// <summary>
        ///  ���͵�
        /// </summary>
        /// <param name="dt"></param>
        public string CCTo(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return "";


            string emps = "";
            string empsExt = "";

            string ip = "127.0.0.1";
            System.Net.IPAddress[] addressList = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList;
            if (addressList.Length > 1)
                ip = addressList[1].ToString();
            else
                ip = addressList[0].ToString();


            foreach (DataRow dr in dt.Rows)
            {
                string no = dr[0].ToString();
                emps += no + ",";

                if (Glo.IsShowUserNoOnly)
                    empsExt += no + "��";
                else
                    empsExt += no + "<" + dr[1] + ">��";
            }

            Paras pss = new Paras();
            pss.Add("Sender", Web.WebUser.No);
            pss.Add("Receivers", emps);
            pss.Add("Title", "���������ͣ���������:" + this.HisFlow.Name + "��������ˣ�" + Web.WebUser.Name);
            pss.Add("Context", "�������� http://" + ip + "/Front/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=0");

            try
            {
                DBAccess.RunSP("CCstaff", pss);
                return "@" + empsExt;
            }
            catch (Exception ex)
            {
                return "@���ͳ��ִ���û�аѸ����̵���Ϣ���͵�(" + empsExt + ")����ϵ����Ա���ϵͳ�쳣" + ex.Message;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ���Ľڵ�
        /// </summary>
        private Nodes _HisNodes = null;
        /// <summary>
        /// �ڵ�s
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                if (this._HisNodes == null)
                    this._HisNodes = this.HisFlow.HisNodes;
                return this._HisNodes;
            }
        }
        /// <summary>
        /// �����ڵ�s(��ͨ�Ĺ����ڵ�)
        /// </summary>
        private WorkNodes _HisWorkNodesOfWorkID = null;
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        public WorkNodes HisWorkNodesOfWorkID
        {
            get
            {
                if (this._HisWorkNodesOfWorkID == null)
                {
                    this._HisWorkNodesOfWorkID = new WorkNodes();
                    this._HisWorkNodesOfWorkID.GenerByWorkID(this.HisFlow, this.WorkID);
                }
                return this._HisWorkNodesOfWorkID;
            }
        }
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        private WorkNodes _HisWorkNodesOfFID = null;
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        public WorkNodes HisWorkNodesOfFID
        {
            get
            {
                if (this._HisWorkNodesOfFID == null)
                {
                    this._HisWorkNodesOfFID = new WorkNodes();
                    this._HisWorkNodesOfFID.GenerByFID(this.HisFlow, this.FID);
                }
                return this._HisWorkNodesOfFID;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        private Flow _HisFlow = null;
        /// <summary>
        /// ��������
        /// </summary>
        public Flow HisFlow
        {
            get
            {
                return this._HisFlow;
            }
        }
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                return new GenerWorkFlow(this.WorkID, this.HisFlow.No);
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        private Int64 _WorkID = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this._WorkID;
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        private Int64 _FID = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this._FID;
            }
        }
        #endregion

        #region ���췽��
        public WorkFlow(string fk_flow, Int64 wkid)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(wkid);
            this._FID = gwf.FID;

            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            Flow flow = new Flow(fk_flow);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }

        public WorkFlow(Flow flow, Int64 wkid)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(wkid);
            this._FID = gwf.FID;

            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        /// <summary>
        /// ����һ������������
        /// </summary>
        /// <param name="flow">����No</param>
        /// <param name="wkid">����ID</param>
        public WorkFlow(Flow flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        public WorkFlow(string FK_flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;

            Flow flow = new Flow(FK_flow);
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        #endregion

        #region ��������

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private StartWork _HisStartWork = null;
        /// <summary>
        /// ����ʼ�Ĺ���.
        /// </summary>
        public StartWork HisStartWork
        {
            get
            {
                if (_HisStartWork == null)
                {
                    StartWork en = (StartWork)this.HisFlow.HisStartNode.HisWork;
                    en.OID = this.WorkID;
                    en.FID = this.FID;
                    if (en.RetrieveFromDBSources() == 0)
                        en.RetrieveFID();
                    _HisStartWork = en;
                }
                return _HisStartWork;
            }
        }
        /// <summary>
        /// ��ʼ�����ڵ�
        /// </summary>
        private WorkNode _HisStartWorkNode = null;
        /// <summary>
        /// ����ʼ�Ĺ���.
        /// </summary>
        public WorkNode HisStartWorkNode
        {
            get
            {
                if (_HisStartWorkNode == null)
                {
                    Node nd = this.HisFlow.HisStartNode;
                    StartWork en = (StartWork)nd.HisWork;
                    en.OID = this.WorkID;
                    en.Retrieve();

                    WorkNode wn = new WorkNode(en, nd);
                    _HisStartWorkNode = wn;

                }
                return _HisStartWorkNode;
            }
        }
        #endregion

        #region ��������
        public int _IsComplete = -1;
        /// <summary>
        /// �ǲ������
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (_IsComplete == -1)
                {
                    bool s = !DBAccess.IsExits("select workid from WF_GenerWorkFlow where WorkID=" + this.WorkID + " and FK_Flow='" + this.HisFlow.No + "'");
                    if (s)
                        _IsComplete = 1;
                    else
                        _IsComplete = 0;
                }

                if (_IsComplete == 0)
                    return false;

                return true;
            }
        }
        /// <summary>
        /// �ǲ������
        /// </summary>
        public string IsCompleteStr
        {
            get
            {
                if (this.IsComplete)
                    return this.ToE("Already", "��");
                else
                    return this.ToE("Not", "δ");

            }
        }
        #endregion

        #region ��̬����

        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="empId">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpStation(int nodeId, string empId)
        {
            bool isCan = false;
            // �жϸ�λ��Ӧ��ϵ�ǲ����ܹ�ִ��.
            string sql = "SELECT a.FK_Node FROM WF_NodeStation a,  Port_EmpStation b WHERE (a.FK_Station=b.FK_Station) AND (a.FK_Node=" + nodeId + " AND b.FK_Emp='" + empId + "' )";
            isCan = DBAccess.IsExits(sql);
            if (isCan)
                return true;
            // �ж�������Ҫ������λ�ܲ���ִ����.
            sql = "select fk_Node from WF_NodeStation where FK_Node=" + nodeId + " and ( FK_Station in (select FK_Station from port_empstation where fk_emp='" + empId + "') ) ";
            return DBAccess.IsExits(sql);
        }
        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="dutyNo">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpDuty(int nodeId, string dutyNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDuty  a,  Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty) AND (a.FK_Node=" + nodeId + " AND b.FK_Duty=" + dutyNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="DeptNo">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpDept(int nodeId, string DeptNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDept  a,  Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND (a.FK_Node=" + nodeId + " AND b.FK_Dept=" + DeptNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���������ܹ������������Ա��
        /// </summary>
        /// <param name="nodeId">�ڵ�ID</param>		 
        /// <returns></returns>
        public static DataTable CanDoWorkEmps(int nodeId)
        {
            string sql = "select a.FK_Node, b.EmpID from wf_Nodestation  a,  Port_EmpStation b where (a.FK_Station=b.FK_Station) and (a.FK_Node=" + nodeId + " )";
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// GetEmpsBy
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Emps GetEmpsBy(DataTable dt)
        {
            // �γ��ܹ��������������û����Ρ�
            Emps emps = new Emps();
            foreach (DataRow dr in dt.Rows)
            {
                emps.AddEntity(new Emp(dr["EmpID"].ToString()));
            }
            return emps;
        }

        #endregion

        #region ���̷���
        /// <summary>
        /// ִ�г���
        /// </summary>
        public string UnSend(string fk_emp)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID, this.HisFlow.No);
            Node nd = new Node(gwf.FK_Node);
            if (nd.IsStartNode)
                return "�����ܳ������ͣ���Ϊ���ǿ�ʼ�ڵ㡣<a href='../../WF/MyFlowInfo.aspx?DoType=DeleteFlow&WorkID=" + this.WorkID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>��������</a>";

            WorkNode wn = this.GetCurrentWorkNode();
            WorkNode wnPri = wn.GetPreviousWorkNode();

            WorkerList wl = new WorkerList();
            int num = wl.Retrieve(WorkerListAttr.FK_Emp, Web.WebUser.No,
                WorkerListAttr.FK_Node, wnPri.HisNode.NodeID);

            if (num == 0)
                return "@" + this.ToE("WF6", "������ִ�г������ͣ���Ϊ��ǰ�������������͵ġ�");

            // if (wl.RDT.IndexOf(DataType.CurrentData) == -1)
            //   return "@������ִ�г������ͣ���Ϊ��ǰ�������������췢�͵ģ���������"+wl.RDT+"��" ;

            WorkerLists wls = new WorkerLists();
            wls.Delete(WorkerListAttr.WorkID, this.WorkID,
                WorkerListAttr.FK_Node, gwf.FK_Node.ToString());
            wn.HisWork.Delete();
            CHOfNodes chs = new CHOfNodes();
            chs.Delete(CHOfNodeAttr.FK_Node, wn.HisNode.NodeID,
                CHOfNodeAttr.WorkID, wn.WorkID.ToString());

            gwf.FK_Node = wl.FK_Node;
            gwf.Update();

            ForwardWorks fws = new ForwardWorks();
            fws.Delete(ForwardWorkAttr.NodeId, wn.HisNode.NodeID.ToString(), ForwardWorkAttr.WorkID, this.WorkID.ToString());

            ReturnWorks rws = new ReturnWorks();
            rws.Delete(ReturnWorkAttr.NodeId, wn.HisNode.NodeID.ToString(), ReturnWorkAttr.WorkID, this.WorkID.ToString());


            //if (wnPri.HisNode.IsStartNode)
            //{
            //    Reback rb = new Reback();
            //    rb.NodeId = wnPri.HisNode.NodeID;
            //    rb.WorkID = this.WorkID;
            //    rb.FK_Emp = Web.WebUser.No;
            //    try
            //    {
            //        rb.Insert();
            //    }
            //    catch
            //    { 
            //    }
            //}
            if (wnPri.HisNode.IsStartNode)
            {
                //����{0}���������ɹ��������Ե�����
                // return "@����ִ�гɹ��������Ե�����" + this.ToE("WF7", wn.HisNode.EnDesc) + "<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����") + "</a>��";

                if (this.HisFlow.FK_FlowSort != "00")
                    return "@����ִ�гɹ��������Ե�����<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='../../WF/MyFlowInfo.aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��";
                else
                    return "@����ִ�гɹ��������Ե�����<a href='../../GovDoc/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��";
            }
            else
            {
                // �����Ƿ���ʾ��
                DBAccess.RunSQL("UPDATE wf_forwardwork SET IsTakeBack=1 WHERE WORKID=" + this.WorkID + " AND NODEID=" + wnPri.HisNode.NodeID);
                //  return "@" + this.ToE("WF7", wn.HisNode.EnDesc) + "<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "ִ�й���") + "</A> ��";
                if (this.HisFlow.FK_FlowSort != "00")
                    return "@����ִ�гɹ��������Ե�����<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "ִ�й���") + "</A>��";
                else
                    return "@����ִ�гɹ��������Ե�����<a href='../../GovDoc/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "ִ�й���") + "</A>��";
            }
        }
        #endregion

    }
    /// <summary>
    /// �������̼���.
    /// </summary>
    public class WorkFlows : CollectionBase
    {
        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="flow">���̱��</param>
        public WorkFlows(Flow flow)
        {
            StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            ens.RetrieveAll(10000);
            foreach (StartWork sw in ens)
            {
                this.Add(new WorkFlow(flow, sw.OID, sw.FID));
            }
        }
        /// <summary>
        /// �������̼���
        /// </summary>
        public WorkFlows()
        {
        }
        /// <summary>
        /// �������̼���
        /// </summary>
        /// <param name="flow">����</param>
        /// <param name="flowState">����ID</param> 
        public WorkFlows(Flow flow, int flowState)
        {
            StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(StartWorkAttr.WFState, flowState);
            qo.DoQuery();
            foreach (StartWork sw in ens)
            {
                this.Add(new WorkFlow(flow, sw.OID, sw.FID));
            }
        }

        #endregion

        #region ��ѯ����
        /// <summary>
        /// GetNotCompleteNode
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <returns>StartWorks</returns>
        public static StartWorks GetNotCompleteWork(string flowNo)
        {
            Flow flow = new Flow(flowNo);
            StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(StartWorkAttr.WFState, "!=", 1);
            qo.DoQuery();
            return ens;

            /*
            foreach(StartWork sw in ens)
            {
                ens.AddEntity( new WorkFlow( flow, sw.OID) ) ; 
            }
            */
        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ����������
        /// </summary>
        /// <param name="wn">��������</param>
        public void Add(WorkFlow wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public WorkFlow this[int index]
        {
            get
            {
                return (WorkFlow)this.InnerList[index];
            }
        }
        #endregion

        #region ���ڵ��ȵ��Զ�����
        /// <summary>
        /// ������ڵ㡣
        /// ���ڵ�Ĳ����������û��Ƿ��Ĳ���������ϵͳ���ִ洢���ϣ���ɵ������еĵ�ǰ�����ڵ�û�й�����Ա���Ӷ�����������������ȥ��
        /// ������ڵ㣬���ǰ����Ƿŵ����ڵ㹤���������档
        /// </summary>
        /// <returns></returns>
        public static string ClearBadWorkNode()
        {
            string infoMsg = "������ڵ����Ϣ��";
            string errMsg = "������ڵ�Ĵ�����Ϣ��";
            return infoMsg + errMsg;
        }
        #endregion
    }
}

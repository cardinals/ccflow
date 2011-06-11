using System;
using System.Data;
using BP.DA ; 
using System.Collections;
using BP.En.Base;
using BP.WF;
using BP.Port ; 
using BP.En;
using BP.DTS;

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
			this.HisDoType=DoType.UnName;
			this.Title="�ڵ�ʱЧ����(���ܽڵ㹤����Ϣ����Ԥ�ڵĹ���׷�������ˣ������㿼�˷���.)";
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		/// <summary>
		/// ����ʱЧ����
		/// </summary>
        public override void Do()
        {
            WFDTS.InitCHOfNode();

            WFDTS.InitOne2More();
        }
	}
	/// <summary>
	/// ���̵���
	/// </summary>
	public class WFDTS
	{
        /// <summary>
        /// Ϊ�˴�������Ԥ�ڹ������ε����⡣
        /// </summary>
        /// <returns></returns>
        public static string InitOne2More()
        {
            string sql = "";

            #region /* ��ѯ����û���굫�����ڵ���� */
            sql = "SELECT WorkID, FK_Node,EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE NodeState=0 AND SUBSTR(SDT,0,11) < '" + DataType.CurrentData + "' GROUP BY WORKID,FK_NODE,EMPS";
            DataTable dt_no = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_no in dt_no.Rows)
            {
                int num = int.Parse(dr_no["Num"].ToString());
                if (num > 1)
                {
                    /*˵�����������Ѽӹ����ˡ�*/
                    continue;
                }
                string myemps = dr_no["Emps"].ToString();
                string WorkID = dr_no["WorkID"].ToString();
                int fk_node = int.Parse(dr_no["FK_Node"].ToString());

                // �ҵ����е�һ��Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    throw new Exception("�����ܳ��ֵ������");

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
                    mycn.NodeState = 0;
                    mycn.FK_Emp = emp;
                    mycn.CDT = "��";
                    mycn.CentOfAdd = 0;
                    mycn.CentOfCut = nd.DeductCent;
                    mycn.Cent = 0;
                    mycn.Save();
                }
                /*ִ�п۷�*/
                DBAccess.RunSQL("UPDATE WF_CHofNode SET CentOfCut=" + nd.DeductCent + " WHERE FK_Node=" + nd.NodeID + " and workid='" + WorkID + "'");
            }
            #endregion

            #region /* ��ѯ���Ѿ����굫�����ڵ���� */
            sql = "SELECT WorkID, FK_Node,EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE NodeState=1 AND SUBSTR(SDT,0,11) < '" + DataType.CurrentData + "' GROUP BY WORKID,FK_NODE,EMPS";
            DataTable dt_yes = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_yes in dt_yes.Rows)
            {
                int num = int.Parse(dr_yes["Num"].ToString());
                if (num > 1)
                {
                    /*˵�����������Ѽӹ����ˡ�*/
                    continue;
                }
                string myemps = dr_yes["Emps"].ToString();
                string WorkID = dr_yes["WorkID"].ToString();
                int fk_node = int.Parse(dr_yes["FK_Node"].ToString());

                // �ҵ����е�һ��Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    throw new Exception("�����ܳ��ֵ������");

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
		/// ����ʱЧ����(�ξ���Ϊ�˴�������Ч�ʵ��µ����⡣�����������й����е�����ͨ���˵������е����)
		/// </summary>
		/// <returns></returns>
        public static string InitCHOfNode()
        {
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + "��ʼ���ȿ�����Ϣ:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string infoMsg = "", errMsg = "";

            //DBAccess.RunSQL("DELETE WF_CHOfStation");  //clear data.
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            // ����һ��������
            //CHOfNode ch = new CHOfNode();

            string fromdatetime = DateTime.Now.Year + "-01-01";
            fromdatetime = "2004-01-01 00:00";
            //string fromdatetime=DateTime.Now.Year+"-01-01 00:00";
            //string fromdatetime=DateTime.Now.Year+"-01-01 00:00";
            string insertSql = "";
            foreach (Node nd in nds)
            {
                if (nd.IsPCNode)  /* ����Ǽ�����ڵ�.*/
                    continue;

                if (nd.IsCheckNode)
                    continue;

                insertSql = "INSERT INTO WF_CHOfNode ( FK_Node, WorkID, NodeState,  Recorder, Emps,RDT, CDT  )"
                    + " "
                    + "  SELECT " + nd.NodeID + " as FK_Node, OID as WorkID, NodeState, Recorder,Emps,RDT,CDT "
                    + "  FROM " + nd.HisWork.EnMap.PhysicsTable
                    + "  WHERE  OID NOT IN ( SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + " )";
                try
                {
                    DBAccess.RunSQL(insertSql);
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineInfo(insertSql + " " + ex.Message);
                }
            }
            // ���������Ϣ��
            //Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + "���ȿ�����ϢEnd"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) ;
            DBAccess.RunSP("WF_UpdateCHOfNode");
            return infoMsg;
        }
		/// <summary>
		/// ����ͳ�Ʒ���
		/// </summary>
		/// <param name="fromDatetime"></param>
		/// <returns></returns>
		public static string InitFlows(string fromDatetime)
		{
			Log.DefaultLogWriteLine(LogType.Info,Web.WebUser.Name+" ################# Start ִ��ͳ�� #####################"); 
			//ɾ�����Ŵ��������
			//DBAccess.RunSQL("DELETE WF_BadWF WHERE BadFlag='FlowDeptBad'");
			fromDatetime="2004-01-01 00:00";

			Flows fls= new Flows();
			fls.RetrieveAll();

			CHOfFlow   fs = new CHOfFlow();
			foreach(Flow fl in fls)
			{
				Node nd =fl.HisStartNode;
				try
				{
					string sql="INSERT INTO WF_CHOfFlow SELECT OID WorkID, "+fl.No+" as FK_Flow, WFState, ltrim(rtrim(Title)) as Title,ltrim(rtrim(WFLog)) as WFLog, Recorder as FK_Emp,"
						+" RDT, CDT, 0 as SpanDays,'' FK_Dept,"
						+"'' as FK_ZSJG,'' AS FK_NY,'' as FK_AP,'' AS FK_ND, '' AS FK_YF, Recorder ,'' as FK_XJ, '' as FK_Station   "
						+" FROM "+nd.HisWork.EnMap.PhysicsTable+" WHERE RDT>='"+fromDatetime+"' AND OID NOT IN ( SELECT WorkID FROM WF_CHOfFlow  )";
					DBAccess.RunSQL(sql);
				}
				catch(Exception ex)
				{
					throw new Exception(fl.Name+ "   "+nd.Name+""+ex.Message );
				}
			}
			DBAccess.RunSP("WF_UpdateCHOfFlow");
			Log.DefaultLogWriteLine(LogType.Info,Web.WebUser.Name+" End ִ��ͳ�Ƶ���"); 
			return "";
		}
		  

		#region ����	 
		/// <summary>
		/// �������棺
		/// 1�����ԭʼ����ɾ���ˣ������������б���Ҫɾ���������߼�����Ҫɾ����		 
		/// 2�����ԭʼ���￪ʼ�ڵ�����û����ɵĹ�����
		/// �����ڲ�����������û�����ǡ��Ͱ�ԭʼ��¼ɾ����
		/// </summary>
		public static void ClearInvalidWF()
		{
			Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name+"==============��ʼ���������ڵ���Ϣ��");
			

			Flows fls = new Flows();
			fls.RetrieveAll();
			string sql="";
			string startNodeTable="";	
			int i=0;

			#region ����ԭ�����������ʱ�䲻��ȷ���Ĵ���.��Щ����ȷ���Ĵ������,�Ƿ����ݿ������ɵĳɵ�.

			// �������Ա��ɾ���ˣ� ɾ������ǰ�Ĺ�����


			// DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE Recorder=1 OR Recorder NOT IN (SELECT EmpID FROM Pub_Emp) "); �˲�������ִ�С�
			// ���ܳ��ֵ�����ǣ������ǰ�Ĺ�����3���˿�������������������ģ���ǰ��Ա�б�ɾ�����ˣ���ô������������Ҳ���ܴ�����������ˡ�

			DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FK_Emp='admin'"); // ϵͳ����Ա���ܷ������̡�
			DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE Recorder='admin'"); // ϵͳ����Ա���ܷ������̡�

			DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE TO_CHAR( FK_EMP )  LIKE '%8888'"); // ϵͳ����Ա���ܷ������̡�
			DBAccess.RunSQL("DELETE WF_GenerWorkFlow   WHERE TO_CHAR( RECORDER )  LIKE '%8888'"); // ϵͳ����Ա���ܷ������̡�
     


			//ɾ���Ѿ�ɾ���û����������.
			DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FK_Emp NOT IN (SELECT EmpID FROM Pub_Emp) "); 
			

			sql="DELETE WF_GenerWorkFlow WHERE   WorkId NOT IN (SELECT WorkId FROM WF_GenerWorkerList)";
			i=DBAccess.RunSQL(sql);
			Log.DefaultLogWriteLine(LogType.Info, "��������������ڵķǷ�workId��ɾ��"+i+"��sql="+sql);

			sql="DELETE WF_GenerWorkerList WHERE   WorkId NOT IN (SELECT WorkId FROM WF_GenerWorkFlow)";
			i=DBAccess.RunSQL(sql);
			Log.DefaultLogWriteLine(LogType.Info, "�������б�������ڵķǷ�workId��ɾ��"+i+"��sql="+sql);
			#endregion


			#region ������ԭ����,���������̱仯,��ɵ�ǰ���̵Ĵ���.
			//sql="DELETE WF_GenerWorkerList WHERE   FK_Node NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "����������ɵĽڵ�(��������)��ɾ��:"+i+"��sql="+sql);

			//sql="DELETE WF_GenerWorkFlow WHERE   FK_CurrentNode NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "����������ɵĽڵ�(�������б�):��ɾ��"+i+"��sql="+sql);


			//sql="DELETE WF_NumCheck WHERE   NodeId NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "������ڵ�:��ɾ��"+i+"��sql="+sql);

			//sql="DELETE WF_StandardCheck WHERE   NodeId NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "������ڵ�:��ɾ��"+i+"��sql="+sql);
			#endregion end 
			 
			foreach(Flow fl in fls)
			{
				Log.DefaultLogWriteLine(LogType.Info, " ---: Ҫ��������������:"+fl.Name);

				/* each flow. */
				Nodes nds = fl.HisNodes;
				startNodeTable = fl.HisStartNode.HisWork.EnMap.PhysicsTable;

				#region ���ڿ�ʼ�ڵ���� �����������̱�֮��Ĺ�ϵ��
				// ���ԭʼ����ɾ���ˣ��������������б���Ҫɾ��  (��ʱ����ʹ��,��Ϊ��������ʼ����,��ͬ��һ�������.) 
				
				sql="DELETE   WF_GenerWorkFlow WHERE  FK_Flow='"+fl.No+"' AND WorkId NOT IN (SELECT OID FROM "+startNodeTable+")";
				i = DBAccess.RunSQL(sql);
				Log.DefaultLogWriteLine(LogType.Info, "����������:"+i+ "��,sql="+sql);
				 

				// �����ʼ�����ڵ�����棬û����ɵĽڵ㣬�����ڲ����Ĺ����������治���ڡ���Ҫɾ�����ǡ�
				sql="DELETE "+startNodeTable+" WHERE WFState!=1 AND OID NOT IN (SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Flow='"+fl.No+"')";
				i=DBAccess.RunSQL(sql);
				Log.DefaultLogWriteLine(LogType.Info, "��������ʼ�ڵ�["+fl.Name+"]:"+i+ "��,sql="+sql);
				#endregion ���ڿ�ʼ�ڵ���� �����������̱�֮��Ĺ�ϵ��

				// ɾ��ÿһ����������뿪ʼ�����ڵ�ID��Ӧ���ϵļ�¼��
				foreach(Node nd in nds)
				{
					if (nd.IsStartNode)
						continue;
					if (nd.IsCheckNode)
					{
						/* �������˽ڵ�*/
						sql="DELETE "+nd.HisWork.EnMap.PhysicsTable+" WHERE NodeId="+nd.NodeID+" and OID  NOT IN ( SELECT OID from "+startNodeTable+" )";
						i =DBAccess.RunSQL(sql);
						Log.DefaultLogWriteLine(LogType.Info, "����ͨ�ڵ�:"+i+ "������["+nd.Name+"],sql="+sql);
						continue;
					}
					
					// ȥ��,��Ϊ�п��ܶ���ڵ�ͬ��һ����.
					sql="DELETE "+nd.HisWork.EnMap.PhysicsTable+" WHERE  OID  NOT IN ( SELECT OID from "+startNodeTable+" )";
					i =DBAccess.RunSQL(sql);
					Log.DefaultLogWriteLine(LogType.Info, "����ͨ�ڵ�:"+i+ "������["+nd.Name+"],sql="+sql);

				}
				// �������б���������̱�֮��Ĺ�ϵ��
				//sql="DELETE WF_GenerWorkerList where WorkId in  ( select WorkId from WF_GenerWorkerList where WorkId not in (select WorkId from WF_GenerWorkFlow) )";
				//  i = DBAccess.RunSQL(sql) ;
				//Log.DefaultLogWriteLine(LogType.Info, "����ͨ�ڵ�:"+i+ "�� ,sql="+sql);
			}

			Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name+"end==============���������ڵ���Ϣ��");
		}
		#endregion	

		/// <summary>
		/// ��ȡ�ⲿ����
		/// </summary>
		/// <returns></returns>
		public static void DTSPCWork()
		{
			 
			ArrayList als = DA.ClassFactory.GetObjects("BP.WF.PCWorks");
			foreach(PCWorks wks in als)
			{
				wks.DoInitData();
			}
		}
		/// <summary>
		/// TransferAutoGenerWorkFlow
		/// </summary>
		/// <param name="flowNo">TransferAutoGenerWorkFlow</param>
		/// <returns></returns>
		public static string TransferAutoGenerWorkFlow(string flowNo)
		{
			Flow fl = new Flow(flowNo);
			return TransferAutoGenerWorkFlow(fl);
		}
		/// <summary>
		/// TransferAutoGenerWorkFlow
		/// </summary>
		/// <param name="fl"></param>
		/// <returns></returns>
		public static string TransferAutoGenerWorkFlow(Flow fl)
		{				 
			//if (fl.IsAutoWorkFlow==false)
			//	throw new Exception("@�˹������̲���һ���Զ������������̡�");
			string str="";
			PCTaxpayerStartWorks ptws=(PCTaxpayerStartWorks)fl.HisStartNode.HisWorks;
			try
			{
				str+= ptws.AutoGenerWorkFlow();
			}
			catch(Exception ex)
			{
				Log.DefaultLogWriteLine(LogType.Error,"@����["+fl.Name+"]�����ɹ������̳��ִ���"+ex.Message);
				//throw new Exception("@����["+fl.Name+"]�����ɹ������̳��ִ���"+ex.Message) ; 
			}
			return str;
		}
		/// <summary>
		/// �����Զ�������������
		/// </summary>
		public static string TransferAutoGenerWorkFlowAll()
		{
			string str="";
			Flows fls = new Flows();
			fls.RetrieveIsAutoWorkFlow();
			foreach(Flow fl in fls)
			{
				str+=TransferAutoGenerWorkFlow(fl);
			}
			return str;
		}

		/// <summary>
		/// StandardCheckss
		/// </summary>
		public WFDTS()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}

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
    /// WF 的摘要说明。
    /// 工作流.
    /// 这里包含了两个方面
    /// 工作的信息．
    /// 流程的信息．
    /// </summary>
    public class WorkNode
    {
        #region ToE
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
        #endregion

        #region 权限判断
        /// <summary>
        /// 判断一个人能不能对这个工作节点进行操作。
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
                    /*如果是开始工作节点，从工作岗位判断他有没有工作的权限。*/
                    return WorkFlow.IsCanDoWorkCheckByEmpStation(this.HisNode.NodeID, empId);
                }
                else
                {
                    /* 如果是初始化阶段,判断他的初始化节点 */
                    WorkerList wl = new WorkerList();
                    wl.WorkID = this.HisWork.OID;
                    wl.FK_Emp = empId;

                    Emp myEmp = new  Emp(empId);
                    wl.FK_EmpText = myEmp.Name;

                    wl.FK_Node = this.HisNode.NodeID;
                    wl.FK_NodeText = this.HisNode.Name;
                    return wl.IsExits;
                }
            }
            else
            {
                /* 如果是初始化阶段 */
                return false;
            }
        }
        #endregion

        //查询出每个节点表里的接收人集合（Emps）。
        public string GenerEmps(Node nd)
        {
            string str = "";
            foreach (WorkerList wl in this.HisWorkerLists)
                str = wl.FK_Emp + ",";
            return str;
        }
        private string _VirPath = null;
        /// <summary>
        /// 虚拟目录的路径
        /// </summary>
        public string VirPath
        {
            get
            {
                if (_VirPath == null && BP.SystemConfig.IsBSsystem)
                    _VirPath = System.Web.HttpContext.Current.Request.ApplicationPath ;
                return _VirPath;
            }
        }
        private string _AppType = null;
        /// <summary>
        /// 虚拟目录的路径
        /// </summary>
        public string AppType
        {
            get
            {
                if (_AppType == null)
                {
                    if (BP.Web.WebUser.IsWap)
                        _AppType = "/WF/WAP/";
                    else
                        _AppType = "/WF/";
                }
                return _AppType;
            }
        }
        private string nextStationName = "";
        /// <summary>
        /// 产生下一步的工作者
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

            // 如果执行了两次发送，那前一次的轨迹就需要被删除。这里是为了避免错误。
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            // 判断当前节点是否采集了目标人员.(分配责任区)
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
            //分配分局(所)长
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

            //从轨迹里面查询.
            sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
               + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
               + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + ")";
            dt = DBAccess.RunSQLReturnTable(sql);

            // 如果能够找到.
            if (dt.Rows.Count >= 1)
            {
                return WorkerListWayOfDept(town, dt);
            }

            // 没有查询到的情况下。
            Stations nextStations = town.HisNode.HisStations;
            // 找不到, 就要判断流向问题。
            Station fromSt = this.HisStationOfUse;
            string DeptofUse = this.HisDeptOfUse.No;
            if (nextStations.Count == 0)
                throw new Exception(this.ToEP1("WF2", "@工作流程{0}已经完成。", town.HisNode.Name));  //管理员维护错误，您没有给节点["+town.HisNode.Name+"]设置工作岗位。

            Station toSt = (Station)nextStations[0];
            if (fromSt.No == toSt.No)
            {
#warning edit 2008 - 05-28 .
                // sql = "SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_Dept FROM Port_EmpDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM Port_EmpSTATION WHERE FK_Station='" + toSt.No + "')";
                sql = "SELECT No,Name FROM Port_Emp WHERE NO='" + WebUser.No + "'";  // IN (SELECT FK_Dept FROM Port_EmpDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM Port_EmpSTATION WHERE FK_Station='" + toSt.No + "')";
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
          
        private WorkNode town = null;
       public WorkerLists GenerWorkerLists_WidthFID(WorkNode town)
       {
            this.town = town;
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;

            // 如果执行了两次发送，那前一次的轨迹就需要被删除。这里是为了避免错误。
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.FID + " AND FK_Node=" + town.HisNode.NodeID);

            #region 首先判断是否配置了获取下一步接受人员的sql.
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySQL)
            {
                if (town.HisNode.RecipientSQL.Length < 4)
                    throw new Exception("@您设置的当前节点按照sql，决定下一步的接受人员，但是你没有设置sql.");

                Attrs attrs = this.HisWork.EnMap.Attrs;
                sql = this.HisNode.RecipientSQL;
                foreach (Attr attr in attrs)
                {
                    if (attr.MyDataType == DataType.AppString)
                        sql = sql.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                    else
                        sql = sql.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                }
                sql = sql.Replace("~", "'");
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    throw new Exception("@没有找到可接受的工作人员。@技术信息：执行的sql没有发现人员:" + sql);

                return WorkerListWayOfDept(town, dt);
            }
            #endregion

            // 按上一节点发送人处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOper
                || town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
            {
                DataRow dr = dt.NewRow();
                dr[0] = Web.WebUser.No;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // 按照选择的人员处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
            {
                sql = "SELECT  FK_Emp FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            // 按照节点指定的人员处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpcEmp)
            {
                dt = DBAccess.RunSQLReturnTable("SELECT FK_Emp FROM WF_NodeEmp WHERE FK_Node=" + town.HisNode.NodeID);
                if (dt.Rows.Count==0)
                    throw new Exception("@您设置的当前节点按照节点绑定人员，但是你没有为("+town.HisNode.NodeID+","+town.HisNode.Name+")节点绑定人员。");

                //fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                //DataRow dr = dt.NewRow();
                //dr[0] = fk_emp;
                //dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // 按照表单字段人员处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByEmp)
            {
                if (this.HisWork.EnMap.Attrs.Contains("FK_Emp") == false)
                    throw new Exception("@您设置的当前节点按照指定节点表单字段人员，决定下一步的接受人员，但是你没有在节点(" + town.HisNode.NodeID + "," + town.HisNode.Name + ")表单中设置该表单FK_Emp字段。");

                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }


            // 判断 节点人员里是否有设置？  如果有就不考虑岗位设置了。从节点人员设置里面查询。
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
                }
                return WorkerListWayOfDept(town, dt);
            }

            // 判断节点部门里面是否设置了部门，如果设置了，就按照它的部门处理。
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
                    sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
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

            #region  最后一定是按照岗位来执行。
            if (this.HisNode.IsStartNode == false)
            {
                // 如果当前的节点不是开始节点， 从轨迹里面查询。
                sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                   + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                   + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.FID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                dt = DBAccess.RunSQLReturnTable(sql);

                // 如果能够找到.
                if (dt.Rows.Count >= 1)
                {
                    if (dt.Rows.Count == 1)
                    {
                        /*如果人员只有一个的情况，说明他可能要 */
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }

            /* 如果执行节点 与 接受节点岗位集合一致 */
            if (this.HisNode.GroupStaNDs == town.HisNode.GroupStaNDs)
            {
                /* 说明，就把当前人员做为下一个节点处理人。*/
                DataRow dr = dt.NewRow();
                dr[0] = WebUser.No;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }


            /* 如果执行节点 与 接受节点岗位集合不一致 */
            if (this.HisNode.GroupStaNDs != town.HisNode.GroupStaNDs)
            {
                /* 没有查询到的情况下, 先按照本部门计算。*/
                sql = "SELECT No FROM Port_Emp WHERE NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Stations nextStations = town.HisNode.HisStations;
                    if (nextStations.Count == 0)
                        throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                }
                else
                {
                    bool isInit = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[0].ToString() == Web.WebUser.No)
                        {
                            /* 如果岗位分组不一样，并且结果集合里还有当前的人员，就说明了出现了当前操作员，拥有本节点上的岗位也拥有下一个节点的工作岗位
                             导致：节点的分组不同，传递到同一个人身上。 */
                            isInit = true;
                        }
                    }
                    if (isInit == false)
                        return WorkerListWayOfDept(town, dt);
                }
            }

            // 没有查询到的情况下, 执行查询隶属本部门的下级部门人员。
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')"
               + " AND No!='" + WebUser.No + "'";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }


            // 没有查询到的情况下, 按照最大匹配数 提高一个级别 计算，递归算法未完成，不过现在已经满足大部分需要。
            int lengthStep = 0; //增长步骤。
            while (true)
            {
                lengthStep += 2;
                int toLen = WebUser.FK_Dept.Length - lengthStep;
                if (toLen <= 0)
                    throw new Exception("@已经列举到最高部门，仍然没有找到接受人员。");

                sql = "SELECT NO FROM Port_Emp WHERE No IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - lengthStep) + "%')"
                   + " AND No!='" + WebUser.No + "'";


                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Stations nextStations = town.HisNode.HisStations;
                    if (nextStations.Count == 0)
                        throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);

                    sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                    sql += " AND  No IN ";
                    if (WebUser.FK_Dept.Length == 2)
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!='" + WebUser.No + "') ";
                    else
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!='" + WebUser.No + "' AND FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 4) + "%')";

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No!='" + WebUser.No + "' AND No IN ";
                        sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            string msg = town.HisNode.HisStationsStr;
                            throw new Exception(this.ToE("WF3", "岗位(" + msg + ")下没有人员，对应节点:") + town.HisNode.Name);
                            //"维护错误，请检查[" + town.HisNode.Name + "]维护的岗位中是否有人员？"
                        }
                    }
                    return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    return WorkerListWayOfDept(town, dt);
                }
            }
            #endregion  按照岗位来执行。
        }
        public WorkerLists GenerWorkerLists(WorkNode town)
        {
            this.town = town;

            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;

            // 如果执行了两次发送，那前一次的轨迹就需要被删除。这里是为了避免错误。
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            // 如果指定特定的人员处理。
            if (JumpToEmp != null)
            {
                DataRow dr = dt.NewRow();
                dr[0] = JumpToEmp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // 按上一节点发送人处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOper
                || town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
            {
                DataRow dr = dt.NewRow();
                dr[0] = Web.WebUser.No;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }
            //首先判断是否配置了获取下一步接受人员的sql.
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySQL)
            {
                if (town.HisNode.RecipientSQL.Length < 4)
                    throw new Exception("@您设置的当前节点按照sql，决定下一步的接受人员，但是你没有设置sql.");

                Attrs attrs = this.HisWork.EnMap.Attrs;
                sql = town.HisNode.RecipientSQL;
                foreach (Attr attr in attrs)
                {
                    if (attr.MyDataType == DataType.AppString)
                        sql = sql.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                    else
                        sql = sql.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                }

                sql = sql.Replace("~", "'");
                sql = sql.Replace("@WebUser.No", "'"+WebUser.No+"'");
                sql = sql.Replace("@WebUser.Name", "'" + WebUser.Name + "'");
                sql = sql.Replace("@WebUser.FK_Dept", "'" + WebUser.FK_Dept + "'");

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    throw new Exception("@没有找到可接受的工作人员。@技术信息：执行的sql没有发现人员:" + sql);
                return WorkerListWayOfDept(town, dt);
            }

            // 按照选择的人员处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
            {
                sql = "SELECT FK_Emp FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    throw new Exception("请选择下一步骤工作(" + town.HisNode.Name + ")接受人员。");
                return WorkerListWayOfDept(town, dt);
            }

            // 按照节点指定的人员处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.BySpcEmp)
            {
                dt = DBAccess.RunSQLReturnTable("SELECT FK_Emp FROM WF_NodeEmp WHERE FK_Node=" + town.HisNode.NodeID);
                if (dt.Rows.Count == 0)
                    throw new Exception("@您设置的当前节点按照节点绑定人员，但是你没有为(" + town.HisNode.NodeID + "," + town.HisNode.Name + ")节点绑定人员。");

                //fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                //DataRow dr = dt.NewRow();
                //dr[0] = fk_emp;
                //dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }


            // 按照节点指定的人员处理。
            if (town.HisNode.HisDeliveryWay == DeliveryWay.ByEmp)
            {
                if (this.HisWork.EnMap.Attrs.Contains("FK_Emp") == false)
                    throw new Exception("@您设置的当前节点按照指定的人员，决定下一步的接受人员，但是你没有在节点表单中设置该表单FK_Emp字段。");

                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // 判断 节点人员里是否有设置？  如果有就不考虑岗位设置了。从节点人员设置里面查询。
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
                }
                return WorkerListWayOfDept(town, dt);
            }

            string prjNo="";
            FlowAppType flowAppType = this.HisNode.HisFlow.HisFlowAppType;
            sql = "";
            if (this.HisNode.HisFlow.HisFlowAppType == FlowAppType.PRJ)
            {
                prjNo = "";
                try
                {
                    prjNo = this.HisWork.GetValStrByKey("PrjNo");
                }
                catch (Exception ex)
                {
                    throw new Exception("@当前流程是工程类流程，但是在节点表单中没有PrjNo字段(注意区分大小写)，请确认。@异常信息:" + ex.Message);
                }
            }

            // 判断节点部门里面是否设置了部门，如果设置了，就按照它的部门处理。
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
                    if (flowAppType == FlowAppType.Normal)
                    {
                        sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                        sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                        sql += ")";
                        sql += "AND NO IN ";
                        sql += "(";
                        sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                        sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
                        sql += ")";
                    }

                    if (flowAppType == FlowAppType.PRJ)
                    {
                        sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                        sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                        sql += ")";
                        sql += "AND NO IN ";
                        sql += "(";
                        sql += "SELECT FK_Emp FROM Prj_EmpPrjStation WHERE FK_Station IN ";
                        sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") AND FK_Prj='" + prjNo + "'";
                        sql += ")";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            /* 如果项目组里没有工作人员就提交到公共部门里去找。*/
                            sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
                            sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                            sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                            sql += ")";
                            sql += "AND NO IN ";
                            sql += "(";
                            sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                            sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
                            sql += ")";
                        }
                        else
                        {
                            return WorkerListWayOfDept(town, dt);
                        }
                    }

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count > 0)
                        return WorkerListWayOfDept(town, dt);
                }
            }

            #region  按照岗位来执行。
            if (this.HisNode.IsStartNode == false)
            {
                if (flowAppType == FlowAppType.Normal)
                {
                    // 如果当前的节点不是开始节点， 从轨迹里面查询。
                    sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                       + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                       + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                }

                if (flowAppType == FlowAppType.PRJ)
                {
                    // 如果当前的节点不是开始节点， 从轨迹里面查询。
                    sql = "SELECT DISTINCT FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Station IN "
                       + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") AND FK_Prj='" + prjNo + "' "
                       + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        /* 如果项目组里没有工作人员就提交到公共部门里去找。*/
                        sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                         + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                         + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                    }
                    else
                    {
                        return WorkerListWayOfDept(town, dt);
                    }
                }

                dt = DBAccess.RunSQLReturnTable(sql);
                // 如果能够找到.
                if (dt.Rows.Count >= 1)
                {
                    if (dt.Rows.Count == 1)
                    {
                        /*如果人员只有一个的情况，说明他可能要 */
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }

            /* 如果执行节点 与 接受节点岗位集合一致 */
            if (this.HisNode.GroupStaNDs == town.HisNode.GroupStaNDs)
            {
                /* 说明，就把当前人员做为下一个节点处理人。*/
                DataRow dr = dt.NewRow();
                dr[0] = WebUser.No;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            /* 如果执行节点 与 接受节点岗位集合不一致 */
            if (this.HisNode.GroupStaNDs != town.HisNode.GroupStaNDs)
            {
                /* 没有查询到的情况下, 先按照本部门计算。*/
                if (flowAppType == FlowAppType.Normal)
                {
                    sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                       + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                       + " AND  NO IN "
                       + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";
                }

                if (flowAppType == FlowAppType.PRJ)
                {
                    sql = "SELECT  FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Prj='" + prjNo + "' AND FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")"
                    + " AND  FK_Prj='" + prjNo + "' ";

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        /* 如果项目组里没有工作人员就提交到公共部门里去找。 */
                        sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                      + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                      + " AND  NO IN "
                      + "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";
                    }
                    else
                    {
                        return WorkerListWayOfDept(town, dt);
                    }
                }

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Stations nextStations = town.HisNode.HisStations;
                    if (nextStations.Count == 0)
                        throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                }
                else
                {
                    bool isInit = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[0].ToString() == Web.WebUser.No)
                        {
                            /* 如果岗位分组不一样，并且结果集合里还有当前的人员，就说明了出现了当前操作员，拥有本节点上的岗位也拥有下一个节点的工作岗位
                             导致：节点的分组不同，传递到同一个人身上。 */
                            isInit = true;
                        }
                    }
#warning edit by peng, 用来确定不同岗位集合的传递包含同一个人的处理方式。
                    if (isInit == false || isInit == true)
                        return WorkerListWayOfDept(town, dt);
                }
            }

            // 没有查询到的情况下, 执行查询隶属本部门的下级部门人员。

            if (flowAppType == FlowAppType.Normal)
            {
                sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')"
                   + " AND No!='" + WebUser.No + "'";
            }

            if (flowAppType == FlowAppType.PRJ)
            {
                sql = "SELECT  FK_Emp  FROM Prj_EmpPrjStation WHERE FK_Prj='" + prjNo + "' AND FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")"
                    + " AND  FK_Prj='" + prjNo + "' ";

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    /* 如果项目组里没有工作人员就提交到公共部门里去找。*/
                    sql = "SELECT NO FROM Port_Emp WHERE NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')"
                   + " AND No!='" + WebUser.No + "'";
                }
                else
                {
                    return WorkerListWayOfDept(town, dt);
                }
            }

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }


            /* 没有查询到的情况下, 按照最大匹配数 提高一个级别 计算，递归算法未完成，不过现在已经满足大部分需要。
             * 
             * 因为:以上已经做的岗位的判断，就没有必要在判断其它类型的流程处理了。
             * 
             * */

            int lengthStep = 0; //增长步骤。
            while (true)
            {
                lengthStep += 2;
                sql = "SELECT NO FROM Port_Emp WHERE No IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
                   + " AND  NO IN "
                   + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - lengthStep) + "%')"
                   + " AND No!='" + WebUser.No + "'";

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    Stations nextStations = town.HisNode.HisStations;
                    if (nextStations.Count == 0)
                        throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);

                    sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                    sql += " AND  No IN ";
                    if (WebUser.FK_Dept.Length == 2)
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE   FK_Emp!='" + WebUser.No + "') ";
                    else
                        sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Emp!='" + WebUser.No + "' AND FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 4) + "%')";

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No!='" + WebUser.No + "' AND No IN ";
                        sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            string msg = town.HisNode.HisStationsStr;
                            throw new Exception(this.ToE("WF3", "岗位(" + msg + ")下没有人员，对应节点:") + town.HisNode.Name);
                            //"维护错误，请检查[" + town.HisNode.Name + "]维护的岗位中是否有人员？"
                        }
                    }
                    return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    return WorkerListWayOfDept(town, dt);
                }
            }
            #endregion  按照岗位来执行。
        }

        public WorkerLists GenerWorkerListsV2(WorkNode town)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;
            // 如果执行了两次发送，那前一次的轨迹就需要被删除。这里是为了避免错误。
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            //首先判断是否配置了获取下一步接受人员的sql.
            if (this.HisNode.RecipientSQL.Length > 4)
            {
                Attrs attrs = this.HisWork.EnMap.Attrs;
                sql = this.HisNode.RecipientSQL;
                foreach (Attr attr in attrs)
                {
                    if (attr.MyDataType == DataType.AppString)
                        sql = sql.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                    else
                        sql = sql.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                }

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    if (SystemConfig.IsDebug)
                        throw new Exception("@没有找到可接受的工作人员。@技术信息：执行的sql没有发现人员:" + sql);
                    else
                        throw new Exception("@没有找到可接受的工作人员。");
                }
                return WorkerListWayOfDept(town, dt);
            }

            if (this.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
            {
                sql = "SELECT  FK_Emp  FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            // 判断当前节点是否采集了目标人员
            if (this.HisWork.EnMap.Attrs.Contains("FK_Emp"))
            {
                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // 判断 节点人员里是否有设置？  如果有就不考虑岗位设置了。从节点人员设置里面查询。
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
                }
                return WorkerListWayOfDept(town, dt);
            }

            // 判断节点部门里面是否设置了部门，如果设置了，就按照它的部门处理。
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
                    sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
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
                // 如果当前的节点不是开始节点， 从轨迹里面查询。
                sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                   + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                   + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs, true) + ") )";
                dt = DBAccess.RunSQLReturnTable(sql);

                // 如果能够找到.
                if (dt.Rows.Count >= 1)
                {
                    if (dt.Rows.Count == 1)
                    {
                        /*如果人员只有一个的情况，说明他可能要 */
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }

            // 没有查询到的情况下, 先按照本部门计算。
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                // throw new Exception(this.ToEP1("WN2", "@工作流程{0}已经完成。", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // 没有查询到的情况下, 按照最大匹配数计算。
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                //  throw new Exception(this.ToEP1("WN2", "@工作流程{0}已经完成。", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // 没有查询到的情况下, 按照最大匹配数 提高一个级别 计算。
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 2) + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                //throw new Exception(this.ToEP1("WF2", "@工作流程{0}已经完成。", town.HisNode.Name));
                else
                {
                    sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                    sql += " AND  No IN ";
                    if (WebUser.FK_Dept.Length == 2)
                        sql += "(SELECT  FK_Emp  FROM Port_EmpDept )";
                    else
                        sql += "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 4) + "%')";

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No IN ";
                        sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            string msg = town.HisNode.HisStationsStr;
                            throw new Exception(this.ToE("WF3", "岗位(" + msg + ")下没有人员，对应节点:") + town.HisNode.Name);
                            //"维护错误，请检查[" + town.HisNode.Name + "]维护的岗位中是否有人员？"
                        }
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }
        }
        public bool IsSubFlowWorkNode
        {
            get
            {
                if (this.HisWork.FID == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 生成一个word
        /// </summary>
        public void DoPrint()
        {
            string tempFile = SystemConfig.PathOfTemp + "\\" + this.WorkID + ".doc";
            Work wk = this.HisNode.HisWork;
            wk.OID = this.WorkID;
            wk.Retrieve();
            Glo.GenerWord(tempFile, wk);
            PubClass.OpenWordDocV2(tempFile, this.HisNode.Name + ".doc");
        }
        public WorkNode DoReturnWork(int backtoNodeID, string msg)
        {
            return DoReturnWork(backtoNodeID, msg, false);
        }
        private WorkNode DoReturnSubFlow(int backtoNodeID, string msg, bool isHiden)
        {
            Node nd = new Node(backtoNodeID);

            // 删除可能存在的数据.
            string sql = "DELETE  FROM WF_GenerWorkerList WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.HisWork.OID + "  AND FID=" + this.HisWork.FID;
            BP.DA.DBAccess.RunSQL(sql);

            // 找出分合流点处理的人员.
            sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.HisWork.FID;
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count != 1)
                throw new Exception("@ system error , this values must be =1");

            string fk_emp = dt.Rows[0][0].ToString();

            // 获取当前工作的信息.
            WorkerList wl = new WorkerList(this.HisWork.FID, this.HisNode.NodeID, fk_emp);
            Emp emp = new Emp(fk_emp);

            // 改变部分属性让它适应新的数据,并显示一条新的待办工作让用户看到。
            wl.IsPass = false;
            wl.WorkID = this.HisWork.OID;
            wl.FID = this.HisWork.FID;
            wl.RDT = DataType.CurrentDataTime;
            wl.FK_Emp = fk_emp;
            wl.FK_EmpText = emp.Name;
             
            wl.FK_Node = backtoNodeID;
            wl.FK_NodeText = nd.Name;
            wl.WarningDays = nd.WarningDays;
            wl.FK_Dept = emp.FK_Dept;

            DateTime dtNew = DateTime.Now;
            dtNew = dtNew.AddDays(nd.WarningDays);
            wl.SDT = dtNew.ToString(DataType.SysDataFormat); // DataType.CurrentDataTime;
            wl.FK_Flow = this.HisNode.FK_Flow;
            wl.Insert();

            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID);
            gwf.FK_Node = backtoNodeID;
            gwf.NodeName = nd.Name;
            gwf.DirectUpdate();

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);

            /* 如果是隐性退回。*/
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wl.WorkID;
            rw.ReturnToNode = wl.FK_Node;
            rw.ReturnNode = this.HisNode.NodeID;
            rw.ReturnNodeName = this.HisNode.Name;
            rw.ReturnToEmp = fk_emp;
            rw.Note = msg;
            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Insert();

            // 加入track.
            this.AddToTrack(ActionType.Return, fk_emp, emp.Name, backtoNodeID, nd.Name, msg);

            WorkNode wn = new WorkNode(this.HisWork.FID, backtoNodeID);
            if (Glo.IsEnableSysMessage)
            {
                WF.Port.WFEmp wfemp = new Port.WFEmp(wn.HisWork.Rec);
                BP.TA.SMS.AddMsg(wl.WorkID + "_" + wl.FK_Node + "_" + wfemp.No, wfemp.No,
                    wfemp.HisAlertWay, wfemp.Tel,
                      this.ToEP3("WN27", "工作退回：流程:{0}.工作:{1},退回人:{2},需您处理",
                      wn.HisNode.FlowName, wn.HisNode.Name, WebUser.Name),
                      wfemp.Email, null, msg);
            }
            return wn;

            throw new Exception("分流子线程退回到分流点或者分合流点功能，没有实现。");
        }
        /// <summary>
        /// 工作退回
        /// </summary>
        /// <param name="backtoNodeID">退回到节点</param>
        /// <param name="msg">退回信息</param>
        /// <param name="isHiden">是否隐性退回</param>
        /// <returns></returns>
        public WorkNode DoReturnWork(int backtoNodeID, string msg, bool isHiden)
        {

            if (this.HisNode.FocusField != "")
            {
                // 把数据更新它。
                this.HisWork.Update(this.HisNode.FocusField, "");
            }

            Node backToNode = new Node(backtoNodeID);
            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.WorkHL: /*如果当前是合流点 */
                    /* 杀掉进程*/
                    WorkerLists wlsSubs = new WorkerLists();
                    wlsSubs.Retrieve(WorkerListAttr.FID, this.HisWork.OID);
                    foreach (WorkerList sub in wlsSubs)
                    {
                        GenerWorkFlow gw1f = new GenerWorkFlow();
                        gw1f.WorkID = sub.WorkID;
                        gw1f.DirectDelete();

                        Node subNode = new Node(sub.FK_Node);
                        Work wk = subNode.GetWork(sub.WorkID);
                        wk.Delete();
                        sub.Delete();
                    }
                    GenerFH fh = new GenerFH();
                    fh.FID = this.WorkID;
                    fh.Delete();
                    break;
                default:
                    switch (backToNode.HisNodeWorkType)
                    {
                        case NodeWorkType.StartWorkFL:
                        case NodeWorkType.WorkFL:
                        case NodeWorkType.WorkFHL:
                            if (this.IsSubFlowWorkNode)
                            {
                                /* 如果是支流，并且向分流或者分合流节点上退回. */
                                return DoReturnSubFlow(backtoNodeID, msg, isHiden);
                            }
                            // return DoReturnSubFlow(backtoNodeID, msg, isHiden);
                            break;
                        default:
                            break;
                    }
                    break;
            }

            // 改变当前的工作节点．
            WorkNode wnOfBackTo = new WorkNode(this.WorkID, backtoNodeID);
            wnOfBackTo.HisWork.NodeState = NodeState.Back; // 更新 return work 状态．
            wnOfBackTo.HisWork.DirectUpdate();

            // 改变当前待办工作节点。
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow   SET FK_Node='" + backtoNodeID + "',NodeName='" + backToNode.Name+ "' WHERE  WorkID=" + this.WorkID);
            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.WorkID);

            // 记录退回轨迹。
            ReturnWork rw = new ReturnWork();
            rw.WorkID = wnOfBackTo.HisWork.OID;
            rw.ReturnToNode = wnOfBackTo.HisNode.NodeID;
            rw.ReturnNodeName = this.HisNode.Name;

            rw.ReturnNode = this.HisNode.NodeID; // 当前退回节点.
            rw.ReturnToEmp = wnOfBackTo.HisWork.Rec; //退回给。

            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Note = msg;
            rw.Insert();

            // 加入track.
            this.AddToTrack(ActionType.Return, wnOfBackTo.HisWork.Rec, wnOfBackTo.HisWork.RecText, 
                backtoNodeID, wnOfBackTo.HisNode.Name, msg);

            try
            {
                // 记录退回日志.
                ReorderLog(backToNode, this.HisNode, rw);
            }
            catch(Exception ex)
            {
                Log.DebugWriteWarning(ex.Message);
            }

            // 以退回到的节点向前数据用递归删除它。
            DeleteToNodesData(backToNode.HisToNodes);

            //删除正常的垃圾数据。
            DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE WorkID NOT IN (SELECT WorkID FROM WF_GenerWorkerList )");
            DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID NOT IN (SELECT FID FROM WF_GenerWorkerList)");

            //向他发送消息。
            WorkNode backWN = new WorkNode(this.WorkID, backtoNodeID);

            if (Glo.IsEnableSysMessage ==true)
            {
                WF.Port.WFEmp wfemp = new Port.WFEmp(wnOfBackTo.HisWork.Rec);
                BP.TA.SMS.AddMsg(rw.MyPK, wfemp.No,
                    wfemp.HisAlertWay, wfemp.Tel,
                      this.ToEP3("WN27", "工作退回：流程:{0}.工作:{1},退回人:{2},需您处理",
                      backToNode.FlowName, backToNode.Name, WebUser.Name),
                      wfemp.Email, null, msg);
            }
            return wnOfBackTo;
        }
        private string infoLog = "";
        public void ReorderLog(Node fromND, Node toND, ReturnWork rw)
        {
            string filePath = BP.SystemConfig.PathOfDataUser + "\\ReturnLog\\" + this.HisNode.FK_Flow + "\\";
            if (System.IO.Directory.Exists(filePath) == false)
                System.IO.Directory.CreateDirectory(filePath);

            string file = filePath + "\\" + rw.MyPK;
            infoLog = "\r\n退回人:" + WebUser.No+","+WebUser.Name + " \r\n退回节点:" + fromND.Name + " \r\n退回到:" + toND.Name;
            infoLog += "\r\n退回时间:" + DataType.CurrentDataTime;
            infoLog += "\r\n原因:" + rw.Note;

            ReorderLog(fromND, toND);
            DataType.WriteFile(file+".txt", infoLog);
            DataType.WriteFile(file + ".htm", infoLog.Replace("\r\n","<br>"));

            this.HisWork.Delete();
        }
        public void ReorderLog(Node fromND, Node toND)
        {
            /*开始遍历到达的节点集合*/
            foreach (Node nd in fromND.HisToNodes)
            {
                Work wk = nd.HisWork;
                wk.OID = this.WorkID;
                if (wk.RetrieveFromDBSources() == 0)
                {
                    wk.FID = this.WorkID;
                    if (wk.Retrieve(WorkAttr.FID, this.WorkID) == 0)
                        continue;
                }

                if (nd.IsFL)
                {
                    /* 如果是分流 */
                    WorkerLists wls = new WorkerLists();
                    QueryObject qo = new QueryObject(wls);
                    qo.AddWhere(WorkerListAttr.FID, this.WorkID);
                    qo.addAnd();

                    string[] ndsStrs = nd.HisToNDs.Split('@');
                    string inStr = "";
                    foreach (string s in ndsStrs)
                    {
                        if (s == "" || s == null)
                            continue;
                        inStr += "'" + s + "',";
                    }
                    inStr = inStr.Substring(0, inStr.Length - 1);
                    if (inStr.Contains(",") == true)
                        qo.AddWhere(WorkerListAttr.FK_Node, int.Parse(inStr));
                    else
                        qo.AddWhereIn(WorkerListAttr.FK_Node, "(" + inStr + ")");

                    qo.DoQuery();
                    foreach (WorkerList wl in wls)
                    {
                        Node subNd = new Node(wl.FK_Node);
                        Work subWK = subNd.GetWork(wl.WorkID);

                        infoLog += "\r\n*****************************************************************************************";
                        infoLog += "\r\n节点ID:" + subNd.NodeID + "  工作名称:" + subWK.EnDesc;
                        infoLog += "\r\n处理人:" + subWK.Rec + " , " + wk.RecOfEmp.Name;
                        infoLog += "\r\n接收时间:" + subWK.RDT + " 处理时间:" + subWK.CDT;
                        infoLog += "\r\n ------------------------------------------------- ";


                        foreach (Attr attr in wk.EnMap.Attrs)
                        {
                            if (attr.UIVisible == false)
                                continue;
                            infoLog += "\r\n " + attr.Desc + ":" + subWK.GetValStrByKey(attr.Key);
                        }

                        //递归调用。
                        ReorderLog(subNd, toND);
                    }
                }
                else
                {
                    infoLog += "\r\n*****************************************************************************************";
                    infoLog += "\r\n节点ID:" + wk.NodeID + "  工作名称:" + wk.EnDesc;
                    infoLog += "\r\n处理人:" + wk.Rec +" , "+wk.RecOfEmp.Name;
                    infoLog += "\r\n接收时间:" + wk.RDT + " 处理时间:" + wk.CDT;
                    infoLog += "\r\n ------------------------------------------------- ";

                    foreach (Attr attr in wk.EnMap.Attrs)
                    {
                        if (attr.UIVisible == false)
                            continue;
                        infoLog += "\r\n" + attr.Desc + " : " + wk.GetValStrByKey(attr.Key);
                    }
                }

                /* 如果到了当前的节点 */
                if (nd.NodeID == toND.NodeID)
                    break;

                //递归调用。
                ReorderLog(nd, toND);
            }
        }
        /// <summary>
        /// 递归删除两个节点之间的数据
        /// </summary>
        /// <param name="nds">到达的节点集合</param>
        public void DeleteToNodesData(Nodes nds)
        {
            /*开始遍历到达的节点集合*/
            foreach (Node nd in nds)
            {
                Work wk = nd.HisWork;
                wk.OID = this.WorkID;
                if (wk.Delete() == 0)
                {
                    wk.FID = this.WorkID;
                    if (wk.Delete(WorkAttr.FID, this.WorkID) == 0)
                        continue;
                }

                #region 删除当前节点数据，删除附件信息。
                // 删除明细表信息。
                MapDtls dtls = new MapDtls("ND" + nd.NodeID);
                foreach (MapDtl dtl in dtls)
                {
                    BP.DA.DBAccess.RunSQL("DELETE " + dtl.PTable + " WHERE RefPK='" + this.WorkID + "'");
                }

                // 删除表单附件信息。
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE RefPKVal='" + this.WorkID + "' AND FK_MapData='ND" + nd.NodeID + "'");
                // 删除签名信息。
                BP.DA.DBAccess.RunSQL("DELETE FROM Sys_FrmEleDB WHERE RefPKVal='" + this.WorkID + "' AND FK_MapData='ND" + nd.NodeID + "'");
                #endregion 删除当前节点数据。


                /*说明:已经删除该节点数据。*/
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + ") AND FK_Node=" + nd.NodeID);
                if (nd.IsFL)
                {
                    /* 如果是分流 */
                    WorkerLists wls = new WorkerLists();
                    QueryObject qo = new QueryObject(wls);
                    qo.AddWhere(WorkerListAttr.FID, this.WorkID);
                    qo.addAnd();

                    string[] ndsStrs = nd.HisToNDs.Split('@');
                    string inStr = "";
                    foreach (string s in ndsStrs)
                    {
                        if (s == "" || s == null)
                            continue;
                        inStr += "'" + s + "',";
                    }
                    inStr = inStr.Substring(0, inStr.Length - 1);
                    if (inStr.Contains(",") == true)
                        qo.AddWhere(WorkerListAttr.FK_Node, int.Parse(inStr));
                    else
                        qo.AddWhereIn(WorkerListAttr.FK_Node, "(" + inStr + ")");

                    qo.DoQuery();
                    foreach (WorkerList wl in wls)
                    {
                        Node subNd = new Node(wl.FK_Node);
                        Work subWK = subNd.GetWork(wl.WorkID);
                        subWK.Delete();

                        //删除分流下步骤的节点信息.
                        DeleteToNodesData(subNd.HisToNodes);
                    }

                    DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + this.WorkID);
                }
                DeleteToNodesData(nd.HisToNodes);
            }
        }
        /// <summary>
        /// 执行退回
        /// </summary>
        /// <param name="msg">退回工作的原因</param>
        /// <returns></returns>
        public WorkNode DoReturnWork(string msg)
        {
            // 改变当前的工作节点．
            WorkNode wn = this.GetPreviousWorkNode();
            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID);
            gwf.FK_Node = wn.HisNode.NodeID;
            gwf.NodeName = wn.HisNode.Name;
            gwf.DirectUpdate();

            // 更新 return work 状态．
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            // 删除工作者.
            WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            wkls.Delete();

            // 写入日志.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wn.HisWork.OID;
            rw.ReturnToNode = wn.HisNode.NodeID;
            rw.ReturnToEmp = wn.HisWork.Rec;
            rw.ReturnNode = this.HisNode.NodeID;
            rw.ReturnNodeName = this.HisNode.Name;

            rw.Note = msg;
            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Insert();

            //WorkFlow wf = this.HisWorkFlow;
            //wf.WritLog(msg);
            // 消息通知上一步工作人员处理．
            WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "退回的工作", wn.HisNode.Name, "退回的工作");

            // 删除退回时当前节点的工作信息。
            this.HisWork.Delete();
            return wn;
        }
        /// <summary>
        /// 执行退回(在合流节点上执行退回到上一个步骤)
        /// </summary>
        /// <param name="workid">线程ID</param>
        /// <param name="msg">退回工作的原因</param>
        /// <returns></returns>
        public WorkNode DoReturnWorkHL(Int64 workid, string msg)
        {
            // 改变当前的工作节点．
            WorkNode wn = this.GetPreviousWorkNode_FHL(workid);

            GenerWorkFlow gwf = new GenerWorkFlow(workid);
            gwf.FK_Node = wn.HisNode.NodeID;
            gwf.NodeName = wn.HisNode.Name;
            gwf.DirectUpdate();

            // 更新 return work 状态．
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            // 删除工作者.
            WorkerLists wkls = new WorkerLists(workid, this.HisNode.NodeID);
            wkls.Delete();

            // 写入日志.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = workid;
            rw.ReturnToNode = wn.HisNode.NodeID;
            rw.ReturnToEmp = wn.HisWork.Rec;
            rw.ReturnNode = this.HisNode.NodeID; 

            rw.Note = msg;
            rw.MyPK = rw.ReturnToNode + "_" + rw.WorkID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            rw.Insert();

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node='" + wn.HisNode.NodeID + "' and workid=" + workid);

            //WorkFlow wf = this.HisWorkFlow;
            //wf.WritLog(msg);
            // 消息通知上一步工作人员处理．
            WorkerLists wls = new WorkerLists(workid, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "退回的工作", wn.HisNode.Name, "退回的工作");

            // 删除退回时当前节点的工作信息。
           // this.HisWork.Delete();
            return wn;
        }
        /// <summary>
        /// 执行工作完成
        /// </summary>
        public string DoSetThisWorkOver()
        {
            this.HisWork.AfterWorkNodeComplete();

            // 更新状态。
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.SetValByKey("CDT", DataType.CurrentDataTime);
            this.HisWork.Rec = Web.WebUser.No;
            this.HisWork.DirectUpdate();

            // 清除其他的工作者.
            string sql = "";
            sql = "DELETE FROM WF_GenerWorkerlist WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp <> '" + this.HisWork.Rec.ToString() + "'";
            DBAccess.RunSQL(sql);
            return "";
        }
        /// <summary>
        /// 按照 "职务" 得到的能够执行这项工作的集合
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDuty(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDuty  a, Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty ) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// 按照 "部门" 得到的能够执行这项工作的集合
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDept(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDept  a, Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }

        #region 根据工作岗位生成工作者
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt)
        {
            return WorkerListWayOfDept(town,dt,0);
        }
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt, Int64 fid)
        {
            if (dt.Rows.Count == 0)
                throw new Exception(this.ToE("WN4", "接受人员列表为空.")); // 接受人员列表为空

            Int64 workID = fid;
            if (workID == 0)
                workID = this.HisWork.OID;

            int toNodeId = town.HisNode.NodeID;
            this.HisWorkerLists = new WorkerLists();
            this.HisWorkerLists.Clear();

#warning 限期时间  town.HisNode.DeductDays-1

            // 2008-01-22 之前的东西。
            //int i = town.HisNode.DeductDays;
            //dtOfShould = DataType.AddDays(dtOfShould, i);
            //if (town.HisNode.WarningDays > 0)
            //    dtOfWarning = DataType.AddDays(dtOfWarning, i - town.HisNode.WarningDays);
            // edit at 2008-01-22 , 处理预警日期的问题。

            DateTime dtOfShould;
            int day = 0;
            int hh = 0;
            if (town.HisNode.DeductDays < 1)
                day = 0;
            else
                day =int.Parse( town.HisNode.DeductDays.ToString());

             dtOfShould = DataType.AddDays(DateTime.Now, day);
            DateTime dtOfWarning = DateTime.Now;
            if (town.HisNode.WarningDays > 0)
                dtOfWarning = DataType.AddDays(dtOfShould, - int.Parse(town.HisNode.WarningDays.ToString())); // dtOfShould.AddDays(-town.HisNode.WarningDays);

            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWorkFL:
                case NodeWorkType.WorkFHL:
                case NodeWorkType.WorkFL:
                case NodeWorkType.WorkHL:
                    break;
                default:
                    this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node,
                        town.HisNode.NodeID,
               "SDT", dtOfShould.ToString("yyyy-MM-dd"));
                    break;
            }

            if (dt.Rows.Count == 1)
            {
                /* 如果只有一个人员 */
                WorkerList wl = new WorkerList();
                wl.WorkID = workID;
                wl.FK_Node = toNodeId;
                wl.FK_NodeText = town.HisNode.Name;

                wl.FK_Emp = dt.Rows[0][0].ToString();

                Emp emp = new Emp(wl.FK_Emp);
                wl.FK_EmpText = emp.Name;
                wl.FK_Dept = emp.FK_Dept;
                wl.WarningDays = town.HisNode.WarningDays;
                wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);

                wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                wl.FK_Flow = town.HisNode.FK_Flow;
                wl.FID = town.HisWork.FID;
                wl.Sender = WebUser.No;
                //if (wl.FID == 0)
                //    wl.FID = this.WorkID;

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
                /* 如果有多个人员 */
                RememberMe rm = this.GetHisRememberMe(town.HisNode);

                // 如果按照选择的人员处理，就设置它的记忆为空。2011-11-06处理电厂需求.
                if (this.town.HisNode.HisDeliveryWay == DeliveryWay.BySelected)
                {
                    if (rm != null)
                        rm.Objs = "";
                }

                if (this.HisNode.IsFL)
                {
                    if (rm != null)
                        rm.Objs = "";
                }

                // 记忆中是否存在当前的人员。
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
                    /* 记忆里面没有当前生成的操作人员 */
                    /* 已经保证了没有重复的人员。*/
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
                        wl.FK_NodeText = town.HisNode.Name;
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
                        wl.WarningDays = town.HisNode.WarningDays;
                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;
                        wl.Sender = WebUser.No;

                        //if (wl.FID == 0)
                        //    wl.FID = this.WorkID;
                        try
                        {
                            wl.DirectInsert();
                            this.HisWorkerLists.AddEntity(wl);
                        }
                        catch (Exception ex)
                        {
                            Log.DefaultLogWriteLineError("不应该出现的异常信息：" + ex.Message);
                        }
                    }
                }
                else
                {
                    string[] strs = rm.Objs.Split('@');
                    string myemps = "";
                    foreach (string s in strs)
                    {
                        if (s.Length < 1)
                            continue;

                        if (myemps.IndexOf(s) != -1)
                            continue;

                        myemps += "@" + s;

                        WorkerList wl = new WorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_NodeText = town.HisNode.Name;
                        wl.FK_Emp = s;
                        Emp emp = null;
                        try
                        {
                            emp = new Emp(s);
                        }
                        catch
                        {
                            continue;
                        }

                        wl.FK_EmpText = emp.Name;
                        wl.FK_Dept = emp.FK_Dept;
                        wl.WarningDays = town.HisNode.WarningDays;
                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;
                        wl.Sender = WebUser.No;

                        try
                        {
                            if (town.HisNode.IsFLHL == false)
                            {
                                wl.DirectInsert();
                                this.HisWorkerLists.AddEntity(wl);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                string objsmy = "@";
                foreach (WorkerList wl in this.HisWorkerLists)
                {
                    objsmy += wl.FK_Emp + "@";
                }

                if (rm.Emps != emps || rm.Objs != objsmy)
                {
                    /* 工作人员列表发生了变化 */
                    rm.Emps = emps;
                    rm.Objs = objsmy;

                    string objExts = "";
                    foreach (WorkerList wl in this.HisWorkerLists)
                    {
                        if (Glo.IsShowUserNoOnly)
                            objExts += wl.FK_Emp + "、";
                        else
                            objExts += wl.FK_Emp + "<" + wl.FK_EmpText + ">、";
                    }
                    rm.ObjsExt = objExts;

                    string empExts = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        Emp emp = new Emp(dr[0].ToString());
                        if (rm.Objs.IndexOf(emp.No) != -1)
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += emp.No + "、";
                            else
                                empExts += emp.No + "<" + emp.Name + ">、";
                        }
                        else
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += "<strike><font color=red>" + emp.No + "</font></strike>、";
                            else
                                empExts += "<strike><font color=red>" + emp.No + "<" + emp.Name + "></font></strike>、";
                        }
                    }
                    rm.EmpsExt = empExts;
                    rm.Save();
                }
            }

            if (this.HisWorkerLists.Count == 0)
                throw new Exception("@根据部门产生工作人员出现错误，流程[" + this.HisWorkFlow.HisFlow.Name + "],中节点[" + town.HisNode.Name + "]定义错误,没有找到接受此工作的工作人员.");

            // 求出日志类型。
            ActionType at = ActionType.Forward;
            switch (town.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWork:
                case NodeWorkType.StartWorkFL:
                    at = ActionType.Start;
                    break;
                case NodeWorkType.Work:
                    if (this.HisNode.HisNodeWorkType == NodeWorkType.WorkFL
                        || this.HisNode.HisNodeWorkType == NodeWorkType.WorkFHL)
                        at = ActionType.ForwardFL;
                    else
                        at = ActionType.Forward;
                    break;
                case NodeWorkType.WorkHL:
                    at = ActionType.ForwardHL;
                    break;
                case NodeWorkType.SubThreadWork:
                    at = ActionType.SubFlowForward;
                    break;
                default:
                    break;
            }
            if (this.HisWorkerLists.Count == 1)
            {
                WorkerList wl = this.HisWorkerLists[0] as WorkerList;
                this.AddToTrack(at, wl.FK_Emp, wl.FK_EmpText, wl.FK_Node, wl.FK_NodeText, null);
            }
            else
            {
                string info = "共(" + this.HisWorkerLists.Count+ ")人接收\t\n";
                foreach (WorkerList wl in this.HisWorkerLists)
                {
                    info += wl.FK_Emp + "," + wl.FK_EmpText + "\t\n";
                }
                this.AddToTrack(at, WebUser.No, WebUser.No, town.HisNode.NodeID, town.HisNode.Name, info);
            }
            return this.HisWorkerLists;
        }
        #endregion


        #region 属性

        #region 工作岗位
        private Station _HisStationOfUse = null;
        /// <summary>
        /// 取出当前工作用的岗位.
        /// 这对与多个工作岗位的节点来说的.
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
                        /* 如果这个工作节点只允许一个工作岗位访问，那末这个工作节点就是这个工作节点用的节点。*/
                        _HisStationOfUse = (Station)HisStations[0];
                        return _HisStationOfUse;
                    }

                    // 如果当前节点允许多个工作岗位访问。 找出工作人员的工作岗位集合					
                    Stations mySts = this.HisWork.RecOfEmp.HisStations;
                    if (mySts.Count == 1)
                    {
                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                    }

                    // 取出，节点的工作岗位与工作人员工作岗位的交集合
                    Stations gainSts = (Stations)mySts.GainIntersection(this.HisStations);
                    if (gainSts.Count == 0)
                    {
                        if (this.HisStations.Count == 0)
                            throw new Exception("@您没有为节点[" + this.HisNode.Name + "],设置工作岗位.");

                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                        ///// //throw new Exception("@获取使用工作岗位出现错误,工作人员["+this.HisWork.RecOfEmp.Name+"]的工作没有处理完成,您改变了他的工作岗位.导致工作不能正常的流转下去.");
                    }

                    /* 判断他的主要工作岗位,主要工作岗位优先. */
                    string mainStatNo = this.HisWork.HisRec.No;
                    foreach (Station myst in gainSts)
                    {
                        if (myst.No == mainStatNo)
                        {
                            _HisStationOfUse = myst;
                            return _HisStationOfUse;
                        }
                    }

                    /*扫描交集,外勤优先. */
                    foreach (Station myst in gainSts)
                    {

                    }

                    /* 没有扫描到就, 返回第一个station.*/
                    if (_HisStationOfUse == null)
                        _HisStationOfUse = (Station)gainSts[0];

                }
                return _HisStationOfUse;
            }
        }
        /// <summary>
        /// 工作岗位
        /// </summary>
        public Stations HisStations
        {
            get
            {
                return this.HisNode.HisStations;
            }
        }
        /// <summary>
        /// 返回第一个工作节点
        /// </summary>
        public Station HisStationOfFirst
        {
            get
            {
                return (Station)this.HisNode.HisStations[0];
            }
        }
        #endregion

        #region 判断一人多部门的情况
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
                    //找到全部的部门。
                    Depts depts;
                    if (this.HisWork.Rec == WebUser.No)
                        depts = WebUser.HisDepts;
                    else
                        depts = this.HisWork.RecOfEmp.HisDepts;

                    if (depts.Count == 0)
                    {
                        throw new Exception("您没有给[" + this.HisWork.Rec + "]设置部门。");
                    }

                    if (depts.Count == 1) /* 如果全部的部门只有一个，就返回它。*/
                    {
                        _HisDeptOfUse = (Dept)depts[0];
                        return _HisDeptOfUse;
                    }

                    if (_HisDeptOfUse == null)
                    {
                        /* 如果还没找到，就返回第一个部门。 */
                        _HisDeptOfUse = depts[0] as Dept;
                    }
                }
                return _HisDeptOfUse;
            }
        }
        #endregion

        #endregion

        #region 条件
        private Conds _HisNodeCompleteConditions = null;
        /// <summary>
        /// 节点完成任务的条件
        /// 条件与条件之间是or 的关系, 就是说,如果任何一个条件满足,这个工作人员在这个节点上的任务就完成了.
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
        /// 他的完成任务的条件,此节点是完成任务的条件集合
        /// 条件与条件之间是or 的关系, 就是说,如果任何一个条件满足,这个任务就完成了.
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

        #region 关于质量考核
        ///// <summary>
        ///// 得到以前的已经完成的工作节点.
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

        #region 流程公共方法
        /// <summary>
        /// 检查全局的工作流程是不是完成任务
        /// 如果完成了，就返回不做任何处理。
        /// 如果符合流程完成的条件，就返回相应的信息。
        /// 如果不符合流程的完成条件，return null;  
        /// </summary>
        public string CheckGlobalCompleteCondition()
        {
            if (this.HisWorkFlow.IsComplete)  // 如果全局的工作已经完成.
                return this.ToE("FlowOver", "当前的任务已经完成"); //当前的任务已经完成

            #region 检查全局的完成条件,由于不经常用到,所以就暂时删除.
            /*
			GlobalCompleteConditions gcc = new GlobalCompleteConditions(this.HisNode.FK_Flow,this.HisWork.OID);
			if (gcc.IsOneOfConditionPassed)
			{
				this.HisWorkFlow.DoFlowOver();
				return "@工作流程["+this.HisNode.HisFlow.Name+"]执行过程中，在节点["+this.HisNode.Name+"]符合流程完成条件，"+gcc.GetOneOfConditionPassed.ConditionDesc+"，工作正常结束！";
			}
			*/
            #endregion

            return null;
        }
        private Flow _HisFlow = null;
        public Flow HisFlow
        {
            get
            {
                if (_HisFlow == null)
                    _HisFlow = this.HisNode.HisFlow;
                return _HisFlow;
            }
        }
        public Node JumpToNode = null;
        public string JumpToEmp = null;
        /// <summary>
        /// 解决流程回滚的问题
        /// </summary>
        /// <param name="TransferPC"></param>
        /// <param name="ByTransfered"></param>
        /// <returns></returns>
        public string AfterNodeSave()
        {
            DBAccess.DoTransactionBegin();
            DateTime dt = DateTime.Now;
            this.HisWork.Rec = Web.WebUser.No;
            this.WorkID = this.HisWork.OID;
            string msg = this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendWhen, this.HisWork);
            // 调用发送前的接口。
            try
            {
                #region 处理调用日志.
                if (this.HisNode.IsStartNode)
                {
                    /*如果是开始流程判断是不是被吊起的流程，如果是就要向父流程写日志。*/
                    if (SystemConfig.IsBSsystem)
                    {
                        string fk_nodeFrom = System.Web.HttpContext.Current.Request.QueryString["FromNode"];
                        if (string.IsNullOrEmpty(fk_nodeFrom) == false)
                        {
                            Node ndFrom = new Node(int.Parse(fk_nodeFrom));
                            string fromWorkID = System.Web.HttpContext.Current.Request.QueryString["FromWorkID"];

                            GenerWorkFlow gwfP = new GenerWorkFlow(Int64.Parse(fromWorkID));

                            //记录当前流程被调起。
                            this.AddToTrack(ActionType.StartSubFlow, WebUser.No,
                                WebUser.Name, ndFrom.NodeID, ndFrom.FlowName + "\t\n" + ndFrom.Name, "被父流程(" + ndFrom.FlowName + ":" + gwfP.Title + ")唤起.");

                            //记录父流程被调起。
                            Track tkParent = new Track();
                            tkParent.WorkID = Int64.Parse(fromWorkID);
                            tkParent.RDT = DataType.CurrentDataTimess;
                            tkParent.HisActionType = ActionType.CallSubFlow;
                            tkParent.EmpFrom = WebUser.No;
                            tkParent.EmpFromT = WebUser.Name;

                            tkParent.NDTo = this.HisNode.NodeID;
                            tkParent.NDToT = this.HisNode.FlowName + " \t\n " + this.HisNode.Name;

                            tkParent.FK_Flow = ndFrom.FK_Flow;

                            tkParent.NDFrom = ndFrom.NodeID;
                            tkParent.NDFromT = ndFrom.Name;

                            tkParent.EmpTo = WebUser.No;
                            tkParent.EmpToT = WebUser.Name;
                            tkParent.Msg = "<a href=WFRpt.aspx?FK_Flow=" + this.HisNode.FK_Flow + "&WorkID=" + this.HisWork.OID + " target=_b >调起子流程(" + this.HisNode.FlowName + ")</a>";
                            tkParent.MyPK = tkParent.WorkID + "_" + tkParent.FID + "_" + (int)tkParent.HisActionType + "_" + tkParent.NDFrom + "_" + DateTime.Now.ToString("yyMMddhhmmss");
                            tkParent.Insert();
                        }
                    }
                }
                #endregion 处理调用日志.

                msg += AfterNodeSave_Do();
                DBAccess.DoTransactionCommit(); //提交事务.

                try
                {
                    // 调起发送成功后的事务。
                    msg += this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendSuccess, this.HisWork);
                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }

                string msgOfSend = this.HisNode.TurnToDealDoc;
                if (msgOfSend.Length > 3)
                {
                    if (msgOfSend.Contains("@"))
                    {
                        Attrs attrs = this.HisWork.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            msgOfSend = msgOfSend.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                        }
                    }
                    return msgOfSend;
                }

                // 如果需要跳转.
                if (town != null)
                {
                    if (town.HisNode.HisDeliveryWay == DeliveryWay.ByPreviousOperSkip)
                        return town.AfterNodeSave();
                }

                return msg;
            }
            catch (Exception ex)
            {
                this.WhenTranscactionRollbackError(ex);
                DBAccess.DoTransactionRollback();
                throw ex;
            }
        }
        private void WhenTranscactionRollbackError(Exception ex)
        {
            /*在提交错误的情况下，回滚数据。*/
            try
            {
                // 把工作的状态设置回来。
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                // 把流程的状态设置回来。
                GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
                if (gwf.WFState != 0 || gwf.FK_Node != this.HisNode.NodeID)
                {
                    /* 如果这两项其中有一项有变化。*/
                    gwf.FK_Node = this.HisNode.NodeID;
                    gwf.NodeName = this.HisNode.Name;
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
                        // 把开始节点的装态设置回来。
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
                    mwk.DirectDelete();
                }
                this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendError, this.HisWork);
            }
            catch (Exception ex1)
            {
                if (this.rptGe != null)
                    this.rptGe.CheckPhysicsTable();
                throw new Exception(ex.Message + "@回滚发送失败数据出现错误：" + ex1.Message + "@有可能系统已经自动修复错误，请您在重新执行一次。");
            }
        }
        #region 用户到的变量
        public WorkerLists HisWorkerLists = null;
        public GenerWorkFlow _HisGenerWorkFlow;
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                if (_HisGenerWorkFlow == null)
                    _HisGenerWorkFlow = new GenerWorkFlow(this.WorkID);
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
        /// <summary>
        /// 启动分流
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        public string FeiLiuStartUp(Node toNode)
        {
            // 发起.
            Work wk = toNode.HisWork;
            WorkNode town = new WorkNode(wk, toNode);

            // 产生下一步骤要执行的人员.
            WorkerLists gwls = this.GenerWorkerLists(town);
            this.AddIntoWacthDog(gwls);  //@加入消息集合里。

            //清除以前的数据，比如两次发送。
            wk.Delete(WorkAttr.FID, this.HisWork.OID);

            // 判断分流的次数.是不是历史记录里面有分流。
            bool IsHaveFH = false;
            if (DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_GenerWorkerlist WHERE FID=" + this.HisWork.OID) != 0)
                IsHaveFH = true;

            string msg = "";
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                                            this.WorkID.ToString());

            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                                           this.WorkID.ToString());

            MapDtls dtlsFrom = new MapDtls("ND" + this.HisNode.NodeID);
            if (dtlsFrom.Count > 1)
            {
                foreach (MapDtl d in dtlsFrom)
                {
                    d.HisGEDtls_temp = null;
                }
            }
            MapDtls dtlsTo = null;
            if (dtlsFrom.Count >= 1)
                dtlsTo = new MapDtls("ND" + toNode.NodeID);

            // 按照部门分组，分别启动流程。
            switch (this.HisNode.HisFLRole)
            {
                case FLRole.ByStation:
                case FLRole.ByDept:
                case FLRole.ByEmp:
                    foreach (WorkerList wl in gwls)
                    {
                        Work mywk = toNode.HisWork;
                        mywk.Copy(this.rptGe);
                        mywk.Copy(this.HisWork);  //复制过来信息。

                        bool isHaveEmp = false;
                        if (IsHaveFH)
                        {
                            /* 如果曾经走过分流合流，就找到同一个人员同一个FID下的OID ，做这当前线程的ID。*/
                            DataTable dt = DBAccess.RunSQLReturnTable("SELECT WorkID,FK_Node FROM WF_GenerWorkerlist WHERE FID=" + this.WorkID + " AND FK_Emp='" + wl.FK_Emp + "' ORDER BY RDT DESC");
                            if (dt.Rows.Count == 0)
                            {
                                /*没有发现，就说明以前分流节点中没有这个人的分流信息 */
                                mywk.OID = DBAccess.GenerOID();
                            }
                            else
                            {
                                int workid_old = (int)dt.Rows[0][0];
                                int fk_Node_nearly = (int)dt.Rows[0][1];
                                Node nd_nearly = new Node(fk_Node_nearly);
                                Work nd_nearly_work = nd_nearly.HisWork;
                                nd_nearly_work.OID = workid_old;
                                if (nd_nearly_work.RetrieveFromDBSources() != 0)
                                {
                                    mywk.Copy(nd_nearly_work);
                                    mywk.OID = workid_old;
                                }
                                else
                                {
                                    mywk.OID = DBAccess.GenerOID();
                                }
                                isHaveEmp = true;
                            }
                        }
                        else
                        {
                            mywk.OID = DBAccess.GenerOID();  //BP.DA.DBAccess.GenerOID();
                        }
                        mywk.FID = this.HisWork.FID;
                        mywk.Rec = wl.FK_Emp;
                        mywk.Emps = wl.FK_Emp;
                        mywk.BeforeSave();
                        mywk.InsertAsOID(mywk.OID);

                        #region  复制附件信息
                        if (athDBs.Count > 0)
                        {
                            /* 说明当前节点有附件数据 */
                            athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + toNode.NodeID,
                                FrmAttachmentDBAttr.RefPKVal, mywk.OID);
                            int i = 0;
                            foreach (FrmAttachmentDB athDB in athDBs)
                            {
                                i++;
                                FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                athDB_N.Copy(athDB);
                                athDB_N.FK_MapData = "ND" + toNode.NodeID;
                                athDB_N.MyPK = mywk.OID + "_" + mywk.FID + "_" + toNode.NodeID + "_" + i.ToString();
                                athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                    "ND" + toNode.NodeID);
                                athDB_N.RefPKVal = mywk.OID.ToString();
                                athDB_N.DirectInsert();
                            }
                        }
                        #endregion  复制附件信息

                        #region  复制签名信息
                        if (eleDBs.Count > 0)
                        {
                            /* 说明当前节点有附件数据 */
                            eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + toNode.NodeID,
                                FrmEleDBAttr.RefPKVal, mywk.OID);
                            int i = 0;
                            foreach (FrmEleDB eleDB in eleDBs)
                            {
                                i++;
                                FrmEleDB athDB_N = new FrmEleDB();
                                athDB_N.Copy(eleDB);
                                athDB_N.FK_MapData = "ND" + toNode.NodeID;
                                athDB_N.RefPKVal = mywk.OID.ToString();
                                athDB_N.GenerPKVal();
                                athDB_N.DirectInsert();
                            }
                        }
                        #endregion  复制附件信息


                        #region  复制明细表信息.
                        if (dtlsFrom.Count > 0)
                        {
                            int i = -1;
                            foreach (Sys.MapDtl dtl in dtlsFrom)
                            {
                                i++;
                                if (dtlsTo.Count <= i)
                                    continue;
                                Sys.MapDtl toDtl = (Sys.MapDtl)dtlsTo[i];
                                if (toDtl.IsCopyNDData == false)
                                    continue;

                                //获取明细数据。
                                GEDtls gedtls = null;
                                if (dtl.HisGEDtls_temp == null)
                                {
                                    gedtls = new GEDtls(dtl.No);
                                    QueryObject qo = null;
                                    qo = new QueryObject(gedtls);
                                    switch (dtl.DtlOpenType)
                                    {
                                        case DtlOpenType.ForEmp:
                                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                            break;
                                        case DtlOpenType.ForWorkID:
                                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                            break;
                                        case DtlOpenType.ForFID:
                                            qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                            break;
                                    }
                                    qo.DoQuery();
                                    dtl.HisGEDtls_temp = gedtls;
                                }
                                gedtls = dtl.HisGEDtls_temp;

                                int unPass = 0;
                                DBAccess.RunSQL("DELETE " + toDtl.PTable + " WHERE RefPK=" + mywk.OID);
                                foreach (GEDtl gedtl in gedtls)
                                {
                                    BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                                    dtCopy.Copy(gedtl);
                                    dtCopy.FK_MapDtl = toDtl.No;
                                    dtCopy.RefPK = mywk.OID.ToString();
                                    dtCopy.OID = 0;
                                    dtCopy.Insert();

                                    //dtCopy.InsertAsOID(gedtl.OID);
                                    //dtCopy.InsertAsOID(gedtl.OID);

                                    #region  复制明细表单条 - 附件信息 - M2M- M2MM
                                    if (toDtl.IsEnableAthM)
                                    {
                                        /*如果启用了多附件,就复制这条明细数据的附件信息。*/
                                        athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
                                        if (athDBs.Count > 0)
                                        {
                                            i = 0;
                                            foreach (FrmAttachmentDB athDB in athDBs)
                                            {
                                                i++;
                                                FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                                athDB_N.Copy(athDB);
                                                athDB_N.FK_MapData = toDtl.No;
                                                athDB_N.MyPK = toDtl.No + "_" + dtCopy.OID + "_" + i.ToString();
                                                athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                                    "ND" + toNode.NodeID);
                                                athDB_N.RefPKVal = dtCopy.OID.ToString();
                                                athDB_N.DirectInsert();
                                            }
                                        }
                                    }
                                    if (toDtl.IsEnableM2M || toDtl.IsEnableM2MM)
                                    {
                                        /*如果启用了m2m */
                                        M2Ms m2ms = new M2Ms(dtl.No, gedtl.OID);
                                        if (m2ms.Count > 0)
                                        {
                                            i = 0;
                                            foreach (M2M m2m in m2ms)
                                            {
                                                i++;
                                                M2M m2m_N = new M2M();
                                                m2m_N.Copy(m2m);
                                                m2m_N.FK_MapData = toDtl.No;
                                                m2m_N.MyPK = toDtl.No + "_" + m2m.M2MNo + "_" + gedtl.ToString() + "_" + m2m.DtlObj;
                                                m2m_N.EnOID = gedtl.OID;
                                                m2m_N.DirectInsert();
                                            }
                                        }
                                    }
                                    #endregion  复制明细表单条 - 附件信息

                                }
                            }
                        }
                        #endregion  复制附件信息

                        // 产生工作的信息。
                        GenerWorkFlow gwf = new GenerWorkFlow();
                        gwf.FID = this.WorkID;
                        gwf.WorkID = mywk.OID;
                        gwf.Title = this.GenerTitle(this.HisWork);

                        //if (BP.WF.Glo.IsShowUserNoOnly == false)
                        //    gwf.Title = WebUser.No + "," + WebUser.Name + " 发起：" + toNode.Name + "(分流节点)";
                        //else
                        //    gwf.Title = WebUser.No + " 发起：" + toNode.Name + "(分流节点)";

                        gwf.WFState = 0;
                        gwf.RDT = DataType.CurrentDataTime;
                        gwf.Rec = Web.WebUser.No;
                        gwf.RecName = Web.WebUser.Name;
                        gwf.FK_Flow = toNode.FK_Flow;
                        gwf.FlowName = toNode.FlowName;
                        gwf.FID = this.WorkID;
                        gwf.FK_FlowSort = toNode.HisFlow.FK_FlowSort;
                        gwf.FK_Node = toNode.NodeID;
                        gwf.NodeName = toNode.Name;
                        gwf.FK_Dept = wl.FK_Dept;
                        gwf.DeptName = wl.FK_DeptT;
                        try
                        {
                            gwf.DirectInsert();
                        }
                        catch
                        {
                            gwf.DirectUpdate();
                        }
                        DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET WorkID=" + mywk.OID + ",FID=" + this.WorkID + " WHERE FK_Emp='" + wl.FK_Emp + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + toNode.NodeID);
                    }
                    break;
                default:
                    throw new Exception("没有处理的类型：" + this.HisNode.HisFLRole.ToString());
            }

            // return 。系统自动下达给如下几位同事。";

            string info = this.ToE("WN28", "@分流节点:{0}已经发起。@任务自动下达给{1}如下{2}位同事,{3}.");

            msg += string.Format(info, toNode.Name,
                this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(),
                this._RememberMe.EmpsExt);

            // 如果是开始节点，就可以允许选择接受人。
            if (this.HisNode.IsStartNode)
            {
                if (gwls.Count >= 2)
                    msg += "@<img src='" + this.VirPath + "/WF/Img/AllotTask.gif' border=0 /><a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&FID=" + this.WorkID + "&NodeID=" + toNode.NodeID + "')\" >" + this.ToE("W29", "修改接受对象") + "</a>.";
            }

            if (this.HisNode.IsStartNode)
            {
                msg += "@<a href='" + this.VirPath + "/" + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>， <a href='" + this.VirPath + "/" + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + toNode.FK_Flow + "'><img src=./Img/New.gif border=0/>" + this.ToE("NewFlow", "新建流程") + "</a>。";
            }
            else
            {
                msg += "@<a href='" + this.VirPath + "/" + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>。";
                //  msg += "@<a href=\"javascript:WinOpen('" + this.VirPath + "/" + this.AppType + "/" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>。";
            }

            msg += this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID);

            //更新节点状态。
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);

            msg += "@<a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >工作报告</a>";
            return msg;
            // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
            // return this.ToEP3("TaskAutoSendTo", "@任务自动下达给{0}如下{1}位同事,{2}.", this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
        }
        /// <summary>
        /// 分流发起
        /// </summary>
        /// <returns></returns>
        public string FeiLiuStartUp()
        {
            #region GenerFH
            GenerFH fh = new GenerFH();
            fh.FID = this.WorkID;
            if (this.HisNode.IsStartNode || fh.IsExits == false)
            {
                try
                {
                    fh.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                }
                catch (Exception ex)
                {
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + this.HisNode.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "Title";
                    int i = attr.Retrieve(MapAttrAttr.FK_MapData, attr.FK_MapData, MapAttrAttr.KeyOfEn, attr.KeyOfEn);
                    if (i == 0)
                    {
                        attr.KeyOfEn = "Title";
                        attr.Name = BP.Sys.Language.GetValByUserLang("Title", "标题"); // "流程标题";
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = true;
                        attr.UIIsEnable = true;
                        attr.UIIsLine = true;
                        attr.MinLen = 0;
                        attr.MaxLen = 200;
                        attr.IDX = -100;
                        attr.Insert();
                    }
                    fh.Title = WebUser.No + "-" + WebUser.Name + " @ " + DataType.CurrentDataTime + " ";
                }
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
            #endregion GenerFH

            string msg = "";
            Nodes toNodes = this.HisNode.HisToNodes;

            // 如果只有一个转向节点, 就不用判断条件了,直接转向他.
            if (toNodes.Count == 1)
                return FeiLiuStartUp((Node)toNodes[0]);

            Conds dcsAll = new Conds();
            dcsAll.Retrieve(CondAttr.NodeID, this.HisNode.NodeID, CondAttr.PRI);
            if (dcsAll.Count == 0)
            {
                /*如果没有设置方向条件就全部通过*/
                return msg + StartNextNode(toNodes);
            }

            #region 获取能够通过的节点集合，如果没有设置方向条件就默认通过.
            Nodes myNodes = new Nodes();
            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in toNodes)
            {
                Conds dcs = new Conds();
                foreach (Cond dc in dcsAll)
                {
                    if (dc.ToNodeID != nd.NodeID)
                        continue;

                    dc.WorkID = this.HisWork.OID;
                    dcs.AddEntity(dc);
                }

                if (dcs.Count == 0)
                {
                    // 如果没有设置方向条件，就默认通过的.
                    myNodes.AddEntity(nd);
                    continue;
                    // throw new Exception(string.Format(this.ToE("WN10", "@定义节点的方向条件错误:没有给从{0}节点到{1},定义转向条件."), this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                }

                if (dcs.IsPass) // 如果多个转向条件中有一个成立.
                {
                    myNodes.AddEntity(nd);
                    continue;
                    //numOfWay++;
                    //toNodeId = nd.NodeID;
                    //msg = FeiLiuStartUp(nd);
                }
            }
            #endregion 获取能够通过的节点集合，如果没有设置方向条件就默认通过.

            if (myNodes.Count == 0)
            {
                throw new Exception(string.Format(this.ToE("WN10_1",
                    "@定义节点的方向条件错误:没有给从{0}节点到其它节点,定义转向条件."), this.HisNode.NodeID + this.HisNode.Name));
            }
            else
            {
                return msg + StartNextNode(myNodes);
            }
        }
        #endregion
        public string GenerTitle(Work wk)
        {
            // 生成标题.
            Attr myattr = wk.EnMap.Attrs.GetAttrByKey("Title");
            if (myattr == null)
                myattr = wk.EnMap.Attrs.GetAttrByKey("Title");

            string titleRole = "";
            if (myattr != null)
                titleRole = myattr.DefaultVal.ToString();

            if (string.IsNullOrEmpty(titleRole) || titleRole.Contains("@") == false)
                titleRole = "@WebUser.FK_DeptName-@WebUser.No,@WebUser.Name在@RDT发起.";

            titleRole = titleRole.Replace("@WebUser.No", wk.Rec);
            titleRole = titleRole.Replace("@WebUser.Name", wk.RecText);
            titleRole = titleRole.Replace("@WebUser.FK_DeptName", wk.RecOfEmp.FK_DeptText);
            titleRole = titleRole.Replace("@RDT", wk.RDT);
            if (titleRole.Contains("@"))
            {
                foreach (Attr attr in wk.EnMap.Attrs)
                {
                    if (titleRole.Contains("@") == false)
                        break;
                    if (attr.IsFKorEnum)
                        continue;
                    titleRole = titleRole.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
                }
            }
            wk.SetValByKey("Title", titleRole);
            return titleRole;
        }
        public GEEntity rptGe = null;
        private void InitStartWorkData()
        {
            /* 产生开始工作流程记录. */
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = this.HisWork.OID;

           //this.HisWork.SetValByKey(StartWorkAttr.Title, title);

            gwf.Title = this.GenerTitle(this.HisWork);
            gwf.WFState = 0;
            gwf.RDT = this.HisWork.RDT;
            gwf.Rec = Web.WebUser.No;
            gwf.RecName = Web.WebUser.Name;

            gwf.FK_Flow = this.HisNode.FK_Flow;
            gwf.FlowName = this.HisNode.FlowName;

            gwf.FK_FlowSort = this.HisNode.HisFlow.FK_FlowSort;
            
            gwf.FK_Node = this.HisNode.NodeID;
            gwf.NodeName = this.HisNode.Name;

            //  gwf.FK_Station = this.HisStationOfUse.No;
            gwf.FK_Dept = this.HisWork.RecOfEmp.FK_Dept;
            gwf.DeptName = this.HisWork.RecOfEmp.FK_DeptText;

            try
            {
                gwf.DirectInsert();
            }
            catch
            {
                gwf.DirectUpdate();
            }

            StartWork sw = (StartWork)this.HisWork;

            #region 设置  HisGenerWorkFlow

            this.HisGenerWorkFlow = gwf;

            //#warning 去掉这个工作没有想到如何去写？
            // 记录到流程统计分析中去。
            //this.HisCHOfFlow = new CHOfFlow();
            //this.HisCHOfFlow.Copy(gwf);
            //this.HisCHOfFlow.WorkID = this.HisWork.OID;
            //this.HisCHOfFlow.WFState = (int)WFState.Runing;

            ///* 说明没有这个记录 */
            //this.HisCHOfFlow.FK_Flow = this.HisNode.FK_Flow;
            //this.HisCHOfFlow.WFState = 0;
            //this.HisCHOfFlow.Title = gwf.Title;
            //this.HisCHOfFlow.FK_Emp = this.HisWork.Rec.ToString();
            //this.HisCHOfFlow.RDT = this.HisWork.RDT;
            //this.HisCHOfFlow.CDT = DataType.CurrentDataTime;
            //this.HisCHOfFlow.SpanDays = 0;
            //this.HisCHOfFlow.FK_Dept = this.HisDeptOfUse.No;
            //this.HisCHOfFlow.FK_NY = DataType.CurrentYearMonth;
            //try
            //{
            //    this.HisCHOfFlow.Insert();
            //}
            //catch
            //{
            //    this.HisCHOfFlow.Update();
            //}
            #endregion HisCHOfFlow

            #region  产生开始工作者,能够执行他们的人员.
            WorkerList wl = new WorkerList();
            wl.WorkID = this.HisWork.OID;
            wl.FK_Node = this.HisNode.NodeID;
            wl.FK_NodeText = this.HisNode.Name;

            wl.FK_Emp = WebUser.No;
            wl.FK_EmpText = WebUser.Name;

            wl.FK_Flow = this.HisNode.FK_Flow;
            wl.FK_Dept = WebUser.FK_Dept;

            // wl.UseDept = this.HisDeptOfUse.No;
            // wl.UseStation = this.HisStationOfUse.No;

            wl.WarningDays = this.HisNode.WarningDays;
            wl.SDT = DataType.CurrentData;
            wl.DTOfWarning = DataType.CurrentData;
            wl.RDT = DataType.CurrentDataTime;
            try
            {
                wl.Insert(); // 先插入，后更新。
            }
            catch (Exception ex)
            {
                //  throw ex;
                wl.Update();
            }
            #endregion
        }
        /// <summary>
        /// 执行数据copy.
        /// </summary>
        /// <param name="fromWK"></param>
        private void DoCopyRptWork(Work fromWK)
        {
            foreach (Attr attr in fromWK.EnMap.Attrs)
            {
                switch (attr.Key)
                {
                    case BP.WF.GERptAttr.FK_NY:
                    case BP.WF.GERptAttr.FK_Dept:
                    case BP.WF.GERptAttr.FlowDaySpan:
                    case BP.WF.GERptAttr.FlowEmps:
                    case BP.WF.GERptAttr.FlowEnder:
                    case BP.WF.GERptAttr.FlowEnderRDT:
                    case BP.WF.GERptAttr.FlowStarter:
                    case BP.WF.GERptAttr.Title:
                        continue;
                    default:
                        break;
                }

                object obj = fromWK.GetValByKey(attr.Key);
                if (obj == null)
                    continue;
                this.rptGe.SetValByKey(attr.Key, obj);
            }
            if (this.HisNode.IsStartNode)
                this.rptGe.SetValByKey("Title", fromWK.GetValByKey("Title"));
        }
        /// <summary>
        /// 在流程节点保存后的操作.
        /// 1, 判断节点的任务是不是完成,如果完成,就设置节点的完成状态.
        /// 2, 判断是不是符合工作流完成任务任务的条件, 如果完成,就设置,工作流程任务完成. return ;
        /// 3, 判断节点的方向.
        /// 1, 一个一个的判断此节点的方向, 满足就启动节点工作.
        /// 2, 如果没有任何一个节点的工作.那末就抛出异常, 流程的节点方向条件定义错误. .
        /// </summary>
        /// <param name="TransferPC">是否要执行获取外部信息工组</param>
        /// <param name="ByTransfered">是不是被调用的,只用在开始节点,并且是外部调用的自动启动的流程,如果设置为true, 流程在当前的节点完成之后就不做下一步骤的工作,如果设置为false, 就做下一步工作.</param>
        /// <returns>执行后的内容</returns>
        private string AfterNodeSave_Do()
        {
            #region 把数据放到报表表里面.
            if (this.HisNode.IsStartNode)
                this.InitStartWorkData();

            this.rptGe = this.HisNode.HisFlow.HisFlowData;
            this.rptGe.SetValByKey("OID", this.WorkID);
            if (rptGe.RetrieveFromDBSources() == 0)
            {
                rptGe.SetValByKey("OID", this.WorkID);
                this.DoCopyRptWork(this.HisWork);

                rptGe.SetValByKey(GERptAttr.FlowEmps, "@" + WebUser.No + "," + WebUser.Name);
                rptGe.SetValByKey(GERptAttr.FlowStarter, WebUser.No);
                rptGe.SetValByKey(GERptAttr.FlowStartRDT, DataType.CurrentDataTime);
                rptGe.SetValByKey(GERptAttr.WFState, 0);
                rptGe.SetValByKey(GERptAttr.FK_NY, DataType.CurrentYearMonth);
                rptGe.SetValByKey(GERptAttr.FK_Dept, WebUser.FK_Dept);
                rptGe.Insert();
            }
            else
            {
                foreach (Attr attr in this.HisWork.EnMap.Attrs)
                {
                    switch (attr.Key)
                    {
                        case StartWorkAttr.FK_Dept:
                        case StartWorkAttr.FID:
                        case StartWorkAttr.CDT:
                        case StartWorkAttr.RDT:
                        case StartWorkAttr.Rec:
                        case StartWorkAttr.Sender:
                        case StartWorkAttr.NodeState:
                        case StartWorkAttr.OID:
                        case StartWorkAttr.Title:
                            continue;
                        default:
                            break;
                    }
                    object obj = this.HisWork.GetValByKey(attr.Key);
                    if (obj == null)
                        continue;
                    rptGe.SetValByKey(attr.Key, obj);
                }

                try
                {
                    string str = rptGe.GetValStrByKey(GERptAttr.FlowEmps);
                    if (str.Contains("@" + WebUser.No + "," + WebUser.Name) == false)
                        rptGe.SetValByKey(GERptAttr.FlowEmps, str + "@" + WebUser.No + "," + WebUser.Name);
                }
                catch
                {
                    this.HisNode.HisFlow.DoCheck();
                }

                if (this.HisNode.IsEndNode)
                {
                    rptGe.SetValByKey(GERptAttr.WFState, 1); // 更新状态。
                    rptGe.SetValByKey(GERptAttr.FlowEnder, WebUser.No);
                    rptGe.SetValByKey(GERptAttr.FlowEnderRDT, DataType.CurrentDataTime);
                    rptGe.SetValByKey(GERptAttr.FlowDaySpan, DataType.GetSpanDays(this.rptGe.GetValStringByKey(GERptAttr.FlowStartRDT), DataType.CurrentDataTime));
                }
                rptGe.DirectUpdate();
            }
            #endregion

            #region 根据当前节点类型不同处理不同的模式。
            string msg = "";
            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWorkFL:
                case NodeWorkType.WorkFL:  /* 启动分流 */
                    this.HisWork.FID = this.HisWork.OID;
                    msg = this.FeiLiuStartUp();
                    break;
                case NodeWorkType.WorkFHL:   /* 启动分流 */
                    this.HisWork.FID = this.HisWork.OID;
                    msg = this.FeiLiuStartUp();
                    break;
                case NodeWorkType.WorkHL:   /* 当前工作节点是合流 */
                    msg = this.StartupNewNodeWork();
                    msg += this.DoSetThisWorkOver(); // 执行此工作结束。
                    break;
                default: /* 其他的点的逻辑 */
                    msg = this.StartupNewNodeWork();
                    msg += this.DoSetThisWorkOver();
                    break;
            }
            #endregion 根据当前节点类型不同处理不同的模式。

            #region 处理收听
            if (Glo.IsEnableSysMessage)
            {
                Listens lts = new Listens();
                lts.RetrieveByLike(ListenAttr.Nodes, "%" + this.HisNode.NodeID + "%");

                foreach (Listen lt in lts)
                {
                    string sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE IsEnable=1 AND IsPass=1 AND FK_Node=" + lt.FK_Node + " AND WorkID=" + this.WorkID;
                    DataTable dtRem = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    foreach (DataRow dr in dtRem.Rows)
                    {
                        string fk_emp = dr["FK_Emp"] as string;
                        Port.WFEmp emp = new BP.WF.Port.WFEmp(fk_emp);
                        if (emp.HisAlertWay == BP.WF.Port.AlertWay.None)
                        {
                            // msg += "@<font color=red>此信息无法发送给：" + emp.Name + "，因为他关闭了信息提醒，给他打电话："+emp.Tel+"</font>";
                            msg += "@<font color=red>" + this.ToEP2("WN25", "此信息无法发送给：{0}，因为他关闭了信息提醒，给他打电话：{1}。", emp.Name, emp.Tel) + "</font>";
                            continue;
                        }
                        else
                        {
                            // msg += "@您的操作已经通过（<font color=green><b>" + emp.HisAlertWayT + "</b></font>）的方式发送给：" + emp.Name;
                            msg += this.ToEP2("WN26", "@您的操作已经通过（<font color=green><b>{0}</b></font>）的方式发送给：{1}", emp.HisAlertWayT, emp.Name);
                        }

                        string title = lt.Title.Clone() as string;

                        title = title.Replace("@WebUser.No", WebUser.No);
                        title = title.Replace("@WebUser.Name", WebUser.Name);
                        title = title.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
                        title = title.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);

                        string doc = lt.Doc.Clone() as string;
                        doc = doc.Replace("@WebUser.No", WebUser.No);
                        doc = doc.Replace("@WebUser.Name", WebUser.Name);
                        doc = doc.Replace("@WebUser.FK_Dept", WebUser.FK_Dept);
                        doc = doc.Replace("@WebUser.FK_DeptName", WebUser.FK_DeptName);

                        Attrs attrs = this.rptGe.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            title = title.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                            doc = doc.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                        }

                        BP.TA.SMS.AddMsg(lt.OID + "_" + this.WorkID, fk_emp, emp.HisAlertWay, emp.Tel, title, emp.Email, title, doc);
                    }
                }
            }
            #endregion

            #region 生成单据
            BillTemplates reffunc = this.HisNode.HisBillTemplates;
            if (reffunc.Count > 0)
            {
                #region 生成单据信息
                Int64 workid = this.HisWork.OID;
                int nodeId = this.HisNode.NodeID;
                string flowNo = this.HisNode.FK_Flow;
                #endregion

                DateTime dt = DateTime.Now;
                Flow fl = new Flow(this.HisNode.FK_Flow);
                string year = dt.Year.ToString();
                string billInfo = "";
                foreach (BillTemplate func in reffunc)
                {
                    string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                    BP.Rpt.RTF.RTFEngine rtf = new BP.Rpt.RTF.RTFEngine();

                    Works works;
                    string[] paths;
                    string path;
                    try
                    {
                        #region 生成单据
                        rtf.HisEns.Clear();
                        rtf.EnsDataDtls.Clear();
                        if (func.NodeID == 0)
                        {
                            // 判断是否是受理回执
                            //if (fl.DateLit == 0)
                            //    continue;
                            //HisCHOfFlow.DateLitFrom = DateTime.Now.AddDays(fl.DateLit).ToString(DataType.SysDataFormat);
                            //HisCHOfFlow.DateLitTo = DateTime.Now.AddDays(fl.DateLit + 10).ToString(DataType.SysDataFormat);
                            //HisCHOfFlow.Update();
                            //   rtf.AddEn(HisCHOfFlow);
                        }
                        else
                        {
                            WorkNodes wns = new WorkNodes();
                            if (this.HisNode.HisFNType == FNType.River)
                                wns.GenerByFID(this.HisNode.HisFlow, this.WorkID);
                            else
                                wns.GenerByWorkID(this.HisNode.HisFlow, this.WorkID);

                            rtf.HisGEEntity = rptGe;
                            works = wns.GetWorks;
                            foreach (Work wk in works)
                            {
                                if (wk.OID == 0)
                                    continue;

                                rtf.AddEn(wk);
                                rtf.ensStrs += ".ND" + wk.NodeID;
                                ArrayList al = wk.GetDtlsDatasOfArrayList();
                                foreach (Entities ens in al)
                                    rtf.AddDtlEns(ens);
                            }
                            //    w = new BP.Port.WordNo(WebUser.FK_DeptOfXJ);
                            // rtf.AddEn(w);
                        }

                        paths = file.Split('_');
                        path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";

                        string billUrl = this.VirPath + "/DataUser/Bill/" + path + file;

                        if (func.HisBillFileType == BillFileType.PDF)
                        {
                            billUrl = billUrl.Replace(".doc", ".pdf");
                            billInfo += "<img src='" + this.VirPath + "/Images/FileType/PDF.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                        }
                        else
                        {
                            billInfo += "<img src='" + this.VirPath + "/Images/FileType/doc.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                        }

                        //  string  = BP.SystemConfig.GetConfig("FtpPath") + file;
                        path = BP.WF.Glo.FlowFileBill + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";

                        if (System.IO.Directory.Exists(path) == false)
                            System.IO.Directory.CreateDirectory(path);

                        rtf.MakeDoc(func.Url + ".rtf",
                            path, file, func.ReplaceVal, false);
                        #endregion

                        #region 转化成pdf.
                        if (func.HisBillFileType == BillFileType.PDF)
                        {
                            string rtfPath = path + file;
                            string pdfPath = rtfPath.Replace(".doc", ".pdf");
                            //  string pdfPath = path + func.Url + ".pdf";
                            try
                            {
                                Glo.Rtf2PDF(rtfPath, pdfPath);
                            }
                            catch (Exception ex)
                            {
                                msg += ex.Message;
                            }
                        }
                        #endregion

                        #region 保存单据
                        Bill bill = new Bill();
                        bill.MyPK = this.HisWork.FID + "_" + this.HisWork.OID + "_" + this.HisNode.NodeID + "_" + func.No;
                        bill.FID = this.HisWork.FID;
                        bill.WorkID = this.HisWork.OID;
                        bill.FK_Node = this.HisNode.NodeID;
                        //  bill.FK_Bill = func.No;
                        bill.FK_Dept = WebUser.FK_Dept;
                        bill.FK_Emp = WebUser.No;
                        bill.Url = billUrl;
                        bill.RDT = DataType.CurrentDataTime;
                        bill.FullPath = path + file;
                        bill.FK_NY = DataType.CurrentYearMonth;
                        bill.FK_Flow = this.HisNode.FK_Flow;
                        bill.FK_BillType = func.FK_BillType;
                        bill.FK_Flow = this.HisNode.FK_Flow;
                        bill.Emps = this.rptGe.GetValStrByKey("Emps");
                        bill.FK_Starter = this.rptGe.GetValStrByKey("Rec");
                        bill.StartDT = this.rptGe.GetValStrByKey("RDT");
                        bill.Title = this.rptGe.GetValStrByKey("Title");
                        bill.FK_Dept = this.rptGe.GetValStrByKey("FK_Dept");
                        try
                        {
                            bill.Insert();
                        }
                        catch
                        {
                            bill.Update();
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        BP.WF.DTS.InitBillDir dir = new BP.WF.DTS.InitBillDir();
                        dir.Do();

                        path = BP.WF.Glo.FlowFileBill + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                        string msgErr = "@" + this.ToE("WN5", "生成单据失败，请让管理员检查目录设置") + "[" + BP.WF.Glo.FlowFileBill + "]。@Err：" + ex.Message + " @File=" + file + " @Path:" + path;
                        billInfo += "@<font color=red>" + msgErr + "</font>";
                        throw new Exception(msgErr + "@其它信息:" + ex.Message);
                    }

                } // end 生成循环单据。

                if (billInfo != "")
                    billInfo = "@" + billInfo;
                msg += billInfo;
            }
            #endregion

            // this.HisWork.DoCopy(); // copy 本地的数据到指定的系统.
            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=1 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);
            return msg;
        }
        /// <summary>
        /// 增加日志
        /// </summary>
        /// <param name="at">类型</param>
        /// <param name="toEmp">到人员</param>
        /// <param name="toEmpName">到人员名称</param>
        /// <param name="toNDid">到节点</param>
        /// <param name="toNDName">到节点名称</param>
        /// <param name="msg">消息</param>
        public void AddToTrack(ActionType at, string toEmp, string toEmpName, int toNDid, string toNDName, string msg)
        {
            Track t = new Track();
            t.WorkID = this.HisWork.OID;
            t.FID = this.HisWork.FID;
            t.RDT = DataType.CurrentDataTimess;
            t.HisActionType = at;

            t.NDFrom = this.HisNode.NodeID;
            t.NDFromT = this.HisNode.Name;

            t.EmpFrom = WebUser.No;
            t.EmpFromT = WebUser.Name;
            t.FK_Flow = this.HisNode.FK_Flow;

            t.NDTo = toNDid;
            t.NDToT = toNDName;

            t.EmpTo = toEmp;
            t.EmpToT = toEmpName;
            t.Msg = msg;
            if (string.IsNullOrEmpty(msg))
            {
                switch (at)
                {
                    case ActionType.Forward:
                    case ActionType.Start:
                    case ActionType.Undo:
                    case ActionType.ForwardFL:
                    case ActionType.ForwardHL:
                        //判断是否有焦点字段，如果有就把它记录到日志里。
                        if (this.HisNode.FocusField.Length > 1)
                        {
                            try
                            {
                                t.Msg = this.HisWork.GetValStrByKey(this.HisNode.FocusField);
                            }
                            catch (Exception ex)
                            {
                                Log.DebugWriteError("@焦点字段被删除了" + ex.Message + "@" + this.HisNode.FocusField);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (at == ActionType.Forward)
            {
                if (this.HisNode.IsFL)
                    at = ActionType.ForwardFL;
            }

            try
            {
                t.MyPK = t.WorkID + "_" + t.FID + "_"  + t.NDFrom + "_" + t.NDTo +"_"+t.EmpFrom+"_"+t.EmpTo+"_"+ DateTime.Now.ToString("yyMMddhhmmss");
                t.Insert();
            }
            catch
            {
                t.CheckPhysicsTable();
            }
        }
        /// <summary>
        /// 加入工作记录
        /// </summary>
        /// <param name="gwls"></param>
        public void AddIntoWacthDog(WorkerLists gwls)
        {
            if (BP.SystemConfig.IsBSsystem == false)
                return;

            if (BP.WF.Glo.IsEnableSysMessage  == false)
                return;

            string basePath = "http://" + System.Web.HttpContext.Current.Request.Url.Host;
            basePath += "/" + System.Web.HttpContext.Current.Request.ApplicationPath;

            string mailTemp = BP.DA.DataType.ReadTextFile2Html(BP.SystemConfig.PathOfDataUser + "\\EmailTemplete\\" + WebUser.SysLang + ".txt");
            foreach (WorkerList wl in gwls)
            {
                if (wl.IsEnable == false)
                    continue;

                string sid = wl.FK_Emp + "_" + wl.WorkID + "_" + wl.FK_Node + "_" + wl.RDT;
                string url = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid;
                string urlWap = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid + "&IsWap=1";

                //string mytemp ="您好" + wl.FK_EmpText + ":  <br><br>&nbsp;&nbsp; "+WebUser.Name+"发来的工作需要您处理，点这里<a href='" + url + "'>打开工作</a>。 \t\n <br>&nbsp;&nbsp;如果打不开请复制到浏览器地址栏里。<br>&nbsp;&nbsp;" + url + " <br><br>&nbsp;&nbsp;此邮件由驰骋工作流程引擎自动发出，请不要回复。<br>*^_^*  谢谢 ";
                string mytemp = mailTemp.Clone() as string;
                mytemp = string.Format(mytemp, wl.FK_EmpText, WebUser.Name, url, urlWap);

                //执行消息发送。
                BP.WF.Port.WFEmp wfemp = new BP.WF.Port.WFEmp(wl.FK_Emp);
                // wfemp.No = wl.FK_Emp;
                BP.TA.SMS.AddMsg(wl.WorkID + "_" + wl.FK_Node + "_" + wfemp.No, wfemp.No, wfemp.HisAlertWay, wfemp.Tel,
                    this.ToEP3("WN27", "流程:{0}.工作:{1},发送人:{2},需您处理",
                    this.HisNode.FlowName, wl.FK_NodeText, WebUser.Name),
                    wfemp.Email, null, mytemp);
            }

            /*
            string workers="";
            // 工作者
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
        /// 会签节点是否全部完成？
        /// </summary>
        private bool IsOverMGECheckStand = false;
        private bool IsStopFlow = false;
        /// <summary>
        /// 检查流程、节点的完成条件
        /// </summary>
        /// <returns></returns>
        private string CheckCompleteCondition()
        {
            this.IsStopFlow = false;
            // 调用检查全局的工作.
            string msg = this.CheckGlobalCompleteCondition();
            if (msg != null)
                return msg;

            #region 判断节点完成条件
            try
            {
                // 如果没有条件,就说明了,保存为完成节点任务的条件.
                if (this.HisNode.IsCCNode == false)
                {
                    msg = string.Format(this.ToE("WN6", "当前工作[{0}]已经完成"), this.HisNode.Name);
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
                            msg = "@当前工作[" + this.HisNode.Name + "]符合完成条件[" + this.HisNodeCompleteConditions.ConditionDesc + "],已经完成.";
                        else
                            msg = string.Format(this.ToE("WN6", "当前工作{0}已经完成"), this.HisNode.Name);  //"@"; //当前工作[" + this.HisNode.Name + "],已经完成.
                    }
                    else
                    {
                        // "@当前工作[" + this.HisNode.Name + "]没有完成,下一步工作不能启动."
                        throw new Exception(string.Format(this.ToE("WN7", "@当前工作{0}没有完成,下一步工作不能启动."), this.HisNode.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN8", "@判断节点{0}完成条件出现错误.") + ex.Message, this.HisNode.Name)); //"@判断节点[" + this.HisNode.Name + "]完成条件出现错误:" + ex.Message;
            }
            #endregion

            #region 判断流程条件.
            try
            {
                if (this.HisNode.HisToNodes.Count == 0 && this.HisNode.IsStartNode)
                {
                    /* 如果流程完成 */
                    string overMsg = this.HisWorkFlow.DoFlowOver("");
                    this.IsStopFlow = true;
                    this.AddToTrack(ActionType.FlowOver, WebUser.No, WebUser.Name,
                        this.HisNode.NodeID, this.HisNode.Name, "流程结束");
                    return "工作已经成功处理(一个流程的工作)。";
                    // string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    // return msg + "@符合工作流程完成条件" + this.HisFlowCompleteConditions.ConditionDesc + "" + overMsg + " @查看<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >工作报告</a>";
                }

                if ((this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass))
                {
                    /* 如果流程完成 */
                    string overMsg = this.HisWorkFlow.DoFlowOver("");
                    this.IsStopFlow = true;

                    this.AddToTrack(ActionType.FlowOver, WebUser.No, WebUser.Name,
                        this.HisNode.NodeID, this.HisNode.Name, "流程结束");
                    // string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    return msg + "@符合工作流程完成条件" + this.HisFlowCompleteConditions.ConditionDesc + "" + overMsg + " @查看<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >工作报告</a>";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN9", "@判断流程{0}完成条件出现错误.") + ex.Message, this.HisNode.Name));
            }
            #endregion
            return msg;
        }
        /// <summary>
        /// 启动新的节点
        /// </summary>
        /// <returns></returns>
        public string StartupNewNodeWork()
        {
            #region 启动下一个节点之前进行必要的判断。
            string msg = this.CheckCompleteCondition();
            if (this.IsStopFlow == true)
            {
                /*在检查完后，流程已经停止了。*/
                return msg;
            }

            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.WorkHL:
                case NodeWorkType.WorkFHL:
                    if (this.HisNode.IsForceKill)
                    {
                        // 检查是否还有没有完成的进程。
                        WorkerLists wls = new WorkerLists();
                        wls.Retrieve(WorkerListAttr.FID, this.HisWork.OID,
                            WorkerListAttr.IsPass, 0);
                        if (wls.Count > 0)
                        {
                            msg += "@开始进行对没有完成分流工作的同事强制删除。";
                            foreach (WorkerList wl in wls)
                            {
                                WorkFlow wf = new WorkFlow(wl.FK_Flow, wl.WorkID, wl.FID);
                                wf.DoDeleteWorkFlowByReal();
                                msg += "@已经把（" + wl.FK_Emp + " , " + wl.FK_EmpText + "）的工作删除。";
                            }
                        }
                    }
                    else
                    {
                        DataTable dt = DBAccess.RunSQLReturnTable("SELECT b.No,b.Name FROM WF_GenerWorkerList a,Port_Emp b WHERE a.FK_Emp=b.No AND IsPass=0 AND FID=" + this.HisWork.OID);
                        if (dt.Rows.Count != 0)
                        {
                            int i = BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET FID=0 WHERE FID=WorkID AND WorkID=" + this.WorkID);
                            if (i == 1)
                            {
                                //  this.HisWorkFlow.FID = 0;
                            }

                            dt = DBAccess.RunSQLReturnTable("SELECT b.No,b.Name FROM WF_GenerWorkerList a,Port_Emp b WHERE a.FK_Emp=b.No AND a.IsPass=0 AND a.IsEnable=1 AND FID=" + this.HisWork.OID);
                            if (dt.Rows.Count != 0)
                            {
                                msg = "@执行完成错误，有如下人员没有完成工作。";
                                foreach (DataRow dr in dt.Rows)
                                {
                                    msg += "@" + dr[0].ToString() + " - " + dr[1].ToString();
                                }
                                msg += "@以上每个成员都完成后，才可以执行此操作。";
                                throw new Exception(msg);
                            }
                        }
                    }
                    break;
                case NodeWorkType.Work:
                case NodeWorkType.StartWork:
                    // 合理的结束。
                    break;
                case NodeWorkType.SubThreadWork:
                    break;
                default:
                    break;
                //throw new Exception("@流程设计错误，结束节点的类型不能为:(" + this.HisNode.HisNodeWorkTypeT + ") 类型.");
            }
            #endregion

            // 取当前节点Nodes.
            Nodes toNodes = this.HisNode.HisToNodes;
            if (toNodes.Count == 0)
            {
                /* 如果是最后一个节点，就设置流程结束。*/
                string ovrMsg = this.HisWorkFlow.DoFlowOver("无");
                
                this.AddToTrack(ActionType.FlowOver, WebUser.No, WebUser.Name,
                    this.HisNode.NodeID, this.HisNode.Name, "流程结束");

                if (this.HisNode.HisFormType == FormType.SDKForm)
                    return ovrMsg + this.ToE("WN0", "@此工作流程运行到最后一个环节，工作成功结束！");
                else
                    return ovrMsg + this.ToE("WN0", "@此工作流程运行到最后一个环节，工作成功结束！") + "<img src='" + this.VirPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "&FK_Node=" + this.HisNode.NodeID + "'target='_blank' >" + this.ToE("WorkRpt", "工作报告") + "</a>";
            }

            // 如果有跳转的节点.
            if (this.JumpToNode != null)
            {
                return msg + StartNextNode(this.JumpToNode);
            }

            // 如果只有一个转向节点, 就不用判断条件了,直接转向他.
            if (toNodes.Count == 1)
                return msg + StartNextNode((Node)toNodes[0]);

            Node toNode = null;
            int numOfWay = 0;
            string condMsg = "";
            Conds dcs = new Conds();
            QueryObject qo = new QueryObject(dcs);
            qo.AddWhere(CondAttr.NodeID, this.HisNode.NodeID);
            qo.addOrderBy(CondAttr.PRI);
            qo.DoQuery();
            foreach (Cond cd in dcs)
            {
                cd.WorkID = this.WorkID;
                foreach (Node nd in toNodes)
                {
                    if (cd.ToNodeID != nd.NodeID)
                        continue;
                    if (cd.IsPassed) // 如果多个转向条件中有一个成立.
                    {
                        numOfWay++;
                        toNode = nd;
                        break;
                    }
                    condMsg += "<b>@检查方向条件：到节点：" + nd.Name + "</b>";
                    condMsg += dcs.MsgOfDesc;
                }

                if (toNode != null)
                    break;
            }

            if (toNode == null)
                throw new Exception(string.Format(this.ToE("WN11", "@转向条件设置错误,节点名称:{0}, 系统无法投递。"),
                    this.HisNode.Name));

            /* 删除曾经在这个步骤上的流程运行数据。
             * 比如说：方向条件，发生了变化后可能产生两个工作上的数据。是为了工作报告上面体现了两个步骤。 */
            foreach (Node nd in toNodes)
            {
                if (nd.NodeID == toNode.NodeID)
                    continue;

                // 删除这个工作，因为这个工作的数据不在有用了。
                Work wk = nd.HisWork;
                wk.OID = this.HisWork.OID;
                if (wk.Delete() != 0)
                {
                    /* 删除其它附件信息，明细表信息。 */
                    #warning 没处理。
                }
            }
            msg += StartNextNode(toNode);
            return msg;
        }

        #region 启动审核节点
        /// <summary>
        /// 启动多个节点.
        /// </summary>
        /// <param name="nds"></param>
        /// <returns></returns>
        public string StartNextNode(Nodes nds)
        {
            /* 分流点 */
            foreach (Node nd in nds)
            {
                if (nd.IsHL == true)
                {
                    /* 如果其中的一个点到达了合流点，就让它发起合流点。*/
                    if (nds.Count != 1)
                    {
                        throw new Exception("@在遇到混合流程节点发送时错误，你不能同时启动分流与干流。");
                    }
                    else
                    {
                        return StartNextNode(nd);
                    }
                }
            }

            /*分别启动每个节点的信息.*/
            string msg = "";

            //查询出来上一个节点的附件信息来。
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());

            //查询出来上一个Ele信息来。
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                       this.WorkID.ToString());

            foreach (Node nd in nds)
            {
                msg += "@"+nd.Name+"工作已经启动，处理工作者：";
                //产生一个工作信息。
                Work wk = nd.HisWork;
                wk.Copy(this.HisWork);
                wk.FID = this.HisWork.OID;
                wk.OID = BP.DA.DBAccess.GenerOID( WebUser.FK_Dept);
                wk.NodeState = NodeState.Init;
                wk.BeforeSave();
                wk.DirectInsert();

                if (athDBs.Count > 0)
                {
                    /*说明当前节点有附件数据*/
                    int idx = 0;
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        idx++;
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                        athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                            "ND" + nd.NodeID) + "_" + idx;
                        athDB_N.RefPKVal = wk.OID.ToString();
                        athDB_N.Insert();
                    }
                }

                if (eleDBs.Count > 0)
                {
                    /*说明当前节点有附件数据*/
                    int idx = 0;
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        idx++;
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + nd.NodeID;
                        eleDB_N.Insert();
                    }
                }

                //获得它的工作者。
                WorkNode town = new WorkNode(wk, nd);
                WorkerLists gwls = this.GenerWorkerLists(town);
                foreach (WorkerList wl in gwls)
                {
                    msg += wl.FK_Emp + "，" + wl.FK_EmpText + "、";

                    // 产生工作的信息。
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.FID = this.WorkID;
                    gwf.WorkID = wk.OID;

#warning 需要修改成标题生成规则。
                    
                    gwf.Title = this.GenerTitle(this.HisWork);

                    gwf.WFState = 0;
                    gwf.RDT = DataType.CurrentDataTime;
                    gwf.Rec = Web.WebUser.No;
                    gwf.RecName = Web.WebUser.Name;
                    gwf.FK_Flow = nd.FK_Flow;
                    gwf.FlowName = nd.FlowName;

                    gwf.FK_FlowSort = this.HisNode.HisFlow.FK_FlowSort;

                    gwf.FK_Node = nd.NodeID;
                    gwf.NodeName = nd.Name;
                    gwf.FK_Dept = wl.FK_Dept;
                    gwf.DeptName = wl.FK_DeptT;
                    gwf.DirectInsert();
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET WorkID=" + wk.OID + ",FID=" + this.WorkID + " WHERE FK_Emp='" + wl.FK_Emp + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);
                }
            }
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);
            return msg;
        }
        /// <summary>
        /// 启动指定的下一个节点 .
        /// </summary>
        /// <param name="nd">要启动的节点</param>
        public string StartNextNode(Node nd)
        {
            /*如果当前节点是分流，下一个节点是合流，就按普通节点启动它。*/
            if (this.HisNode.IsFL && nd.IsHL)
                return StartNextWorkNodeOrdinary(nd);  /* 普通节点 */
            string msg = "";
            try
            {
                switch (nd.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFHL: // 如果下一个节点是合流节点，或者是分合流节点。
                        // 判断当前节点的类型是什么.
                        switch (this.HisNode.HisNodeWorkType)
                        {
                            case NodeWorkType.Work:
                            case NodeWorkType.SubThreadWork:
                            case NodeWorkType.StartWork: /* 如果当前点是以上类型 */
                                if (this.HisWork.FID == 0)
                                    msg = StartNextWorkNodeHeLiu_WithOutFID(nd);  /* 没有流程ID,比如: */
                                else
                                    msg = StartNextWorkNodeHeLiu_WithFID(nd);  /* 合流节点, 开始节点是分流节点 */
                                break;
                            default:
                                throw new Exception("@没有判断的情况。");
                        }
                        msg += "@<a href='" + this.VirPath + this.AppType+"/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&FID=" + this.HisWork.FID + "&WorkID=" + this.WorkID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>。";
                        return msg;
                    case NodeWorkType.StartWork:
                    case NodeWorkType.StartWorkFL:
                        throw new Exception("System Error ");
                        break;
                    case NodeWorkType.SubThreadWork: // 如果是下一个节点是子线程.
                    default:
                        return StartNextWorkNodeOrdinary(nd);  /* 普通节点 */
                }
                // this.InitEmps(nd);
            }
            catch (Exception ex)
            {
                throw new Exception("@" + this.ToE("StartNextNodeErr", "@启动下一个节点出现错误") + ":" + ex.Message); //启动下一个节点出现错误
            }
        }
        /// <summary>
        /// 在启动下个工作要做的工作。
        /// </summary>
        /// <param name="wk">要启动的工作</param>
        /// <param name="nd">要启动的节点。</param>
        /// <returns>启动的信息</returns>
        private string beforeStartNode(Work wk, Node nd)
        {
            // town.
            WorkNode town = new WorkNode(wk, nd);

            // 初试化他们的工作人员．
            WorkerLists gwls = this.GenerWorkerLists(town);

            //@加入消息集合里。
            this.AddIntoWacthDog(gwls);

            string msg = "";
            msg = this.ToEP3("TaskAutoSendTo", "@任务自动下达给{0}如下{1}位同事,{2}.", this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
            if (this._RememberMe.NumOfEmps >= 2)
            {
                if (WebUser.IsWap)
                    msg += "<a href=\"" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>" + this.ToE("WN24", "指定特定的同事处理") + "</a>。";
                else
                    msg += "<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>" + this.ToE("WN24", "指定特定的同事处理") + "</a>。";
            }

            msg += this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);

            if (WebUser.IsWap == false)
                msg += "@<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "');\" ><img src='" + this.VirPath + "/WF/Img/SMS.gif' border=0 />" + this.ToE("WN21", "发手机短信提醒他(们)") + "</a>";

            if (this.HisNode.HisFormType != FormType.SDKForm)
            {
                if (this.HisNode.IsStartNode)
                    msg += "@<a href='" + this.VirPath + "/" + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>， <a href='" + this.VirPath + "/" + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + nd.FK_Flow + "'><img src=" + this.VirPath + "/WF/Img/New.gif border=0/>" + this.ToE("NewFlow", "新建流程") + "</a>。";
                else
                    msg += "@<a href='" + this.VirPath + "/" + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>。";
            }

            string str = "";
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + ", NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.OID);

            if (this.HisNode.HisFormType == FormType.SDKForm)
                return msg;
            else
                return msg + "@" + str + " <img src='" + this.VirPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FID=" + wk.FID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >" + this.ToE("WorkRpt", "工作报告") + "</a>。";
        }
        /// <summary>
        /// 生成为什么发送给他们
        /// </summary>
        /// <param name="fNodeID"></param>
        /// <param name="toNodeID"></param>
        /// <returns></returns>
        public string GenerWhySendToThem(int fNodeID, int toNodeID)
        {
            return "";
            //return "@<a href='WhySendToThem.aspx?NodeID=" + fNodeID + "&ToNodeID=" + toNodeID + "&WorkID=" + this.WorkID + "' target=_blank >" + this.ToE("WN20", "为什么要发送给他们？") + "</a>";
        }
        /// <summary>
        /// 工作流程ID
        /// </summary>
        public static Int64 FID = 0;
        /// <summary>
        /// 没有FID
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithOutFID(Node nd)
        {
            throw new Exception("未完成:StartNextWorkNodeHeLiu_WithOutFID");
        }
        private string StartNextWorkNodeHeLiu_WithFID_YiBu(Node nd)
        {
            GenerFH myfh = new GenerFH(this.HisWork.FID);
            if (myfh.FK_Node == nd.NodeID)
            {
                /* 说明不是第一次到这个节点上来了, 
                 * 比如：一条流程：
                 * A分流-> B普通-> C合流
                 * 从B 到C 中, B中有N 个线程，在之前已经有一个线程到达过C.
                 */

                /* 
                 * 首先:更新它的节点 worklist 信息, 说明当前节点已经完成了.
                 * 不让当前的操作员能看到自己的工作。
                 */
                DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID + " AND FK_Node=" + this.HisNode.NodeID);
                DBAccess.RunSQL("UPDATE WF_GenerWorkFlow   SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.OID);

                /*
                 * 其次更新当前节点的状态与完成时间.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                // 产生合流汇总明细表数据.
                this.GenerHieLiuHuiZhongDtlData(nd);

                #region 处理完成率
                Nodes fromNds = nd.HisFromNodes;
                string nearHLNodes = "";
                foreach (Node mynd in fromNds)
                {
                    if (mynd.HisNodeWorkType == NodeWorkType.SubThreadWork)
                        nearHLNodes += "," + mynd.NodeID;
                }
                nearHLNodes = nearHLNodes.Substring(1);

                string sqlOK = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE FK_Node IN (" + nearHLNodes + ") AND FID=" + this.HisWork.FID + " AND IsPass=1";
                decimal ok = (decimal)DBAccess.RunSQLReturnValInt(sqlOK);

                string sqlAll = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(sqlAll);
                decimal passRate = ok / all * 100;
                string numStr = "@您是第(" + ok + ")到达此节点上的同事，共启动了(" + all + ")个子流程。";
                if (nd.PassRate <= passRate)
                {
                    /*说明全部的人员都完成了，就让合流点显示它。*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.FID);
                    numStr += "@下一步工作(" + nd.Name + ")已经启动。";
                }
                #endregion 处理完成率

                string fk_emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('<'));
                this.AddToTrack(ActionType.ForwardHL, fk_emp1, myfh.ToEmpsMsg, nd.NodeID, nd.Name, null);
                return "@流程已经运行到合流节点[" + nd.Name + "]，当前工作已经完成.@您的工作已经发送给如下人员[" + myfh.ToEmpsMsg + "]，<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >短信通知他们</a>。" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID) + numStr;
            }

            /* 已经有FID，说明：以前已经有分流或者合流节点。*/
            /*
             * 以下处理的是没有流程到达此位置
             * 说明是第一次到这个节点上来了.
             * 比如：一条流程:
             * A分流-> B普通-> C合流
             * 从B 到C 中, B中有N 个线程，在之前他是第一个到达C.
             */

            // 首先找到此节点的接受人员的集合。做为 FID 合流分流的FID。
            WorkNode town = new WorkNode(nd.HisWork, nd);

            // 初试化他们的工作人员．
            WorkerLists gwls = this.GenerWorkerLists_WidthFID(town);
            string fk_emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (WorkerList wl in gwls)
            {
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "、";
                else
                    toEmpsStr += wl.FK_Emp + "<" + wl.FK_EmpText + ">、";

                if (gwls.Count == 1)
                    emps = fk_emp;
                else
                    emps += "@" + fk_emp;
            }

            /* 
            * 更新它的节点 worklist 信息, 说明当前节点已经完成了.
            * 不让当前的操作员能看到自己的工作。
            */

            // 设置父流程状态 设置当前的节点为:
            myfh.Update(GenerFHAttr.FK_Node, nd.NodeID,
                GenerFHAttr.ToEmpsMsg, toEmpsStr);

            #region 处理合流节点表单数据。
            Work mainWK = town.HisWork;
            mainWK.OID = this.HisWork.FID;
            if (mainWK.RetrieveFromDBSources() == 1)
                mainWK.Delete();

            #region 复制报表上面的数据到合流点上去。
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM ND" + int.Parse(nd.FK_Flow) + "Rpt WHERE OID=" + this.HisWork.FID);
            foreach (DataColumn dc in dt.Columns)
                mainWK.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName]);

            mainWK.NodeState = NodeState.Init;
            mainWK.Rec = fk_emp;
            mainWK.Emps = emps;
            mainWK.OID = this.HisWork.FID;
            mainWK.Insert();
            #endregion 复制报表上面的数据到合流点上去。

            #region 复制附件。
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (athDBs.Count > 0)
            {
                /*说明当前节点有附件数据*/
                int idx = 0;
                foreach (FrmAttachmentDB athDB in athDBs)
                {
                    idx++;
                    FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                    athDB_N.Copy(athDB);
                    athDB_N.FK_MapData = "ND" + nd.NodeID;
                    athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID) + "_" + idx;
                    athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                       "ND" + nd.NodeID);
                    athDB_N.RefPKVal = this.HisWork.FID.ToString();
                    athDB_N.Save();
                }
            }
            #endregion 复制附件。

            #region 复制EleDB。
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (eleDBs.Count > 0)
            {
                /*说明当前节点有附件数据*/
                int idx = 0;
                foreach (FrmEleDB eleDB in eleDBs)
                {
                    idx++;
                    FrmEleDB eleDB_N = new FrmEleDB();
                    eleDB_N.Copy(eleDB);
                    eleDB_N.FK_MapData = "ND" + nd.NodeID;
                    eleDB_N.MyPK = eleDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);

                    eleDB_N.RefPKVal = this.HisWork.FID.ToString();
                    eleDB_N.Save();
                }
            }
            #endregion 复制EleDB。

            // 产生合流汇总明细表数据.
            this.GenerHieLiuHuiZhongDtlData(nd);

            #endregion 处理合流节点表单数据


            /* 合流点需要等待各个分流点全部处理完后才能看到它。*/
            string info = "";
            string sql1 = "";
#warning 对于多个分合流点可能会有问题。
            sql1 = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE  FID=" + this.HisWork.FID + " AND FK_Node IN (" + this.SpanSubTheadNodes(nd) + ")";
            decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(sql1);
            decimal passRate1 = 1 / numAll1 * 100;
            if (nd.PassRate <= passRate1)
            {
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
                info = "@下一步合流点("+nd.Name+")已经启动。";
            }
            else
            {
#warning 为了不让其显示在途的工作需要， =3 不是正常的处理模式。
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.FID);
            return "@当前工作已经完成，流程已经运行到合流节点[" + nd.Name + "]。@您的工作已经发送给如下人员[" + toEmpsStr + "]，<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >短信通知他们</a>。" + "@您是第一个到达此节点的同事."+info;
        }
        /// <summary>
        /// 产生合流汇总数据
        /// </summary>
        /// <param name="nd"></param>
        private void GenerHieLiuHuiZhongDtlData(Node ndOfHeLiu)
        {
            MapDtls mydtls = ndOfHeLiu.HisWork.HisMapDtls;
            foreach (MapDtl dtl in mydtls)
            {
                if (dtl.IsHLDtl == false)
                    continue;
                GEDtl geDtl = dtl.HisGEDtl;
                geDtl.Copy(this.HisWork);
                geDtl.RefPK = this.HisWork.FID.ToString();
                geDtl.Rec = WebUser.No;
                geDtl.RDT = DataType.CurrentDataTime;
                try
                {
                    geDtl.InsertAsOID(geDtl.OID);
                }
                catch
                {
                    geDtl.Update();
                }
                break;
            }
        }
        /// <summary>
        /// 子线程节点
        /// </summary>
        private string _SpanSubTheadNodes = null;
        /// <summary>
        /// 获取分流与合流之间的子线程节点集合.
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        private string SpanSubTheadNodes(Node toHLNode)
        {
            _SpanSubTheadNodes = "";
            SpanSubTheadNodes_DG(toHLNode.HisFromNodes);
            if (_SpanSubTheadNodes == "")
                throw new Exception("获取分合流之间的子线程节点集合出现错。");
            _SpanSubTheadNodes = _SpanSubTheadNodes.Substring(1);
            return _SpanSubTheadNodes;

            //DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM WF_GenerWorkerlist WHERE FID=" + this.HisWork.FID + " AND FK_Node IN (" + _SpanSubTheadNodes + ") ");
            //_SpanSubTheadNodes = "";
            //foreach (DataRow dr in dt.Rows)
            //{
            //    _SpanSubTheadNodes += "," + dr["FK_Node"].ToString();
            //}
            //_SpanSubTheadNodes = _SpanSubTheadNodes.Substring(1);
            //return _SpanSubTheadNodes;
        }
        private void SpanSubTheadNodes_DG(Nodes subNDs)
        {
            foreach (Node nd in subNDs)
            {
                if (nd.IsFL == true)
                    continue;

                if (nd.HisNodeWorkType == NodeWorkType.SubThreadWork)
                    _SpanSubTheadNodes += "," + nd.NodeID;

                SpanSubTheadNodes_DG(nd.HisFromNodes);
            }
        }
        /// <summary>
        /// 有FID
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithFID(Node nd)
        {
            //设置已经通过.
            DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID );

            string spanNodes = this.SpanSubTheadNodes(nd);
            if (nd.HisFromNodes.Count != 1)
            {
                return StartNextWorkNodeHeLiu_WithFID_YiBu(nd);
            }

            GenerFH myfh = new GenerFH(this.HisWork.FID);
            if (myfh.FK_Node == nd.NodeID)
            {
                /* 说明不是第一次到这个节点上来了, 
                 * 比如：一条流程：
                 * A分流-> B普通-> C合流
                 * 从B 到C 中, B中有N 个线程，在之前已经有一个线程到达过C.
                 */

                /* 
                 * 首先:更新它的节点 worklist 信息, 说明当前节点已经完成了.
                 * 不让当前的操作员能看到自己的工作。
                 */

                DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID + " AND FK_Node=" + this.HisNode.NodeID);
                DBAccess.RunSQL("UPDATE WF_GenerWorkFlow   SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.OID);

                /*
                 * 其次更新当前节点的状态与完成时间.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                #region 处理完成率
                string sql = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID + " AND IsPass=1";
                decimal ok = (decimal)DBAccess.RunSQLReturnValInt(sql);

                // sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID;
                // sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE IsPass=0 AND FID=" + this.HisWork.FID;
                sql = "SELECT  COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE   IsEnable=1 AND FID=" + this.HisWork.FID + " AND FK_Node IN (" + spanNodes + ")";
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(sql);
                decimal passRate = ok / all * 100;
                string numStr = "@您是第(" + ok + ")到达此节点上的同事，共启动了(" + all + ")个子流程。";
                if (nd.PassRate <= passRate)
                {
                    /*说明全部的人员都完成了，就让合流点显示它。*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.FID);
                    numStr += "@下一步工作(" + nd.Name + ")已经启动。";
                }
                #endregion 处理完成率


                // 产生合流汇总明细表数据.
                this.GenerHieLiuHuiZhongDtlData(nd);

                string fk_emp1 = myfh.ToEmpsMsg.Substring(0, myfh.ToEmpsMsg.LastIndexOf('<'));
                this.AddToTrack(ActionType.ForwardHL, fk_emp1, myfh.ToEmpsMsg, nd.NodeID, nd.Name, null);

                return "@流程已经运行到合流节点[" + nd.Name + "]，当前工作已经完成.@您的工作已经发送给如下人员[" + myfh.ToEmpsMsg + "]，<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >短信通知他们</a>。" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID) + numStr;
            }

            /* 已经有FID，说明：以前已经有分流或者合流节点。*/
            /*
             * 以下处理的是没有流程到达此位置
             * 说明是第一次到这个节点上来了.
             * 比如：一条流程:
             * A分流-> B普通-> C合流
             * 从B 到C 中, B中有N 个线程，在之前他是第一个到达C.
             */

            // 首先找到此节点的接受人员的集合。做为 FID 合流分流的FID。
            WorkNode town = new WorkNode(nd.HisWork, nd);

            // 初试化他们的工作人员．
            WorkerLists gwls = this.GenerWorkerLists_WidthFID(town);
            string fk_emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (WorkerList wl in gwls)
            {
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "、";
                else
                    toEmpsStr += wl.FK_Emp + "<" + wl.FK_EmpText + ">、";

                if (gwls.Count == 1)
                    emps = fk_emp;
                else
                    emps += "@" + fk_emp;
            }

            /* 
            * 更新它的节点 worklist 信息, 说明当前节点已经完成了.
            * 不让当前的操作员能看到自己的工作。
            */

            #region 设置父流程状态 设置当前的节点为:
            myfh.Update(GenerFHAttr.FK_Node, nd.NodeID,
                GenerFHAttr.ToEmpsMsg, toEmpsStr);

            Work mainWK = town.HisWork;
            mainWK.OID = this.HisWork.FID;
            if (mainWK.RetrieveFromDBSources() == 1)
                mainWK.Delete();

            // 复制报表上面的数据到合流点上去。
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT * FROM ND" + int.Parse(nd.FK_Flow) + "Rpt WHERE OID=" + this.HisWork.FID);
            foreach (DataColumn dc in dt.Columns)
                mainWK.SetValByKey(dc.ColumnName, dt.Rows[0][dc.ColumnName]);

            mainWK.NodeState = NodeState.Init;
            mainWK.Rec = fk_emp;
            mainWK.Emps = emps;
            mainWK.OID = this.HisWork.FID;
            mainWK.Insert();

            // 产生合流汇总明细表数据.
            this.GenerHieLiuHuiZhongDtlData(nd);

            /*处理表单数据的复制。*/
            #region 复制附件。
            FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (athDBs.Count > 0)
            {
                /*说明当前节点有附件数据*/
                int idx = 0;
                foreach (FrmAttachmentDB athDB in athDBs)
                {
                    idx++;
                    FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                    athDB_N.Copy(athDB);
                    athDB_N.FK_MapData = "ND" + nd.NodeID;
                    athDB_N.MyPK = athDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID) + "_" + idx;
                    athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                       "ND" + nd.NodeID);
                    athDB_N.RefPKVal = this.HisWork.FID.ToString();
                    athDB_N.Save();
                }
            }
            #endregion 复制附件。

            #region 复制Ele。
            FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                  this.WorkID.ToString());
            if (eleDBs.Count > 0)
            {
                /*说明当前节点有附件数据*/
                int idx = 0;
                foreach (FrmEleDB eleDB in eleDBs)
                {
                    idx++;
                    FrmEleDB eleDB_N = new FrmEleDB();
                    eleDB_N.Copy(eleDB);
                    eleDB_N.FK_MapData = "ND" + nd.NodeID;
                    eleDB_N.MyPK = eleDB_N.MyPK.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                    eleDB_N.RefPKVal = this.HisWork.FID.ToString();
                    eleDB_N.Save();
                }
            }
            #endregion 复制附件。

            /* 合流点需要等待各个分流点全部处理完后才能看到它。*/

            string sql1 = "";
            // "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID;
            // string sql1 = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE  IsPass=0 AND FID=" + this.HisWork.FID;

#warning 对于多个分合流点可能会有问题。
            sql1 = "SELECT COUNT(distinct WorkID) AS Num FROM WF_GenerWorkerList WHERE  FID=" + this.HisWork.FID + " AND FK_Node IN (" + spanNodes + ")";
            decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(sql1);
            decimal passRate1 = 1 / numAll1 * 100;
            if (nd.PassRate <= passRate1)
            {
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }
            else
            {
#warning 为了不让其显示在途的工作需要， =3 不是正常的处理模式。
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }

            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + ",NodeName='" + nd.Name + "' WHERE WorkID=" + this.HisWork.FID);
            #endregion 设置父流程状态

            return "@当前工作已经完成，流程已经运行到合流节点[" + nd.Name + "]。@您的工作已经发送给如下人员[" + toEmpsStr + "]，<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >短信通知他们</a>。" + "@您是第一个到达此节点的同事.";
        }
        /// <summary>
        /// 启动下一个工作节点
        /// </summary>
        /// <param name="nd">节点</param>		 
        /// <returns></returns>
        private string StartNextWorkNodeOrdinary(Node nd)
        {
            string sql = "";
            try
            {
                #region  初始化发起的工作节点。
                Work wk = nd.HisWork;
                wk.SetValByKey("OID", this.HisWork.OID); //设定它的ID.
                if (this.HisNode.IsStartNode == false)
                    wk.Copy(this.rptGe);

                wk.FID = 0;
                wk.Copy(this.HisWork); // 执行 copy 上一个节点的数据。
                wk.NodeState = NodeState.Init; //节点状态。
                wk.Rec = BP.Web.WebUser.No;
                try
                {
                    wk.Insert();
                }
                catch (Exception ex)
                {
                    wk.CheckPhysicsTable();
                    try
                    {
                        wk.Update();
                    }
                    catch (Exception ex11)
                    {
                        throw new Exception(ex.Message + " == " + ex11.Message);
                    }
                }

                #region 复制附件。
                FrmAttachmentDBs athDBs = new FrmAttachmentDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                int idx = 0;
                if (athDBs.Count > 0)
                {
                    athDBs.Delete(FrmAttachmentDBAttr.FK_MapData, "ND" + nd.NodeID,
                        FrmAttachmentDBAttr.RefPKVal, this.WorkID);

                    /*说明当前节点有附件数据*/
                    foreach (FrmAttachmentDB athDB in athDBs)
                    {
                        idx++;
                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                        athDB_N.Copy(athDB);
                        athDB_N.FK_MapData = "ND" + nd.NodeID;
                        athDB_N.RefPKVal = this.WorkID.ToString();
                        athDB_N.MyPK = this.WorkID + "_" + idx + "_" + athDB_N.FK_MapData;
                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                           "ND" + nd.NodeID);
                        athDB_N.Save();
                    }
                }
                #endregion 复制附件。


                #region 复制Ele。
                FrmEleDBs eleDBs = new FrmEleDBs("ND" + this.HisNode.NodeID,
                      this.WorkID.ToString());
                if (eleDBs.Count > 0)
                {
                    eleDBs.Delete(FrmEleDBAttr.FK_MapData, "ND" + nd.NodeID,
                        FrmEleDBAttr.RefPKVal, this.WorkID);

                    /*说明当前节点有附件数据*/
                    foreach (FrmEleDB eleDB in eleDBs)
                    {
                        idx++;
                        FrmEleDB eleDB_N = new FrmEleDB();
                        eleDB_N.Copy(eleDB);
                        eleDB_N.FK_MapData = "ND" + nd.NodeID;
                        eleDB_N.GenerPKVal();
                        eleDB_N.Save();
                    }
                }
                #endregion 复制Ele。

                #region 复制多选数据
                M2Ms m2ms = new M2Ms("ND"+this.HisNode.NodeID, this.WorkID);
                if (m2ms.Count >= 1)
                {
                    foreach (M2M item in m2ms)
                    {
                        M2M m2 = new M2M();
                        m2.Copy(item);
                        m2.EnOID = this.WorkID;
                        m2.FK_MapData = m2.FK_MapData.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                        try
                        {
                            m2.DirectInsert();
                        }
                        catch
                        {
                            m2.DirectUpdate();
                        }
                    }
                }
                #endregion

                #region 复制明细数据。
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + this.HisNode.NodeID);
                if (dtls.Count >= 1)
                {
                    Sys.MapDtls toDtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                    Sys.MapDtls startDtls = null;
                    bool isEnablePass = false;
                    foreach (MapDtl dtl in dtls)
                    {
                        if (dtl.IsEnablePass)
                            isEnablePass = true;
                    }

                    if (isEnablePass)
                        startDtls = new BP.Sys.MapDtls("ND" + int.Parse(nd.FK_Flow) + "01");

                    int i = -1;
                    foreach (Sys.MapDtl dtl in dtls)
                    {
                        i++;
                        if (toDtls.Count <= i)
                            continue;

                        Sys.MapDtl toDtl = (Sys.MapDtl)toDtls[i];
                        if (dtl.IsEnablePass == true)
                        {
                            /*如果启用了是否明细表的审核通过机制,就允许copy节点数据。*/
                            toDtl.IsCopyNDData = true;
                        }

                        if (toDtl.IsCopyNDData == false)
                            continue;

                        //获取明细数据。
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
                                qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                break;
                            case DtlOpenType.ForFID:
                                qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                break;
                        }
                        qo.DoQuery();
                        int unPass = 0;
                        // 是否起用审核机制。
                        isEnablePass = dtl.IsEnablePass;
                        if (isEnablePass && this.HisNode.IsStartNode == false)
                            isEnablePass = true;
                        else
                            isEnablePass = false;

                        if (isEnablePass == true)
                        {
                            /*判断当前节点该明细表上是否有，isPass 审核字段，如果没有抛出异常信息。*/
                            if (gedtls.Count != 0)
                            {
                                GEDtl dtl1 = gedtls[0] as GEDtl;
                                if (dtl1.EnMap.Attrs.Contains("IsPass") == false)
                                    isEnablePass = false;
                            }
                        }

                        DBAccess.RunSQL("DELETE " + toDtl.PTable + " WHERE RefPK=" + this.WorkID);
                        foreach (GEDtl gedtl in gedtls)
                        {
                            if (isEnablePass)
                            {
                                if (gedtl.GetValBooleanByKey("IsPass") == false)
                                {
                                    /*没有审核通过的就 continue 它们，仅复制已经审批通过的.*/
                                    continue;
                                }
                            }

                            BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                            dtCopy.Copy(gedtl);
                            dtCopy.FK_MapDtl = toDtl.No;
                            dtCopy.RefPK = this.WorkID.ToString();
                            dtCopy.InsertAsOID(dtCopy.OID);

                            #region  复制明细表单条 - 附件信息
                            if (toDtl.IsEnableAthM)
                            {
                                /*如果启用了多附件,就复制这条明细数据的附件信息。*/
                                athDBs = new FrmAttachmentDBs(dtl.No, gedtl.OID.ToString());
                                if (athDBs.Count > 0)
                                {
                                    i = 0;
                                    foreach (FrmAttachmentDB athDB in athDBs)
                                    {
                                        i++;
                                        FrmAttachmentDB athDB_N = new FrmAttachmentDB();
                                        athDB_N.Copy(athDB);
                                        athDB_N.FK_MapData = toDtl.No;
                                        athDB_N.MyPK = toDtl.No + "_" + dtCopy.OID + "_" + i.ToString();
                                        athDB_N.FK_FrmAttachment = athDB_N.FK_FrmAttachment.Replace("ND" + this.HisNode.NodeID,
                                            "ND" + nd.NodeID);
                                        athDB_N.RefPKVal = dtCopy.OID.ToString();
                                        athDB_N.DirectInsert();
                                    }
                                }
                            }
                            if (toDtl.IsEnableM2M || toDtl.IsEnableM2MM)
                            {
                                /*如果启用了m2m */
                                m2ms = new M2Ms(dtl.No, gedtl.OID);
                                if (m2ms.Count > 0)
                                {
                                    i = 0;
                                    foreach (M2M m2m in m2ms)
                                    {
                                        i++;
                                        M2M m2m_N = new M2M();
                                        m2m_N.Copy(m2m);
                                        m2m_N.FK_MapData = toDtl.No;
                                        m2m_N.MyPK = toDtl.No + "_" + m2m.M2MNo + "_" + gedtl.ToString() + "_" + m2m.DtlObj;
                                        m2m_N.EnOID = gedtl.OID;
                                        m2m_N.DirectInsert();
                                    }
                                }
                            }
                            #endregion  复制明细表单条 - 附件信息

                        }
                        if (isEnablePass)
                        {
                            /* 如果启用了审核通过机制，就把未审核的数据copy到第一个节点上去 
                             * 1, 找到对应的明细点.
                             * 2, 把未审核通过的数据复制到开始明细表里.
                             */

                            string startTable = "ND" + int.Parse(nd.FK_Flow) + "01";
                            string startUser = "SELECT Rec FROM " + startTable + " WHERE OID=" + this.WorkID;
                            startUser = DBAccess.RunSQLReturnString(startUser);

                            MapDtl startDtl = (MapDtl)startDtls[i];
                            foreach (GEDtl gedtl in gedtls)
                            {
                                if (gedtl.GetValBooleanByKey("IsPass"))
                                    continue; /* 排除审核通过的 */

                                BP.Sys.GEDtl dtCopy = new GEDtl(startDtl.No);
                                dtCopy.Copy(gedtl);
                                dtCopy.OID = 0;
                                dtCopy.FK_MapDtl = startDtl.No;
                                dtCopy.RefPK = gedtl.OID.ToString(); //this.WorkID.ToString();
                                dtCopy.SetValByKey("BatchID", this.WorkID);
                                dtCopy.SetValByKey("IsPass", 0);
                                dtCopy.SetValByKey("Rec", startUser);
                                dtCopy.SetValByKey("Checker", BP.Web.WebUser.Name);
                                dtCopy.SaveAsOID(gedtl.OID);
                            }
                            DBAccess.RunSQL("UPDATE " + startDtl.PTable + " SET Rec='" + startUser + "',Checker='" + WebUser.No + "' WHERE BatchID=" + this.WorkID + " AND Rec='" + WebUser.No + "'");
                        }
                    }
                }
                #endregion 复制明细数据

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
                catch
                {
                    #region 解决工作节点字段修复问题
                    try
                    {
                        wk.CheckPhysicsTable();
                        wk.DirectSave();
                    }
                    catch (Exception ex)
                    {
                        Log.DebugWriteInfo(this.ToE("SaveWorkErr", "保存工作错误") + "：" + ex.Message);
                        throw new Exception(this.ToE("SaveWorkErr", "保存工作错误  ") + wk.EnDesc + ex.Message);
                    }
                    #endregion 解决字段修复问题
                }
                // 启动一个工作节点.
                string msg = this.beforeStartNode(wk, nd);
                return "@" + string.Format(this.ToE("NStep", "@第{0}步"), nd.Step.ToString()) + "<font color=blue>" + nd.Name + "</font>" + this.ToE("WorkStartOK", "工作成功启动") + "." + msg;
            }
            catch (Exception ex)
            {
                nd.HisWorks.DoDBCheck(DBLevel.Middle);
                throw new Exception(string.Format("StartGEWorkErr", nd.Name) + "@" + ex.Message + sql);
            }
        }
        #endregion

        #region 基本属性
        /// <summary>
        /// 工作
        /// </summary>
        private Work _HisWork = null;
        /// <summary>
        /// 工作
        /// </summary>
        public Work HisWork
        {
            get
            {
                return this._HisWork;
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        private Node _HisNode = null;
        /// <summary>
        /// 节点
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
        /// 工作流程
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
        /// 当前节点的工作是不是完成。
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

        #region 构造方法
        /// <summary>
        /// 建立一个工作节点事例.
        /// </summary>
        /// <param name="workId">工作ID</param>
        /// <param name="nodeId">节点ID</param>
        public WorkNode(Int64 workId, int nodeId)
        {
            this.WorkID = workId;
            Node nd = new Node(nodeId);
            Work wk = nd.HisWork;
            wk.OID = workId;
            wk.Retrieve();
            this._HisWork = wk;
            this._HisNode = nd;
        }
        /// <summary>
        /// 建立一个工作节点事例
        /// </summary>
        /// <param name="nd">节点ID</param>
        /// <param name="wk">工作</param>
        public WorkNode(Work wk, Node nd)
        {
            this.WorkID = wk.OID;

            //Node nd  = new Node(ndId);
            // if (nd.HisWorks.GetNewEntity.ToString() != wk.ToString())
            //   throw new Exception("@创建工作接点失败:定义节点［" + nd.Name + "］采集信息存放的实体[" + nd.WorksEnsName + "]，与数据实体[" + wk.ToString() + "]不一致．");
            this._HisWork = wk;
            this._HisNode = nd;
        }
        #endregion

        #region 运算属性
        private void Repair()
        {
        }
        public WorkNode GetPreviousWorkNode_FHL(Int64 workid)
        {
            WorkNodes wns = this.GetPreviousWorkNodes_FHL();
            foreach (WorkNode wn in wns)
            {
                if (wn.HisWork.OID == workid)
                    return wn;
            }
            return null;
        }
        public WorkNodes GetPreviousWorkNodes_FHL()
        {
            // 如果没有找到转向他的节点,就返回,当前的工作.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "此节点是开始节点,没有上一步工作")); //此节点是开始节点,没有上一步工作.

            if (this.HisNode.HisNodeWorkType == NodeWorkType.WorkHL
               || this.HisNode.HisNodeWorkType == NodeWorkType.WorkFHL)
            {
            }
            else
            {
                throw new Exception("@当前工作节 - 非是分合流节点。");
            }

            WorkNodes wns = new WorkNodes();
            Nodes nds = this.HisNode.HisFromNodes;
            foreach (Node nd in nds)
            {
                Works wks = (Works)nd.HisWorks;
                wks.Retrieve(WorkAttr.FID, this.HisWork.OID);

                if (wks.Count == 0)
                    continue;

                foreach (Work wk in wks)
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    wns.Add(wn);
                }
            }
            return wns;
        }
        /// <summary>
        /// 得当他的上一步工作
        /// 1, 从当前的找到他的上一步工作的节点集合.		 
        /// 如果没有找到转向他的节点,就返回,当前的工作.
        /// 
        /// </summary>
        /// <returns>得当他的上一步工作</returns>
        public WorkNode GetPreviousWorkNode()
        {
            // 如果没有找到转向他的节点,就返回,当前的工作.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "此节点是开始节点,没有上一步工作")); //此节点是开始节点,没有上一步工作.

            WorkNodes wns = new WorkNodes();
            Nodes nds = this.HisNode.HisFromNodes;
            foreach (Node nd in nds)
            {
                switch (this.HisNode.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL: /* 如果是合流 */
                        if (this.IsSubFlowWorkNode == false)
                        {
                            /* 如果不是线程 */
                            Node pnd = nd.HisPriFLNode;
                            if (pnd == null)
                                throw new Exception("@没有取道它的上一步骤的分流节点，请确认设计是否错误？");

                            Work wk1 = (Work)pnd.HisWorks.GetNewEntity;
                            wk1.OID = this.HisWork.OID;
                            if (wk1.RetrieveFromDBSources() == 0)
                                continue;
                            WorkNode wn11 = new WorkNode(wk1, pnd);
                            return wn11;
                            break;
                        }
                        break;
                    default:
                        break;
                }

                Work wk = (Work)nd.HisWorks.GetNewEntity;
                wk.OID = this.HisWork.OID;
                if (wk.RetrieveFromDBSources() == 0)
                    continue;

                WorkNode wn = new WorkNode(wk, nd);
                wns.Add(wn);
            }
            switch (wns.Count)
            {
                case 0:
                    throw new Exception(this.ToE("WN15", "没有找到他的上一步工作,系统错误。请通知管理员来处理，请上让上一步同事撤消发送、或者用本区县管理员用户登陆=》待办工作=》流程查询=》在关键字中输入Workid其它条件选择全部，查询到该流程删除它。") + "@WorkID=" + this.WorkID);
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
        /// 得当他的下一步工作.
        /// 如果当前工作在没有处理完毕状态,就返回当前的工作.
        /// </summary>
        /// <returns>得当他的下一步工作</returns>
        private WorkNode GetNextWorkNode()
        {
            // 如果当前工作在没有处理完毕状态,就返回当前的工作.
            if (this.HisWork.NodeState != NodeState.Complete)
                throw new Exception(this.ToE("WN16", "@此节点的工作任务还没有完成,没有下一步工作.")); //"@此节点的工作任务还没有完成,没有下一步工作."

            // 如果他是一个结束节点
            if (this.HisNode.IsEndNode)
                throw new Exception(this.ToE("ND17", "@此节点是结束节点,没有下一步工作.")); // "@此节点是结束节点,没有下一步工作."

            // throw new Exception("@当前的工作没有完成任务,这是最");
            Nodes nds = this.HisNode.HisToNodes;
            if (nds.Count == 0)
                throw new Exception("@没有找到从当前节点【" + this.HisNode.Name + "】的转向节点，可能这是一个最后节点，不能取道下一个工作节点。");

            foreach (Node nd in nds)
            {
                Work wk = (Work)nd.HisWorks.GetNewEntity;
                
                wk.OID = this.HisWork.OID;

                if (wk.IsExits == false)
                    continue;

                wk.Retrieve();
                WorkNode wn = new WorkNode(wk, nd);
                return wn;
            }

            if (nds.Count == 1)
            {
                /* 这一中情况是,工作表的记录被非法删除.
                 * 补救办法是:
                 * 1,把当前的流程的当前工作节点设置为当前的节点.,
                 * 2,节设置为工作人员.
                 * */
                // 判断是不是用下一步的工作人员列表.
                Node nd = (Node)nds[0];
                WorkerLists wls = new WorkerLists(this.HisWork.OID, nd.NodeID);
                if (wls.Count == 0)
                {
                    /*说名没有产生工作者列表.*/
                    this.HisWork.NodeState = NodeState.Init;
                    this.HisWork.DirectUpdate();
                    GenerWorkFlow wgf = new GenerWorkFlow(this.HisWork.OID);
                    throw new Exception("@当前的工作没有正确的处理, 没有下一步骤工作节点.也可能是你非法删除了工作表中的记录,造成的没有找到,下一步工作内容,但是系统已经恢复当前节点为为完成状态,此流程可以正常的运行下去.");
                }
                else
                {
                    /*说名已经产生工作者列表.*/
                    DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node=" + nd.NodeID);
                }
            }
            throw new Exception("@没有找到下一步骤工作节点[" + this.HisNode.Name + "]的下一步工作,流程错误.请把此问题发送给管理人员．");
        }
        #endregion
    }
    /// <summary>
    /// 工作节点集合.
    /// </summary>
    public class WorkNodes : CollectionBase
    {
        #region 构造
        /// <summary>
        /// 他的工作s
        /// </summary> 
        public Works GetWorks
        {
            get
            {
                if (this.Count == 0)
                    throw new Exception("@初始化失败，没有找到任何节点。");

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
        /// 工作节点集合
        /// </summary>
        public WorkNodes()
        {
        }

        public int GenerByFID(Flow flow, Int64 fid)
        {
            this.Clear();

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
                //if (nd.HisFNType == FNType.River)
                //    continue;

                Work wk = nd.GetWork(oid);
                if (wk == null)
                    continue;

                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }
        /// <summary>
        /// 删除工作流程
        /// </summary>
        public void DeleteWorks()
        {
            foreach (WorkNode wn in this)
            {
                wn.HisWork.Delete();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 增加一个WorkNode
        /// </summary>
        /// <param name="wn">工作 节点</param>
        public void Add(WorkNode wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// 根据位置取得数据
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

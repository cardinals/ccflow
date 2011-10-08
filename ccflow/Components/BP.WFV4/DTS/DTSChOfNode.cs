using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class DTSCHOfNode : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public DTSCHOfNode()
        {
            this.Title = "ͬ���������ݵ�wf_chofNode������С�";
            this.Help = "Ϊ��������";
            this.Warning = "ִ������Ҫһ��ʱ�������ĵȴ���";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "��ȷ��Ҫִ����";
            //HisAttrs.AddTBString("P1", null, "ԭ����", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "������", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "ȷ��", true, false, 0, 10, 10);
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            CHOfNode cn = new CHOfNode();
            cn.CheckPhysicsTable();

            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
                string sql = "";
                sql += "INSERT INTO WF_CHOFNODE(MyPK,FK_Node,WorkID,FID,RDT,FK_NY,CDT,REC,Emps,NodeState,FK_Dept) ";
                sql += "  (SELECT MyPK,FK_Node,OID,FID,RDT,FK_NY,CDT,REC,Emps,NodeState,FK_Dept FROM V" + fl.No + " WHERE MyPK NOT IN (SELECT MYPK FROM WF_CHOFNODE)  )";
                DBAccess.RunSQL(sql);
            }
            return "ִ�гɹ�....";
        }
    }
}

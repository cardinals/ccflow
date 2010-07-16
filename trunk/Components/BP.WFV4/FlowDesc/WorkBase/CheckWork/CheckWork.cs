
using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// ���״̬
    /// </summary>
    public enum CheckState
    {
        /// <summary>
        /// ��ͣ
        /// </summary>
        Pause = 2,
        /// <summary>
        /// ͬ��
        /// </summary>
        Agree = 1,
        /// <summary>
        /// ��ͬ��
        /// </summary>
        Dissent = 0
    }
    /// <summary>
    /// �ɼ���Ϣ��������
    /// </summary>
    public class CheckWorkAttr : WorkAttr
    {
        /// <summary>
        /// ���״̬( 0, ��ͬ��, 1, ��ʾͬ��, 2 ����) 
        /// </summary>
        public const string CheckState = "CheckState";
        /// <summary>
        /// NodeID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// �ο���Ϣ
        /// </summary>
        public const string RefMsg = "RefMsg";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
    }
    /// <summary>
    /// WorkBase ��ժҪ˵����
    /// �������̲ɼ���Ϣ�� ��˻���.
    /// </summary>
    abstract public class CheckWork : Work
    {
        #region ��������
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(CheckWorkAttr.Note);
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(CheckWorkAttr.Note);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.Note, value);
            }
        }
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public string RefMsg
        {
            get
            {
                return this.GetValStringByKey(CheckWorkAttr.RefMsg);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.RefMsg, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public CheckState HisCheckState
        {
            get
            {
                return (CheckState)this.CheckState;
            }
        }
        /// <summary>
        /// ���״̬( 0,��ͬ��,1,ͬ��,2����) 
        /// </summary>
        public CheckState CheckState
        {
            get
            {
                return (CheckState)this.GetValIntByKey(CheckWorkAttr.CheckState);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.CheckState, (int)value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(CheckWorkAttr.Sender);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.Sender, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ��������
        /// </summary>
        protected CheckWork()
        {

        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="oid">�������̵�ID</param>
        protected CheckWork(int oid) : base(oid) { }
        #endregion

        #region  ��д����ķ���
        #endregion

        #region ��������
        #endregion
    }
    /// <summary>
    /// ��˹����Ļ��� ����
    /// </summary>
    abstract public class CheckWorks : Works
    {
        #region ���췽��
        /// <summary>
        /// ��˹�������
        /// </summary>
        public CheckWorks()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="NodeId">�ڵ�ID</param>
        /// <param name="fromDateTime">��¼���ڴ�</param>
        /// <param name="toDateTime">��</param>
        public CheckWorks(int nodeId, string fromDateTime, string toDateTime)
        {
            this.NodeID = nodeId;
            QueryObject qo = new QueryObject(this);
            qo.Top = 100000;
            qo.AddWhere(CheckWorkAttr.RDT, ">=", fromDateTime);
            qo.addAnd();
            qo.AddWhere(CheckWorkAttr.RDT, "<=", toDateTime);
            qo.DoQuery();
        }
        public int NodeID = 0;
        #endregion


        #region ��������
        /// <summary>
        /// ��˽ڵ��ѯ 
        /// </summary>
        /// <param name="empId">������Ա(all)</param>
        /// <param name="nodeStat">�ڵ�״̬</param>
        /// <param name="nodeId">�ڵ�ID</param>
        /// <param name="fromdate">��¼���ڴ�</param>
        /// <param name="todate">��¼���ڵ�</param>
        /// <returns></returns>
        public int RetrieveCheckWork(string empId, string fromdate, string todate)
        {
            QueryObject qo = new QueryObject(this);
            if (empId == "all")
            {
                qo.AddWhere(WorkAttr.Rec, " in  ", "(" + Web.WebUser.HisEmpsOfPower.ToStringOfPK(",", true) + ")");
            }
            else
            {
                qo.AddWhere(WorkAttr.Rec, empId);
            }

            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, ">=", fromdate);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, "<=", todate);

            return qo.DoQuery();
        }
        /// <summary>
        /// ��˽ڵ��ѯ 
        /// </summary>
        /// <param name="empId">������Ա(all)</param>
        /// <param name="nodeStat">�ڵ�״̬</param>
        /// <param name="nodeId">�ڵ�ID</param>
        /// <param name="fromdate">��¼���ڴ�</param>
        /// <param name="todate">��¼���ڵ�</param>
        /// <returns></returns>
        public int Retrieve(string empId, int nodeId, string fromdate, string todate)
        {
            QueryObject qo = new QueryObject(this);
            if (empId == "all")
            {
                qo.AddWhere(WorkAttr.Rec, " in  ", "(" + Web.WebUser.HisEmpsOfPower.ToStringOfPK(",", true) + ")");
            }
            else
            {
                qo.AddWhere(WorkAttr.Rec, empId);
            }

            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, ">=", fromdate);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, "<=", todate);
            return qo.DoQuery();
        }
        #endregion
    }
}

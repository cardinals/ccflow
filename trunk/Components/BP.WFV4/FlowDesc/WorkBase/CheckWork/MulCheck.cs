using System;
using BP.En;
using BP.En;
using BP.DA;
using BP.Web;

namespace BP.WF
{
    /// <summary>
    /// ��ǩ��˽ڵ�����
    /// </summary>
    public class MulCheckAttr : CheckWorkAttr
    {
    }
    /// <summary>
    /// ��ǩ��˽ڵ�
    /// </summary>
    public class MulCheck : CheckWork
    {
        #region ����

        /// <summary>
        /// ��ǩ��˽ڵ�
        /// </summary>
        public MulCheck()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public MulCheck(int nodeid)
        {
            this.NodeID = nodeid;
        }
        /// <summary>
        /// ��ǩ��˽ڵ�
        /// </summary>
        /// <param name="oid"></param>
        public MulCheck(Int64 workid, int fk_node, string fk_emp)
        {
            this.NodeID = fk_node;
            this.OID = workid;
            this.Rec = fk_emp;
            this.Retrieve();
        }
        #endregion
    }
    /// <summary>
    /// ��ǩ��˽ڵ㼯��
    /// </summary>
    public class MulChecks : CheckWorks
    {
        #region ����
        /// <summary>
        /// ��׼���
        /// </summary>
        public MulChecks()
        {
        }
        /// <summary>
        /// MulChecks
        /// </summary>
        /// <param name="nodeid"></param>
        public MulChecks(int nodeid)
            : base(nodeid)
        {
        }
        /// <summary>
        /// MulChecks
        /// </summary>
        /// <param name="nodeid">nodeid</param>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        public MulChecks(int nodeid, string from, string to)
            : base(nodeid, from, to)
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.NodeID == 0)
                    return new MulCheck();
                return new MulCheck(this.NodeID);
            }
        }
        #endregion
    }
}

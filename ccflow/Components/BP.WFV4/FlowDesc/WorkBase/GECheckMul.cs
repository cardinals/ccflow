
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// ��˹���
    /// </summary>
    public class GECheckMulAttr : WorkAttr
    {
    }
    /// <summary>
    /// ��˹���
    /// </summary>
    public class GECheckMul : GECheckStand
    {
        #region ���캯��
        /// <summary>
        /// ��˹���
        /// </summary>
        public GECheckMul()
        {
        }
        /// <summary>
        /// ��˹���
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        public GECheckMul(int nodeid)
        {
            this.NodeID = nodeid;
            this.SQLCash = null;
        }
        /// <summary>
        /// ��˹���
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="_oid">OID</param>
        public GECheckMul(int nodeid, Int64 _oid)
        {
            this.NodeID = nodeid;
            this.OID = _oid;
            this.SQLCash = null;
        }
        #endregion
    }
    /// <summary>
    /// ��˹���s
    /// </summary>
    public class GECheckMuls : GECheckStands
    {
        #region ���ػ��෽��
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID = 0;
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.NodeID == 0)
                    return new GECheckMul();
                return new GECheckMul(this.NodeID);
            }
        }
        /// <summary>
        /// ��˹���ID
        /// </summary>
        public GECheckMuls()
        {
        }
        /// <summary>
        /// ��˹���ID
        /// </summary>
        /// <param name="nodeid"></param>
        public GECheckMuls(int nodeid)
        {
            this.NodeID = nodeid;
        }
        #endregion
    }
}

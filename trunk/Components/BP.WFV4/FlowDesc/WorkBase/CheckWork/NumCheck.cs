using System;
using BP.En;
using BP.En;
using BP.DA;

namespace BP.WF
{
	/// <summary>
	/// ������˹���������
	/// </summary>
    public class NumCheckAttr : CheckWorkAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Num = "Num";
    }
	/// <summary>
	/// SimpleCheckWork ��ժҪ˵����
	/// ������˹�����
	/// </summary>
    public class NumCheck : CheckWork
    {
        #region ��������
        /// <summary>
        /// ���Num
        /// </summary>
        public float Num
        {
            get
            {
                return this.GetValFloatByKey(NumCheckAttr.Num);
            }
            set
            {
                this.SetValByKey(NumCheckAttr.Num, value);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ������˹�����
        /// </summary>
        public NumCheck()
        {
        }
        /// <summary>
        /// ������˹�����
        /// </summary>
        /// <param name="wfoid">��������ID</param>
        public NumCheck(int wfoid)
        {
            this.OID = wfoid;
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NumCheckAttr.OID, this.OID);
            if (qo.DoQuery() > 1)
                throw new Exception("@�˹�����������������������˽ڵ�,�����ô˷����õ���˵Ľ��.����� new NumCheck(oid, nodeId) ���� ");
        }
        /// <summary>
        /// ������˹�����
        /// </summary>
        /// <param name="oid">��������ID</param>
        /// <param name="nodeId">�ڵ�ID</param>
        public NumCheck(int oid, int nodeId)
        {
            this.OID = oid;
            this.NodeID = nodeId;
            this.Retrieve();
        }
        /// <summary>
        /// ����
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NumCheck");
                map.EnDesc = "���������ڵ�";

               // map.AddTBString(NumCheckAttr.FK_Taxpayer, null, "��˰�˱��", true, true, 0, 30, 100);
               // map.AddTBString(NumCheckAttr.TaxpayerName, null, "����", true, true, 0, 150, 100);

                map.AddDDLSysEnum(NumCheckAttr.CheckState, 1, "���״̬", true, false);
                map.AddTBInt(NumCheckAttr.Num, 0, "��˽��", true, false);

                map.AddTBStringDoc(NumCheckAttr.Note, "ͬ��.", "������", true, false);
                map.AddTBStringDoc(CheckWorkAttr.RefMsg, null, "������Ϣ", true, true);

                map.AddDDLEntities(NumCheckAttr.Sender, Web.WebUser.No, "������", new Port.Emps(), false);
                map.AddTBDateTime(NumCheckAttr.RDT, "��������", true, true);

                map.AddDDLEntities(NumCheckAttr.Rec, Web.WebUser.No, "�����", new Port.Emps(), false);
                map.AddTBInt(NumCheckAttr.NodeState, 0, "NodeState", false, true);

                map.AddTBDateTime(StandardCheckAttr.CDT, "��", "�������", true, true);
               // map.AddTBString(NumCheckAttr.FK_Taxpayer, null, "FK_Taxpayer", false, false, 0, 100, 100);

                map.AddTBIntPK(NumCheckAttr.OID, 0, "��������ID", false, true);
                map.AddTBInt(NumCheckAttr.FID, 0, "FID", false, true);

                map.AddTBIntPK(NumCheckAttr.NodeID, 0, "�ڵ�ID", false, true);
                map.AddTBString(StandardCheckAttr.Emps, null, "Emps", false, false, 0, 500, 100);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    
    }
	/// <summary>
	/// ������˹����༯��
	/// </summary>
    public class NumChecks : CheckWorks
    {
        #region ����
        #endregion

        /// <summary>
        /// ��׼���
        /// </summary>
        public NumChecks()
        {
        }
        /// <summary>
        /// ���ɹ����ڵ�
        /// </summary>
        /// <param name="nodeid"></param>
        public NumChecks(int nodeid)
            : base(nodeid)
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="nodeid">nodeid</param>
        /// <param name="fromDateTime">fromDateTime</param>
        /// <param name="toDateTime">toDateTime</param>
        public NumChecks(int nodeid, string fromDateTime, string toDateTime)
            : base(nodeid, fromDateTime, toDateTime)
        {
        }
        /// <summary>
        /// �����б�s
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NumCheck();
            }
        }
    }
}

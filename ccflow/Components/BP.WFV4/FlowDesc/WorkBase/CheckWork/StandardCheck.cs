using System;
using BP.En;
using BP.En;
using BP.DA;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// ��׼����˹���������
	/// </summary>
    public class GECheckStandAttr : GECheckStandAttr
    {

    }
	/// <summary>
	/// SimpleGECheckStand ��ժҪ˵����
	/// ��׼����˹�����
	/// </summary>
    public class GECheckStand :GECheckStand
    {
        #region ����
        /// <summary>
        /// ��׼����˹�����
        /// </summary>
        public GECheckStand()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public GECheckStand(int nodeid)
        {
            this.NodeID = nodeid;
        }
        /// <summary>
        /// ��׼����˹�����
        /// </summary>
        /// <param name="oid"></param>
        public GECheckStand(Int64 workid, int fk_node)
        {
            this.NodeID = fk_node;
            this.OID = workid;

            this.MyPK = fk_node + "_" + workid;
            this.Retrieve();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            this.MyPK = this.NodeID + "_" + this.OID;
            return base.beforeInsert();
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

                Map map = new Map("WF_GECheckStand");
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��׼�����ڵ�";

                map.AddMyPK();

                map.AddTBInt(NumCheckAttr.NodeID, 0, "�ڵ�ID", false, true);
                map.AddTBInt(NumCheckAttr.OID, 0, "����ID", false, true);
                map.AddTBInt(NumCheckAttr.FID, 0, "FID", false, true);

             
                map.AddDDLEntities(NumCheckAttr.Sender, null, "������", new Port.Emps(), false);
                map.AddDDLSysEnum(NumCheckAttr.CheckState, 1, "���״̬", true, false);

                map.AddTBStringDoc(NumCheckAttr.Note, "ͬ��.", "������", true, false);
                map.AddTBStringDoc(GECheckStandAttr.RefMsg, null, "������Ϣ", true, true);

                map.AddTBDateTime(NumCheckAttr.RDT, "��������", true, true);
                map.AddDDLEntities(GECheckStandAttr.Rec, null, "�����", new Port.Emps(), false);

                map.AddTBDateTime(NumCheckAttr.RDT, "��¼����", false, true);
                map.AddTBInt(NumCheckAttr.NodeState, 0, "NodeState", false, true);

                map.AddTBDateTime(GECheckStandAttr.CDT, "�������", false, true);
          
                map.AddTBString(GECheckStandAttr.Emps, null, "Emps", false, false, 0, 500, 100);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public override int RetrieveFromDBSources()
        {
            this.MyPK = this.NodeID + "_" + this.OID;
            return base.RetrieveFromDBSources();
        }
    }
	/// <summary>
	/// ��׼����˹����༯��
	/// </summary>
    public class GECheckStands : GECheckStands
	{
		#region ����
		/// <summary>
		/// ��׼���
		/// </summary>
		public GECheckStands()
		{
		}
		/// <summary>
		/// GECheckStands
		/// </summary>
		/// <param name="nodeid"></param>
		public GECheckStands(int nodeid):base(nodeid)
		{
		}
		/// <summary>
		/// GECheckStands
		/// </summary>
		/// <param name="nodeid">nodeid</param>
		/// <param name="from">from</param>
		/// <param name="to">to</param>
		public GECheckStands(int nodeid,string from ,string to):base(nodeid,from,to)
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
                    return new GECheckStand();
                return new GECheckStand(this.NodeID);
            }
        }
		#endregion
	}
}

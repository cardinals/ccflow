using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
//using BP.ZHZS.Base;

namespace BP.WF
{
    /// <summary>
    /// ���̸�λ��������	  
    /// </summary>
    public class FlowStationAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ������λ
        /// </summary>
        public const string FK_Station = "FK_Station";
    }
    /// <summary>
    /// ���̸�λ����
    /// �ڵ�Ĺ�����λ�����������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class FlowStation : EntityMM
    {
        #region ��������
        /// <summary>
        /// HisUAC
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        ///�ڵ�
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(FlowStationAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(FlowStationAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ������λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(FlowStationAttr.FK_Station);
            }
            set
            {
                this.SetValByKey(FlowStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        public FlowStation() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FlowStation");
                map.EnDesc = "���̸�λ������Ϣ";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddDDLEntitiesPK(FlowStationAttr.FK_Flow, null, "FK_Flow", new Flows(), true);
                map.AddDDLEntitiesPK(FlowStationAttr.FK_Station, null, "������λ", new Stations(), true);
                this._enMap = map;

                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���̸�λ����
    /// </summary>
    public class FlowStations : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ�����λ
        /// </summary>
        public Stations HisStations
        {
            get
            {
                Stations ens = new Stations();
                foreach (FlowStation ns in this)
                {
                    ens.AddEntity(new Station(ns.FK_Station));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (FlowStation ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Flow));
                }
                return ens;

            }
        }
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        public FlowStations() { }
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public FlowStations(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowStationAttr.FK_Flow, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        /// <param name="StationNo">StationNo </param>
        public FlowStations(string StationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowStationAttr.FK_Station, StationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowStation();
            }
        }
        /// <summary>
        /// ȡ��һ��������λ�����ܹ����ʵ��Ľڵ�s
        /// </summary>
        /// <param name="sts">������λ����</param>
        /// <returns></returns>
        public Nodes GetHisNodes(Stations sts)
        {
            Nodes nds = new Nodes();
            Nodes tmp = new Nodes();
            foreach (Station st in sts)
            {
                tmp = this.GetHisNodes(st.No);
                foreach (Node nd in tmp)
                {
                    if (nds.Contains(nd))
                        continue;
                    nds.AddEntity(nd);
                }
            }
            return nds;
        }
        /// <summary>
        /// ȡ��һ��������Ա�ܹ����ʵ��Ľڵ㡣
        /// </summary>
        /// <param name="empId">������ԱID</param>
        /// <returns></returns>
        public Nodes GetHisNodes_del(string empId)
        {
            Emp em = new Emp(empId);
            return this.GetHisNodes(em.HisStations);
        }
        /// <summary>
        /// ������λ��Ӧ�Ľڵ�
        /// </summary>
        /// <param name="stationNo">������λ���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowStationAttr.FK_Station, stationNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (FlowStation en in this)
            {
                ens.AddEntity(new Node(en.FK_Flow));
            }
            return ens;
        }
        /// <summary>
        /// ת��˽ڵ�ļ��ϵ�Nodes
        /// </summary>
        /// <param name="nodeID">�˽ڵ��ID</param>
        /// <returns>ת��˽ڵ�ļ��ϵ�Nodes (FromNodes)</returns> 
        public Stations GetHisStations(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowStationAttr.FK_Flow, nodeID);
            qo.DoQuery();

            Stations ens = new Stations();
            foreach (FlowStation en in this)
            {
                ens.AddEntity(new Station(en.FK_Station));
            }
            return ens;
        }
    }
}

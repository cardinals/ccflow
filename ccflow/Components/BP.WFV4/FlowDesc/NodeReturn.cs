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
    public class NodeReturnAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string ReturnN = "ReturnN";
    }
    /// <summary>
    /// ���̸�λ����
    /// �ڵ�Ĺ����ڵ������������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class NodeReturn : EntityMM
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
        public int ReturnN
        {
            get
            {
                return this.GetValIntByKey(NodeReturnAttr.ReturnN);
            }
            set
            {
                this.SetValByKey(NodeReturnAttr.ReturnN, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(NodeReturnAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeReturnAttr.FK_Node, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        public NodeReturn() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeReturn");
                map.EnDesc = "���˻صĽڵ�";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(NodeReturnAttr.FK_Node, null, "���̱��", true, true, 1, 20, 20);
                map.AddDDLEntitiesPK(NodeReturnAttr.ReturnN, null, "�ڵ�", new NodeExts(), true);
                this._enMap = map;
                
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���˻صĽڵ�
    /// </summary>
    public class NodeReturns : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (NodeReturn ns in this)
                {
                    ens.AddEntity(new Node(ns.ReturnN));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���˻صĽڵ�
        /// </summary>
        public NodeReturns() { }
        /// <summary>
        /// ���˻صĽڵ�
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public NodeReturns(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���˻صĽڵ�
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public NodeReturns(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.ReturnN, NodeNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeReturn();
            }
        }
        /// <summary>
        /// ���˻صĽڵ�s
        /// </summary>
        /// <param name="sts">���˻صĽڵ�</param>
        /// <returns></returns>
        public Nodes GetHisNodes(Nodes sts)
        {
            Nodes nds = new Nodes();
            Nodes tmp = new Nodes();
            foreach (Node st in sts)
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
        /// ���˻صĽڵ�
        /// </summary>
        /// <param name="NodeNo">�����ڵ���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.ReturnN, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeReturn en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
        /// <summary>
        /// ת��˽ڵ�ļ��ϵ�Nodes
        /// </summary>
        /// <param name="nodeID">�˽ڵ��ID</param>
        /// <returns>ת��˽ڵ�ļ��ϵ�Nodes (FromNodes)</returns> 
        public Nodes GetHisNodes(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.FK_Node, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeReturn en in this)
            {
                ens.AddEntity(new Node(en.ReturnN));
            }
            return ens;
        }
    }
}

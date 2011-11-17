using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.WF;

namespace BP.PRJ
{
    /// <summary>
    /// ���̸�λ��������	  
    /// </summary>
    public class NodeRuleAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Rule = "FK_Rule";
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
    }
    /// <summary>
    /// ���̸�λ����
    /// �ڵ�Ĺ����ڵ������������.	 
    /// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
    /// Ҳ��¼�˵�����ڵ�������Ľڵ�.
    /// </summary>
    public class NodeRule : EntityMM
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
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(NodeRuleAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeRuleAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_Rule
        {
            get
            {
                return this.GetValStringByKey(NodeRuleAttr.FK_Rule);
            }
            set
            {
                this.SetValByKey(NodeRuleAttr.FK_Rule, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ���̸�λ����
        /// </summary>
        public NodeRule() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Prj_NodeRule");
                map.EnDesc = "�ϴ�����";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(NodeRuleAttr.FK_Rule, null, "�ϴ�����", true, true, 1, 20, 20);
                map.AddTBStringPK(NodeRuleAttr.FK_Node, null, "�ڵ�", true, true, 1, 20, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���̳��ͽڵ�
    /// </summary>
    public class NodeRules : EntitiesMM
    {
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Rules HisNodes
        {
            get
            {
                Rules ens = new Rules();
                foreach (NodeRule ns in this)
                {
                    ens.AddEntity(new Rule(ns.FK_Node));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���̳��ͽڵ�
        /// </summary>
        public NodeRules() { }
        /// <summary>
        /// ���̳��ͽڵ�
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        public NodeRules(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRuleAttr.FK_Rule, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// ���̳��ͽڵ�
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public NodeRules(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRuleAttr.FK_Node, NodeNo);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeRule();
            }
        }
        /// <summary>
        /// ���̳��ͽڵ�
        /// </summary>
        /// <param name="NodeNo">�����ڵ���</param>
        /// <returns>�ڵ�s</returns>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRuleAttr.FK_Node, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeRule en in this)
            {
                ens.AddEntity(new Node(en.FK_Rule));
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
            qo.AddWhere(NodeRuleAttr.FK_Rule, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeRule en in this)
                ens.AddEntity(new Node(en.FK_Node));

            return ens;
        }
    }
}

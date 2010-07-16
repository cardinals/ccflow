using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    public enum DLink
    {
        AppCenterDSN,
        Oracle,
        SQLServer,
        Ole,
        ODBC
    }
    /// <summary>
    /// ����ת��
    /// </summary>
    public class ExpAttr:BP.En.EntityOIDNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string DLink = "DLink";
        /// <summary>
        /// ����
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// �ڵ���
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// x
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// y
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// DTSWhen
        /// </summary>
        public const string DTSWhen = "DTSWhen";
        public const string ExpDesc = "ExpDesc";
        public const string RefTable = "RefTable";
        public const string IsEnable = "IsEnable";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ⲿ�������õ���Ϣ.	 
    /// </summary>
    public class Exp : EntityNoName
    {
        #region ��������
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsUpdate = true;
                return uac;
            }
        }
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(ExpAttr.NodeID);
            }
            set
            {
                this.SetValByKey(ExpAttr.NodeID, value);
            }
        }
      
        /// <summary>
        /// ��ʾʱ��
        /// </summary>
        public int DTSWhen
        {
            get
            {
                return this.GetValIntByKey(ExpAttr.DTSWhen);
            }
            set
            {
                this.SetValByKey(ExpAttr.DTSWhen, value);
            }
        }
        public string DTSWhenT
        {
            get
            {
                return this.GetValRefTextByKey(ExpAttr.DTSWhen);
            }
        }
        public string ExpDesc
        {
            get
            {
                return this.GetValStringByKey(ExpAttr.ExpDesc);
            }
            set
            {
                SetValByKey(ExpAttr.ExpDesc, value);
            }
        }
        public string RefTable
        {
            get
            {
                return this.GetValStringByKey(ExpAttr.RefTable);
            }
            set
            {
                SetValByKey(ExpAttr.RefTable, value);
            }
        }

        public DLink DLink
        {
            get
            {
                return (DLink)this.GetValIntByKey(ExpAttr.DLink);
            }
            set
            {
                SetValByKey(ExpAttr.DLink, (int)value);
            }
        }
        public string DLinkT
        {
            get
            {
                return this.GetValRefTextByKey(ExpAttr.DLink);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        public Exp() { }
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        /// <param name="_oid">�ⲿ��������ID</param>	
        public Exp(string id)
        {
            this.No = id;
            int i =this.RetrieveFromDBSources();
            if (i == 0)
            {
                Flow f = new Flow(id);
                this.Name = f.Name;
                this.Insert();
            }

        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_Exp");
                map.EnDesc = "����ת��";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                //      map.AddTBIntPKOID();
                map.AddTBStringPK(ExpAttr.No, null, "���̱��", false, true, 0, 100, 10);
                map.AddTBString(ExpAttr.Name, null, "��������", true, false, 0, 400, 10);
                map.AddTBInt(ExpAttr.NodeID, 0, "NodeID", false, false);
                map.AddTBInt(ExpAttr.IsEnable, 0, "�Ƿ�����", false, false);

                map.AddDDLSysEnum(ExpAttr.DTSWhen, 0, "����ʱ��", true, false, ExpAttr.DTSWhen, "@0=�����̽���ʱ@1=����ؽڵ�ɹ����ͺ�");
                map.AddDDLSysEnum(ExpAttr.DLink, 0, "����Դ", true, false, ExpAttr.DLink, "@0=AppCenterDSN(���ؿ�)@1=Oracle����@2=SQLServer����@3=Ole����@4=ODBC����");
                map.AddTBString(ExpAttr.RefTable, null, "�����", true, false, 0, 20, 10);
                map.AddTBString(ExpAttr.ExpDesc, null, "˵��", false, true, 0, 100, 10);

                //map.AddTBInt(ExpAttr.H, 0, "���ڸ߶�", false, false);
                //map.AddTBInt(ExpAttr.W, 0, "���ڿ��", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ����ת��
    /// </summary>
    public class Exps : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Exp();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����ת��
        /// </summary>
        public Exps()
        {
        }
        /// <summary>
        /// ����ת��.
        /// </summary>
        /// <param name="FlowNo"></param>
        public Exps(string fk_flow)
        {
            //this.Retrieve(NodeAttr.No, fk_flow);
        }
        public Exps(int nodeid )
        {
            ///this.Retrieve(NodeAttr.NodeID, nodeid);
        }
        #endregion
    }
}

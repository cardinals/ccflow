using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// �ⲿ������������
    /// </summary>
    public class FAppSetAttr:BP.En.EntityOIDNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string AppType = "AppType";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
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
        /// ShowTime
        /// </summary>
        public const string ShowTime = "ShowTime";
        public const string DoWhat = "DoWhat";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ⲿ�������õ���Ϣ.	 
    /// </summary>
    public class FAppSet : EntityOIDName
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
                return this.GetValIntByKey(FAppSetAttr.NodeID);
            }
            set
            {
                this.SetValByKey(FAppSetAttr.NodeID, value);
            }
        }
        /// <summary>
        /// x
        /// </summary>
        public int H
        {
            get
            {
                return this.GetValIntByKey(FAppSetAttr.H);
            }
            set
            {
                this.SetValByKey(FAppSetAttr.H, value);
            }
        }
        /// <summary>
        /// y
        /// </summary>
        public int W
        {
            get
            {
                return this.GetValIntByKey(FAppSetAttr.W);
            }
            set
            {
                this.SetValByKey(FAppSetAttr.W, value);
            }
        }
        /// <summary>
        /// ��ʾʱ��
        /// </summary>
        public int ShowTime_
        {
            get
            {
                return this.GetValIntByKey(FAppSetAttr.ShowTime );
            }
            set
            {
                this.SetValByKey(FAppSetAttr.ShowTime, value);
            }
        }
        public string ShowTimeT_del
        {
            get
            {
                return this.GetValRefTextByKey(FAppSetAttr.ShowTime);
            }
        }
        /// <summary>
        /// �ⲿ�������õ�������
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FK_Flow);
            }
            set
            {
                SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        public string DoWhat
        {
            get
            {
                return this.GetValStringByKey(FAppSetAttr.DoWhat);
            }
            set
            {
                SetValByKey(FAppSetAttr.DoWhat, value);
            }
        }
        
        public string AppType
        {
            get
            {
                return this.GetValStringByKey(FAppSetAttr.AppType);
            }
            set
            {
                SetValByKey(FAppSetAttr.AppType, value);
            }
        }
        public string AppTypeT
        {
            get
            {
                return this.GetValRefTextByKey(FAppSetAttr.AppType);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        public FAppSet() { }
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        /// <param name="_oid">�ⲿ��������ID</param>	
        public FAppSet(int _oid)
        {
            this.OID = _oid;
            this.Retrieve();
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
                Map map = new Map("WF_FAppSet");
                map.EnDesc = "�ⲿ��������";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBString(NodeAttr.Name, null, "��ʾ��ǩ", true, false, 0, 400, 10);
                map.AddTBInt(FAppSetAttr.NodeID, 0, "NodeID", false, false);

                map.AddDDLSysEnum(FAppSetAttr.AppType, 0, "Ӧ������", true, false, FAppSetAttr.AppType,"@0=�ⲿUrl����@1=���ؿ�ִ���ļ�");

                //map.AddDDLSysEnum(FAppSetAttr.ShowTime, 0, "����ʱ��", true, false, FAppSetAttr.ShowTime, 
                //    "@0=��(��ʾ�ڱ��ײ�)@1=������ѡ��ʱ@2=������ʱ@3=������ʱ");

                map.AddTBString(FAppSetAttr.FK_Flow, null, "���̱��", false, true, 0, 100, 10);
                map.AddTBString(FAppSetAttr.DoWhat, null, "ִ��ʲô��", false, true, 0, 100, 10);

                map.AddTBInt(FAppSetAttr.H, 0, "���ڸ߶�", false, false);
                map.AddTBInt(FAppSetAttr.W, 0, "���ڿ��", false, false);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// �ⲿ�������ü���
    /// </summary>
    public class FAppSets : EntitiesOID
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FAppSet();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ⲿ�������ü���
        /// </summary>
        public FAppSets()
        {
        }
        /// <summary>
        /// �ⲿ�������ü���.
        /// </summary>
        /// <param name="FlowNo"></param>
        public FAppSets(string fk_flow)
        {
            this.Retrieve(NodeAttr.FK_Flow, fk_flow);
        }
        public FAppSets(int nodeid)
        {
            this.Retrieve(NodeAttr.NodeID, nodeid);
        }
        #endregion
    }
}

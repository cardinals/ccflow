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
    /// ���̱�������
    /// </summary>
    public class RptAttrAttr:BP.En.EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string RefAttrOID = "RefAttrOID";
        /// <summary>
        /// RefFieldName
        /// </summary>
        public const string RefFieldName = "RefFieldName";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ִ��
        /// </summary>
        public const string FK_Rpt = "FK_Rpt";
        public const string Field = "Field";
        public const string FieldName = "FieldName";
        public const string FieldNameRpt = "FieldNameRpt";
        public const string IsCanDel = "IsCanDel";
        public const string FK_Flow = "FK_Flow";
        public const string RefTable = "RefTable";
        public const string RefField = "RefField";
        public const string IsCanEdit = "IsCanEdit";
        public const string IDX = "IDX";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ⲿ�������õ���Ϣ.	 
    /// </summary>
    public class RptAttr : EntityMyPK
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
        /// <summary>
        /// �ⲿ�������õ�������
        /// </summary>
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.FK_Node);
            }
            set
            {
                SetValByKey(RptAttrAttr.FK_Node, value);
            }
        }
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(RptAttrAttr.IDX);
            }
            set
            {
                SetValByKey(RptAttrAttr.IDX, value);
            }
        }
        public int RefAttrOID
        {
            get
            {
                return this.GetValIntByKey(RptAttrAttr.RefAttrOID);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefAttrOID, value);
            }
        }
        public string RefField
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.RefField);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefField, value);
            }
        }
        public string RefFieldName
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.RefFieldName);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefFieldName, value);
            }
        }
        public string FieldName
        {
            get
            {
                string s = this.GetValStringByKey(RptAttrAttr.FieldName);
                if (s == "")
                    return this.RefFieldName;
                return s;
            }
            set
            {
                SetValByKey(RptAttrAttr.FieldName, value);
            }
        }
        public string RefTable
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.RefTable);
            }
            set
            {
                SetValByKey(RptAttrAttr.RefTable, value);
            }
        }
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public bool IsCanDel
        {
            get
            {
                return this.GetValBooleanByKey(RptAttrAttr.IsCanDel);
            }
            set
            {
                this.SetValByKey(RptAttrAttr.IsCanDel, value);
            }
        }
        public bool IsCanEdit
        {
            get
            {
                return this.GetValBooleanByKey(RptAttrAttr.IsCanEdit);
            }
            set
            {
                this.SetValByKey(RptAttrAttr.IsCanEdit, value);
            }
        }
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptAttrAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptAttrAttr.FK_Rpt, value);
            }
        }
        
        public string Field
        {
            get
            {
                string s =  this.GetValStringByKey(RptAttrAttr.Field);
                if (s == "")
                    return this.RefField;
                return s;
            }
            set
            {
                SetValByKey(RptAttrAttr.Field, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                return this.GetValRefTextByKey(RptAttrAttr.FK_Node);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        public RptAttr() { }
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        /// <param name="_oid">�ⲿ��������ID</param>	
        public RptAttr(string _oid)
        {
            this.MyPK = _oid;
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
                Map map = new Map("WF_RptAttr");
                map.EnDesc = "������ͼ����";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();  

                map.AddTBString(RptAttrAttr.FK_Rpt, null, "����", false, true, 0, 100, 10);
                map.AddDDLEntities(RptAttrAttr.FK_Node, null, "�ڵ�", new NodeExts(), false);

                map.AddTBInt(RptAttrAttr.RefAttrOID, 0, "����ID", false, false);
                map.AddTBString(RptAttrAttr.RefTable, null, "�����", true, false, 0, 20, 10);
                map.AddTBString(RptAttrAttr.RefField, null, "�ֶ�", true, false, 0, 20, 10);
                map.AddTBString(RptAttrAttr.RefFieldName, null, "�ֶ�����", true, false, 0, 200, 10);

                map.AddTBString(RptAttrAttr.Field, null, "�ֶ�(ת�����)", true, false, 0, 20, 10);
                map.AddTBString(RptAttrAttr.FieldName, null, "�ֶ�����(ת�����)", true, false, 0, 200, 10);

                map.AddTBInt(RptAttrAttr.IsCanDel, 0, "�ɷ�ɾ��", false, false);
                map.AddTBInt(RptAttrAttr.IsCanEdit, 0, "�ɷ�༭", false, false);

                map.AddTBInt(RptAttrAttr.IDX, 0, "���", true, false);

                //map.AddTBInt(RptAttrAttr.NodeID, 0, "NodeID", false, false);
                //map.AddDDLSysEnum(RptAttrAttr.ShowTime, 0, "����ʱ��", true, false, RptAttrAttr.ShowTime, "@0=��(��ʾ�ڱ��ײ�)@1=������ѡ��ʱ@2=������ʱ@3=������ʱ");
                //map.AddTBString(RptAttrAttr.FK_Node, null, "���̱��", false, true, 0, 100, 10);
                //map.AddTBString(RptAttrAttr.DoWhat, null, "ִ��ʲô��", false, true, 0, 100, 10);
                //map.AddTBInt(RptAttrAttr.H, 0, "���ڸ߶�", false, false);
                //map.AddTBInt(RptAttrAttr.W, 0, "���ڿ��", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

       

        protected override void afterDelete()
        {
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            attr.Delete(BP.Sys.MapAttrAttr.FK_MapData, this.FK_Rpt,
                BP.Sys.MapAttrAttr.KeyOfEn, this.Field);

            base.afterDelete();
        }
 
        #region ˳��
        /// <summary>
        /// �ȵ�������
        /// </summary>
        private void DoOrder()
        {
            RptAttrs attrs = new RptAttrs(this.FK_Rpt);
            int i = 0;
            foreach (RptAttr attr in attrs)
            {
                i++;
                attr.IDX = i;
                attr.Update(RptAttrAttr.IDX, attr.IDX);
            }
        }
        public string DoUp()
        {
            this.DoOrder();

            this.RetrieveFromDBSources();
            if (this.IDX == 1)
                return null;

            RptAttrs attrs = new RptAttrs(this.FK_Rpt);
            RptAttr attrUp = (RptAttr)attrs[this.IDX - 1 - 1];
            attrUp.Update(RptAttrAttr.IDX, this.IDX);
            this.Update(RptAttrAttr.IDX, this.IDX - 1);
            return null;
        }
        public string DoDown()
        {
            this.DoOrder();
            this.RetrieveFromDBSources();

            RptAttrs attrs = new RptAttrs(this.FK_Rpt);
            if (this.IDX == attrs.Count)
                return null;

            RptAttr attrDown = (RptAttr)attrs[this.IDX];
            attrDown.Update(RptAttrAttr.IDX, this.IDX);
            this.Update(RptAttrAttr.IDX, this.IDX + 1);
            return null;
        }
        #endregion
    }
    /// <summary>
    /// �ⲿ�������ü���
    /// </summary>
    public class RptAttrs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new RptAttr();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ⲿ�������ü���
        /// </summary>
        public RptAttrs()
        {
        }
        /// <summary>
        /// �ⲿ�������ü���.
        /// </summary>
        /// <param name="FlowNo"></param>
        public RptAttrs(string fk_rpt)
        {
            int i = this.Retrieve(RptAttrAttr.FK_Rpt, fk_rpt, RptAttrAttr.IDX);
        }
        public RptAttrs(int nodeid)
        {
            this.Retrieve(RptAttrAttr.FK_Node, nodeid, RptAttrAttr.IDX);
        }
        #endregion
    }
}

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
    public class ExpDtlAttr 
    {
        #region ��������
        public const string MyPK = "MyPK";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string Field = "Field";
        /// <summary>
        /// �ֶ�����
        /// </summary>
        public const string FieldName = "FieldName";
        /// <summary>
        /// �Է��ֶ�
        /// </summary>
        public const string RefField = "RefField";
        /// <summary>
        /// ���
        /// </summary>
        public const string IDX = "IDX";
        public const string FK_Exp = "FK_Exp";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ⲿ�������õ���Ϣ.	 
    /// </summary>
    public class ExpDtl : EntityMyPK
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
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(ExpDtlAttr.FK_Node);
            }
            set
            {
                SetValByKey(ExpDtlAttr.FK_Node, value);
            }
        }
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(ExpDtlAttr.IDX);
            }
            set
            {
                SetValByKey(ExpDtlAttr.IDX, value);
            }
        }
         public string RefField
        {
            get
            {
                return this.GetValStringByKey(ExpDtlAttr.RefField);
            }
            set
            {
                SetValByKey(ExpDtlAttr.RefField, value);
            }
        }
        public string FK_Exp
        {
            get
            {
                return this.GetValStringByKey(ExpDtlAttr.FK_Exp);
            }
            set
            {
                SetValByKey(ExpDtlAttr.FK_Exp, value);
            }
        }
     
        public string FieldName
        {
            get
            {
                string s = this.GetValStringByKey(ExpDtlAttr.FieldName);
                //if (s == "")
                //    return this.RefFieldName;
                return s;
            }
            set
            {
                SetValByKey(ExpDtlAttr.FieldName, value);
            }
        }
     
        
        public string Field
        {
            get
            {
                string s =  this.GetValStringByKey(ExpDtlAttr.Field);
                if (s == "")
                    return this.RefField;
                return s;
            }
            set
            {
                SetValByKey(ExpDtlAttr.Field, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                return this.GetValRefTextByKey(ExpDtlAttr.FK_Node);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        public ExpDtl() { }
        /// <summary>
        /// �ⲿ��������
        /// </summary>
        /// <param name="_oid">�ⲿ��������ID</param>	
        public ExpDtl(string _oid)
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
                Map map = new Map("WF_ExpDtl");
                map.EnDesc = "������ͼ����";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();  /* FK_Node +"_"+ RefOID */

                //map.AddTBString(ExpDtlAttr.FK_Exp, null, "����", false, true, 0, 100, 10);
                //map.AddDDLEntities(ExpDtlAttr.FK_Node, null, "�ڵ�", new NodeExts(), false);
                //map.AddTBInt(ExpDtlAttr.RefAttrOID, 0, "����ID", false, false);
                //map.AddTBString(ExpDtlAttr.RefTable, null, "�����", true, false, 0, 20, 10);
                //map.AddTBString(ExpDtlAttr.RefField, null, "�ֶ�", true, false, 0, 20, 10);
                //map.AddTBString(ExpDtlAttr.RefFieldName, null, "�ֶ�����", true, false, 0, 200, 10);

                //map.AddTBString(ExpDtlAttr.Field, null, "�ֶ�(ת�����)", true, false, 0, 20, 10);
                //map.AddTBString(ExpDtlAttr.FieldName, null, "�ֶ�����(ת�����)", true, false, 0, 200, 10);

              

                map.AddTBInt(ExpDtlAttr.IDX, 0, "���", true, false);

                //map.AddTBInt(ExpDtlAttr.NodeID, 0, "NodeID", false, false);
                //map.AddDDLSysEnum(ExpDtlAttr.ShowTime, 0, "����ʱ��", true, false, ExpDtlAttr.ShowTime, "@0=��(��ʾ�ڱ��ײ�)@1=������ѡ��ʱ@2=������ʱ@3=������ʱ");
                //map.AddTBString(ExpDtlAttr.FK_Node, null, "���̱��", false, true, 0, 100, 10);
                //map.AddTBString(ExpDtlAttr.DoWhat, null, "ִ��ʲô��", false, true, 0, 100, 10);
                //map.AddTBInt(ExpDtlAttr.H, 0, "���ڸ߶�", false, false);
                //map.AddTBInt(ExpDtlAttr.W, 0, "���ڿ��", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override void afterDelete()
        {
            //BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            //attr.Delete(BP.Sys.MapAttrAttr.FK_MapData, this.FK_Exp,
            //    BP.Sys.MapAttrAttr.KeyOfEn, this.Field);

            base.afterDelete();
        }
 
        #region ˳��
        /// <summary>
        /// �ȵ�������
        /// </summary>
        private void DoOrder()
        {
            ExpDtls attrs = new ExpDtls(this.FK_Exp);
            int i = 0;
            foreach (ExpDtl attr in attrs)
            {
                i++;
                attr.IDX = i;
                attr.Update(ExpDtlAttr.IDX, attr.IDX);
            }
        }
        public string DoUp()
        {
            this.DoOrder();

            this.RetrieveFromDBSources();
            if (this.IDX == 1)
                return null;

            ExpDtls attrs = new ExpDtls(this.FK_Exp);
            ExpDtl attrUp = (ExpDtl)attrs[this.IDX - 1 - 1];
            attrUp.Update(ExpDtlAttr.IDX, this.IDX);
            this.Update(ExpDtlAttr.IDX, this.IDX - 1);
            return null;
        }
        public string DoDown()
        {
            this.DoOrder();
            this.RetrieveFromDBSources();

            ExpDtls attrs = new ExpDtls(this.FK_Exp);
            if (this.IDX == attrs.Count)
                return null;

            ExpDtl attrDown = (ExpDtl)attrs[this.IDX];
            attrDown.Update(ExpDtlAttr.IDX, this.IDX);
            this.Update(ExpDtlAttr.IDX, this.IDX + 1);
            return null;
        }
        #endregion
    }
    /// <summary>
    /// �ⲿ�������ü���
    /// </summary>
    public class ExpDtls : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ExpDtl();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ⲿ�������ü���
        /// </summary>
        public ExpDtls()
        {
        }
        /// <summary>
        /// �ⲿ�������ü���.
        /// </summary>
        /// <param name="FlowNo"></param>
        public ExpDtls(string fk_rpt)
        {
          //  int i = this.Retrieve(ExpDtlAttr.FK_Exp, fk_rpt, ExpDtlAttr.IDX);
        }
        public ExpDtls(int nodeid)
        {
            this.Retrieve(ExpDtlAttr.FK_Node, nodeid, ExpDtlAttr.IDX);
        }
        #endregion
    }
}

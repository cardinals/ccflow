using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ״̬
    /// </summary>
    public enum ReqSta
    {
        /// <summary>
        /// δ����
        /// </summary>
        UnDeal=0,
        /// <summary>
        /// ����ɹ�
        /// </summary>
        OK,
        /// <summary>
        /// ������
        /// </summary>
        Dealing,
        /// <summary>
        /// ��������
        /// </summary>
        Dust
    }
	/// <summary>
    /// ��Ϣ��������
	/// </summary>
    public class InfoReqAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string FK_Sort = "FK_Sort";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��ϵ��
        /// </summary>
        public const string LinkMan = "LinkMan";
        /// <summary>
        /// LinkMan
        /// </summary>
        public const string LinkAddress = "LinkAddress";
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public const string LinkTel="LinkTel";
        /// <summary>
        /// Email
        /// </summary>
        public const string LinkEmail="LinkEmail"; 
        /// <summary>
        /// ״̬
        /// </summary>
        public const string ReqSta = "ReqSta";
        /// <summary>
        /// �Ƿ񹫿�
        /// </summary>
        public const string IsOpen = "IsOpen";
        /// <summary>
        /// �ɼ���Ϣ
        /// </summary>
        public const string SubDoc = "SubDoc";
        /// <summary>
        /// IP
        /// </summary>
        public const string IP = "IP";
        /// <summary>
        /// �ύ��
        /// </summary>
        public const string SubMan = "SubMan";
        /// <summary>
        /// ������
        /// </summary>
        public const string DealMan = "DealMan";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DealDoc = "DealDoc";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DealRDT = "DealRDT";

        public const string IsAnonymous = "IsAnonymous";
    }
	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class InfoReq : EntityNoName
    {
        #region ����
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = true;
                uac.IsUpdate = true;
                uac.IsInsert = false;
                return uac;
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.RDT);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.RDT, value);
            }
        }

        // �Ƿ���Ϊ����
        public string IsOpen
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.IsOpen);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.IsOpen, value);
            }
        }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsAnonymous
        {
            get
            {
                return this.GetValBooleanByKey(InfoReqAttr.IsAnonymous);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.IsAnonymous, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string SubDoc
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.SubDoc);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.SubDoc, value);
            }
        }
        /// <summary>
        /// ��ϵ��
        /// </summary>
        public string LinkMan
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkMan);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkMan, value);
            }
        }
        /// <summary>
        /// ��ϵ��ַ
        /// </summary>
        public string LinkAddress
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkAddress);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkAddress, value);
            }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string LinkTel
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkTel);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkTel, value);
            }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string LinkEmail 
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.LinkEmail);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.LinkEmail, value);
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string DealRDT
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.DealRDT);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.DealRDT, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string DealMan
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.DealMan);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.DealMan, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string FK_Sort
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.FK_Sort);
            }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        public ReqSta ReqSta
        {
            get
            {
                return (ReqSta)this.GetValIntByKey(InfoReqAttr.ReqSta);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.ReqSta, (int)value);
            }
        }
        /// <summary>
        /// �ظ�
        /// </summary>
        public string DealDoc
        {
            get
            {
                return this.GetValStrByKey(InfoReqAttr.DealDoc);
            }
            set
            {
                this.SetValByKey(InfoReqAttr.DealDoc, value);
            }
        }
        #endregion ����

        #region ���췽��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoReq(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoReq()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_InfoReq");

                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.InfoReqSorts' >���</a> - <a href=\"javascript:WinOpen('./Sys/EnsAppCfg.aspx?EnsName=BP.GE.InfoReqs')\">��������</a>";

                map.IsAutoGenerNo = true;
                map.EnType = EnType.Sys;
                map.EnDesc = "��Ϣ����";
                map.DepositaryOfEntity = Depositary.Application;
               
                map.CodeStruct = "5";
                map.MoveTo = InfoReqAttr.ReqSta;

                map.AddTBStringPK(InfoReqAttr.No, null, "���", false, true, 5, 5, 5);
                map.AddDDLEntities(InfoReqAttr.FK_Sort, null, "���", new InfoReqSorts(), false);
                map.AddDDLSysEnum(InfoReqAttr.ReqSta, 0, "״̬", true, true, InfoReqAttr.ReqSta, "@0=δ����@1=�Ѵ���@2=������@3=��������");
                map.AddTBString(InfoReqAttr.Name, null, "����", true, false, 0, 500, 10, true);
                map.AddTBStringDoc(InfoReqAttr.SubDoc, null, "�ύ��Ϣ", true, true, true);
                map.AddTBString(InfoReqAttr.LinkMan, null, "��ϵ��", true, true, 0, 100, 10, true);
                map.AddTBString(InfoReqAttr.LinkAddress, null, "��ϵ��ַ", true, true, 0, 500, 10, true);
                map.AddTBString(InfoReqAttr.LinkTel, null, "��ϵ�绰", true, true, 0, 50, 10, true);
                map.AddTBString(InfoReqAttr.LinkEmail, null, "��������", true, true, 0, 100, 10, true);

                map.AddTBDate(InfoReqAttr.RDT, null, "�ύ����", true, true);


                map.AddTBDate(InfoReqAttr.DealMan, null, "������", true, false);
                map.AddTBStringDoc(InfoReqAttr.DealDoc, null, "��������", true, false, true);
                map.AddTBDate(InfoReqAttr.DealRDT, null, "��������", true, false);
                map.AddBoolean(InfoReqAttr.IsOpen, false, "�Ƿ񹫿�", true, true);
                map.AddBoolean(InfoReqAttr.IsAnonymous, true, "��������", true, true);
                //map.AddMyFileS();
                map.AddSearchAttr(InfoReqAttr.FK_Sort);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeInsert()
        {
            this.RDT = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            return base.beforeInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.DealRDT = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            if (this.DealMan == "")
                this.DealMan = Web.WebUser.Name;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
    /// ��Ϣ����s
	/// </summary>
    public class InfoReqs : EntitiesNoName
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public InfoReqs()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoReq();
            }
        }
    }
}

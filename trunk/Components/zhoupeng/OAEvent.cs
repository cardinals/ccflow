using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
 
	/// <summary>
    /// �������±���
	/// </summary>
    public class OAEventAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string PictSta = "PictSta";
    }
	/// <summary>
    /// �������±���
	/// </summary>
    public class OAEvent : EntityNoName
    {
        #region ����
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(OAEventAttr.RDT);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public string PictStaT
        {
            get
            {
                return this.GetValRefTextByKey(OAEventAttr.PictSta);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string FK_SortT
        {
            get
            {
                return this.GetValRefTextByKey(OAEventAttr.FK_Sort);
            }
        }
        /// <summary>
        /// �ļ���׺
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey("MyFileExt");
            }
        }
        /// <summary>
        /// �ļ�����
        /// </summary>
        public int MyFileNum
        {
            get
            {
                return this.GetValIntByKey("MyFileNum");
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string DocHtml
        {
            get
            {
                return this.GetValHtmlStringByKey("Doc");
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string FK_Sort
        {
            get
            {
                return this.GetValStrByKey(OAEventAttr.FK_Sort);
            }
        }
        #endregion ����

        #region ���췽��
        /// <summary>
        /// �������±���
        /// </summary>
        public OAEvent(string no)
            : base(no)
        {

        }
        /// <summary>
        /// �������±���
        /// </summary>
        public OAEvent()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("GE_OAEvent");
                map.EnType = EnType.Sys;
                map.EnDesc = "�������±���";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";

                map.AddTBStringPK(OAEventAttr.No, null, "���", false, true, 6, 6, 6);
                map.AddDDLEntities(OAEventAttr.FK_Sort, null, "���", new PictSorts(), true);
                map.AddDDLSysEnum(OAEventAttr.PictSta, 1, "״̬", true, true, OAEventAttr.PictSta, "@0=������@1=��ͨ@1=����");

                map.AddTBString(OAEventAttr.Name, null, "����", true, false, 0, 500, 10);
                Attr attr = map.GetAttrByKey("Name");
                attr.UIIsLine = true;

                map.AddTBStringDoc();
                attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;


                map.AddMyFile("���֤��ӡ��","ID");
                map.AddMyFile("��˾�³�","Inc");

                map.AddSearchAttr(OAEventAttr.FK_Sort);
                map.AddSearchAttr(OAEventAttr.PictSta);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// �������±���s
	/// </summary>
    public class OAEvents : EntitiesNoName
    {
        /// <summary>
        /// �������±���s
        /// </summary>
        public OAEvents()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new OAEvent();
            }
        }
    }
}

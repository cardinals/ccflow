using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    public enum PictSta
    {
        /// <summary>
        /// ɾ��
        /// </summary>
        Del,
        /// <summary>
        /// ��ͨ��
        /// </summary>
        Ordinary,
        /// <summary> 
        /// ����� 
        /// </summary>
        Hot
    }
	/// <summary>
    /// ͼƬ
	/// </summary>
    public class PictAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string PictSta = "PictSta";
    }
	/// <summary>
    /// ͼƬ
	/// </summary>
    public class Pict : EntityNoName
    {
        #region ����
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(PictAttr.RDT);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public string PictStaT
        {
            get
            {
                return this.GetValRefTextByKey(PictAttr.PictSta);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string FK_SortT
        {
            get
            {
                return this.GetValRefTextByKey(PictAttr.FK_Sort);
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
                return this.GetValStrByKey(PictAttr.FK_Sort);
            }
        }
        #endregion ����

        #region ���췽��
        /// <summary>
        /// ͼƬ
        /// </summary>
        public Pict(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public Pict()
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
                Map map = new Map("GE_Pict");
                map.EnType = EnType.Sys;
                map.EnDesc = "ͼƬ";
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.PictSorts' >ͼƬ���</a>";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = PictAttr.PictSta;

                map.AddTBStringPK(PictAttr.No, null, "���", false, true, 6, 6, 6);
                map.AddDDLEntities(PictAttr.FK_Sort, null, "���", new PictSorts(), true);
                map.AddDDLSysEnum(PictAttr.PictSta, 1, "״̬", true, true, PictAttr.PictSta, "@0=������@1=��ͨ@1=����");

                map.AddTBString(PictAttr.Name, null, "����", true, false, 0, 500, 10);
                Attr attr = map.GetAttrByKey("Name");
                attr.UIIsLine = true;

                map.AddTBStringDoc();
                attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;

                map.AddTBDate("RDT", "��¼����", true, true);
                map.AddMyFile();

                map.AddSearchAttr(PictAttr.FK_Sort);
                map.AddSearchAttr(PictAttr.PictSta);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ͼƬs
	/// </summary>
    public class Picts : EntitiesNoName
    {
        /// <summary>
        /// ͼƬs
        /// </summary>
        public Picts()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Pict();
            }
        }
    }
}

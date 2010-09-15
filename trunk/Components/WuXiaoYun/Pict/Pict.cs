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
        public const string Tag = "Tag";
        public const string Doc = "Doc";
        public const string MyFileName = "MyFileName";
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string WebPath = "WebPath";
        public const string ReadTimes = "ReadTimes";
    }
	/// <summary>
    /// ͼƬ
	/// </summary>
    public class Pict : EntityNoName
    {
        #region ����
        /// <summary>
        /// ��ע��
        /// </summary>
        public int ReadTimes
        {
            get
            {
                return this.GetValIntByKey(PictAttr.ReadTimes);
            }
            set
            {
                this.SetValByKey(PictAttr.ReadTimes, value);
            }
        }
        /// <summary>
        /// ͼƬ·��
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStrByKey(PictAttr.WebPath);
            }
            set
            {
                this.SetValByKey(PictAttr.WebPath, value);
            }
        }
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey(PictAttr.MyFileName);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileName, value);
            }
        }
        /// <summary>
        /// ͼƬ��չ��
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey(PictAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileExt, value);
            }
        }
        /// <summary>
        /// ͼƬ���
        /// </summary>
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey(PictAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileW, value);
            }
        }

        /// <summary>
        /// ͼƬ�߶�
        /// </summary>
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey(PictAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(PictAttr.MyFileH, value);
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Tag
        {
            get
            {
                return this.GetValStrByKey(PictAttr.Tag);
            }
        }
        /// <summary>
        /// ��ϸ��Ϣ
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValHtmlStringByKey(PictAttr.Doc);
            }
        }

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
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.PictSorts' >���</a>";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = PictAttr.PictSta;

                map.AddTBStringPK(PictAttr.No, null, "���", false, true, 6, 6, 6);
                map.AddTBString(PictAttr.Name, null, "����", true, false, 1, 500, 10, true);
                map.AddDDLEntities(PictAttr.FK_Sort, null, "���", new PictSorts(), true);
                map.AddTBString(PictAttr.Tag, null, BP.Sys.EnsAppCfgs.GetValString("BP.GE.Picts", "Tag"), true, false, 0, 500, 10, true);
                map.AddDDLSysEnum(PictAttr.PictSta, 1, "״̬", true, true, PictAttr.PictSta, "@0=������@1=��ͨ@1=����");
                map.AddTBInt(PictAttr.ReadTimes, 0, "���ʴ���", true, true);
                map.AddTBDate("RDT", "��¼����", true, true);
                map.AddTBStringDoc(PictAttr.Doc, null, "��ϸ��Ϣ", true, false);

                Attr attr = map.GetAttrByKey(PictAttr.Doc);
                attr.UIIsLine = true;

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

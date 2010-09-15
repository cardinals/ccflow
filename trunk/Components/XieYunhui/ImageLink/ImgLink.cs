using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	/// <summary>
    /// ͼƬ��������
	/// </summary>
    public class ImgLinkAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string Url = "Url";
        public const string Target = "Target";
        /// <summary>
        /// �Ƿ��ǽ���
        /// </summary>
        public const string IsFocus = "IsFocus";
    }
	/// <summary>
    /// ͼƬ����
	/// </summary>
    public class ImgLink : EntityNoName
    {
        #region ����
        // ��¼����
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.RDT);
            }
            set
            {
                this.SetValByKey(ImgLinkAttr.RDT, value);
            }
        }

        // �Ƿ���Ϊ����
        public string IsFocus
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.IsFocus);
            }
            set
            {
                this.SetValByKey(ImgLinkAttr.IsFocus, value);
            }
        }

        // ͼƬ������
        public string Url
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.Url);
            }
            set
            {
                this.SetValByKey(ImgLinkAttr.Url, value);
            }
        }

        // ͼƬ��׺
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey("MyFileExt");
            }
        }

        // ͼƬ��
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey("MyFileName");
            }
        }

        // ͼƬ�ĸ߶�
        public string MyFileH
        {
            get
            {
                return this.GetValStrByKey("MyFileH");
            }
        }

        // ͼƬ���
        public string MyFileW
        {
            get
            {
                return this.GetValStrByKey("MyFileW");
            }
        }

        // ͼƬ��С
        public string MyFileSize
        {
            get
            {
                return this.GetValStrByKey("MyFileSize");
            }
        }

        // ���
        public string FK_Sort
        {
            get
            {
                return this.GetValStrByKey(ImgLinkAttr.FK_Sort);
            }
        }

        // �Ƿ��ڱ����ڴ�
        public string Target
        {
            get
            {
                switch (this.GetValIntByKey(ImgLinkAttr.Target))
                {
                    case 0:
                        return "_self";
                    default:
                        return "_blank";
                }
            }
        }
        #endregion ����

        #region ���췽��
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public ImgLink(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public ImgLink()
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
                Map map = new Map("GE_ImgLink");

                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.ImgLinkSorts' >���</a> - <a href=\"javascript:WinOpen('./Sys/EnsAppCfg.aspx?EnsName=BP.GE.ImgLinks')\">��������</a>";

                map.EnType = EnType.Sys;
                map.EnDesc = "ͼƬ����";
                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "2";

                map.AddTBStringPK(ImgLinkAttr.No, null, "���", false, true, 2, 2, 2);
                map.AddDDLEntities(ImgLinkAttr.FK_Sort, null, "���", new ImgLinkSorts(), true);

                map.AddTBString(ImgLinkAttr.Name, null, "����", true, false, 0, 500, 10, true);
                map.AddTBString(ImgLinkAttr.Url, null, "����", true, false, 0, 500, 100, true);

                map.AddBoolean(ImgLinkAttr.IsFocus, false, "�Ƿ��ǽ���", true, true);
                map.AddDDLSysEnum(ImgLinkAttr.Target, 1, "�򿪷�ʽ", true, true, ImgLinkAttr.Target, "@0=�����ڴ�@1=�´��ڴ�");
                map.AddMyFile("ͼƬ");
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ͼƬ����s
	/// </summary>
    public class ImgLinks : EntitiesNoName
    {
        /// <summary>
        /// ͼƬ����s
        /// </summary>
        public ImgLinks()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgLink();
            }
        }
    }
}

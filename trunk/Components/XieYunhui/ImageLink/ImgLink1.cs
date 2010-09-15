using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    public enum UrlType
    {
        Ftp,
        Http
    }
    /// <summary>
    /// ͼƬ��������
    /// </summary>
    public class ImgLink1Attr : EntityNoNameAttr
    {
        public const string RDT = "RDT";
        public const string Url = "Url";
        public const string Target = "Target";
        /// <summary>
        /// �Ƿ��ǽ���
        /// </summary>
        public const string IsFocus = "IsFocus";
        /// <summary>
        /// ��Դ����(URL����FTP)
        /// </summary>
        public const string UrlType = "UrlType";
       // public const string UrlHidden = "UrlHidden";
        public const string ReadTimes = "ReadTimes";
    }
    /// <summary>
    /// ͼƬ����
    /// </summary>
    public class ImgLink1 : EntityNoName
    {
        #region ����

        public int ReadTimes
        {
            get
            {
                return this.GetValIntByKey(ImgLink1Attr.ReadTimes);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.ReadTimes, value);
            }
        }

        public UrlType HisUrlType
        {
            get
            {
                return (UrlType)this.GetValIntByKey(ImgLink1Attr.UrlType);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.UrlType, (int)value);
            }
        }
        // ��¼����
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(ImgLink1Attr.RDT);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.RDT, value);
            }
        }

        // �Ƿ���Ϊ����
        public bool IsFocus
        {
            get
            {
                return this.GetValBooleanByKey(ImgLink1Attr.IsFocus);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.IsFocus, value);
            }
        }

        // ͼƬ������
        public string Url
        {
            get
            {
                return this.GetValStrByKey(ImgLink1Attr.Url);
            }
            set
            {
                this.SetValByKey(ImgLink1Attr.Url, value);
            }
        }
        //public string UrlHidden
        //{
        //    get
        //    {
        //        return this.GetValStrByKey(ImgLink1Attr.UrlHidden);
        //    }
        //    set
        //    {
        //        this.SetValByKey(ImgLink1Attr.UrlHidden, value);
        //    }
        //}

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

        public string WebPath
        {
            get
            {
                return this.GetValStrByKey("WebPath");
            }
        }


        // �Ƿ��ڱ����ڴ�
        public string Target
        {
            get
            {
                switch (this.GetValIntByKey(ImgLink1Attr.Target))
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
        public ImgLink1(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ͼƬ����
        /// </summary>
        public ImgLink1()
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
                Map map = new Map("GE_ImgLink1");

                map.EnType = EnType.Sys;
                map.EnDesc = BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", "AppName");
                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "3";

                map.AddTBStringPK(ImgLink1Attr.No, null, "���", false, true, 3, 3, 3);
                map.AddTBString(ImgLink1Attr.Name, null, "����", true, false, 1, 500, 10, true);
                map.AddTBString(ImgLink1Attr.Url, null, "����", true, false, 1, 500, 100, true);
                //map.AddTBString(ImgLink1Attr.UrlHidden, null, "�������ص�", true, false, 0, 500, 100, true);
                map.AddBoolean(ImgLink1Attr.IsFocus, false, "�Ƿ��ǽ���", true, true);
                map.AddDDLSysEnum(ImgLink1Attr.UrlType, 0, "��Դ����", true, true,
                    ImgLink1Attr.UrlType, "@0=FTP��Դ@1=Http����");

                map.AddDDLSysEnum(ImgLink1Attr.Target, 1, "�򿪷�ʽ", true, true,
                    ImgLink1Attr.Target, "@0=�����ڴ�@1=�´��ڴ�");
                map.AddTBInt(ImgLink1Attr.ReadTimes, 0, "�������", true, true);
                map.AddMyFile("ͼƬ");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        #region ����
        
        //��FTP
        public void OpenFTP()
        {
            GloFTP.OpenFtp();
        }
        protected override bool beforeUpdateInsertAction()
        {
            //this.UrlHidden = this.Url;
            string startStr = "";
            //�����ַ
            switch (this.HisUrlType)
            {
                //������Դ
                case UrlType.Http:
                    startStr = "http:";
                    break;
                //FTP��Դ
                case UrlType.Ftp:
                    startStr = "ftp:";

                    //this.UrlHidden= System.con
                    break;
                default:
                    break;
            }

            if (startStr != "")
            {
                if (!this.Url.ToLower().StartsWith(startStr))
                {
                    if (!this.Url.ToLower().StartsWith("//"))
                    {
                        this.Url = "//" + this.Url;
                    }
                    this.Url = startStr + this.Url;
                }
            }

            return base.beforeUpdateInsertAction();
        }
        #endregion

    }
    /// <summary>
    /// ͼƬ����s
    /// </summary>
    public class ImgLink1s : EntitiesNoName
    {
        /// <summary>
        /// ͼƬ����s
        /// </summary>
        public ImgLink1s()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgLink1();
            }
        }
        /// <summary>
        /// �õ�����Ϊ�������Ϣ
        /// </summary>
        public int RetrieveFocus()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(ImgLink1Attr.IsFocus, true);
            return qo.DoQuery();
        }
    }
}

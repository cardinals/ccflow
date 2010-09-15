using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class InfoBaseAttr : EntityNoNameAttr
    {
        public const string FK_Sort = "FK_Sort";
        public const string RDT = "RDT";
        public const string InfoSta = "InfoSta";
        public const string MyFileName = "MyFileName";
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string NumRead = "NumRead";
        public const string WebPath = "WebPath";
        public const string Author = "Author";
    }
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    abstract public class InfoBase : EntityNoName
    {
        #region ����
        public string Author
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.Author);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.Author, value);
            }
        }
        /// <summary>
        /// ����ͼƬ·��
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.WebPath);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.WebPath, value);
            }
        }
        /// <summary>
        /// ����ͼƬ����
        /// </summary>
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileName);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileName, value);
            }
        }
        /// <summary>
        /// ����ͼƬ��չ��
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileExt, value);
            }
        }
        /// <summary>
        /// ����ͼƬ���
        /// </summary>
        public string MyFileW
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileW, value);
            }
        }

        /// <summary>
        /// ����ͼƬ�߶�
        /// </summary>
        public string MyFileH
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.MyFileH, value);
            }
        }

        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.RDT);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.RDT, value);
            }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public string InfoSta
        {
            get
            {
                return this.GetValStrByKey(InfoBaseAttr.InfoSta);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.InfoSta, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public string InfoStaT
        {
            get
            {
                return this.GetValRefTextByKey(InfoBaseAttr.InfoSta);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string FK_SortT
        {
            get
            {
                return this.GetValRefTextByKey(InfoBaseAttr.FK_Sort);
            }
        }
        public int NumRead
        {
            get
            {
                return this.GetValIntByKey(InfoBaseAttr.NumRead);
            }
            set
            {
                this.SetValByKey(InfoBaseAttr.NumRead, value);
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
                return this.GetValStrByKey(InfoBaseAttr.FK_Sort);
            }
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public abstract string PTable
        {
            get;
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public abstract string SortEntity
        {
            get;
        }
        /// <summary>
        /// ���DDLEntitees
        /// </summary>
        public abstract EntitiesNoName SortDDLEntities
        {
            get;
        }

        #endregion ����

        #region ���췽��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoBase(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoBase()
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
                Map map = new Map(PTable);
                map.EnType = EnType.Sys;
                map.EnDesc = this.GetCfgValStr("AppName");
                //map.EnDesc = BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", "AppName");
                map.TitleExt = " - <a href='Ens.aspx?EnsName=" + SortEntity + "s' >���</a>";

                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = InfoBaseAttr.InfoSta;

                map.AddTBStringPK(InfoBaseAttr.No, null, "���", false, true, 6, 6, 6);
                map.AddTBString(InfoBaseAttr.Name, null, "����", true, false, 1, 500, 10, true);
                map.AddDDLEntities(InfoBaseAttr.FK_Sort, null, "���", SortDDLEntities, true);
                map.AddDDLSysEnum(InfoBaseAttr.InfoSta, 1, "״̬", true, true, InfoBaseAttr.InfoSta, "@0=���ɼ�@1=��ͨ@2=����@3=ͼƬ��Ϣ");
                map.AddTBDate(InfoBaseAttr.RDT, "��¼����", true, true);
                map.AddTBString(InfoBaseAttr.Author, null, "����", true, false, 0, 20, 20);
                map.AddTBInt(InfoBaseAttr.NumRead, 0, "�Ķ�����", true, true);

                map.AddTBStringDoc();
                Attr attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;

                map.AddSearchAttr(InfoBaseAttr.FK_Sort);
                map.AddSearchAttr(InfoBaseAttr.InfoSta);

                map.AddMyFile();
                map.AddMyFileS();

                this._enMap = map;
                return this._enMap;
            }
        }

        #endregion
    }
    /// <summary>
    /// ��Ϣ����s
    /// </summary>
    abstract public class InfoBases : EntitiesNoName
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public InfoBases()
        {

        }
        /// <summary>
        /// ����(������):topС�ڵ���0ʱ��ѯ����
        /// </summary>
        /// <returns></returns>
        public int RetrieveRecom(int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereNotIn(InfoBaseAttr.InfoSta, "0");
            qo.addOrderByDesc(InfoBaseAttr.No);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();

        }

        /// <summary>
        /// ĳһ�������(������):topС�ڵ���0ʱ��ѯ����
        /// </summary>
        /// <returns></returns>
        public int RetrieveByType(string fk_type, int top)
        {
            // return this.RetrieveFromCash(InfoAttr.FK_Sort, fk_type, InfoAttr.InfoSta, 0, "No", false);
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(InfoBaseAttr.FK_Sort, fk_type);
            qo.addAnd();
            qo.AddWhereNotIn(InfoBaseAttr.InfoSta, "0");
            qo.addOrderByDesc(InfoBaseAttr.No);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }
    }
}

using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class InfoListBaseAttr : EntityNoNameAttr
    {
        public const string RDT = "RDT";
        public const string InfoListSta = "InfoListSta";
        public const string MyFileName = "MyFileName";
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string NumRead = "NumRead";
        public const string WebPath = "WebPath";
    }
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    abstract public class InfoListBase : EntityNoName
    {
        #region ����
        /// <summary>
        /// ����ͼƬ·��
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.WebPath);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.WebPath, value);
            }
        }
        /// <summary>
        /// ����ͼƬ����
        /// </summary>
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileName);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileName, value);
            }
        }
        /// <summary>
        /// ����ͼƬ��չ��
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileExt, value);
            }
        }
        /// <summary>
        /// ����ͼƬ���
        /// </summary>
        public string MyFileW
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileW, value);
            }
        }

        /// <summary>
        /// ����ͼƬ�߶�
        /// </summary>
        public string MyFileH
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.MyFileH, value);
            }
        }

        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.RDT);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public string InfoListSta
        {
            get
            {
                return this.GetValStrByKey(InfoListBaseAttr.InfoListSta);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.InfoListSta, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public string InfoListStaT
        {
            get
            {
                return this.GetValRefTextByKey(InfoListBaseAttr.InfoListSta);
            }
        }
        public int NumRead
        {
            get
            {
                return this.GetValIntByKey(InfoListBaseAttr.NumRead);
            }
            set
            {
                this.SetValByKey(InfoListBaseAttr.NumRead, value);
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
        #endregion ����

        #region ���췽��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoListBase(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoListBase()
        {
        }
        /// <summary>
        /// ���ݿ��
        /// </summary>
        public abstract string PTable
        {
            get;
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

                Map map = new Map(this.PTable);
                map.EnType = EnType.Sys;
                map.EnDesc = this.GetCfgValStr("AppName");
                map.DepositaryOfEntity = Depositary.Application;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.MoveTo = InfoListBaseAttr.InfoListSta;

                map.AddTBString(InfoListBaseAttr.Name, null, "����", true, false, 1, 500, 10, true);
                map.AddTBStringPK(InfoListBaseAttr.No, null, "���", false, true, 6, 6, 6);
                map.AddDDLSysEnum(InfoListBaseAttr.InfoListSta, 1, "״̬", true, true, InfoListBaseAttr.InfoListSta, "@0=���ɼ�@1=��ͨ@2=����@3=ͼƬ��Ϣ");

                map.AddTBStringDoc();
                Attr attr = map.GetAttrByKey("Doc");
                attr.UIIsLine = true;

                map.AddTBDate("RDT", "��¼����", true, true);
                map.AddTBInt(InfoListBaseAttr.NumRead, 0, "�Ķ�����", true, true);

                map.AddSearchAttr(InfoListBaseAttr.InfoListSta);

                //map.AddMyFile("����ͼƬ", "InfoListBasePic");

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
    abstract public class InfoListBases : EntitiesNoName
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public InfoListBases()
        {
        }
        /// <summary>
        /// ����(������):topС�ڵ���0ʱ��ѯ����
        /// </summary>
        /// <returns></returns>
        public int RetrieveRecom(int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereNotIn("InfoListSta", "0");
            qo.addOrderByDesc("RDT");
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }
    }
}

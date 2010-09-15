using System;
using System.Data;
using BP.DA;
using BP.En;
using System.IO;

namespace BP.GE
{
	/// <summary>
	/// �ļ���
	/// </summary>
    public class FDBAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string MyPK = "MyPK";
        public const string No = "No";
        /// <summary>
        /// �ļ������
        /// </summary>
        public const string NameShort = "NameShort";
        /// <summary>
        /// ����·��
        /// </summary>
        public const string NameFull = "NameFull";
        /// <summary>
        /// �ļ���չ��
        /// </summary>
        public const string Ext = "Ext";
        /// <summary>
        /// ��С
        /// </summary>
        public const string FSize = "FSize";
        /// <summary>
        /// ���
        /// </summary>
        public const string FK_FDBDir = "FK_FDBDir";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// �Ķ���
        /// </summary>
        public const string ReadTimes = "ReadTimes";
        public const string Doc = "Doc";
        public const string PayCent = "PayCent";
        public const string RDT = "RDT";
        #endregion
    }
	/// <summary>
	/// �ļ���
	/// </summary>
    public class FDB : EntityMyPK
    {
        #region ��������
        public string No
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.No);
            }
            set
            {
                this.SetValByKey(FDBAttr.No, value);
            }
        }
        public int FSize
        {
            get
            {
                return this.GetValIntByKey(FDBAttr.FSize);
            }
            set
            {
                this.SetValByKey(FDBAttr.FSize, value);
            }
        }
        public int PayCent
        {
            get
            {
                return this.GetValIntByKey(FDBAttr.PayCent);
            }
            set
            {

                this.SetValByKey(FDBAttr.PayCent, value);
            }
        }
        public string NameShort
        {
            get
            {
                return "<img src='/Edu/Images/FileType/" + this.Ext + ".gif' border=0 />" + this.GetValStringByKey(FDBAttr.NameShort);
            }
            set
            {
                this.SetValByKey(FDBAttr.NameShort, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.RDT);
            }
            set
            {
                this.SetValByKey(FDBAttr.RDT, value);
            }
        }
        public string NameS
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.NameShort);
            }
            set
            {
                this.SetValByKey(FDBAttr.NameShort, value);
            }
        }
        public string NameFull
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.NameFull);
            }
            set
            {
                this.SetValByKey(FDBAttr.NameFull, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_FDBDir
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.FK_FDBDir);
            }
            set
            {
                this.SetValByKey(FDBAttr.FK_FDBDir, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_FDBDirText
        {
            get
            {
                return this.GetValRefTextByKey(FDBAttr.FK_FDBDir);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.CDT);
            }
            set
            {
                this.SetValByKey(FDBAttr.CDT, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.Doc);
            }
            set
            {
                this.SetValByKey(FDBAttr.Doc, value);
            }
        }
        public string Ext
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.Ext);
            }
            set
            {
                this.SetValByKey(FDBAttr.Ext, value);
            }
        }
        #endregion

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsInsert = false;
                uac.IsUpdate = true;
                uac.IsDelete = true;
                //uac.OpenForAppAdmin();
                return uac;
            }
        }
        /// <summary>
        /// �ļ���
        /// </summary>		
        public FDB()
        {
        }
        public FDB(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// FDBMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_FDB");

                #region ��������
                map.EnType = EnType.Sys;
                map.EnDesc = BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName");
                map.DepositaryOfEntity = Depositary.None;
                //map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.FDBDirs' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName") + "Ŀ¼</a> - <a href=\"javascript:WinOpen('./Sys/EnsAppCfg.aspx?EnsName=BP.GE.FDBs')\">��������</a>";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.FDBDirs' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName")
                    + "Ŀ¼</a> - <a href=\"javascript:WinOpen('./../GE/FDB/OpenFtp.aspx?DoType=OpenFtp','FTPĿ¼',1000,700)\" >��FTP</a>"
                +" - <a href=\"javascript:WinOpen('Method.aspx?M=BP.GE.FDBDTS');\" >��������</a>";

                #endregion

                #region �ֶ�
                map.AddTBStringPK(FDBAttr.MyPK, null, "MyPK", false, true, 0, 200, 200);
                map.AddTBString(FDBAttr.No, null, "No", false, true, 0, 200, 200);
                map.AddDDLEntities(FDBAttr.FK_FDBDir, "10", "Ŀ¼", new FDBDirs(), false);
                map.AddTBString(FDBAttr.NameShort, null, "������", true, true, 1, 50, 200);
                map.AddTBString(FDBAttr.NameFull, null, "������", true, true, 1, 300, 200);
                map.AddTBString(FDBAttr.Ext, null, "����", true, false, 0, 50, 200);
                map.AddTBInt(FDBAttr.FSize, 0, "��С", true, true);
                map.AddTBDate(FDBAttr.CDT, null, "����ʱ��", true, true);
                map.AddTBInt(FDBAttr.PayCent, 0, "֧����", true, false);
                map.AddTBStringDoc("Doc", null, "˵��", true, false);
                map.AddTBIntMyNum();
                #endregion

                map.AddSearchAttr(FDBAttr.FK_FDBDir);
                
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ����
        #endregion
    }
	/// <summary>
	/// �ļ���
	/// </summary>
    public class FDBs : EntitiesMyPK
    {
        #region ʵ��
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FDB();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ļ���
        /// </summary>
        public FDBs()
        {

        }
        public FDBs(string fk_sort)
        {
            this.Retrieve(FDBAttr.FK_FDBDir, fk_sort);
        }
        #endregion
    }
}

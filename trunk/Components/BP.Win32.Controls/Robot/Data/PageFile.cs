using System;
using System.Data;
using System.IO;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// ��ҳ�ļ�
	/// </summary>
    public class PageFileAttr : EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// RDT
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string FileSize = "FileSize";
        public const string FK_WebSite = "FK_WebSite";
        public const string URL = "URL";
        /// <summary>
        /// ����
        /// </summary>
        public const string FDesc = "FDesc";
        /// <summary>
        /// PageUrl
        /// </summary>
        public const string PageUrl = "PageUrl";
        #endregion
    }
	/// <summary>
	/// ��ҳ�ļ�
	/// </summary>
    public class PageFile : EntityNoName
	{	
		#region ��������
        /// <summary>
        /// ���ʹ���
        /// </summary>
        public Int64 FileSize
        {
            get
            {
                return this.GetValInt64ByKey(PageFileAttr.FileSize);
            }
            set
            {
                this.SetValByKey(PageFileAttr.FileSize, value);
            }
        }
        /// <summary>
        /// PageUrl
        /// </summary>
        public string PageUrl
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.PageUrl);
            }
            set
            {
                this.SetValByKey(PageFileAttr.PageUrl, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.RDT);
            }
            set
            {
                this.SetValByKey(PageFileAttr.RDT, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_WebSite
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.FK_WebSite);
            }
            set
            {
                this.SetValByKey(PageFileAttr.FK_WebSite, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FDesc
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.FDesc);
            }
            set
            {
                this.SetValByKey(PageFileAttr.FDesc, value);
            }
        }
		#endregion

		#region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
		/// <summary>
		/// ��ҳ�ļ�
		/// </summary>		
		public PageFile()
		{
		}
		/// <summary>
		/// ��ҳ�ļ�
		/// </summary>
        /// <param name="no"></param>
        public PageFile(string no)
            : base(no)
        {
        }
		/// <summary>
		/// PageFileMap
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "RB_PageFile";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "�ļ�(��ҳ��ȡ)";
                map.EnType = EnType.App;
                #endregion

                #region ����

                map.AddTBStringPK(PageFileAttr.No, null, "���URL", true, true, 1, 900, 4);
                map.AddTBString(PageFileAttr.Name, null, "����", true, true, 0, 100, 30);
                map.AddTBString(PageFileAttr.PageUrl, null, "������ҳ", true, true, 0, 500, 30);
                map.AddTBDate(PageFileAttr.RDT, "��¼����", true, true);

                map.AddTBString(PageFileAttr.FK_WebSite, null, "����", true, true, 0, 300, 30);

                map.AddTBInt(PageFileAttr.FileSize, 0, "�ļ���С", true, true);
                map.AddTBStringDoc(PageFileAttr.FDesc, null, "����", true, true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion
	}
	/// <summary>
	/// ��ҳ�ļ�
	/// </summary>
    public class PageFiles : EntitiesNoName
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PageFile();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��ҳ�ļ�
		/// </summary>
		public PageFiles()
        {
        }
		#endregion

	 
	}
	
}

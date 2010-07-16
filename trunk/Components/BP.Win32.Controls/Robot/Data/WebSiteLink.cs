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
	/// ��վ������
	/// </summary>
    public class WebSiteLinkAttr : EntityNoNameAttr
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
        public const string HostName = "HostName";
        public const string URL = "URL";
        /// <summary>
        /// ����
        /// </summary>
        public const string FDesc = "FDesc";
        /// <summary>
        /// 
        /// </summary>
        public const string PageUrl = "PageUrl";
        #endregion
    }
	/// <summary>
	/// ��վ������
	/// </summary>
    public class WebSiteLink : EntityNoName
	{	
		#region ��������
        /// <summary>
        /// ��������
        /// </summary>
        public string HostName
        {
            get
            {
                return this.GetValStringByKey(WebSiteLinkAttr.HostName);
            }
            set
            {
                this.SetValByKey(WebSiteLinkAttr.HostName, value);
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
		/// ��վ������
		/// </summary>		
		public WebSiteLink()
		{
		}
		/// <summary>
		/// ��վ������
		/// </summary>
        /// <param name="no"></param>
        public WebSiteLink(string no)
            : base(no)
        {
        }
		/// <summary>
		/// WebSiteLinkMap
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
                map.PhysicsTable = "RB_WebSiteLink";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "�ⲿ����";
                map.EnType = EnType.App;
                #endregion

                #region ����
                map.AddTBStringPK(WebSiteLinkAttr.No, null, "���", true, true, 1, 200, 4);
                map.AddTBString(WebSiteLinkAttr.Name, null, "����", true, true, 0, 900, 4);

                map.AddTBString(WebSiteLinkAttr.HostName, null, "HostName", true, true, 0, 900, 4);
                #endregion


                 

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion

       
	}
	/// <summary>
	/// ��վ������
	/// </summary>
    public class WebSiteLinks : EntitiesNoName
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WebSiteLink();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��վ������
		/// </summary>
		public WebSiteLinks()
        {
        }
		#endregion

	 
	}
	
}

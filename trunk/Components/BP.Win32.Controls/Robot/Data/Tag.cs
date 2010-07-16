using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.RB
{
	/// <summary>
	/// ��ǩ
	/// </summary>
    public class TagAttr : EntityOIDAttr
    {
        #region ��������
        /// <summary>
        /// ��ǩ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ��ǩ����
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FK_WebSiteType = "FK_WebSiteType";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �Ķ���
        /// </summary>
        public const string ReadTimes = "ReadTimes";
        /// <summary>
        /// IsPass �Ƿ񷢲�
        /// </summary>
        public const string IsPass = "IsPass";
        /// <summary>
        /// ����Ա����
        /// </summary>
        public const string AdminName = "AdminName";
        /// <summary>
        /// ��ϵ��ʽ
        /// </summary>
        public const string LinkWay = "LinkWay";
        #endregion
    }
	/// <summary>
	/// ��ǩ
	/// </summary>
    public class Tag : EntityOID
    {
        #region ��������
        public string FK_WebSiteType
        {
            get
            {
                return this.GetValStringByKey(TagAttr.FK_WebSiteType);
            }
            set
            {
                this.SetValByKey(TagAttr.FK_WebSiteType, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(TagAttr.Name);
            }
            set
            {
                this.SetValByKey(TagAttr.Name, value);
            }
        }
        public string NameHtml
        {
            get
            {
                return "<img src='../img/dot.gif' ><a href='" + this.URL + "' target=_blank >" + this.Name + "</a>";
            }
            set
            {
                this.SetValByKey(TagAttr.Name, value);
            }
        }
        public string URLHtml
        {
            get
            {
                return "<a href='" + this.URL + "' target=_blank >" + this.URL + "</a>";
            }
        }
        public string URL
        {
            get
            {
                string url = this.GetValStringByKey(TagAttr.URL);
                if (url.ToLower().IndexOf("http://") == -1)
                    url = "http://" + url;
                return url;
            }
            set
            {
                this.SetValByKey(TagAttr.URL, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TagAttr.RDT);
            }
            set
            {
                this.SetValByKey(TagAttr.RDT, value);
            }
        }
        public string NameOfShort
        {
            get
            {
                if (this.Name.Length > 10)
                    return this.Name.Substring(0, 9) + "...";
                else
                    return this.Name;
            }
        }
        public int ReadTimes
        {
            get
            {
                return this.GetValIntByKey(TagAttr.ReadTimes);
            }
            set
            {
                this.SetValByKey(TagAttr.ReadTimes, value);
            }
        }
        public bool IsPass
        {
            get
            {
                return this.GetValBooleanByKey(TagAttr.IsPass);
            }
            set
            {
                this.SetValByKey(TagAttr.IsPass, value);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }

        public Tag()
        {

        }
        /// <summary>
        /// EnMap
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
                map.PhysicsTable = "SE_Tag";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "��ǩ";
                map.EnType = EnType.App;

                map.AddTBIntPKOID();

                map.AddTBString(TagAttr.Name, null, "��ǩ", true, false, 0, 100, 1);

                map.AttrsOfOneVSM.Add(new TagTags(), new Tags(), TagTagAttr.T1, TagTagAttr.T2, TagAttr.Name, TagAttr.OID, "��ǩ��Ӧ");
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
    }
	/// <summary>
	/// ��ǩ
	/// </summary>
	public class Tags : Entities
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Tag();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��ǩ
		/// </summary>
		public Tags(){}
		#endregion
	}
	
}

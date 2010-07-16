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
	/// ��ҳ����
	/// </summary>
    public class PageMailAttr : EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// RDT
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string SendTimes = "SendTimes";
        /// <summary>
        /// SDT
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// �Ƿ�Ч
        /// </summary>
        public const string IsEnable = "IsEnable";
        #endregion
    }
	/// <summary>
	/// ��ҳ����
	/// </summary>
    public class PageMail : EntityNoName
	{	
		#region ��������
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(PageMailAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(PageMailAttr.IsEnable, value);
            }
        }
        /// <summary>
        /// ���ʹ���
        /// </summary>
        public int SendTimes
        {
            get
            {
                return this.GetValIntByKey(PageMailAttr.SendTimes);
            }
            set
            {
                this.SetValByKey(PageMailAttr.SendTimes, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(PageMailAttr.RDT);
            }
            set
            {
                this.SetValByKey(PageMailAttr.RDT, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(PageMailAttr.SDT);
            }
            set
            {
                this.SetValByKey(PageMailAttr.SDT, value);
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
		/// ��ҳ����
		/// </summary>		
		public PageMail()
		{
		}
		/// <summary>
		/// ��ҳ����
		/// </summary>
        /// <param name="no"></param>
        public PageMail(string no)
            : base(no)
        {
        }
		/// <summary>
		/// PageMailMap
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

                map.PhysicsTable = "RB_PageMail";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "EPageMail";
                map.EnType = EnType.App;

                #endregion

                #region ������

                map.AddTBStringPK(PageMailAttr.No, null, "EPageMail", true, true, 1, 100, 4);
                map.AddTBString(PageMailAttr.Name, null, "�û�", true, true, 0, 100, 30);

                map.AddTBDate(PageMailAttr.RDT, "��¼����", true, true);

                map.AddTBDate(PageMailAttr.SDT, "�����������", true, true);
                map.AddTBInt(PageMailAttr.SendTimes, 0, "���ʹ���", true, true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion

		#region ���ط���
		#endregion 

	}
	/// <summary>
	/// ��ҳ����
	/// </summary>
    public class PageMails : EntitiesNoName
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PageMail();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��ҳ����
		/// </summary>
		public PageMails(){}
		 
		
		#endregion

	 
	}
	
}

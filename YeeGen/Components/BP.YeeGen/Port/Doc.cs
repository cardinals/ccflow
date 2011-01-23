using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.YG
{
	public class DocAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// ������
		/// </summary>
		public const string Cent="Cent";
		/// <summary>
		/// ����Ա
		/// </summary>
		public const string FK_Member="FK_Member";
        /// <summary>
        /// ����
        /// </summary>
		public const string Doc="Doc";
        /// <summary>
        /// �ؼ���
        /// </summary>
		public const string KeyWords="KeyWords";
        /// <summary>
        /// ��¼����
        /// </summary>
		public const string RDT="RDT";
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        public const string EDT = "EDT";

        /// <summary>
        /// �Ķ�����
        /// </summary>
        public const string ReadTimes = "ReadTimes";
	}
	/// <summary>
	/// ��Ա����
	/// </summary>
	public class Doc :EntityOIDName
	{
		#region cent .
        public int ReadTimes
        {
            get
            {
                return this.GetValIntByKey(DocAttr.ReadTimes);
            }
            set
            {
                this.SetValByKey(DocAttr.ReadTimes, value);
            }
        }
		public string DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(DocAttr.Doc);
			}
			set
			{
				this.SetValByKey(DocAttr.Doc,value);
			}
		}
		public string FK_Member
		{
			get
			{
				return this.GetValStringByKey(DocAttr.FK_Member);
			}
			set
			{
				this.SetValByKey(DocAttr.FK_Member,value);
			}
		}
		public string KeyWords
		{
			get
			{
				return this.GetValStringByKey(DocAttr.KeyWords);
			}
			set
			{
				this.SetValByKey(DocAttr.KeyWords,value);
			}
		}
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(DocAttr.RDT);
			}
			set
			{
				this.SetValByKey(DocAttr.RDT,value);
			}
		}
        public string EDT
        {
            get
            {
                return this.GetValStringByKey(DocAttr.EDT);
            }
            set
            {
                this.SetValByKey(DocAttr.EDT, value);
            }
        }
		#endregion

		#region ʵ�ֻ����ķ���
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// FLinkMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map();

				#region �������� 
				map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; 
				map.PhysicsTable="YG_Doc";
				map.AdjunctType = AdjunctType.AllType ;  
				map.DepositaryOfMap=Depositary.Application; 
				map.DepositaryOfEntity=Depositary.None;
				map.EnDesc="��Ա����";
				map.EnType=EnType.App;

                map.AddTBIntPKOID();

                map.AddTBString(DocAttr.Name, null, "����", true, false, 1, 100, 100);
                map.AddTBStringDoc();// ("��������");
                map.AddTBString(DocAttr.KeyWords, null, "KeyWords", true, false, 0, 500, 10);

                map.AddTBStringPK(DocAttr.FK_Member, null, "FK_Member", true, false, 1, 100, 100);

                map.AddTBInt(DocAttr.ReadTimes, 0, "�Ķ�����", true, false);

				map.AddTBDateTime(DocAttr.RDT,null,"��������",true,false);
                map.AddTBDateTime(DocAttr.EDT, null, "�޸�����", true, false);

				#endregion

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��Ա����
		/// </summary>
		public Doc(){}
		#endregion 
	}
	/// <summary>
	/// ��Ա����
	/// </summary>
	public class Docs :EntitiesOID
	{
		#region ����
		/// <summary>
		/// ��Ա����s
		/// </summary>
		public Docs(){}
		/// <summary>
		/// ��Ա����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Doc();
			}
		}
		#endregion
	}
}

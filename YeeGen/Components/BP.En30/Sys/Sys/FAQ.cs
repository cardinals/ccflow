using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// �û�����
	/// </summary>
    public class FAQAttr  //EntityEnsNameAttr
    {
        /// <summary>
        /// OID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// ������
        /// </summary>
        public const string Asker = "Asker";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ����
        /// </summary>
        public const string Docs = "Docs";
        /// <summary>
        /// dtl num
        /// </summary>
        public const string DtlNum = "DtlNum";
        /// <summary>
        /// ReadNum
        /// </summary>
        public const string ReadNum = "ReadNum";
    }
	/// <summary>
	/// �û�����
	/// </summary> 
	public class FAQ:EntityOID 
	{
		#region ��������
		/// <summary>
		/// AskerText
		/// </summary>
		public  string  AskerText
		{
			get
			{
				return this.GetValRefTextByKey(FAQAttr.Asker);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public  string  Asker
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Asker);
			}
			set
			{
				this.SetValByKey(FAQAttr.Asker,value);
			}
		}
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public  string  RDT
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.RDT);
			}
			set
			{
				this.SetValByKey(FAQAttr.RDT,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Title
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Title);
			}
			set
			{
				this.SetValByKey(FAQAttr.Title,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Docs
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Docs);
			}
			set
			{
				this.SetValByKey(FAQAttr.Docs,value);
			}
		}
        public  string  Doc
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Docs);
			}
			set
			{
				this.SetValByKey(FAQAttr.Docs,value);
			}
		}
		public  string  DocsHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(FAQAttr.Docs);
			}
		}
		/// <summary>
		/// dtl Num
		/// </summary>
		public  int  DtlNum
		{
			get
			{
				return this.GetValIntByKey(FAQAttr.DtlNum);
			}
			set
			{
				this.SetValByKey(FAQAttr.DtlNum,value);
			}
		}
		/// <summary>
		/// �Ķ���
		/// </summary>
		public  int  ReadNum
		{
			get
			{
				return this.GetValIntByKey(FAQAttr.ReadNum);
			}
			set
			{
				this.SetValByKey(FAQAttr.ReadNum,value);
			}
		}
		#endregion 

		#region ���췽��
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
		/// �û�����
		/// </summary>
		public FAQ(){}
		/// <summary>
		/// �û�����
		/// </summary>
		/// <param name="oid"></param>
		public FAQ(int oid ) : base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("Sys_FAQ");
				map.EnDesc="�û�����";
				map.EnType=EnType.Admin;
				map.EnDBUrl= new DBUrl(DBUrlType.DBAccessOfOracle9i) ; 

				map.AddTBIntPKOID();
				map.AddDDLEntities(FAQAttr.Asker,Web.WebUser.No ,"������",new Emps(),false);
				map.AddTBString(FAQAttr.Title,null,"����",true,false,0,500,20);
				map.AddTBStringDoc(FAQAttr.Docs,null,"��������",true,false);
				map.AddTBDateTime(FAQAttr.RDT,"����ʱ��",true,false);
				map.AddTBInt(FAQAttr.DtlNum,0,"�ش����",true,false);
				map.AddTBInt(FAQAttr.ReadNum,0,"�Ķ���",true,false);

				map.AddDtl( new FAQDtls(), FAQDtlAttr.FK_FAQ);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// �û�����
	/// </summary> 
	public class FAQs : EntitiesOID
	{
		#region ���캯��
		/// <summary>
		/// ����ʵ����ʵĹ���
		/// </summary>
		public FAQs()
		{
		}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FAQ();
			}
		}
		#endregion
	
	}
}

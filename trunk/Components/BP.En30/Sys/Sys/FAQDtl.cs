using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// �û���������
	/// </summary>
	public class FAQDtlAttr  //EntityEnsNameAttr
	{
		/// <summary>
		/// OID
		/// </summary>
		public const string OID="OID";
		/// <summary>
		/// FK_FAQ
		/// </summary>
		public const string FK_FAQ="FK_FAQ";
		/// <summary>
		/// ������
		/// </summary>
		public const string Answer="Answer";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// ����
		/// </summary>
		public const string Docs="Docs";
	}
	/// <summary>
	/// �û���������
	/// </summary> 
	public class FAQDtl:EntityOID 
	{
		#region ��������
		public  int  FK_FAQ
		{
			get
			{
				return this.GetValIntByKey(FAQDtlAttr.FK_FAQ);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.FK_FAQ,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Answer
		{
			get
			{
				return this.GetValStringByKey(FAQDtlAttr.Answer);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.Answer,value);
			}
		}
		public  string  AnswerText
		{
			get
			{
				return this.GetValRefTextByKey(FAQDtlAttr.Answer);
			}
		}
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public  string  RDT
		{
			get
			{
				return this.GetValStringByKey(FAQDtlAttr.RDT);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.RDT,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Docs
		{
			get
			{
				return this.GetValStringByKey(FAQDtlAttr.Docs);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.Docs,value);
			}
		}
		public  string  DocsHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(FAQDtlAttr.Docs);
			}
		}
		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// �û���������
		/// </summary>
		public FAQDtl(){}
		/// <summary>
		/// �û���������
		/// </summary>
		/// <param name="oid"></param>
		public FAQDtl(int oid ) : base(oid)
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
				Map map = new Map("Sys_FAQDtl");
				map.EnDesc="�û���������";
				map.EnType=EnType.Admin;
				map.EnDBUrl= new DBUrl(DBUrlType.DBAccessOfOracle9i) ; 
				map.AddTBIntPKOID();
				map.AddDDLEntities(FAQDtlAttr.FK_FAQ,0,DataType.AppInt,"FAQ",new FAQs(),"OID","Title", false);
				map.AddDDLEntities(FAQDtlAttr.Answer,null,"����",new Emps(),false);
				map.AddTBStringDoc(FAQDtlAttr.Docs,null,"������",true,false);
				map.AddTBDateTime(FAQDtlAttr.RDT,"��ʱ��",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// �û���������
	/// </summary> 
	public class FAQDtls : EntitiesOID
	{
		#region ���캯��
		/// <summary>
		/// ����ʵ����ʵĹ���
		/// </summary>
		public FAQDtls(){}
		public FAQDtls(int fk_faq)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( FAQDtlAttr.FK_FAQ, fk_faq);
			qo.addOrderByDesc( FAQDtlAttr.OID) ;
			qo.DoQuery();
		}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FAQDtl();
			}
		}
		#endregion
	}
}

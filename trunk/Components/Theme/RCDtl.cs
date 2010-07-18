using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	public class RCDtlAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// �Ķ������
		/// </summary>
		public const string Answer="Answer";
		/// <summary>
		/// FK_RC
		/// </summary>
		public const string FK_RC="FK_RC";
	}
	/// <summary>
	/// ��˰���ջ���
	/// </summary>
	public class RCDtl :EntityOIDName
	{
		public string NameHtml
		{
			get
			{
                return "<b><font color=blue>" + this.GetValHtmlStringByKey("Name") + "</font></b>";
			}
		}

		#region attrs
		/// <summary>
		/// FK_RC
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(RCDtlAttr.FK_RC);
			}
			set
			{
				this.SetValByKey(RCDtlAttr.FK_RC,value);
			}
		}
		public string AnswerHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(EssayQuestionAttr.Answer);
			}
		}
		public string NameExt
		{
			get
			{
				return ChoseBase.GenerStr(this.Name)+"\n\n\n";
				
			}
		}
		/// <summary>
		/// Answer
		/// </summary>
		public string Answer
		{
			get
			{
				return this.GetValStringByKey(RCDtlAttr.Answer);
			}
			set
			{
				this.SetValByKey(RCDtlAttr.Answer,value);
			}
		}
		public string AnswerExt
		{
			get
			{
				return ChoseBase.GenerStr( this.Answer);
			}
		}
		#endregion

	 
		#region ʵ�ֻ����ķ���
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_RCDtl");
				map.EnDesc="С��";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBIntPKOID();
				map.AddTBStringDoc(RCDtlAttr.Name,null,"����",true,false);
				map.AddTBStringDoc(RCDtlAttr.Answer,null,"��",true,false);
				map.AddDDLEntities(RCDtlAttr.FK_RC,"0001","�Ķ������",new RCs(),false);
				//map.AddSearchAttr( ChoseAttr.FK_Chose);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ���ջ���
		/// </summary> 
		public RCDtl()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="oid"></param>
		public RCDtl(int oid):base(oid){}
		#endregion 

		protected override void afterDelete()
		{
			this.DeleteHisRefEns();
			base.afterDelete ();
		}
	 
	}
	/// <summary>
	/// RCDtls
	/// </summary>
	public class RCDtls :Entities
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="FK_RC"></param>
		public RCDtls(string FK_RC)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(RCDtlAttr.FK_RC,FK_RC);
			qo.DoQuery();
		}
		/// <summary>
		/// Choses
		/// </summary>
		public RCDtls(){}
		/// <summary>
		/// Chose
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RCDtl();
			}
		}
	}
}

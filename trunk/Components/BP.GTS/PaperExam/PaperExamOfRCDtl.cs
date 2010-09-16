using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ķ��⿼��attr
	/// </summary>
	public class PaperExamOfRCDtlAttr:EntityOIDAttr
	{
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// ѧ��
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// �Ķ���
		/// </summary>
		public const string FK_RCDtl="FK_RCDtl";
		/// <summary>
		/// ֵ
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// �÷�
		/// </summary>
		public const string Cent="Cent";
	}
	/// <summary>
	/// �Ķ��⿼��
	/// </summary>
	public class PaperExamOfRCDtl :Entity
	{
		#region attrs
		/// <summary>
		/// �÷�
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfRCDtlAttr.Cent);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.Cent,value);
			}
		}
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfRCDtlAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfRCDtlAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_RCDtl
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfRCDtlAttr.FK_RCDtl);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.FK_RCDtl,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfRCDtlAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfRCDtlAttr.Val,value);
			}
		}
		#endregion

		#region ʵ�ֻ����ķ���
		/// <summary>
		/// uac
		/// </summary>
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
				
				Map map = new Map("GTS_PaperExamOfRCDtl");
				map.EnDesc="�Ծ��ʴ���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamOfRCDtlAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(PaperExamOfRCDtlAttr.FK_Paper,null,"����",true,true,0,50,20);
				map.AddTBIntPK( PaperExamOfRCDtlAttr.FK_RCDtl,1,"�Ķ��ʴ���",true,false);

				map.AddTBString(PaperExamOfRCDtlAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(PaperExamOfRCDtlAttr.Cent,1,"�÷�",true,true);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfEssayQuestionOne+this.CentOfEssayQuestionM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ķ��⿼��
		/// </summary> 
		public PaperExamOfRCDtl()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public PaperExamOfRCDtl(string paper,string empid, int fk_rcdtl )
		{
			this.FK_RCDtl =fk_rcdtl;
			this.FK_Emp=empid;
			this.FK_Paper=paper;

			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
			}
		}
		#endregion 

		#region �߼�����
		
		#endregion
	}
	/// <summary>
	///  �Ķ��⿼��
	/// </summary>
	public class PaperExamOfRCDtls :EntitiesOID
	{
		/// <summary>
		/// PaperExamOfRCDtls
		/// </summary>
		public PaperExamOfRCDtls(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamOfRCDtls(string fk_paper,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamOfRCDtlAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(PaperExamOfRCDtlAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}
		/// <summary>
		/// PaperExamOfRCDtl
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfRCDtl();
			}
		}
	}
}

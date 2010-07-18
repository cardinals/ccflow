using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ķ��⿼��attr
	/// </summary>
	public class WorkRandomOfRCDtlAttr:WorkRandomThemeBaseAttr
	{
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
	public class WorkRandomOfRCDtl :WorkRandomThemeBase
	{
		#region attrs
		/// <summary>
		/// �÷�
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkRandomOfRCDtlAttr.Cent);
			}
			set
			{
				this.SetValByKey(WorkRandomOfRCDtlAttr.Cent,value);
			}
		}
		/// <summary>
		/// FK_RCDtl
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(WorkRandomOfRCDtlAttr.FK_RCDtl);
			}
			set
			{
				this.SetValByKey(WorkRandomOfRCDtlAttr.FK_RCDtl,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfRCDtlAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfRCDtlAttr.Val,value);
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
				
				Map map = new Map("GTS_WorkRandomOfRCDtl");
				map.EnDesc="�Ծ��ʴ���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRandomOfRCDtlAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfRCDtlAttr.FK_WorkRandom,null,"�����ҵ",true,true,0,50,20);
				map.AddTBIntPK( WorkRandomOfRCDtlAttr.FK_RCDtl,0,"�Ķ��ʴ���",true,false);

				map.AddTBString(WorkRandomOfRCDtlAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(WorkRandomOfRCDtlAttr.Cent,0,"�÷�",true,true);
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
		public WorkRandomOfRCDtl()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfRCDtl(string work,string empid, int fk_rcdtl )
		{
			this.FK_RCDtl =fk_rcdtl;
			this.FK_Emp=empid;
			this.FK_WorkRandom=work;

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
	public class WorkRandomOfRCDtls :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfRCDtls
		/// </summary>
		public WorkRandomOfRCDtls(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfRCDtls(string FK_WorkRandom,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfRCDtlAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfRCDtlAttr.FK_WorkRandom, FK_WorkRandom);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfRCDtl
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfRCDtl();
			}
		}
	}
}

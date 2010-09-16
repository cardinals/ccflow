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
	public class WorkOfRCDtlAttr:WorkThemeBaseAttr
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
	public class WorkOfRCDtl :WorkThemeBase
	{
		#region attrs
		/// <summary>
		/// �÷�
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkOfRCDtlAttr.Cent);
			}
			set
			{
				this.SetValByKey(WorkOfRCDtlAttr.Cent,value);
			}
		}
		/// <summary>
		/// FK_RCDtl
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(WorkOfRCDtlAttr.FK_RCDtl);
			}
			set
			{
				this.SetValByKey(WorkOfRCDtlAttr.FK_RCDtl,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkOfRCDtlAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfRCDtlAttr.Val,value);
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
				
				Map map = new Map("GTS_WorkOfRCDtl");
				map.EnDesc="�Ծ��ʴ���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfRCDtlAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkOfRCDtlAttr.FK_Work,null,"����",true,true,0,50,20);
				map.AddTBIntPK( WorkOfRCDtlAttr.FK_RCDtl,1,"�Ķ��ʴ���",true,false);

				map.AddTBString(WorkOfRCDtlAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(WorkOfRCDtlAttr.Cent,1,"�÷�",true,true);
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
		public WorkOfRCDtl()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfRCDtl(string work,string empid, int fk_rcdtl )
		{
			this.FK_RCDtl =fk_rcdtl;
			this.FK_Emp=empid;
			this.FK_Work=work;

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
	public class WorkOfRCDtls :WorkThemeBases
	{
		/// <summary>
		/// WorkOfRCDtls
		/// </summary>
		public WorkOfRCDtls(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfRCDtls(string fk_work,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfRCDtlAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(WorkOfRCDtlAttr.FK_Work, fk_work);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfRCDtl
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfRCDtl();
			}
		}
	}
}

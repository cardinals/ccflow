using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ѡ���⿼��attr
	/// </summary>
	public class WorkRandomOfJudgeThemeAttr:WorkRandomThemeBaseAttr
	{
		/// <summary>
		/// ѡ����
		/// </summary>
		public const string FK_JudgeTheme="FK_JudgeTheme";
		/// <summary>
		/// ֵ
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// Answer
		/// </summary>
		public const string Answer="Answer";
	}
	/// <summary>
	/// ѡ���⿼��
	/// </summary>
	public class WorkRandomOfJudgeTheme :WorkRandomThemeBase
	{
		public bool IsRight
		{
			get
			{
				if (this.Val==this.Answer)
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Answer
		{
			get
			{
				 
				return this.GetValIntByKey(WorkRandomOfJudgeThemeAttr.Answer);
			}
			set
			{
				this.SetValByKey(WorkRandomOfJudgeThemeAttr.Answer,value);
			}
		}
		public string AnswerText
		{
			get
			{
				if (this.Answer==1)
					return "��ȷ";
				else
					return "����";
			}
		}
		

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
				
				Map map = new Map("GTS_WorkRandomOfJudgeTheme");
				map.EnDesc="ѡ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRandomOfJudgeThemeAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfJudgeThemeAttr.FK_WorkRandom,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkRandomOfJudgeThemeAttr.FK_JudgeTheme,null,"�ж���",true,true,0,50,20);
				map.AddTBInt(WorkRandomOfJudgeThemeAttr.Val,2,"val",true,false);
				map.AddTBInt(WorkRandomOfJudgeThemeAttr.Answer,1,"Answer",true,false);

				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// ѡ���⿼��
		/// </summary> 
		public WorkRandomOfJudgeTheme()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfJudgeTheme(string work,string empid,string fk_chose)
		{
			this.FK_JudgeTheme = fk_chose;
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
		/// <summary>
		/// FK_Chose
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkRandomOfJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public int Val
		{
			get
			{
				return this.GetValIntByKey(WorkRandomOfJudgeThemeAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfJudgeThemeAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ѡ���⿼��
	/// </summary>
	public class WorkRandomOfJudgeThemes :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfJudgeThemes
		/// </summary>
		public WorkRandomOfJudgeThemes(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfJudgeThemes(string FK_WorkRandom,string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfJudgeThemeAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfJudgeThemeAttr.FK_WorkRandom,FK_WorkRandom);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfJudgeTheme
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfJudgeTheme();
			}
		}
	}
}

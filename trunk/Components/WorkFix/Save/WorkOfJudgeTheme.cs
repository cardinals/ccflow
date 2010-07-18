using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ѡ���⿼��attr
	/// </summary>
	public class WorkOfJudgeThemeAttr:WorkThemeBaseAttr
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
	public class WorkOfJudgeTheme :WorkThemeBase
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
				 
				return this.GetValIntByKey(WorkOfChoseAttr.Answer);
			}
			set
			{
				this.SetValByKey(WorkOfChoseAttr.Answer,value);
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
				
				Map map = new Map("GTS_WorkOfJudgeTheme");
				map.EnDesc="�Ծ�ѡ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfJudgeThemeAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkOfJudgeThemeAttr.FK_Work,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkOfJudgeThemeAttr.FK_JudgeTheme,null,"�ж���",true,true,0,50,20);
				map.AddTBInt(WorkOfJudgeThemeAttr.Val,2,"val",true,false);
				map.AddTBInt(WorkOfJudgeThemeAttr.Answer,1,"Answer",true,false);

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
		public WorkOfJudgeTheme()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfJudgeTheme(string work,string empid,string fk_chose)
		{
			this.FK_JudgeTheme = fk_chose;
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
		/// <summary>
		/// FK_Chose
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(WorkOfJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkOfJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public int Val
		{
			get
			{
				return this.GetValIntByKey(WorkOfJudgeThemeAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfJudgeThemeAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ѡ���⿼��
	/// </summary>
	public class WorkOfJudgeThemes :WorkThemeBases
	{
		/// <summary>
		/// WorkOfJudgeThemes
		/// </summary>
		public WorkOfJudgeThemes(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfJudgeThemes(string fk_work,string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkOfChoseAttr.FK_Work,fk_work);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfJudgeTheme
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfJudgeTheme();
			}
		}
	}
}

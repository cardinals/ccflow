using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �ʴ��⿼��attr
	/// </summary>
	public class WorkRandomOfEssayQuestionAttr:WorkRandomThemeBaseAttr
	{
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string FK_EssayQuestion="FK_EssayQuestion";
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
	/// �ʴ��⿼��
	/// </summary>
	public class WorkRandomOfEssayQuestion :WorkRandomThemeBase
	{
		#region attrs
		/// <summary>
		/// �÷�
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkRandomOfEssayQuestionAttr.Cent);
			}
			set
			{
				this.SetValByKey(WorkRandomOfEssayQuestionAttr.Cent,value);
			}
		}
		/// <summary>
		/// FK_EssayQuestion
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkRandomOfEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfEssayQuestionAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfEssayQuestionAttr.Val,value);
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
				
				Map map = new Map("GTS_WorkRandomOfEssayQuestion");
				map.EnDesc="�ʴ���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRandomOfEssayQuestionAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfEssayQuestionAttr.FK_WorkRandom,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkRandomOfEssayQuestionAttr.FK_EssayQuestion,null,"�ʴ���",true,true,0,50,20);

				map.AddTBString(WorkRandomOfEssayQuestionAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(WorkRandomOfEssayQuestionAttr.Cent,0,"�÷�",true,true);

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
		/// �ʴ��⿼��
		/// </summary> 
		public WorkRandomOfEssayQuestion()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfEssayQuestion(string work,string empid,string fk_EssayQuestion)
		{
			this.FK_EssayQuestion = fk_EssayQuestion;
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
	///  �ʴ��⿼��
	/// </summary>
	public class WorkRandomOfEssayQuestions :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfEssayQuestions
		/// </summary>
		public WorkRandomOfEssayQuestions(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfEssayQuestions(string FK_WorkRandom,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfEssayQuestionAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfEssayQuestionAttr.FK_WorkRandom, FK_WorkRandom);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfEssayQuestion
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfEssayQuestion();
			}
		}
	}
}

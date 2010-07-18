using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �ʴ��⿼��attr
	/// </summary>
	public class WorkOfEssayQuestionAttr:WorkThemeBaseAttr
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
	public class WorkOfEssayQuestion :WorkThemeBase
	{
		#region attrs
		/// <summary>
		/// �÷�
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkOfEssayQuestionAttr.Cent);
			}
			set
			{
				this.SetValByKey(WorkOfEssayQuestionAttr.Cent,value);
			}
		}
		/// <summary>
		/// FK_EssayQuestion
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(WorkOfEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkOfEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkOfEssayQuestionAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfEssayQuestionAttr.Val,value);
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
				
				Map map = new Map("GTS_WorkOfEssayQuestion");
				map.EnDesc="�ʴ���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfEssayQuestionAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkOfEssayQuestionAttr.FK_Work,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkOfEssayQuestionAttr.FK_EssayQuestion,null,"�ʴ���",true,true,0,50,20);
				map.AddTBString(WorkOfEssayQuestionAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(WorkOfEssayQuestionAttr.Cent,0,"�÷�",true,true);

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
		public WorkOfEssayQuestion()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfEssayQuestion(string work,string empid,string fk_EssayQuestion)
		{
			this.FK_EssayQuestion = fk_EssayQuestion;
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
	///  �ʴ��⿼��
	/// </summary>
	public class WorkOfEssayQuestions :WorkThemeBases
	{
		/// <summary>
		/// WorkOfEssayQuestions
		/// </summary>
		public WorkOfEssayQuestions(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfEssayQuestions(string fk_work,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfEssayQuestionAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(WorkOfEssayQuestionAttr.FK_Work, fk_work);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfEssayQuestion
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfEssayQuestion();
			}
		}
	}
}

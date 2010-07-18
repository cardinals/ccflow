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
	public class PaperExamOfEssayQuestionAttr:EntityOIDAttr
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
	public class PaperExamOfEssayQuestion :Entity
	{
		#region attrs
		/// <summary>
		/// �÷�
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfEssayQuestionAttr.Cent);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.Cent,value);
			}
		}
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_EssayQuestion
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.Val,value);
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
				
				Map map = new Map("GTS_PaperExamOfEssayQuestion");
				map.EnDesc="�Ծ��ʴ���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamOfEssayQuestionAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(PaperExamOfEssayQuestionAttr.FK_Paper,null,"����",true,true,0,50,20);
				map.AddTBStringPK(PaperExamOfEssayQuestionAttr.FK_EssayQuestion,null,"�ʴ���",true,true,0,50,20);
				map.AddTBString(PaperExamOfEssayQuestionAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(PaperExamOfEssayQuestionAttr.Cent,0,"�÷�",true,true);

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
		public PaperExamOfEssayQuestion()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public PaperExamOfEssayQuestion(string paper,string empid,string fk_EssayQuestion)
		{

			this.FK_EssayQuestion = fk_EssayQuestion;
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
	///  �ʴ��⿼��
	/// </summary>
	public class PaperExamOfEssayQuestions :EntitiesOID
	{
		/// <summary>
		/// PaperExamOfEssayQuestions
		/// </summary>
		public PaperExamOfEssayQuestions(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamOfEssayQuestions(string fk_paper,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamOfEssayQuestionAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(PaperExamOfEssayQuestionAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}
		/// <summary>
		/// PaperExamOfEssayQuestion
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfEssayQuestion();
			}
		}
	}
}

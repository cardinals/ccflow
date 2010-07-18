using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ�
	/// </summary>
	public class PaperFixRandomAttr:EntityNoNameAttr
	{
		/// <summary>
		/// �Ծ�״̬
		/// </summary>
		public const string PaperState="PaperState";
		/// <summary>
		/// ����ʱ�����
		/// </summary>
		public const string MM="MM";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfChoseOne="CentOfChoseOne";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfChoseM="CentOfChoseM";
		/// <summary>
		/// �����
		/// </summary>
		public const string CentOfFillBlank="CentOfFillBlank";
		/// <summary>
		/// �ж���
		/// </summary>
		public const string CentOfJudgeTheme="CentOfJudgeTheme";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string CentOfEssayQuestion="CentOfEssayQuestion";
		/// <summary>
		/// CentOfRC
		/// </summary>
		public const string CentOfRC="CentOfRC";
		/// <summary>
		/// �ϼ�
		/// </summary>
		public const string CentOfSum="CentOfSum";
	}
	/// <summary>
	/// �Ծ�
	/// </summary>
	public class PaperFixRandom :EntityNoName
	{
		#region his attrs

		/// <summary>
		/// ���Լ���
		/// </summary>
		public PaperExams HisPaperExams
		{
			get
			{
				PaperExams chs = new PaperExams(this.No);
				return chs;
			}
		}
		/// <summary>
		/// ���ĵ���ѡ����
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				ChoseOnes chs = new ChoseOnes();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseOne where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// ���ĵ���ѡ����
		/// </summary>
		public ChoseMs HisChoseMs
		{
			get
			{
				ChoseMs chs = new ChoseMs();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// �ж���
		/// </summary>
		public JudgeThemes HisJudgeThemes
		{
			get
			{
				JudgeThemes chs = new JudgeThemes();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		public EssayQuestions HisEssayQuestions
		{
			get
			{
				EssayQuestions chs = new EssayQuestions();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// �Ķ������Ŀ
		/// </summary>
		public RCs HisRCs
		{
			get
			{
				RCs chs = new RCs();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// �����Ŀ
		/// </summary>
		public FillBlanks HisFillBlanks
		{
			get
			{
				FillBlanks chs = new FillBlanks();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs 
		public string HisPaperRandomNo
		{
			get
			{
				return this.GetValStringByKey(PaperAttr.HisPaperRandomNo);
			}
			set
			{
				this.SetValByKey(PaperAttr.HisPaperRandomNo,value);
			}
		}
		public string HisExamEmp
		{
			get
			{
				return this.GetValStringByKey(PaperAttr.HisExamEmp);
			}
			set
			{
				this.SetValByKey(PaperAttr.HisExamEmp,value);
			}
		}
		/// <summary>
		/// �Ծ�״̬
		/// </summary>
		public PaperState PaperState
		{
			get
			{
				return (PaperState)this.GetValIntByKey(PaperFixRandomAttr.PaperState);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.PaperState,(int)value);
			}
		}
		public string PaperStateText
		{
			get
			{
				return this.GetValRefTextByKey(PaperFixRandomAttr.PaperState);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// ÿ������ѡ����÷֡�
		/// </summary>
		public int SumCentOfChoseOne
		{
			get
			{
				try
				{
					return this.CentOfChoseOne*this.HisChoseOnes.Count;
				}
				catch
				{
					return 0 ;
				}
			}
		}
		/// <summary>
		/// duo��ѡ����÷֡�
		/// </summary>
		public int SumCentOfChoseM
		{
			get
			{
				try
				{
					return this.CentOfChoseM * this.HisChoseMs.Count;
				}
				catch
				{
					return 0;
				}
			}
		}
		/// <summary>
		/// �ж���ķ֡�
		/// </summary>
		public int SumCentOfJudgeTheme
		{
			get
			{
				try
				{
					return this.CentOfJudgeTheme * this.HisJudgeThemes.Count;
				}
				catch
				{
					return 0;
				}
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		public int SumCentOfFillBlank
		{
			get
			{
				try
				{
					int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"' )");
					return this.CentOfFillBlank *blankNum;
				}
				catch
				{
					return 0 ;
				}
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
				
				Map map = new Map("GTS_PaperFixRandom");
				map.EnDesc="����Ծ�";
				map.CodeStruct="4";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperFixRandomAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(PaperFixRandomAttr.Name,null,"�Ծ�����",true,false,0,50,20);
				map.AddDDLSysEnum(PaperFixRandomAttr.PaperState,0,"�Ծ�״̬",true,true);				 
				map.AddTBInt(PaperFixRandomAttr.MM,90,"����ʱ��(����)",true,false);

				map.AddTBInt(PaperFixRandomAttr.CentOfChoseOne,0,"��ѡ��ÿ���",true,false);
				map.AddTBInt(PaperFixRandomAttr.CentOfChoseM,0,"��ѡ��ÿ���",true,false);
				map.AddTBInt(PaperFixRandomAttr.CentOfFillBlank,0,"�����ÿ���",true,false);

				map.AddTBInt(PaperFixRandomAttr.CentOfJudgeTheme,0,"�ж���ÿ���",true,false);
				map.AddTBInt(PaperFixRandomAttr.CentOfEssayQuestion,0,"�ʴ���ÿ���",true,true);

				map.AddTBInt(PaperFixRandomAttr.CentOfRC,0,"�Ķ����ܷ�",true,true);
				map.AddTBInt(PaperFixRandomAttr.CentOfSum,0,"�ܷ�",true,true);

				map.AddTBString(PaperAttr.HisExamEmp,null,"HisExamEmp",false,true,0,50,20);
				map.AddTBString(PaperAttr.HisPaperRandomNo,null,"HisExamEmp",false,false,0,50,20);

				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"�����");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж���");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"�ʴ���");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"�Ķ���");
				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new EmpExts(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"���Ե�ѧ��");

				//map.AddDtl(new PaperFixRandomExams(),PaperFixRandomExamAttr.FK_Paper);
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);
				map.AddSearchAttr(PaperFixRandomAttr.PaperState );

				//map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new choseonhemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");
				//map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override void afterDelete()
		{
			DBAccess.RunSQL("DELETE GTS_PaperVSChoseOne WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSChoseM WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSJudgeTheme WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSFillBlank WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSEssayQuestion WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSRC WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSRCDtl WHERE FK_Paper='"+this.No+"'");
		
			DBAccess.RunSQL("DELETE GTS_PaperExam WHERE FK_Paper='"+this.No+"'");
			base.afterDelete ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			this.CentOfEssayQuestion = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_PaperVSEssayQuestion where FK_Paper='"+this.No+"'") ;
			this.CentOfRC = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_PaperVSRCDtl where FK_Paper='"+this.No+"'") ;
			this.CentOfSum=this.SumCentOfFillBlank+this.CentOfEssayQuestion+this.SumCentOfChoseOne+this.SumCentOfChoseM+this.SumCentOfJudgeTheme+this.CentOfRC;
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�
		/// </summary> 
		public PaperFixRandom()
		{
		}
		/// <summary>
		/// �Ծ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public PaperFixRandom(string _No ):base(_No)
		{

		}
		#endregion 

		#region �߼�����
		#endregion

	}
	/// <summary>
	///  �Ծ�
	/// </summary>
	public class PaperFixRandoms :EntitiesNoName
	{
		/// <summary>
		/// PaperFixRandoms
		/// </summary>
		public PaperFixRandoms(){}

		public PaperFixRandoms(string fk_randomNo, string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperAttr.HisExamEmp, fk_emp);
			qo.addAnd();
			qo.AddWhere(PaperAttr.HisPaperRandomNo, fk_randomNo);
			qo.DoQuery();
		}
		/// <summary>
		/// PaperFixRandom
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperFixRandom();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public int RetrievePaperFixRandom(PaperState state )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperFixRandomAttr.PaperState, (int)state);
			return qo.DoQuery();
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrievePaperFixRandom(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixRandomAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}

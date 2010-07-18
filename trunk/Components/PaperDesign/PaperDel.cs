using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ�attr
	/// </summary>
	public class PaperAttr:EntityNoNameAttr
	{
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
		/// <summary>
		/// �Ƿ��鷶Χ
		/// </summary>
		public const string IsCheckScope="IsCheckScope";
	}
	/// <summary>
	/// �Ծ�����
	/// </summary>
	public enum PaperType
	{
		/// <summary>
		/// ��׼�Ծ�
		/// </summary>
		Standard,
		/// <summary>
		/// ���ѧ���Ծ�
		/// </summary>
		Random
	}
	/// <summary>
	/// �Ծ�״̬
	/// </summary>
	public enum PaperState
	{
		/// <summary>
		/// ����
		/// </summary>
		Secrecy,
		/// <summary>
		/// ����
		/// </summary>
		Examing,
		/// <summary>
		/// ����
		/// </summary>
		Cancellation,
	}
	/// <summary>
	/// �Ծ�
	/// </summary>
	public class Paper :EntityNoName
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseOne where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs
		/// <summary>
		/// �Ծ�����
		/// </summary>
		public PaperType PaperType
		{
			get
			{
				return (PaperType)this.GetValIntByKey(PaperAttr.PaperType);
			}
			set
			{
				this.SetValByKey(PaperAttr.PaperType,(int)value);
			}
		}
		/// <summary>
		/// �Ծ�״̬
		/// </summary>
		public PaperState PaperState
		{
			get
			{
				return (PaperState)this.GetValIntByKey(PaperAttr.PaperState);
			}
			set
			{
				this.SetValByKey(PaperAttr.PaperState,(int)value);
			}
		}
		public string PaperStateText
		{
			get
			{
				return this.GetValRefTextByKey(PaperAttr.PaperState);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// ����Ծ��š�
		/// </summary>
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
		/// �ж�
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// ÿ������ѡ����÷֡�
		/// </summary>
		public int PerCentOfChoseOne
		{
			get
			{
				try
				{
					return this.CentOfChoseOne/this.HisChoseOnes.Count;
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
		public int PerCentOfChoseM
		{
			get
			{
				try
				{
					return this.CentOfChoseM/this.HisChoseMs.Count;
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
		public int PerCentOfJudgeTheme
		{
			get
			{
				try
				{
					return this.CentOfJudgeTheme/this.HisJudgeThemes.Count;
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
		public int PerCentOfFillBlank
		{
			get
			{
				try
				{
					int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_paper='"+this.No+"' )");
					return this.CentOfFillBlank/blankNum;
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
				
				Map map = new Map("V_GTS_Paper");
				map.EnDesc="�̶��Ծ�";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(PaperAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(PaperAttr.Name,null,"�Ծ�����",true,false,0,50,20);
				map.AddDDLSysEnum(PaperAttr.PaperState,0,"�Ծ�״̬",true,true);
				map.AddDDLSysEnum(PaperAttr.PaperType,0,"�Ծ�����",true,true);
				map.AddTBInt(PaperAttr.MM,90,"����ʱ��(����)",true,false);

				map.AddTBInt(PaperAttr.CentOfChoseOne,0,"��ѡ���",true,false);
				map.AddTBInt(PaperAttr.CentOfChoseM,0,"��ѡ���",true,false);
				map.AddTBInt(PaperAttr.CentOfFillBlank,0,"������",true,false);
				map.AddTBInt(PaperAttr.CentOfJudgeTheme,0,"�ж����",true,false);
				map.AddTBInt(PaperAttr.CentOfEssayQuestion,0,"�ʴ����",true,true);
				map.AddTBInt(PaperAttr.CentOfRC,0,"�Ķ����",true,true);
				map.AddTBInt(PaperAttr.CentOfSum,0,"�ܷ�",true,true);

				map.AddTBString(PaperAttr.HisExamEmp,"a","HisExamEmp",false,true,0,50,20);
				map.AddTBString(PaperAttr.HisPaperRandomNo,"a","HisExamEmp",false,false,0,50,20);



 


				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"�����");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж���");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"�ʴ���");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"�Ķ���");
				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new EmpExts(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"���Ե�ѧ��");

				//map.AddDtl(new PaperExams(),PaperExamAttr.FK_Paper);
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);
				map.AddSearchAttr(PaperAttr.PaperState );
				map.AddSearchAttr(PaperAttr.PaperType );

				//map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new choseonhemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");
				//map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�
		/// </summary> 
		public Paper()
		{
		}
		/// <summary>
		/// �Ծ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public Paper(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		#endregion

	}
	/// <summary>
	///  �Ծ�
	/// </summary>
	public class Papers :EntitiesNoName
	{
		/// <summary>
		/// Papers
		/// </summary>
		public Papers(){}
		/// <summary>
		/// Paper
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Paper();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public int RetrievePaper(PaperState state )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperAttr.PaperState, (int)state);
			return qo.DoQuery();
		}
		/// <summary>
		/// ��ѯ������ǰ�ҵĹ̶�paper.
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrievePaper(string fk_emp, PaperState state )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Paper NOT IN (SELECT FK_Paper FROM GTS_PaperExam) AND len(FK_Paper)=5 and FK_Emp='"+fk_emp+"'");
			qo.addAnd();
			qo.AddWhere(PaperAttr.PaperState, (int)state);
			return qo.DoQuery();
		}
		public int RetrieveHisRandom (string fk_emp, string fk_random  )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperAttr.HisExamEmp,fk_emp );
			qo.addAnd();
			qo.AddWhere(PaperAttr.HisPaperRandomNo, fk_random );
			return qo.DoQuery();
		}
	}

	 
}

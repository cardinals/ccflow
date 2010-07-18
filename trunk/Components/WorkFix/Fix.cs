using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ��
	/// </summary>
	public class FixAttr:EntityNoNameAttr
	{
		/// <summary>
		/// �Ծ�״̬
		/// </summary>
		public const string State="State";
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
	/// �̶���ҵ
	/// </summary>
	public class Fix :EntityNoName
	{
		#region attrs
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfChoseM,value);
			}
		}
		#endregion 

		#region his attrs
		/// <summary>
		/// ���Լ���
		/// </summary>
		public Exams HisExams
		{
			get
			{
				Exams chs = new Exams(this.No);
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_VSChoseOne where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_VSChoseM where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_VSJudgeTheme where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_VSEssayQuestion where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_VSRC where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_VSFillBlank where FK_='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs 

		public string ValidTimeFrom
		{
			get
			{
				return this.GetValStringByKey(RandomAttr.ValidTimeFrom);
			}
			set
			{
				this.SetValByKey(RandomAttr.ValidTimeFrom,value);
			}
		}
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(RandomAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(RandomAttr.ValidTimeTo,value);
			}
		}
		/// <summary>
		/// �Ƿ񳬹���ʱ��
		/// </summary>
		public bool IsPastTime
		{
			get
			{

				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;
				if ( dtto>= dt )
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// �Ƿ��ڿ�������ҵ��ʱ�䷶Χ��
		/// </summary>
		public bool IsValid
		{
			get
			{
				DateTime dtfrom=DataType.ParseSysDateTime2DateTime(this.ValidTimeFrom);
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;

				if ( dtfrom <= dt )
				{
					if ( dtto>= dt )
						return true;
					else
						return false;
				}
				else
					return false;
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(FixAttr.MM);
			}
			set
			{
				this.SetValByKey(FixAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		public bool IsExam
		{
			get
			{
				if (this.MM==0)
					return false;
				else
					return true;
			}
		}
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
					int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_VSFillBlank where FK_='"+this.No+"' )");
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
				
				Map map = new Map("GTS_Fix");
				map.EnDesc="�̶������";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(FixAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(FixAttr.Name,"�½�1","����",true,false,0,50,20);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(1);
				map.AddTBDateTime(RandomAttr.ValidTimeFrom,dt.ToString("yyyy-MM-dd")+" 09:00","��Чʱ���",true,false);
				map.AddTBDateTime(RandomAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","��",true,false);

				map.AddTBInt(FixAttr.MM,90,"����ʱ��(����)",true,false);

				map.AddTBInt(FixAttr.CentOfChoseOne,1,"��ѡ��ÿ���",true,false);
				map.AddTBInt(FixAttr.CentOfChoseM,1,"��ѡ��ÿ���",true,false);
				map.AddTBInt(FixAttr.CentOfFillBlank,1,"�����ÿ���",true,false);

				map.AddTBInt(FixAttr.CentOfJudgeTheme,1,"�ж���ÿ���",true,false);
				map.AddTBInt(FixAttr.CentOfEssayQuestion,1,"�ʴ����",true,true);

				map.AddTBInt(FixAttr.CentOfRC,20,"�Ķ����",true,true);
				map.AddTBInt(FixAttr.CentOfSum,0,"�ܷ�",true,true);

				map.AttrsOfOneVSM.Add( new VSChoseOnes(), new ChoseOnes(),VSChoseOneAttr.FK_,VSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new VSChoseMs(), new ChoseMs(),VSChoseMAttr.FK_,VSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new VSFillBlanks(), new FillBlanks(),VSFillBlankAttr.FK_,VSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"�����");
				map.AttrsOfOneVSM.Add( new VSJudgeThemes(), new JudgeThemes(),VSJudgeThemeAttr.FK_,VSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж���");
				map.AttrsOfOneVSM.Add( new VSEssayQuestions(), new EssayQuestions(),VSEssayQuestionAttr.FK_,VSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"�ʴ���");
				map.AttrsOfOneVSM.Add( new VSRCs(), new RCs(),VSRCAttr.FK_,VSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"�Ķ���");

				map.AttrsOfOneVSM.Add( new VSEmps(), new EmpExts(),VSEmpAttr.FK_,VSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"���Ե�ѧ��");
				map.AddDtl(new VSEssayQuestions(),VSEssayQuestionAttr.FK_);
				map.AddDtl(new VSRCDtls(),VSRCAttr.FK_);

				this._enMap=map;
				return this._enMap;
			}
		}
		protected override void afterDelete()
		{
			DBAccess.RunSQL("DELETE GTS_VSChoseOne WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSChoseM WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSJudgeTheme WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSFillBlank WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSEssayQuestion WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSRC WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSRCDtl WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_Exam WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSEmp WHERE FK_='"+this.No+"'");
			base.afterDelete ();
		}

		protected override bool beforeUpdateInsertAction()
		{

			// �ж��Ƿ��п������⡣
			string sql="SELECT COUNT(*) FROM GTS_Exam WHERE ExamState >0 and FK_='"+this.No+"'";
			int i=DBAccess.RunSQLReturnValInt(sql);
			if (i>=1)
				throw new Exception("�Ծ�["+this.Name+"], ���ܱ�������ƣ���Ϊ���Ѿ��С�"+i+"����������ʼ���⡣");

			// ���÷�����
			this.CentOfEssayQuestion = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_VSEssayQuestion where FK_='"+this.No+"'") ;
			this.CentOfRC = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_VSRCDtl where FK_='"+this.No+"'") ;
			this.CentOfSum=this.SumCentOfFillBlank+this.CentOfEssayQuestion+this.SumCentOfChoseOne+this.SumCentOfChoseM+this.SumCentOfJudgeTheme+this.CentOfRC;

			// ��ʼ������ , �ڿ���ʱ�����ÿ�����Χ, ��ÿ��������ʼ�������Ծ�
			sql="DELETE GTS_Exam WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_VSEmp WHERE FK_='"+this.No+"') AND FK_='"+this.No+"'";
			DBAccess.RunSQL(sql); // ɾ��û�С�

			sql="SELECT FK_Emp FROM GTS_VSEmp WHERE FK_='"+this.No+"' AND FK_Emp ";
			sql+="NOT IN (SELECT FK_Emp FROM GTS_Exam WHERE FK_='"+this.No+"')";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);
			foreach(DataRow dr in dt.Rows)
			{
				Exam pe = new Exam();
				pe.FK_Emp=dr[0].ToString();
				pe.FK_=this.No;
				pe.IsExam=this.IsExam;
				pe.Insert();
			}
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�
		/// </summary> 
		public Fix()
		{
		}
		/// <summary>
		/// �Ծ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public Fix(string _No ):base(_No)
		{

		}
		#endregion 

		#region �߼�����
		#endregion

	}
	/// <summary>
	///  �̶���ҵ
	/// </summary>
	public class Fixs :EntitiesNoName
	{
		/// <summary>
		/// �̶���ҵs
		/// </summary>
		public Fixs(){}

		/// <summary>
		/// �̶���ҵs
		/// </summary>
		/// <param name="fk_emp">����Ա</param>
		public Fixs(string fk_emp)
		{
			QueryObject qo= new QueryObject(this);
			qo.AddWhereInSQL(FixAttr.No, "SELECT FK_ FROM GTS_VSEmp WHERE FK_Emp='"+Web.WebUser.No+"'" );
			qo.DoQuery();
		}

		/// <summary>
		/// �̶���ҵ
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Fix();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrieveFix(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(FixAttr.No,  "SELECT FK_ FROM GTS_VSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}

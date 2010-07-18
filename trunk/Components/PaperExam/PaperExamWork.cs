using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ��attr
	/// </summary>
	public class PaperExamAttr:EntityOIDAttr
	{
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// ��
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// �ɼ��ȼ�
		/// </summary>
		public const string FK_Level="FK_Level";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const string ExamState="ExamState";
		/// <summary>
		/// dept
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// from datatime
		/// </summary>
		public const string FromDateTime="FromDateTime";
		/// <summary>
		/// to datetime
		/// </summary>
		public const string ToDateTime="ToDateTime";
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
		/// ��׼��
		/// </summary>
		public const string CentOfStd="CentOfStd";
		/// <summary>
		/// �Ƿ��ǿ���
		/// </summary>
		public const string IsExam="IsExam";
	}
	/// <summary>
	/// ����״̬
	/// </summary>
	public enum PaperExamState
	{
		/// <summary>
		/// ��ʼ��
		/// </summary>
		Init,
		/// <summary>
		/// ������
		/// </summary>
		Examing,
		/// <summary>
		/// �ľ���
		/// </summary>
		Reading,
		/// <summary>
		/// �ľ����
		/// </summary>
		ReadOver,
	}
	/// <summary>
	/// �Ծ��
	/// </summary>
	public class PaperExam :EntityOID
	{
		#region ����
		/// <summary>
		/// ExamStateText
		/// </summary>
		public string ExamStateText
		{
			get
			{
				//if (this.
				return this.GetValRefTextByKey( PaperExamAttr.ExamState);
			}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public PaperExamState ExamState
		{
			get
			{
				return (PaperExamState)this.GetValIntByKey( PaperExamAttr.ExamState);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.ExamState,(int)value);
			}
		}
		/// <summary>
		/// �Ƿ��ǿ���
		/// </summary>
		public bool IsExam
		{
			get
			{
				return this.GetValBooleanByKey( PaperExamAttr.IsExam);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.IsExam,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey( PaperExamAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// paper.
		/// </summary>
		public PaperFix HisPaper
		{
			get
			{
				return new PaperFix(this.FK_Paper);
			}
		}
		#endregion

		/// <summary>
		/// �Զ��ľ�:
		/// </summary>
		public void AutoReadPaper()
		{
			PaperFix pp = new PaperFix(this.FK_Paper) ;
			// ѡ����.
			PaperExamOfChoses  pes = new PaperExamOfChoses(this.FK_Paper,this.FK_Emp);
			ChoseOnes chs =this.HisChoseOnes;
			int countOne=0;
			int countM=0;
			foreach(PaperExamOfChose pe in pes)
			{
				if (pe.Val.Length==0)
					continue;
				/* */
				ChoseItems cts = new ChoseItems();
				cts.RetrieveRightItems(pe.FK_Chose); //��ѯ������ȷ����Ŀ��
				if (cts.Val==pe.Val)
				{
					if (pe.Val.Length==1)
						countOne++; // ����
					else
						countM++; // ���
				}
			}

			this.CentOfChoseOne= countOne*pp.CentOfChoseOne; // ����ѡ����÷֡�
			this.CentOfChoseM= countM*pp.CentOfChoseM; // duo��ѡ����÷֡�

			// �ж���
			PaperExamOfJudgeThemes  pej = new PaperExamOfJudgeThemes(this.FK_Paper,this.FK_Emp);
			int count=0;
			foreach(PaperExamOfJudgeTheme pe in pej)
			{
				
				JudgeTheme jt =new JudgeTheme(pe.FK_JudgeTheme);
				if (pe.Val==jt.IsOkOfInt)
				{
					count++;
				}
			}
			this.CentOfJudgeTheme= count*pp.CentOfJudgeTheme; // duo��ѡ����÷֡�


			// �����
			PaperExamOfFillBlanks  peb = new PaperExamOfFillBlanks(this.FK_Paper, this.FK_Emp);
			count=0;
			foreach(PaperExamOfFillBlank pe in peb)
			{
				FillBlank fb= new FillBlank(pe.FK_FillBlank);
				if (pe.Val.Trim()==fb.HisKeys[pe.IDX].Trim() )
				{
					/*  */
					count++;
				}
			}
			this.CentOfFillBlank= count*pp.CentOfFillBlank; // blank��ѡ����÷֡�

			PaperExamOfEssayQuestions  pee = new PaperExamOfEssayQuestions(this.FK_Paper, this.FK_Emp);
			if (pee.Count==0)
			{
				/*���û�м����*/
				this.ExamState=PaperExamState.ReadOver;
			}
			else
			{
				if (this.ExamState==PaperExamState.Examing || this.ExamState==PaperExamState.Init )
					this.ExamState=PaperExamState.Reading;
			}

			this.DoResetLevel( pp.CentOfSum );
		}
		/// <summary>
		/// ִ���������ü���
		/// </summary>
		public void DoResetLevel(int centOfSum)
		{
			
			PaperFix pf= new PaperFix(this.FK_Paper);

			// ��ʼ�����׼�֡�
			this.CentOfStd=centOfSum / pf.CentOfSum*100 ;
			//int sume pf.CentOfSum

			if (this.CentOfStd > 90)
			{
				this.FK_Level="4";
				this.Update();
				return;
			}
			if (this.CentOfStd >80)
			{
				this.FK_Level="3";
				this.Update();
				return;
			}

			if (this.CentOfStd >60)
			{
				this.FK_Level="2";
				this.Update();
				return;
			}
			this.FK_Level="1";
			this.Update();
		}

		#region his attrs
		/// <summary>
		/// ���ĵ���ѡ����
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				ChoseOnes chs = new ChoseOnes();
				QueryObject qo = new QueryObject(chs);
				//qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperExamVSChoseOne where fk_PaperExam='"+this.No+"'");
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
				//qo.AddWhereInSQL( ChoseOneAttr.FK_Chose, "select fk_chose from GTS_PaperExamVSChoseM where fk_PaperExam='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion


		#region attrs
		 
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// FK_EmpText
		/// </summary>
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(PaperExamAttr.FK_Emp);
			}
		}
		/// <summary>
		/// FK_Paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// �ɼ��ȼ�
		/// </summary>
		public string FK_Level
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FK_Level);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Level,value);
			}
		}
		public string FK_PaperText
		{
			get
			{
				return this.GetValRefTextByKey(PaperExamAttr.FK_Paper);
			}
		}
		/// <summary>
		/// ��ʱ��
		/// </summary>
		public string FromDateTime
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FromDateTime);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FromDateTime,value);
			}
		}
		/// <summary>
		/// ��ʱ��
		/// </summary>
		public string ToDateTime
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.ToDateTime);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.ToDateTime,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfEssayQuestion,value);
			}
		}
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ��׼��
		/// </summary>
		public int CentOfStd
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfStd);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfStd,value);
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
				uc.IsView=true;
				return uc;
			}
		}

		/// <summary>
		/// �Ƿ񳬳���ʱ��
		/// </summary>
		public bool IsOutTime
		{
			get
			{
				return false;
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
				
				Map map = new Map("GTS_PaperExam");
				map.EnDesc="����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBIntPKOID();
				map.AddDDLSysEnum(PaperExamAttr.ExamState,0,"״̬",true,false);
				map.AddDDLEntities(PaperExamAttr.FK_Paper,null,"�Ծ�",new PaperFixs(),false);
				map.AddDDLEntities(PaperExamAttr.FK_Emp,Web.WebUser.No,"ѧ��",new EmpExts(),false);
				map.AddDDLEntities(PaperExamAttr.FK_Dept,Web.WebUser.FK_Dept,"����",new Depts(),false);
			
				map.AddTBInt(PaperExamAttr.CentOfChoseOne,0,"��ѡ���",true,true);
				map.AddTBInt(PaperExamAttr.CentOfChoseM,0,"��ѡ���",true,true);
				map.AddTBInt(PaperExamAttr.CentOfFillBlank,0,"������",true,true);
				map.AddTBInt(PaperExamAttr.CentOfJudgeTheme,0,"�ж����",true,true);
				map.AddTBInt(PaperExamAttr.CentOfEssayQuestion,0,"�ʴ����",true,true);
				map.AddTBInt(PaperExamAttr.CentOfRC,0,"�Ķ�������",true,true);
				map.AddTBInt(PaperExamAttr.CentOfSum,0,"�ܷ�",true,true);
				map.AddTBInt(PaperExamAttr.CentOfStd,0,"��׼��",true,true);
				map.AddDDLEntities(PaperExamAttr.FK_Level,"1","�ɼ��ȼ�",new Levels(),false);
				map.AddTBDateTime(PaperExamAttr.FromDateTime,"��ʱ��",true,true);
				map.AddTBDateTime(PaperExamAttr.ToDateTime,"��",true,true);

				map.AddBoolean(PaperExamAttr.IsExam,false,"�Ƿ��ǿ���",true,false);

				map.AddSearchAttr(PaperExamAttr.FK_Paper);
				map.AddSearchAttr(PaperExamAttr.FK_Dept);
				map.AddSearchAttr(PaperExamAttr.ExamState);
				map.AddSearchAttr(PaperExamAttr.FK_Level);



				/*
				map.AttrsOfOneVSM.Add( new PaperExamVSChoseOnes(), new ChoseOnes(),PaperExamVSChoseOneAttr.FK_PaperExam,PaperExamVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperExamVSChoseMs(), new ChoseMs(),PaperExamVSChoseMAttr.FK_PaperExam,PaperExamVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperExamVSFillBlanks(), new FillBlanks(),PaperExamVSFillBlankAttr.FK_PaperExam,PaperExamVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"�����");
				map.AttrsOfOneVSM.Add( new PaperExamVSJudgeThemes(), new JudgeThemes(),PaperExamVSJudgeThemeAttr.FK_PaperExam,PaperExamVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж���");
				map.AttrsOfOneVSM.Add( new PaperExamVSEssayQuestions(), new EssayQuestions(),PaperExamVSEssayQuestionAttr.FK_PaperExam,PaperExamVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"�ʴ���");
			    map.AddDtl(new PaperExamVSEssayQuestions(),PaperExamVSEssayQuestionAttr.FK_PaperExam);
				*/

				//map.AttrsOfOneVSM.Add( new PaperExamVSChoseOnes(), new choseonhemes(),PaperExamVSJudgeThemeAttr.FK_PaperExam,PaperExamVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");
				//map.AttrsOfOneVSM.Add( new PaperExamVSChoseMs(), new JudgeThemes(),PaperExamVSJudgeThemeAttr.FK_PaperExam,PaperExamVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");
  
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeInsert()
		{

			if (this.Search(this.FK_Paper,this.FK_Emp)>=1)
				throw new Exception("�Ѿ������ѧ��"+this.FK_Emp+"���Ծ�.");

		 
			Emp emp = new Emp(this.FK_Emp);
			this.FK_Dept = emp.FK_Dept;

			return base.beforeInsert ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			 
			
			//this.FK_Dept =
			//this.MM =DataType.GetSpanMinute(this.FromDateTime,this.ToDateTime);
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ��
		/// </summary> 
		public PaperExam()
		{
		}
		public PaperExam(int _oid) : base(_oid){}	
		public PaperExam(string paper, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Paper=paper;
			if (this.Retrieve("FK_Emp",fk_emp, "FK_Paper", paper)==0)
				throw new Exception("����û�����ɿ���[��"+fk_emp+"��]������Ϣ��");
		}
		#endregion 

		public int Search(string paper, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Paper=paper;
			QueryObject qo = new QueryObject(this);			
			qo.AddWhere(PaperExamAttr.FK_Emp,fk_emp);
			qo.addAnd();
			qo.AddWhere(PaperExamAttr.FK_Paper,paper);
			return qo.DoQuery();
		}

		#region �߼�����
		/// <summary>
		/// �ϼƵ÷֡�
		/// </summary>
		public void SumIt()
		{
		}
		#endregion

	}
	/// <summary>
	///  �Ծ��
	/// </summary>
	public class PaperExams :EntitiesOID
	{
		/// <summary>
		/// 
		/// </summary>
		public void AutoReadPaper()
		{
			foreach(PaperExam pe in this)
			{
				pe.AutoReadPaper();
			}
		}
		/// <summary>
		/// PaperExams
		/// </summary>
		public PaperExams(){}
		public PaperExams(string fk_paper)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( PaperExamAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}
		public int Search(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( PaperExamAttr.FK_Emp, fk_emp);
			return qo.DoQuery();
		}



		 
		/// <summary>
		/// PaperExam
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExam();
			}
		}
	}
}

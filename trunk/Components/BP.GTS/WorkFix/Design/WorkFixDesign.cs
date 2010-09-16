using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �̶���ҵ
	/// </summary>
	public class WorkFixDesignAttr:EntityNoNameAttr
	{
		/// <summary>
		/// �Ծ�״̬
		/// </summary>
		public const string WorkState="WorkState";
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
		/// ��
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";

	}
	/// <summary>
	/// �̶���ҵ
	/// </summary>
	public class WorkFixDesign :EntityNoName
	{
		#region attrs
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfChoseM,value);
			}
		}
		#endregion 

		#region his attrs
		/// <summary>
		/// ���ĵ���ѡ����
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				//ChoseOnes ens = new ChoseOnes();
				if (BP.DA.Cash.IsExits("ChoseOnes"+this.No,Depositary.Session) ==false )
				{
					ChoseOnes chs = new ChoseOnes();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "SELECT FK_Chose FROM GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("ChoseOnes"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (ChoseOnes)BP.DA.Cash.GetObjFormSession("ChoseOnes"+this.No);
				//				if (ens.Count==0)
				//				{
				//
				//				}
			}
		}
		/// <summary>
		/// ���ĵ���ѡ����
		/// </summary>
		public ChoseMs HisChoseMs
		{
			get
			{
				if (BP.DA.Cash.IsExits("HisChoseMs"+this.No,Depositary.Session) ==false )
				{
					ChoseMs chs = new ChoseMs();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_WorkVSChoseM where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("HisChoseMs"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (ChoseMs)BP.DA.Cash.GetObjFormSession("HisChoseMs"+this.No);
			}
		}
		/// <summary>
		/// �ж���
		/// </summary>
		public JudgeThemes HisJudgeThemes
		{
			get
			{
				if (BP.DA.Cash.IsExits("JudgeThemes"+this.No,Depositary.Session) ==false )
				{
					JudgeThemes chs = new JudgeThemes();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_WorkVSJudgeTheme where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("JudgeThemes"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (JudgeThemes)BP.DA.Cash.GetObjFormSession("JudgeThemes"+this.No);
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		public EssayQuestions HisEssayQuestions
		{
			get
			{
				if (BP.DA.Cash.IsExits("EssayQuestions"+this.No,Depositary.Session) ==false )
				{
					EssayQuestions chs = new EssayQuestions();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_WorkVSEssayQuestion where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("EssayQuestions"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (EssayQuestions)BP.DA.Cash.GetObjFormSession("EssayQuestions"+this.No);

			}
		}
		/// <summary>
		/// �Ķ������Ŀ
		/// </summary>
		public RCs HisRCs
		{
			get
			{
				if (BP.DA.Cash.IsExits("RCs"+this.No,Depositary.Session) ==false )
				{
					RCs chs = new RCs();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_WorkVSRC where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("RCs"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (RCs)BP.DA.Cash.GetObjFormSession("RCs"+this.No);

			}
		}
		/// <summary>
		/// �����Ŀ
		/// </summary>
		public FillBlanks HisFillBlanks
		{
			get
			{
				if (BP.DA.Cash.IsExits("FillBlanks"+this.No,Depositary.Session) ==false )
				{
					FillBlanks chs = new FillBlanks();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_WorkVSFillBlank where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("FillBlanks"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (FillBlanks)BP.DA.Cash.GetObjFormSession("FillBlanks"+this.No);
			}
		}
		#endregion

		#region attrs 
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(WorkFixDesignAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.ValidTimeTo,value);
			}
		}
		/// <summary>
		/// �Ƿ񳬹���ʱ��
		/// </summary>
		public bool IsPastTime_del
		{
			get
			{
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;
				if ( dtto>= dt)
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
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo+" 10:10");
				DateTime dt=DateTime.Now;
				if ( dtto>= dt )
					return true;
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
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfSum,value);
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
				string sql="SELECT COUNT(*) FROM GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return i*this.CentOfChoseOne;
			}
		}
		/// <summary>
		/// duo��ѡ����÷֡�
		/// </summary>
		public int SumCentOfChoseM
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_WorkVSChoseM WHERE FK_Work='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return i*this.CentOfChoseM;
			}
		}
		/// <summary>
		/// �ж���ķ֡�
		/// </summary>
		public int SumCentOfJudgeTheme
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_WorkVSJudgeTheme WHERE FK_Work='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return i*this.CentOfJudgeTheme;
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
					int blankNum=DBAccess.RunSQLReturnValInt("SELECT SUM(BlankNum) FROM GTS_FillBlank WHERE No in  ( SELECT FK_FillBlank from GTS_WorkVSFillBlank where FK_Work='"+this.No+"' )");
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
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GTS_WorkFixDesign");
                map.EnDesc = "��ҵ����";
                map.CodeStruct = "4";
                map.EnType = EnType.Admin;
                map.AddTBStringPK(WorkFixDesignAttr.No, null, "���", true, true, 0, 50, 20);
                map.AddTBString(WorkFixDesignAttr.Name, "�½���ҵ1", "��ҵ����", true, false, 0, 50, 20);

                DateTime dt = DateTime.Now;
                dt = dt.AddDays(7);
                //map.AddTBDate(WorkFixDesignAttr.ValidTimeFrom,DateTime.Now.ToString("yyyy-MM-dd"),"��Ч���ڴ�",true,false);
                map.AddTBDate(WorkFixDesignAttr.ValidTimeTo, dt.ToString("yyyy-MM-dd"), "�ύ��ҵ����", true, false);

                map.AddTBDecimal(WorkFixDesignAttr.CentOfChoseOne, 1, "��ѡ��ÿ���", true, false);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfChoseM, 1, "��ѡ��ÿ���", true, false);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfFillBlank, 1, "�����ÿ�շ�", true, false);

                map.AddTBDecimal(WorkFixDesignAttr.CentOfJudgeTheme, 1, "�ж���ÿ���", true, false);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfEssayQuestion, 1, "�ʴ����", true, true);

                map.AddTBDecimal(WorkFixDesignAttr.CentOfRC, 20, "�Ķ����", true, true);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfSum, 0, "�ܷ�", true, true);

                map.AttrsOfOneVSM.Add(new WorkVSChoseOnes(), new ChoseOnes(), WorkVSChoseOneAttr.FK_Work, WorkVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name, ChoseOneAttr.No, "��ѡ��");
                map.AttrsOfOneVSM.Add(new WorkVSChoseMs(), new ChoseMs(), WorkVSChoseMAttr.FK_Work, WorkVSChoseMAttr.FK_Chose, ChoseMAttr.Name, ChoseMAttr.No, "��ѡ��");
                map.AttrsOfOneVSM.Add(new WorkVSFillBlanks(), new FillBlanks(), WorkVSFillBlankAttr.FK_Work, WorkVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name, FillBlankAttr.No, "�����");
                map.AttrsOfOneVSM.Add(new WorkVSJudgeThemes(), new JudgeThemes(), WorkVSJudgeThemeAttr.FK_Work, WorkVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name, JudgeThemeAttr.No, "�ж���");
                map.AttrsOfOneVSM.Add(new WorkVSEssayQuestions(), new EssayQuestions(), WorkVSEssayQuestionAttr.FK_Work, WorkVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name, EssayQuestionAttr.No, "�ʴ���");
                map.AttrsOfOneVSM.Add(new WorkVSRCs(), new RCs(), WorkVSRCAttr.FK_Work, WorkVSRCAttr.FK_RC, RCAttr.Name, RCAttr.No, "�Ķ���");

                map.AttrsOfOneVSM.Add(new WorkVSEmps(), new Emps(), WorkVSEmpAttr.FK_Work, WorkVSEmpAttr.FK_Emp, RCAttr.Name, RCAttr.No, "Ҫ���õ�ѧ��");
                map.AddDtl(new WorkVSEssayQuestions(), WorkVSEssayQuestionAttr.FK_Work);
                map.AddDtl(new WorkVSRCDtls(), WorkVSRCAttr.FK_Work);

                this._enMap = map;
                return this._enMap;
            }
		}
		protected override void afterDelete()
		{
			DBAccess.RunSQL("DELETE GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSChoseM WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSJudgeTheme WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSFillBlank WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSEssayQuestion WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSRC WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSRCDtl WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkFix WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSEmp WHERE FK_Work='"+this.No+"'");
			base.afterDelete ();
		}

		protected override bool beforeUpdateInsertAction()
		{

			// �ж��Ƿ��п������⡣
			//			string sql="SELECT COUNT(*) FROM GTS_WorkFix WHERE  FK_Work='"+this.No+"'";
			//			int i=DBAccess.RunSQLReturnValInt(sql);
			//			if (i>=1)
			//				throw new Exception("��ҵ["+this.Name+"], ���ܱ�������ƣ���Ϊ���Ѿ��С�"+i+"����ѧ����ʼ���⡣");
			//
			//			if (this.IsPastTime)
			//				throw new Exception("��ҵ["+this.Name+"], ���ܱ�������ƣ���Ϊ���Ѿ�����ʱ�䡣");

			string sql="";
			// ���÷�����
			this.CentOfEssayQuestion = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_WorkVSEssayQuestion where FK_Work='"+this.No+"'") ;
			this.CentOfRC = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_WorkVSRCDtl where FK_Work='"+this.No+"'") ;
			this.CentOfSum=this.SumCentOfFillBlank+this.CentOfEssayQuestion+this.SumCentOfChoseOne+this.SumCentOfChoseM+this.SumCentOfJudgeTheme+this.CentOfRC;

			// ��ʼ������ , ��ÿ��������ʼ��������ҵ��
			sql="DELETE GTS_WorkFix WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_WorkVSEmp WHERE FK_Work='"+this.No+"') AND FK_Work='"+this.No+"'";
			DBAccess.RunSQL(sql);

			sql="SELECT FK_Emp FROM GTS_WorkVSEmp WHERE FK_Work='"+this.No+"' AND FK_Emp";
			sql+=" NOT IN (SELECT FK_Emp FROM GTS_WorkFix WHERE FK_Work='"+this.No+"')";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);
			foreach(DataRow dr in dt.Rows)
			{
				WorkFix pe = new WorkFix();
				pe.No=dr[0].ToString()+"@"+this.No;
				pe.FK_Emp=dr[0].ToString();
				pe.FK_Work=this.No;
				if ( pe.IsExit(WorkFixAttr.FK_Emp, pe.FK_Emp, WorkFixAttr.FK_Work, this.No))
					continue;
				else
					pe.Insert();
			}
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�
		/// </summary> 
		public WorkFixDesign()
		{
		}
		/// <summary>
		/// �Ծ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public WorkFixDesign(string _No ):base(_No)
		{

		}
		#endregion 

		#region �߼�����
		#endregion
	}
	/// <summary>
	///  �̶���ҵ
	/// </summary>
	public class WorkFixDesigns :EntitiesNoName
	{
		/// <summary>
		/// �̶���ҵs
		/// </summary>
		public WorkFixDesigns(){}

		/// <summary>
		/// �̶���ҵs
		/// </summary>
		/// <param name="fk_emp">����Ա</param>
		public WorkFixDesigns(string fk_emp)
		{
			QueryObject qo= new QueryObject(this);
			qo.AddWhereInSQL(WorkFixDesignAttr.No, "SELECT FK_Work FROM GTS_WorkVSEmp WHERE FK_Emp='"+Web.WebUser.No+"'" );
			qo.DoQuery();
		}

		/// <summary>
		/// �̶���ҵ
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkFixDesign();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrieveWorkFixDesign(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(WorkFixDesignAttr.No,  "SELECT FK_Work FROM GTS_WorkVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}

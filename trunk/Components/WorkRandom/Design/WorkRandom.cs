using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Web;


namespace BP.GTS
{
	/// <summary>
	/// ��ҵ��attr
	/// </summary>
    public class WorkRandomAttr : EntityOIDAttr
    {
        public const string No = "No";
        /// <summary>
        /// ��ҵ
        /// </summary>
        public const string FK_WorkRD = "FK_WorkRD";
        /// <summary>
        /// ��
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// dept
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// from datatime
        /// </summary>
        public const string FromDateTime = "FromDateTime";
        /// <summary>
        /// to datetime
        /// </summary>
        public const string ToDateTime = "ToDateTime";
        /// <summary>
        /// ����ʱ�����
        /// </summary>
        public const string MM = "MM";
        /// <summary>
        /// ����ѡ����
        /// </summary>
        public const string CentOfChoseOne = "CentOfChoseOne";
        /// <summary>
        /// ����ѡ����
        /// </summary>
        public const string CentOfChoseM = "CentOfChoseM";
        /// <summary>
        /// �����
        /// </summary>
        public const string CentOfFillBlank = "CentOfFillBlank";
        /// <summary>
        /// �ж���
        /// </summary>
        public const string CentOfJudgeTheme = "CentOfJudgeTheme";
        /// <summary>
        /// �ʴ���
        /// </summary>
        public const string CentOfEssayQuestion = "CentOfEssayQuestion";
        /// <summary>
        /// CentOfRC
        /// </summary>
        public const string CentOfRC = "CentOfRC";
        /// <summary>
        /// �ϼ�
        /// </summary>
        public const string CentOfSum = "CentOfSum";
        /// <summary>
        /// ��׼��
        /// </summary>
        public const string RightRate = "RightRate";
        /// <summary>
        /// FK_Level
        /// </summary>
        public const string FK_Level = "FK_Level";
    }
	/// <summary>
	/// ��ҵ��
	/// </summary>
	public class WorkRandom :EntityNo
	{
		 
		/// <summary>
		/// �Զ��ľ�
		/// </summary>
		public void AutoReadWork()
		{
			WorkRD pr = this.HisWorkRD;
			// ѡ����.
			WorkOfChoses  pes = new WorkOfChoses(this.No,this.FK_Emp);
			ChoseOnes chs =this.HisChoseOnes;
			int countOne=0;
			int countM=0;
			foreach(WorkOfChose pe in pes)
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

			this.CentOfChoseOne= countOne*pr.CentOfPerChoseOne; // ����ѡ����÷֡�
			this.CentOfChoseM= countM*pr.CentOfPerChoseM; // duo��ѡ����÷֡�

			// �ж���
			WorkOfJudgeThemes  pej = new WorkOfJudgeThemes(this.No,this.FK_Emp);
			int count=0;
			foreach(WorkOfJudgeTheme pe in pej)
			{
				
				JudgeTheme jt =new JudgeTheme(pe.FK_JudgeTheme);
				if (pe.Val==jt.IsOkOfInt)
				{
					count++;
				}
			}
			this.CentOfJudgeTheme= count*pr.CentOfPerJudgeTheme; // duo��ѡ����÷֡�
			// �����
			WorkOfFillBlanks  peb = new WorkOfFillBlanks(this.No,this.FK_Emp);
			count=0;
			foreach(WorkOfFillBlank pe in peb)
			{
				FillBlank fb= new FillBlank(pe.FK_FillBlank);
				if (pe.Val.Trim().Replace(" ","")==fb.HisKeys[pe.IDX].Trim().Replace(" ","") )
				{
					count++;
				}
			}
			this.CentOfFillBlank= count*pr.CentOfPerFillBlank; // blank��ѡ����÷֡�
			WorkOfEssayQuestions  pee = new WorkOfEssayQuestions(this.No, this.FK_Emp);
			this.DoResetLevel( pr.CentOfSum );
		}
		/// <summary>
		/// ��׼��
		/// </summary>
		public int RightRate
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.RightRate);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.RightRate,value);
			}
		}
		public void DoResetLevel(int centOfSum)
		{
			// ��ʼ�����׼�֡�
			this.RightRate=	this.CentOfSum/centOfSum *100;
			if (this.RightRate > 90)
			{
				this.FK_Level="4";
				this.Update();
				return;
			}
			if (this.RightRate >80)
			{
				this.FK_Level="3";
				this.Update();
				return;
			}

			if (this.RightRate >60)
			{
				this.FK_Level="2";
				this.Update();
				return;
			}
			this.FK_Level="1";
			this.Update();
		}

		/// <summary>
		/// ����漴��ҵ��
		/// </summary>
		public void ThemeOfClear()
		{
			// ɾ�����⡣
			DBAccess.RunSQL("DELETE GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSChoseM WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSJudgeTheme WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSFillBlank WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSEssayQuestion WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSRC WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSRCDtl WHERE FK_Work='"+this.No+"'");

			//DBAccess.RunSQL("DELETE GTS_WorkRandom WHERE FK_Work='"+this.No+"'");
			//DBAccess.RunSQL("DELETE GTS_WorkRandomVSEmp WHERE FK_Work='"+this.No+"'");
		}
		/// <summary>
		/// �Ƿ�������ҵ��
		/// </summary>
		public bool IsInit
		{
			get
			{
				int i=0;
				i+=DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM GTS_WorkVSChoseM WHERE FK_Work='"+this.No+"'");
				i+=DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'");

				if (i==0)
					return false;
				else
					return true;

			}
		}
		/// <summary>
		/// ����漴��ҵ��
		/// </summary>
		public void ThemeOfInit()
		{
			this.ThemeOfClear();

			WorkRD prd = this.HisWorkRD;
			//Emp emp = new Emp(WebUser.No);
			//Random rd = new Random( emp.Seed );
			#region ����ChonseOne ѡ����Ŀ
			ChoseOnes chs = new ChoseOnes();
			chs.RetrieveAllOrderByRandom(prd.NumOfChoseOne);
			foreach(ChoseOne ch in chs)
			{
				WorkVSChoseOne one = new WorkVSChoseOne();
				one.Cent=prd.CentOfPerChoseOne;
				one.FK_Chose=ch.No;
				one.FK_Work=this.No;
				one.Insert();
			}
			#endregion

			#region ���� ChonseM ѡ����Ŀ
			ChoseMs chMs = new ChoseMs();
			chMs.RetrieveAllOrderByRandom(prd.NumOfChoseM);
			foreach(ChoseM ch in chMs)
			{
				WorkVSChoseM theme = new WorkVSChoseM();
				theme.Cent=prd.CentOfPerChoseM;
				theme.FK_Chose=ch.No;
				theme.FK_Work=this.No;
				theme.Insert();
			}
			#endregion

			#region ���� fillbla ѡ����Ŀ
			FillBlanks fbs = new FillBlanks();
			fbs.RetrieveAllOrderByRandom(prd.NumOfFillBlank);
			foreach(FillBlank ch in fbs)
			{
				WorkVSFillBlank theme = new WorkVSFillBlank();
				theme.Cent=prd.CentOfPerFillBlank;
				theme.FK_FillBlank=ch.No;
				theme.FK_Work=this.No;
				theme.Insert();
			}
			#endregion

			#region ���� JudgeThemes  ��Ŀ
			JudgeThemes jts = new JudgeThemes();
			jts.RetrieveAllOrderByRandom(prd.NumOfJudgeTheme);
			foreach(JudgeTheme ch in jts)
			{
				WorkVSJudgeTheme theme = new WorkVSJudgeTheme();
				theme.Cent=prd.CentOfPerJudgeTheme;
				theme.FK_JudgeTheme=ch.No;
				theme.FK_Work=this.No;
				theme.Insert();
			}
			#endregion


			#region ���� EssayQuestions ��Ŀ
			EssayQuestions eqs = new EssayQuestions();
			eqs.RetrieveAllOrderByRandom(prd.NumOfEssayQuestion);
			foreach(EssayQuestion ch in eqs)
			{
				WorkVSEssayQuestion theme = new WorkVSEssayQuestion();
				theme.Cent=prd.CentOfPerEssayQuestion;
				theme.FK_EssayQuestion=ch.No;
				theme.FK_Work=this.No;
				theme.Insert();
			}
			#endregion

			#region ���� rcs ��Ŀ
			RCs rcs = new RCs();
			rcs.RetrieveAllOrderByRandom( prd.NumOfRC);
			foreach(RC ch in rcs)
			{
				WorkVSRC theme = new WorkVSRC();
				theme.FK_RC=ch.No;
				theme.FK_Work=this.No;
				theme.Insert();
			}
 
			RCDtls rcdtl = new RCDtls();
			int numES = 0;
			foreach(RC myrc in rcs)
			{
				DBAccess.RunSQL(" DELETE GTS_WorkVSRCDtl  WHERE FK_Work='"+this.No+"' and FK_RC='"+myrc.No+"' ");
				RCDtls dtls = myrc.HisRCDtls;
				numES+=dtls.Count;
				rcdtl.AddEntities(dtls);
			}
				
			decimal d=decimal.Parse( prd.CentOfPerRC.ToString() )  / decimal.Parse( numES.ToString() ) ;
			int perCent = Convert.ToInt32(  d  ) ;
			// ���� С�ʴ��⡣
			foreach(RCDtl dtl in rcdtl)
			{
				WorkVSRCDtl pdtl = new WorkVSRCDtl();
				pdtl.Cent=perCent; // ���÷�����
				pdtl.FK_Work=this.No;
				pdtl.FK_RC=dtl.FK_RC;
				pdtl.FK_RCDtl=dtl.OID;
				pdtl.Insert();
			}
			#endregion
		}

		#region ����
		/// <summary>
		/// �ɼ��ȼ�
		/// </summary>
		public string FK_Level
		{
			get
			{
				return this.GetValStringByKey(WorkRandomAttr.FK_Level);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.FK_Level,value);
			}
		}
		public string FK_LevelText
		{
			get
			{
				return this.GetValRefTextByKey(WorkRandomAttr.FK_Level);
			}
		}
	 
		/// <summary>
		/// FK_Dept
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey( WorkRandomAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.FK_Dept,value);
			}
		}
		 
		#endregion

		/// <summary>
		/// �Զ��ľ�:
		/// </summary>
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_WorkVSChoseOne where FK_Work='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_WorkVSChoseM where FK_Work='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_WorkVSJudgeTheme where FK_Work='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_WorkVSEssayQuestion where FK_Work='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_WorkVSRC where FK_Work='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_WorkVSFillBlank where FK_Work='"+this.No+"'");
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
				return this.GetValStringByKey(WorkRandomAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// FK_EmpText
		/// </summary>
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(WorkRandomAttr.FK_Emp);
			}
		}
		/// <summary>
		/// FK_WorkRD
		/// </summary>
		public string FK_WorkRD
		{
			get
			{
				return this.GetValStringByKey(WorkRandomAttr.FK_WorkRD);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.FK_WorkRD,value);
			}
		}
		/// <summary>
		/// HisWorkRD
		/// </summary>
		public WorkRD HisWorkRD
		{
			get
			{
				return new WorkRD(this.FK_WorkRD);
			}
		}
		public string FK_WorkText
		{
			get
			{
				return this.GetValRefTextByKey(WorkRandomAttr.FK_WorkRD);
			}
		}
		/// <summary>
		/// ��ʱ��
		/// </summary>
		public string FromDateTime
		{
			get
			{
				return this.GetValStringByKey(WorkRandomAttr.FromDateTime);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.FromDateTime,value);
			}
		}
		/// <summary>
		/// ��ʱ��
		/// </summary>
		public string ToDateTime
		{
			get
			{
				return this.GetValStringByKey(WorkRandomAttr.ToDateTime);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.ToDateTime,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.CentOfEssayQuestion,value);
			}
		}
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(WorkRandomAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(WorkRandomAttr.CentOfSum,value);
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
				uc.Readonly();
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
				
				Map map = new Map("GTS_WorkRandom");
				map.EnDesc="��ҵ";
				map.CodeStruct="7";
				map.IsAllowRepeatNo=false;
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRandomAttr.No,null,"ϵͳID",false,true,0,50,20);
				map.AddDDLEntities(WorkRandomAttr.FK_WorkRD,null,"�����ҵ",new WorkRDs(),false);

				map.AddDDLEntities(WorkRandomAttr.FK_Emp,Web.WebUser.No,"ѧ��",new Emps(),false);
                map.AddDDLEntities(WorkRandomAttr.FK_Dept, null, "����", new BP.Port.Depts(), false);

				map.AddTBInt(WorkRandomAttr.CentOfSum,0,"�ܷ�",true,true);
				map.AddTBInt(WorkRandomAttr.RightRate,0,"��׼��",true,true);

				map.AddTBInt(WorkRandomAttr.CentOfChoseOne,0,"��ѡ��",true,true);
				map.AddTBInt(WorkRandomAttr.CentOfChoseM,0,"��ѡ��",true,true);

				map.AddTBInt(WorkRandomAttr.CentOfFillBlank,0,"�����",true,true);
				map.AddTBInt(WorkRandomAttr.CentOfJudgeTheme,0,"�ж���",true,true);

				map.AddTBInt(WorkRandomAttr.CentOfEssayQuestion,0,"�ʴ���",true,true);
				map.AddTBInt(WorkRandomAttr.CentOfRC,0,"�Ķ������",true,true);

				map.AddDDLEntities(WorkRandomAttr.FK_Level,"1","�ɼ��ȼ�",new Levels(),false);

             //   map.AddTBInt(WorkFixAttr.FK_Level, 0, "�ɼ��ȼ�", false, false);
 
				map.AddSearchAttr(WorkRandomAttr.FK_WorkRD);
				//map.AddSearchAttr(WorkRandomAttr.FK_Dept);
			//	map.AddSearchAttr(WorkRandomAttr.FK_Level);


				// ������ҵ 
				RefMethod func1 = new RefMethod();
				func1.Title="������ҵ";
				//func1.Warning="ȷ��ִ����";
				func1.ClassMethodName=this.ToString()+".DoCheckWork()";
				func1.Icon="/Images/Btn/PrintWorkRpt.gif";
				func1.ToolTip="������ҵ��";
				func1.Width=0;
				func1.Height=0;
				func1.Target=null;
				map.AddRefMethod(func1);

				// ������ҵ 
				RefMethod f2 = new RefMethod();
				f2.Title="����������ҵ";
				//func1.Warning="ȷ��ִ����";
				f2.ClassMethodName=this.ToString()+".DoBatchCheckWork()";
				f2.Icon="/Images/Btn/PrintWorkRpt.gif";
				f2.ToolTip="������ҵ��";
				f2.Width=0;
				f2.Height=0;
				f2.Target=null;
				map.AddRefMethod(f2);

				this._enMap=map;
				return this._enMap;
			}
		}
		/// <summary>
		/// ִ��������ҵ
		/// </summary>
		/// <returns></returns>
		public string DoCheckWork()
		{
			if (this.HisWorkRD.IsValid)
				return "����������ҵ��ʱ��. ������ҵ��ʱ��Ϊ:"+this.HisWorkRD.ValidTimeTo ;

			if (this.IsInit==false)
				return "ѧ��["+this.FK_EmpText+"]û������ҵ��������ִ�����ġ�";

			PubClass.WinOpen("../App/Paper/ReadEssayQuestionWork.aspx?Flag=PaperExam&No="+this.No.ToString()+"&FK_Emp="+this.FK_Emp+"&FK_Work="+this.FK_WorkRD+"&IsRandom=1");
			return null;
		}
		public string DoBatchCheckWork()
		{
			if (this.HisWorkRD.IsValid)
				return "����������ҵ��ʱ��. ������ҵ��ʱ��Ϊ:"+this.HisWorkRD.ValidTimeTo ;

			string msg="";
			WorkRandoms wrd = new WorkRandoms(this.FK_WorkRD);
			foreach(WorkRandom wr in wrd)
			{
				if (wr.IsInit==false)
					continue;

				try
				{
					wr.AutoReadWork();
					msg+="@�ɹ�����["+wr.FK_EmpText+"]����ҵ���ɼ���"+wr.CentOfSum+"����ȷ�ʣ�"+wr.RightRate.ToString("0.00")+"��<hr>";
				}
				catch(Exception ex)
				{
					msg+="@����["+wr.FK_EmpText+"]��ҵ�ڼ���ִ���"+ex.Message+"<hr>";
				}
			}


			return msg;
		}
		/// <summary>
		/// beforeUpdateInsertAction
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdateInsertAction()
		{
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction();
		}
		protected override bool beforeInsert()
		{
			this.No=this.FK_Emp+"@"+this.FK_WorkRD;
			Emp emp = new Emp(this.FK_Emp);
			this.FK_Dept = emp.FK_Dept;
			return base.beforeInsert ();
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// ��ҵ��
		/// </summary> 
		public WorkRandom()
		{
		}
		public WorkRandom(string no):base(no)
		{
			if (this.HisChoseOnes.Count==0)
			{
				this.ThemeOfInit();
			}
		}

		public WorkRandom(string work, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_WorkRD=work;
            if (this.Retrieve("FK_Emp", fk_emp, "FK_Work", work) == 0)
            {
                this.Insert();
                //throw new Exception("����û�����ɿ���[��" + fk_emp + "��" + work + "]������Ϣ��");
            }
			

		}
		#endregion 

		 
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
	///  ��ҵ��
	/// </summary>
	public class WorkRandoms :EntitiesOID
	{
		 
		/// <summary>
		/// WorkRandoms
		/// </summary>
		public WorkRandoms(){}
		public WorkRandoms(string fk_paper)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkRandomAttr.FK_WorkRD, fk_paper);
			qo.DoQuery();
		}
		public int Search(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkRandomAttr.FK_Emp, fk_emp);
			return qo.DoQuery();
		}



		 
		/// <summary>
		/// WorkRandom
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandom();
			}
		}
	}
}

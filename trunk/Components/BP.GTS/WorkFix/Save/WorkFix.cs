using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ��attr
	/// </summary>
	public class WorkFixAttr:EntityNoAttr
	{
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const string FK_Work="FK_Work";
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
        public const string FK_Dept = "FK_Dept";
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
		public const string RightRate="RightRate";
	}
	/// <summary>
	/// �Ծ��
	/// </summary>
	public class WorkFix :EntityNo
	{
		#region ����
		public string GenerWorkResult()
		{
			string strs="<TABLE border=1 align=center >";
			strs+="<CAPTION>"+this.FK_EmpText+"</CAPTION>";
			strs+="<TR>";
			strs+="<TD >����ѡ����</TD>";
			strs+="<TD>"+this.CentOfChoseOne+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>����ѡ����</TD>";
			strs+="<TD>"+this.CentOfChoseM+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>�����</TD>";
			strs+="<TD>"+this.CentOfFillBlank+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>�ж���</TD>";
			strs+="<TD>"+this.CentOfJudgeTheme+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>�����</TD>";
			strs+="<TD>"+this.CentOfEssayQuestion+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>�Ķ������</TD>";
			strs+="<TD>"+this.CentOfRC+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>�ܷ�</TD>";
			strs+="<TD>"+this.CentOfSum+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>��ȷ��%</TD>";
			strs+="<TD>"+this.RightRate.ToString("0.00")+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>�ȼ�</TD>";
			strs+="<TD>"+this.FK_LevelText+"</TD>";
			strs+="</TR>";

			strs+="</Table>";
			return strs ;
		}
		/// <summary>
		/// ����
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey( WorkFixAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// Work.
		/// </summary>
		public WorkFixDesign HisWorkFixDesign
		{
			get
			{
				return new WorkFixDesign(this.FK_Work);
			}
		}
		#endregion

		/// <summary>
		/// �Զ�������ҵ:
		/// </summary>
		public void AutoReadWork()
		{
			WorkFixDesign pp = new WorkFixDesign(this.FK_Work);
			// ѡ����.
			WorkOfChoses  pes = new WorkOfChoses(this.FK_Work,this.FK_Emp);
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

				pe.Answer=cts.Val;
				pe.Update();
				
			}

			this.CentOfChoseOne= countOne*pp.CentOfChoseOne; // ����ѡ����÷�
			this.CentOfChoseM= countM*pp.CentOfChoseM; // duo��ѡ����÷�

			// �ж���
			WorkOfJudgeThemes  pej = new WorkOfJudgeThemes(this.FK_Work,this.FK_Emp);
			int count=0;
			foreach(WorkOfJudgeTheme pe in pej)
			{
				
				JudgeTheme jt =new JudgeTheme(pe.FK_JudgeTheme);
				if (pe.Val==jt.IsOkOfInt)
					count++;

				pe.Answer= jt.IsOkOfInt;
				pe.Update();
			}
			this.CentOfJudgeTheme= count*pp.CentOfJudgeTheme; // duo��ѡ����÷֡�


			// �����
			WorkOfFillBlanks  peb = new WorkOfFillBlanks(this.FK_Work, this.FK_Emp);
			count=0;
			foreach(WorkOfFillBlank pe in peb)
			{
				FillBlank fb= new FillBlank(pe.FK_FillBlank);
				if (pe.Val.Trim()==fb.HisKeys[pe.IDX].Trim() )
					count++;

				pe.Answer= fb.HisKeys[pe.IDX].Trim();
				pe.Update();
			}
			this.CentOfFillBlank= count*pp.CentOfFillBlank; // blank��ѡ����÷֡�

			WorkOfEssayQuestions  pee = new WorkOfEssayQuestions(this.FK_Work, this.FK_Emp);
			//			if (pee.Count==0)
			//			{
			//				/*���û�м����*/
			//				this.ExamState=WorkFixState.ReadOver;
			//			}
			//			else
			//			{
			//				if (this.ExamState==WorkFixState.Examing || this.ExamState==WorkFixState.Init )
			//					this.ExamState=WorkFixState.Reading;
			//			}

			this.DoResetLevel();
		}
		/// <summary>
		/// ִ���������ü���
		/// </summary>
		public void DoResetLevel()
		{
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			
			WorkFixDesign wfl= new WorkFixDesign(this.FK_Work);

			// ��ʼ�����׼�֡�
			decimal sum1=decimal.Parse( this.CentOfSum.ToString()) ;
			decimal sum2=decimal.Parse( wfl.CentOfSum.ToString()) ;
			this.RightRate= sum1/sum2 *100;
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

		#region ��������
		/// <summary>
		/// ���ĵ���ѡ����
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				ChoseOnes chs = new ChoseOnes();
				QueryObject qo = new QueryObject(chs);
				//qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_WorkFixVSChoseOne where fk_WorkFix='"+this.No+"'");
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
				//qo.AddWhereInSQL( ChoseOneAttr.FK_Chose, "select fk_chose from GTS_WorkFixVSChoseM where fk_WorkFix='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region ����
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// FK_EmpText
		/// </summary>
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(WorkFixAttr.FK_Emp);
			}
		}
		/// <summary>
		/// FK_Work
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FK_Work);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Work,value);
			}
		}
		/// <summary>
		/// �ɼ��ȼ�
		/// </summary>
		public string FK_Level
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FK_Level);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Level,value);
			}
		}
		public string FK_LevelText
		{
			get
			{
				return this.GetValRefTextByKey(WorkFixAttr.FK_Level);
			}
		}
		public string FK_WorkText
		{
			get
			{
				return this.GetValRefTextByKey(WorkFixAttr.FK_Work);
			}
		}
		/// <summary>
		/// ��ʱ��
		/// </summary>
		public string FromDateTime
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FromDateTime);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FromDateTime,value);
			}
		}
		/// <summary>
		/// ��ʱ��
		/// </summary>
		public string ToDateTime_
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.ToDateTime);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.ToDateTime,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public decimal CentOfEssayQuestion
		{
			get
			{
				return this.GetValDecimalByKey(WorkFixAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfEssayQuestion,value);
			}
		}
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public decimal CentOfSum
		{
			get
			{
				return this.GetValDecimalByKey(WorkFixAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ��׼��
		/// </summary>
		public decimal RightRate
		{
			get
			{
				return this.GetValDecimalByKey(WorkFixAttr.RightRate);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.RightRate,value);
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
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_WorkFix");
				map.EnDesc="��ҵ";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkFixAttr.No,null,"���",true,false,1,30,20);
				map.AddDDLEntities(WorkFixAttr.FK_Work,null,"��ҵ",new WorkFixDesigns(),false);
				map.AddDDLEntities(WorkFixAttr.FK_Emp,Web.WebUser.No,"ѧ��",new Emps(),false);
                map.AddDDLEntities(WorkFixAttr.FK_Dept, null, "����", new BP.Port.Depts(), false);

				map.AddTBInt(WorkFixAttr.CentOfChoseOne,0,"��ѡ��",true,true);
				map.AddTBInt(WorkFixAttr.CentOfChoseM,0,"��ѡ��",true,true);
				map.AddTBInt(WorkFixAttr.CentOfFillBlank,0,"�����",true,true);
				map.AddTBInt(WorkFixAttr.CentOfJudgeTheme,0,"�ж���",true,true);
				map.AddTBInt(WorkFixAttr.CentOfEssayQuestion,0,"�ʴ���",true,true);
				map.AddTBInt(WorkFixAttr.CentOfRC,0,"�Ķ������",true,true);
				map.AddTBInt(WorkFixAttr.CentOfSum,0,"�ܷ�",true,true);
				map.AddTBDecimal(WorkFixAttr.RightRate,0,"��ȷ��%",true,true);

               // map.AddTBInt(WorkFixAttr.FK_Level, 0, "�ɼ��ȼ�", false, false);

			 	map.AddDDLEntities(WorkFixAttr.FK_Level,"1","�ɼ��ȼ�",new Levels(),false);

				map.AddSearchAttr(WorkFixAttr.FK_Work);
				map.AddSearchAttr(WorkFixAttr.FK_Dept);
			//	map.AddSearchAttr(WorkFixAttr.FK_Level);
				/*
				map.AttrsOfOneVSM.Add( new WorkFixVSChoseOnes(), new ChoseOnes(),WorkFixVSChoseOneAttr.FK_WorkFix,WorkFixVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new WorkFixVSChoseMs(), new ChoseMs(),WorkFixVSChoseMAttr.FK_WorkFix,WorkFixVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new WorkFixVSFillBlanks(), new FillBlanks(),WorkFixVSFillBlankAttr.FK_WorkFix,WorkFixVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"�����");
				map.AttrsOfOneVSM.Add( new WorkFixVSJudgeThemes(), new JudgeThemes(),WorkFixVSJudgeThemeAttr.FK_WorkFix,WorkFixVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж���");
				map.AttrsOfOneVSM.Add( new WorkFixVSEssayQuestions(), new EssayQuestions(),WorkFixVSEssayQuestionAttr.FK_WorkFix,WorkFixVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"�ʴ���");
				map.AddDtl(new WorkFixVSEssayQuestions(),WorkFixVSEssayQuestionAttr.FK_WorkFix);
				*/
				//map.AttrsOfOneVSM.Add( new WorkFixVSChoseOnes(), new choseonhemes(),WorkFixVSJudgeThemeAttr.FK_WorkFix,WorkFixVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");
				//map.AttrsOfOneVSM.Add( new WorkFixVSChoseMs(), new JudgeThemes(),WorkFixVSJudgeThemeAttr.FK_WorkFix,WorkFixVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж������");

			
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
			PubClass.WinOpen("../App/Paper/ReadEssayQuestionWork.aspx?Flag=PaperExam&RefNo="+this.No.ToString()+"&IsFix=1&FK_Emp="+this.FK_Emp+"&FK_Work="+this.FK_Work);
			return null;
		}
		/// <summary>
		/// ����������ҵ
		/// </summary>
		/// <returns></returns>
		public string DoBatchCheckWork()
		{
			string msg="";
			WorkFixs wrd = new WorkFixs(this.FK_Work);
			foreach(WorkFix wr in wrd)
			{
				try
				{
					wr.AutoReadWork();
					msg+="@�ɹ�����["+wr.FK_EmpText+"]����ҵ���ɼ���"+wr.CentOfSum+"����ȷ�ʣ�"+wr.RightRate.ToString("0.00")+"��";
				}
				catch(Exception ex)
				{
					msg+="@����["+wr.FK_EmpText+"]��ҵ�ڼ���ִ���"+ex.Message;
				}
			}
			return msg;
		}
		protected override bool beforeInsert()
		{

			if (this.Search(this.FK_Work,this.FK_Emp)>=1)
				throw new Exception("�Ѿ������ѧ��"+this.FK_Emp+"���Ծ�.");

		 
			Emp emp = new Emp(this.FK_Emp);
			this.FK_Dept = emp.FK_Dept;

			return base.beforeInsert ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			 
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			//this.FK_Dept =
			//this.MM =DataType.GetSpanMinute(this.FromDateTime,this.ToDateTime);
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ��
		/// </summary> 
		public WorkFix()
		{
		}
		public WorkFix(string no):base(no){}
		public WorkFix(string Work, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Work=Work;
            if (this.Retrieve("FK_Emp", fk_emp, "FK_Work", Work) == 0)
            {
                this.Insert();
               // throw new Exception("����û�����ɿ���[��" + fk_emp + "��]������Ϣ��");
            }
		}
		#endregion 

		public int Search(string Work, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Work=Work;
			QueryObject qo = new QueryObject(this);			
			qo.AddWhere(WorkFixAttr.FK_Emp,fk_emp);
			qo.addAnd();
			qo.AddWhere(WorkFixAttr.FK_Work,Work);
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
	public class WorkFixs :EntitiesNo
	{
		/// <summary>
		/// 
		/// </summary>
		public void AutoReadWork()
		{
			foreach(WorkFix pe in this)
			{
				pe.AutoReadWork();
			}
		}
		/// <summary>
		/// WorkFixs
		/// </summary>
		public WorkFixs(){}
		public WorkFixs(string fk_Work)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkFixAttr.FK_Work, fk_Work);
			qo.DoQuery();
		}
		public int Search(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkFixAttr.FK_Emp, fk_emp);
			return qo.DoQuery();
		}
		/// <summary>
		/// WorkFix
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkFix();
			}
		}
	}
}

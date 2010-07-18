using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ�attr
	/// </summary>
	public class PaperRandomDesignAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ��Чʱ���
		/// </summary>
		public const string ValidTimeFrom="ValidTimeFrom";
		/// <summary>
		/// ��
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";
		/// <summary>
		/// �Ծ�״̬
		/// </summary>
		public const string PaperState="PaperState";
		/// <summary>
		/// ����ʱ�����
		/// </summary>
		public const string MM="MM";

		#region  ��Ŀ����
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string NumOfChoseOne="NumOfChoseOne";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string NumOfChoseM="NumOfChoseM";
		/// <summary>
		/// �����
		/// </summary>
		public const string NumOfFillBlank="NumOfFillBlank";
		/// <summary>
		/// �ж���
		/// </summary>
		public const string NumOfJudgeTheme="NumOfJudgeTheme";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string NumOfEssayQuestion="NumOfEssayQuestion";
		/// <summary>
		/// NumOfRC
		/// </summary>
		public const string NumOfRC="NumOfRC";
		#endregion

		#region cent
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfPerChoseOne="CentOfPerChoseOne";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string CentOfPerChoseM="CentOfPerChoseM";
		/// <summary>
		/// �����
		/// </summary>
		public const string CentOfPerFillBlank="CentOfPerFillBlank";
		/// <summary>
		/// �ж���
		/// </summary>
		public const string CentOfPerJudgeTheme="CentOfPerJudgeTheme";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string CentOfPerEssayQuestion="CentOfPerEssayQuestion";
		/// <summary>
		/// CentOfPerRC
		/// </summary>
		public const string CentOfPerRC="CentOfPerRC";
		/// <summary>
		/// �ϼ�
		/// </summary>
		public const string CentOfSum="CentOfSum";
		#endregion
	}
	/// <summary>
	/// �Ծ�
	/// </summary>
	public class PaperRandomDesign :EntityNoName
	{

		#region his attrs
		/// <summary>
		/// ���Լ���
		/// </summary>
		public PaperExams HisPaperRandomDesignExams
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseOne where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs
		  
				 

		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfPerChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerChoseOne);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int CentOfPerChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerChoseM);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int CentOfPerJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int CentOfPerEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int CentOfPerRC
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerRC);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int CentOfPerFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerFillBlank);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.MM,value);
			}
		}
		#endregion

		#region Numof 
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int NumOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public int NumOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfChoseM,value);
			}
		}
		/// <summary>
		/// �ж�
		/// </summary>
		public int NumOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public int NumOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public int NumOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfRC);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int NumOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfFillBlank,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// ÿ������ѡ����÷֡�
		/// </summary>
		public int PerCentOfPerChoseOne
		{
			get
			{
				return this.CentOfPerChoseOne/this.HisChoseOnes.Count;
			}
		}
		/// <summary>
		/// duo��ѡ����÷֡�
		/// </summary>
		public int PerCentOfPerChoseM
		{
			get
			{
				return this.CentOfPerChoseM/this.HisChoseMs.Count;
			}
		}
		/// <summary>
		/// �ж���ķ֡�
		/// </summary>
		public int PerCentOfPerJudgeTheme
		{
			get
			{
				return this.CentOfPerJudgeTheme/this.HisJudgeThemes.Count;
			}
		}
	 
		/// <summary>
		/// �����
		/// </summary>
		public int PerCentOfPerFillBlank
		{
			get
			{

				int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_Paper='"+this.No+"' )");
				return this.CentOfPerFillBlank/blankNum;
			}
		}
		public string ValidTimeFrom
		{
			get
			{
				return this.GetValStringByKey(PaperRandomDesignAttr.ValidTimeFrom);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.ValidTimeFrom,value);
			}
		}
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(PaperRandomDesignAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.ValidTimeTo,value);
			}
		}
		public bool IsValid
		{
			get
			{
				DateTime dtfrom=DataType.ParseSysDateTime2DateTime(this.ValidTimeFrom);
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;

				if ( dtfrom <= dt || dt>= dtto )
					return true;
				else
					return false;
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
				
				Map map = new Map("GTS_PaperRandomDesign");
				map.EnDesc="����Ծ����";
				map.CodeStruct="4";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperRandomDesignAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(PaperRandomDesignAttr.Name,"�½�����Ծ�1","����",true,false,0,50,20);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(1);

				map.AddTBDateTime(PaperRandomDesignAttr.ValidTimeFrom,dt.ToString("yyyy-MM-dd")+" 09:00","��Чʱ���",true,false);
				map.AddTBDateTime(PaperRandomDesignAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","��",true,false);


				map.AddTBInt(PaperRandomDesignAttr.NumOfChoseOne,30,"��ѡ�����",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerChoseOne,1,"ÿ��ѡ���",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfChoseM,10,"��ѡ�����",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerChoseM,2,"ÿ��ѡ���",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfFillBlank,10,"��������",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerFillBlank,1,"ÿ������",true,false);
				map.AddTBInt(PaperRandomDesignAttr.NumOfJudgeTheme,10,"�ж������",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerJudgeTheme,1,"ÿ�ж����",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfEssayQuestion,5,"�ʴ������",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerEssayQuestion,4,"ÿ�ʴ����",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfRC,1,"�Ķ������",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerRC,10,"�Ķ����ܷ�",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfSum,100,"�ܷ�",true,true);
				map.AddTBInt(PaperRandomDesignAttr.MM,90,"����ʱ��(����)",true,false);

				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new Emps(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"���Ե�ѧ��");
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			// �ж��Ǵ˿����Ѿ��ɿ�ʼ�Ŀ�����
			string sql="SELECT COUNT(*) FROM GTS_PaperExamRandom WHERE ExamState >0 and FK_Paper='"+this.No+"' " ; 
			int i=DBAccess.RunSQLReturnValInt(sql);
			if (i>=1)
				throw new Exception("�Ծ�["+this.Name+"], ���ܱ�������ƣ���Ϊ���Ѿ��С�"+i+"����������ʼ���⡣");

			if (this.CentOfPerChoseOne==0 && this.NumOfChoseOne > 0 )
				throw new Exception("ÿ ��ѡ�� ��������Ϊ0��");

			if (this.CentOfPerChoseM==0 && this.NumOfChoseM > 0 )
				throw new Exception("ÿ ��ѡ�� ��������Ϊ0��");

			if (this.CentOfPerFillBlank==0 && this.NumOfFillBlank > 0 )
				throw new Exception("ÿ ����� ��������Ϊ0��");

			if (this.CentOfPerJudgeTheme==0 && this.PerCentOfPerJudgeTheme > 0 )
				throw new Exception("ÿ �ж��� ��������Ϊ0��");

			//			if (this.CentOfPerEssayQuestion==0 && this.PerCentOfPerEssayQuestion > 0 )
			//				throw new Exception("ÿ �ʴ��� ��������Ϊ0��");

			if (this.CentOfPerRC==0 && this.NumOfRC > 0 )
				throw new Exception("ÿ�Ķ����������Ϊ0��");

			if (this.NumOfRC==0)
				this.CentOfPerRC=0;
			 
			if (this.NumOfEssayQuestion==0)
				this.CentOfPerEssayQuestion=0;


			int centsum=0;
			centsum+=this.CentOfPerChoseOne*this.NumOfChoseOne;
			centsum+=this.CentOfPerChoseM*this.NumOfChoseM;
			centsum+=this.CentOfPerFillBlank*this.NumOfFillBlank;
			centsum+=this.CentOfPerJudgeTheme*this.NumOfJudgeTheme;
			centsum+=this.CentOfPerEssayQuestion*this.NumOfEssayQuestion;
			centsum+=this.CentOfPerRC;

			sql="SELECT COUNT(*) FROM V_GTS_ChoseOne";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfChoseOne)
				throw new Exception("��ѡ�� ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM V_GTS_ChoseM";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfChoseM)
				throw new Exception("��ѡ�� ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM GTS_EssayQuestion";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfEssayQuestion)
				throw new Exception("����� ��Ŀ̫��");


			sql="SELECT COUNT(*) FROM GTS_FillBlank";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfFillBlank)
				throw new Exception("����� ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM GTS_RC";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfRC)
				throw new Exception("�Ķ���� "+NumOfRC+" ��Ŀ̫��");

			sql="SELECT COUNT(*) FROM GTS_JudgeTheme";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfJudgeTheme)
				throw new Exception("�ж��� "+NumOfJudgeTheme+"��Ŀ̫��");

			if (this.CentOfPerRC ==0 )
				this.NumOfRC=0;

			if (this.NumOfRC==0)
				this.CentOfPerRC=0;

			/* 1, ��ʼ������, �ڿ���ʱ�����ÿ�����Χ, ��ÿ��������ʼ�������Ծ��ʽ�����ǲ�����������Ŀ��
			 * �ȴ��û����뿼�Ժ��ٸ�����Ŀ��
			 * 2, ���ӿ�������
			 * */
			sql="DELETE GTS_PaperExam WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"') AND FK_Paper='"+this.No+"'";
			DBAccess.RunSQL(sql); // ɾ��û�С�

			sql="SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"' AND FK_Emp ";
			sql+="NOT IN (SELECT FK_Emp FROM GTS_PaperExam WHERE FK_Paper='"+this.No+"')";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);

			// ɾ������
			DBAccess.RunSQL("DELETE GTS_PaperExamRandom WHERE FK_Paper='"+this.No+"' ");
			foreach(DataRow dr in dt.Rows)
			{
				PaperExamRandom pe = new PaperExamRandom();
				pe.No=dr[0].ToString()+""+this.No;
				pe.FK_Emp=dr[0].ToString();
				pe.FK_Paper=this.No;
				pe.Insert(); // ���ӿ�������
			}
			// �ܷ�
			this.CentOfSum=this.CentOfPerChoseOne*this.NumOfChoseOne+this.CentOfPerChoseM*this.NumOfChoseM+this.CentOfPerEssayQuestion*this.NumOfEssayQuestion+this.CentOfPerFillBlank*this.NumOfFillBlank+this.CentOfPerJudgeTheme*this.NumOfJudgeTheme+this.CentOfPerRC;
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�
		/// </summary> 
		public PaperRandomDesign()
		{
		}
		/// <summary>
		/// �Ծ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public PaperRandomDesign(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		#endregion

	}
	/// <summary>
	///  �Ծ�
	/// </summary>
	public class PaperRandomDesigns :EntitiesNoName
	{
		/// <summary>
		/// PaperRandomDesigns
		/// </summary>
		public PaperRandomDesigns(){}
		/// <summary>
		/// PaperRandomDesign
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperRandomDesign();
			}
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrievePaperRandomDesign(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}

		 
	}
}

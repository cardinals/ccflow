using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// �̶��Ծ�
	/// </summary>
	public class PaperFixAttr:EntityNoNameAttr
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
		/// <summary>
		/// ValidTimeFrom
		/// </summary>
		public const string ValidTimeFrom="ValidTimeFrom";
		/// <summary>
		/// ValidTimeTo
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";
        public const string TTypes = "TTypes";
	}
	  
	/// <summary>
	/// �̶��Ծ�
	/// </summary>
	public class PaperFix :EntityNoName
	{
 
		#region attrs
		 
		/// <summary>
		/// ��ѡ
		/// </summary>
        public decimal CentOfChoseOne
		{
			get
			{
				return this.GetValDecimalByKey(PaperFixAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// ��ѡ
		/// </summary>
		public decimal CentOfChoseM
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfChoseM,value);
			}
		}
		#endregion 

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
        public ChoseOnes HisChoseOnes_SQL
        {
            get
            {
                ChoseOnes chs = new ChoseOnes();
                QueryObject qo = new QueryObject(chs);
                qo.AddWhereInSQL(ChoseOneAttr.No, "SELECT fk_chose from GTS_PaperVSChoseOne where FK_Paper='" + this.No + "'");
                qo.addOrderByRandom();
                qo.DoQuery();
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
				if (BP.DA.Cash.IsExits("ChoseOnes"+this.No,Depositary.Session) ==false )
				{
					ChoseOnes chs = new ChoseOnes();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "SELECT fk_chose from GTS_PaperVSChoseOne where FK_Paper='"+this.No+"'");
					qo.addOrderByRandom();
					qo.DoQuery();
					BP.DA.Cash.AddObj("ChoseOnes"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (ChoseOnes)BP.DA.Cash.GetObjFormSession("ChoseOnes"+this.No);
			}
		}
        public ChoseMs HisChoseMs_SQL
        {
            get
            {
                ChoseMs chs = new ChoseMs();
                QueryObject qo = new QueryObject(chs);
                qo.AddWhereInSQL(ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where FK_Paper='" + this.No + "'");
                qo.addOrderByRandom();
                qo.DoQuery();
                return chs;
            }
        }
        public JudgeThemes HisJudgeThemes_SQL
        {
            get
            {
                JudgeThemes chs = new JudgeThemes();
                QueryObject qo = new QueryObject(chs);
                qo.AddWhereInSQL(ChoseOneAttr.No, "select fk_JudgeTheme from GTS_PaperVSJudgeTheme where FK_Paper='" + this.No + "'");
                qo.addOrderByRandom();
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
				if (BP.DA.Cash.IsExits("HisChoseMs"+this.No,Depositary.Session) ==false )
				{
					ChoseMs chs = new ChoseMs();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"'");
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
		public string ValidTimeFrom
		{
			get
			{
				return this.GetValStringByKey(PaperFixAttr.ValidTimeFrom);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.ValidTimeFrom,value);
			}
		}
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(WorkRDAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.ValidTimeTo,value);
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
        public string TTypes
        {
            get
            {
                return this.GetValStringByKey(PaperFixAttr.TTypes);
            }
            set
            {
                this.SetValByKey(PaperFixAttr.TTypes, value);
            }
        }
		/// <summary>
		/// �ж�
		/// </summary>
		public decimal CentOfJudgeTheme
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// �ʴ�
		/// </summary>
		public decimal CentOfEssayQuestion
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// �Ķ�
		/// </summary>
		public decimal CentOfRC
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public decimal CentOfFillBlank
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// �ϼ�
		/// </summary>
		public decimal CentOfSum
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperFixAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// ÿ������ѡ����÷֡�
		/// </summary>
		public decimal SumCentOfChoseOne
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_PaperVSChoseOne WHERE FK_Paper='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
                return decimal.Parse(i.ToString()) * this.CentOfChoseOne;
			}
		}
		/// <summary>
		/// duo��ѡ����÷֡�
		/// </summary>
		public decimal SumCentOfChoseM
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_PaperVSChoseM WHERE FK_Paper='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
                return decimal.Parse(i.ToString()) * this.CentOfChoseM;
			}
		}
		/// <summary>
		/// �ж���ķ֡�
		/// </summary>
		public decimal SumCentOfJudgeTheme
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_PaperVSJudgeTheme WHERE FK_Paper='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return decimal.Parse(i.ToString()) *this.CentOfJudgeTheme;
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		public decimal SumCentOfFillBlank
		{
			get
			{
				int blankNum=DBAccess.RunSQLReturnValInt("SELECT ISNULL( SUM(BlankNum) , 0 )  FROM GTS_FillBlank WHERE No IN  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"' )");
				return this.CentOfFillBlank * decimal.Parse( blankNum.ToString() );
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
				
				Map map = new Map("GTS_PaperFix");
				map.EnDesc="�̶��Ծ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(PaperFixAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(PaperFixAttr.Name,"�½��̶��Ծ�1","����",true,false,0,50,20);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(1);
				map.AddTBDateTime(PaperFixAttr.ValidTimeFrom,dt.ToString("yyyy-MM-dd")+" 09:00","��Чʱ���",true,false);
				map.AddTBDateTime(PaperFixAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","��",true,false);

                map.AddTBDecimal(PaperFixAttr.MM, 90, "����ʱ��(����)", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfChoseOne, 1, "��ѡ��ÿ���", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfChoseM, 1, "��ѡ��ÿ���", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfFillBlank, 1, "�����ÿ�շ�", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfJudgeTheme, 1, "�ж���ÿ���", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfEssayQuestion, 1, "�ʴ����", true, true);
                map.AddTBDecimal(PaperFixAttr.CentOfRC, 20, "�Ķ����", true, true);
                map.AddTBDecimal(PaperFixAttr.CentOfSum, 0, "�ܷ�", true, true);


                map.AddTBString(PaperFixAttr.TTypes, null, "��Ŀ����s", false, false, 0, 50, 20);

				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"��ѡ��");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"�����");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"�ж���");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"�ʴ���");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"�Ķ���");

				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new Emps(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"���Ե�ѧ��");
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);

                RefMethod rm = new RefMethod();
                rm.Title = "�����Ծ�";
                rm.ClassMethodName = this.ToString() + ".DoGenerIt";
                map.AddRefMethod(rm);


				this._enMap=map;
				return this._enMap;
			}
		}
        public string DoGenerIt()
        {
            PubClass.WinOpen("../App/Paper/GenerPaper.aspx?No="+this.No);
            return null;

            string doc = this.Name;
            doc += "<BR><b>"+this.Name+" </b><hr>";

            ChoseOnes ones = this.HisChoseOnes;
            doc += "<BR><b>һ��</b>����ѡ���� ��" + ones.Count + "�� <br>";
            int i=0;
            foreach (ChoseOne on in ones)
            {
                i++;
                doc += i.ToString() + " ��" +on.Name;
                doc += "<br> &nbsp;&nbsp;&nbsp;&nbsp;";
                ChoseItems items = on.HisChoseItems;
                foreach (ChoseItem item in items)
                {
                    doc += item.Item + "��" + item.ItemDoc + "<br>";
                }
            }


            ChoseMs chms = this.HisChoseMs;
            doc += "<BR><b>����</b>��ѡ��   ��"+chms.Count+"��<br>";
             i = 0;
            foreach (ChoseM on in chms)
            {
                i++;
                doc += i.ToString() + " ��" + on.Name;
                doc += "<br> &nbsp;&nbsp;&nbsp;&nbsp;";
                ChoseItems items = on.HisChoseItems;
                foreach (ChoseItem item in items)
                {
                    doc += item.Item + "��" + item.ItemDoc + "<br>";
                }
            }

           

            JudgeThemes jts = this.HisJudgeThemes;
            doc += "<BR><b>����</b>�ж���   ��" + jts.Count + "��<br>";
              i = 0;
            foreach (JudgeTheme on in jts)
            {
                i++;
                doc += i.ToString() + " ��" + on.Name;
                doc += "<br> &nbsp;&nbsp;&nbsp;&nbsp;";
                doc += "<br> A ��ȷ        B ����";
            }

            return doc;
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
			DBAccess.RunSQL("DELETE GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"'");
			base.afterDelete ();
		}

        protected override bool beforeUpdateInsertAction()
        {

            // �ж��Ƿ���ѧ�����⡣
            string sql = "SELECT COUNT(*) FROM GTS_PaperExam WHERE FK_Paper='" + this.No + "' AND ExamState >0  ";
            int i = DBAccess.RunSQLReturnValInt(sql);
            if (i >= 1)
                throw new Exception("�Ծ�[" + this.Name + "], ���ܱ�������ƣ���Ϊ���Ѿ��С�" + i + "����ѧ����ʼ���⡣");


            // ���÷�����
            this.CentOfEssayQuestion = DBAccess.RunSQLReturnValDecimal("select isnull( sum(cent), 0) from GTS_PaperVSEssayQuestion where FK_Paper='" + this.No + "'", 0, 2);
            this.CentOfRC = DBAccess.RunSQLReturnValDecimal("select isnull( sum(cent), 0) from GTS_PaperVSRCDtl where FK_Paper='" + this.No + "'", 0, 2);

            this.CentOfSum = this.SumCentOfFillBlank + this.SumCentOfChoseOne + this.SumCentOfChoseM + this.SumCentOfJudgeTheme + this.CentOfEssayQuestion + this.CentOfRC;

            // ��ʼ��ѧ�� , �ڿ���ʱ������ѧ����Χ, ��ÿ��ѧ����ʼ�������Ծ�
            sql = "DELETE GTS_PaperExam WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='" + this.No + "') AND FK_Paper='" + this.No + "'";
            DBAccess.RunSQL(sql); // ɾ��û���漰��ѧ����

            sql = "SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='" + this.No + "' AND FK_Emp ";
            sql += "NOT IN (SELECT FK_Emp FROM GTS_PaperExam WHERE FK_Paper='" + this.No + "')";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                PaperExam pe = new PaperExam();
                pe.No = dr[0].ToString() + this.No;
                pe.FK_Emp = dr[0].ToString();
                pe.FK_Paper = this.No;
                pe.Insert();
            }

            string ttypes = "";
            if (this.SumCentOfChoseM > 0)
                ttypes += "@" + BP.GTS.ThemeType.ChoseM;

            if (this.SumCentOfChoseOne > 0)
                ttypes += "@" + BP.GTS.ThemeType.ChoseOne;

            if (this.CentOfEssayQuestion > 0)
                ttypes += "@" + BP.GTS.ThemeType.EssayQuestion;

            if (this.SumCentOfFillBlank > 0)
                ttypes += "@" + BP.GTS.ThemeType.FillBlank;

            if (this.SumCentOfJudgeTheme > 0)
                ttypes += "@" + BP.GTS.ThemeType.JudgeTheme;

            if (this.CentOfRC > 0)
                ttypes += "@" + BP.GTS.ThemeType.RC;

            this.TTypes = ttypes;
            return base.beforeUpdateInsertAction();
        }
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�
		/// </summary> 
		public PaperFix()
		{
		}
		/// <summary>
		/// �Ծ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public PaperFix(string _No ):base(_No)
		{

		}
		#endregion 

		#region �߼�����
		#endregion

	}
	/// <summary>
	///  �̶��Ծ�
	/// </summary>
	public class PaperFixs :EntitiesNoName
	{
		/// <summary>
		/// �̶��Ծ�s
		/// </summary>
		public PaperFixs(){}

		/// <summary>
		/// �̶��Ծ�s
		/// </summary>
		/// <param name="fk_emp">����Ա</param>
		public PaperFixs(string fk_emp)
		{
			QueryObject qo= new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No, "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+Web.WebUser.No+"'" );
			qo.DoQuery();
		}

		/// <summary>
		/// �̶��Ծ�
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperFix();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrievePaperFix(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}

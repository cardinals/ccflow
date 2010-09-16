using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ѡ���⿼��attr
	/// </summary>
	public class PaperExamOfJudgeThemeAttr:EntityOIDAttr
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
		/// ѡ����
		/// </summary>
		public const string FK_JudgeTheme="FK_JudgeTheme";
		/// <summary>
		/// ֵ
		/// </summary>
		public const string Val="Val";
        public const string IDX = "IDX";
	}
	/// <summary>
	/// ѡ���⿼��
	/// </summary>
	public class PaperExamOfJudgeTheme :Entity
	{
		#region ʵ�ֻ����ķ���
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(PaperExamOfJudgeThemeAttr.IDX);
            }
            set
            {
                this.SetValByKey(PaperExamOfJudgeThemeAttr.IDX, value);
            }
        }
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
				
				Map map = new Map("GTS_PaperExamOfJudgeTheme");
				map.EnDesc="�Ծ�ѡ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamOfJudgeThemeAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(PaperExamOfJudgeThemeAttr.FK_Paper,null,"����",true,true,0,50,20);
				map.AddTBStringPK(PaperExamOfJudgeThemeAttr.FK_JudgeTheme,null,"�ж���",true,true,0,50,20);
				map.AddTBInt(PaperExamOfJudgeThemeAttr.Val,2,"val",true,false);
                map.AddTBInt(PaperExamOfJudgeThemeAttr.IDX, 0, "IDX", true, false);


				this._enMap=map;
				return this._enMap;
			}
		}
        protected override bool beforeInsert()
        {
            this.IDX = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM GTS_PaperExamOfJudgeTheme WHERE FK_Paper='" + this.FK_Paper + "' AND FK_Emp='" + this.FK_Emp + "'") + 1;
            //this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
            return base.beforeInsert();
        }
		#endregion 

		#region ���췽��
		/// <summary>
		/// ѡ���⿼��
		/// </summary> 
		public PaperExamOfJudgeTheme()
		{
		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public PaperExamOfJudgeTheme(string paper,string empid,string fk_chose)
		{
			this.FK_JudgeTheme = fk_chose;
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
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfJudgeThemeAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfJudgeThemeAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfJudgeThemeAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfJudgeThemeAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_Chose
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperExamOfJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public int Val
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfJudgeThemeAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfJudgeThemeAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ѡ���⿼��
	/// </summary>
	public class PaperExamOfJudgeThemes :EntitiesOID
	{
		/// <summary>
		/// PaperExamOfJudgeThemes
		/// </summary>
		public PaperExamOfJudgeThemes(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamOfJudgeThemes(string fk_paper,string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(PaperExamOfChoseAttr.FK_Paper,fk_paper);
			qo.DoQuery();
		}
		/// <summary>
		/// PaperExamOfJudgeTheme
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfJudgeTheme();
			}
		}
	}
}

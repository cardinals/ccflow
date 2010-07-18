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
	public class PaperExamOfChoseAttr:EntityOIDAttr
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
		public const string FK_Chose="FK_Chose";
		/// <summary>
		/// ֵ
		/// </summary>
		public const string Val="Val";
        public const string IDX = "IDX";
        public const string IsChoseM = "IsChoseM";

	}
	/// <summary>
	/// ѡ���⿼��
	/// </summary>
	public class PaperExamOfChose :EntityMyPK
	{
		#region ʵ�ֻ����ķ���
        /// <summary>
        /// IDX
        /// </summary>
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(PaperExamOfChoseAttr.IDX);
            }
            set
            {
                this.SetValByKey(PaperExamOfChoseAttr.IDX, value);
            }
        }
        public bool IsChoseM
        {
            get
            {
                return this.GetValBooleanByKey(PaperExamOfChoseAttr.IsChoseM);
            }
            set
            {
                this.SetValByKey(PaperExamOfChoseAttr.IsChoseM, value);
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
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GTS_PaperExamOfChose");
                map.EnDesc = "�Ծ�ѡ����";
                map.CodeStruct = "5";
                map.EnType = EnType.Admin;

                map.AddMyPK();
                map.AddTBString(PaperExamOfChoseAttr.FK_Paper, null, "����", true, true, 0, 50, 20);
                map.AddTBString(PaperExamOfChoseAttr.FK_Chose, null, "ѡ����", true, true, 0, 50, 20);
                map.AddTBString(PaperExamOfChoseAttr.FK_Emp, Web.WebUser.No, "ѧ��", true, false, 0, 50, 20);
                map.AddTBString(PaperExamOfChoseAttr.Val, null, "��Ŀ", true, true, 0, 50, 20);
                map.AddTBInt(PaperExamOfChoseAttr.IDX, 0, "IDX", true, true);

                map.AddTBInt(PaperExamOfChoseAttr.IsChoseM, 0, "@0=��@1=��", true, true);

                this._enMap = map;
                return this._enMap;
            }
		}
        protected override bool beforeInsert()
        {
            this.IDX = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM GTS_PaperExamOfChose WHERE FK_Paper='" + this.FK_Paper + "' AND FK_Emp='" + this.FK_Emp + "' and IsChoseM=" + this.GetValIntByKey("IsChoseM")) + 1;
            //this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
            return base.beforeInsert();
        }
		#endregion 

		#region ���췽��
		/// <summary>
		/// ѡ���⿼��
		/// </summary> 
		public PaperExamOfChose()
		{

		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
        public PaperExamOfChose(string paper, string empid, string fk_chose, bool isChoseM)
        {
            this.FK_Chose = fk_chose;
            this.FK_Emp = empid;
            this.FK_Paper = paper;
            this.MyPK = paper + "_" + fk_chose + "_" + empid;

            this.IsChoseM = isChoseM;
            try
            {
                this.Retrieve();
            }
            catch
            {
               // this.IDX = DBAccess.RunSQL("SELECT COUNT(*) FROM GTS_PaperExamOfChose WHERE FK_Paper='" + this.FK_Paper + "' AND FK_Emp='" + this.FK_Emp + "'") + 1;
                this.Insert();
            }
        }
		#endregion 

		#region �߼�����
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfChoseAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfChoseAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfChoseAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfChoseAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_Chose
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfChoseAttr.FK_Chose);
			}
			set
			{
				this.SetValByKey(PaperExamOfChoseAttr.FK_Chose,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfChoseAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfChoseAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ѡ���⿼��
	/// </summary>
	public class PaperExamOfChoses :EntitiesMyPK
	{
		/// <summary>
		/// PaperExamOfChoses
		/// </summary>
		public PaperExamOfChoses(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
        public PaperExamOfChoses(string fk_paper, string empNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(PaperExamOfChoseAttr.FK_Emp, empNo);
            qo.addAnd();
            qo.AddWhere(PaperExamOfChoseAttr.FK_Paper, fk_paper);
            qo.DoQuery();
        }
		/// <summary>
		/// PaperExamOfChose
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfChose();
			}
		}
	}
}

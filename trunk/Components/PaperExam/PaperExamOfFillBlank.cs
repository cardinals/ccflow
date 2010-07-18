using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ����⿼��attr
	/// </summary>
	public class PaperExamOfFillBlankAttr:EntityOIDAttr
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
		/// �����
		/// </summary>
		public const string FK_FillBlank="FK_FillBlank";
		/// <summary>
		/// ���
		/// </summary>
		public const string IDX="IDX";
		/// <summary>
		/// ֵ
		/// </summary>
		public const string Val="Val";

	}
	/// <summary>
	/// ����⿼��
	/// </summary>
	public class PaperExamOfFillBlank :Entity
	{

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
				
				Map map = new Map("GTS_PaperExamOfFillBlank");
				map.EnDesc="�Ծ������";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamOfFillBlankAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(PaperExamOfFillBlankAttr.FK_Paper,null,"����",true,true,0,50,20);
				map.AddTBStringPK(PaperExamOfFillBlankAttr.FK_FillBlank,null,"�����",true,true,0,50,20);
				map.AddTBIntPK(PaperExamOfFillBlankAttr.IDX,-1,"���",true,false);
				map.AddTBString(PaperExamOfFillBlankAttr.Val,null,"val",true,true,0,5000,20);

				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfFillBlank+this.CentOfFillBlankOne+this.CentOfFillBlankM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// ����⿼��
		/// </summary> 
		public PaperExamOfFillBlank()
		{

		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public PaperExamOfFillBlank(string paper,string empNo,string fk_FillBlank, int idx)
		{
			this.FK_FillBlank = fk_FillBlank;
			this.FK_Emp=empNo;
			this.FK_Paper=paper;
			this.IDX=idx;
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

		#region attrs
		/// <summary>
		/// ���
		/// </summary>
		public int IDX
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfFillBlankAttr.IDX);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.IDX,value);
			}
		}
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfFillBlankAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfFillBlankAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_FillBlank
		/// </summary>
        public string FK_FillBlank
        {
            get
            {
                return this.GetValStringByKey(PaperExamOfFillBlankAttr.FK_FillBlank);
            }
            set
            {
                this.SetValByKey(PaperExamOfFillBlankAttr.FK_FillBlank, value);
            }
        }
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfFillBlankAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfFillBlankAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ����⿼��
	/// </summary>
	public class PaperExamOfFillBlanks :EntitiesOID
	{
		/// <summary>
		/// PaperExamOfFillBlanks
		/// </summary>
		public PaperExamOfFillBlanks(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamOfFillBlanks(string fk_paper, string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(PaperExamOfChoseAttr.FK_Paper,fk_paper);

			qo.addOrderBy(PaperExamOfFillBlankAttr.FK_FillBlank, PaperExamOfFillBlankAttr.IDX );
			qo.DoQuery();
		}
		/// <summary>
		/// PaperExamOfFillBlank
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfFillBlank();
			}
		}
	}
}

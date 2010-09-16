using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ����⿼��attr
	/// </summary>
	public class WorkRandomOfFillBlankAttr:WorkRandomThemeBaseAttr
	{
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
		/// <summary>
		/// ��
		/// </summary>
		public const string Answer="Answer";

	}
	/// <summary>
	/// ����⿼��
	/// </summary>
	public class WorkRandomOfFillBlank :WorkRandomThemeBase
	{

		/// <summary>
		/// �Ƿ���ȷ
		/// </summary>
		public bool IsRight
		{
			get
			{
				if (this.Val==this.Answer)
					return true;
				else
					return false;
			}
		}
		public string Answer
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfChoseOneAttr.Answer);
			}
			set
			{
				this.SetValByKey(WorkRandomOfChoseOneAttr.Answer,value);
			}
		}

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
				
				Map map = new Map("GTS_WorkRandomOfFillBlank");
				map.EnDesc="�Ծ������";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRandomOfFillBlankAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfFillBlankAttr.FK_WorkRandom,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkRandomOfFillBlankAttr.FK_FillBlank,null,"�����",true,true,0,50,20);
				map.AddTBIntPK(WorkRandomOfFillBlankAttr.IDX,-1,"���",true,false);
				map.AddTBString(WorkRandomOfFillBlankAttr.Val,null,"val",true,true,0,500,20);
				map.AddTBString(WorkRandomOfFillBlankAttr.Answer,null,"��",true,true,0,500,20);


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
		public WorkRandomOfFillBlank()
		{

		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfFillBlank(string work,string empNo,string fk_FillBlank, int idx)
		{
			this.FK_FillBlank = fk_FillBlank;
			this.FK_Emp=empNo;
			this.FK_WorkRandom=work;
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
				return this.GetValIntByKey(WorkRandomOfFillBlankAttr.IDX);
			}
			set
			{
				this.SetValByKey(WorkRandomOfFillBlankAttr.IDX,value);
			}
		}
		/// <summary>
		/// FK_FillBlank
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfFillBlankAttr.FK_FillBlank);
			}
			set
			{
				this.SetValByKey(WorkRandomOfFillBlankAttr.FK_FillBlank,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfFillBlankAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfFillBlankAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ����⿼��
	/// </summary>
	public class WorkRandomOfFillBlanks :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfFillBlanks
		/// </summary>
		public WorkRandomOfFillBlanks(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfFillBlanks(string FK_WorkRandom, string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfFillBlankAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfFillBlankAttr.FK_WorkRandom,FK_WorkRandom);

			qo.addOrderBy(WorkRandomOfFillBlankAttr.FK_FillBlank, WorkRandomOfFillBlankAttr.IDX );
			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfFillBlank
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfFillBlank();
			}
		}
	}
}

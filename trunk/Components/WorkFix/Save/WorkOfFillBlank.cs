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
	public class WorkOfFillBlankAttr:WorkThemeBaseAttr
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
	public class WorkOfFillBlank :WorkThemeBase
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
				return this.GetValStringByKey(WorkOfChoseAttr.Answer);
			}
			set
			{
				this.SetValByKey(WorkOfChoseAttr.Answer,value);
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
				
				Map map = new Map("GTS_WorkOfFillBlank");
				map.EnDesc="�Ծ������";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfFillBlankAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkOfFillBlankAttr.FK_Work,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkOfFillBlankAttr.FK_FillBlank,null,"�����",true,true,0,50,20);
				map.AddTBIntPK(WorkOfFillBlankAttr.IDX,-1,"���",true,false);
				map.AddTBString(WorkOfFillBlankAttr.Val,null,"val",true,true,0,500,20);
				map.AddTBString(WorkOfFillBlankAttr.Answer,null,"��",true,true,0,500,20);


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
		public WorkOfFillBlank()
		{

		}
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfFillBlank(string work,string empNo,string fk_FillBlank, int idx)
		{
			this.FK_FillBlank = fk_FillBlank;
			this.FK_Emp=empNo;
			this.FK_Work=work;
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
				return this.GetValIntByKey(WorkOfFillBlankAttr.IDX);
			}
			set
			{
				this.SetValByKey(WorkOfFillBlankAttr.IDX,value);
			}
		}
		/// <summary>
		/// FK_FillBlank
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(WorkOfFillBlankAttr.FK_FillBlank);
			}
			set
			{
				this.SetValByKey(WorkOfFillBlankAttr.FK_FillBlank,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkOfFillBlankAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfFillBlankAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ����⿼��
	/// </summary>
	public class WorkOfFillBlanks :WorkThemeBases
	{
		/// <summary>
		/// WorkOfFillBlanks
		/// </summary>
		public WorkOfFillBlanks(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfFillBlanks(string fk_work, string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkOfChoseAttr.FK_Work,fk_work);

			qo.addOrderBy(WorkOfFillBlankAttr.FK_FillBlank, WorkOfFillBlankAttr.IDX );
			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfFillBlank
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfFillBlank();
			}
		}
	}
}

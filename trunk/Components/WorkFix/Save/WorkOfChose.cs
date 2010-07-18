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
	public class WorkOfChoseAttr:WorkThemeBaseAttr
	{
		/// <summary>
		/// ѡ����
		/// </summary>
		public const string FK_Chose="FK_Chose";
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
	/// ѡ���⿼��
	/// </summary>
	public class WorkOfChose :WorkThemeBase
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
				
				Map map = new Map("GTS_WorkOfChose");
				map.EnDesc="�Ծ�ѡ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfChoseAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkOfChoseAttr.FK_Work,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkOfChoseAttr.FK_Chose,null,"ѡ����",true,true,0,50,20);
				map.AddTBString(WorkOfChoseAttr.Val,null,"��Ŀ",true,true,0,50,20);
				map.AddTBString(WorkOfChoseAttr.Answer,null,"��",true,true,0,50,20);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// ѡ���⿼��
		/// </summary> 
		public WorkOfChose()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfChose(string work,string empid,string fk_chose)
		{
			this.FK_Chose = fk_chose;
			this.FK_Emp=empid;
			this.FK_Work=work;
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
		/// <summary>
		/// FK_Chose
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(WorkOfChoseAttr.FK_Chose);
			}
			set
			{
				this.SetValByKey(WorkOfChoseAttr.FK_Chose,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkOfChoseAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfChoseAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ѡ���⿼��
	/// </summary>
	public class WorkOfChoses :WorkThemeBases
	{
		/// <summary>
		/// WorkOfChoses
		/// </summary>
		public WorkOfChoses(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfChoses(string fk_work,string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfChoseAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkOfChoseAttr.FK_Work,fk_work);


			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfChose
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfChose();
			}
		}
	}
}

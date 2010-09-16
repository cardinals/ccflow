using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ѡ���⿼��attr
	/// </summary>
	public class WorkRandomOfChoseMAttr:WorkRandomThemeBaseAttr
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
	public class WorkRandomOfChoseM :WorkRandomThemeBase
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
				 
				return this.GetValStringByKey(WorkRandomOfChoseMAttr.Answer);
			}
			set
			{
				this.SetValByKey(WorkRandomOfChoseMAttr.Answer,value);
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
				
				Map map = new Map("GTS_WorkRandomOfChoseM");
				map.EnDesc="��ҵ����ѡ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(WorkRandomOfChoseMAttr.FK_Emp,Web.WebUser.No,"ѧ��",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfChoseMAttr.FK_WorkRandom,null,"��ҵ",true,true,0,50,20);
				map.AddTBStringPK(WorkRandomOfChoseMAttr.FK_Chose,null,"ѡ����",true,true,0,50,20);
				map.AddTBString(WorkRandomOfChoseMAttr.Val,null,"��Ŀ",true,true,0,50,20);
				map.AddTBString(WorkRandomOfChoseMAttr.Answer,null,"��",true,true,0,50,20);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ѡ���⿼��
		/// </summary> 
		public WorkRandomOfChoseM()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfChoseM(string work,string empid,string fk_chose)
		{
			this.FK_Chose = fk_chose;
			this.FK_Emp=empid;
			this.FK_WorkRandom=work;
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
				return this.GetValStringByKey(WorkRandomOfChoseOneAttr.FK_Chose);
			}
			set
			{
				this.SetValByKey(WorkRandomOfChoseOneAttr.FK_Chose,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfChoseOneAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfChoseOneAttr.Val,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ѡ���⿼��
	/// </summary>
	public class WorkRandomOfChoseMs :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfChoseMs
		/// </summary>
		public WorkRandomOfChoseMs(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfChoseMs(string FK_WorkRandom,string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfChoseMAttr.FK_Emp,empNo);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfChoseMAttr.FK_WorkRandom,FK_WorkRandom);


			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfChoseM
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfChoseM();
			}
		}
	}
}

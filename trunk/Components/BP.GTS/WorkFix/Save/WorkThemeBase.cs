using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	 
	/// <summary>
	/// ��ҵ
	/// </summary>
	public class WorkThemeBaseAttr
	{
		/// <summary>
		/// ��ҵ
		/// </summary>
		public const string FK_Work="FK_Work";
		/// <summary>
		/// ѧ��
		/// </summary>
		public const string FK_Emp="FK_Emp";

	}
	/// <summary>
	/// ������洢����
	/// </summary>
	abstract public class WorkThemeBase :Entity
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
 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ������洢����
		/// </summary> 
		public WorkThemeBase()
		{
		}
		#endregion 

		#region ������洢����
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(WorkThemeBaseAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(WorkThemeBaseAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// Work
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkThemeBaseAttr.FK_Work);
			}
			set
			{
				this.SetValByKey(WorkThemeBaseAttr.FK_Work,value);
			}
		}
		#endregion
	}
	/// <summary>
	///   
	/// </summary>
	abstract public class WorkThemeBases :EntitiesOID
	{
		/// <summary>
		/// WorkThemeBases
		/// </summary>
		public WorkThemeBases(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkThemeBases(string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkThemeBaseAttr.FK_Emp,empNo);
			qo.DoQuery();
		}
		 
	}
}

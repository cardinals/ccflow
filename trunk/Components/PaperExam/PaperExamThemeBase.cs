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
	public class PaperExamThemeBaseAttr:EntityOIDAttr
	{
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// ѧ��
		/// </summary>
		public const string FK_Emp="FK_Emp";

	}
	/// <summary>
	/// ������洢����
	/// </summary>
	abstract public class PaperExamThemeBase :Entity
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
		public PaperExamThemeBase()
		{
		}

		 
		 
		#endregion 

		#region ������洢����
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamThemeBaseAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamThemeBaseAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamThemeBaseAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamThemeBaseAttr.FK_Paper,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  ѡ���⿼��
	/// </summary>
	abstract public class PaperExamThemeBases :EntitiesOID
	{
		/// <summary>
		/// PaperExamThemeBases
		/// </summary>
		public PaperExamThemeBases(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamThemeBases(string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamThemeBaseAttr.FK_Emp,empNo);
			qo.DoQuery();
		}
		 
	}
}

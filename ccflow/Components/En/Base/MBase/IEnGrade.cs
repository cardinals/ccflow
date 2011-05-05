using System;
using BP.En;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP;
 

namespace BP.En
{
	/// <summary>
	/// ������ʵ������
	/// </summary>
	public class IEnGradeAttr:IEnAttr
	{
		/// <summary>
		/// �Ƿ���ϸ
		/// </summary>
		public const string IsDetail = "IsDetail";
		/// <summary>
		/// Grade
		/// </summary>
		public const string Grade="Grade";		
	}
	/// <summary>
	/// �����Ե�ʵ��
	/// </summary>
	[Serializable]
	abstract public class IEnGrade : IEn
	{
		#region ��������
		/// <summary>
		/// ���
		/// </summary>
		public int Grade
		{
			get
			{
				return this.GetValIntByKey(IEnGradeAttr.Grade) ; 
			}
			set
			{
				this.SetValByKey(IEnGradeAttr.Grade,value);
			}
		}
		/// <summary>
		/// �Ƿ���ϸ
		/// </summary>
		public bool  IsDetail
		{
			get
			{
				return this.GetValBooleanByKey(IEnGradeAttr.IsDetail);
			}
			set
			{
				this.SetValByKey(IEnGradeAttr.IsDetail,value);
			}
		}
		#endregion		
 
		#region ����
		protected IEnGrade(){}
		protected IEnGrade(int oid) : base(oid){}
		protected IEnGrade(string no, string langugae ) : base(no,langugae){}
		#endregion
			
		
	}
	/// <summary>
	/// �����Ե�ʵ�弯��
	/// </summary>
	[Serializable]
	abstract public  class IEnsGrade : IEns
	{
		#region ���캯��
		public IEnsGrade( string fk_language) :base( fk_language){}
		/// <summary>
		/// ���캯��
		/// </summary>
		public IEnsGrade(){}
		#endregion
	}

}

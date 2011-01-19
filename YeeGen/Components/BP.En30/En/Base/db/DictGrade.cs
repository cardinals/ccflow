using System;
using System.Collections;

namespace BP.En.Base
{
	/// <summary>
	/// ����
	/// </summary>
	public class DictGradeAttr:DictAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Grade="Grade";
		/// <summary>
		/// �ǲ�������
		/// </summary>
		public const string IsDetal="IsDetal";

	}
	/// <summary>
	/// GradeDictEntity ��ժҪ˵����
	/// </summary>
	abstract public class DictGrade:Dict
	{
		public DictGrade()
		{			
		}
		public DictGrade(int oid)  : base(oid){}
		
		/// <summary>
		/// ����
		/// </summary>
		public int Grade
		{
			get
			{
				return this.GetValIntByKey(DictGradeAttr.Grade);
			}
			set
			{
				this.SetValByKey(DictGradeAttr.Grade,value);
			}
		}
		/// <summary>
		/// �ǲ�������
		/// </summary>
		public bool IsDetal
		{
			get
			{
				return this.GetValBooleanByKey(DictGradeAttr.IsDetal);
			}
			set
			{
				this.SetValByKey(DictGradeAttr.IsDetal,value);
			}

		}

	}
	abstract public class DictGrades: Dicts
	{
		public DictGrades()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}

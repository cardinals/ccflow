
using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Tax;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// ������ؿ�ʼ�����ڵ�����
	/// </summary>
	public class StartWorkOfZSJGAttr: StartWorkAttr
	{
		/// <summary>
		/// ������ر��
		/// </summary>
		public const string FK_Dept="FK_Dept";	 
	 
	}
	/// <summary>	 
	/// ������ؿ�ʼ�ڵ�,���й�����ص����ԵĿ�ʼ�ڵ�.
	/// </summary>
	abstract public class StartWorkOfZSJG : StartWork 
	{		 
		#region ��������
		/// <summary>
		/// �������
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(StartWorkOfZSJGAttr.FK_Dept);				
			}
			set
			{
				this.SetValByKey(StartWorkOfZSJGAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// �����������
		/// </summary>
		public string FK_DeptName
		{
			get
			{
				return this.GetValRefTextByKey(StartWorkOfZSJGAttr.FK_Dept);				
			}
		}
		#endregion		  		

		#region ��չ����
		/// <summary>
		/// ��˰��
		/// </summary>
		public ZSJG HisZSJG
		{
			get
			{
				return new ZSJG(this.FK_Dept);
			}
		}		 
		#endregion 

		#region ���캯��
		/// <summary>
		/// ��������
		/// </summary>
		protected StartWorkOfZSJG()
		{
		}
			 
		#endregion

		#region ��д
		/// <summary>
		/// �ڲ����ڸ���ǰ�ҵ���˰�˶�Ӧ�Ĳ���.
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction();
		}
		/// <summary>
		/// ����֮ǰ�Ĳ���
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			return base.beforeUpdate();
		}
		#endregion

	}
	/// <summary>
	/// �������̲ɼ���Ϣ�Ļ��� ����
	/// </summary>
	abstract public class StartWorkOfZSJGs : StartWorks
	{
		#region ���췽��
		/// <summary>
		/// ��Ϣ�ɼ�����
		/// </summary>
		public StartWorkOfZSJGs()
		{
		}
		#endregion		 
	}
}

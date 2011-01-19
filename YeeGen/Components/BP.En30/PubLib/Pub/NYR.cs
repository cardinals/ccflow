using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// ������
	/// </summary>
	public class NYR :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_NYR";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("NYR", "����"); // "����";
			}
		}
		#endregion 

		#region ���췽��
		 
		public NYR(){}
		 
		public NYR(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class NYRs :SimpleNoNameFixs
	{
		/// <summary>
		/// �����ռ���
		/// </summary>
		public NYRs()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NY();
			}
		}
		 
		 

	}
}

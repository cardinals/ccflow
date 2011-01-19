using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// ����ڼ�
	/// </summary>
	public class AP :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����		 
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_AP";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("QJ", "����ڼ�");  //"����ڼ�";
			}
		}
		#endregion 

		#region ���췽��
		 
		public AP(){}
		 
		public AP(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// APs
	/// </summary>
	public class APs :SimpleNoNameFixs
	{
		/// <summary>
		/// ����ڼ伯��
		/// </summary>
		public APs()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new AP();
			}
		}
	}
}

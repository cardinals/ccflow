using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ��ҵ
	/// </summary>
	public class WorkExt :SimpleNoNameFix
	{

		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "V_GTS_Work";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "��ҵ";
			}
		}
		#endregion 

		#region ���췽��
		public WorkExt()
		{
		}
		public WorkExt(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class WorkExts :SimpleNoNameFixs
	{
		/// <summary>
		///  ����
		/// </summary>
		public WorkExts()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkExt();
			}
		}
	}
}

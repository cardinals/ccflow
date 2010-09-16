using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �Ծ�
	/// </summary>
	public class PaperExt :SimpleNoNameFix
	{

		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "V_GTS_Paper";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "�Ծ�";
			}
		}
		#endregion 

		#region ���췽��
		public PaperExt()
		{
		}
		public PaperExt(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class PaperExts :SimpleNoNameFixs
	{
		/// <summary>
		///  ����
		/// </summary>
		public PaperExts()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExt();
			}
		}
	}
}

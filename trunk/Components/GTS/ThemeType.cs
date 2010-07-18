using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ����ڼ�
	/// </summary>
	public class ThemeType :SimpleNoNameFix
	{
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string ChoseOne="ChoseOne";
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string ChoseM="ChoseM";
		/// <summary>
		/// �ж���
		/// </summary>
		public const string JudgeTheme="JudgeTheme";
		/// <summary>
		/// �����
		/// </summary>
		public const string FillBlank="FillBlank";
		/// <summary>
		/// �ʴ���
		/// </summary>
		public const string EssayQuestion="EssayQuestion";
		/// <summary>
		/// �Ķ������Ŀ
		/// </summary>
		public const string RC="RC";


		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "GTS_ThemeType";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "��Ŀ����";
			}
		}
		#endregion 

		#region ���췽��
		public ThemeType()
		{
		}
		public ThemeType(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class ThemeTypes :SimpleNoNameFixs
	{
		/// <summary>
		/// ����ڼ伯��
		/// </summary>
		public ThemeTypes()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ThemeType();
			}
		}
	}
}

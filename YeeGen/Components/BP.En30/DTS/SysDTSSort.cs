using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// ����ڼ�
	/// </summary>
	public class SysDTSSort :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		 
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_DTSSort";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "���";
			}
		}
		#endregion 

		#region ���췽��
		 
		public SysDTSSort(){
		
		}
		 
		public SysDTSSort(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class SysDTSSorts :SimpleNoNameFixs
	{
		/// <summary>
		/// ����ڼ伯��
		/// </summary>
		public SysDTSSorts()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysDTSSort();
			}
		}
	}
}

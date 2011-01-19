using System;
using System.Collections; 
using CWAI.En.Base;
//using CWAI.ZHZS.Base;

namespace CWAI.DA
{
	/// <summary>
	/// DSTaxpayer ��ժҪ˵����
	/// </summary>
	public class EnsUAC :SimpleNoName
	{
		#region ʵ�ֻ����ķ�����
		protected override string  PhysicsTable
		{
			get
			{
				return "Sys_DBSimpleNoNames";
			}
		}
		protected override string  Desc
		{
			get
			{
				return "Sys_DBSimpleNoNames";				
			}
		}
		#endregion 

		#region ���췽��
		public EnsUAC(){}
		/// <summary>
		/// ˰����
		/// </summary>
		/// <param name="_No"></param>
		public EnsUAC(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// ��˰�˼���
	/// </summary>
	public class EnsUACs :SimpleNoNames
	{
		public EnsUACs(){}
		/// <summary>
		/// �õ����� Entity.
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EnsUAC();
			}
			 
		}
		 
	}
}

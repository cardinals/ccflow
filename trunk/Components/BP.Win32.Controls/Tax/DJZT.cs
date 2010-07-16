using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
 

namespace BP.Tax
{
	/// <summary>
	/// DSTaxpayer ��ժҪ˵����DSDJZT
	/// </summary>
	public class DJZT : SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_DJZT";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "�Ǽ�״̬";		
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// dengji
		/// </summary>
		public DJZT(){}
		
		/// <summary>
		/// state 
		/// </summary>
		/// <param name="_No">No</param>
		public DJZT(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// ��˰�˼���DSDJZTs
	/// </summary>
	public class DJZTs :SimpleNoNameFixs
	{
		/// <summary>
		/// state
		/// </summary>
		public DJZTs(){}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DJZT();
			}
		}
	}
}

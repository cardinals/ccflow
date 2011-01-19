using System;
using System.Collections;
using BP.DA;
using BP.En;
 

namespace BP.Sys
{	 
	/// <summary>
	/// װ������
	/// </summary>
	public class DataLoadType :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_DataLoadType";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "װ������";				
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// װ������
		/// </summary> 
		public DataLoadType(){}
		/// <summary>
		/// װ������
		/// </summary>
		/// <param name="_No"></param>
		public DataLoadType(string _No ): base(_No){}
		#endregion 
	}
	 /// <summary>
	 /// װ������s
	 /// </summary>
	public class DataLoadTypes :SimpleNoNameFixs
	{
		/// <summary>
		/// ��������
		/// </summary>
		public DataLoadTypes(){}
		/// <summary>
		/// װ������ Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DataLoadType();
			}
		}
	}
}

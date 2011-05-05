using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// ����ڼ�
	/// </summary>
	public class Zoom :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_Zoom";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
        public override string Desc
        {
            get
            {
                return "ͼƬ����";
            }
        }
		#endregion 

		#region ���췽��
		public Zoom(){}
		public Zoom(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class Zooms :SimpleNoNameFixs
	{
		/// <summary>
		/// ����ڼ伯��
		/// </summary>
		public Zooms()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Zoom();
			}
		}
	}
}

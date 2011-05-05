using System;
using BP.DTS;
using BP.DA;


namespace BP.WF.DTS
{


	/// <summary>
	/// ����ͳ��
	/// </summary>
	public class InitFlows:DataIOEn2
	{
		/// <summary>
		/// ����ͳ��
		/// </summary>
		public InitFlows()
		{
			this.HisDoType=DoType.UnName;
			this.Title="���̻���(��������ͳ�Ʒ���)";
			this.Note="�������̵�ͳ�Ʒ���";
			this.HisRunTimeType=RunTimeType.UnName;
			this.HisUserType = BP.Web.UserType.SysAdmin ; 
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		/// <summary>
		/// ����ͳ��
		/// </summary>
		public override void Do()
		{
			WFDTS.InitFlows(DataType.CurrentYear+"-01-01 00:00");
		 
		}
	}	 
}

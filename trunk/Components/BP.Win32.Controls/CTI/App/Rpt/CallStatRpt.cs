
using System;

using BP.Tax;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En.Base;
using BP.Rpt;
using BP.Web;
 


namespace BP.CTI.App
{
	/// <summary>
	/// ��˰��ͳ�Ʊ���
	/// </summary>
	public class CallStatRpt : Rpt3D
	{
		/// <summary>
		/// ����
		/// </summary>
		public CallStatRpt()
		{
			#region ����Ļ�������
			this.HisEns = new BP.CTI.App.CallStats();
			this.Title="����ͳ�Ʒ���";
			this.DataProperty="����";
			this.IsShowRate=true; //�Ƿ���ʾ�ٷֱȡ�
			this.IsShowSum=true;  //�Ƿ���ʾ�ϼơ�
			// ���� �����С�default  COUNT(*)
			//this.OperationColumn="AVG(ZCZJ)";
			#endregion

			#region ������ͬ�ֶβ�ѯ����
			this.IsShowSearchKey=false;
			#endregion

			#region ���������ѯ����
			//this.AddFKSearchAttrs(CallStatAttr.FK_Dept);
			this.AddFKSearchAttrs(CallStatAttr.CallDate);
			this.AddFKSearchAttrs(CallStatAttr.FK_TelType);
			#endregion

			#region ����γ������
			this.AddDAttrByKey(CallStatAttr.CallDate);
			this.AddDAttrByKey(CallStatAttr.FK_TelType);
			#endregion

			#region ����Ĭ�ϵ�γ�����ԣ� ���û�����Ϳ���ʹ����.
			this.AttrOfD1=CallStatAttr.CallDate;
			this.AttrOfD2=CallStatAttr.FK_TelType;
			#endregion
		} 
		 
	}
}

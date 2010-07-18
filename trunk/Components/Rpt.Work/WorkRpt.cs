using System;

using BP.Tax;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;
using BP.Rpt;
using BP.Web;
using BP.GTS;
 


namespace BP.GTS
{
	/// <summary>
	/// ��˰��ͳ�Ʊ���
	/// </summary>
	public class WorkRpt : Rpt3D
	{
		/// <summary>
		/// ����
		/// </summary>
		public WorkRpt()
		{
			#region ����Ļ�������
			this.HisEns = new WorkDtls();
			this.Title="��ҵ����";
			this.DataProperty="����";
			this.IsShowRate=true; //�Ƿ���ʾ�ٷֱȡ�
			this.IsShowSum=true;  //�Ƿ���ʾ�ϼơ�

			//this.HisAnalyseObjs.AddAnalyseObj("������������","COUNT(*)");

			this.HisAnalyseObjs.AddAnalyseObj("�ܷ�","SUM("+WorkDtlAttr.CentOfSum+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("ƽ����","AVG("+WorkDtlAttr.CentOfSum+")", AnalyseDataType.AppMoney);

			this.HisAnalyseObjs.AddAnalyseObj("��ѡ���ܷ�","SUM("+WorkDtlAttr.CentOfChoseOne+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("��ѡ��ƽ����","AVG("+WorkDtlAttr.CentOfChoseOne+")", AnalyseDataType.AppMoney);
			
			
			this.HisAnalyseObjs.AddAnalyseObj("��ѡ���ܷ�","SUM("+WorkDtlAttr.CentOfChoseM+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("��ѡ��ƽ����","AVG("+WorkDtlAttr.CentOfChoseM+")", AnalyseDataType.AppMoney);

			
			this.HisAnalyseObjs.AddAnalyseObj("������ܷ�","SUM("+WorkDtlAttr.CentOfFillBlank+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("�����ƽ����","AVG("+WorkDtlAttr.CentOfFillBlank+")", AnalyseDataType.AppMoney);

			this.HisAnalyseObjs.AddAnalyseObj("�ж����ܷ�","SUM("+WorkDtlAttr.CentOfJudgeTheme+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("�ж���ƽ����","AVG("+WorkDtlAttr.CentOfJudgeTheme+")", AnalyseDataType.AppMoney);


			this.HisAnalyseObjs.AddAnalyseObj("�ʴ����ܷ�","SUM("+WorkDtlAttr.CentOfEssayQuestion+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("�ʴ���ƽ����","AVG("+WorkDtlAttr.CentOfEssayQuestion+")", AnalyseDataType.AppMoney);
			#endregion

			#region ������ͬ�ֶβ�ѯ����
			this.IsShowSearchKey=false;
			//	this.HisAttrsOfSearch.AddFromTo("��¼����",WorkDtlAttr.FromDateTime, DateTime.Now.Year+"-01-01", DA.DataType.CurrentData, 7 );
			#endregion

			#region ���������ѯ����
			this.AddFKSearchAttrs(WorkDtlAttr.FK_Dept);
		//	this.AddFKSearchAttrs(WorkDtlAttr.FK_Paper);
			this.AddFKSearchAttrs(WorkDtlAttr.FK_Level);
			#endregion

			#region ����γ������
			this.AddDAttrByKey(WorkDtlAttr.FK_Dept);
			this.AddDAttrByKey(WorkDtlAttr.FK_Emp);
			//this.AddDAttrByKey(WorkDtlAttr.FK_Paper);
			this.AddDAttrByKey(WorkDtlAttr.FK_Level);
			#endregion

			#region ����Ĭ�ϵ�γ�����ԣ� ���û�����Ϳ���ʹ����.
			//this.AttrOfD1=WorkDtlAttr.FK_Paper;
			this.AttrOfD2=WorkDtlAttr.FK_Emp;
			this.AttrOfD3=WorkDtlAttr.FK_Emp;
			#endregion
		} 
	}
}

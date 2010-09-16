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
	public class ExamRpt : Rpt3D
	{
		/// <summary>
		/// ����
		/// </summary>
		public ExamRpt()
		{
			#region ����Ļ�������
			this.HisEns = new PaperExamExts();
			this.Title="�Ծ����";
			this.DataProperty="����";
			this.IsShowRate=true; //�Ƿ���ʾ�ٷֱȡ�
			this.IsShowSum=true;  //�Ƿ���ʾ�ϼơ�

			//this.HisAnalyseObjs.AddAnalyseObj("������������","COUNT(*)");

			this.HisAnalyseObjs.AddAnalyseObj("�ܷ�","SUM("+PaperExamAttr.CentOfSum+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("ƽ����","AVG("+PaperExamAttr.CentOfSum+")", AnalyseDataType.AppMoney);

			this.HisAnalyseObjs.AddAnalyseObj("��ѡ���ܷ�","SUM("+PaperExamAttr.CentOfChoseOne+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("��ѡ��ƽ����","AVG("+PaperExamAttr.CentOfChoseOne+")", AnalyseDataType.AppMoney);
			
			
			this.HisAnalyseObjs.AddAnalyseObj("��ѡ���ܷ�","SUM("+PaperExamAttr.CentOfChoseM+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("��ѡ��ƽ����","AVG("+PaperExamAttr.CentOfChoseM+")", AnalyseDataType.AppMoney);

			
			this.HisAnalyseObjs.AddAnalyseObj("������ܷ�","SUM("+PaperExamAttr.CentOfFillBlank+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("�����ƽ����","AVG("+PaperExamAttr.CentOfFillBlank+")", AnalyseDataType.AppMoney);

			this.HisAnalyseObjs.AddAnalyseObj("�ж����ܷ�","SUM("+PaperExamAttr.CentOfJudgeTheme+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("�ж���ƽ����","AVG("+PaperExamAttr.CentOfJudgeTheme+")", AnalyseDataType.AppMoney);


			this.HisAnalyseObjs.AddAnalyseObj("�ʴ����ܷ�","SUM("+PaperExamAttr.CentOfEssayQuestion+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("�ʴ���ƽ����","AVG("+PaperExamAttr.CentOfEssayQuestion+")", AnalyseDataType.AppMoney);
			#endregion

			#region ������ͬ�ֶβ�ѯ����
			this.IsShowSearchKey=false;
			//	this.HisAttrsOfSearch.AddFromTo("��¼����",PaperExamAttr.FromDateTime, DateTime.Now.Year+"-01-01", DA.DataType.CurrentData, 7 );
			#endregion

			#region ���������ѯ����
			this.AddFKSearchAttrs(PaperExamAttr.FK_Dept);
			this.AddFKSearchAttrs(PaperExamAttr.FK_Paper);
			this.AddFKSearchAttrs(PaperExamAttr.FK_Level);
			#endregion

			#region ����γ������
			this.AddDAttrByKey(PaperExamAttr.FK_Dept);
			this.AddDAttrByKey(PaperExamAttr.FK_Emp);
			this.AddDAttrByKey(PaperExamAttr.FK_Paper);
			this.AddDAttrByKey(PaperExamAttr.FK_Level);
			#endregion

			#region ����Ĭ�ϵ�γ�����ԣ� ���û�����Ϳ���ʹ����.
			this.AttrOfD1=PaperExamAttr.FK_Paper;
			this.AttrOfD2=PaperExamAttr.FK_Emp;
			this.AttrOfD3=PaperExamAttr.FK_Emp;
			#endregion
		} 
	}
}

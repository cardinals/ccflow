using System;

using BP.Tax;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En.Base;
using BP.Rpt;
using BP.Web;
using BP.WF;


namespace BP.WF
{
	/// <summary>
	/// ��˰��ͳ�Ʊ���
	/// </summary>
	public class BookRpt : Rpt3D
	{
		/// <summary>
		/// ����
		/// </summary>
		public BookRpt()
		{
			#region ����Ļ�������

			this.HisEns = new BP.WF.Books();
			this.Title="�������";
			this.DataProperty="����";
			this.IsShowRate=true;  //�Ƿ���ʾ�ٷֱȡ�
			this.IsShowSum=false;  //�Ƿ���ʾ�ϼơ�

			//this.HisAnalyseObjs.AddAnalyseObj("������������","COUNT(*)");
			//this.HisAnalyseObjs.AddAnalyseObj("���忼��ͳ��","SUM(Cent)", AnalyseDataType.AppMoney);
			//this.HisAnalyseObjs.AddAnalyseObj("ʱЧ�۷�ͳ��","SUM(CentOfCut)", AnalyseDataType.AppMoney);
			//this.HisAnalyseObjs.AddAnalyseObj("�������÷�ͳ��","SUM(CentOfAdd)", AnalyseDataType.AppMoney);
			//this.HisAnalyseObjs.AddAnalyseObj("��������ͳ��","SUM(CentOfQU)", AnalyseDataType.AppMoney);
			//�����÷�ͳ��
			//this.HisAnalyseObjs.AddAnalyseObj("���忼��ͳ��","SUM(Cent)+SUM(CentOfCut)+SUM(CentOfQU)");
			// ���� �����С�default  COUNT(*)
			//this.OperationColumn="AVG(ZCZJ)";
			#endregion

			#region ������ͬ�ֶβ�ѯ����
			this.IsShowSearchKey=false;
			//this.HisAttrsOfSearch.AddFromTo("����",BookAttr.RDT, DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"), DA.DataType.CurrentData, 7 );
			#endregion

			#region ���������ѯ����
			if (WebUser.IsSJUser)
				this.AddFKSearchAttrs(BookAttr.FK_XJ);
			else
				this.AddFKSearchAttrs(BookAttr.FK_Dept);

			this.AddFKSearchAttrs(BookAttr.BookState);

			#endregion

			#region ����γ������
			//this.AddDAttrByKey(BookAttr.FK_Flow);
			this.AddDAttrByKey(BookAttr.FK_Dept);


//			if (WebUser.IsSJUser)
//				this.AddDAttrByKey(BookAttr.FK_XJ);
//			else
//				this.AddDAttrByKey(BookAttr.FK_Dept);

			//this.AddDAttrByKey(BookAttr.FK_Node);
			this.AddDAttrByKey(BookAttr.FK_NY);
			this.AddDAttrByKey(BookAttr.FK_NodeRefFunc);
			this.AddDAttrByKey(BookAttr.Recorder);
			#endregion

			#region ����Ĭ�ϵ�γ�����ԣ� ���û�����Ϳ���ʹ����.
			this.AttrOfD1=BookAttr.FK_NY;
			if (WebUser.IsSJUser)
			{
				this.AttrOfD2=BookAttr.FK_XJ;
				this.AttrOfD3= BookAttr.FK_XJ;
			}
			else
			{
				//this.AddFKSearchAttrs(BookAttr.FK_Emp);
				//this.AddFKSearchAttrs(BookAttr.FK_Emp);
				this.AttrOfD2=BookAttr.FK_Dept;
				this.AttrOfD3= BookAttr.FK_Dept;
			}
			#endregion

		} 
	}
}

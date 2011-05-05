using System;

using BP.Port;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;
using BP.Rpt;
using BP.Web;
using BP.WF;


namespace BP.WF
{
	/// <summary>
	/// ��˰��ͳ�Ʊ���
	/// </summary>
	public class CHOfNodeRpt : Rpt3D
	{
		/// <summary>
		/// ����
		/// </summary>
        public CHOfNodeRpt()
        {
            #region ����Ļ�������

            this.HisEns = new BP.WF.CHOfNodes();
            this.Title = "��������";
            this.DataProperty = "��������";
            this.IsShowRate = true;  //�Ƿ���ʾ�ٷֱȡ�
            this.IsShowSum = false;  //�Ƿ���ʾ�ϼơ�

            //this.HisAnalyseObjs.AddAnalyseObj("������������","COUNT(*)");
            this.HisAnalyseObjs.AddAnalyseObj("���忼��ͳ��", "SUM(Cent)", AnalyseDataType.AppMoney);
            this.HisAnalyseObjs.AddAnalyseObj("ʱЧ�۷�ͳ��", "SUM(CentOfCut)", AnalyseDataType.AppMoney);
            this.HisAnalyseObjs.AddAnalyseObj("�������÷�ͳ��", "SUM(CentOfAdd)", AnalyseDataType.AppMoney);
            this.HisAnalyseObjs.AddAnalyseObj("��������ͳ��", "SUM(CentOfQU)", AnalyseDataType.AppMoney);
            //�����÷�ͳ��
            //this.HisAnalyseObjs.AddAnalyseObj("���忼��ͳ��","SUM(Cent)+SUM(CentOfCut)+SUM(CentOfQU)");
            // ���� �����С�default  COUNT(*)
            //this.OperationColumn="AVG(ZCZJ)";
            #endregion

            #region ������ͬ�ֶβ�ѯ����
            this.IsShowSearchKey = false;
            //this.HisAttrsOfSearch.AddFromTo("����",CHOfNodeAttr.RDT, DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"), DA.DataType.CurrentData, 7 );
            #endregion

            #region ���������ѯ����
            this.AddFKSearchAttrs(CHOfNodeAttr.FK_Dept);
            this.AddFKSearchAttrs(CHOfNodeAttr.FK_NY);
            this.AddFKSearchAttrs(CHOfNodeAttr.FK_Emp);
            #endregion

            #region ����γ������
            this.AddDAttrByKey(CHOfNodeAttr.FK_Flow);

            
                this.AddDAttrByKey(CHOfNodeAttr.FK_Dept);

            this.AddDAttrByKey(CHOfNodeAttr.FK_Station);
            this.AddDAttrByKey(CHOfNodeAttr.FK_NY);
            //this.AddDAttrByKey(CHOfNodeAttr.FK_AP);
            this.AddDAttrByKey(CHOfNodeAttr.FK_Dept);
            this.AddDAttrByKey(CHOfNodeAttr.FK_Emp);
            #endregion

            #region ����Ĭ�ϵ�γ�����ԣ� ���û�����Ϳ���ʹ����.
            this.AttrOfD1 = CHOfNodeAttr.FK_NY;
          
                this.AttrOfD2 = CHOfNodeAttr.FK_Dept;
                this.AttrOfD3 = CHOfNodeAttr.FK_Dept;
            #endregion

        } 
	}
}

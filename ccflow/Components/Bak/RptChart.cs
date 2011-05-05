using System;
//using OWC10;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.Rpt
{
	/// <summary>
	/// RptChart ��ժҪ˵����
	/// </summary>
	public class RptChart
	{
		/// <summary>
		/// ����ͼ��
		/// </summary>
		/// <param name="sdr">����Դ(sqlDataReader)</param>
		/// <param name="_type">ͼ������(ö��)</param>
		/// <param name="filePath">ͼƬ·��</param>
		/// <param name="chartWidth">ͼƬ���</param>
		/// <param name="chartHeight">ͼƬ�߶�</param>
		/// <returns>ͼƬ����</returns>
		public static string GenerChart(Rpt3DEntity rpt , ChartChartTypeEnum charttype, int chartWidth,int chartHeight,string fileName)
		{
			OWC10.ChartSpace objCSpace = new OWC10.ChartSpaceClass(); 
			//��ChartSpace���������ͼ��Add��������chart����
			OWC10.ChChart objChart = objCSpace.Charts.Add (0);
			
			//ָ��ͼ���Ƿ���Ҫͼ��
			objChart.HasLegend=true;
			objChart.HasTitle=true;
			objChart.Title.Caption=rpt.Title;
			//ָ��ͼ������͡�������OWC.ChartChartTypeEnumö��ֵ�õ�
			//objChart.Type = OWC10.ChartChartTypeEnum.chChartTypeColumnClustered;
			objChart.Type = charttype;			
			
			#region chart1
			int j=-1;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				j++;
				//�� ChartSpace ���������ͼ��Add��������chart����
				objChart.SeriesCollection.Add(j);
				objChart.SeriesCollection[j].DataLabelsCollection.Add();
				//string strSeriesName=""+(i+1);
				string strSeriesName=en1.No+en1.Name;
				//����series������
				objChart.SeriesCollection[j].SetData (OWC10.ChartDimensionsEnum.chDimSeriesNames,
					(int)OWC10.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);

				string strCategory="";
				string strValue="";
				foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
				{
					foreach(EntityNoName en3 in rpt.SingleDimensionItem3)
					{
						float val =float.Parse( rpt.HisCells.GetCell(en1.No,en2.No,en3.No).val.ToString())  ;
						if (val==0)
							continue;

						strCategory+= en2.Name + '\t'  +en3.Name + '\t' ;
						strValue+= val.ToString() + '\t';
					}
				}
				//��������
				objChart.SeriesCollection[j].SetData (OWC10.ChartDimensionsEnum.chDimCategories,
					(int)OWC10.ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
				//����ֵ
				objChart.SeriesCollection[j].SetData
					(OWC10.ChartDimensionsEnum.chDimValues,
					(int)OWC10.ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
				//objChart.SeriesCollection[i].Caption="";
			}		 
			#endregion

			if (fileName=="" || fileName==null)
			{

				fileName=DBAccess.GenerOID()+".gif";
				fileName = "D:\\WebApp\\"+fileName;
			}

			try
			{
				objCSpace.ExportPicture(fileName, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@���ܴ����ļ�,������Ȩ�޵����⣬��Ѹ�Ŀ¼����Ϊ�κ��˶������޸ġ�"+fileName+" Exception:"+ex.Message);
			}
			return  fileName ;
		}
	}
}

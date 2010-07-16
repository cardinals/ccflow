using System; 
using System.Data;
using System.Drawing;
using CWAI.En.Base;
using CWAI.DA;

namespace CWAI.Win32.Controls
{
	/// <summary>
	/// DG ��ժҪ˵����
	/// </summary>
	public class DG : System.Windows.Forms.DataGrid
	{	
		#region ����ʱ�̵ı���
		/// <summary>
		/// ������ʱ����
		/// </summary>
		private float[]  MyCount= new float[50];
		public Entity HisEn=null;
		public string DataKeyField=null;
		#endregion


		#region ����
		public DG()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

	

		#region ���ڶ��б༭�ķ���
		/// <summary>
		/// ��ʼ��һ��datagride �ṹ���»�������
		/// </summary>
		/// <param name="en">entity</param> 
		public void InitDGColumns(Entity  en)
		{
			this.DataKeyField=en.PK;
			this.HisEn = en;
			this.Columns.Clear();
			this.AllowSorting=true;
			this.AutoGenerateColumns=false;

			ButtonColumn btn1 = new ButtonColumn();
			btn1.HeaderText="����";
			btn1.ButtonType = ButtonColumnType.LinkButton;
			btn1.CommandName="Select";
			btn1.Text="ѡ��";			
			this.Columns.Add(btn1);
			Attrs attrs = en.EnMap.Attrs;
			foreach(Attr attr in attrs)
			{
				BoundColumn bc = new  BoundColumn(); 
				bc.DataField=attr.Key;
				bc.HeaderText=attr.Desc;
				bc.Visible=attr.UIVisible;
				this.Columns.Add(bc);
			}
		}		
		#endregion
	}
}

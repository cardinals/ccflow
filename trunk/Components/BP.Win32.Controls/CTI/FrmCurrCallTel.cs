using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.En;
using BP.DA;
using BP.En.Base;

namespace BP.CTI
{
	/// <summary>
	/// FrmCurrCall ��ժҪ˵����
	/// </summary>
	public class FrmCurrCall : System.Windows.Forms.Form
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.ToolBarButton Btn_Ref;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.ComponentModel.IContainer components;

		public FrmCurrCall()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmCurrCall));
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_Ref = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.Btn_Ref});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(292, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// Btn_Ref
			// 
			this.Btn_Ref.ImageIndex = 0;
			this.Btn_Ref.Text = "ˢ��";
			this.Btn_Ref.ToolTipText = "ˢ��";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 41);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(292, 232);
			this.dataGrid1.TabIndex = 1;
			// 
			// FrmCurrCall
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "FrmCurrCall";
			this.Text = "FrmCurrCall";
			this.Load += new System.EventHandler(this.FrmCurrCall_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
		
			DataTable dt = BP.CTI.App.CallLists.GetCurrentCall; 
			dt.TableName="��ǰ����";
			this.dataGrid1.DataSource = dt;
			this.dataGrid1.ReadOnly=true;
			//this.dg1
			//this.dg1.BindEnsThisOnly(BP.CTI.App.CallLists.HisCallList,true,true);
		}

		private void FrmCurrCall_Load(object sender, System.EventArgs e)
		{
			this.dataGrid1.CaptionText="��ǰ�����绰";
 
			this.Text="��ǰ�����绰";
			this.bpToolbar1_ButtonClick(null,null);
		}
	}
}

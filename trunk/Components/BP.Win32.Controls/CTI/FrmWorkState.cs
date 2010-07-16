using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.CTI;

namespace BP.CTI
{
	/// <summary>
	/// FrmWorkState ��ժҪ˵����
	/// </summary>
	public class FrmWorkState : BP.Win32.PageBase
	{
		private System.Timers.Timer timer1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton Btn_CH;
		private System.Windows.Forms.ToolBarButton Btn_WorkList;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public FrmWorkState()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmWorkState));
			this.timer1 = new System.Timers.Timer();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.Btn_CH = new System.Windows.Forms.ToolBarButton();
			this.Btn_WorkList = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 3000;
			this.timer1.SynchronizingObject = this;
			this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(292, 273);
			this.dataGrid1.TabIndex = 0;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 251);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(292, 22);
			this.statusBar1.TabIndex = 1;
			this.statusBar1.Text = "statusBar1";
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.Btn_CH,
																						this.Btn_WorkList});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(292, 41);
			this.toolBar1.TabIndex = 2;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// Btn_CH
			// 
			this.Btn_CH.ImageIndex = 0;
			this.Btn_CH.Text = "ͨ������״̬";
			// 
			// Btn_WorkList
			// 
			this.Btn_WorkList.ImageIndex = 0;
			this.Btn_WorkList.Text = "�ڴ���ж���";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FrmWorkState
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.dataGrid1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmWorkState";
			this.Text = "FrmWorkState";
			this.Load += new System.EventHandler(this.FrmWorkState_Load);
			((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			//this.dataGrid1.DataSource=;
		
			
			//this.textBox1.Text=Card.GetCHStates();
		}

		private void FrmWorkState_Load(object sender, System.EventArgs e)
		{
			this.Text="����״̬";
			if (Card.Serial==null)
			{
				this.toolBar1.Enabled=false;
				this.Information("�������ڿͻ��˲쿴���忨�Ĺ���״̬��");
				this.Close();
				return;
			}
			
			if (Card.HisCardWorkState==CardWorkState.Stop)
			{
				this.Information("������ֹͣ����,���ܲ쿴����״̬.");
				this.Close();
			}
			else
			{
				this.Information("��ʱ��Ĳ쿴�������Ĺ���״̬��Ӱ��������������...");
			}
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			switch( e.Button.Text )
			{
				case "ͨ������״̬":
					this.dataGrid1.CaptionText="ͨ������״̬";
					if ( Card.StateDT !=null)
					{
						this.dataGrid1.SetDataBinding(Card.StateDT,"");
					}
					break;
				case "�ڴ���ж���":
					this.dataGrid1.CaptionText="�ڴ���ж���";
					if ( BP.CTI.App.CallLists.HisCallList !=null)
					{
						this.dataGrid1.SetDataBinding(BP.CTI.App.CallLists.HisCallList.ToDataTableDesc(),"");
					}
					break;				 
				case "�˳�":
					this.Close();
					break;
			}
			this.Cursor = Cursors.Default;
			Application.DoEvents();
		}
	}
}

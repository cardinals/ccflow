using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BP.En;

namespace BP.Admin
{
	/// <summary>
	/// MainFrom ��ժҪ˵����
	/// </summary>
	public class MainFrom : BP.Win32.PageBase
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.ToolBarButton Btn_Exit;
		public  string bgPath ;
		private System.Windows.Forms.ToolBarButton Btn_DTS;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ToolBarButton Btn_EnsUac;
		private System.Windows.Forms.ToolBarButton Btn_UIUserEns;
		private System.Windows.Forms.ToolBarButton Btn_UIAdminEns;
		private System.Windows.Forms.ToolBarButton Btn_ChangePass;
		private System.Windows.Forms.ToolBarButton Btn_SysOption;
		private System.Windows.Forms.ToolBarButton Btn_GenerSQL;
		private System.ComponentModel.IContainer components;

		public MainFrom()
		{
			//BP.WF.WFDTS.TransferAutoGenerWorkBreed();

			//BP.Tax.WF.DJ.KYDJInfoes

		//	BP.Tax.WF.DJ.KYDJInfoes.AutoGenerWorkBreed();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainFrom));
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_UIUserEns = new System.Windows.Forms.ToolBarButton();
			this.Btn_UIAdminEns = new System.Windows.Forms.ToolBarButton();
			this.Btn_EnsUac = new System.Windows.Forms.ToolBarButton();
			this.Btn_DTS = new System.Windows.Forms.ToolBarButton();
			this.Btn_SysOption = new System.Windows.Forms.ToolBarButton();
			this.Btn_ChangePass = new System.Windows.Forms.ToolBarButton();
			this.Btn_Exit = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.Btn_GenerSQL = new System.Windows.Forms.ToolBarButton();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.Btn_UIUserEns,
																						  this.Btn_UIAdminEns,
																						  this.Btn_EnsUac,
																						  this.Btn_DTS,
																						  this.Btn_SysOption,
																						  this.Btn_ChangePass,
																						  this.Btn_GenerSQL,
																						  this.Btn_Exit});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(472, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// Btn_UIUserEns
			// 
			this.Btn_UIUserEns.ImageIndex = 0;
			this.Btn_UIUserEns.Tag = "UIUserEns";
			this.Btn_UIUserEns.Text = "�û���ά��";
			// 
			// Btn_UIAdminEns
			// 
			this.Btn_UIAdminEns.ImageIndex = 0;
			this.Btn_UIAdminEns.Tag = "UIAdminEns";
			this.Btn_UIAdminEns.Text = "Adminά��";
			// 
			// Btn_EnsUac
			// 
			this.Btn_EnsUac.ImageIndex = 0;
			this.Btn_EnsUac.Tag = "BP.Sys.SysEnsUACs";
			this.Btn_EnsUac.Text = "Ȩ�޹���";
			// 
			// Btn_DTS
			// 
			this.Btn_DTS.ImageIndex = 1;
			this.Btn_DTS.Tag = "DTS";
			this.Btn_DTS.Text = "����";
			// 
			// Btn_SysOption
			// 
			this.Btn_SysOption.ImageIndex = 4;
			this.Btn_SysOption.Text = "ѡ��";
			this.Btn_SysOption.ToolTipText = "����ϵͳ�����е��û����á�";
			// 
			// Btn_ChangePass
			// 
			this.Btn_ChangePass.ImageIndex = 3;
			this.Btn_ChangePass.Text = "�޸�����";
			// 
			// Btn_Exit
			// 
			this.Btn_Exit.ImageIndex = 2;
			this.Btn_Exit.Tag = "Exit";
			this.Btn_Exit.Text = "�˳�";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(0, 41);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(472, 268);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "richTextBox1";
			// 
			// Btn_GenerSQL
			// 
			this.Btn_GenerSQL.ImageIndex = 4;
			this.Btn_GenerSQL.Text = "����SQL";
			// 
			// MainFrom
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(472, 309);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.bpToolbar1);
			this.MaximizeBox = false;
			this.Name = "MainFrom";
			this.Text = "�����齨";
			this.Load += new System.EventHandler(this.MainFrom_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void MainFrom_Load(object sender, System.EventArgs e)
		{
			this.Text = SystemConfig.SysName + "--����̨";
			//this.richTextBox1.Text="����;\n   �˿���̨��������,���̹�ϵά�������ݶ�ʱ���뵼���Լ����뵼���Ķ�ʱ���á�\n �˹���������ɸ���ˮ��˰ʹ�á� 2004-09-18 \n"; 
			this.richTextBox1.Text="�û���֪\n\n";
			this.richTextBox1.Text+="    �˿���̨��������,���̹�ϵά�������ݶ�ʱ���뵼���Լ����뵼���Ķ�ʱ���á��Լ�������Ŀά����\n";
			this.richTextBox1.Text+="    �˿���̨����,�ṩ��ϵͳ����Ա������ά����Աʹ�ã� ���� b/s �����һ������,�����b/s ��Ч�����⡣\n"; 
			this.richTextBox1.Text+="    ��Ӧ�ó���,��ccflow��������鿪�������["+BP.SystemConfig.CustomerName+"]ʹ�á�\n\n" ; 
			this.richTextBox1.Text+="ccflow���������\n";
			this.richTextBox1.Text+="    ��������ҳ: http://www.chichengsoft.com\n";
			this.richTextBox1.Text+="    ����֧���ĵ�:http://helper.chichengsoft.net/ E-mail: "+SystemConfig.ServiceMail+"\n";
			this.richTextBox1.Text+="    ����绰:"+SystemConfig.ServiceTel+"\n" ; 
			this.richTextBox1.Text+="    ��������:2004��9��18��\n" ;
 
			bgPath = Application.StartupPath+"\\back.bin";
		}

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(e.Button.Text)
			{
				case "����SQL":
					
					//this.Information(
					break;
				case "ѡ��":
					UAC uac = new UAC();
					uac.IsAdjunct=true;
					uac.IsDelete=true;
					uac.IsInsert=true;
					uac.IsUpdate=true;
					uac.IsView=true;
					this.InvokUIEns(new Sys.SysConfigs(),uac);
					break;
				case "�޸�����":
					BP.Win32.Controls.Comm.FrmChangePass cps= new BP.Win32.Controls.Comm.FrmChangePass();
					cps.Show();					
					break;
				case "�˳�":
					this.Close();
					break;
				case "����":
					this.InvokUIDTS();
					break;
				case "Adminά��":
					this.InvokUIUserEns(EnType.Admin);
					break;
				case "�û���ά��":
					this.InvokUIUserEns(EnType.PowerAble);
					break;
				case "Ȩ�޹���":
					Entities ens = BP.DA.ClassFactory.GetEns(e.Button.Tag.ToString())  ; 
					InvokUIEns(ens,this);
					break;
				default:
					break;
			}
		}
	}
}

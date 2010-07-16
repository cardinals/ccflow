using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.Port;
using BP.Sys;
using BP.CTI.App;
using BP.Win32.Controls;
using BP.DA;

namespace BP.CTI
{
	/// <summary>
	/// FrmCTI ��ժҪ˵����
	/// </summary>
	public class FrmClient : BP.Win32.PageBase
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton Btn_DataBase;
		private System.Windows.Forms.ToolBarButton Btn_File;
		private System.Windows.Forms.ToolBarButton Btn_CTILog;
		private System.Windows.Forms.ToolBarButton Btn_CTITimeSet;
		private System.Windows.Forms.ToolBarButton Btn_Exit;
		private System.Windows.Forms.ToolBarButton Btn_CTIContext;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolBarButton Btn_CTIList;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ToolBarButton Btn_Release;
		private System.Windows.Forms.ToolBarButton Btn_Comm;
		private BP.Win32.Controls.DG dg1;
		private System.Windows.Forms.ToolBarButton Btn_Opention;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolBarButton Btn_Test;
		private System.Windows.Forms.ToolBarButton Btn_Schedule;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.ToolBarButton Btn_CurCall;
		private System.ComponentModel.IContainer components;

		public FrmClient()
		{
		
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;


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
		public void SetDG()
		{
		//	System.Drawing.Image

			//this.groupBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox4.BackgroundImage")));

			//this.groupBox4.BackgroundImage="";
		}


		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmClient));
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_Schedule = new System.Windows.Forms.ToolBarButton();
			this.Btn_DataBase = new System.Windows.Forms.ToolBarButton();
			this.Btn_File = new System.Windows.Forms.ToolBarButton();
			this.Btn_CTITimeSet = new System.Windows.Forms.ToolBarButton();
			this.Btn_CTIContext = new System.Windows.Forms.ToolBarButton();
			this.Btn_CTIList = new System.Windows.Forms.ToolBarButton();
			this.Btn_Release = new System.Windows.Forms.ToolBarButton();
			this.Btn_CTILog = new System.Windows.Forms.ToolBarButton();
			this.Btn_Comm = new System.Windows.Forms.ToolBarButton();
			this.Btn_Opention = new System.Windows.Forms.ToolBarButton();
			this.Btn_Test = new System.Windows.Forms.ToolBarButton();
			this.Btn_Exit = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.dg1 = new BP.Win32.Controls.DG();
			this.panel1 = new System.Windows.Forms.Panel();
			this.Btn_CurCall = new System.Windows.Forms.ToolBarButton();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.Btn_Schedule,
																						  this.Btn_DataBase,
																						  this.Btn_File,
																						  this.Btn_CTITimeSet,
																						  this.Btn_CTIContext,
																						  this.Btn_CTIList,
																						  this.Btn_Release,
																						  this.Btn_CTILog,
																						  this.Btn_CurCall,
																						  this.Btn_Comm,
																						  this.Btn_Opention,
																						  this.Btn_Test,
																						  this.Btn_Exit});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(678, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// Btn_Schedule
			// 
			this.Btn_Schedule.ImageIndex = 14;
			this.Btn_Schedule.Text = "�����";
			// 
			// Btn_DataBase
			// 
			this.Btn_DataBase.ImageIndex = 0;
			this.Btn_DataBase.Text = "���ݿ����";
			this.Btn_DataBase.Visible = false;
			// 
			// Btn_File
			// 
			this.Btn_File.ImageIndex = 0;
			this.Btn_File.Text = "�����ļ�����";
			this.Btn_File.Visible = false;
			// 
			// Btn_CTITimeSet
			// 
			this.Btn_CTITimeSet.ImageIndex = 0;
			this.Btn_CTITimeSet.Text = "�û���������";
			// 
			// Btn_CTIContext
			// 
			this.Btn_CTIContext.ImageIndex = 5;
			this.Btn_CTIContext.Text = "������������";
			this.Btn_CTIContext.ToolTipText = "������������";
			// 
			// Btn_CTIList
			// 
			this.Btn_CTIList.ImageIndex = 3;
			this.Btn_CTIList.Text = "�����б�";
			this.Btn_CTIList.ToolTipText = "��Ҫ�������б�";
			// 
			// Btn_Release
			// 
			this.Btn_Release.ImageIndex = 3;
			this.Btn_Release.Text = "����б�";
			this.Btn_Release.ToolTipText = "����б�";
			// 
			// Btn_CTILog
			// 
			this.Btn_CTILog.ImageIndex = 1;
			this.Btn_CTILog.Text = "����ͳ��";
			// 
			// Btn_Comm
			// 
			this.Btn_Comm.ImageIndex = 4;
			this.Btn_Comm.Text = "�������";
			// 
			// Btn_Opention
			// 
			this.Btn_Opention.ImageIndex = 2;
			this.Btn_Opention.Text = "ϵͳ����";
			// 
			// Btn_Test
			// 
			this.Btn_Test.ImageIndex = 13;
			this.Btn_Test.Text = "��������";
			// 
			// Btn_Exit
			// 
			this.Btn_Exit.ImageIndex = 6;
			this.Btn_Exit.Text = "�˳�";
			this.Btn_Exit.ToolTipText = "�˳�";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "�ı��ļ�(*.txt)|*.txt|���ӱ��(*.xls)|*.xls";
			this.openFileDialog1.Title = "����ѡ��Ҫ����������ļ�";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 365);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(678, 22);
			this.statusBar1.TabIndex = 3;
			this.statusBar1.Text = "��л��ѡ��۳����";
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.Text = "��л��ѡ��۳���� www.chichengsoft.com";
			this.statusBarPanel1.Width = 500;
			// 
			// dg1
			// 
			this.dg1.DataMember = "";
			this.dg1.DGModel = BP.Win32.Controls.DGModel.None;
			this.dg1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dg1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dg1.IsDGReadonly = false;
			this.dg1.Location = new System.Drawing.Point(3, 17);
			this.dg1.Name = "dg1";
			this.dg1.Size = new System.Drawing.Size(592, 304);
			this.dg1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 41);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(678, 324);
			this.panel1.TabIndex = 4;
			// 
			// Btn_CurCall
			// 
			this.Btn_CurCall.ImageIndex = 3;
			this.Btn_CurCall.Text = "��ǰ����";
			this.Btn_CurCall.ToolTipText = "��ǰʱ��ĺ���";
			// 
			// FrmClient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(678, 387);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.bpToolbar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FrmClient";
			this.Text = "�������п���̨";
			this.Load += new System.EventHandler(this.FrmCTI_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmCTI_Load(object sender, System.EventArgs e)
		{
			//this.ddl1.BindEnum("CTIGetDataWay");			
			//this.comboBox1.Items.Clear();
			this.SetState();				
			this.Text="�۳����CTIϵͳ����̨";
			this.SetDG();
		}

		 
		 
		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			switch( e.Button.Text )
			{
					
				case "��ǰ����":
					FrmCurrCall ft1   = new FrmCurrCall();
					ft1.Show();
					break;
				case "�����":
					Schedules schs = new  Schedules();
					this.InvokUIEns(schs,this);
					break;
				case "��������":
					FrmTest ft   = new FrmTest();
					ft.Show();
					break;
				case "����б�":
					CallReleases reles = new  CallReleases();
					this.InvokUIEns(reles,this);
					break;
				case "ϵͳ����":
					SysConfigs cfgs = new  SysConfigs();
					this.InvokUIEns(cfgs,this);
					break;
				case "��������":
					this.openFileDialog1.DefaultExt="*.txt";
					this.openFileDialog1.ShowDialog();
					break;
				case "�����б�":
					CallLists lists = new  CallLists();
					this.InvokUIEns(lists,this);
					break;
				case "����ͳ��":
					CallStats logs = new CallStats();
					this.InvokUIEns(logs,this);
					break;
				case "�û���������":
					TelTypes types = new  TelTypes();
					this.InvokUIEns(types,this);
					break;
				case "������������":
					CallContexts cont = new  CallContexts();
					this.InvokUIEns(cont,this);
					break;
				case "�������":
					BP.Admin.MainFrom mf = new BP.Admin.MainFrom();
					mf.Show();
					break;
				case "�˳�":
					this.Close();
					break;
			}
			this.Cursor = Cursors.Default;
			Application.DoEvents();
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.SetDG();
		}
		public void SetState()
		{}
		private void button_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Button btn =(Button)sender;
			SysConfig sc = new SysConfig();
			switch(btn.Text)
			{
				case "�ѻ�":
					sc = new SysConfig("DataBaseWorkWay",0);
					sc.Val="0";
					sc.Update();
					this.SetState();
					break;
				case "����":
					sc = new SysConfig("DataBaseWorkWay",0);
					sc.Val="1";
					sc.Update();
					this.SetState();
					break;
				case "��ͣ":
					this.SetState();
					Card.Pause();
					break;
				case "ֹͣ":
					this.SetState();
					Card.Stop();					
					break;
				case "��ʼ":
					//Card.DoPause();
					this.SetState();
					break;				 
				default:
					throw new Exception("id error"+btn.Name);
			}
		}

		private void btn_Close_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//BP.CTI.Card.DoCall();
			this.ResponseWriteBlueMsg("ִ�����");
		}

		private void groupBox3_Enter(object sender, System.EventArgs e)
		{
		
		}
		 
		private void timer2_Tick(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
//			string str="custom00.TW,custom01.TW";
//
//
//			string[] fls =str.Split(',');
//
//			byte[] by =DataType.StringToByte( fls );
//
//			this.ResponseWriteBlueMsg( fls.ToString() );

			



			//this.ResponseWriteBlueMsg(DataType.TurnToFiels(float.Parse(this.textBox1.Text) ));
		}

		private void groupBox4_Enter(object sender, System.EventArgs e)
		{
		
		}

		 
	}
}

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.CTI.App;
using BP.Sys;

namespace BP.CTI
{
	/// <summary>
	/// FrmMain ��ժҪ˵����
	/// </summary>
	public class FrmMain : BP.Win32.PageBase
	{
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton Btn_SRV;
		private System.Windows.Forms.ToolBarButton Btn_Cont;
		private System.Windows.Forms.ToolBarButton Btn_Worker;
		private System.Windows.Forms.ToolBarButton Btn_Schouble;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton Btn_Client;
		private System.Windows.Forms.ToolBarButton Btn_Context;
		private System.Windows.Forms.ToolBarButton Btn_Opention;
		private System.Windows.Forms.ToolBarButton Btn_Test;
		private System.Windows.Forms.ToolBarButton Btn_Help;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.ToolBarButton Btn_Schoudle;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem menuItem31;
		private System.Windows.Forms.MenuItem menuItem32;
		private System.Windows.Forms.MenuItem menuItem35;
		private System.Windows.Forms.MenuItem menuItem34;
		private System.Windows.Forms.MenuItem menuItem36;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem37;
		private System.Windows.Forms.MenuItem menuItem38;
		private System.Windows.Forms.MenuItem menuItem40;
		private System.Windows.Forms.MenuItem menuItem41;
		private System.Windows.Forms.MenuItem menuItem42;
		private System.Windows.Forms.MenuItem menuItem43;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ToolBarButton Btn_Min;
		private System.Windows.Forms.MenuItem menuItem44;
		private System.Windows.Forms.MenuItem menuItem45;
		private System.Windows.Forms.MenuItem menuItem46;
		private System.Windows.Forms.MenuItem menuItem33;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem39;
		private System.Windows.Forms.MenuItem menuItem47;
		private System.Windows.Forms.MenuItem menuItem48;
		private System.Windows.Forms.MenuItem menuItem49;
		private System.ComponentModel.IContainer components;

		public FrmMain()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmMain));
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem43 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem23 = new System.Windows.Forms.MenuItem();
			this.menuItem35 = new System.Windows.Forms.MenuItem();
			this.menuItem38 = new System.Windows.Forms.MenuItem();
			this.menuItem33 = new System.Windows.Forms.MenuItem();
			this.menuItem47 = new System.Windows.Forms.MenuItem();
			this.menuItem48 = new System.Windows.Forms.MenuItem();
			this.menuItem49 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.menuItem42 = new System.Windows.Forms.MenuItem();
			this.menuItem25 = new System.Windows.Forms.MenuItem();
			this.menuItem26 = new System.Windows.Forms.MenuItem();
			this.menuItem41 = new System.Windows.Forms.MenuItem();
			this.menuItem22 = new System.Windows.Forms.MenuItem();
			this.menuItem46 = new System.Windows.Forms.MenuItem();
			this.menuItem30 = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.menuItem21 = new System.Windows.Forms.MenuItem();
			this.menuItem24 = new System.Windows.Forms.MenuItem();
			this.menuItem27 = new System.Windows.Forms.MenuItem();
			this.menuItem37 = new System.Windows.Forms.MenuItem();
			this.menuItem28 = new System.Windows.Forms.MenuItem();
			this.menuItem29 = new System.Windows.Forms.MenuItem();
			this.menuItem31 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem32 = new System.Windows.Forms.MenuItem();
			this.menuItem45 = new System.Windows.Forms.MenuItem();
			this.menuItem39 = new System.Windows.Forms.MenuItem();
			this.menuItem44 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem40 = new System.Windows.Forms.MenuItem();
			this.menuItem34 = new System.Windows.Forms.MenuItem();
			this.menuItem36 = new System.Windows.Forms.MenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.Btn_SRV = new System.Windows.Forms.ToolBarButton();
			this.Btn_Cont = new System.Windows.Forms.ToolBarButton();
			this.Btn_Worker = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.Btn_Schouble = new System.Windows.Forms.ToolBarButton();
			this.Btn_Client = new System.Windows.Forms.ToolBarButton();
			this.Btn_Schoudle = new System.Windows.Forms.ToolBarButton();
			this.Btn_Context = new System.Windows.Forms.ToolBarButton();
			this.Btn_Opention = new System.Windows.Forms.ToolBarButton();
			this.Btn_Test = new System.Windows.Forms.ToolBarButton();
			this.Btn_Help = new System.Windows.Forms.ToolBarButton();
			this.Btn_Min = new System.Windows.Forms.ToolBarButton();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.SuspendLayout();
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 355);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(712, 22);
			this.statusBar1.TabIndex = 0;
			this.statusBar1.Text = "statusBar1";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem4,
																					  this.menuItem5,
																					  this.menuItem20,
																					  this.menuItem28,
																					  this.menuItem9,
																					  this.menuItem6});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem43});
			this.menuItem1.Text = "�ļ�";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "�޸�����";
			this.menuItem2.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem43
			// 
			this.menuItem43.Index = 1;
			this.menuItem43.Text = "�˳�";
			this.menuItem43.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem15,
																					  this.menuItem23,
																					  this.menuItem35,
																					  this.menuItem38,
																					  this.menuItem33,
																					  this.menuItem47,
																					  this.menuItem48,
																					  this.menuItem49});
			this.menuItem4.Text = "����������";
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 0;
			this.menuItem15.Text = "���������";
			this.menuItem15.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem23
			// 
			this.menuItem23.Index = 1;
			this.menuItem23.Text = "��������";
			this.menuItem23.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem35
			// 
			this.menuItem35.Index = 2;
			this.menuItem35.Text = "����״̬";
			this.menuItem35.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem38
			// 
			this.menuItem38.Index = 3;
			this.menuItem38.Text = "Ӳ����Ϣ";
			this.menuItem38.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem33
			// 
			this.menuItem33.Index = 4;
			this.menuItem33.Text = "��ϵͳ��־";
			this.menuItem33.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem47
			// 
			this.menuItem47.Index = 5;
			this.menuItem47.Text = "-";
			// 
			// menuItem48
			// 
			this.menuItem48.Index = 6;
			this.menuItem48.Text = "Ӳ��������";
			this.menuItem48.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem49
			// 
			this.menuItem49.Index = 7;
			this.menuItem49.Text = "Ӳ����ʾ����";
			this.menuItem49.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem16,
																					  this.menuItem17,
																					  this.menuItem18,
																					  this.menuItem42,
																					  this.menuItem25,
																					  this.menuItem26,
																					  this.menuItem41,
																					  this.menuItem22,
																					  this.menuItem46,
																					  this.menuItem30,
																					  this.menuItem19});
			this.menuItem5.Text = "���п���";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 0;
			this.menuItem16.Text = "�û���������";
			this.menuItem16.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 1;
			this.menuItem17.Text = "������������";
			this.menuItem17.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 2;
			this.menuItem18.Text = "ʱ���";
			this.menuItem18.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem42
			// 
			this.menuItem42.Index = 3;
			this.menuItem42.Text = "-";
			// 
			// menuItem25
			// 
			this.menuItem25.Index = 4;
			this.menuItem25.Text = "����б�";
			this.menuItem25.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem26
			// 
			this.menuItem26.Index = 5;
			this.menuItem26.Text = "�绰���� ";
			this.menuItem26.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem41
			// 
			this.menuItem41.Index = 6;
			this.menuItem41.Text = "-";
			// 
			// menuItem22
			// 
			this.menuItem22.Index = 7;
			this.menuItem22.Text = "�����б�";
			this.menuItem22.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem46
			// 
			this.menuItem46.Index = 8;
			this.menuItem46.Text = "�򿪺������ݵ���ģ��";
			this.menuItem46.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem30
			// 
			this.menuItem30.Index = 9;
			this.menuItem30.Text = "-";
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 10;
			this.menuItem19.Text = "ѡ��";
			this.menuItem19.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 3;
			this.menuItem20.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem21,
																					   this.menuItem24,
																					   this.menuItem27,
																					   this.menuItem37});
			this.menuItem20.Text = "������ѯ";
			this.menuItem20.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem21
			// 
			this.menuItem21.Index = 0;
			this.menuItem21.Text = "����ͳ��";
			this.menuItem21.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem24
			// 
			this.menuItem24.Index = 1;
			this.menuItem24.Text = "��ǰ�����绰";
			this.menuItem24.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem27
			// 
			this.menuItem27.Index = 2;
			this.menuItem27.Text = "��ǰ��������";
			this.menuItem27.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem37
			// 
			this.menuItem37.Index = 3;
			this.menuItem37.Text = "��ǰ����";
			this.menuItem37.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem28
			// 
			this.menuItem28.Index = 4;
			this.menuItem28.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem29,
																					   this.menuItem31,
																					   this.menuItem3,
																					   this.menuItem32,
																					   this.menuItem45,
																					   this.menuItem39,
																					   this.menuItem44});
			this.menuItem28.Text = "����";
			// 
			// menuItem29
			// 
			this.menuItem29.Index = 0;
			this.menuItem29.Text = "�����ļ�ת������";
			this.menuItem29.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem31
			// 
			this.menuItem31.Index = 1;
			this.menuItem31.Text = "¼����";
			this.menuItem31.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "��˷����";
			this.menuItem3.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem32
			// 
			this.menuItem32.Index = 3;
			this.menuItem32.Text = "��������";
			this.menuItem32.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem45
			// 
			this.menuItem45.Index = 4;
			this.menuItem45.Text = "-";
			// 
			// menuItem39
			// 
			this.menuItem39.Index = 5;
			this.menuItem39.Text = "���ݿ���־���";
			this.menuItem39.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem44
			// 
			this.menuItem44.Index = 6;
			this.menuItem44.Text = "���±�";
			this.menuItem44.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 5;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem10,
																					  this.menuItem11,
																					  this.menuItem12,
																					  this.menuItem13,
																					  this.menuItem14});
			this.menuItem9.Text = "����";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 0;
			this.menuItem10.Text = "���";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.Text = "����ƽ��";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 2;
			this.menuItem12.Text = "����ƽ��";
			this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 3;
			this.menuItem13.Text = "�رյ�ǰ����";
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 4;
			this.menuItem14.Text = "�ر����д���";
			this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 6;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem40,
																					  this.menuItem34,
																					  this.menuItem36});
			this.menuItem6.Text = "����";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 0;
			this.menuItem7.Text = "����";
			this.menuItem7.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 1;
			this.menuItem8.Shortcut = System.Windows.Forms.Shortcut.F1;
			this.menuItem8.Text = "����";
			this.menuItem8.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem40
			// 
			this.menuItem40.Index = 2;
			this.menuItem40.Text = "-";
			// 
			// menuItem34
			// 
			this.menuItem34.Index = 3;
			this.menuItem34.Text = "���߰���";
			this.menuItem34.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// menuItem36
			// 
			this.menuItem36.Index = 4;
			this.menuItem36.Text = "��������";
			this.menuItem36.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.Btn_SRV,
																						this.Btn_Cont,
																						this.Btn_Worker,
																						this.toolBarButton1,
																						this.Btn_Schouble,
																						this.Btn_Client,
																						this.Btn_Schoudle,
																						this.Btn_Context,
																						this.Btn_Opention,
																						this.Btn_Test,
																						this.Btn_Help,
																						this.Btn_Min});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(712, 72);
			this.toolBar1.TabIndex = 2;
			this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// Btn_SRV
			// 
			this.Btn_SRV.ImageIndex = 5;
			this.Btn_SRV.Text = "���������";
			// 
			// Btn_Cont
			// 
			this.Btn_Cont.ImageIndex = 3;
			this.Btn_Cont.Text = "����ͳ��";
			// 
			// Btn_Worker
			// 
			this.Btn_Worker.ImageIndex = 3;
			this.Btn_Worker.Text = "�����б�";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// Btn_Schouble
			// 
			this.Btn_Schouble.ImageIndex = 3;
			this.Btn_Schouble.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.Btn_Schouble.Text = "�����";
			// 
			// Btn_Client
			// 
			this.Btn_Client.ImageIndex = 3;
			this.Btn_Client.Text = "�û���������";
			// 
			// Btn_Schoudle
			// 
			this.Btn_Schoudle.ImageIndex = 14;
			this.Btn_Schoudle.Text = "ʱ���";
			// 
			// Btn_Context
			// 
			this.Btn_Context.ImageIndex = 3;
			this.Btn_Context.Text = "������������";
			// 
			// Btn_Opention
			// 
			this.Btn_Opention.ImageIndex = 3;
			this.Btn_Opention.Text = "ѡ��";
			// 
			// Btn_Test
			// 
			this.Btn_Test.ImageIndex = 7;
			this.Btn_Test.Text = "��������";
			// 
			// Btn_Help
			// 
			this.Btn_Help.ImageIndex = 15;
			this.Btn_Help.Text = "����";
			// 
			// Btn_Min
			// 
			this.Btn_Min.ImageIndex = 8;
			this.Btn_Min.Text = "��С��";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
			// 
			// FrmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(712, 377);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.statusBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "FrmMain";
			this.Text = "FrmMain";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmMain_Closing);
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.Closed += new System.EventHandler(this.FrmMain_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmMain_Load(object sender, System.EventArgs e)
		{
			this.Text=BP.SystemConfig.SysName;
			
			this.statusBar1.Text="��л��ѡ��۳����,ʹ�õ�λ:" +BP.SystemConfig.CustomerName+",��ǰ�汾"+BP.SystemConfig.Ver;

			BP.DTS.SysDTSs.InitDataIOEns();

			this.Do("���������");
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);			 
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			if(this.ActiveMdiChild!=null)
				this.ActiveMdiChild.Close();
		
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			foreach(Form f in this.MdiChildren)
			{
				f.Close();
			}
		}
		private void Do(string text)
		{
			foreach(Form f in this.MdiChildren)
			{
				if (f.Text==text)
				{
					f.Activate();
					return;
				}
			}

			this.Cursor = Cursors.WaitCursor;
			switch( text.Trim() )
			{
				case "Ӳ��������":

					break;
				case "����״̬":
				case "�ڴ����":
					FrmWorkState fcc1 = new FrmWorkState();
					fcc1.Show();
					break;				
				case "�򿪺������ݵ���ģ��":
					this.Information("�����򿪵��ļ���ֻ����,��Ҫ���Ϊ��༭��������.");
					this.RunExeFile("�����б�.xls","ϵͳ�ڵ���[��������]�ڼ���������쳣��ȷ�ϱ����Ƿ�װ");
					break;
				case "���ݿ���־���":
					this.InvokSqlLogClear();
					break;
				case "��ϵͳ��־":
					//this.Information("�����򿪵��ļ���ֻ����,��Ҫ���Ϊ��༭��������.");
					this.RunExeFile("D:\\WebApp\\Log\\DefaultLog\\"+BP.DA.DataType.CurrentData+".log","error");
					break;					
				case "��������":
					this.RunExeFile("sndvol32.exe","ϵͳ�ڵ���[��������]�ڼ���������쳣��ȷ�ϱ����Ƿ�װ");
					break;
				case "��˷����":
					this.RunExeFile("sndrec32.exe","ϵͳ�ڵ���[¼�����ڼ�]�ڼ���������쳣��ȷ�ϱ����Ƿ�װ");
					break;
				case "¼����":
					this.RunExeFile("sndrec32.exe","ϵͳ�ڵ���[¼�����ڼ�]�ڼ���������쳣��ȷ�ϱ����Ƿ�װ");
					break;
				case "���±�":
					this.RunExeFile("notepad.exe","ϵͳ�ڵ��ü��±��ڼ���������쳣��ȷ�ϱ����Ƿ�װ");
					break;
				case "�����ļ�ת������":
					this.RunExeFile(".\\VioceTool\\TW-Wav.exe","ϵͳ�ڵ���[�����ļ�ת������]�ڼ���������쳣,��ȷ���Ƿ�ʧ�ļ�D:\\WebApp\\Components\\BP.CTI\\bin\\Debug\\VioceTool\\TW-Wav.exe");
					break;					
				case "��������":
					this.RunExeFile("http://www.chichengsoft.com/ccs/down/","error");
					break;
				case "���߰���":
					this.RunExeFile("http://www.chichengsoft.com","error");
					break;
				case "����":
				switch(Card.HisCardType)
				{
					case CardType.pcmn7api:
						this.RunExeFile("D:\\WebApp\\Components\\BP.CTI\\�����߽�ϵͳ_��ͨ_�����ֲ�.doc","ϵͳ�ڵ���[�����߽�ϵͳ]�ڼ���������쳣��ȷ�ϱ����Ƿ�װ");
						break;
					case CardType.Usbid:
						this.RunExeFile("D:\\WebApp\\Components\\BP.CTI\\�����߽�ϵͳ_˰��_�����ֲ�.doc","ϵͳ�ڵ���[�����߽�ϵͳ]�ڼ���������쳣��ȷ�ϱ����Ƿ�װ");
						break;
					default:
						break;
				}
					break;
				case "�绰����":
					TelTests tests = new  TelTests();
					this.InvokUIEns(tests,this);
					break;
				case "�޸�����":
					BP.Win32.Controls.Comm.FrmChangePass pass = new BP.Win32.Controls.Comm.FrmChangePass(); 
					pass.ShowDialog();
					//pass.MdiParent=this;
					//pass.Show();
					break;
				case "���������":
					//if (this.MdiChildren
					FrmSRV srv = new FrmSRV();	
					srv.ShowInTaskbar=false;
					//srv.Parent = this;
					srv.MdiParent=this;
					srv.Show();
					break;
				case "��ǰ��������":
					FrmCurrCallContext fcc   = new FrmCurrCallContext();
					fcc.MdiParent=this;
					fcc.Show();
					break;
				case "��ǰ�����绰":
					FrmCurrCall ft1   = new FrmCurrCall();
					ft1.MdiParent=this;
					ft1.Show();
					break;
				case "ʱ���":
					Schedules schs = new  Schedules();
					this.InvokUIEns(schs,this);
					break;
				case "��������":
					//					if ( Card.Serial==null)
					//					{
					//						this.Information("���������е��ǿͻ�������������������ԡ�");
					//						break;
					//					}

					if (Card.HisCardWorkState !=CardWorkState.Disable)
					{
						this.Information("�忨������״̬�����ܲ�����������");
						break;
					}
					FrmTest ft   = new FrmTest();
					ft.MdiParent = this;
					ft.Show();
					break;
				case "����б�":
					CallReleases reles = new  CallReleases();
					this.InvokUIEns(reles,this);
					break;
				case "ѡ��":
					SysConfigs cfgs = new  SysConfigs();
					this.InvokUIEns(cfgs,this);
					break;
				case "��������":
					//this.openFileDialog1.DefaultExt="*.txt";
					//this.openFileDialog1.ShowDialog();
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
					if (this.Warning("��ȷ��Ҫ�˳�ϵͳ��? \n\n�˳�ϵͳ֮ǰһ��Ҫ����ֹͣ���з���"))
						this.Close();
					break;
				case "��ǰ����":					 
					FrmCurrDTS fcdts= new FrmCurrDTS();
					fcdts.MdiParent =this;					
					fcdts.Show();
					//BP.Win32.Controls.Comm.About ab = new BP.Win32.Controls.Comm.About();
					//ab.ShowDialog();
					//this.Close();
					break;		
				case "Ӳ����ʾ����":
				case "Ӳ����Ϣ":
					BP.CTI.FrmCardInfo card = new FrmCardInfo(); // ab = new BP.Win32.Controls.Comm.About();
					card.ShowDialog();
					break;
				case "��С��":					
					this.ShowInTaskbar=false;
					this.notifyIcon1.Icon=this.Icon;
					this.notifyIcon1.Visible=true;
					this.WindowState=FormWindowState.Minimized; 
					break;					 
				case "����":
					BP.Win32.Controls.Comm.About ab = new BP.Win32.Controls.Comm.About();
					ab.ShowDialog();
					//this.Close();
					break;
				default:
					throw new Exception("error "+text);
			}
			this.Cursor = Cursors.Default;
			Application.DoEvents();

		}

		private void menuItem_Click(object sender, System.EventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			this.Do(item.Text);
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			this.Do(e.Button.Text);
		}

		private void notifyIcon1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;
			this.Activate();
			this.ShowInTaskbar = true ;
			this.Show();
		}

		private void FrmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
//			if (Card.Serial!=null)
//			{
//				if (Card.HisCardWorkState !=CardWorkState.Stop
//					|| Card.HisCardWorkState !=CardWorkState.Disable)
//				{
//				     this.Information("�������л���ͣ״̬���������˳�������");
//					this.Activate();
//					this.Show();
//				}
//			}
		}

		private void FrmMain_Closed(object sender, System.EventArgs e)
		{
			
			this.FrmMain_Closing(null,null);
		}

		 

		 

	 

		 
	}
}

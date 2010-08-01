using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BP.En;
using BP.DA;
using BP.Port;
using BP.Win.WF;
using BP.WF;
using BP.Win.Controls;


namespace BP.WF.Design
{
	//public class MainForm : BP.Win32.PageBase
    public class MainForm : BP.Win32.PageBase
	{
		#region �ؼ�
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ImageList imageList2;
		private BP.Win32.Controls.BPToolbar tBar1;
		private BP.Win.Controls.Pan pan1;
		private System.Windows.Forms.Splitter splitter1;
        public BP.Win.WF.WFToolBar wfToolBar1;
		public System.Windows.Forms.StatusBarPanel sBarPanelUser;
		public System.Windows.Forms.StatusBarPanel sBarPanelRight;
		public System.Windows.Forms.StatusBarPanel sBarPanelLoadTime;
		private BP.Win.Controls.Tree tree1;
		private System.Windows.Forms.ContextMenu NodeCMenu;
		private System.Windows.Forms.MenuItem M_Ref;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem M_Edit;
		private BP.Win.Controls.SBar sBar1;
        private System.Windows.Forms.ImageList imageList3;
        public System.Windows.Forms.StatusBarPanel sBarVersion;
        private System.Windows.Forms.ToolBarButton Btn_Book;
		private System.Windows.Forms.ToolBarButton Btn_Exit;
		private System.Windows.Forms.ToolBarButton Btn_Open;
		private System.Windows.Forms.ToolBarButton Btn_Save;
        private System.Windows.Forms.ToolBarButton Btn_SaveAll;
        private System.Windows.Forms.ToolBarButton Btn_Emp;
        private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.ToolBarButton Btn_DTS;
        private MenuItem M_NewFlowSort;
        private MenuItem M_Del;
        private MenuItem M_NewFlow;
        private MenuItem M_NewFlowByTemplate;
        private MenuItem menuItem6;
        private ImageList imageList4;
        private ToolBarButton toolBarButton1;
        private ToolBarButton Btn_New;
        private ToolBarButton Btn_FLS;
        private ToolBarButton Btn_DeptEdit;
        private ToolBarButton Btn_StationEdit;
        private ToolBarButton Btn_EmpEdit;
        private ToolBarButton Btn_OperVideo;
        private MenuItem miReLoad;
        private MenuItem menuItem26;
        private MenuItem miExit;
        private MenuItem menuItem18;
        private MenuItem miCascade;
        private MenuItem miHorizontal;
        private MenuItem miVertical;
        private MenuItem menuItem9;
        private MenuItem miChangeBack;
        private MenuItem miDeleteBack;
        private MenuItem menuItem11;
        private MenuItem miCloseCurrent;
        private MenuItem miCloseAll;
        private MenuItem menuItem4;
        private MenuItem Btn_OpeVideo;
        private MenuItem Btn_CCS;
        private MenuItem menuItem16;
        private MenuItem menuItem1;
        private MenuItem Btn_About;
        private MenuItem menuItem14;
        private SaveFileDialog saveFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
       
		private System.ComponentModel.IContainer components;
		#endregion �ؼ�

		#region ��ʼ��
		public MainForm()
		{
        
			InitializeComponent();
			bgPath = Application.StartupPath+"\\back.bin";

            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
		}
		public  readonly string bgPath ;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (System.IO.File.Exists(bgPath))
                try
                {
                    System.Drawing.Image img = Bitmap.FromFile(bgPath);
                    this.BackgroundImage = img;
                }
                catch
                {
                    MessageBox.Show("�ļ���ʽ���󣬸��ļ�������ȷ��ͼ���ļ�", "��ʾ");
                }

            //this.fileSystemWatcher1.Path = Path.GetDirectoryName(Global.AppConfigPath);
            //this.fileSystemWatcher1.Filter = Path.GetFileName(Global.AppConfigPath);

            //this.fileSystemWatcher2.Path = Path.GetDirectoryName(Global.RefConfigPath);
            //this.fileSystemWatcher2.Filter = Path.GetFileName(Global.RefConfigPath);

            this.Cursor = Cursors.WaitCursor;
            this.sBarPanelLoadTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd,HH:mm");
            this.sBarVersion.Text =  " ver " + Global.VersionDate;

            TreeNode top = new TreeNode(this.ToE("WorkFlow", "������"), 0, 2);
            this.tree1.Nodes.Add(top);
            try
            {
                this.BindTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }
        public void BindTree()
        {
            this.tree1.Nodes.Clear();

            TreeNode top = new TreeNode(this.ToE("FlowSort","�������"), 0, 2);
            this.tree1.Nodes.Add(top);
           

            FlowSorts sorts = new FlowSorts();
            try
            {
                sorts.RetrieveAllFromDBSource();
            }
            catch
            {
                sorts.RetrieveAllFromDBSource();
            }

            sorts.FlodInCash();


            foreach (FlowSort sort in sorts)
            {
                TreeNode tn = new TreeNode();
                tn.Text = sort.Name;
                tn.Tag = sort.No;
                top.Nodes.Add(tn);

                Flows fls = new Flows(sort.No);
                foreach (Flow fl in fls)
                {
                    TreeNode flowTn = new TreeNode();
                    flowTn.Tag = fl;
                    flowTn.Text = fl.Name;
                    //   flowTn.StateImageIndex = 2;
                    // flowTn.SelectedImageIndex = 3;
                    tn.Nodes.Add(flowTn);
                }
            }

           //this.tree1.AddNodes(tree, top, "No", "Name", 1, 2, true);

            foreach (TreeNode tn in this.tree1.Nodes)
            {
                tn.ExpandAll();
            }

            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }
		#endregion ��ʼ��
		
		#region Windows ������������ɵĴ���
		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
			base.Dispose( disposing );
		}
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tBar1 = new BP.Win32.Controls.BPToolbar();
            this.Btn_New = new System.Windows.Forms.ToolBarButton();
            this.Btn_Save = new System.Windows.Forms.ToolBarButton();
            this.Btn_Open = new System.Windows.Forms.ToolBarButton();
            this.Btn_SaveAll = new System.Windows.Forms.ToolBarButton();
            this.Btn_DeptEdit = new System.Windows.Forms.ToolBarButton();
            this.Btn_StationEdit = new System.Windows.Forms.ToolBarButton();
            this.Btn_EmpEdit = new System.Windows.Forms.ToolBarButton();
            this.Btn_OperVideo = new System.Windows.Forms.ToolBarButton();
            this.Btn_Exit = new System.Windows.Forms.ToolBarButton();
            this.pan1 = new BP.Win.Controls.Pan();
            this.tree1 = new BP.Win.Controls.Tree();
            this.NodeCMenu = new System.Windows.Forms.ContextMenu();
            this.M_NewFlowSort = new System.Windows.Forms.MenuItem();
            this.M_NewFlow = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.M_Edit = new System.Windows.Forms.MenuItem();
            this.M_Del = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.M_Ref = new System.Windows.Forms.MenuItem();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.M_NewFlowByTemplate = new System.Windows.Forms.MenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.sBar1 = new BP.Win.Controls.SBar();
            this.sBarPanelUser = new System.Windows.Forms.StatusBarPanel();
            this.sBarPanelRight = new System.Windows.Forms.StatusBarPanel();
            this.sBarPanelLoadTime = new System.Windows.Forms.StatusBarPanel();
            this.sBarVersion = new System.Windows.Forms.StatusBarPanel();
            this.imageList4 = new System.Windows.Forms.ImageList(this.components);
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.miReLoad = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.miCascade = new System.Windows.Forms.MenuItem();
            this.miHorizontal = new System.Windows.Forms.MenuItem();
            this.miVertical = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.miChangeBack = new System.Windows.Forms.MenuItem();
            this.miDeleteBack = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.miCloseCurrent = new System.Windows.Forms.MenuItem();
            this.miCloseAll = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.Btn_OpeVideo = new System.Windows.Forms.MenuItem();
            this.Btn_CCS = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.Btn_About = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.wfToolBar1 = new BP.Win.WF.WFToolBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pan1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelLoadTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "asf.gif");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            // 
            // tBar1
            // 
            this.tBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tBar1.BackColor = System.Drawing.SystemColors.Control;
            this.tBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.Btn_New,
            this.Btn_Save,
            this.Btn_Open,
            this.Btn_SaveAll,
            this.Btn_DeptEdit,
            this.Btn_StationEdit,
            this.Btn_EmpEdit,
            this.Btn_OperVideo,
            this.Btn_Exit});
            this.tBar1.DropDownArrows = true;
            this.tBar1.ImageList = this.imageList1;
            this.tBar1.Location = new System.Drawing.Point(0, 0);
            this.tBar1.Name = "tBar1";
            this.tBar1.ShowToolTips = true;
            this.tBar1.Size = new System.Drawing.Size(954, 30);
            this.tBar1.TabIndex = 1;
            this.tBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.tBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tBar1_ButtonClick);
            // 
            // Btn_New
            // 
            this.Btn_New.ImageIndex = 0;
            this.Btn_New.Name = "Btn_New";
            this.Btn_New.Text = "�½�����";
            // 
            // Btn_Save
            // 
            this.Btn_Save.ImageIndex = 2;
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Text = "����";
            this.Btn_Save.ToolTipText = "����";
            // 
            // Btn_Open
            // 
            this.Btn_Open.ImageIndex = 1;
            this.Btn_Open.Name = "Btn_Open";
            this.Btn_Open.Text = "��";
            this.Btn_Open.ToolTipText = "��";
            this.Btn_Open.Visible = false;
            // 
            // Btn_SaveAll
            // 
            this.Btn_SaveAll.ImageIndex = 3;
            this.Btn_SaveAll.Name = "Btn_SaveAll";
            this.Btn_SaveAll.Text = "ȫ������";
            this.Btn_SaveAll.ToolTipText = "ȫ������";
            this.Btn_SaveAll.Visible = false;
            // 
            // Btn_DeptEdit
            // 
            this.Btn_DeptEdit.ImageIndex = 11;
            this.Btn_DeptEdit.Name = "Btn_DeptEdit";
            this.Btn_DeptEdit.Text = "����ά��";
            // 
            // Btn_StationEdit
            // 
            this.Btn_StationEdit.ImageIndex = 6;
            this.Btn_StationEdit.Name = "Btn_StationEdit";
            this.Btn_StationEdit.Text = "��λά��";
            this.Btn_StationEdit.ToolTipText = "��λά��";
            // 
            // Btn_EmpEdit
            // 
            this.Btn_EmpEdit.ImageIndex = 10;
            this.Btn_EmpEdit.Name = "Btn_EmpEdit";
            this.Btn_EmpEdit.Text = "������Աά��";
            this.Btn_EmpEdit.ToolTipText = "������Աά��";
            // 
            // Btn_OperVideo
            // 
            this.Btn_OperVideo.ImageIndex = 12;
            this.Btn_OperVideo.Name = "Btn_OperVideo";
            this.Btn_OperVideo.Text = "¼�����";
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.ImageIndex = 8;
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Text = "�˳�";
            this.Btn_Exit.ToolTipText = "�˳�";
            // 
            // pan1
            // 
            this.pan1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pan1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pan1.Controls.Add(this.tree1);
            this.pan1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pan1.Location = new System.Drawing.Point(0, 30);
            this.pan1.Name = "pan1";
            this.pan1.Size = new System.Drawing.Size(200, 362);
            this.pan1.TabIndex = 2;
            // 
            // tree1
            // 
            this.tree1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tree1.ContextMenu = this.NodeCMenu;
            this.tree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree1.HideSelection = false;
            this.tree1.HotTracking = true;
            this.tree1.ImageIndex = 0;
            this.tree1.ImageList = this.imageList3;
            this.tree1.Location = new System.Drawing.Point(0, 0);
            this.tree1.Name = "tree1";
            this.tree1.ReadOnly = false;
            this.tree1.SelectedImageIndex = 2;
            this.tree1.Size = new System.Drawing.Size(196, 358);
            this.tree1.TabIndex = 0;
            this.tree1.DoubleClick += new System.EventHandler(this.tree1_DoubleClick);
            this.tree1.FirstExpand += new BP.Win.Controls.OnFirstExpandEventHandler(this.tree1_FirstExpand);
            // 
            // NodeCMenu
            // 
            this.NodeCMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.M_NewFlowSort,
            this.M_NewFlow,
            this.M_NewFlowByTemplate,
            this.menuItem6,
            this.M_Edit,
            this.M_Del,
            this.menuItem3,
            this.M_Ref});
            // 
            // M_NewFlowSort
            // 
            this.M_NewFlowSort.Index = 0;
            this.M_NewFlowSort.Text = "�½��������";
            this.M_NewFlowSort.Click += new System.EventHandler(this.DoAddFlowSort_Click);
            // 
            // M_NewFlow
            // 
            this.M_NewFlow.Index = 1;
            this.M_NewFlow.Text = "�½�����";
            this.M_NewFlow.Click += new System.EventHandler(this.DoAddFlow_Click);


            // 
            // M_NewFlowByTemplate
            // 
            this.M_NewFlowByTemplate.Index = 2;
            this.M_NewFlowByTemplate.Text = "�½�����(��������ģ��...)";
            this.M_NewFlowByTemplate.Click += new System.EventHandler(this.DoAddFlowByTemplate_Click);

            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.Text = "-";
            // 
            // M_Edit
            // 
            this.M_Edit.DefaultItem = true;
            this.M_Edit.Index = 4;
            this.M_Edit.Text = "�༭";
            this.M_Edit.Click += new System.EventHandler(this.miTreeNodeAttr_Click);
            // 
            // M_Del
            // 
            this.M_Del.Index = 5;
            this.M_Del.Text = "ɾ��";
            this.M_Del.Click += new System.EventHandler(this.DoDeleteFlow_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 6;
            this.menuItem3.Text = "-";
            // 
            // M_Ref
            // 
            this.M_Ref.Index = 7;
            this.M_Ref.Text = "ˢ��";
            this.M_Ref.Click += new System.EventHandler(this.DoRefreshNodes_Click);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "");
            this.imageList3.Images.SetKeyName(1, "");
            this.imageList3.Images.SetKeyName(2, "");
            this.imageList3.Images.SetKeyName(3, "WF.ICO");
          
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 30);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 362);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // menuItem20
            // 
            this.menuItem20.Index = -1;
            this.menuItem20.Text = "����";
            this.menuItem20.Click += new System.EventHandler(this.menuItem_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = -1;
            this.menuItem22.Text = "";
            // 
            // sBar1
            // 
            this.sBar1.Location = new System.Drawing.Point(0, 392);
            this.sBar1.Name = "sBar1";
            this.sBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sBarPanelUser,
            this.sBarPanelRight,
            this.sBarPanelLoadTime,
            this.sBarVersion});
            this.sBar1.ShowPanels = true;
            this.sBar1.Size = new System.Drawing.Size(954, 21);
            this.sBar1.TabIndex = 9;
            this.sBar1.Text = "sBar1";
            // 
            // sBarPanelUser
            // 
            this.sBarPanelUser.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.sBarPanelUser.Name = "sBarPanelUser";
            this.sBarPanelUser.Text = "�û�";
            this.sBarPanelUser.Width = 860;
            // 
            // sBarPanelRight
            // 
            this.sBarPanelRight.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sBarPanelRight.Name = "sBarPanelRight";
            this.sBarPanelRight.Width = 10;
            // 
            // sBarPanelLoadTime
            // 
            this.sBarPanelLoadTime.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sBarPanelLoadTime.Name = "sBarPanelLoadTime";
            this.sBarPanelLoadTime.Width = 10;
            // 
            // sBarVersion
            // 
            this.sBarVersion.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sBarVersion.Name = "sBarVersion";
            this.sBarVersion.Text = "Ver 4.0 ";
            this.sBarVersion.Width = 58;
            // 
            // imageList4
            // 
            this.imageList4.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList4.ImageStream")));
            this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList4.Images.SetKeyName(0, "Delete.gif");
            this.imageList4.Images.SetKeyName(1, "Edit.gif");
            this.imageList4.Images.SetKeyName(2, "New.gif");
            this.imageList4.Images.SetKeyName(3, "New.gif");
            this.imageList4.Images.SetKeyName(4, "Refurbish.gif");
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 4;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // miReLoad
            // 
            this.miReLoad.Index = 0;
            this.miReLoad.Text = "";
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 1;
            this.menuItem26.Text = "";
            // 
            // miExit
            // 
            this.miExit.Index = 2;
            this.miExit.Text = "";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = -1;
            this.menuItem18.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miReLoad,
            this.menuItem26,
            this.miExit});
            this.menuItem18.Text = "System";
            // 
            // miCascade
            // 
            this.miCascade.Index = 0;
            this.miCascade.Text = "WindowsCascade";
            this.miCascade.Click += new System.EventHandler(this.miCascade_Click);
            // 
            // miHorizontal
            // 
            this.miHorizontal.Index = 1;
            this.miHorizontal.Text = "HXPP";
            this.miHorizontal.Click += new System.EventHandler(this.miHorizontal_Click);
            // 
            // miVertical
            // 
            this.miVertical.Index = 2;
            this.miVertical.Text = "ZXPP";
            this.miVertical.Click += new System.EventHandler(this.miVertical_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            this.menuItem9.Text = "-";
            // 
            // miChangeBack
            // 
            this.miChangeBack.Index = 4;
            this.miChangeBack.Text = "";
            // 
            // miDeleteBack
            // 
            this.miDeleteBack.Index = 5;
            this.miDeleteBack.Text = "";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 6;
            this.menuItem11.Text = "-";
            // 
            // miCloseCurrent
            // 
            this.miCloseCurrent.Index = 7;
            this.miCloseCurrent.Text = "CloseCurrWindows";
            this.miCloseCurrent.Click += new System.EventHandler(this.miCloseCurrent_Click);
            // 
            // miCloseAll
            // 
            this.miCloseAll.Index = 8;
            this.miCloseAll.Text = "CloseAllWindows";
            this.miCloseAll.Click += new System.EventHandler(this.miCloseAll_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = -1;
            this.menuItem4.MdiList = true;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miCascade,
            this.miHorizontal,
            this.miVertical,
            this.menuItem9,
            this.miChangeBack,
            this.miDeleteBack,
            this.menuItem11,
            this.miCloseCurrent,
            this.miCloseAll});
            this.menuItem4.Text = "Windows";
            // 
            // Btn_OpeVideo
            // 
            this.Btn_OpeVideo.Index = 0;
            this.Btn_OpeVideo.Text = "OperVideo";
            this.Btn_OpeVideo.Click += new System.EventHandler(this.Menu_Do_Click);
            // 
            // Btn_CCS
            // 
            this.Btn_CCS.Index = -1;
            this.Btn_CCS.Text = "";
            // 
            // menuItem16
            // 
            this.menuItem16.Index = -1;
            this.menuItem16.Text = "";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = -1;
            this.menuItem1.Text = "";
            // 
            // Btn_About
            // 
            this.Btn_About.Index = 1;
            this.Btn_About.Text = "About";
            this.Btn_About.Click += new System.EventHandler(this.Menu_Do_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = -1;
            this.menuItem14.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Btn_OpeVideo,
            this.Btn_About});
            this.menuItem14.Text = "Help";
            // 
            // wfToolBar1
            // 
            this.wfToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.wfToolBar1.AutoSize = false;
            this.wfToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.wfToolBar1.ButtonsCount = 5;
            this.wfToolBar1.ButtonSize = new System.Drawing.Size(22, 22);
            this.wfToolBar1.Divider = false;
            this.wfToolBar1.DropDownArrows = true;
            this.wfToolBar1.ImageList = this.imageList2;
            this.wfToolBar1.Location = new System.Drawing.Point(203, 30);
            this.wfToolBar1.Name = "wfToolBar1";
            this.wfToolBar1.ShowToolTips = true;
            this.wfToolBar1.Size = new System.Drawing.Size(751, 39);
            this.wfToolBar1.TabIndex = 7;
            this.wfToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.wfToolBar1_ButtonClick);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(954, 413);
            this.Controls.Add(this.wfToolBar1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pan1);
            this.Controls.Add(this.tBar1);
            this.Controls.Add(this.sBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pan1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelLoadTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region �¼�����
		private void miExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
        private void tree1_FirstExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                e.Node.Nodes.Clear();
                string no = e.Node.Tag.ToString();
                Flows fs = new Flows(no);
                this.tree1.AddNodes(fs, e.Node, "", "Name", 1, 2, false);
            }
            else //���ڵ�
            {
                this.BindTree();
            }
        }

        private void tree1_DoubleClick(object sender, System.EventArgs e)
        {
            BP.Win.WF.Global.ToolIndex = 0;
            this.OpenFlow();
        }

		private void SaveAll()
		{
			foreach(Form f in this.MdiChildren)
			{
				FrmDesigner ch = f as FrmDesigner;
				if(f!=null)
				{
					try
					{
						ch.Save();
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message );
						ch.Activate();
					}
				}
			}
		}
		private bool SaveAndCloseAll()
		{
			bool all = true;
            foreach (Form f in this.MdiChildren)
            {
                FrmDesigner ch = f as FrmDesigner;
                if (f != null)
                {
                    try
                    {
                        ch.Save();
                        ch.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(f.Text + "��\n" + ex.Message, "����ʱ��������");
                        ch.Activate();
                        all = false;
                        break;
                    }
                }
            }
			return all;
		}
		public void Do(string text)
		{
            switch (text)
            {
                case "�½�����":
                case "�½�":
                case "New":
                case "NewFlow":
                    DoAddFlow_Click(null, null);
                    break;
                case "����Ա":
                case "��Աά��":
                case "�û�ά��":
                case "�û�":
                case "Emp":
                case "EmpEdit":
                    BP.WF.Global.DoEdit(new BP.Port.Emps());
                    break;
                case "��λά��":
                case "StationEdit":
                    BP.WF.Global.DoEdit(new Stations());
                    break;
                case "����":
                case "����ά��":
                case "DeptEdit":
                    BP.WF.Global.DoEdit(new Depts());
                    return;
                case "�������":
                    BP.Admin.MainFrom mf = new BP.Admin.MainFrom();
                    mf.MdiParent = this;
                    mf.Show();
                    break;
                case "Open": // op
                    break;
                case "Open1": // save
                    if (this.tree1.SelectedNode != null)
                        this.tree1.SelectedNode.Expand();
                    this.OpenFlow();
                    break;
                case "����"://saveall
                case "��������"://saveall
                case "Save":
                case "SaveFlow":
                    FrmDesigner design = this.ActiveMdiChild as FrmDesigner;
                    if (design != null)
                        design.Save();
                    break;
                case "ȫ������"://save alll
                case "SaveAll":
                    this.SaveAll();
                    break;
                case "����������": // work para
                case "FlowProperty":
                    FrmDesigner p = this.ActiveMdiChild as FrmDesigner;
                    if (p != null)
                    {
                        p.HisWinFlow.ShowFlowAttr();
                    }
                    break;
                case "�˳�":
                case "Exit":
                    if (this.Question(this.ToE("ExitQuestion","��ȷ��Ҫ�˳���")))
                        this.Close();
                    break;
                case "About":
                case "����":
                case "���ʳ۳����":
                case "CCS":
                    BP.WF.Global.DoIE(SystemConfig.GetValByKey("AboutUrl", "http://ccflow.cn"));
                    break;
                case "����¼��":
                case "OperVideo":
                case "Oper Video":
                    BP.WF.Global.DoIE(SystemConfig.GetValByKey("AboutUrl", "http://ccflow.cn/ftp/flow/demo/"));
                    break;
                default:
                    MessageBox.Show(text);
                    break;
            }
		}
        private void tBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string id = e.Button.Name.Replace("Btn_", "");
            //this.Do(e.Button.Text);
            this.Do(id);
            this.Cursor = Cursors.Default;
            Application.DoEvents();
        }


        private void RefreshsBar()
        {
            this.sBarPanelUser.Text = Global.User;
            this.sBarPanelRight.Text = Global.Right;
            if (msgshow < 1)
                this.sBarVersion.Text = "";
            else
                msgshow--;
        }
		private int  msgshow=5;
		private bool lock_check = false;
		private void CheckVersion() //�������ݿ�
		{
            return;

			if(Global.UpdatePath=="")
				return ;
			string newvertime="";
			try
			{
				newvertime= DA.DBAccess.RunSQLReturnVal("select top 1 * from WF_CSFlowDesigner").ToString().Trim();//fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm");
			}
			catch{}
			if(newvertime=="")
				return;
			lock_check = true;
			if(string.Compare(Global.VersionDate ,newvertime)<0) 
			{
				string str = "��ǰʹ�ð汾�İ汾ʱ��Ϊ��"+Global.VersionDate+"��";
				str += "\n�����°汾������°淢��ʱ��Ϊ��" + newvertime +"��";//tb.Rows[0][0]+"��";
				str += Global.UpdateItems;//������Ŀ
				str += "\n�Ƿ�����ʾ����Y/N��";
				if(MSG.ShowQuestion( "�Ƿ�������������Y/N��","��ʾ")==DialogResult.Yes)
				{
					if(this.SaveAndCloseAll())
					{
						Global.Update();
						return ;
					}
				}
				else if( MSG.ShowQuestion( str ,"������ʾ��")==DialogResult.Yes)
				{
					lock_check = true; //������ʾ����
					return;
				}
			}
			lock_check = false;
		}
		private void miUpdate_Click(object sender, System.EventArgs e)
		{
			this.DirUpdate();
		}
		//ֱ�����ӷ���������Ŀ¼
		public void DirUpdate()
		{
			if( Global.UpdatePath=="")
			{
				MessageBox.Show("����·��[UpdatePath]Ϊ�գ�");
				return;
			}
			string file = Global.UpdatePath + Path.GetFileName( Application.ExecutablePath);
			string newvertime = "";
			FileInfo fi = null ;
			try
			{
				fi=new FileInfo( file );
				newvertime = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm");
			}
			catch(Exception ex)
			{
				MessageBox.Show("���ӷ�����ʧ�ܣ��޷������£�\n"+ ex.Message ,"ʧ�ܣ�");
				return ;
			}
			if(string.Compare(Global.VersionDate ,newvertime)<0) 
			{
				if(MSG.ShowQuestion( "�Ƿ�������������Y/N��","��ʾ")==DialogResult.Yes)
				{
					if( this.ActiveMdiChild ==null)
						Global.Update();
					else if(MSG.ShowQuestion( "�����˳�������ȷ�����Ѿ��������ݣ��Ƿ��������Y/N��","��ʾ")==DialogResult.Yes)
					{
						this.miCloseAll_Click( this ,null );
						Global.Update();
					}
				}
			}
			else
			{
				MessageBox.Show("��ʹ�õ������°汾���汾ʱ��Ϊ��"+Global.VersionDate+"���������⣬����������ϵ��","��ʾ");
			}
		}

        /// <summary>
        /// ˢ�½ڵ㡣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoRefreshNodes_Click(object sender, System.EventArgs e)
        {
            TreeViewCancelEventArgs te = new TreeViewCancelEventArgs(this.tree1.SelectedNode, false, TreeViewAction.ByMouse);
            this.tree1_FirstExpand(this.tree1, te);
        }

		private void miTreeNodeAttr_Click(object sender, System.EventArgs e)
		{
			if(this.tree1.SelectedNodeLevel ==-1)
				return;

			if( this.tree1.SelectedNode.Tag != null)
				this.ShowAttr();

		}
        public void OpenFlow()
        {
            if (this.tree1.SelectedNodeLevel != 2)
                return;

            Flow flow = this.tree1.SelectedNode.Tag as Flow;
            OpenFlow(flow);

        }
        public void OpenFlow(Flow flow)
        {
            if (flow.IsExits == false)
            {
                this.BindTree();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            if (this.FindChildForm(flow.No) == null)
            {
                Nodes nds = new Nodes();
                nds.Retrieve(NodeAttr.FK_Flow, flow.No);
                flow.HisNodes = nds;

                FrmDesigner design = new FrmDesigner();
                design.Name = flow.No;
                design.Text = flow.Name;
                if (flow.HisNodes.Count == 0)
                {
                    if (this.Question("ϵͳû���ҵ������̵Ľڵ�����ݣ����Ƿ�Ҫע������"))
                        this.RegFlow(flow);
                }
                design.BindData(flow);
                ShowChild(design);
            }
            this.Cursor = Cursors.Default;
        }
		public void ShowAttr()
		{
			if( this.tree1.SelectedNodeLevel ==2)
			{
				Flow fs = this.tree1.SelectedNode.Tag as Flow;
				FrmAttr fotm = new FrmAttr();
				if(fotm.ShowAttr( "���������� ["+fs.Name +"]",  fs ,false )==DialogResult.OK)
					this.tree1.SelectedNode.Text = fs.Name;
			}
			else if( this.tree1.SelectedNodeLevel ==1)
			{
				FlowSort fs = new FlowSort( this.tree1.SelectedNode.Tag.ToString() );
				FrmAttr fotm = new FrmAttr();
				if(fotm.ShowAttr( "������������� ["+fs.Name +"]",  fs ,false )==DialogResult.OK)
					this.tree1.SelectedNode.Text = fs.Name;
			}
		}
		#endregion �¼�����
	
		#region ChildForm

		public Form FindChildForm(string name)
		{
			foreach(Form f in this.MdiChildren)
			{
				if(f.Name == name)
				{
					f.Activate();
					return f;
				}
			}
			return null;
		}
		/// <summary>
		/// ��ʾһ��������ƿ�
		/// </summary>
		/// <param name="child">Ҫ��ʾ��from</param>
        public void ShowChild(Form child)
        {
            child.MdiParent = this; // ָ���������Ǳ�����
            child.StartPosition = FormStartPosition.CenterParent; // ָ��λ�á�
            child.Show();
        }

		private void miCascade_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}
		
		private void miHorizontal_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void miVertical_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		private void miDeleteBack_Click(object sender, System.EventArgs e)
		{
			this.DeleteBackgroundImage();
		}
		private void miChangeBack_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg=new OpenFileDialog();
			dlg.Filter="ͼ���ļ� (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|�����ļ� (*.*)|*.*";
			dlg.RestoreDirectory = false;
			if(dlg.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					this.BackgroundImage = Bitmap.FromFile(dlg.FileName);
					//System.IO.File.Copy( dlg.FileName ,bgPath ,true);
					this.BackgroundImage.Save( bgPath );
				}
				catch(Exception ex)
				{//"�ļ���ʽ���󣬸��ļ�������ȷ��ͼ���ļ�\n"
					MessageBox.Show(ex.Message,"��ʾ��");
					return;
				}
			}
			else
			{
				if(this.BackgroundImage!=null && MSG.ShowQuestion("�Ƿ�ȡ��������","")==DialogResult.Yes)
				{
					DeleteBackgroundImage();
				}
			}
		}
		private void DeleteBackgroundImage()
		{
			if(this.BackgroundImage!=null )
			{
				System.Drawing.Image img = this.BackgroundImage;
				this.BackgroundImage = null;
				img.Dispose();
			}
			System.IO.File.Delete( bgPath );
		}

        private void miCloseCurrent_Click(object sender, System.EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

		private void miCloseAll_Click(object sender, System.EventArgs e)
		{
			this.SaveAndCloseAll();
		}
		 
		#endregion 

		#region ϵͳά��

		 

		
		private void miEmpDeptRun()
		{
		 
		}
		 
		private void miReLoad_Click(object sender, System.EventArgs e)
		{
			bool flag = true;
			if( this.MdiChildren.Length>0 )
			{
				flag = MSG.ShowQuestion( "���ر����д��ڣ��Ƿ����(Y/N)��" ,"���µ�½" )==DialogResult.Yes;
			}
			if( flag )
			{
				this.miCloseAll_Click( this,e);
				 
			}
		}
		private void miOtherEdit_Click(object sender, System.EventArgs e)
		{
			//new FrmEditInfo().ShowDialog();
		}
		private void RefreshClass()
		{
			if (Web.WebUser.No!="admin" )
				throw new Exception ("��û�д�Ȩ�ޣ�����");

			this.Cursor = Cursors.WaitCursor;
			this.Cursor = Cursors.Default;
		}
		private void miExecSQL_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			DataTable tb = DA.DBAccess.RunSQLReturnTable( "select sqlText,sqlType,Note,OID from WF_AttemperSql ");
			string filename = "RunSqlLog_"+ System.DateTime.Now.ToString("yyyy-MM") +".log";
			bool first = true;
			string log ="";
			foreach( DataRow row in tb.Rows)
			{
				if( first)
				{
					first = false;
					log = "\r\n***** ***** "+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" Start! ***** ***** ";
					LogBase.WriteLogLine(filename ,log);
				}
				try
				{
					log="";
					int ef = DA.DBAccess.RunSQL( row["sqlText"].ToString() );
					log      ="  **** ִ�С�"+ row["Note"] +"����ϣ�";
					log +="\r\n  **** ��OID["+row["OID"]+"], sqlType["+row["sqlType"] +"], sqlText:"+ row["sqlText"] +"��";
					log +="\r\n  **** ��Ӱ��������"+ef ;
					LogBase.WriteLogLine( filename, log);
					LogBase.WriteLogLine( filename, "");
				}
				catch(Exception ex)
				{
					if(log=="")
					{
						log      ="  **** ִ�С�"+ row["Note"] +"��ʧ�ܣ�";
						log +="\r\n  **** ��OID:"+row["OID"]+", SQL:"+ row["sqlText"] +"��";
						log +="\r\n  **** ������Ϣ��"+ ex.Message ;
						LogBase.WriteLogLine( filename, log);
						LogBase.WriteLogLine( filename, "");

						MessageBox.Show( ex.Message +"\nsql:"+row[0]+"\n"+row[1]+"\n"+row[2],"ִ��Sqlʧ�ܣ�");
					}
					else
					{
						this.Cursor = Cursors.Default;
						MessageBox.Show("����ʧ�ܣ���������־�ļ��޷�д�룡","ʧ��");
						return;
					}
				}
			}
			log = "***** ***** "+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" End! ***** ***** ";
			LogBase.WriteLogLine( filename, log);
			this.Cursor = Cursors.Default;
			MessageBox.Show( "ִ����ϣ������뿴��־�ļ�["+ LogBase.LogPathDir+filename +"]","��ɣ�");
		}
		 
		#endregion ϵͳά��

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.Text = SystemConfig.SysName;
        }

		private void menuItem_Click(object sender, System.EventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			this.Do(item.Text);
		}
        private void DoAddFlowSort_Click(object sender, EventArgs e)
        {
            FlowSort fs = new FlowSort();
            fs.No = fs.GenerNewNo;
            fs.Name = this.ToE("FlowSort", "�������") + fs.No;
            fs.Insert();

            FrmAttr fotm = new FrmAttr();
            if (fotm.ShowAttr("�༭", fs, false) == DialogResult.OK)
            {
            }
            this.BindTree();
            //this.tree1.SelectedNode.Text = fs.Name;
        }

        private void DoAddFlowByTemplate_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            //   this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.DesktopDirectory; // = "C:\FlowTemplate";
            this.folderBrowserDialog1.ShowDialog();

            string path = this.folderBrowserDialog1.SelectedPath;
            if (path == null)
                return;

            try
            {

                Flow myflow = new Flow();
                if (this.tree1.SelectedNodeLevel == 2)
                {
                    Flow fs = this.tree1.SelectedNode.Tag as Flow;
                    myflow.FK_FlowSort = fs.FK_FlowSort;
                }
                else if (this.tree1.SelectedNodeLevel == 1)
                {
                    FlowSort fs = new FlowSort(this.tree1.SelectedNode.Tag.ToString());
                    myflow.FK_FlowSort = fs.No;
                }
                //  myflow.DoNewFlow();
                myflow =  Global.DoLoadFlowTemplate(myflow.FK_FlowSort, path);

                this.BindTree();
                try
                {
                    this.tree1.SelectedNode.Text = myflow.Name;
                }
                catch
                {
                }
                this.OpenFlow(myflow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "@װ������ģ����ִ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DoAddFlow_Click(object sender, EventArgs e)
        {
            Flow myflow = new Flow();
            if (this.tree1.SelectedNodeLevel == 2)
            {
                Flow fs = this.tree1.SelectedNode.Tag as Flow;
                myflow.FK_FlowSort = fs.FK_FlowSort;
            }
            else if (this.tree1.SelectedNodeLevel == 1)
            {
                FlowSort fs = new FlowSort(this.tree1.SelectedNode.Tag.ToString());
                myflow.FK_FlowSort = fs.No;
            }

            myflow.DoNewFlow();

            FrmAttr fotm = new FrmAttr();
            if (fotm.ShowAttr("���������� [" + myflow.Name + "]", myflow, false) != DialogResult.OK)
            {
                myflow.Delete();
                return;
            }

            this.BindTree();
            try
            {
                this.tree1.SelectedNode.Text = myflow.Name;
            }
            catch
            { 
            }
            this.OpenFlow(myflow);
        }
        /// <summary>
        /// ע������
        /// </summary>
        /// <param name="fs"></param>
        public void RegFlow(Flow fs)
        {
            this.Cursor = Cursors.WaitCursor;
            string msg = Node.RegFlow(fs);
            this.Cursor = Cursors.Default;
            MessageBox.Show(msg, "����ע����Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// ˢ������
        /// </summary>
        public void RefFlow()
        {
            Flow fs = this.tree1.SelectedNode.Tag as Flow;

            if (Web.WebUser.No != "admin")
                throw new Exception("��û�д�Ȩ�ޣ�����");

            this.Cursor = Cursors.WaitCursor;
            string str = "InitGlobalWorks";
            this.Cursor = Cursors.Default;
        }
        private void DoDeleteFlow_Click(object sender, EventArgs e)
        {
            if (this.tree1.SelectedNodeLevel == -1)
                return;

            if (this.tree1.SelectedNode.Tag == null)
                return;

            Flow flow = this.tree1.SelectedNode.Tag as Flow;
            if (flow != null)
            {
                if (BP.Win32.PubClass.Question( this.ToE("AYS","��ȷ����") ) == false)
                    return;

                if (BP.Win32.PubClass.Question(this.ToE("AYS","��ȷ����") ) == false)
                    return;
                flow.DoDelete();
            }

            string fk_sort = this.tree1.SelectedNode.Tag as string;
            if (fk_sort != null)
            {

                FlowSort sort = new FlowSort(fk_sort);
                if (BP.Win32.PubClass.Question(this.ToE("AYS","��ȷ����") ) == false)
                    return;

                Flows fls = new Flows(sort.No);
                if (fls.Count > 0)
                {
                    BP.Win32.PubClass.Alert(this.ToE("DelSortErr", "���������������������ɾ����"));
                    return;
                }
                sort.Delete();
            }

            this.BindTree();

        }

        private void wfToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

        }

        private void NodeCMenu_Popup(object sender, EventArgs e)
        {
            Alert(sender.ToString());
        }

        private void mMenu1_Collapse(object sender, EventArgs e)
        {
            //Alert(sender.ToString());
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Do_Click(object sender, EventArgs e)
        {
            MenuItem it = sender as MenuItem;
            this.Do(it.Text);
        }
        
	}
}

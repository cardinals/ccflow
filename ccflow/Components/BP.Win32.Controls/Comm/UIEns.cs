using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port;
using BP.Web; 
using BP.Pub;
using BP.Web.Controls;

namespace BP.Win32.Comm
{
	/// <summary>
	/// UIEns ��ժҪ˵����
	/// </summary>
	public class UIEns : System.Windows.Forms.Form    //  BP.Win32.PageBase
	{
        public Entity HisEn = null;

		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private BP.Win32.Controls.DG dg1;
		public System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.ComponentModel.IContainer components; 
		public System.Windows.Forms.StatusBarPanel sta1;
		private System.Windows.Forms.ToolBarButton Btn_New;
		private System.Windows.Forms.ToolBarButton Btn_Xml;
        private System.Windows.Forms.ToolBarButton Btn_Refurbish;
		private System.Windows.Forms.ToolBarButton Btn_Edit1;
		private System.Windows.Forms.ToolBarButton Btn_Save1;
		private System.Windows.Forms.ToolBarButton Btn_Delete;
		private System.Windows.Forms.ToolBarButton Btn_Execl;
		private System.Windows.Forms.ToolBarButton Btn_Close;
		private System.Windows.Forms.ToolBarButton Btn_Income;
		private System.Windows.Forms.ToolBarButton Btn_DeleteAll;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripComboBox toolStripComboBox1;	
		private System.Windows.Forms.StatusBarPanel sta3;

		private void InitToolBar()
		{
			try
			{
				//this.bpToolbar1.Buttons.Clear();
			}
			catch
			{

			}

			#region tool1
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Card);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.New);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Save);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.SaveAndNew);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.SaveAndClose);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Delete);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Adjunct);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Close);
			#endregion
		}
		public void BindEns(Entities ens, UAC uac,bool IsFirstBind)
		{
			if (IsFirstBind==false)
			{
				this.dg1.DataSource=null;
				this.dg1.DataMember=null;
			}

			this.HisSysEnsUAC = uac;

			this.HisEn =ens.GetNewEntity;
			this.HisEns= ens ;
			this.Text  = this.HisEn.EnDesc;

			#region  ����Ȩ�޿���
			// ����Ȩ�޿��ơ�
            foreach (ToolBarButton btn in this.bpToolbar1.Buttons)
            {
                switch (btn.Text)
                {
                    case "ɾ��":
                    case "���":
                        btn.Visible = uac.IsDelete;
                     //   btn.Visible = uac.IsDelete;
                        continue;
                    case "�༭":
                        btn.Visible = uac.IsDelete;
                        continue;
                    case "�½�":
                        btn.Visible = uac.IsInsert;
                        continue;
                    case "����":
                        btn.Visible = uac.IsUpdate;
                        continue;
                    default:
                        continue;
                }
            }
			#endregion
		
			this.AddSearchControl();
			// this.InitToolBar();			
			this.dg1.BindEnsThisOnly(this.HisEns,!uac.IsUpdate,true);

			btn_SearchClick(null,null);
		}

		/// <summary>
		/// ����ʵ�� 
		/// </summary>
		/// <param name="ens"></param>
		public void BindEns_del(Entities ens, bool IsReadonly,bool IsFirstBind)
		{
			UAC uac = new UAC();
			uac.IsUpdate = !IsReadonly ;
			
			this.HisEn =ens.GetNewEntity;
			this.HisEns= ens ;
			this.Text  = "��л��ѡ��"+SystemConfig.DeveloperShortName+","+this.HisEn.EnDesc ;
			this.AddSearchControl();			
			// this.InitToolBar();
			this.dg1.BindEnsThisOnly(this.HisEns,IsReadonly,IsFirstBind);
		}

		#region ���캯��
		public UIEns()
		{

			InitializeComponent();

		}
	 
		#endregion ���캯��

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			this.sta3.Text = "��ǰʱ��:" + System.DateTime.Now.ToString("yyyy��M��d��,HH:mm");
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

		#region �Զ����ӿؼ�
		/// <summary>
		/// �����ѯ���ܡ�
		/// </summary>
		public void AddSearchControl()
		{
            this.InitByMap(this.HisEns.GetNewEntity.EnMap);
            return;

			
            //#region �����ѯ����
            ////			this.gb1.Controls.Clear();

		 

            //int width=100;
            //int y=10;
            //int x=1;
            //int hight=22;
            //// lab��ʾ�ı�
            //Lab lab = new Lab();
            //lab.Text="����ؼ���:";
            //lab.Name="Lab_Search";		 
            //lab.Location =new Point( x+5+10,y) ; 
            //lab.Size= new Size(80,hight-10);
            //lab.Visible=true;
            //this.gb1.Controls.Add(lab);

            //// TBʽ��ѯ����			
            //TB tb = new TB();			
            ////tb.Text="����ؼ���:";
            //tb.Name="TB_Search";
            //x+=100;
            //tb.Location = new Point( x,y );
            //tb.Size= new Size(width,hight);
            //tb.Visible=true;
            //this.gb1.Controls.Add(tb);

            ////int OffsetH = 0; //ƫ����

            //#region �������DLLʽ��ѯ����
            //Map map =this.HisEn.EnMap;
            //foreach(Attr attr in map.SearchAttrs)
            //{
            //    if (attr.MyFieldType==FieldType.RefText)				 
            //        continue;
				 
            //    DDL ddl = new DDL();
            //    ddl.AddAllLocation = BP.Web.Controls.AddAllLocation.TopAndEnd ;
            //    if (attr.MyFieldType==FieldType.PKFK || attr.MyFieldType==FieldType.FK) 
            //    {
					 
            //        Entities myens =DA.ClassFactory.GetEns(attr.UIBindKey) ; 
            //        myens.RetrieveAll();
            //        ddl.BindEns( myens,attr.UIRefKeyText , attr.UIRefKeyValue );
				
            //    }
            //    else if (attr.MyFieldType==FieldType.PKEnum || attr.MyFieldType==FieldType.Enum ) 
            //    {
            //        SysEnums enums = new SysEnums(attr.UIBindKey) ;				
            //        ddl.BindEns(enums);
            //    }
            //    else
            //    {
            //        throw new Exception("@����"+attr.Key+" ����ö�����ͣ�Ҳ������� ������Ϊ��ѯ���ԡ�");
            //    }
            //    x += 100;
            //    ddl.Location = new Point(x ,y ) ; 
            //    ddl.Size= new Size(width,hight);
            //    ddl.Visible=true;
            //    ddl.Name="DDL_"+attr.Key;
            //    this.gb1.Controls.Add(ddl);
            //}
            //#endregion ѭ������DLLʽ��ѯ����

            //// ��ѯ��ť 
            //Btn btn = new Btn();
            //x += 100;
            //btn.Location =  new Point( x, y);
            //btn.Size=new Size(width,hight);
            //btn.Name="Btn_Search";
            //btn.Text="��ѯ(&F)";
            //btn.Click+=new EventHandler(btn_SearchClick);
            //this.gb1.Controls.Add(btn);

            //this.gb1.Width=x+100;
            //this.gb1.Text="��ѯ";

            //#endregion
		}

		/// <summary>
		/// �Զ������ؼ�
		/// </summary>
		public void AutoAddControl_old()
		{
			#region �����ѯ����
			//			this.gb1.Controls.Clear();

			// lab��ʾ�ı�			
			Lab lab = new Lab();			
			lab.Text="�������������";
			lab.Name="lab_search";		 
			lab.Location =new Point( 45,52 )  ; 
			lab.Size= new Size(100,22);
			lab.Visible=true;
			this.Controls.Add(lab);

			// TBʽ��ѯ����			
			TB tb = new TB();			
			tb.Text="����ؼ���";
			tb.Name="TB_Search";			 
			tb.Location = new Point( 145,50 ) ; 
			tb.Size= new Size(100,22);
			tb.Visible=true;
			this.Controls.Add(tb);

			int OffsetH = 0; //ƫ����

			#region �������DLLʽ��ѯ����
			Map map =this.HisEn.EnMap;
			foreach(Attr attr in map.SearchAttrs)
			{
				if (attr.MyFieldType==FieldType.RefText)				 
					continue;
				 
				DDL ddl = new DDL();
				ddl.AddAllLocation = BP.Web.Controls.AddAllLocation.TopAndEnd ;
				if (attr.MyFieldType==FieldType.PKFK || attr.MyFieldType==FieldType.FK) 
				{
					Entities myens =DA.ClassFactory.GetEns(attr.UIBindKey) ; 
					myens.RetrieveAll();
					ddl.BindEns( myens,attr.UIRefKeyText , attr.UIRefKeyValue );
				}
				else if (attr.MyFieldType==FieldType.PKEnum || attr.MyFieldType==FieldType.Enum ) 
				{
					SysEnums enums = new SysEnums(attr.UIBindKey) ;				
					ddl.BindEns(enums);
				}
				else
				{
					throw new Exception("@����"+attr.Key+" ����ö�����ͣ�Ҳ������� ������Ϊ��ѯ���ԡ�");
				}			
				
				ddl.Location = new Point(OffsetH+250,50 ) ; 
				ddl.Size= new Size(100,22);
				ddl.Visible=true;
				ddl.Name="DDL_"+attr.Key;
				this.Controls.Add(ddl);
				OffsetH += 100;
			}
			#endregion ѭ������DLLʽ��ѯ����
			// ��ѯ��ť 
			Btn btn = new Btn();
			btn.Location =  new Point( OffsetH+250, 50 );
			btn.Size=new Size(60,22);
			btn.Name="Btn_Search";			
			btn.Text="��ѯ(&F)";
			btn.Click+=new EventHandler(btn_SearchClick);
			this.Controls.Add(btn);

			#endregion
		}
		#endregion �Զ����ӿؼ�

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIEns));
            this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
            this.Btn_New = new System.Windows.Forms.ToolBarButton();
            this.Btn_Edit1 = new System.Windows.Forms.ToolBarButton();
            this.Btn_Save1 = new System.Windows.Forms.ToolBarButton();
            this.Btn_Delete = new System.Windows.Forms.ToolBarButton();
            this.Btn_DeleteAll = new System.Windows.Forms.ToolBarButton();
            this.Btn_Refurbish = new System.Windows.Forms.ToolBarButton();
            this.Btn_Execl = new System.Windows.Forms.ToolBarButton();
            this.Btn_Xml = new System.Windows.Forms.ToolBarButton();
            this.Btn_Income = new System.Windows.Forms.ToolBarButton();
            this.Btn_Close = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dg1 = new BP.Win32.Controls.DG();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.sta1 = new System.Windows.Forms.StatusBarPanel();
            this.sta3 = new System.Windows.Forms.StatusBarPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta3)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bpToolbar1
            // 
            this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.Btn_New,
            this.Btn_Edit1,
            this.Btn_Save1,
            this.Btn_Delete,
            this.Btn_DeleteAll,
            this.Btn_Refurbish,
            this.Btn_Execl,
            this.Btn_Xml,
            this.Btn_Income,
            this.Btn_Close});
            this.bpToolbar1.DropDownArrows = true;
            this.bpToolbar1.ImageList = this.imageList1;
            this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
            this.bpToolbar1.Name = "bpToolbar1";
            this.bpToolbar1.ShowToolTips = true;
            this.bpToolbar1.Size = new System.Drawing.Size(774, 28);
            this.bpToolbar1.TabIndex = 0;
            this.bpToolbar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
            // 
            // Btn_New
            // 
            this.Btn_New.ImageIndex = 0;
            this.Btn_New.Name = "Btn_New";
            this.Btn_New.Tag = "New";
            this.Btn_New.Text = "�½�";
            // 
            // Btn_Edit1
            // 
            this.Btn_Edit1.ImageIndex = 1;
            this.Btn_Edit1.Name = "Btn_Edit1";
            this.Btn_Edit1.Tag = "Edit";
            this.Btn_Edit1.Text = "�༭";
            this.Btn_Edit1.ToolTipText = "ѡ��һ��ʵ��򿪱༭����";
            // 
            // Btn_Save1
            // 
            this.Btn_Save1.ImageIndex = 2;
            this.Btn_Save1.Name = "Btn_Save1";
            this.Btn_Save1.Tag = "Save";
            this.Btn_Save1.Text = "����";
            this.Btn_Save1.ToolTipText = "����ȫ�����ݡ�";
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.ImageIndex = 3;
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Tag = "Delete";
            this.Btn_Delete.Text = "ɾ��";
            this.Btn_Delete.ToolTipText = "ɾ����ǰѡ����С�";
            // 
            // Btn_DeleteAll
            // 
            this.Btn_DeleteAll.ImageIndex = 8;
            this.Btn_DeleteAll.Name = "Btn_DeleteAll";
            this.Btn_DeleteAll.Text = "���";
            this.Btn_DeleteAll.ToolTipText = "�����ǰ��ѯ�����ļ�¼";
            // 
            // Btn_Refurbish
            // 
            this.Btn_Refurbish.ImageIndex = 4;
            this.Btn_Refurbish.Name = "Btn_Refurbish";
            this.Btn_Refurbish.Text = "ˢ��";
            this.Btn_Refurbish.ToolTipText = "���¶�ȡ����";
            // 
            // Btn_Execl
            // 
            this.Btn_Execl.ImageIndex = 5;
            this.Btn_Execl.Name = "Btn_Execl";
            this.Btn_Execl.Tag = "Execl";
            this.Btn_Execl.Text = "����";
            // 
            // Btn_Xml
            // 
            this.Btn_Xml.ImageIndex = 5;
            this.Btn_Xml.Name = "Btn_Xml";
            this.Btn_Xml.Tag = "XML";
            this.Btn_Xml.Text = "������Xml";
            this.Btn_Xml.Visible = false;
            // 
            // Btn_Income
            // 
            this.Btn_Income.ImageIndex = 6;
            this.Btn_Income.Name = "Btn_Income";
            this.Btn_Income.Text = "����";
            this.Btn_Income.ToolTipText = "����";
            // 
            // Btn_Close
            // 
            this.Btn_Close.ImageIndex = 7;
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Tag = "Close";
            this.Btn_Close.Text = "�ر�";
            this.Btn_Close.ToolTipText = "�رհ�ť��";
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
            // 
            // dg1
            // 
            this.dg1.AlternatingBackColor = System.Drawing.Color.Blue;
            this.dg1.DataMember = "";
            this.dg1.DGModel = BP.Win32.Controls.DGModel.None;
            this.dg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
            this.dg1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dg1.IsDGReadonly = false;
            this.dg1.Location = new System.Drawing.Point(0, 28);
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowHeaderWidth = 0;
            this.dg1.Size = new System.Drawing.Size(774, 467);
            this.dg1.TabIndex = 1;
            this.dg1.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dg1_Navigate);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 495);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sta1,
            this.sta3});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(774, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "statusBar1";
            // 
            // sta1
            // 
            this.sta1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.sta1.Name = "sta1";
            this.sta1.Text = "��ǰѡ����";
            this.sta1.Width = 694;
            // 
            // sta3
            // 
            this.sta3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sta3.Name = "sta3";
            this.sta3.Text = "��ǰʱ��";
            this.sta3.Width = 64;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripLabel1,
            this.toolStripDropDownButton1,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(774, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(95, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            // 
            // UIEns
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(774, 517);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.bpToolbar1);
            this.Name = "UIEns";
            this.Text = "UIEns";
            this.Resize += new System.EventHandler(this.UIEns_Resize);
            this.Load += new System.EventHandler(this.UIEns_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta3)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		#region ����

		/// <summary>
		/// ��ǰ�Ĺ�����Ens
		/// </summary>
		private UAC _SysEnsUAC=null;
		/// <summary>
		/// ��ǰ�Ĺ�����Ens
		/// </summary>
		public UAC HisSysEnsUAC
		{
			get
			{
				if (_SysEnsUAC==null)
					throw new Exception("û�и�hisEns������ֵ");
				return _SysEnsUAC;
			}
			set
			{
				_SysEnsUAC=value;
			}
		}
		/// <summary>
		/// ��ǰ�Ĺ�����Ens
		/// </summary>
		private Entities _HisEns=null;
		/// <summary>
		/// ��ǰ�Ĺ�����Ens
		/// </summary>
		public new Entities HisEns
		{
			get
			{
				if (_HisEns==null)
					throw new Exception("û�и�hisEns������ֵ");
				return _HisEns;
			}
			set
			{
				_HisEns=value;
			}
		}
		#endregion

		#region Edit
		 
		 
		public void Btn_New1()
		{
			En ui =new En() ;
			ui.Bind2(this.dg1.HisEns.GetNewEntity );
			ui.ShowDialog();

//			UIEn ui =new UIEn() ;
//			ui.SetData(this.dg1.HisEns.GetNewEntity,false);
//			ui.ShowDialog();
		}
		#endregion

		#region ���÷���
		/// <summary>
		/// ��ÿؼ�DDL
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private BP.Win32.Controls.DDL GetDDLByKey(string key)
		{
			return (DDL)this.GetCtlByKey(key);
		}
        /// <summary>
        /// ��ÿؼ�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		public Control GetCtlByKey(string key)
		{
			foreach(Control ctl in this.Controls)
			{
				if (ctl.Name==key)
				{
					return  ctl;
				}
			}
			throw new Exception("û���ҵ�name="+key+"�Ŀؼ�.");
		}

		
		#endregion

		/// <summary>
		/// �������ݴ���Ĺ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UIEns_Resize(object sender, System.EventArgs e)
		{
			this.dg1.Width = this.Width-50;
			this.dg1.Height = this.Height-150;
		}
		#region �¼�

        public void BtnRefurbish()
        {
            UAC en = this.HisEn.HisUAC;
            this.BindEns(this.HisEns, en, false);
        }

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			string msg="";
            try
            {
                switch (e.Button.Text)
                {
                    case "ˢ��":
                        this.dg1.Bind();
                        break;
                    case "����":
                        UILoadData ui = new UILoadData();
                        ui.HisEn = this.HisEn;
                        ui.HisEns = this.HisEns;
                        ui.ShowDialog();
                        break;
                    case "�༭":
                        this.dg1.Card();
                        break;
                    case "����":
                        this.dg1.Save();
                        this.sta1.Text = "����ɹ�...";
                        break;
                    case "�ر�":
                        this.Close();
                        break;
                    case "�½�":
                        this.Btn_New1();
                        break;
                    case "ɾ��":
                        this.dg1.DeleteSelected();
                        this.btn_SearchClick(null, null);
                        this.Activate();
                        break;
                    case "���":
                        this.dg1.DeleteAll();
                        break;
                    case "������Excel":
                    case "����":
                        this.saveFileDialog1.Title = "ccflow���--��ѡ���ļ���ŵ�λ��.";
                        this.saveFileDialog1.Filter = "txt files (*.txt)|*.txt| ���ӱ��(*.xls)|*.xls";
                        this.saveFileDialog1.FileName = this.HisEn.EnDesc;
                        this.saveFileDialog1.DefaultExt = ".xls";
                        this.saveFileDialog1.ShowDialog();
                        string file = this.saveFileDialog1.FileName;
                        if (file.Trim().Length == 0)
                            return;
                        //this.ExportDGToExcel(this.dg1, file);
                        break;
                    case "������Xml":
                       // this.ExportToXml(this.dg1.HisEns);
                        break;
                    default:
                        throw new Exception("Error tag" + e.Button.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               // this.ResponseWriteRedMsg(ex);
                //this.Alert(msg+ex.Message);
            }
		}


		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_SearchClick(object sender, EventArgs e)
		{
            //QueryObject qo =new QueryObject(this.HisEns);
            //TB tb = (TB)this.gb1.GetTBByKey("TB_Search");//���TB������ΪTB_Search���ؼ�
            //string keyVal=tb.Text.Trim();
            //Attrs attrs = this.HisEn.EnMap.Attrs;
		
            //string pk=this.HisEn.PK ; 
            //// TBʽ��ѯ����
            //if (keyVal.Length > 0)
            //{
            //    qo.addLeftBracket();
            //    qo.AddWhere(pk, " LIKE ", "%" + keyVal + "%");
            //    foreach (Attr en in attrs)
            //    {
            //        if (en.MyFieldType == FieldType.RefText || en.MyFieldType == FieldType.Enum)
            //            continue;
            //        if (en.UIContralType == UIContralType.DDL || en.UIContralType == UIContralType.CheckBok)
            //            continue;
            //        if (en.Key == pk)
            //            continue;

            //        qo.addOr();
            //        qo.AddWhere(en.Key, " LIKE ", "%" + keyVal + "%");
            //    }
            //    qo.addRightBracket();
            //}
            //else
            //{
            //    qo.addLeftBracket();
            //    qo.AddWhere("abc", "all");
            //    qo.addRightBracket();
            //}

            //// ���ԡ�
            //Attrs searchAttrs = this.HisEn.EnMap.SearchAttrs;
            //foreach (Attr attr in searchAttrs)
            //{
            //    if (attr.MyFieldType == FieldType.RefText)
            //        continue;
            //    qo.addAnd();
            //    qo.addLeftBracket();
            //    qo.AddWhere(attr.Key, this.gb1.GetDDLByKey("DDL_" + attr.Key).SelectedValue.ToString());
            //    qo.addRightBracket();
            //}
            //qo.DoQuery();

            //this.dg1.BindEnsThisOnly(this.HisEns,false,false);

            ///*
            ////
            // DataTable dt= qo.DoQueryToTable();
            // dt.TableName = this.HisEn.EnDesc ;
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            //this.dg1.SetDataBinding(ds,dt.TableName) ; 
            //if(dt.Rows.Count==0)
            //{
            //    System.Windows.Forms.MessageBox.Show("û�з��ϵ�ǰ�����ļ�¼��","��ѯ���");
            //}
            //*/
			
            ////this.dg1.ReSetDataSource(ens);
		}
		#endregion �¼�

		private void UIEns_Load(object sender, System.EventArgs e)
		{
			
		}

        private void dg1_Navigate(object sender, NavigateEventArgs ne)
        {

        }

        public void InitByMap(Map map)
        {
            InitByMapV2(map.IsShowSearchKey, map.AttrsOfSearch, map.HisFKAttrs,null) ; // map.AttrsOfSearch, map.SearchAttrs);
        }

        public void InitByMapV2(bool isShowKey, AttrsOfSearch attrsOfSearch, Attrs attrsOfFK, Attrs attrD1)
        {
            this.toolStrip1.Items.Clear();
            int keysNum = 0;

            //ToolStripItemCollection tsc = new ToolStripItemCollection(this.toolStrip1

            // �ؼ��֡�
            if (isShowKey)
            {
                ToolStripLabel lab = new ToolStripLabel();
                lab.Text = "�ؼ���:";
                lab.Name = "lab";
                this.toolStrip1.Items.Add(lab);

                ToolStripTextBox tb = new ToolStripTextBox();
                tb.Text = "";
                tb.Name = "TB_Key";
                tb.BorderStyle = BorderStyle.FixedSingle;
                this.toolStrip1.Items.Add(tb);

                //this.AddLab("Lab_Key", "�ؼ���");
                //ToolbarTB tb = new ToolbarTB();
                //tb.ID = "TB_Key";
                //tb.Columns = 9;
                //this.AddTB(tb);
                keysNum++;
            }




            string className = this.HisEns.ToString();
            string cfgVal = "";
            if (className != null)
            {
                UserRegedit ur = new UserRegedit(WebUser.No, className + "_SearchAttrs");
                ur.FK_Emp = WebUser.No;
                cfgVal = ur.Vals;
            }


            // ������Բ�ѯ��			 
            bool isfirst = true;
            foreach (Attr attr in attrsOfFK)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                //ToolStripLabel
                ToolStripComboBox ddl = new ToolStripComboBox();
                ddl.FlatStyle = FlatStyle.System;
                ddl.DropDownStyle= ComboBoxStyle.DropDownList;
                //= FlatStyle.Standard;

                
                ddl.Name = "DDL_" + attr.Key;

                //ddl.Text = "all:>>ȫ��" + attr.Desc;
                ddl.Items.Insert(0,"all:>>" + attr.Desc);
                ddl.SelectedIndex = 0;


                this.toolStrip1.Items.Add(ddl);
                //   this.toolStrip1.Items.Add(ddl);

                keysNum++;

                if (attr.MyFieldType == FieldType.Enum)
                {
                    SysEnums ses = new SysEnums(attr.UIBindKey);
                    foreach (SysEnum se in ses)
                    {
                        ddl.Items.Add(se.IntKey + ":" + se.Lab);
                    }
                }
                else
                {
                    // �����е���Ϣ��Bind.					
                    if (attr.UIBindKey == "BP.Port.Depts")
                    {
                        // �ж������������ѯ������Ȩ�ޡ�
                        BP.Port.Depts zsjgs = new BP.Port.Depts();
                        zsjgs.RetrieveAll();
                        foreach (BP.Port.Dept zsjg in zsjgs)
                        {
                            string space = "";
                            space = space.PadLeft(zsjg.Grade - 1, '-');
                            ddl.Items.Add(zsjg.No + ":" + space + zsjg.Name);
                        }
                    }
                    else
                    {
                        Entities ens = DA.ClassFactory.GetEns(attr.UIBindKey);
                        ens.RetrieveAll();
                        foreach (Entity en in ens)
                        {
                            ddl.Items.Add(en.GetValStringByKey(attr.UIRefKeyValue) + ":" + en.GetValStringByKey(attr.UIRefKeyText));
                        }
                        // this.GetDDLByKey("DDL_" + attr.Key).SelfBind();
                        // this.GetDDLByKey("DDL_" + attr.Key).Items[0].Text = ">>ȫ��" + attr.Desc;
                    }
                }

                if (isfirst)
                {
                    //this.GetDDLByKey("DDL_"+attr.Key).SetSelectItem(defSeleVal);
                    //this.GetDDLByKey("DDL_"+attr.Key).SetSelectItem(defSeleVal);
                    isfirst = false;
                }
            }

            ToolStripButton btn = new ToolStripButton();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btn.Text = "����";
            btn.Click += new EventHandler(btn_Search_Click);
            this.toolStrip1.Items.Add(btn);


          
        }

        void btn_Search_Click(object sender, EventArgs e)
        {

            QueryObject qo = new QueryObject(this.HisEns);

            string keyVal = "";
            ToolStripTextBox tb = (ToolStripTextBox)this.toolStrip1.Items.Find("TB_Key",true)[0];
            if (tb != null)
                keyVal = tb.Text;

            Attrs attrs = this.HisEn.EnMap.Attrs;
            string pk = this.HisEns.GetNewEntity.PK;
            // TBʽ��ѯ����
            if (keyVal.Length > 0)
            {
                qo.addLeftBracket();
                qo.AddWhere(pk, " LIKE ", "%" + keyVal + "%");
                foreach (Attr en in attrs)
                {
                    if (en.MyFieldType == FieldType.RefText || en.MyFieldType == FieldType.Enum)
                        continue;

                    if (en.UIContralType == UIContralType.DDL || en.UIContralType == UIContralType.CheckBok)
                        continue;

                    if (en.Key == pk)
                        continue;

                    if (en.IsNum  )
                        continue;

                    qo.addOr();
                    qo.AddWhere(en.Key, " LIKE ", "%" + keyVal + "%");
                }
                qo.addRightBracket();
            }
            else
            {
                qo.addLeftBracket();
                qo.AddWhere("abc", "all");
                qo.addRightBracket();
            }

            // ���ԡ�
            AttrSearchs searchAttrs = this.HisEn.EnMap.SearchAttrs;
            foreach (AttrSearch attr in searchAttrs)
            {
                //if (attr.MyFieldType == FieldType.RefText)
                //    continue;
                qo.addAnd();
                qo.addLeftBracket();
                ToolStripComboBox cb = (ToolStripComboBox)this.toolStrip1.Items.Find("DDL_" + attr.Key, true)[0];
                string val = cb.SelectedItem.ToString().Split(':')[0];
                qo.AddWhere(attr.Key, val);
                qo.addRightBracket();
            }
            qo.DoQuery();

            this.dg1.BindEnsThisOnly(this.HisEns, false, false);

            /*
            //
             DataTable dt= qo.DoQueryToTable();
             dt.TableName = this.HisEn.EnDesc ;
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            this.dg1.SetDataBinding(ds,dt.TableName) ; 
            if(dt.Rows.Count==0)
            {
                System.Windows.Forms.MessageBox.Show("û�з��ϵ�ǰ�����ļ�¼��","��ѯ���");
            }
            */

            //this.dg1.ReSetDataSource(ens);
            //throw new Exception("The method or operation is not implemented.");
        }

      
	}
}

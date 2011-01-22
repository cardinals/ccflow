using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.WF;

namespace BP.Win.WF
{
	/// <summary>
	/// WFDesigner ��ժҪ˵����
	/// </summary>
	public class FrmDesigner : WFForm
	{
        private BP.Win.WF.WinWFFlow winFlow1;
        private ContextMenuStrip contextMenuStrip1;
        private ImageList imageList1;
        private ToolStripMenuItem Btn_Rpt;
        private ToolStripMenuItem Btn_Check;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem Btn_New;
        private ToolStripMenuItem Btn_Del;
        private ToolStripMenuItem Btn_Copy;
        private ToolStripMenuItem Btn_GenerFlowTemplate;
        private ToolStripMenuItem Btn_Run;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem Btn_DataTo;
        private ToolStripMenuItem FlowFlowToolStripMenuItem;
        private ToolStripMenuItem FlowToolStripMenuItem;
        private ToolStripMenuItem AddNodeToolStripMenuItem;
        private ToolStripMenuItem CheckNodeToolStripMenuItem;
        private ToolStripMenuItem OrdinNodeToolStripMenuItem;
        private ToolStripMenuItem LabToolStripMenuItem;
        private ToolStripMenuItem NodeLineToolStripMenuItem;
        private IContainer components;
    
		public FrmDesigner()
		{
			InitializeComponent();
			this.winFlow1 = new WinWFFlow();
			this.winFlow1.Dock =DockStyle.Fill;
			this.Controls.Add(this.winFlow1);  
		}
		public WinWFFlow HisWinFlow
		{
			get
			{
				return this.winFlow1;
			}
		}
        public bool IsEdit = false;
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.IsEdit)
            {
                if (MessageBox.Show(this.ToE("NoSaveClose","û�б��棬��ȷ��Ҫ������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    base.OnFormClosing(e);
                    return;
                }
                else
                {
                    this.Activate();
                }
            }
            base.OnFormClosing(e);
        }
         
		public void BindData(Flow flow)
		{
			this.winFlow1.BindData( flow );
		}

        public void Save()
        {
            this.winFlow1.Save();
        }

		#region Windows ������������ɵĴ���
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

		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDesigner));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OrdinNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NodeLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Check = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_Rpt = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_DataTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_New = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Del = new System.Windows.Forms.ToolStripMenuItem();
         //   this.Btn_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_GenerFlowTemplate = new System.Windows.Forms.ToolStripMenuItem();


            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.FlowFlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddNodeToolStripMenuItem,
            this.FlowToolStripMenuItem,
            this.Btn_Run,
            this.Btn_Check,
            this.toolStripSeparator2,
            this.Btn_Rpt,
            this.Btn_DataTo,
            this.toolStripSeparator1,
            this.Btn_New,
            this.Btn_Del,
            this.Btn_GenerFlowTemplate
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 214);
            this.contextMenuStrip1.Text = "���̲˵�";
            this.contextMenuStrip1.DoubleClick += new System.EventHandler(this.contextMenuStrip1_DoubleClick);
            // 
            // AddNodeToolStripMenuItem
            // 
            this.AddNodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckNodeToolStripMenuItem,
            this.OrdinNodeToolStripMenuItem,
            this.LabToolStripMenuItem,
            this.NodeLineToolStripMenuItem});

          //  this.AddNodeToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.WorkFlowOp;

            this.AddNodeToolStripMenuItem.Name = "AddNodeToolStripMenuItem";
            this.AddNodeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.AddNodeToolStripMenuItem.Text = this.ToE("NewNode", "���ӽڵ�"); //���ӽڵ�
          //  this.AddNodeToolStripMenuItem.Text = "���ӽڵ�"; 

            // 
            // CheckNodeToolStripMenuItem
            // 
            //this.CheckNodeToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.StandardChecks;
            //this.CheckNodeToolStripMenuItem.Name = "CheckNode";
            //this.CheckNodeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            //this.CheckNodeToolStripMenuItem.Text = this.ToE("CheckNode", "��˽ڵ�");
            //this.CheckNodeToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            //this.CheckNodeToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);
            // 
            // OrdinNodeToolStripMenuItem
            // 
   //         this.OrdinNodeToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.Work;
            this.OrdinNodeToolStripMenuItem.Name = "OrdinaryNode";
            this.OrdinNodeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.OrdinNodeToolStripMenuItem.Text = this.ToE("OrdinaryNode", "��ͨ�ڵ�"); 
            this.OrdinNodeToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);

            // 
            // LabToolStripMenuItem
            // 
            this.LabToolStripMenuItem.Name = "Label";
            this.LabToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.LabToolStripMenuItem.Text = this.ToE("Label", "��ǩ");
            this.LabToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.LabToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);
            // 
            // NodeLineToolStripMenuItem
            // 
            this.NodeLineToolStripMenuItem.Name = "NodeLine";
            this.NodeLineToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.NodeLineToolStripMenuItem.Text = this.ToE("NodeLine", "�ڵ�����"); ;
            this.NodeLineToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.NodeLineToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);

            // 
            // FlowToolStripMenuItem
            // 
            this.FlowToolStripMenuItem.Checked = true;
            this.FlowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
          //  this.FlowToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.Authorize;
            this.FlowToolStripMenuItem.Name = "FlowProperty";
            this.FlowToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.FlowToolStripMenuItem.Text = this.ToE("FlowProperty","��������") ;
            this.FlowToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.FlowToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);

            // 
            // Btn_Run
            // 
   //         this.Btn_Run.Image = global::FlowDesign.Properties.Resources.Start;
            this.Btn_Run.Name = "Run";
            this.Btn_Run.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.Btn_Run.Size = new System.Drawing.Size(183, 22);
            this.Btn_Run.Text = this.ToE("RunFlow", "��������");
            this.Btn_Run.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Run.Click += new System.EventHandler(this.Menu_Click);
            // 
            // Btn_Check
            // 
          //  this.Btn_Check.Image = global::FlowDesign.Properties.Resources.Confirm;
            this.Btn_Check.Name = "FlowRpt";
            this.Btn_Check.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Btn_Check.Size = new System.Drawing.Size(183, 22);
            this.Btn_Check.Text = this.ToE("FlowRpt", "������ϱ���");
            this.Btn_Check.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Check.Click += new System.EventHandler(this.Menu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // Btn_Rpt
            // 
            this.Btn_Rpt.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Rpt.Image")));
            this.Btn_Rpt.Name = "RptDefinition";
            this.Btn_Rpt.Size = new System.Drawing.Size(183, 22);
            this.Btn_Rpt.Text = this.ToE("RptDefinition", "������"); // "������";
            this.Btn_Rpt.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Rpt.Click += new System.EventHandler(this.Menu_Click);
            // 
            // Btn_DataTo
            // 
   //         this.Btn_DataTo.Image = global::FlowDesign.Properties.Resources.Table;
            this.Btn_DataTo.Name = "RptDefinitionTurn";
            this.Btn_DataTo.Size = new System.Drawing.Size(183, 22);
            this.Btn_DataTo.Text = this.ToE("RptDefinitionTurn", "����ת������");  //"����ת������";
            this.Btn_DataTo.ToolTipText = "������������ʱ���ݰ���һ���Ĺ���ת����ָ����ϵͳ���С�";
            this.Btn_DataTo.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_DataTo.Click += new System.EventHandler(this.Menu_Click);

            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // Btn_New
            // 
         //   this.Btn_New.Image = global::FlowDesign.Properties.Resources.New;
            this.Btn_New.Name = "NewFlow";
            this.Btn_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.Btn_New.Size = new System.Drawing.Size(183, 22);
            this.Btn_New.Text = this.ToE("NewFlow", "�½�����");// "�½�����";
            this.Btn_New.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_New.Click += new System.EventHandler(this.Menu_Click);
            // 
            // Btn_Del
            // 
   //         this.Btn_Del.Image = global::FlowDesign.Properties.Resources.Delete;
            this.Btn_Del.Name = "Delete";
            this.Btn_Del.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.Btn_Del.Size = new System.Drawing.Size(183, 22);
            this.Btn_Del.Text = this.ToE("Delete", "ɾ��");  //"ɾ��";
            this.Btn_Del.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Del.Click += new System.EventHandler(this.Menu_Click);


            //// 
            //// Btn_Copy
            //// 
            ////this.Btn_Copy.Image = global::FlowDesign.Properties.Resources.Confirm;
            //this.Btn_Copy.Name = "Copy";
            ////this.Btn_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            //this.Btn_Copy.Size = new System.Drawing.Size(183, 22);
            //this.Btn_Copy.Text = this.ToE("Copy", "��������");  //"����";
            //this.Btn_Copy.DoubleClick += new System.EventHandler(this.Menu_Click);
            //this.Btn_Copy.Click += new System.EventHandler(this.Menu_Click);


            // 
            // Btn_GenerFlowTemplate
            // 
            //this.Btn_Copy.Image = global::FlowDesign.Properties.Resources.Confirm;
            this.Btn_GenerFlowTemplate.Name = "GenerFlowTemplate";
            this.Btn_GenerFlowTemplate.Size = new System.Drawing.Size(183, 22);

            this.Btn_GenerFlowTemplate.Text = this.ToE("GenerFlowTemplate", "��������ģ��");  //"����";

            this.Btn_GenerFlowTemplate.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_GenerFlowTemplate.Click += new System.EventHandler(this.Menu_Click);

            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "New.gif");
            this.imageList1.Images.SetKeyName(1, "Save.gif");
            this.imageList1.Images.SetKeyName(2, "DataIO.gif");
            this.imageList1.Images.SetKeyName(3, "Delete.gif");
            //this.imageList1.Images.SetKeyName(4, "Copy.gif");

            // 
            // FlowFlowToolStripMenuItem
            // 
            this.FlowFlowToolStripMenuItem.Name = "FlowProperty";
            this.FlowFlowToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.FlowFlowToolStripMenuItem.Text = this.ToE("FlowProperty", "��������");  //"��������";
            this.FlowFlowToolStripMenuItem.ToolTipText = "ִ�����̵������ơ�";
            this.FlowFlowToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.FlowFlowToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);
            // 
            // FrmDesigner
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
       //     this.BackgroundImage = global::FlowDesign.Properties.Resources.BJ1;
            this.ClientSize = new System.Drawing.Size(447, 379);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDesigner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = this.ToE("FlowDesign", "���������");  //"���������";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.DoubleClick += new System.EventHandler(this.contextMenuStrip1_DoubleClick);
            this.Load += new System.EventHandler(this.FrmDesigner_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
		}
		#endregion

		private void FrmDesigner_Load(object sender, System.EventArgs e)
		{
			this.BackColor=Color.DarkBlue ;
		}
        private void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = sender as ToolStripMenuItem;
            if (btn == null)
            {
                PubClass.Alert(e.ToString());
                return;
            }

            DoText( btn.Name.Replace("Btn_Run","") );
        }

        public void DoText(string text)
        {
            switch (text)
            {

                case "Run":
                case "��������":
                case "��������":
                    BP.WF.Global.DoUrlByType("RunFlow&FK_Flow=" + this.HisWinFlow.HisFlow.No, null);
                    break;
                case "����":
                case "Save":
                    this.HisWinFlow.Save();
                    break;
                case "FlowRpt":
                case "������Ƽ�鱨��":
                case "��Ƽ�鱨��":
                case "������ϱ���":
                case "����У��":
                    this.Save();
                    BP.WF.Global.DoUrlByType("FlowCheck", this.HisWinFlow.HisFlow.No);
                    break;
                case "CheckFlow":
                case "RptDefinition":
                case "RptDefinitionTurn":
                case "���������ȷ��":
                case "������":
                case "����ת������":
                    BP.WF.Global.DoUrl("/DoPort.aspx?DoType=WFRpt&PK=ND" + int.Parse(this.HisWinFlow.HisFlow.No) + "Rpt");
                    //������
                    //BP.WF.Global.DoEdit(this.HisWinFlow.HisFlow);
                    break;
                case "�½�����":
                case "�½�":
                case "NewFlow":
                    MessageBox.Show(this.ToE("WhenNewFlow", "��㣬�������ϵ��½���ť�����������˵��ϵ��Ҽ���"), " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "ɾ��":
                case "Delete":
                case "Del":
                case "ɾ������":
                    if (BP.Win32.PubClass.Question(this.ToE("AYS", "��ȷ����")) == false)
                        return;

                    if (BP.Win32.PubClass.Question(this.ToE("AYS", "��ȷ����")) == false)
                        return;
                    this.HisWinFlow.HisFlow.DoDelete();
                    this.Close();
                    break;
                case "FlowProperty":
                case "����":
                case "��������":
                    Flow fl = this.HisWinFlow.HisFlow;
                    BP.WF.Global.DoEdit(fl);
                    return;

                case "OrdinaryNode":
                case "��ͨ�ڵ�":
                    BP.Win.WF.Global.ToolIndex = 1;
                    break;
                case "��˽ڵ�":
                case "CheckNode":
                    BP.Win.WF.Global.ToolIndex = 1;
                    break;
                case "�ڵ�����":
                case "�ڵ�������":
                case "NodeLine":
                    MessageBox.Show(this.ToE("WhenAddLine",
                        "������ʮ����ʱ�����ȵ����ӵĵ�һ���ڵ㣬Ȼ���Ҫ���ӵĵڶ����ڵ㡣"),
                        " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BP.Win.WF.Global.ToolIndex = 2;
                    break;
                case "��ǩ":
                case "Label":
                    BP.Win.WF.Global.ToolIndex = 3;
                    break;
                //  case "���ƽڵ�":
                //case "NodeCopy":
                //    BP.Win.WF.Global.ToolIndex = 1;
                //    if (BP.Win32.PubClass.Question(this.ToE("AYS", "��ȷ����")) == false)
                //        return;
                //    Flow fl1 = this.HisWinFlow.HisFlow;
                //    fl1.DoCopy();
                //    break;
                case "����":
                case "Copy":
                    BP.Win.WF.Global.ToolIndex = 0;
                    if (BP.Win32.PubClass.Question(this.ToE("AYS", "��ȷ����")) == false)
                        return;
                    Flow fl11 = this.HisWinFlow.HisFlow;
                    fl11.DoCopy();
                    break;

                case "��������ģ��":
                case "GenerFlowTemplate":
                    string msg = this.ToE("CheckAlertInfo", 
                        "ϵͳ���ỨһЩʱ�䴦���������顣\r\n\r\n1��Ϊÿһ������ִ����졣\r\n2������html���̱���\r\n3������ϵͳ��ʶ�������ģ�塣\r\n==================\r\n�����ĵȴ���ֱ�������״�仯Ϊֹ��");

                    if (MessageBox.Show(msg,
                         "Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;
                    Flows.GenerHtmlRpts();
                    this.Cursor = Cursors.Default;
                    //BP.WF.Global.DoGenerFlowTemplate(this.HisWinFlow.HisFlow);
                    break;
                default:
                    MessageBox.Show("δ��������" + text);
                    break;
            }
        }
        private void contextMenuStrip1_DoubleClick(object sender, EventArgs e)
        {
            this.DoText("��������");
        }
	}
}

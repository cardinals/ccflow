using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port ; 
using BP.Web; 
using BP.Pub;
using BP.Web.Controls;
using System.IO;
 

namespace BP.Win32.Comm
{
	/// <summary>
	/// UILoadData ��ժҪ˵����
	/// </summary>
	public class UILoadData : BP.Win32.PageBase
	{
		 

		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.ToolBarButton Btn_Open;
		private System.Windows.Forms.ToolBarButton Btn_DBCheck;
		private System.Windows.Forms.ToolBarButton Btn_Close;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ToolBarButton Btn_Template;
		private System.Windows.Forms.ToolBarButton Btn_Help;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton Btn_LoadAsOverride;
		private System.Windows.Forms.ToolBarButton Btn_LoadAsClear;
		private System.Windows.Forms.ToolBarButton Btn_GenerFile;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.ComponentModel.IContainer components;

		public UILoadData()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UILoadData));
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_Template = new System.Windows.Forms.ToolBarButton();
			this.Btn_GenerFile = new System.Windows.Forms.ToolBarButton();
			this.Btn_Open = new System.Windows.Forms.ToolBarButton();
			this.Btn_DBCheck = new System.Windows.Forms.ToolBarButton();
			this.Btn_LoadAsOverride = new System.Windows.Forms.ToolBarButton();
			this.Btn_LoadAsClear = new System.Windows.Forms.ToolBarButton();
			this.Btn_Close = new System.Windows.Forms.ToolBarButton();
			this.Btn_Help = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.Btn_Template,
																						  this.Btn_GenerFile,
																						  this.Btn_Open,
																						  this.Btn_DBCheck,
																						  this.Btn_LoadAsOverride,
																						  this.Btn_LoadAsClear,
																						  this.Btn_Close,
																						  this.Btn_Help});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(552, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// Btn_Template
			// 
			this.Btn_Template.ImageIndex = 6;
			this.Btn_Template.Text = "�ļ���ʽ���";
			this.Btn_Template.ToolTipText = "�ļ���ʽ���";
			// 
			// Btn_GenerFile
			// 
			this.Btn_GenerFile.ImageIndex = 6;
			this.Btn_GenerFile.Text = "��ȡ�ⲿ����";
			this.Btn_GenerFile.ToolTipText = "�������ӵ����������ݿ��ϡ�";
			this.Btn_GenerFile.Visible = false;
			// 
			// Btn_Open
			// 
			this.Btn_Open.ImageIndex = 7;
			this.Btn_Open.Text = "ѡ���ļ�";
			this.Btn_Open.ToolTipText = "ѡ���ļ�";
			// 
			// Btn_DBCheck
			// 
			this.Btn_DBCheck.ImageIndex = 8;
			this.Btn_DBCheck.Text = "���ݼ��";
			// 
			// Btn_LoadAsOverride
			// 
			this.Btn_LoadAsOverride.ImageIndex = 9;
			this.Btn_LoadAsOverride.Text = "׷�ӷ�ʽװ��";
			// 
			// Btn_LoadAsClear
			// 
			this.Btn_LoadAsClear.ImageIndex = 10;
			this.Btn_LoadAsClear.Text = "��շ�ʽװ��";
			// 
			// Btn_Close
			// 
			this.Btn_Close.ImageIndex = 0;
			this.Btn_Close.Text = "�ر�";
			this.Btn_Close.ToolTipText = "�ر�";
			// 
			// Btn_Help
			// 
			this.Btn_Help.ImageIndex = 2;
			this.Btn_Help.Text = "����";
			this.Btn_Help.ToolTipText = "����";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 251);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(552, 22);
			this.statusBar1.TabIndex = 2;
			this.statusBar1.Text = "statusBar1";
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 41);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(552, 210);
			this.dataGrid1.TabIndex = 3;
			// 
			// UILoadData
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(552, 273);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "UILoadData";
			this.Text = "���ݵ���";
			this.Load += new System.EventHandler(this.UILoadData_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//string str="";
			try
			{
				switch(e.Button.Text)
				{
					case "�ļ���ʽ���":
						this.saveFileDialog1.Title="ccflow���--��ѡ���ļ���ŵ�λ��.";
						this.saveFileDialog1.FileName=this.HisEn.EnDesc;
						this.saveFileDialog1.DefaultExt=".xls";
						this.saveFileDialog1.ShowDialog();
						string file =this.saveFileDialog1.FileName;
						if (file.Trim().Length==0)
							return;
						this.ExportExcelExcelTemplate(this.HisEn,file);
						break;
					case "����":
						break;
					case "ѡ���ļ�":
						this.BtnChoseFile();
						 
						if (this.Question("�Ƿ���Ҫ���ݼ��?"))
							this.BtnCheckDB();
						break;
					case "���ݼ��":
						this.BtnCheckDB();
						break;
					case "׷�ӷ�ʽװ��":
						this.BtnSaveToData(false);
						break;
					case "��շ�ʽװ��":
						if (this.Warning("��շ�ʽװ�� \t\n \t\nԭ�������ݽ���ɾ�����ָܻ����ѵ�ǰ�����ݼ������棬ȷ����"))
							this.BtnSaveToData(true);
						break;
					case "�ر�":
						this.Close();
						break;					 
					default:
						throw new Exception(e.Button.Text+" error ");
				}
			}
			catch(Exception ex)
			{
				this.ResponseWriteRedMsg(ex);
			}
		}
		/// <summary>
		/// ������ݵ�������
		/// </summary>
		public bool BtnCheckDB()
		{
			return true;			 
		}
		/// <summary>
		/// ���浽���ݿ�
		/// </summary>
		public void BtnSaveToData(bool IsClear)
		{
			
			int okNum=0;
			int errorNum=0;
			string errMsg="";

			

			Entities ens =this.HisEns;
			if (IsClear)
			{
				ens.RetrieveAll();
				ens.Delete();
			}

			foreach(DataRow dr in this.Table.Rows)
			{
				try
				{
					Entity en =this.HisEns.GetNewEntity;
					foreach(DataColumn dc in this.Table.Columns)
					{
						en.SetValByDesc( dc.ColumnName, dr[dc.ColumnName].ToString().Trim() );
					}

					if (IsClear)
						en.Insert();
					else
						en.Save();
					ens.AddEntity(en);
					okNum++;
				}
				catch(Exception msg)
				{
					errorNum++;
					errMsg+=msg.Message;
				}
			}

			if (errorNum==0)
				this.Information("@�ļ�������Ϣ:�ɹ�����ȫ������Ϣ,��"+okNum+"����");
			else
				this.Alert("@�ļ�������Ϣ:"+"\n�ɹ�������"+okNum+",���������:"+errorNum+",������Ϣ����:\n"+errMsg);
		}

		/// <summary>
		/// ����
		/// </summary>
		public void BtnChoseFile()
		{
			try
			{
			 
				this.openFileDialog1.Filter="���ӱ��(*.xls)|*.xls|���ӱ��(�ı��ļ�(*.txt)|*.txt";
				this.openFileDialog1.ShowDialog();

				if(this.openFileDialog1.FileName.Trim()=="")
					throw new Exception("�밴�������ť�ҵ�Ҫ�ϴ����ļ�����ִ�д˲�����");
			 
				#region �ϴ��ļ���Ϣ����֤��
				string fName = System.IO.Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName );
				string fExt = System.IO.Path.GetExtension( this.openFileDialog1.FileName  ).ToLower();

				if( fExt!=".xls" && fExt!=".dbf")
					throw new Exception("��ѡ��һ��Excel�ļ�(*.xls)����dBASE���ݱ��ļ�(*.dbf)��");

				//fPath += "U"+WebUser.No+"\\"+ fName+fExt ;
				//fPath = Server.MapPath( fPath);
				//	System.IO.Directory.CreateDirectory( Path.GetDirectoryName(fPath) );
				//	this.File1.PostedFile.SaveAs( fPath ); //������У��򸲸ǣ�
				// �ļ���Ϣ�ࡣ

				FileInfo fi = new FileInfo( this.openFileDialog1.FileName );
				this.statusBar1.Text ="�ļ�����"+fName+fExt+"��С��"+fi.Length.ToString("###,###,###")+"�ֽ�";

				#endregion �ϴ��ļ���Ϣ

				//string sql="SELECT ";				
				//				Attrs attrs =this.HisEn.EnMap.HisPhysicsAttrs;		
				//				foreach(Attr attr in attrs )
				//					sql+=attr.Desc+" ,";
				
				//sql=sql.Substring(0,sql.Length-1);

				string sql="";

				if(fExt==".xls")
					sql += "SELECT * FROM ["+  this.HisEn.EnDesc +"$]";
				else
					sql +="SELECT *  FROM  "+  this.HisEn.EnDesc ;

				try
				{
                    // #warning
					this.Table = DBLoad.GetTableByExt( this.openFileDialog1.FileName, sql );
				}
				catch(Exception ex)
				{
					//throw new Exception("ִ�в�ѯ���ִ��������ѡ�����excle �ļ���sheet�����Ƿ���["+this.HisEn.EnDesc+"]");

					throw new Exception("����ԭ��:\n 1)��ѡ����ļ��Ƿ���excel 98��ʽ����. \n 2) excle �ļ��� sheet�������Ƿ���["+this.HisEn.EnDesc+"]. \nϵͳ���쳣��Ϣ: "+ex.Message);

				}

				DataSet ds = new DataSet();
				ds.Tables.Add(this.Table);

				this.dataGrid1.DataSource=ds;
				this.dataGrid1.SetDataBinding(ds,this.Table.TableName);

				//System.IO.File.Delete( fPath ); // ɾ������ļ���
				
			}
			catch(Exception ex)
			{
				throw new Exception("����ʧ�ܣ�"+ ex.Message );
			}
			 
		}

		private void UILoadData_Load(object sender, System.EventArgs e)
		{
			this.Text="���ݵ���:"+this.HisEn.EnDesc;
			this.statusBar1.Text="��ѡ������ļ�";
		}
	 
	}
}

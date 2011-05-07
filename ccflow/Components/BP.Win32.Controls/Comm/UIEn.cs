using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port ; 
//using BP.Web.Controls ; 


namespace BP.Win32.Comm
{
	public class UIEn : BP.Win32.PageBase
	{

		#region �ؼ�
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ToolTip toolTip1;
		private BP.Win32.Controls.GB gb1;
		protected internal BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.ImageList imageList1;
		private BP.Win32.Controls.GB gb2;
		#endregion

		public System.Windows.Forms.ToolBarButton toolBarButton_SaveAndNew;
		public System.Windows.Forms.ToolBarButton toolBarButton_SaveAndClose;
		public System.Windows.Forms.ToolBarButton toolBarButton_New;
		public System.Windows.Forms.ToolBarButton toolBarButton_Save;
		public System.Windows.Forms.ToolBarButton toolBarButton_Close;

		private System.Windows.Forms.StatusBar statusBar1;


		#region �Զ�������
		/// <summary>
		/// �����С
		/// </summary>
		public Size BoxSize
		{
			get
			{
				return this.ClientSize;
			}
			set
			{
				this.ClientSize = new Size(value.Width ,value.Height );//+ (this.ClientSize.Height-this.gb1.Location.Y-this.gb2.Location.Y));
			}
		}	
		 
		private bool _IsReadonly=false;
		/// <summary>
		/// ʵ���Ƿ�ֻ��
		/// </summary>
		public bool IsReadonly
		{
			get
			{
				return _IsReadonly; 
			}
			set
			{
				_IsReadonly=value;
			}
		}
		#endregion

		#region ��������
		/// <summary>
		/// ��ӿؼ�
		/// </summary>
		/// <param name="ctl">�ؼ�</param>
		/// <param name="IsLeft">�ж��Ƿ������</param>		
		private void AddCtl(System.Windows.Forms.Control ctl, bool IsLeft)
		{
			if (IsLeft)
			{
				this.gb1.Controls.Add(ctl);
				this.gb1.Height+=ctl.Height*4/5+5;
			}
			else
			{
				
				this.gb2.Controls.Add(ctl);			
				this.gb2.Height+=ctl.Height+5;
			}
		}
		/// <summary>
		/// ��ʼ������
		/// </summary>
		private void InitToolBar()
		{

			this.bpToolbar1.Buttons.Clear();
//			this.toolBarButton3_Update.Enabled=false;
//			this.toolBarButton4_Delete.Enabled=false;

//			#region tool
//			this.bpToolbar1.AddBtn(NamesOfBtn.Card);
//			this.bpToolbar1.AddBtn(NamesOfBtn.New);
//			this.bpToolbar1.AddBtn(NamesOfBtn.Save);
//			this.bpToolbar1.AddBtn(NamesOfBtn.SaveAndNew);
//			this.bpToolbar1.AddBtn(NamesOfBtn.SaveAndClose);
//			this.bpToolbar1.AddBtn(NamesOfBtn.Delete);
//			this.bpToolbar1.AddBtn(NamesOfBtn.Adjunct);
//			this.bpToolbar1.AddBtn(NamesOfBtn.Close); 
//			#endregion
		}
		/// <summary>
		/// ����ID��ÿؼ�
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Control GetContralByID(string id)
		{
			foreach(Control ctl in this.gb1.Controls)
			{
				if (ctl.Name==id)
					return ctl;
			}

			foreach(Control ctl in this.gb2.Controls)
			{
				if (ctl.Name==id)
					return ctl;
			}

			throw new Exception("@û���ҵ�id��"+id );
		}
		/// <summary>
		/// �������� 
		/// </summary>
		/// <param name="en"></param>
		public void BindData(Entity en)
		{
			
			foreach(Attr attr in en.EnMap.Attrs)
			{
				if (attr.MyFieldType==FieldType.RefText)//map ����ֶ����ͣ���AddTBString ���¹���Normal��AddTBStringPK���¹���Pk��
					continue;
				if (attr.MyDataType==DataType.AppBoolean) 
				{
					CB cb= (CB)this.GetContralByID("CB_"+attr.Key);
					cb.Checked = en.GetValBooleanByKey(attr.Key);
					continue;
				}

				if ( attr.MyFieldType==FieldType.Enum || attr.MyFieldType==FieldType.PKEnum )
				{
					DDL ddl = (DDL)this.GetContralByID("DDL_"+attr.Key) ;					
					ddl.SetSelectedVal(en.GetValStringByKey(attr.Key)) ;			
					continue;
				}
				if ( attr.MyFieldType==FieldType.FK  || attr.MyFieldType==FieldType.PKFK )
				{
					if (attr.UIContralType == UIContralType.DDL) 
					{
						DDL ddl = (DDL)this.GetContralByID("DDL_"+attr.Key) ;						
						ddl.SetSelectedVal(en.GetValStringByKey(attr.Key)) ;			
						continue;
					}					
				}
				else
				{
					TB tb=(TB)this.GetContralByID("TB_"+attr.Key) ; 
					tb.Text = en.GetValStringByKey(attr.Key) ; 
				}
			}
		}
		/// <summary>
		/// ����һ��ʵ��
		/// </summary>
		/// <param name="en"></param>
		public void SetData(Entity en, bool IsReadonly)
		{
			this.gb1.Height=0;
			this.gb2.Height=0;

			this.IsReadonly = IsReadonly ;

			if (this.IsReadonly)
			{                 
				this.toolBarButton_New.Enabled=false;
				this.toolBarButton_Save.Enabled=false;
				this.toolBarButton_SaveAndClose.Enabled=false;
				this.toolBarButton_SaveAndNew.Enabled=false;
			}

			if (en.IsEmpty)
			{
				foreach(Attr attr in en.EnMap.Attrs)
				{
					if (attr.Key=="No")
					{
						if (attr.UIIsReadonly || en.EnMap.IsAutoGenerNo)
						{
							en.SetValByKey("No",en.GenerNewNoByKey("No")) ;
						}
					}
				}
				this.IsNew=true;
			}
			else
				this.IsNew=false;

			this.gb1.Text=en.EnDesc+"��";
			this.gb2.Text=en.EnDesc+"��";

			this.HisEn = en;

			if( HisEn == null)
				return;

			Map map = HisEn.EnMap;

			int OffsetH = 12; //ƫ����
			int tbWidth = 130;//Textbox
			Point locLab = new Point( 10,OffsetH+12 );  //����Lab���Ͻ�λ��
			Point locTB  = new Point(100,OffsetH-4+12 );

			bool IsLeft=false;

			foreach(Attr attr in map.Attrs )
			{
				IsLeft=!IsLeft;
				//ö�����͵��ı�
				if (attr.MyFieldType==FieldType.RefText)
				{
					IsLeft=!IsLeft;
					continue;
				}

				#region ���� lab 
				Lab lab = new Lab();
				lab.AutoSize = false;
				lab.Width=locTB.X-20;
				lab.Name = "lab" + attr.Key ;
				lab.Text = attr.Desc ; 
				lab.TextAlign=System.Drawing.ContentAlignment.TopRight ; 
				if(lab.Width>locTB.X)
					this.toolTip1.SetToolTip( lab ,lab.Text);
				lab.Location = locLab;
				this.AddCtl( lab,IsLeft);
				#endregion				 
             
				#region �����DDL����
				 
				if (attr.MyDataType==DataType.AppBoolean)
				{
					#region boolean
					CB cb = new CB();
					cb.Name="CB_"+attr.Key ; 
					cb.Location = locTB ;
					cb.Text = attr.Desc ;
					cb.Size = new Size( tbWidth,22);

					
					if ( attr.UIIsReadonly==false || this.IsReadonly )
						cb.Enabled =false;
					else
						cb.Enabled =true;
					 
					this.AddCtl( cb ,IsLeft );
					cb.BringToFront();
				 

					int val = this.HisEn.GetValIntByKey(attr.Key);
					if (val==1)
						cb.Checked=true;
					else
						cb.Checked=false;

					#endregion boolean				

				}
				else if(  attr.UIContralType == UIContralType.DDL )
				{

					DDL ddl = new DDL();
					ddl.Name ="DDL_"+attr.Key;
					ddl.Location = locTB ;
					ddl.Size = new Size( tbWidth,22);
					 
					if ( attr.UIIsReadonly==false || this.IsReadonly )
						ddl.Enabled =false;
					else
						ddl.Enabled =true;
 
					this.AddCtl( ddl ,IsLeft);
					ddl.BringToFront();

					if( attr.UIIsReadonly==false || this.IsReadonly )
					{
						/* readonly ������� */
						ddl.BindItem(this.HisEn.GetValRefTextByKey(attr.Key),this.HisEn.GetValStringByKey(attr.Key) , null) ; 
					}
					else
					{ 
						/*  ���Ա༭�������*/
						if (attr.MyFieldType==FieldType.Enum || attr.MyFieldType==FieldType.PKEnum)
						{
							// �����ö������
							SysEnums enu = new SysEnums(attr.UIBindKey); //,"CN",en.GetValIntByKey(attr.Key));
							ddl.BindEns(enu);
							ddl.SetSelectedVal(en.GetValStringByKey(attr.Key));							
						}
						else
						{
							Entities ens =ClassFactory.GetEns(attr.UIBindKey);
							ens.RetrieveAll(); 
							ddl.BindEns(ens,attr.UIRefKeyText,attr.UIRefKeyValue); 
						}

					}
				}
				else if(attr.UIContralType == UIContralType.TB)
				{    // �����TextBox����
					TB tb = new TB();
					tb.Name ="TB_"+ attr.Key ;
					tb.Size = new Size( tbWidth ,22);
					tb.Location = locTB;
					tb.ReadOnly = !this.IsReadonly && attr.UIIsReadonly;
					
					this.AddCtl(tb ,IsLeft );
					 
					tb.BringToFront();
					if(  attr.UIHeight>0 )
					{
						locLab.Offset( 0 , OffsetH*2 );
						locTB.Offset( 0 ,  OffsetH*2 );
						tb.Height = 66;
						tb.Multiline = true;
						tb.ScrollBars = ScrollBars.Vertical ;
					}
					//tb.DataBindings.Add("Text" ,this.HisEntity ,att.Key);
					tb.Text = this.HisEn.GetValStringByKey(attr.Key);					
				} 
				#endregion
				locLab.Offset( 0 , OffsetH );
				locTB.Offset( 0 ,  OffsetH );
			}

			int h = locTB.Y ;
			if( h!=400)
			{
				h=400;
				locTB.Offset( 20,0 );
			}
			//pan1.Size =  new Size( locTB.X + tbWidth+8 ,locTB.Y);
			this.BoxSize = new Size( locTB.X + tbWidth+432 , h );
		}
		#endregion

		#region ����

		public UIEn()
		{
			// �õ����� Windows ���������������ġ�
			InitializeComponent();
		}		

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region ȡ�ÿؼ�
		/// <summary>
		/// ȡ��BP.Win32.Controls.CB
		/// </summary>
		/// <param name="key">ָ����Key</param>
		/// <returns>CB</returns>
		private BP.Win32.Controls.CB GetCBByKey(string key)
		{
			if (this.gb1.IsContains(key))
				return this.gb1.GetCBByKey(key);
			else
				return this.gb2.GetCBByKey(key);
		}
		/// <summary>
		/// ȡ��BP.Win32.Controls.TB
		/// </summary>
		/// <param name="key">ָ����Key</param>
		/// <returns>CB</returns>
		private BP.Win32.Controls.TB GetTBByKey(string key)
		{
			if (this.gb1.IsContains(key))
				return this.gb1.GetTBByKey(key);
			else
				return this.gb2.GetTBByKey(key);
		}
		/// <summary>
		/// DateTimePicker
		/// </summary>
		/// <param name="key">ָ����Key</param>
		/// <returns>DateTimePicker</returns>
		private DateTimePicker GetDLLDateByKey(string key)
		{
			if (this.gb1.IsContains(key))
				return this.gb1.GetDateByKey(key);
			else
				return this.gb2.GetDateByKey(key);
			
		}
		/// <summary>
		/// ȡ��BP.Win32.Controls.DDL
		/// </summary>
		/// <param name="key">ָ����Key</param>
		/// <returns>DDL</returns>
		private BP.Win32.Controls.DDL GetDDLByKey(string key)
		{
			if (this.gb1.IsContains(key))
				return this.gb1.GetDDLByKey(key);
			else
				return this.gb2.GetDDLByKey(key);
		}
		#endregion

		#region ����
		/// <summary>
		/// ��������ʵ���ֵ
		/// </summary>
		/// <returns></returns>
		public void ReSetEnValue()
		{
			foreach(Attr attr in this.HisEn.EnMap.Attrs)
			{
				if (attr.MyFieldType==FieldType.RefText)//map ����ֶ����ͣ���AddTBString ���¹���Normal��AddTBStringPK���¹���Pk��
					continue;

				if (attr.MyDataType==DataType.AppBoolean) //�������ͣ��� int,float �ȣ�
				{
					this.HisEn.SetValByKey(attr.Key,this.GetCBByKey("CB_"+attr.Key).Checked  );
					continue;
				}
//				if (attr.MyDataType==DataType.AppDate||attr.MyDataType==DataType.AppDatetime) 
//				{
//					this.HisEn.SetValByKey(attr.Key,this.GetDLLDateByKey("DDL_"+attr.Key).Value );
//					continue;
//				}
				if (attr.UIContralType == UIContralType.DDL) //�ؼ����� ���� TextBox,ddl�ȣ�
				{
					this.HisEn.SetValByKey(attr.Key,this.GetDDLByKey("DDL_"+attr.Key).SelectedValue );
				}
				else  
				{
					this.HisEn.SetValByKey(attr.Key,this.GetTBByKey("TB_"+attr.Key).Text );
				}
			}
		}
		private bool IsNew=true;
		/// <summary>
		/// ����
		/// </summary>
		public void Btn_Save()
		{
			this.ReSetEnValue();
			if (this.IsNew)
			{
				/* ������½� */
				if(this.HisEn.IsExits)
				{
					this.statusBar1.Text="�Ѿ��д��ڣ����������룡����ʧ�ܣ�";
					//MessageBox .Show("�Ѿ��д��ڣ����������룡","����ʧ��");
				}
				else
				{
					this.HisEn.Insert();
					this.statusBar1.Text="����ɹ�!";

					//this.ResponseWriteBlueMsg_InsertOK();
				}
			}
			else
			{
				this.HisEn.Update();
				this.statusBar1.Text="���³ɹ�!";
				//this.ResponseWriteBlueMsg_UpdataOK();
			}
			//this.HisEn.RetrieveFromDBSources();
			//this.BindData(this.HisEn);
			this.IsNew=false;
		}
		private bool first=true;
		/// <summary>
		/// �½�
		/// </summary>
		public void Btn_New()
		{
			if (this.IsReadonly ==true)
				throw new Exception("�������½���");
			if(first)
			{
				this.SetData(this.HisEn.CreateInstance(),false);
				this.first=false;
			}
			else
				//				this.BindData(this.HisEn);
				this.SetData(this.HisEn.CreateInstance(),false);
		}
		/// <summary>
		/// ȡ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_Close()
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		/// <summary>
		/// ȷ��
		/// </summary>
		public void Btn_Sub()
		{
			this.ReSetEnValue();
			this.HisEn.Update();
			this.ResponseWriteBlueMsg_UpdataOK();
			this.Close();

		}
		/// <summary>
		///ɾ��
		/// </summary>
		public void Btn_Delete()
		{
			System.Windows.Forms.DialogResult result =MessageBox.Show("��Ҫִ��ɾ������ȷ�ϣ�", "��ʾ",
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes )

			{
				//this.ReSetEnValue();
				this.HisEn.Delete();
				this.ResponseWriteBlueMsg_DeleteOK();				 
			}
			 
		}
		#endregion 

		#region ��������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIEn));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gb1 = new BP.Win32.Controls.GB();
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.toolBarButton_New = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton_Save = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton_SaveAndNew = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton_SaveAndClose = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton_Close = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.gb2 = new BP.Win32.Controls.GB();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// gb1
			// 
			this.gb1.Location = new System.Drawing.Point(32, 40);
			this.gb1.Name = "gb1";
			this.gb1.Size = new System.Drawing.Size(312, 64);
			this.gb1.TabIndex = 0;
			this.gb1.TabStop = false;
			this.gb1.Text = "gb1";
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.toolBarButton_New,
																						  this.toolBarButton_Save,
																						  this.toolBarButton_SaveAndNew,
																						  this.toolBarButton_SaveAndClose,
																						  this.toolBarButton_Close});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(706, 28);
			this.bpToolbar1.TabIndex = 1;
			this.bpToolbar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// toolBarButton_New
			// 
			this.toolBarButton_New.ImageIndex = 0;
			this.toolBarButton_New.Tag = "New";
			this.toolBarButton_New.Text = "�½�";
			// 
			// toolBarButton_Save
			// 
			this.toolBarButton_Save.ImageIndex = 1;
			this.toolBarButton_Save.Tag = "Save";
			this.toolBarButton_Save.Text = "����";
			// 
			// toolBarButton_SaveAndNew
			// 
			this.toolBarButton_SaveAndNew.ImageIndex = 2;
			this.toolBarButton_SaveAndNew.Tag = "SaveAndNew";
			this.toolBarButton_SaveAndNew.Text = "���沢�½�";
			// 
			// toolBarButton_SaveAndClose
			// 
			this.toolBarButton_SaveAndClose.ImageIndex = 3;
			this.toolBarButton_SaveAndClose.Tag = "SaveAndClose";
			this.toolBarButton_SaveAndClose.Text = "���沢�ر�";
			// 
			// toolBarButton_Close
			// 
			this.toolBarButton_Close.ImageIndex = 4;
			this.toolBarButton_Close.Tag = "Close";
			this.toolBarButton_Close.Text = "�ر�";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// gb2
			// 
			this.gb2.Location = new System.Drawing.Point(344, 40);
			this.gb2.Name = "gb2";
			this.gb2.Size = new System.Drawing.Size(312, 64);
			this.gb2.TabIndex = 2;
			this.gb2.TabStop = false;
			this.gb2.Text = "gb2";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 305);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(706, 22);
			this.statusBar1.TabIndex = 3;
			this.statusBar1.Text = "statusBar1";
			// 
			// UIEn
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(706, 327);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.gb2);
			this.Controls.Add(this.bpToolbar1);
			this.Controls.Add(this.gb1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "UIEn";
			this.Text = "��Ϣ�༭";
			this.Load += new System.EventHandler(this.UIEn_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			string msg="";			
			try
			{
				switch(e.Button.Text)
				{
					case "�½�":
						this.Btn_New();
						break;
					case "����":
						this.Btn_Save();
						break;
					case "���沢�½�":
						this.Btn_Save();
						this.Btn_New();						
						break;
					case "���沢�ر�":
						this.Btn_Save();
						this.Close();
						break;
					case "�ر�":
						this.Btn_Close();
						break;
					default:
						throw new Exception("error tag = "+e.Button.Tag.ToString() ); 						
				}
			}
			catch(Exception ex)
			{
				this.Alert(msg+ex.Message);
				 
			}
		}

		private void UIEn_Load(object sender, System.EventArgs e)
		{
			this.statusBar1.Text="��л��ѡ��"+SystemConfig.CustomerShortName+"��";
		}

		
	}
}

 
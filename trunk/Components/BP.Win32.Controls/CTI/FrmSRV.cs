using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using BP.DTS;

using BP.CTI;
using BP.CTI.App;

namespace BP.CTI
{
	/// <summary>
	/// FrmSRV ��ժҪ˵����
	/// </summary>
	public class FrmSRV :  BP.Win32.PageBase
	{
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btn_SrvPause;
		private System.Windows.Forms.Button btn_SrvStart;
		private System.Windows.Forms.Button btn_SrvStop;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btn_DBOut;
		private System.Windows.Forms.Button btn_DBContinue;
		private System.Windows.Forms.Button Btn_Hand;
		private System.Windows.Forms.StatusBarPanel statusBarPanel3;
		private System.Windows.Forms.Timer timer1_RunDTS;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btn_WorkState;
		private System.ComponentModel.IContainer components;

		public FrmSRV()
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
			//Card.DoStop();
			this.thisThread.Abort();
			//this.Alert("ss");

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmSRV));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btn_SrvPause = new System.Windows.Forms.Button();
			this.btn_SrvStart = new System.Windows.Forms.Button();
			this.btn_SrvStop = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.timer1_RunDTS = new System.Windows.Forms.Timer(this.components);
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Btn_Hand = new System.Windows.Forms.Button();
			this.btn_DBOut = new System.Windows.Forms.Button();
			this.btn_DBContinue = new System.Windows.Forms.Button();
			this.btn_WorkState = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btn_SrvPause);
			this.groupBox1.Controls.Add(this.btn_SrvStart);
			this.groupBox1.Controls.Add(this.btn_SrvStop);
			this.groupBox1.Location = new System.Drawing.Point(80, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(152, 152);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "�������п���";
			// 
			// btn_SrvPause
			// 
			this.btn_SrvPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_SrvPause.ImageIndex = 9;
			this.btn_SrvPause.ImageList = this.imageList1;
			this.btn_SrvPause.Location = new System.Drawing.Point(32, 64);
			this.btn_SrvPause.Name = "btn_SrvPause";
			this.btn_SrvPause.TabIndex = 2;
			this.btn_SrvPause.Text = "��ͣ";
			this.btn_SrvPause.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_SrvStart
			// 
			this.btn_SrvStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_SrvStart.ImageIndex = 8;
			this.btn_SrvStart.ImageList = this.imageList1;
			this.btn_SrvStart.Location = new System.Drawing.Point(32, 32);
			this.btn_SrvStart.Name = "btn_SrvStart";
			this.btn_SrvStart.TabIndex = 0;
			this.btn_SrvStart.Text = "��ʼ";
			this.btn_SrvStart.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_SrvStop
			// 
			this.btn_SrvStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_SrvStop.ImageIndex = 12;
			this.btn_SrvStop.ImageList = this.imageList1;
			this.btn_SrvStop.Location = new System.Drawing.Point(32, 96);
			this.btn_SrvStop.Name = "btn_SrvStop";
			this.btn_SrvStop.TabIndex = 7;
			this.btn_SrvStop.Text = "ֹͣ";
			this.btn_SrvStop.Click += new System.EventHandler(this.btn_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(8, 200);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(152, 24);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "��ϵͳ����ʱ�Զ�����";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.ImageIndex = 7;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new System.Drawing.Point(376, 232);
			this.button1.Name = "button1";
			this.button1.TabIndex = 4;
			this.button1.Text = "ȷ��";
			this.button1.Click += new System.EventHandler(this.btn_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.ImageIndex = 6;
			this.button2.ImageList = this.imageList1;
			this.button2.Location = new System.Drawing.Point(288, 232);
			this.button2.Name = "button2";
			this.button2.TabIndex = 5;
			this.button2.Text = "�ر�";
			this.button2.Click += new System.EventHandler(this.btn_Click);
			// 
			// timer1_RunDTS
			// 
			this.timer1_RunDTS.Enabled = true;
			this.timer1_RunDTS.Interval = 10000;
			this.timer1_RunDTS.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "CTI���������";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 273);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1,
																						  this.statusBarPanel2,
																						  this.statusBarPanel3});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(594, 22);
			this.statusBar1.TabIndex = 6;
			this.statusBar1.Text = "statusBar1";
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.Text = "statusBarPanel1";
			this.statusBarPanel1.Width = 110;
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.Text = "statusBarPanel2";
			this.statusBarPanel2.Width = 300;
			// 
			// statusBarPanel3
			// 
			this.statusBarPanel3.Text = "statusBarPanel3";
			this.statusBarPanel3.Width = 150;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Btn_Hand);
			this.groupBox2.Controls.Add(this.btn_DBOut);
			this.groupBox2.Controls.Add(this.btn_DBContinue);
			this.groupBox2.Location = new System.Drawing.Point(264, 40);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(144, 152);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "����Դ����ģʽ����";
			// 
			// Btn_Hand
			// 
			this.Btn_Hand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Btn_Hand.ImageIndex = 0;
			this.Btn_Hand.ImageList = this.imageList1;
			this.Btn_Hand.Location = new System.Drawing.Point(32, 104);
			this.Btn_Hand.Name = "Btn_Hand";
			this.Btn_Hand.TabIndex = 2;
			this.Btn_Hand.Text = "�ֶ�";
			this.Btn_Hand.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_DBOut
			// 
			this.btn_DBOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_DBOut.ImageIndex = 11;
			this.btn_DBOut.ImageList = this.imageList1;
			this.btn_DBOut.Location = new System.Drawing.Point(32, 72);
			this.btn_DBOut.Name = "btn_DBOut";
			this.btn_DBOut.TabIndex = 1;
			this.btn_DBOut.Text = "�ѻ�";
			this.btn_DBOut.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_DBContinue
			// 
			this.btn_DBContinue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_DBContinue.ImageIndex = 10;
			this.btn_DBContinue.ImageList = this.imageList1;
			this.btn_DBContinue.Location = new System.Drawing.Point(32, 40);
			this.btn_DBContinue.Name = "btn_DBContinue";
			this.btn_DBContinue.TabIndex = 0;
			this.btn_DBContinue.Text = "����";
			this.btn_DBContinue.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_WorkState
			// 
			this.btn_WorkState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_WorkState.ImageIndex = 4;
			this.btn_WorkState.ImageList = this.imageList1;
			this.btn_WorkState.Location = new System.Drawing.Point(72, 232);
			this.btn_WorkState.Name = "btn_WorkState";
			this.btn_WorkState.Size = new System.Drawing.Size(88, 23);
			this.btn_WorkState.TabIndex = 9;
			this.btn_WorkState.Text = "����״̬";
			this.btn_WorkState.Click += new System.EventHandler(this.btn_Click);
			// 
			// button5
			// 
			this.button5.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button5.ImageIndex = 13;
			this.button5.ImageList = this.imageList1;
			this.button5.Location = new System.Drawing.Point(184, 232);
			this.button5.Name = "button5";
			this.button5.TabIndex = 12;
			this.button5.Text = "ˢ��";
			this.button5.Click += new System.EventHandler(this.btn_Click);
			// 
			// button6
			// 
			this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button6.ImageIndex = 14;
			this.button6.ImageList = this.imageList1;
			this.button6.Location = new System.Drawing.Point(472, 232);
			this.button6.Name = "button6";
			this.button6.TabIndex = 13;
			this.button6.Text = "����";
			this.button6.Click += new System.EventHandler(this.btn_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(448, 48);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 136);
			this.pictureBox1.TabIndex = 14;
			this.pictureBox1.TabStop = false;
			// 
			// FrmSRV
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(594, 295);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.btn_WorkState);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmSRV";
			this.Text = "CTI���������";
			this.Click += new System.EventHandler(this.FrmSRV_Click);
			this.Load += new System.EventHandler(this.FrmSRV_Load);
			this.Closed += new System.EventHandler(this.FrmSRV_Closed);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
 
		public bool DBWorkState
		{
			get
			{
				return Sys.SysConfigs.GetValByKeyBoolen("DBWorkState",false);
			}
		}
		 
		public void SetState()
		{
			if (DBWorkState)
			{
				this.btn_DBContinue.Enabled=false;
				this.btn_DBOut.Enabled=true;
			}
			else
			{
				this.btn_DBContinue.Enabled=true;
				this.btn_DBOut.Enabled=false;
			}

			//this._SysDTSs=null; 

			//this.statusBar1.Panels[1].Text="����"+Card.GetCHsCount(CHState.Connect)+"��ͨ"+Card.GetCHsCount(CHState.Play)+"����"+Card.GetCHsCount(CHState.HangUp);
			 
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					this.statusBar1.Panels[1].Text="����"+DTCard.GetCHsCount( DTCHState.Connect)+"��ͨ"+DTCard.GetCHsCount(DTCHState.Play)+"����"+DTCard.GetCHsCount(DTCHState.HangUp);
					break;
				case CardType.Usbid:
					this.statusBar1.Panels[1].Text="����"+TVCard.GetCHsCount(TVCHState.Dialing)+"����"+TVCard.GetCHsCount(TVCHState.Connect)+"����"+TVCard.GetCHsCount(TVCHState.Play)+"����"+TVCard.GetCHsCount(TVCHState.HangUp);
					break;
				default:
					this.statusBar1.Panels[1].Text="������û�й���";
					break;
			}


			switch(Card.HisCardWorkState)
			{
				case CardWorkState.Runing:
					this.btn_SrvStart.Enabled=false;
					this.btn_SrvPause.Enabled=true;
					this.btn_SrvStop.Enabled=true;
					this.statusBar1.Panels[0].Text="���������С�";
					break;
				case CardWorkState.Pause:
					this.btn_SrvStart.Enabled=true;
					this.btn_SrvPause.Enabled=false;
					this.btn_SrvStop.Enabled=true;
					this.statusBar1.Panels[0].Text="������ͣ��";
					break;
				case CardWorkState.Stop:
				case CardWorkState.Disable:
					this.btn_SrvStart.Enabled=true;
					this.btn_SrvPause.Enabled=false;
					this.btn_SrvStop.Enabled=false;
					this.statusBar1.Panels[0].Text="����ֹͣ��";
					break;
			}
		}
	
		private void btn_Click(object sender, System.EventArgs e)
		{						
			try
			{
				System.Windows.Forms.Button btn = (Button)sender;
				switch(btn.Text)
				{
					case "����":
						this.RunExeFile("http://www.chichengsoft.com","error");						 
						break;
					case "ˢ��":
						break;
					case "����״̬":
						if (Card.HisCardWorkState==CardWorkState.Disable)
						{
							this.Information("����ϵͳû�������������ܲ쿴�忨�Ĺ���״̬��");
							//btn.Enabled=false;
							break;
						}
						FrmWorkState f= new FrmWorkState();
						f.Show();
						break;
					case "��ʼ":
						this.statusBarPanel1.Text="��������...";
						//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Runing);
						Card.DoRuning();

						// ͨ�����Ʊ���������ϵͳ����
						//Card.DoRuning();
						break;
					case "��ͣ":
						//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Pause);
						Card.DoPause();
						// ͨ�����Ʊ���������ϵͳ����
						//Card.DoPause();
						this.statusBarPanel1.Text="������ͣ...";
						break;
					case "ֹͣ":
						//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Stop);
						// ͨ�����Ʊ���������ϵͳ����
						Card.DoStop();
						this.statusBarPanel1.Text="����ֹͣ...";
						break;
					case "����":
						//Sys.SysConfigs.SetValByKey("DBWorkState",1);
						//this.timer1_RunDTS.Start();
						break;
					case "�ѻ�":
						//Sys.SysConfigs.SetValByKey("DBWorkState",0);
						//this.timer1_RunDTS.Stop();
						break;
					case "�ֶ�":
						if (Card.HisCardWorkState==CardWorkState.Runing)
						{
							this.Information("��ֹͣ���з�������������");
							return ;
						}

						if (SystemConfig.CustomerNo.IndexOf("713") > -1 )
						{
							if (this.Question("�ֹ�ִ�л�ȡ����"+BP.SystemConfigOfTax.FK_ZSJG+"���ݣ���Ҫ���������ӵ�ʱ����߸��������������绷��ȷ����\nִ���ڼ������ĵȴ�ִ�н����Ϣ������Ҫ�������Ĳ�����\nȷ��ִ����")==false)
								return ;

							TelDTS tel = new TelDTS();
							tel.Do();
							this.Information("�ֹ�ִ�����..");
							return;
						}

						int i = Card.CurrentDTSs.Count ; 
					 
						if (this.Question("��ǰ����"+i+"��������Ҫִ�У����Ժ��ִ�й�����ϵͳ���������ʾ��\n �ֹ�ִ�л�ȡ�������ݣ���Ҫ���������ӵ�ʱ����߸��������������绷��ȷ����\nִ���ڼ������ĵȴ�ִ�н����Ϣ������Ҫ�������Ĳ�����\nȷ��ִ����")==false)
							return ;
 
						foreach(SysDTS dts in  Card.CurrentDTSs)
						{
							if (this.Question("��ʼִ��"+dts.Name+"��ȷ����ʼִ�У�ȡ����������"))
								dts.Execute(); // ִ�е�ǰ������.
						}

						this.Information("�ֹ�ִ�����..");
						break;
					case "�ͻ���":
						BP.CTI.FrmClient fc = new FrmClient();
						fc.Show();
						break;
					case "ȷ��":
						//this.Information("ϵͳ������ͼ����Ŀ��,��ͨ��˫��ͼ��Ϳ�����ʾ.");
//						this.ShowInTaskbar=false;
//						this.notifyIcon1.Icon=this.Icon;
//						this.notifyIcon1.Visible=true;
						this.WindowState=FormWindowState.Minimized;
						break;
					case "�ر�":
					 

						if (Card.Serial ==null)
						{
							/*����ǿͻ���������*/
							 
							this.Close();
							return ;
						}
 

						if (this.Question("��ȷʵҪֹͣ���з����˳�ϵͳ��?"))
						{
							Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Stop);
							Card.DoStop();
							Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Stop);
							this.thisThread.Abort();
							Card.Disable();
							this.Close();
							return;
						}

						break;
					default:
						throw new Exception("error "+btn.Text);
				}
				 
				Card.ResetParas();
				Card.CtlCardByParas();
				this.SetState();

			}
			catch(Exception ex)
			{
				this.ResponseWriteRedMsg(ex);
			}

		}
		/// <summary>
		/// 
		/// </summary>
		public System.Threading.Thread thisThread = null;

		private void FrmSRV_Load(object sender, System.EventArgs e)
		{
			//Sys.SysConfigs.SetValByKey("DBWorkState",0);
			//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Stop);
			//this.Text=SystemConfig.CustomerShortName+"--���з��������";

			this.Text="���������";
			 
			ThreadStart ts =new ThreadStart( Card.Run )  ; 
			thisThread = new Thread(ts);
			thisThread.Start();
			this.timer1_RunDTS.Start();

			Card.CtlCardByParas();
			this.SetState();


			this.statusBar1.Panels[2].Text="����:"+DateTime.Now.ToString("HH:mm:ss");
 
		}

		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;
			this.Activate();
			this.ShowInTaskbar = true;
			this.Show();
		}

		 
		private int dtsRunNum=0;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			return;
			/* ���ð忨���б������Ա���ӦԶ�̵Ŀ��ơ� */
			if ( DBWorkState )
			{
				/*���������Ҫ����DTS*/
				//Card.CurrentDTSs.Run();
				this.statusBar1.Panels[2].Text="��������:"+DateTime.Now.ToString("HH:mm:ss");
			}
			else
			{
				this.statusBar1.Panels[2].Text="����ֹͣ:"+DateTime.Now.ToString("HH:mm:ss");
			}

			dtsRunNum++;
			if (dtsRunNum==2)			
				this.SetState();

			if (dtsRunNum==4)
				Card.ResetParas();

			 
			if (dtsRunNum>6)
			{
				dtsRunNum=0;
				Card.CtlCardByParas();
			}
 
		}	
		  
		private void notifyIcon1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;
			this.Activate();			 
		}

		private void FrmSRV_Click(object sender, System.EventArgs e)
		{
			this.SetState();
		
		}

		private void FrmSRV_Closed(object sender, System.EventArgs e)
		{
			if (Card.Serial!=null)
			{
				Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Stop);
				Card.DoStop();
				Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Stop);
				this.thisThread.Abort();
				Card.Disable();	 
			}
		}

		 
	}
}

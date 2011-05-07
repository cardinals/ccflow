using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.Sys;
using BP.DTS;
using BP;
namespace BP.Win32.Controls.Comm
{
	/// <summary>
	/// About ��ժҪ˵����
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 273);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(112, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(472, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(112, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(480, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(112, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(480, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "label3";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(112, 168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(344, 96);
			this.label4.TabIndex = 5;
			this.label4.Text = "label4";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(464, 240);
			this.button1.Name = "button1";
			this.button1.TabIndex = 6;
			this.button1.Text = "ȷ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(112, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(424, 48);
			this.label5.TabIndex = 7;
			this.label5.Text = "label5";
			// 
			// About
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.button1;
			this.ClientSize = new System.Drawing.Size(544, 273);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += new System.EventHandler(this.About_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void About_Load(object sender, System.EventArgs e)
		{
			this.Text="����"+BP.SystemConfig.SysName;
			this.label1.Text=BP.SystemConfig.SysName+"       "+BP.SystemConfig.Ver;
			this.label2.Text="��Ȩ����(c) 2000-"+DateTime.Now.Year+" chichengsoft ��������Ȩ��";
			this.label3.Text="����Ʒʹ��Ȩ���� "+BP.SystemConfig.CustomerName;

			this.label5.Text="��վ:http://www.chichengsoft.com \nE-mail:chichengsoft@hotmail.com\nTel:0531-82126470";
			

			this.label4.Text="����:������������ܰ�Ȩ���͹�����Լ��������δ����Ȩ�����Ը��ƻ򴫲������򣬽��ܵ����������º������Ʋã������ڷ�����ɵ�����޶����ܵ����ߡ�";



		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}

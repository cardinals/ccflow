using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.CTI
{
	/// <summary>
	/// FrmCardInfo ��ժҪ˵����
	/// </summary>
	public class FrmCardInfo :  BP.Win32.PageBase
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmCardInfo()
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
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(400, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(328, 224);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "ȷ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(408, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(408, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "label3";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(408, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "label4";
			// 
			// FrmCardInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(440, 273);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmCardInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "��������Ϣ";
			this.Load += new System.EventHandler(this.FrmCardInfo_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void FrmCardInfo_Load(object sender, System.EventArgs e)
		{
			this.Text="Ӳ����Ϣ";
			if (Card.Serial==null)
			{
				//this.toolBar1.Enabled=false;
				this.Information("�������ڿͻ��˲쿴Ӳ����Ϣ��");
				this.Close();
				return;
			}

			switch( BP.CTI.Card.HisCardType)
			{
				case CardType.Usbid:
					this.label1.Text="Ӳ���ͺ�: 2 ·USB������";
					break;
				case CardType.pcmn7api:
					this.label1.Text="Ӳ���ͺ�: 64 ·����������";
					break;
				default:
					break;
			}

			this.label2.Text="����ͨ����:" +Card.ChCount;
			this.label3.Text="���������к�:"+Card.Serial;
			this.label4.Text="�û����:"+BP.SystemConfigOfTax.FK_ZSJG;
		}
	}
}

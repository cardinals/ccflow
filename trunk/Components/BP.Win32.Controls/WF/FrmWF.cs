using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.Win32.Controls.WF
{
	/// <summary>
	/// FrmWF ��ժҪ˵����
	/// </summary>
	public class FrmWF : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmWF()
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
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(292, 42);
			this.toolBar1.TabIndex = 0;
			// 
			// FrmWF
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.toolBar1);
			this.Name = "FrmWF";
			this.Text = "FrmWF";
			this.ResumeLayout(false);

		}
		#endregion
	}
}

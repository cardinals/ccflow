using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.WF.Design
{
	/// <summary>
	/// TreeNodeAttr ��ժҪ˵����
	/// </summary>
	public class TreeNodeAttr : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TreeNodeAttr()
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
            this.SuspendLayout();
            // 
            // TreeNodeAttr
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "TreeNodeAttr";
            this.Text = "��������������";
            this.Load += new System.EventHandler(this.TreeNodeAttr_Load);
            this.ResumeLayout(false);

		}
		#endregion

        private void TreeNodeAttr_Load(object sender, EventArgs e)
        {

        }
	}
}

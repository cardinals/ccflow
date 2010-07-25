using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.Win.WF
{
	/// <summary>
	/// WFForm ��ժҪ˵����
	/// </summary>
	public class WFForm : System.Windows.Forms.Form
	{
        public string ToE(string no,string chVal)
        {
           // return no;
            return BP.Sys.Language.GetValByUserLang(no, chVal);
        }
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WFForm()
		{
			InitializeComponent();
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
            // WFForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(633, 440);
            this.Name = "WFForm";
            this.ShowInTaskbar = false;
            this.Text = "WFForm";
            this.ResumeLayout(false);

		}
		#endregion
	}
}

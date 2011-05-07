using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.Win.Controls;

namespace BP.Win.Controls
{
	/// <summary>
	/// FrmAttrBase ��ժҪ˵����
	/// </summary>
	public class FrmAttrBase : Form
	{
		#region �ؼ�
		private BP.Win.Controls.GB gb1;
		public System.Windows.Forms.ToolTip toolTip1;
		private BP.Win.Controls.Btn btnSave;
		private BP.Win.Controls.Btn btnSub;
		private BP.Win.Controls.Btn btnCancel;
		private BP.Win.Controls.Pan pan1;
		public  BP.Win.Controls.Pan PanBox
		{
			get{ return pan1;}
		}
		private System.ComponentModel.IContainer components;
		#endregion �ؼ�

		#region ���캯�� ��������
		public FrmAttrBase()
		{
			InitializeComponent();
		}
		public Size BoxSize
		{
			get
			{
				return this.ClientSize;
			}
			set
			{
				this.ClientSize = new Size(value.Width ,value.Height + (this.ClientSize.Height-this.gb1.Location.Y));
			}
		}
		protected bool AllowEditImportantAttr = false;
		#endregion ���캯�� ��������


		public virtual void BindData()
		{
		}

		protected virtual void EndEdit()
		{
		}
		public virtual bool Save()
		{
			return true;
		}

		protected void ClearBoxControl( )
		{
			this.pan1.Controls.Clear();
		}
		protected void AddBoxControl( Control con)
		{
			this.pan1.Controls.Add( con );
		}
        protected void ClearCtrl()
        {
            this.pan1.Controls.Clear(); 
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
			this.gb1 = new BP.Win.Controls.GB();
			this.btnSub = new BP.Win.Controls.Btn();
			this.btnCancel = new BP.Win.Controls.Btn();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnSave = new BP.Win.Controls.Btn();
			this.pan1 = new BP.Win.Controls.Pan();
			this.SuspendLayout();
			// 
			// gb1
			// 
			this.gb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gb1.Location = new System.Drawing.Point(0, 166);
			this.gb1.Name = "gb1";
			this.gb1.Size = new System.Drawing.Size(328, 8);
			this.gb1.TabIndex = 1;
			this.gb1.TabStop = false;
			// 
			// btnSub
			// 
			this.btnSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSub.Location = new System.Drawing.Point(178, 178);
			this.btnSub.Name = "btnSub";
			this.btnSub.Size = new System.Drawing.Size(64, 24);
			this.btnSub.TabIndex = 2;
            this.btnSub.Text = this.ToE("OK", "ȷ��"); // "ȷ��";
			this.toolTip1.SetToolTip(this.btnSub, "�����ر�");
			this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(258, 178);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = this.ToE("Cancel","ȡ��");
			this.toolTip1.SetToolTip(this.btnCancel, "ֱ�ӹر�");
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.Location = new System.Drawing.Point(0, 178);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(64, 24);
			this.btnSave.TabIndex = 1;
            this.btnSave.Text = BP.Sys.Language.GetValByUserLang("Save", "����");
			this.toolTip1.SetToolTip(this.btnSave, "��������");
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// pan1
			// 
			this.pan1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pan1.AutoScroll = true;
			this.pan1.BackColor = System.Drawing.SystemColors.Control;
			this.pan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pan1.Location = new System.Drawing.Point(1, 0);
			this.pan1.Name = "pan1";
			this.pan1.Size = new System.Drawing.Size(321, 168);
			this.pan1.TabIndex = 4;
			// 
			// FrmAttrBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(322, 207);
			this.Controls.Add(this.pan1);
			this.Controls.Add(this.btnSub);
			this.Controls.Add(this.gb1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmAttrBase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = this.ToE("Attr", "����");
			this.ResumeLayout(false);

		}
        public string ToE(string no,string chval)
        {
            return BP.Sys.Language.GetValByUserLang( no,chval);
        }
		#endregion

		#region �¼�
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Save();
			}
			catch(Exception ex )
			{
				MessageBox.Show(ex.Message ,"����ʧ�ܣ�");
			}
		}

		private void btnSub_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Save();
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch(Exception ex )
			{
				MessageBox.Show(ex.Message ,"����ʧ�ܣ�");
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#endregion �¼�
	}
}

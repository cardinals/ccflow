using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port ; 
using BP.Web; 
using BP.Web.Controls;

namespace BP.Win32.Controls
{
	/// <summary>
	/// UIDefaultValues ��ժҪ˵����
	/// </summary>
	public class UIWinDefaultValues : System.Windows.Forms.Form 
	{
		#region �ؼ�
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		private BP.Win32.Controls.TB tb1;
		private BP.Win32.Controls.Btn btn1;
		private BP.Win32.Controls.Btn btn2;
		private BP.Win32.Controls.Btn btn3;
		private BP.Win32.Controls.Btn btn4;
		private BP.Win32.Controls.CBL cbl1;

		#region ���ñ���
		#region Ens
		private Entity _HisEn=null;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
	
		public Entity HisEn
		{
			get
			{
				return _HisEn;
			}
			set
			{
				_HisEn=value;
			}
		}
		#endregion 

	    private string _attrKey=null;
		public string AttrKey
		{
			get
			{
				return _attrKey;
			}
			set
			{
				_attrKey=value;

			}
		}
		#endregion

		#region ˽��
		 
		#endregion

		#region ���캯��

		

		public UIWinDefaultValues(Entity en, string attrKey)
		{
		 
			this.HisEn =en;
			this.AttrKey =attrKey;
		 
			this.Text="��ȡ������ʵ��\""+en.EnDesc+"\"��\""+attrKey+"\"Ĭ��ֵ";
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			this.BindCBL(3);
			this.ShowDialog();

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
		#endregion

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.tb1 = new BP.Win32.Controls.TB();
			this.btn1 = new BP.Win32.Controls.Btn();
			this.btn2 = new BP.Win32.Controls.Btn();
			this.btn3 = new BP.Win32.Controls.Btn();
			this.btn4 = new BP.Win32.Controls.Btn();
			this.cbl1 = new BP.Win32.Controls.CBL();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.toolBarButton1,
																						  this.toolBarButton2,
																						  this.toolBarButton3,
																						  this.toolBarButton4,
																						  this.toolBarButton5,
																						  this.toolBarButton6});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(688, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// tb1
			// 
			this.tb1.Location = new System.Drawing.Point(16, 56);
			this.tb1.Name = "tb1";
			this.tb1.Size = new System.Drawing.Size(104, 21);
			this.tb1.TabIndex = 2;
			this.tb1.Text = "tb1";
			// 
			// btn1
			// 
			this.btn1.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn1.Location = new System.Drawing.Point(128, 56);
			this.btn1.Name = "btn1";
			this.btn1.Size = new System.Drawing.Size(120, 23);
			this.btn1.TabIndex = 3;
			this.btn1.Text = "����Ϊ�ҵ�Ĭ��ֵ";
			// 
			// btn2
			// 
			this.btn2.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn2.Location = new System.Drawing.Point(264, 56);
			this.btn2.Name = "btn2";
			this.btn2.Size = new System.Drawing.Size(120, 23);
			this.btn2.TabIndex = 4;
			this.btn2.Text = "����Ϊȫ��Ĭ��ֵ";
			// 
			// btn3
			// 
			this.btn3.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn3.Location = new System.Drawing.Point(472, 56);
			this.btn3.Name = "btn3";
			this.btn3.TabIndex = 5;
			this.btn3.Text = "ȷ��";
			this.btn3.Click += new System.EventHandler(this.btn3_Click);
			// 
			// btn4
			// 
			this.btn4.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn4.Location = new System.Drawing.Point(392, 56);
			this.btn4.Name = "btn4";
			this.btn4.TabIndex = 6;
			this.btn4.Text = "ȡ��";
			this.btn4.Click += new System.EventHandler(this.btn4_Click);
			// 
			// cbl1
			// 
			this.cbl1.Location = new System.Drawing.Point(0, 88);
			this.cbl1.Name = "cbl1";
			this.cbl1.Size = new System.Drawing.Size(680, 212);
			this.cbl1.TabIndex = 7;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Text = "�ҵ�Ĭ��ֵ";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Text = "ȫ��Ĭ��ֵ";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Text = "��ʷ��¼";
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.Text = "ɾ��";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.Text = "����";
			// 
			// UIWinDefaultValues
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(688, 325);
			this.Controls.Add(this.cbl1);
			this.Controls.Add(this.btn4);
			this.Controls.Add(this.btn3);
			this.Controls.Add(this.btn2);
			this.Controls.Add(this.btn1);
			this.Controls.Add(this.tb1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "UIWinDefaultValues";
			this.Text = "UIDefaultValues";
			this.ResumeLayout(false);

		}
		#endregion

		private void BindCBL(int i)
		{
			try
			{
				string sql=""; 
				switch(i)
				{
					case 1:
						sql="SELECT DefaultVal, DefaultVal as Text FROM Sys_UIDefaultValue WHERE EmpId="+WebUser.No+" and ClassName='"+this.HisEn.ToString()+"' and AttrKey='"+this.AttrKey+"'" ;
						break;
					case 2:
						sql="SELECT DefaultVal, DefaultVal as Text  FROM Sys_UIDefaultValue WHERE ClassName='"+this.HisEn.ToString()+"' and AttrKey='"+this.AttrKey+"' and EmpId=0";

						break;
					case 3:
						string field=this.HisEn.EnMap.GetFieldByKey(this.AttrKey) ; 
						sql="SELECT DISTINCT TOP 40  "+field+" as DefaultVal,  "+field+" as Text  FROM  "+this.HisEn.EnMap.PhysicsTable+" WHERE  len(rtrim(ltrim("+field+" )) ) > 0 " ;
						break;
					default:
						throw new Exception("errory ");
				}

				DataTable dt = DBAccess.RunSQLReturnTable(sql); 
				this.cbl1.BindTable(dt,"DefaultVal","DefaultVal") ; 

				//this.cbl1.bin
				 
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message) ; 
				//this.Alert(ex);
			}

		}

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			string msg="";
			
			try 
			{
				switch(e.Button.Text)
				{
					case "����":
						msg="ִ�б�����ִ���"; 
						//this.Btn_Save();
						break;
					case "�ر�":
						this.Close();
						break;
					case "�½�":
						msg="ִ���½����ִ���";
						//this.Btn_New();
						break;
					default:
						//this.dg1.TableStyles[0].clo
						// this.dg1.TableStyles[0].GridColumnStyles[0].HeaderText
						break;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(msg+ex.Message ) ; 

				//this.Alert(msg+ex.Message);
				 
			}
		}

		private void btn3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btn4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}

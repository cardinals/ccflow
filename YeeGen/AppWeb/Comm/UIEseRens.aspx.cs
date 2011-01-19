using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.Sys;
using BP.DA;
using BP.En;
using BP.Web.Controls;

namespace BP.Web.Comm.UI
{
	/// <summary>
	/// SystemClass ��ժҪ˵����
	/// </summary>
	public partial class UIUserEns : WebPage
	{
		protected BP.Web.Controls.Btn Btn1;

		protected BP.Web.Controls.ToolbarTB TB_EnsName
		{
			get
			{
				return this.BPToolBar1.GetTBByID("TB_EnsName");
			}
		}

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.BPToolBar1.ButtonClick += new System.EventHandler(this.BPToolBar1_ButtonClick);

			if (this.IsPostBack==false)
			{
				this.BPToolBar1.AddLab("sss","�û���ά����Ϣ");
				this.BPToolBar1.AddLab("sss","ѡ��һ����");
				this.BPToolBar1.AddBtn(NamesOfBtn.Edit);
				//				//this.BPToolBar1.AddLab("ss2","����һ������������");
				//				this.BPToolBar1.AddTB("TB_EnsName");
				//				this.BPToolBar1.AddBtn(NamesOfBtn.Export);
				//				this.TB_EnsName.Text="BP.En.Entity";
				this.LB1.Items.Clear();
				SysEnPowerAbles ens = new SysEnPowerAbles();
				ens.RetrieveAll();
				foreach(SysEnPowerAble en in ens)
				{
					this.LB1.Items.Add(new ListItem(en.Name,en.EnsEnsName ) ) ; 
				}
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

	 
		

		 

		private void BPToolBar1_ButtonClick(object sender, System.EventArgs e)
		{
			try
			{
				ToolbarBtn btn = (ToolbarBtn)sender;
				switch(btn.ID)
				{
					case NamesOfBtn.Export:
						this.ExportEntityToExcel(this.TB_EnsName.Text);
						return;
					case NamesOfBtn.Edit:
						this.WinOpen("UIEns.aspx?EnsName="+this.LB1.SelectedItemStringVal);
						return;
					default:
						throw new Exception("error");

				}
			}
			catch(Exception ex)
			{
				this.ResponseWriteRedMsg(ex);
			}
		}
	}
}

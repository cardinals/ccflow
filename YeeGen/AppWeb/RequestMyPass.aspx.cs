using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Web.Controls;
using BP.Sys;
using BP.YG;

namespace HiTax
{
	/// <summary>
	/// RequestMyPass ��ժҪ˵����
	/// </summary>
	public partial class RequestMyPass : YGPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Customer c = new Customer();
			if (c.IsExit("No",this.TB_No.Text))
			{
				ToMsgPage("ϵͳ�Ѿ����������뷢�͵�["+c.Email+"]������ա����������������Ϣ");
				//this.Response.Write("<script language=javascript >alert('ϵͳ�Ѿ����������뷢�͵�[]������ա�');</script>");
				//this.Response.Redirect("Login.aspx",true);
			}
			else
			{
				this.Label1.Text="�û�������";
			}
		}
	}
}

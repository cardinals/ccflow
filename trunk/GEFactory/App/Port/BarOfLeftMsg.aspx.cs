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

namespace BP.Web.GTS.App.Port
{
	/// <summary>
	/// BarOfLeftMsg ��ժҪ˵����
	/// </summary>
	public partial class BarOfLeftMsg : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //if (SystemConfigOfTax.IsWorkOn == false)
            //{
            string path = this.Request.ApplicationPath;
            string str = "";
            string no = WebUser.No;
            //   str = "<a href='/Edu/App/Port/Signin.aspx'  target='mainfrm' ><img src='OnlineUser.gif' border='0' /></a><font color='#000000' size=2 >&nbsp;�л�(" + WebUser.No + ")</a>";
            //this.Response.Write(str);

            str = "<img src='OnlineUser.gif' border='0' /><font color='#000000' size=2 >&nbsp;" + WebUser.No + "</font>";
            this.Response.Write(str);
            return;
            // }
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
	}
}

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
using BP.YG;
using BP.En;
using BP.DA;
using BP.Web;

namespace BP.YG
{
	/// <summary>
	/// Login ��ժҪ˵����
	/// </summary>
	public partial class Login : BP.YG.YGPage
	{
        public string LoginResult_
        {
            get
            {
                string no = this.Request.QueryString["LoginResult"];
                if (no == null)
                    return "No";
                return no;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                HttpCookie hc = this.Request.Cookies["HiTax"];
                if (hc != null)
                {
                    this.TB_No.Text = hc.Values["UserNo"];
                    string script = "<script language=javascript>function setFocus(ctl) {if (document.forms[0][ctl] !=null )  { document.forms[0][ctl].focus(); } } setFocus('" + this.TB_Pass.ClientID + "'); </script>";
                    this.RegisterStartupScript("func", script);
                }
                else
                {
                    //this.TB_No.Text=hc.Values["UserNo"];
                    string script1 = "<script language=javascript>function setFocus(ctl) {if (document.forms[0][ctl] !=null )  { document.forms[0][ctl].focus(); } } setFocus('" + this.TB_No.ClientID + "'); </script>";
                    this.RegisterStartupScript("func", script1);
                }
                this.Label1.Text = Global.MsgOfReLogin;
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

		public string GoWhere
		{
			get
			{
				string wherego=this.Request.QueryString["GoWhere"];
				if (wherego==null)
					  wherego=this.Request.QueryString["WhereGo"];

				return wherego;
			}
		}

        protected void Button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                Customer c = new Customer();
                c.No = this.TB_No.Text;
                if (c.IsExit("No", this.TB_No.Text) == false)
                    throw new Exception("�û���[" + c.No + "]�����ڡ�");
                else
                    c.Retrieve();

                if (c.CheckPass(this.TB_Pass.Text) == false)
                    throw new Exception("��������û�������<BR> ��ʾ��<BR>1)�������ִ�Сд��<BR>2)�������������������ͨ�����һ�ȡ���롣");

                Global.Signin(c, this.CheckBox1.Checked );

                string comefrom = this.GoWhere;
                if (comefrom == null)
                {
                    comefrom = Global.GoWhere;
                    if (comefrom == null)
                        comefrom = "FAQ.aspx";
                    else
                        Global.GoWhere = null;
                }
                else
                {
                    string url = this.Request.RawUrl;
                    if (url.ToLower().Contains("login.aspx") || url.ToLower().Contains("reguser.aspx"))
                    {
                        comefrom = "FAQ.aspx";
                    }
                    else
                    {

                        if (comefrom.IndexOf("?") != -1)
                            comefrom = comefrom + "&from=23&" + url.Substring(url.IndexOf("?"));
                        else
                            comefrom = comefrom + "?from=23&" + url.Substring(url.IndexOf("?"));

                        if (comefrom.IndexOf("Msg.aspx") != -1)
                            comefrom = "FAQ.aspx";
                    }
                }
                this.Response.Redirect(comefrom, true);
            }
            catch (Exception ex)
            {
                this.Label1.Text = ex.Message;
            }
        }
	}
}

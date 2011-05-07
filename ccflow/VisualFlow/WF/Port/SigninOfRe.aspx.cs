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
using BP.Port;


namespace BP.Web
{
    /// <summary>
    /// SignInOfRe ��ժҪ˵����
    /// </summary>
    public partial class SignInOfRe : Page
    {
        public string RawUrl
        {
            get
            {
                return ViewState["RawUrl"] as string;
            }
            set
            {
                ViewState["RawUrl"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string script = "<script language=javascript>function setFocus(ctl) {if (document.forms[0][ctl] !=null )  { document.forms[0][ctl].focus(); } } setFocus('" + this.TB_Pass.ClientID + "'); </script>";
            this.RegisterStartupScript("func", script);
            if (this.Request.QueryString["Token"] != null)
            {
                HttpCookie hc1 = this.Request.Cookies["CCS"];
                if (hc1 != null)
                {
                    if (this.Request.QueryString["Token"] == hc1.Values["Token"])
                    {
                        Emp em = new Emp(this.Request.QueryString["No"]);
                        WebUser.SignInOfGenerLang(em, "CH");
                        WebUser.Token = this.Request.QueryString["Token"];
                        Response.Redirect("Home.htm", false);
                        return;
                    }
                }
            }

            if (this.IsPostBack == false)
            {
                this.TB_No.Attributes["background-image"] = "url('beer.gif')";
                HttpCookie hc = this.Request.Cookies["CCS"];
                if (hc != null)
                {
                    this.TB_No.Text = hc.Values["UserNo"];
                }
            }
            if (this.Request.QueryString["IsChangeUser"] != null)
            {

            }

            if (this.Request.Browser.MajorVersion < 6)
            {
                this.Response.Write("�Բ���ϵͳ��⵽����ǰʹ�õ�IE�汾��[" + this.Request.Browser.Version + "]��ϵͳ�����ڵ�ǰ��IE����������������ȷ��ʹ�ô�ϵͳ����������IE6.0������<a href='../IE6.rar'>��������IE6.0</A>�����غ󣬽⿪ѹ���ļ������� ie6setup.exe��������������µ� [" + BP.SystemConfig.ServiceTel + "], ���߷� Mail [" + BP.SystemConfig.ServiceMail + "]��");
                this.Btn1.Enabled = false;
                this.TB_No.Enabled = false;
                this.TB_Pass.Enabled = false;
            }

            HttpCookie cookie = new HttpCookie("CCS");
        }

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN���õ����� ASP.NET Web ���������������ġ�
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

        protected void Btn1_Click(object sender, System.EventArgs e)
        {
            try
            {
                Emp em = new Emp(this.TB_No.Text);
                //		if (  this.TB_Pass.Text=="test" || SystemConfig.IsDebug || em.CheckPass(this.TB_Pass.Text) ) 
                if (SystemConfig.IsDebug || em.CheckPass(this.TB_Pass.Text))
                {
                    //if (this.Request.QueryString["IsChangeUser"]!=null)
                    /* ����Ǹ����û�.*/
                    if (this.Session["OID"] != null)
                    {
                        string no = WebUser.No;
                        this.Session.Clear();
                        //OnlineUserManager.ReomveUser( no );
                    }

                    WebUser.SignInOfGenerLang(em, null);
                    WebUser.Token = this.Session.SessionID;
                    string url1 = "../Home.aspx";
                    if (this.RawUrl == null)
                    {
                        Response.Redirect(url1, false);
                    }
                    else
                    {
                        string url = this.RawUrl;
                        if (url.ToLower().IndexOf("signin.aspx") > 0)
                        {
                            Response.Redirect(url1, true);
                            return;
                        }
                        Response.Redirect(url1, true);
                    }
                    return;
                }
                else
                {
                    throw new Exception("@�������@����Ƿ�����CapsLock");
                }
            }
            catch (System.Exception ex)
            {
                this.Response.Write("<font color=red ><b>@�û����������!@����Ƿ�����CapsLock.@����ϸ����Ϣ:" + ex.Message + "</b></font>");

            }
        }

    }
}

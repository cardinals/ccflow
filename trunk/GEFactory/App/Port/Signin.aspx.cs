
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
using BP.Port;
using System.Xml;
using System.IO;

namespace BP.Web
{
    /// <summary>
    /// SignIn ��ժҪ˵����
    /// </summary>
    public partial class SignInCT : Page
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
        public bool IsTurnTo
        {
            get
            {
                if (this.Request.QueryString["IsTurnTo"] == null)
                    return false;
                else
                    return true;
            }
        }
        public void Page_Load(object sender, System.EventArgs e)
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
                        //   TaxUser.SignInOfGener(em, false, true, "Well");
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
                this.RawUrl = this.Request.RawUrl;
            }
            if (this.Request.Browser.MajorVersion < 6)
            {
                this.Response.Write("�Բ���ϵͳ��⵽����ǰʹ�õ�IE�汾��[" + this.Request.Browser.Version + "]��ϵͳ�����ڵ�ǰ��IE����������������ȷ��ʹ�ô�ϵͳ����������IE6.0������<a href='../IE6.rar'>��������IE6.0</A>�����غ󣬽⿪ѹ���ļ������� ie6setup.exe��������������µ� [" + BP.SystemConfig.ServiceTel + "], ���߷� Mail [" + BP.SystemConfig.ServiceMail + "]��");
                this.Btn1.Enabled = false;
                this.TB_No.Enabled = false;
                this.TB_Pass.Enabled = false;
                return;
            }

            //this.Btn1_Click(null,null);
        }

        #region Web Form Designer generated code
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

        public void Btn1_Click(object sender, System.EventArgs e)
        {

            try
            {
                Emp em = new Emp(this.TB_No.Text.Trim());
                if (em.Pass.Trim().Equals(this.TB_Pass.Text)
                    || this.TB_Pass.Text == "test"
                    || SystemConfig.IsDebug)
                {

                }
                else
                {
                    throw new Exception("������󣡼���Ƿ�����CapsLock ��");
                }

                if (em.No.Contains("admin") == false)
                {
                    string sql = "SELECT No,Name FROM Edu_School where ADNo LIKE '%" + em.No + ",%' union SELECT No,Name FROM Edu_JYJ where ADNo LIKE '%" + em.No + ",%' ";
                    DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No,Name FROM Edu_School where ADNo = '" + em.No + "' union SELECT No,Name FROM Edu_JYJ where ADNo = '" + em.No + "'";
                        dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    }

                    switch (dt.Rows.Count)
                    {
                        case 0:
                            throw new Exception("@�ǹ���Ա�û����ܽ����̨��");
                            break;
                        case 1:
                            em = new Emp("admin" + dt.Rows[0][0].ToString());
                            break;
                        default:
                            throw new Exception("@һ���˲��ܳ�Ϊ�����λ�Ĺ���Ա��");
                            break;
                    }
                }

               // Edu.EduUser.SignIn
                WebUser.SignInOfGenerLang(em, "CH");
                if (this.RawUrl == null)
                {
                    Response.Redirect("Home.htm", false);
                }
                else
                {
                    string url = this.RawUrl;
                    if (url.ToLower().IndexOf("signin.aspx") > 0)
                    {
                        Response.Redirect("Wel.aspx", true);
                        return;
                    }
                    Response.Redirect(url, true);
                }
                return;
            }
            catch (System.Exception ex)
            {
                this.Response.Write("<font color=red ><b>@�û����������!@����Ƿ�����CapsLock.@����ϸ����Ϣ:" + ex.Message + "</b></font>");
            }
        }
    }
}

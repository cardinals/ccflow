using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using Tax666.SystemFramework;
using Tax666.Common;

namespace Tax666.AppWeb
{
    /// <summary>
    ///	����aspxҳ��Ļ��ࡣ
    ///	<remarks>
    ///	����̳���System.Web.UI.Page��
    ///	</remarks>
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        #region ˽�б�������
        /// <summary>
        /// ��ǰҳ���Ƿ�POST����
        /// </summary>
        protected internal bool ispost;

        /// <summary>
        /// ��ǰҳ���Ƿ�GET����
        /// </summary>
        protected internal bool isget;

        /// <summary>
        /// ��վ����������Ϣ��
        /// </summary>
        protected internal SiteConfigInfo config;

        /// <summary>
        /// ������־������
        /// </summary>
        private const String UNHANDLED_EXCEPTION = "Unhandled Exception:";

        /// <summary>
        /// Session�е�¼��ͨ�û��ļ����Ƴ�����
        /// </summary>
        private const String KEY_CACHEUSER = "Cache:User:";

        /// <summary>
        /// Session�к�̨�����û�Sessions��
        /// </summary>
        private const String KEY_CACHEADMINUSER = "Cache:AdminUser:";

        #endregion

        #region ���캯��
        public PageBase()
        {
            config = SiteConfigs.GetConfig();

            //��ֹҳ�滺��
            if (config.Nocacheheaders == 1)
            {
                System.Web.HttpContext.Current.Response.BufferOutput = false;
                System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                System.Web.HttpContext.Current.Response.Expires = 0;
                System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
                System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            }

            //�Ƿ���Ȩʹ�ø�ϵͳ��
            if (config.SiteEnabled == 0)
            {
                new Terminator().ThrowError("��վδ��Ȩ�޷�����ʹ�ã�����ͷ���ϵ��");
            }

            // ���IP��������б�������������ж�
            //if (config.Ipaccess.Trim() != "")
            //{
            //    string[] regctrl = WebUtility.SplitString(config.Ipaccess, "\r\n");
            //    if (!WebUtility.InIPArray(WebUtility.GetIP(), regctrl))
            //        new Terminator().Throw("��Ǹ��ϵͳ������IP�����б����ƣ����޷����ʱ���վ��");
            //}

            // ���IP��ֹ�����б�������������ж�
            //if (config.Ipdenyaccess.Trim() != "")
            //{
            //    string[] regctrl = WebUtility.SplitString(config.Ipdenyaccess, "\n");
            //    if (WebUtility.InIPArray(WebUtility.GetIP(), regctrl))
            //        new Terminator().Throw("��Ǹ������IP�ѱ���ֹ���ʱ�ϵͳ��");
            //}

            ispost = WebRequests.IsPost();
            isget = WebRequests.IsGet();
        }
        #endregion

        #region ���Web Pageҳ���ͷ����Ϣ
        /// <summary>
        /// ���Pageҳ���ͷ����Ϣ��
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        public string PageHeaderInfo(string pageTitle) 
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html;\" />\r\n");

            if (config.Webtitle != "")
                sb.AppendFormat("<title>{0} - {1}</title>\r\n", pageTitle, config.Webtitle);
            else
                sb.AppendFormat("<title>{0} - �������˰����ѯ�߶˷����Ż�</title>\r\n", pageTitle);

            if (config.Seokeywords != "")
                sb.AppendFormat("<meta name=\"keywords\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seokeywords).Replace("\"", " "));
            if (config.Seodescription != "")
                sb.AppendFormat("<meta name=\"description\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seodescription).Replace("\"", " "));

            sb.Append("<link rel=\"Shortcut Icon\" href=\"www.zmsoft.net/favicon.ico\"  type=\"image/x-icon\" />\r\n");
            sb.Append("<link rel=\"Bookmark\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n");  //���IE6��IE7��IE8��ʽ����������

            string defaultThemepath = "Default";
            if (config.ThemeID > 0 && config.Themepath != "")
                defaultThemepath = config.Themepath;
            sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}App_Themes/{1}/default.css\" />\r\n", UrlBase, defaultThemepath);

            //���JavaScript���ԣ�
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/jquery-1.3.2.min.js\"></script>\r\n", UrlBase);
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/default.js\"></script>\r\n", UrlBase);
            return sb.ToString();

        }

        /// <summary>
        /// ��Ա����ҳ��ͷ����Ϣ
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        public string PageAccountHeader(string pageTitle)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html;\" />\r\n");

            if (config.Webtitle != "")
                sb.AppendFormat("<title>{0} - {1}</title>\r\n", pageTitle, config.Webtitle);
            else
                sb.AppendFormat("<title>{0} - �������˰����ѯ�߶˷����Ż�</title>\r\n", pageTitle);

            if (config.Seokeywords != "")
                sb.AppendFormat("<meta name=\"keywords\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seokeywords).Replace("\"", " "));
            if (config.Seodescription != "")
                sb.AppendFormat("<meta name=\"description\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seodescription).Replace("\"", " "));

            sb.Append("<link rel=\"Shortcut Icon\" href=\"www.zmsoft.net/favicon.ico\"  type=\"image/x-icon\" />\r\n");
            sb.Append("<link rel=\"Bookmark\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n");  //���IE6��IE7��IE8��ʽ����������

            string defaultThemepath = "Default";
            if (config.ThemeID > 0 && config.Themepath != "")
                defaultThemepath = config.Themepath;
            sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}App_Themes/{1}/account.css\" />\r\n", UrlBase, defaultThemepath);

            //���JavaScript���ԣ�
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/jquery-1.3.2.min.js\"></script>\r\n", UrlBase);
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/default.js\"></script>\r\n", UrlBase);
            return sb.ToString();

        }

        /// <summary>
        /// ��ӹ���ҳ���ҳͷ����
        /// </summary>
        /// <returns></returns>
        public string AdminHeaderInfo()
        {
            StringBuilder sb = new StringBuilder();

            if (config.Webtitle != "")
                sb.Append("<title>" + config.Webtitle + " - �������˰����ѯ�߶˷����Ż�</title>\r\n");
            else
                sb.Append("<title>�������˰����ѯ�߶˷����Ż�</title>\r\n");

            if (config.Seokeywords != "")
                sb.Append("<meta name=\"keywords\" content=\"" + WebUtility.RemoveHtml(config.Seokeywords).Replace("\"", " ") + "\" />\r\n");
            if (config.Seodescription != "")
                sb.Append("<meta name=\"description\" content=\"" + WebUtility.RemoveHtml(config.Seodescription).Replace("\"", " ") + "\" />\r\n");

            sb.Append("<link rel=\"Shortcut Icon\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<link rel=\"Bookmark\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n");  //���IE6��IE7��IE8��ʽ����������

            sb.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + UrlBase + "App_Themes/Manager/style.css\"/>\r\n");

            //���JavaScript���ԣ�
            sb.Append("<script  language=\"javascript\" type=\"text/javascript\" src=\"" + UrlBase + "Manager/JScript/admin.js\"></script>\r\n");
            sb.Append("<script  language=\"javascript\" type=\"text/javascript\" src=\"" + UrlBase + "Manager/JScript/checkform.js\"></script>\r\n");

            return sb.ToString();
        }
        #endregion

        #region ��ͨ�û��͹���Ա�û��������
        /// <value>
        /// ��ͨ�û��ĵ�¼Session��Ϣ��UserInfo����
        /// </value>
        public DataSet UserInfo
        {
            get
            {
                try
                {
                    return (DataSet)(Session[KEY_CACHEUSER]);
                }
                catch
                {
                    return (null);
                }
            }
            set
            {
                if (null == value)
                {
                    Session.Remove(KEY_CACHEUSER);
                    Session.Abandon();
                }
                else
                {
                    Session[KEY_CACHEUSER] = value;
                }
            }
        }

        /// <summary>
        /// ��̨�������Ա��¼��Sessions��
        /// </summary>
        public DataSet AdminUserInfo
        {
            get
            {
                try
                {
                    return (DataSet)(Session[KEY_CACHEADMINUSER]);
                }
                catch
                {
                    return (null);
                }
            }
            set
            {
                if (null == value)
                {
                    Session.Remove(KEY_CACHEADMINUSER);
                    Session.Abandon();
                }
                else
                {
                    Session[KEY_CACHEADMINUSER] = value;
                }
            }
        }
        #endregion

        #region ��������ʾҳ��ʱ���������Ĵ���
        /// <summary>
        ///	��������ʾҳ��ʱ���������Ĵ���
        /// </summary>
        /// <param name="e">����event���ݵ�EventArgs</param>
        protected override void OnError(EventArgs e)
        {
            ApplicationLog.WriteError(ApplicationLog.FormatException(Server.GetLastError(), UNHANDLED_EXCEPTION));
            base.OnError(e);
        }
        #endregion

        #region ��ȡCSS�ļ���·��
        /// <summary>
        /// ��ȡCSS�ļ���·����
        /// </summary>
        /// <param name="cssName"></param>
        /// <returns></returns>
        public string GetCSSPath(string cssName)
        {
            StringBuilder sb = new StringBuilder();
            //string defaultThemepath = "Default";
            //if (config.ThemeID > 0 && config.Themepath != "")
            //    defaultThemepath = config.Themepath;

            ////�˴��ж��ļ��Ƿ�Ϸ���
            //string extName = cssName.Substring(cssName.Length-2,3);

            sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}{1}\"/>\r\n",UrlBase,cssName);
            return sb.ToString();
        }
        #endregion

        #region ����Ա�û����У��
        /// <summary>
        /// ����Ա�û����У��
        /// </summary>
        public void PageValidCheck()
        {
            if (AdminUserInfo == null)
            {
                //��ת����¼ҳ�棻
                HttpContext.Current.Response.Redirect(string.Format("{0}Manager/AdminLogin.aspx", UrlBase), true);
            }
        }
        #endregion

        #region ���ԣ���վ�������ڵ������Ŀ¼·��
        /// <summary>
        ///	��վ�������ڵ������Ŀ¼·��
        /// ��ʽ��http://Tax666:8080/��
        /// </summary>
        public String UrlBase
        {
            get
            {
                return WebRequests.GetWebUrl();
            }
        }
        #endregion

    }
}

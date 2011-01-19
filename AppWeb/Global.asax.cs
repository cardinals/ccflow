using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.Security;
using Tax666.SystemFramework;
using Tax666.Common;

namespace Tax666.AppWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ApplicationConfiguration.OnApplicationStart(Context.Server.MapPath(Context.Request.ApplicationPath));
            string configPath = Path.Combine(Context.Server.MapPath(Context.Request.ApplicationPath), "remotingclient.cfg");
            if (File.Exists(configPath))
                RemotingConfiguration.Configure(configPath, false);

            Application["user_sessions"] = 0;
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (Request.Path.IndexOf('\\') >= 0 || Path.GetFullPath(Request.PhysicalPath) != Request.PhysicalPath)
                new Terminator().ThrowError("�Բ��������ʵ�ҳ�治���ڣ�����������ĵ�ַ�Ƿ���ȷ��");
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            //�Ự������
            Application.Lock();
            Application["user_sessions"] = (int)Application["user_sessions"] + 1;
            Application.UnLock();
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            Application.Lock();
            Application["user_sessions"] = (int)Application["user_sessions"] - 1;
            Application.UnLock();

            // Session�еļ����Ƴ���
            const String KEY_CACHEUSER = "Cache:User:";			            //�����û���Session-UserInfo��
            const String KEY_CACHEADMINUSER = "Cache:AdminUser:";			//��̨�����û�Session-AdminUserInfo;

            DataSet UserInfo = (DataSet)(Session[KEY_CACHEUSER]);
            DataSet AdminUserInfo = (DataSet)(Session[KEY_CACHEADMINUSER]);
        }

        #region ȫ�ֵ����Ի򷽷�
        /// <summary>
        /// ����ϵͳ��װ���Web��Ŀ¼�����·��(/Tax666 )��
        /// </summary>
        /// <remarks>
        /// ��ASPX��ASCX�еĵ��ã�
        /// 1�����ã� %@ Import Namespace="Tax666.AppWeb" %��
        /// 2����%=Global.WebPath%��/Images/Logo1.gif
        /// </remarks>
        public static string WebPath
        {
            get
            {
                if (!HttpContext.Current.Request.Url.IsDefaultPort)
                {
                    return @"http://" + string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString());
                }
                else
                {
                    return @"http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                }
            }
        }

        /// <summary>
        /// ��Ӵ����̹���ҳ���ҳͷ����
        /// </summary>
        /// <returns></returns>
        public static string GetHeadInfo()
        {
            PageBase page = new PageBase();
            string header = page.AdminHeaderInfo();

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        /// <summary>
        /// ��ȡCSS�ļ���·����
        /// </summary>
        /// <param name="cssName"></param>
        /// <returns></returns>
        public static string GetCSSPathInfo(string cssName)
        {
            PageBase page = new PageBase();
            string header = page.GetCSSPath(cssName);

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        /// <summary>
        /// ���Pageҳ���ͷ����Ϣ
        /// </summary>
        /// <param name="secondTitle">������</param>
        /// <returns></returns>
        public static string GetHeadInfo(string secondTitle)
        {
            PageBase page = new PageBase();
            string header = page.PageHeaderInfo(secondTitle);

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        /// <summary>
        /// ����û��������� Pageҳ���ͷ����Ϣ
        /// </summary>
        /// <param name="secondTitle"></param>
        /// <returns></returns>
        public static string GetUserHeadInfo(string secondTitle)
        {
            PageBase page = new PageBase();
            string header = page.PageAccountHeader(secondTitle);

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        #endregion
    }
}
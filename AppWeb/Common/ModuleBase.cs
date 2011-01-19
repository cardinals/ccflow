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
    ///	����ascx�ؼ�ҳ��Ļ���,UserControl�û��ؼ��Ĺ�����,ͬʱ������ҳ��Ļ���,���е�ҳ�涼�̳и���.
    ///	<remarks>
    ///	����̳���System.Web.UI.UserControl��
    ///	</remarks>
    /// </summary>
    public class ModuleBase : System.Web.UI.UserControl
    {
        #region ��������
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

        #region ����
        /// <summary>
        ///	��վ�������ڵ������Ŀ¼·��
        /// ��ʽ��http://192.168.8.170:8008/Tax666/ �� http://192.168.8.170:8008/��
        /// </summary>
        public String UrlBase
        {
            get
            {
                return WebRequests.GetWebUrl();
            }
        }

        /// <value>
        /// UserInfo��������get����set�ѵ�¼���û���������ݡ�
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
        #endregion

        #region ���ԣ���̨�������Ա��¼��Sessions
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

    }
}
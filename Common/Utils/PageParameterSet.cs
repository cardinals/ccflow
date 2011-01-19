using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace Tax666.Common
{
    public class PageParameterSet
    {
        #region ��ҳÿҳ��¼��(Ĭ��10)
        /// <summary>
        /// ��ҳÿҳ��¼��(Ĭ��10)
        /// </summary>
        public static int PageSize
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["PageSize"] == null)
                {
                    return 10;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Request.Cookies["PageSize"].Value);
                }
            }
            set
            {
                HttpContext.Current.Response.Cookies["PageSize"].Value = value.ToString();
            }
        }
        #endregion

        #region �û����߹���ʱ�� (��)Ĭ��30�� ����û��ڵ�ǰ�趨��ʱ����û���κβ���,���ᱻϵͳ�Զ��˳�
        /// <summary>
        /// �û����߹���ʱ�� (��)Ĭ��30�� ����û��ڵ�ǰ�趨��ʱ����û���κβ���,���ᱻϵͳ�Զ��˳�
        /// </summary>
        public static int OnlineMinute
        {
            get
            {
                try
                {
                    int _onlineminute = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineMinute"]);
                    if (_onlineminute == 0)
                        return 10000;
                    else
                        return _onlineminute;
                }
                catch
                {
                    return 30;
                }
            }
        }
        #endregion

    }
}

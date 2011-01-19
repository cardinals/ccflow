using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Tax666.Common.Utils;

namespace Tax666.Common
{
    /// <summary>
    /// ��վ����������
    /// </summary>
    public class SiteConfigs
    {
        #region ˽���ֶ�

        private static object lockHelper = new object();
        private static System.Timers.Timer siteConfigTimer = new System.Timers.Timer(15000);
        private static SiteConfigInfo m_configinfo;

        #endregion

        /// <summary>
        /// ��̬���캯����ʼ����Ӧʵ���Ͷ�ʱ��
        /// </summary>
        static SiteConfigs() 
        {
            m_configinfo = SiteConfigFileManager.LoadConfig();

            siteConfigTimer.AutoReset = true;
            siteConfigTimer.Enabled = true;
            siteConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            siteConfigTimer.Start();
        }

        public static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) 
        {
            ResetConfig();
        }

        /// <summary>
        /// ����������ʵ��
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = SiteConfigFileManager.LoadConfig();
        }

        public static SiteConfigInfo GetConfig()
        {
            return m_configinfo;
        }

        /// <summary>
        /// �����������Ϣ
        /// </summary>
        /// <returns>������</returns>
        public static bool SetIpDenyAccess(string denyipaccess)
        {
            bool result;

            lock (lockHelper)
            {
                try
                {
                    SiteConfigInfo configInfo = SiteConfigs.GetConfig();
                    configInfo.Ipdenyaccess = configInfo.Ipdenyaccess + "\n" + denyipaccess;
                    SiteConfigs.Serialiaze(configInfo, WebRequests.GetPhysicsPath(ConfigurationManager.AppSettings["CustomConfigFile"]));
                    result = true;
                }
                catch
                {
                    return false;
                }

            }
            return result;
        }

        #region Helper

        /// <summary>
        /// ���л�������ϢΪXML
        /// </summary>
        /// <param name="configinfo">������Ϣ</param>
        /// <param name="configFilePath">�����ļ�����·��</param>
        public static SiteConfigInfo Serialiaze(SiteConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }


        public static SiteConfigInfo Deserialize(string configFilePath)
        {
            return (SiteConfigInfo)SerializationHelper.Load(typeof(SiteConfigInfo), configFilePath);
        }

        #endregion
    }
}

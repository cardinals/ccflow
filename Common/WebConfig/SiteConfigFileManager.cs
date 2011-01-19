using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Tax666.Common
{
    /// <summary>
    /// ��վ�������ù�����
    /// </summary>
    public class SiteConfigFileManager : DefaultConfigFileManager
    {
        private static SiteConfigInfo m_configinfo;

        /// <summary>
        /// �ļ��޸�ʱ��
        /// </summary>
        private static DateTime m_fileoldchange;

        /// <summary>
        /// �����ļ�����·��
        /// </summary>
        public static string filename = null;

        /// <summary>
        /// ���캯��:��ʼ���ļ��޸�ʱ��Ͷ���ʵ��
        /// </summary>
        static SiteConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);

            try
            {
                m_configinfo = (SiteConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(SiteConfigInfo));
            }
            catch
            {
                if (File.Exists(ConfigFilePath))
                {
                    m_configinfo = (SiteConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(SiteConfigInfo));
                }
            }
        }

        #region ����

        /// <summary>
        /// ��ǰ�������ʵ��
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (SiteConfigInfo)value; }
        }

        /// <summary>
        /// ��ȡ�����ļ�����·��
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = WebRequests.GetPhysicsPath(ConfigurationManager.AppSettings["CustomConfigFile"]);
                }
                return filename;
            }
        }

        #endregion

        /// <summary>
        /// ����������ʵ��
        /// </summary>
        /// <returns></returns>
        public static SiteConfigInfo LoadConfig()
        {

            try
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            catch
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            return ConfigInfo as SiteConfigInfo;
        }


        /// <summary>
        /// ����������ʵ��
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}

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
using System.Xml.Serialization;

namespace Tax666.Common
{
    public class DefaultConfigFileManager
    {
        #region ˽�о�̬����

        private static string m_configfilepath;                 // �ļ�����·������
        private static IConfigInfo m_configinfo = null;         // ��ʱ���ö������
        private static object m_lockHelper = new object();      // ������

        #endregion

        #region ����

        /// <summary>
        /// �ļ�����·��
        /// </summary>
        public static string ConfigFilePath
        {
            get { return m_configfilepath; }
            set { m_configfilepath = value; }
        }

        /// <summary>
        /// ��ʱ���ö���
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = value; }
        }

        #endregion

        /// <summary>
        /// ����(�����л�)ָ���������͵����ö���
        /// </summary>
        /// <param name="fileoldchange">�ļ�����ʱ��</param>
        /// <param name="configFilePath">�����ļ�����·��</param>
        /// <param name="configinfo">��Ӧ�ı��� ע:�ò�����Ҫ��������m_configinfo���� �� ��ȡ����.GetType()</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo)
        {
            return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
        }

        /// <summary>
        /// ����(�����л�)ָ���������͵����ö���
        /// </summary>
        /// <param name="fileoldchange">�ļ�����ʱ��</param>
        /// <param name="configFilePath">�����ļ�����·��(�����ļ���)</param>
        /// <param name="configinfo">��Ӧ�ı��� ע:�ò�����Ҫ��������m_configinfo���� �� ��ȡ����.GetType()</param>
        /// <param name="checkTime">�Ƿ��鲢���´��ݽ�����"�ļ�����ʱ��"����</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo, bool checkTime)
        {
            m_configfilepath = configFilePath;
            m_configinfo = configinfo;

            if (checkTime)
            {
                DateTime m_filenewchange = System.IO.File.GetLastWriteTime(configFilePath);

                //������������config�ļ������仯ʱ���config���¸�ֵ
                if (fileoldchange != m_filenewchange)
                {
                    fileoldchange = m_filenewchange;
                    lock (m_lockHelper)
                    {
                        m_configinfo = DeserializeInfo(configFilePath, configinfo.GetType());
                    }
                }
            }
            else
            {
                lock (m_lockHelper)
                {
                    m_configinfo = DeserializeInfo(configFilePath, configinfo.GetType());
                }

            }


            return m_configinfo;
        }

        /// <summary>
        /// �����л�ָ������
        /// </summary>
        /// <param name="configfilepath">config �ļ���·��</param>
        /// <param name="configtype">��Ӧ������</param>
        /// <returns></returns>
        public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
        {

            IConfigInfo iconfiginfo = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(configfilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(configtype);
                iconfiginfo = (IConfigInfo)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                new Terminator().ThrowError("�����ļ������л�ʱ��������" + ex.Message);

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return iconfiginfo;
        }

        /// <summary>
        /// ��������ʵ��(�鷽����̳�)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig()
        {
            return true;
        }

        /// <summary>
        /// ����(���л�)ָ��·���µ������ļ�
        /// </summary>
        /// <param name="configFilePath">ָ���������ļ����ڵ�·��(�����ļ���)</param>
        /// <param name="configinfo">������(���л�)�Ķ���</param>
        /// <returns></returns>
        public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            bool succeed = false;
            FileStream fs = null;
            try
            {
                fs = new FileStream(configFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(configinfo.GetType());
                serializer.Serialize(fs, configinfo);
                //�ɹ��򽫻᷵��true
                succeed = true;
            }
            catch (Exception ex)
            {
                new Terminator().ThrowError("���������ļ�ʱ��������" + ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return succeed;
        }
    }
}

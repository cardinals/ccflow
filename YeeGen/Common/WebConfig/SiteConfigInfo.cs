using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Tax666.Common
{
    /// <summary>
    /// ��վ��������������, ��[Serializable]���Ϊ�����л�IConfigInfo
    /// </summary>
    [Serializable]
    public class SiteConfigInfo : IConfigInfo
    {
        #region ˽���ֶ�
        /// <summary>
        /// ��վ�Ļ�������
        /// </summary>
        private string m_weburl = "http://www.tax666.com/";           //��վurl��ַ
        private string m_webtitle = "�������˰����ѯ�߶˷����Ż�-����߶���ҵ����˰����ѯ����ר��";         //��վ������

        private int m_siteenabled = 0;                                    //1-��վ����Ȩ����ʹ��;0-δ��Ȩ�޷�ʹ�ã�
        private int m_closed = 0;		                            //1-�ر�ά����0-������
        private string m_reason = "��Ǹ!��վ���ڽ���ά��,��ʱ�ر�,���Ժ����.";          //��վ�ر���ʾ��Ϣ
        private string defaultkey = "foxnet990812";                       //DES���ܵ�Ĭ����Կ
        private string m_seotitle = "�������˰����ѯ�߶˷����Ż�-��ҵ������ѯ,˰����ѯ,�߶�רҵ����,������ѵ,˰���Ը���,��ѧ�μ�";         //���⸽����
        private string m_seokeywords = "���,����,˰��,��ѯ,�߶�,�����Ż�,��ҵ����,˰����ѯ,��ѵ,���Ը���,רҵ,tax,˰����Ѷר��,��ѵ��Ƶ,��ѧ�μ�,����,��˰,�ȵ�,��ѯ,Ͷ��,˰�ճﻮ,ר��,����,��˰,����,���,ʵ��,����,���,����";   //Meta Keywords
        private string m_seodescription = "�������˰����ѯ�߶˷����Ż�,����߶���ҵ����˰��ҵ����ѯ�������ר��";      //Meta Description

        private int m_themeid = 1;
        private string m_themedesc = "Ĭ�Ϸ��";
        private string m_themepath = "Default";
        private string authentinfo = "������...";

        /// <summary>
        /// �ͻ�������Ϣ
        /// </summary>
        private string m_serviceqq = "863858965";
        private string m_saleqq = "863858965";
        private string m_telphone = "86-0755-83279705";
        private string m_fax = "86-0755-83279705";
        private string m_email = "qifl23702570@163.com";

        private int m_nocacheheaders = 0;                                    //��ֹ���������

        /// <summary>
        /// �û�ע�����Ȩ������
        /// </summary>
        private int m_regstatus = 1;                                    //�Ƿ��������û�ע��
        private int m_doublee = 0;                                    //����ͬһ Email ע�᲻ͬ�û�
        private int m_regverify = 0;                                    //���û�ע����֤��0���ޣ�1��Email��֤��2���˹���ˣ�
        private string m_censoruser = "admin";                             //�û���Ϣ�����ؼ���

        private int m_regctrl = 0;            //IP ע��������(Сʱ)
        private string m_ipregctrl = "";           //���� IP ע������
        private string m_ipdenyaccess = "";           //IP��ֹ�����б�
        private string m_ipaccess = "";           //IP�����б�
        private string adminipdenyaccess = "";	        //������ֱ����IP�����б�
        private string adminipaccess = "";		    //������ֱ����IP��ֹ�����б�

        private int m_welcomemsg = 1;            //���ͻ�ӭ����Ϣ
        private string m_msg = "";           //��ӭ����Ϣ����
        private int m_sendemil = 0;            //���ͻ�ӭ����Ϣ
        private string m_emailcontent = "";

        private string smtp = "mail.163.com";           //smtp ��ַ
        private int port = 25;           //�˿ں�
        private string sysemail = "qfl@163.net";           //ϵͳ�ʼ���ַ
        private string username = "qfl";           //�ʼ��ʺ�
        private string password = "12345";           //�ʼ�����

        private int m_searchctrl = 0;            //����ʱ������(��)
        private int m_maxspm = 5;            //60 �������������
        private int m_maxonlines = 10000;         //�����������
        private string m_visitbanperiods = "";          //��ֹ����ʱ���
        private string m_searchbanperiods = "";         //��ֹȫ������ʱ���

        #endregion

        #region ����

        /// <summary>
        /// ��վ��������
        /// </summary>
        public string Webtitle
        {
            get { return m_webtitle; }
            set { m_webtitle = value; }
        }

        /// <summary>
        /// ��վurl��ַ
        /// </summary>
        public string Weburl
        {
            get { return m_weburl; }
            set { m_weburl = value; }
        }

        /// <summary>
        /// 0-��վ����;1-�ر�ά����
        /// </summary>
        public int SiteEnabled
        {
            get { return m_siteenabled; }
            set { m_siteenabled = value; }
        }

        public int Closed
        {
            get { return m_closed; }
            set { m_closed = value; }
        }

        /// <summary>
        /// ��վ�ر���ʾ��Ϣ
        /// </summary>
        public string Reason
        {
            get { return m_reason; }
            set { m_reason = value; }
        }

        /// <summary>
        /// DES���ܵ�Ĭ����Կ
        /// </summary>
        public string DefaultKey
        {
            get { return defaultkey; }
            set { defaultkey = value; }
        }

        /// <summary>
        /// ���⸽����
        /// </summary>
        public string Seotitle
        {
            get { return m_seotitle; }
            set { m_seotitle = value; }
        }

        /// <summary>
        /// Meta Keywords
        /// </summary>
        public string Seokeywords
        {
            get { return m_seokeywords; }
            set { m_seokeywords = value; }
        }

        /// <summary>
        /// Meta Description
        /// </summary>
        public string Seodescription
        {
            get { return m_seodescription; }
            set { m_seodescription = value; }
        }

        public int ThemeID
        {
            get { return m_themeid; }
            set { m_themeid = value; }
        }

        public string Themedesc
        {
            get { return m_themedesc; }
            set { m_themedesc = value; }
        }

        public string Themepath
        {
            get { return m_themepath; }
            set { m_themepath = value; }
        }

        /// <summary>
        /// ��ֹ���������
        /// </summary>
        public int Nocacheheaders
        {
            get { return m_nocacheheaders; }
            set { m_nocacheheaders = value; }
        }

        public string AuthentInfo
        {
            get { return authentinfo; }
            set { authentinfo = value; }
        }

        /// <summary>
        /// �ͻ�������Ϣ
        /// </summary>
        public string ServiceQQ
        {
            get { return m_serviceqq; }
            set { m_serviceqq = value; }
        }

        public string SaleQQ
        {
            get { return m_saleqq; }
            set { m_saleqq = value; }
        }

        public string Telphone
        {
            get { return m_telphone; }
            set { m_telphone = value; }
        }

        public string Fax
        {
            get { return m_fax; }
            set { m_fax = value; }
        }

        public string Email
        {
            get { return m_email; }
            set { m_email = value; }
        }

        /// <summary>
        /// �Ƿ��������û�ע��
        /// </summary>
        public int Regstatus
        {
            get { return m_regstatus; }
            set { m_regstatus = value; }
        }

        /// <summary>
        /// ����ͬһ Email ע�᲻ͬ�û�
        /// </summary>
        public int Doublee
        {
            get { return m_doublee; }
            set { m_doublee = value; }
        }

        /// <summary>
        /// ���û�ע����֤
        /// </summary>
        public int Regverify
        {
            get { return m_regverify; }
            set { m_regverify = value; }
        }

        /// <summary>
        /// �û���Ϣ�����ؼ���
        /// </summary>
        public string Censoruser
        {
            get { return m_censoruser; }
            set { m_censoruser = value; }
        }

        /// <summary>
        /// IP ע��������(Сʱ)
        /// </summary>
        public int Regctrl
        {
            get { return m_regctrl; }
            set { m_regctrl = value; }
        }

        /// <summary>
        /// ���� IP ע������
        /// </summary>
        public string Ipregctrl
        {
            get { return m_ipregctrl; }
            set { m_ipregctrl = value; }
        }

        /// <summary>
        /// IP��ֹ�����б�
        /// </summary>
        public string Ipdenyaccess
        {
            get { return m_ipdenyaccess; }
            set { m_ipdenyaccess = value; }
        }

        /// <summary>
        /// IP��������б�
        /// </summary>
        public string Ipaccess
        {
            get { return m_ipaccess; }
            set { m_ipaccess = value; }
        }

        /// <summary>
        /// ������ֱ����IP��ֹ�����б�
        /// </summary>
        public string AdminIpdenyaccess
        {
            get { return adminipdenyaccess; }
            set { adminipdenyaccess = value; }
        }

        /// <summary>
        /// ������ֱ����IP��������б�
        /// </summary>
        public string AdminIpaccess
        {
            get { return adminipaccess; }
            set { adminipaccess = value; }
        }

        /// <summary>
        /// smtp������
        /// </summary>
        public string Smtp
        {
            get { return smtp; }
            set { smtp = value; }
        }

        /// <summary>
        /// �˿ں�
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// ϵͳEmail��ַ
        /// </summary>
        public string Sysemail
        {
            get { return sysemail; }
            set { sysemail = value; }
        }

        /// <summary>
        /// �û���
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// ���ͻ�ӭ����Ϣ
        /// </summary>
        public int Welcomemsg
        {
            get { return m_welcomemsg; }
            set { m_welcomemsg = value; }
        }

        /// <summary>
        /// ��ӭ����Ϣ����
        /// </summary>
        public string Msg
        {
            get { return m_msg; }
            set { m_msg = value; }
        }

        public int SendEmail
        {
            get { return m_sendemil; }
            set { m_sendemil = value; }
        }

        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Emailcontent
        {
            get { return m_emailcontent; }
            set { m_emailcontent = value; }
        }

        /// <summary>
        /// ����ʱ������(��)
        /// </summary>
        public int Searchctrl
        {
            get { return m_searchctrl; }
            set { m_searchctrl = value; }
        }

        /// <summary>
        /// 60 �������������
        /// </summary>
        public int Maxspm
        {
            get { return m_maxspm; }
            set { m_maxspm = value; }
        }

        /// <summary>
        /// �����������
        /// </summary>
        public int Maxonlines
        {
            get { return m_maxonlines; }
            set { m_maxonlines = value; }
        }

        /// <summary>
        /// ��ֹ����ʱ���
        /// </summary>
        public string Visitbanperiods
        {
            get { return m_visitbanperiods; }
            set { m_visitbanperiods = value; }
        }

        /// <summary>
        /// ��ֹȫ������ʱ���
        /// </summary>
        public string Searchbanperiods
        {
            get { return m_searchbanperiods; }
            set { m_searchbanperiods = value; }
        }

        #endregion
    }
}

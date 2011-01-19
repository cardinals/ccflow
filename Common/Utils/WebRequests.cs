using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace Tax666.Common
{
    public class WebRequests
    {
        #region ҳ������POST��GET���ͻ�ȡ����ֵ
        /// <summary>
        /// �жϵ�ǰҳ���Ƿ���յ���Post����
        /// </summary>
        /// <returns>�Ƿ���յ���Post����</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// ��ȡҳ���ύ�Ĳ���ֵ���൱�� Request.Form
        /// </summary>
        public static string Post(string name)
        {
            string value = HttpContext.Current.Request.Form[name];
            return value == null ? string.Empty : value.Trim();
        }

        /// <summary>
        /// �жϵ�ǰҳ���Ƿ���յ���Get����
        /// </summary>
        /// <returns>�Ƿ���յ���Get����</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// ��ȡҳ���ַ�Ĳ���ֵ���൱�� Request.QueryString
        /// </summary>
        public static string Get(string name)
        {
            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
        }

        /// <summary>
        /// ��ȡҳ���ַ�Ĳ���ֵ����鰲ȫ�ԣ��൱�� Request.QueryString
        /// chkType �� CheckGetEnum.Int�� CheckGetEnum.Safety�������ͣ�
        /// CheckGetEnum.Int ��֤������������
        /// CheckGetEnum.Safety ��֤�ύ�Ĳ���ֵû�в������ݿ�����
        /// </summary>
        public static string Get(string name, CheckGetEnum chkType)
        {
            string value = Get(name);
            bool isPass = false;
            switch (chkType)
            {
                default:
                    isPass = true;
                    break;
                case CheckGetEnum.Int:
                    {
                        try
                        {
                            int.Parse(value);
                            isPass = RegExp.IsNumeric(value);
                        }
                        catch
                        {
                            isPass = false;
                        }
                        break;
                    }
                case CheckGetEnum.Safety:
                    isPass = RegExp.IsSafety(value);
                    break;
            }
            if (!isPass)
            {
                new Terminator().Throw("��ַ���в�����" + name + "����ֵ������Ҫ������Ǳ����в���벻Ҫ�ֶ��޸�URL��");
                return string.Empty;
            }
            return value;
        }

        #endregion

        #region ��ȡ����URL��ֵ
        /// <summary>
        /// ���ָ����������ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>��������ֵ</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }

        /// <summary>
        /// ���ָ��Url������ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������ֵ</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }

        /// <summary>
        /// ���ָ��Url������int����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url������int����ֵ</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return WebUtility.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        /// <summary>
        /// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <returns>Url���������ֵ</returns>
        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);
            }
            else
            {
                return GetQueryString(strName);
            }
        }
        #endregion

        #region ��õ�ǰӦ�ó����·��
        /// <summary>
        /// ��õ�ǰ����·��
        /// �磺��Ŀ¼��~/���ķ���ֵΪ��G:\Tax666\AppWeb\����
        /// WebRequests.GetMapPath("~/TestDel.aspx")����ֵ��G:\Tax666\AppWeb\TestDel.aspx 
        /// </summary>
        /// <param name="strPath">ָ����·��</param>
        /// <returns>����·��</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //��web��������
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// �õ���ǰURL�����ĵ�ַ;
        /// ��ʽ��http://Tax666:8080/��
        /// </summary>
        /// <returns></returns>
        public static string GetWebUrl()
        {
            string webUrl = string.Empty;

            if (!HttpContext.Current.Request.Url.IsDefaultPort)
            {
                webUrl = @"http://" + string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString()) + "/";
            }
            else
            {
                webUrl = @"http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/";
            }

            return webUrl;
        }

        /// <summary>
        /// ����ϵͳ��װ���Web��Ŀ¼�����·��(/zmsoft )��
        /// </summary>
        /// <remarks>
        public static string WebPath
        {
            get
            {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                else
                {
                    return applicationPath;
                }
            }
        }

        /// <summary>
        /// ��õ�ǰ����Url��ַ
        /// ��ʽ��http://192.168.1.119:8080/TestDel.aspx
        /// </summary>
        /// <returns>��ǰ����Url��ַ</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// �������·��
        /// </summary>
        /// <param name="path">·�� ��ʽ��Configfile\WebSite.config����Web.config��appSettings���ý��л�ȡ��</param>
        /// <returns>����·��</returns>
        public static string GetPhysicsPath(string path)
        {
            string AppDir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (path.IndexOf(":") < 0)
            {
                string str = path.Replace("..\\", "");
                if (str != path)
                {
                    int Num = (path.Length - str.Length) / ("..\\").Length + 1;
                    for (int i = 0; i < Num; i++)
                    {
                        AppDir = AppDir.Substring(0, AppDir.LastIndexOf("\\"));
                    }
                    str = "\\" + str;

                }
                path = AppDir + str;
            }
            return path;
        }
        #endregion

        #region ��ȡҳ��url
        /// <summary>
        /// ��ȡ��ǰ����ҳ���ַ
        /// </summary>
        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }

        /// <summary>
        /// ��⵱ǰurl�Ƿ����ָ�����ַ�
        /// </summary>
        /// <param name="sChar">Ҫ�����ַ�</param>
        /// <returns></returns>
        public static bool CheckScriptNameChar(string sChar)
        {
            bool rBool = false;
            if (GetScriptName.ToLower().LastIndexOf(sChar) >= 0)
                rBool = true;
            return rBool;
        }

        /// <summary>
        /// ��ȡ��ǰҳ�����չ��
        /// </summary>
        public static string GetScriptNameExt
        {
            get
            {
                return GetScriptName.Substring(GetScriptName.LastIndexOf(".") + 1);
            }
        }

        /// <summary>
        /// ��ȡ��ǰ����ҳ���ַ����
        /// </summary>
        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }

        /// <summary>
        /// ���ҳ���ļ����Ͳ�����
        /// </summary>
        public static string GetScriptNameUrl
        {
            get
            {
                string Script_Name = GetScriptName;
                Script_Name = Script_Name.Substring(Script_Name.LastIndexOf("/") + 1);
                Script_Name += "?" + GetScriptNameQueryString;
                return Script_Name;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ����ҳ��Url
        /// </summary>
        public static string GetScriptUrl
        {
            get
            {
                return GetScriptNameQueryString == "" ? GetScriptName : string.Format("{0}?{1}", GetScriptName, GetScriptNameQueryString);
            }
        }

        /// <summary>
        /// ���ص�ǰҳ��Ŀ¼��url
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <returns></returns>
        public static string GetHomeBaseUrl(string FileName)
        {
            string Script_Name = GetScriptName;
            return string.Format("{0}/{1}", Script_Name.Remove(Script_Name.LastIndexOf("/")), FileName);
        }

        /// <summary>
        /// ���ص�ǰ��վ��ַ
        /// </summary>
        /// <returns></returns>
        public static string GetHomeUrl()
        {
            return HttpContext.Current.Request.Url.Authority;
        }

        /// <summary>
        /// ��ȡ��ǰ�����ļ�����Ŀ¼
        /// </summary>
        /// <returns>·��</returns>
        public static string GetScriptPath
        {
            get
            {
                string Paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"].ToString();
                return Paths.Remove(Paths.LastIndexOf("\\"));
            }
        }
        #endregion

        #region ��õ�ǰҳ��ͻ��˵�IP��IPת������
        /// <summary>
        /// ��õ�ǰҳ��ͻ��˵�IP
        /// </summary>
        /// <returns>��ǰҳ��ͻ��˵�IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;

            if (null == result || result == String.Empty || !WebUtility.IsIP(result))
                return "0.0.0.0";

            return result;
        }

        /// <summary>
        /// IP ��ַ�ַ�����ʽת���ɳ�����
        /// </summary>
        public static long Ip2Int(string ip)
        {
            if (!RegExp.IsIp(ip))
            {
                return -1;
            }
            string[] arr = ip.Split('.');
            long lng = long.Parse(arr[0]) * 16777216;
            lng += int.Parse(arr[1]) * 65536;
            lng += int.Parse(arr[2]) * 256;
            lng += int.Parse(arr[3]);
            return lng;
        }
        #endregion

        #region ��ȡ����ĵ�ַ��Ϣ
        /// <summary>
        /// �õ�����ͷ
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// ��ȡ��ǰ�����ԭʼ URL(URL ������Ϣ֮��Ĳ���,������ѯ�ַ���(�������))
        /// </summary>
        /// <returns>ԭʼ URL(����������ͷ)</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        #endregion

        #region �ж�url·�� �Ƿ�Ϊ��Ŀ¼
        /// <summary>
        /// �ж�url·�� �Ƿ�Ϊ��Ŀ¼
        /// Req.Url.GetLeftPart(UriPartial.Authority); �ڸ�Ŀ¼����£�����http://localhost;
        /// �����������Ŀ¼�£�����Ŀ¼����Ϊweb���򷵻ص���http://localhost/web
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;

                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);

                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //ֱ�Ӱ�װ�� Web վ��   
                    AppPath = UrlAuthority;
                else
                    //��װ��������Ŀ¼��   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }
        #endregion

        #region ��ȡ��ǰCookies���Ƶ�ǰ׺
        /// <summary>
        /// ��ȡ��ǰCookies����
        /// </summary>
        public static string Get_CookiesPrefix
        {
            get
            {
                return ConfigurationManager.AppSettings["AppPrefix"];
            }
        }
        #endregion

        #region ����GUID
        /// <summary>
        /// ��ȡһ��GUID�ַ���
        /// </summary>
        public static string GetGUID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        #endregion        

    }
}

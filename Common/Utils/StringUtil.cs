using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Security.Cryptography;
using System.Configuration;

namespace Tax666.Common
{
    /// <summary>
    /// �ַ���������̬��
    /// </summary>
    public class StringUtil
    {
        #region �滻JS�������ַ�
        /// <summary>
        /// ��JS�е������ַ��滻
        /// </summary>
        /// <param name="str">Ҫ�滻�ַ�</param>
        /// <returns></returns>
        public static string ReplaceJs(string str)
        {

            if (str != null)
            {
                str = str.Replace("\"", "&quot;");
                str = str.Replace("(", "&#40;");
                str = str.Replace(")", "&#41;");
                str = str.Replace("%", "&#37;");
            }

            return str;

        }
        #endregion

        #region �Ƴ�Html���
        /// <summary>
        /// �Ƴ�Html���
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion

        #region ����HTML�еĲ���ȫ��ǩ
        /// <summary>
        /// ����HTML�еĲ���ȫ��ǩ
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }
        #endregion

        #region ����Ƿ���SqlΣ���ַ�
        /// <summary>
        /// ����Ƿ���SqlΣ���ַ�
        /// </summary>
        /// <param name="str">Ҫ�ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        #endregion

        #region ����Ƿ���Σ�յĿ����������ӵ��ַ���
        /// <summary>
        /// ����Ƿ���Σ�յĿ����������ӵ��ַ���
        /// </summary>
        /// <param name="str">Ҫ�ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|�ο�|^Guest");
        }
        #endregion

        #region �Ա� �����ݽ���ת��HTML����
        /// <summary>
        /// ����:�Ա� �����ݽ���ת��HTML����,
        /// </summary>
        /// <param name="s">html�ַ���</param>
        /// <returns></returns>
        public static string HtmlCode(string s)
        {
            string str = "";
            str = s.Replace(">", "&gt;");
            str = s.Replace("<", "&lt;");
            str = s.Replace(" ", "&nbsp;");
            str = s.Replace("\n", "<br />");
            str = s.Replace("\r", "<br />");
            str = s.Replace("\r\n", "<br />");

            return str;
        }

        /// <summary>
        /// ����:�Ա� �����ݽ���ת��HTML����,
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CodeHtml(string s)
        {
            string str = "";
            str = s.Replace("&gt;", ">");
            str = s.Replace("&lt;", "<");
            str = s.Replace("&nbsp;", " ");
            str = s.Replace("<br />", "\n");
            str = s.Replace("<br />", "\r");
            str = s.Replace("<br />", "\r\n");

            return str;
        }
        #endregion

        #region ����xss�����ű�
        /// <summary>     
        /// ����xss�����ű�     
        /// </summary>     
        /// <param name="input">�����ַ���</param>     
        /// <returns>���˺���ַ���</returns>     
        public static string FilterXSS(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;

            // CR(0a) ��LF(0b) ��TAB(9) ���⣬���˵����еĲ���ӡ�����ַ�.     
            // Ŀ�ķ�ֹ������ʽ������ ��java\0script��     
            // ע�⣺\n, \r,  \t ������Ҫ����������Ϊ���ܻ�Ҫ�õ�     
            string ret = System.Text.RegularExpressions.Regex.Replace(
                html, "([\x00-\x08][\x0b-\x0c][\x0e-\x20])", string.Empty);

            ret = ret.Replace("\t", "");  //(���䣬����TAB�ո���Ҳ��Σ�յ�XSS�ַ�)

            //�滻���п��ܵ�16��10���ƹ����Ķ������     
            //<IMG SRC=&#X40&#X61&#X76&#X61&#X73&#X63&#X72&#X69&#X70&#X74&#X3A&#X61&_#X6C&#X65&#X72&#X74&#X28&#X27&#X58&#X53&#X53&#X27&#X29>            
            ret = System.Text.RegularExpressions.Regex.Replace(ret, @"(&#[x|X]?\d+);?", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //����Javascript�¼������Ķ������   
            string[] keywords = {
                                    "javascript", "vbscript", "expression", "applet", "meta", 
                                    "xml", "blink", "script", "embed", "object", 
                                    "iframe", "frame", "frameset", "ilayer", "layer", "bgsound", "title", "base",   
                                    "onabort", "onactivate", "onafterprint", "onafterupdate", "onbeforeactivate", 
                                    "onbeforecopy", "onbeforecut", "onbeforedeactivate", "onbeforeeditfocus", "onbeforepaste", 
                                    "onbeforeprint", "onbeforeunload", "onbeforeupdate", "onblur", "onbounce", "oncellchange", 
                                    "onchange", "onclick", "oncontextmenu", "oncontrolselect", "oncopy", "oncut", "ondataavailable", 
                                    "ondatasetchanged", "ondatasetcomplete", "ondblclick", "ondeactivate", "ondrag", "ondragend",
                                    "ondragenter", "ondragleave", "ondragover", "ondragstart", "ondrop", "onerror", "onerrorupdate", 
                                    "onfilterchange", "onfinish", "onfocus", "onfocusin", "onfocusout", "onhelp", "onkeydown", 
                                    "onkeypress", "onkeyup", "onlayoutcomplete", "onload", "onlosecapture", "onmousedown", 
                                    "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover", "onmouseup", 
                                    "onmousewheel", "onmove", "onmoveend", "onmovestart", "onpaste", "onpropertychange", 
                                    "onreadystatechange", "onreset", "onresize", "onresizeend", "onresizestart", 
                                    "onrowenter", "onrowexit", "onrowsdelete", "onrowsinserted", "onscroll", "onselect", 
                                    "onselectionchange", "onselectstart", "onstart", "onstop", "onsubmit", "onunload"};

            bool found = true;
            while (found)
            {
                string retBefore = ret;
                for (int i = 0; i < keywords.Length; i++)
                {
                    //string pattern = "/"; (����, ����ǰ̨��/���˲���)
                    string pattern = "";
                    for (int j = 0; j < keywords[i].Length; j++)
                    {
                        if (j > 0)
                            pattern = string.Concat(pattern, '(', "(&#[x|X]0{0,8}([9][a][b]);?)?", "|(&#0{0,8}([9][10][13]);?)?",
                                ")?");
                        pattern = string.Concat(pattern, keywords[i][j]);
                    }
                    string replacement = string.Concat(keywords[i].Substring(0, 2), "��x��", keywords[i].Substring(2));
                    ret = System.Text.RegularExpressions.Regex.Replace(ret, pattern, replacement, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    if (ret == retBefore)
                        found = false;
                }

            }

            return ret;
        }
        #endregion

        #region ��������ʽ���ַ���������֤
        /// <summary>
        /// ��������ʽ���ַ���������֤
        /// </summary>
        /// <param name="str">����֤���ַ���</param>
        /// <param name="i">���:1,��������0;2,�Ǹ���;3,0��99֮�������;4,�����ַ�;5,��������6���ַ�����ʹ����ĸ�����ּ��»��ߣ�</param>
        /// <returns>���س������,��Ϊ""������ͨ��</returns>
        public static string InputValidate(string str, int i)
        {
            if (i == 0)
            {
                return "";
            }
            string exp = "", tip = "";
            switch (i)
            {
                case 1:
                    exp = @"^\d+$";  //��������0
                    tip = "ֻ��������������0";
                    break;
                case 2:
                    exp = @"^\d+(\.\d+)?$"; //�Ǹ���
                    tip = "ֻ������Ǹ���";
                    break;
                case 3:
                    exp = @"^(\d|[1-9]\d)$";  //0��99֮�������
                    tip = "ֻ������0��99֮�������";
                    break;
                case 4:
                    exp = @"[\u4e00-\u9fa5]"; //�����ַ�
                    tip = "ֻ�����������ַ�";
                    break;
                case 5:
                    exp = @"^\w{6,}$";
                    tip = "��������6���ַ�����ʹ����ĸ�����ּ��»��ߣ�";
                    break;
                default:
                    exp = "";
                    tip = "";
                    break;


            }

            if (Regex.IsMatch(str, exp))
            {
                return "";
            }
            else
            {
                return tip;
            }
        }
        #endregion

        #region �ж��Ƿ�������
        /// <summary>
        /// �ж��Ƿ�������
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>ȫ���ַ���True,����ĳ��ֵ��Ϊ���ַ���False</returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region �ַ���ֱ��md5����
        /// <summary>
        /// �ַ���ֱ��md5����
        /// </summary>
        /// <param name="str">����ܵ��ַ���</param>
        /// <returns>md5���ܺ������</returns>
        public static string StringTomd5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str,"md5").ToLower();

        }
        #endregion

        #region �ַ���ת��Сд��md5����
        /// <summary>
        /// �ַ���ת��Сд��md5����
        /// </summary>
        /// <param name="psw">��������</param>
        /// <returns>md5���ܺ������</returns>
        public static string stringTomd5(string str)
        {
            str = str.ToLower();
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str,"md5").ToLower();

        }
        #endregion

        #region �ַ���ֱ��SHA1����
        /// <summary>
        /// �ַ���ֱ��SHA1����
        /// </summary>
        /// <param name="str">����ܵ��ַ���</param>
        /// <returns>SHA1���ܺ������</returns>
        public static string StringToSHA1(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1").ToLower();

        }
        #endregion

        #region �ַ���ת��Сд��SHA1����
        /// <summary>
        /// �ַ���ת��Сд��SHA1����
        /// </summary>
        /// <param name="psw">��������</param>
        /// <returns>SHA1���ܺ������</returns>
        public static string stringToSHA1(string str)
        {
            str = str.ToLower();
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");

        }
        #endregion

        #region Ĭ����Կ���� DES���ܽ�����
        /// <summary>
        /// Ĭ����Կ����
        /// </summary>
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        #endregion

        #region DES�����ַ����������ü�����Կ,Ҫ��Ϊ8λ
        /// <summary>
        /// DES�����ַ����������ü�����Կ,Ҫ��Ϊ8λ
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        #endregion

        #region DES�����ַ���,�����ʱ����Կ,Ҫ��Ϊ8λ
        /// <summary>
        /// DES�����ַ���,�����ʱ����Կ,Ҫ��Ϊ8λ
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion

        #region "����ǰ���ں�ʱ�����������"
        /// <summary>
        /// ����ǰ���ں�ʱ�����������
        /// </summary>
        /// <param name="Num">�������������</param>
        /// <returns></returns>
        public static string sRndNum(int Num)
        {
            string sTmp_Str = System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString("00") + System.DateTime.Today.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00");
            return sTmp_Str + RndNum(Num);
        }
        #endregion

        #region ����0-9�����
        /// <summary>
        /// ����0-9�����
        /// </summary>
        /// <param name="VcodeNum">���ɳ���</param>
        /// <returns></returns>
        public static string RndNum(int VcodeNum)
        {
            StringBuilder sb = new StringBuilder(VcodeNum);
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }
        #endregion

        #region "ͨ��RNGCryptoServiceProvider ��������� 0-9"
        /// <summary>
        /// ͨ��RNGCryptoServiceProvider ��������� 0-9 
        /// </summary>
        /// <param name="length">���������</param>
        /// <returns></returns>
        public static string RndNumRNG(int length)
        {
            byte[] bytes = new byte[16];
            RNGCryptoServiceProvider r = new RNGCryptoServiceProvider();
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                r.GetBytes(bytes);
                sb.AppendFormat("{0}", (int)((decimal)bytes[0] / 256 * 10));
            }
            return sb.ToString();

        }
        #endregion

        #region "ת������"
        /// <summary>
        /// ת������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {

                return System.Web.HttpUtility.UrlEncode(Encoding.GetEncoding(54936).GetBytes(str));
            }
        }
        #endregion

        #region "���ַ���λ����0"
        /// <summary>
        /// ���ַ���λ����0
        /// </summary>
        /// <param name="CharTxt">�ַ���</param>
        /// <param name="CharLen">�ַ�����</param>
        /// <returns></returns>
        public static string FillZero(string CharTxt, int CharLen)
        {
            if (CharTxt.Length < CharLen)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < CharLen - CharTxt.Length; i++)
                {
                    sb.Append("0");
                }
                sb.Append(CharTxt);
                return sb.ToString();
            }
            else
            {
                return CharTxt;
            }
        }

        #endregion

        #region "��ʽ���ʽ��֤"
        /// <summary>
        /// ��ʽ���ʽ��֤
        /// </summary>
        /// <param name="C_Value">��֤�ַ�</param>
        /// <param name="C_Str">��ʽ���ʽ</param>
        /// <returns>����true������false</returns>
        public static bool CheckRegEx(string C_Value, string C_Str)
        {
            Regex objAlphaPatt;
            objAlphaPatt = new Regex(C_Str, RegexOptions.Compiled);


            return objAlphaPatt.Match(C_Value).Success;
        }
        #endregion

        #region "����Ƿ�Ϊ��Ч�ʼ���ַ��ʽ"
        /// <summary>
        /// ����Ƿ�Ϊ��Ч�ʼ���ַ��ʽ
        /// </summary>
        /// <param name="strIn">�����ʼ���ַ</param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region �����ַ������ַ����г��ֵĴ���
        /// <summary>
        /// �����ַ������ַ����г��ֵĴ���
        /// </summary>
        /// <param name="Char">Ҫ�����ֵ��ַ�</param>
        /// <param name="String">Ҫ�����ַ���</param>
        /// <returns>���ִ���</returns>
        public static int GetCharInStringCount(string Char, string String)
        {
            string str = String.Replace(Char, "");
            return (String.Length - str.Length) / Char.Length;

        }
        #endregion

        #region ȫ�ǰ��ת��
        /// <summary>
        /// תȫ�ǵĺ���(SBC case)
        /// javascript: onblur="javascript:this.value=this.value.replace(/��/ig,',');"
        /// </summary>
        /// <param name="input">�����ַ���</param>
        /// <returns>ȫ���ַ���</returns>
        ///<remarks>
        ///ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
        ///�����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
        ///</remarks>
        public static string ToSBC(string input)
        {
            //���תȫ�ǣ�
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary> ת��ǵĺ���(DBC case) </summary>
        /// <param name="input">�����ַ���</param>
        /// <returns>����ַ���</returns>
        ///<remarks>
        ///ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
        ///�����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        
        /// <summary>
        /// ��ȫ������ת��Ϊ����
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }
        #endregion

        #region ��ȡWEBCache����ǰ�
        /// <summary>
        /// ��ȡWEBCache����ǰ�
        /// </summary>
        public static string Get_WebCacheName
        {
            get
            {
                return "Tax666_manager";
            }
        }
        #endregion

        #region "��ȡ��½�û�UserID"
        /// <summary>
        /// ��ȡ��½�û�UserID,���δ��½Ϊ0
        /// </summary>
        public static int Get_UserID
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated ? Convert.ToInt32(HttpContext.Current.User.Identity.Name) : 0;
            }

        }
        #endregion

        #region "��ȡ��ǰCookies����"
        /// <summary>
        /// "��ȡ��ǰCookies����
        /// </summary>
        public static string Get_CookiesName
        {
            get
            {
                return "Tax666_manager";
            }
        }
        #endregion

        #region "��ȡ�û�IP��ַ"
        /// <summary>
        /// ��ȡ�û�IP��ַ
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress() 
        {
            string user_IP = string.Empty;
            user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
            return user_IP;
        }
        #endregion

        #region "�û�����ʱ��"
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

        #region "�������ͳ�����ݱ��滷��"
        /// <summary>
        /// �������ͳ�����ݱ��滷��
        /// </summary>
        public static OnlineCountType GetOnlineCountType
        {
            get
            {
                if (GetConfigOnlineCountType == 1)
                    return OnlineCountType.DataBase;
                else
                    return OnlineCountType.Cache;
            }
        }

        /// <summary>
        /// �����������ͳ������
        /// </summary>
        private static int GetConfigOnlineCountType
        {
            get
            {
                int rInt = 0;
                try
                {
                    rInt = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineCountType"]);
                }
                catch (Exception)
                {
                   // FileTxtLogs.WriteLog(ex.ToString());
                }
                return rInt;
            }
        }
        #endregion

        #region "��ȡҳ��url"
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
                string Script_Name = StringUtil.GetScriptName;
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
                return StringUtil.GetScriptNameQueryString == "" ? StringUtil.GetScriptName : string.Format("{0}?{1}", StringUtil.GetScriptName, StringUtil.GetScriptNameQueryString);
            }
        }

        /// <summary>
        /// ���ص�ǰҳ��Ŀ¼��url
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <returns></returns>
        public static string GetHomeBaseUrl(string FileName)
        {
            string Script_Name = StringUtil.GetScriptName;
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

        #region "���ݿ�����"

        /// <summary>
        /// ��ȡ���ݿ�����
        /// </summary>
        public static string GetDBType
        {
            get
            {
                return ConfigurationManager.AppSettings["DBType"];
            }
        }

        /// <summary>
        /// ��ȡ���ݿ������ַ���
        /// </summary>
        public static string GetConnString
        {
            get
            {
                return ConfigurationManager.AppSettings[GetDBType];
            }
        }
        #endregion

        #region "���sessionid"
        /// <summary>
        /// ���sessionid
        /// </summary>
        public static string GetSessionID
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }
        #endregion

        #region "��ʽ������24Сʱ��Ϊ�ַ�����:2008/12/12 21:22:33"
        /// <summary>
        /// ��ʽ������24Сʱ��Ϊ�ַ�����:2008/12/12 21:22:33
        /// </summary>
        /// <param name="d">����</param>
        /// <returns>�ַ�</returns>
        public static string FormatDateToString(DateTime d)
        {
            return d.ToString("yyyy/MM/dd HH:mm:ss");
        }
        #endregion

        #region ��ȡָ�����ȵ��ַ���
        /// <summary>
        /// ��ȡָ�����ȵ��ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetStrLength(string str, int length)
        {
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    str = str.Substring(0, j);
                    break;
                }
                j++;
            }
            return str;
        }
        #endregion

        /// <summary>
        /// ����DataGrid,DataList�����ַ����ĳ���;
        /// ��aspxҳ����õķ�����
        /// SmartVOD.Components.StringUtil.ControlStrLength("����Ƶ��ַ���",���Ƴ���);
        /// һ������Ϊ�����ַ���
        /// </summary>
        /// <param name="str">��Ҫ���Ƶ��ַ���</param>
        /// <param name="length">��󳤶�ֵ</param>
        /// <returns></returns>
        public static string ControlStrLength(string str, int length)
        {
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    str = str.Substring(0, j) + "��";
                    break;
                }
                j++;
            }
            return str;
        }
    }
}

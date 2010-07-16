using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using BP.En;
using BP.DA;
using System.Web;
using System.Text.RegularExpressions;

namespace BP.RB
{
    public class PubClass
    {
        #region sds
        public static string StringCutHtml(string docs)
        {
            docs = docs.Replace("&nbsp;", " ");
            docs = docs.Replace("  ", " ");

            // �滻һЩ��ǡ�
            docs = docs.Replace("javascipt", "");
            docs = docs.Replace("function", "");

            // ȥ�� html ��ǡ�
            char[] chars = docs.ToCharArray();
            bool begainRec = true;
            string str = "";
            foreach (char c in chars)
            {
                if (c == '<')
                {
                    begainRec = false;
                    continue;
                }

                if (c == '>')
                {
                    begainRec = true;
                    continue;
                }

                if (c == '\t')
                    continue;

                if (c == '\n')
                    continue;

                if (c == '\r')
                    continue;

                if (begainRec)
                    str += c.ToString();
            }


            // ȥ��javascript ���֡�
            chars = str.ToCharArray();
            str = "";
            begainRec = true;

            foreach (char c in chars)
            {
                if (c == '{')
                {
                    begainRec = false;
                    continue;
                }

                if (c == '}')
                {
                    begainRec = true;
                    continue;
                }

                if (begainRec)
                    str += c.ToString();
            }
            return str.Trim();
        }
        #endregion

        #region sss

        public static int NumOfAll=0;
        public static int NumOfThisSite=0;
        public static void DoPage(Href hf)
        {
            if (hf.IsLocalHost == false)
                return;

            if (hf.IsFile)
                return; /* ������ļ��Ͳ������ˡ�*/

            Page pg = new Page();
            pg.No = hf.Url;

            if (pg.IsExits)
                return;

            pg.FK_WebSite = hf.HostName;
            if (hf.IsLocalHost == false)
                return;

            string docs = PubClass.ReadContext(hf.Url, hf.HisEncoder);
            if (docs == null)
                return;


            Hrefs hfs = PubClass.GetHrefs(hf.Url, hf.HisEncoder);
            pg.No = hf.Url;
            pg.Name = hf.Lab;
            pg.Url = hf.Url;
            pg.HostName = hf.HostName;
            pg.DocHtml = docs;
            pg.DocText = PubClass.StringCutHtml(docs);
            pg.NumOfHrefs = hfs.Count;
            pg.Insert();

            pg.GenerEmails(); // ����email.
            pg.GenerFiles(hfs, pg.Url ); //�����ļ���

            foreach (Href myhf in hfs)
            {
                PubClass.DoPage(myhf);
            }
        }
        #endregion

        #region ���÷���

        public static int PageTimeout
        {
            get
            {
                return int.Parse(SystemConfig.AppSettings["PageTimeout"]);
            }
        }
        /// <summary>
        /// ������ʱ�ļ�
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerTempFileByUrl(string url, string hostName)
        {

            string dirName = "C:\\StealTempFile\\" + hostName + "\\";

            if (Directory.Exists(dirName) == false)
                Directory.CreateDirectory(dirName);

            string tempfile = url;
            tempfile = tempfile.Replace("http://", "");
            tempfile = tempfile.Replace("HTTP://", "");
            tempfile = tempfile.Replace("/", "@");
            tempfile = tempfile.Replace("?", "_");
            tempfile = tempfile.Replace("&", "_");
            tempfile = tempfile.Replace("=", "_");
            tempfile = tempfile.Replace(":", "_");
            tempfile = dirName + "\\" + tempfile + ".htm";
            return tempfile;
        }
        public static string ReadContextMsg = null;
        public static Encoding ParseFormat(string url)
        {
            return Encoding.Default;
            try
            {

                string html = getHTML_del(url, Encoding.ASCII.EncodingName, PubClass.PageTimeout);
                if (html == null)
                    return Encoding.Default;


                Regex reg_charset = new Regex(@"charset\b\s*=\s*(?<charset>[^""]*)");

                string enconding = null;
                if (reg_charset.IsMatch(html))
                {
                    enconding = reg_charset.Match(html).Groups["charset"].Value;
                }
                else
                {
                    enconding = Encoding.Default.EncodingName;
                }

                return Encoding.GetEncoding(enconding);

            }
            catch (UriFormatException ex)
            {
                return Encoding.Default;
            }
        }
        public static Int64 GetFileSize(string url, int timeOut)
        {
            return 100;

            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = timeOut;

                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                long size = stream.Length;

                Int64 s = Int64.Parse(size.ToString()) / 100000;
                return s;
            }
            catch (UriFormatException ex)
            {
                return 0;
            }
            catch (WebException ex)
            {
                return 0;
            }
        }

        static string getHTML_del(string url, string encodingName, int timeOut)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = timeOut;

                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(encodingName));
                string html = sr.ReadToEnd();
                return html;
            }
            catch (UriFormatException ex)
            {
                return null;
            }
            catch (WebException ex)
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡһ��url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ReadContext(string url, Encoding  encodeing )
        {
            try
            {

                HttpWebRequest webRequest = null;
                HttpWebResponse webResponse;
                Stream stream;
                string hostName = "";
                try
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(url);
                    webRequest.Timeout = PubClass.PageTimeout;
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    stream = webResponse.GetResponseStream();
                    webRequest.AllowAutoRedirect = true;

                    string str = webRequest.Address.AbsoluteUri;
                    hostName = webRequest.Address.Host;
                }
                catch (Exception ex)
                {
                    PubClass.ReadContextMsg = "@��ȡURL���ִ���:URL=" + url + "@������Ϣ��" + ex.Message;
                    Log.DefaultLogWriteLineInfo("��ȡURL" + url + "����:" + ex.Message);
                    return null;
                }

                string tempfile = GenerTempFileByUrl(url, hostName);

                //Encoding  encoding = PubClass.ParseFormat(url);
                //if (encoding == null)
                //    return null;

                System.IO.StreamReader streamReader = new StreamReader(stream, encodeing);

                // ��ȡ���ַ�������.
                string content = streamReader.ReadToEnd();

                //StreamWriter sw = new StreamWriter(tempfile);
                //sw.Write(content);
                //sw.Close();

                // �ر���ض���
                streamReader.Close();
                webResponse.Close();
                PubClass.ReadContextMsg = "@��ȡ����[" + url + "]�ɹ���";
                return content;
            }
            catch (Exception ex)
            {

                string msg = "��ȡ[" + url + "]�ڼ��������Ĵ���" + ex.Message;
                Log.DefaultLogWriteLineError(msg);
                return null;
            }

        }
        public static bool CheckUrl(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Timeout = PubClass.PageTimeout;
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            webRequest.AllowAutoRedirect = true;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="docs"></param>
        /// <param name="hrefFlag"></param>
        /// <returns></returns>
        public static Hrefs GetHrefs(string url, System.Text.Encoding encoder)
        {
            string docs = PubClass.ReadContext(url, encoder);
            Uri uri = new Uri(url);
            string path = uri.AbsolutePath;
            path = path.Substring(0, path.LastIndexOf('/') + 1);
            path = path.ToLower();

            string hostName = uri.Authority.ToLower();

            Hrefs hfs = new Hrefs();
            if (docs == null)
                return hfs;


            //docs = "�й���Ա <a href='as.aspx' > ���ӱ��� </a>";

            char[] chars = docs.ToCharArray();

            bool isStartRecordMarket = false; // ��ʼ��¼ < 
            bool isStartRecordLab = false; // ��ʼ��¼ lab 

            string str1 = "";
            string str2 = "";
            string str3 = "";
            string recordering = "";
            string doc = "";

            bool isChecUrl = false;
            foreach (char c in chars)
            {
                #region ��ʼ����str1
                if (c == '<')
                {
                    /*��ʼ��¼���*/
                    isStartRecordMarket = true;
                    recordering = "";
                    isStartRecordLab = false; // ֹͣ��¼lab.
                }


                if (c == '>')
                {
                    isStartRecordMarket = false; // ֹͣ��¼���
                    //�жϼ�¼���������ݡ�
                    doc = recordering.ToLower();
                    if (doc.IndexOf("href") == -1 || doc.Contains("window"))
                    {
                        /* ˵������ href ���� */
                        if (doc.IndexOf("/a") == -1)
                        {
                            /* ˵��Ҳ���� str3 ���� */
                            recordering = "";
                            continue; //��¼�ǵ���Ч�ı�ǡ�
                        }
                    }

                    if (doc.ToLower().IndexOf("href") != -1)
                    {
                        /* ���� str1 �����ݡ�*/
                        str1 = recordering;
                        isStartRecordLab = true; // ��ʼ��¼ ��ǩ��
                        //recordering = "";
                        str2 = "";
                        str3 = "";
                    }

                    if (doc.IndexOf("/a") != -1)
                    {
                        /* ���� str3 �����ݡ�*/
                        str3 = doc;
                        isChecUrl = true; //ִ�м��url ��������url �ļ�¼��
                        isStartRecordLab = false; // ��ʼ��¼ ��ǩ��
                    }
                }

                recordering += c.ToString();
                if (isStartRecordLab)
                    str2 += c.ToString();
                #endregion


                if (isChecUrl == false)
                    continue;

                // ���������û�м�¼�����Ͱ����Ƿ��ص���
                if (str1 == "" || str2 == "" || str3 == "")
                    continue;

                // ���� str1 . href='ab.htm   ' ;
                str1 = str1.Replace(" ",""); //�滻���ո�

                try
                {
                    if (str1.IndexOf("HREF") != -1)
                        str1 = str1.Substring(str1.IndexOf("HREF=") + 5);
                    else
                        str1 = str1.Substring(str1.IndexOf("href=") + 5);
                }
                catch (Exception ex)
                {
                    throw new Exception("����" + str1 + ex.Message);
                }

                int pos = str1.IndexOf("target");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); //ȥ��target ����Ĳ��֡�
                    // ˵�� �� target . 
                }

                pos = str1.IndexOf("class");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); //ȥ�� class ����Ĳ��֡�
                    // ˵�� �� class . 
                }


                pos = str1.IndexOf("style=");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); // ȥ�� class ����Ĳ��֡�
                    // ˵�� �� class . 
                }

                pos = str1.IndexOf("title");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); // ȥ�� class ����Ĳ��֡�
                    // ˵�� �� class . 
                }

                str1 = str1.Replace("<", "");
                str1 = str1.Replace(">", "");
                str1 = str1.Replace(" ", "");
                str1 = str1.Replace("'", "");
                str1 = str1.Replace(".html/", "html");
                str1 = str1.Replace(".HTML/", "HTML");

                str1 = str1.Replace(".html\\", "html");
                str1 = str1.Replace(".HTML\\", "HTML");
                str1 = str1.Replace("\"", "");

                str2 = str2.Replace(">", "");
                str2 = str2.Replace("<", "");

                if (str1.ToLower().Contains("to:"))
                    continue;

                if (str1.Contains("/#"))
                    continue;

                if (str1.ToLower().Contains(".com") || str1.ToLower().Contains(".net") || str1.ToLower().Contains(".cn"))
                    continue;
                

                string temUrl = str1;

                Href href = new Href();
                if (temUrl.Substring(0, 1) == "/")
                {
                    /*
                      /zone/abc.aspx �������
                     */
                    //  temUrl = path + "/" + temUrl;
                }
                else
                {
                    /*
                        abc.aspx �������
                    */
                    if (temUrl.ToLower().Contains("http://") == false)
                        temUrl = path + "/" + temUrl;
                }

                if (str1.Contains(":"))
                {
                    href.Url = temUrl;
                }
                else
                {
                    href.Url = "http://" + hostName + temUrl;
                }

                //try
                //{
                //    PubClass.CheckUrl(href.Url);
                //}
                //catch(Exception ex)
                //{
                //    int i = 100;
                //}

                href.Lab = str2.Trim();
                href.PageUrl = str1; //ԭʼ��url.

                

                href.HostName = hostName;
                href.HisEncoder = encoder;
                href.Path = path.ToLower();

                string[] strs = href.Url.Split('?');

                if (strs.Length > 2)
                {
                    Log.DefaultLogWriteLineInfo("������:"+href.Url );
                    continue;
                }

                if (hfs.Contains(href) == false)
                    hfs.Add(href);


                //�ָ����ñ���
                isChecUrl = false;
                str1 = "";
                str2 = "";
                str3 = "";
                recordering = "";
                doc = "";
                isStartRecordLab = false;
                isStartRecordMarket = false;
            }
            return hfs;
        }
        
        #endregion

    }
}

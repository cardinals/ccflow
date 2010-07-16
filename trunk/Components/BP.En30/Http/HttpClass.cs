using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Net;
using System.IO;

namespace BP.HTTP
{
    public class HTTPClass
    {
        public static long DownLoadFile(string url, string savePath, int timeOut)
        {
            CookieContainer cookie  =new CookieContainer();;
            long Filelength = 0;
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.CookieContainer = cookie;
            req.AllowAutoRedirect = true;
            req.Timeout = timeOut;

            HttpWebResponse res = req.GetResponse() as HttpWebResponse;
            System.IO.Stream stream = res.GetResponseStream();
            try
            {
                Filelength = res.ContentLength;

                byte[] b = new byte[512];
                int nReadSize = 0;
                nReadSize = stream.Read(b, 0, 512);

                System.IO.FileStream fs = System.IO.File.Create(savePath);
                try
                {
                    while (nReadSize > 0)
                    {
                        fs.Write(b, 0, nReadSize);
                        nReadSize = stream.Read(b, 0, 512);
                    }
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("�����ļ�����" + ex.Message);
            }
            finally
            {
                res.Close();
                stream.Close();
            }
            return Filelength;
        }

        public static string GenerTempFileByUrl(string url)
        {
            string tempfile = url;
            tempfile = tempfile.Replace("http://", "");
            tempfile = tempfile.Replace("HTTP://", "");
            tempfile = tempfile.Replace("/", "");
            tempfile = tempfile.Replace("?", "_");
            tempfile = tempfile.Replace("&", "_");
            tempfile = tempfile.Replace("=", "_");
            tempfile = tempfile.Replace(":", "_");
            tempfile = "C:\\StealTempFile\\" + tempfile + ".htm";
            return tempfile;
        }
        public static string ReadContext(string url, int timeOut, Encoding encode )
        {
            // string tempfile = GenerTempFileByUrl(url);
            HttpWebRequest webRequest = null;
            try
            {
                // Uri uri = new Uri(this.Url);
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                // webRequest.Timeout = Program.PageTimeout;
                webRequest.Timeout = timeOut;

                // webRequest.KeepAlive = false;
                string str = webRequest.Address.AbsoluteUri;
                // this.HostName = webRequest.Address.Host;
                str = str.Substring(0, str.LastIndexOf("/"));
                //this.Host = str;
            }
            catch (Exception ex)
            {
                try
                {
                    BP.DA.Log.DefaultLogWriteLineWarning("@��ȡURL���ִ���:URL=" + url + "@������Ϣ��" + ex.Message);
                    return null;
                }
                catch
                {
                    return ex.Message;
                }
            }

            //	��Ϊ�����ص�ʵ��������WebRequest������HttpWebRequest,��˼ǵ�Ҫ����ǿ������ת��
            //  ����������һ��HttpWebResponse�Ա���շ��������͵���Ϣ�����ǵ���HttpWebRequest.GetResponse����ȡ�ģ�

            HttpWebResponse webResponse;
            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                //webResponse
            }
            catch (Exception ex)
            {
                try
                {
                    // ������������ӡ�
                    BP.DA.Log.DefaultLogWriteLineWarning("@��ȡurl=" + url + "ʧ�ܡ��쳣��Ϣ:" + ex.Message, true);
                    return null;
                }
                catch
                {
                    return ex.Message;
                }
            }

            //���webResponse.StatusCode��ֵΪHttpStatusCode.OK����ʾ�ɹ�������Ϳ��Խ��Ŷ�ȡ���յ��������ˣ�
            // ��ȡ���յ�����

            Stream stream = webResponse.GetResponseStream();
            System.IO.StreamReader streamReader = new StreamReader(stream, encode);
            string content = streamReader.ReadToEnd();
            webResponse.Close();
            return content;
        }
        public static string StrReplaceChar(string str)
        {
            str = str.Replace(";", "��");
            str = str.Replace(":", "��");
            str = str.Replace("\r\n", "");
            str = str.Replace("?", "��");
            str = str.Replace("!", "��");
            str = str.Replace(",", "��");
            str = str.Replace("\"", "��");
            str = str.Replace("\n", "");
            str = str.Replace("\t", "");
            str = str.Replace("\t\n", "");
            str = str.Replace("\r", "");
            str = str.Replace(" ", "");

            return str;
        }
        #region ���ַ�������
        public static string ParseHtmlToText(string val)
        {
            if (val == null)
                return val;

            val = val.Replace("��ӭ����", "");
            val = val.Replace("���壺����С", "");
            val = val.Replace("βҳ", "");
            val = val.Replace("��һҳ", "");
            val = val.Replace("��һҳ", "");
            val = val.Replace("βҳ", "");
            val = val.Replace("ҳ��", "");

            val = val.Replace("��վ����", "");
            val = val.Replace("��ϵ����", "");
            val = val.Replace("��������", "");
            val = val.Replace("��վ��ͼ", "");
            val = val.Replace("��汨��", "");

            val = val.Replace("&nbsp;", " ");
            val = val.Replace("&raquo;", " ");

             
            val = val.Replace("  ", " ");

            val = val.Replace("</td>", "");
            val = val.Replace("</TD>", "");

            val = val.Replace("</tr>", "");
            val = val.Replace("</TR>", "");

            val = val.Replace("<tr>", "");
            val = val.Replace("<TR>", "");

            val = val.Replace("</font>", "");
            val = val.Replace("</FONT>", "");

            val = val.Replace("</table>", "");
            val = val.Replace("</TABLE>", "");


            val = val.Replace("<BR>", "\n\t");
            val = val.Replace("<BR>", "\n\t");
            val = val.Replace("&nbsp;", " ");

            val = val.Replace("<BR><BR><BR><BR>", "<BR><BR>");
            val = val.Replace("<BR><BR><BR><BR>", "<BR><BR>");
            val = val.Replace("<BR><BR>", "<BR>");

            val = StrDelFunc(val);

            val = val.Replace("<script", "{");
            val = val.Replace("</script>", "}");

            val = val.Replace("<!--", "{");
            val = val.Replace("-->", "}");

            val = StrDelBigBracket(val);
            val = StrDelBigBracket(val);
            val = StrDelBigBracket(val);
            val = StrDelBigBracket(val);


            char[] chs = val.ToCharArray();
            bool isStartRec = false;
            string recStr = "";
            foreach (char c in chs)
            {
                if (c == '<')
                {
                    recStr = "";
                    isStartRec = true; /* ��ʼ��¼ */
                }

                if (isStartRec)
                {
                    recStr += c.ToString();
                }

                if (c == '>')
                {

                    isStartRec = false;
                    if (recStr == "")
                    {
                        isStartRec = false;
                        continue;
                    }

                    /* ��ʼ�����������ڵĶ�����*/
                    string market = recStr.ToLower();
                    val = val.Replace(recStr, "");
                    isStartRec = false;
                    recStr = "";

                }
            }




            val = val.Replace("&gt;", "");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");

            val = val.Replace("\t", "");
            val = val.Replace("\n", "");
            val = val.Replace("\r", "");
            val = val.Replace("|", " ");

          

          

           
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("|", " ");
            return val;
        }
        public static string StrDelFunc(string val)
        {
            int idx = val.IndexOf("function");
            if (idx == -1)
                return val;

            val = val.Replace("function", "^function");
            char[]  chs = val.ToCharArray();
            bool startRec = false;
            string funcStr = "";
            int timesL = 0;
            int timesR = 0;
            foreach (char c in chs)
            {
                if (c == '^')
                {
                    startRec = true;
                    funcStr = "";
                    timesL = 0;
                    timesR = 0;
                }

                if (startRec)
                    funcStr += c;


                if (startRec)
                {
                    if (c == '}')
                    {
                        timesR++;
                    }

                    if (c == '{')
                    {
                        timesL++;
                    }
                }

                if (timesL == timesR)
                {
                    if (timesL >= 1)
                    {
                        val = val.Replace(funcStr, "");
                        startRec = false;
                        funcStr = "";
                        timesL = 0;
                        timesR = 0;
                    }
                }
            }

            return val;
        }
        /// <summary>
        /// ɾ���������м�Ĳ���
        /// </summary>
        /// <param name="docs"></param>
        /// <returns></returns>
        public static string StrDelBigBracket(string val)
        {
            if (val.IndexOf("{") == -1)
                return val;


            char[] chs = val.ToCharArray();
            bool startRec = false;
            string funcStr = "";
            int timesL = 0;
            int timesR = 0;
            foreach (char c in chs)
            {
                if (c == '{')
                {
                    startRec = true;
                    funcStr = "";
                    timesL = 0;
                    timesR = 0;
                }

                if (startRec)
                    funcStr += c;

                if (startRec)
                {
                    if (c == '}')
                    {
                        val = val.Replace(funcStr, "");
                        startRec = false;
                        funcStr = "";
                    }
                }
            }
            return val;
        }
        #endregion

        public static bool SaveAsFile(string url, string fileName)
        {
            string doc = BP.HTTP.HTTPClass.ReadContext(url, 5000, System.Text.Encoding.GetEncoding("gb2312"));
            StreamWriter sw = new StreamWriter(fileName, true);
            sw.Write(doc );
            //sw.Write(doc.Replace(" ", ""));
            sw.Close();
            return true;
        }

        public static string StrDelHeadBody(string htmldoc)
        {
            if (htmldoc == null)
                return "";

            string s= HTTPClass.StrGetContext("<body", "</body>", htmldoc);
            return s.Substring(1);
        }

        public static string StrGetContext(string from, string to, string myDocs)
        {
            // return myDocs;
            //from = "12";
            //to = "78";
            //myDocs = "0123456789";

            if (from == "" && to == "")
                return myDocs;

            string msg = "";
            string dealDocs = "";

            try
            {

                int fromPos = myDocs.IndexOf(from);
                if (fromPos == -1)
                {
                    msg += "@û�ҵ�[" + from + "]����ȷ�� from ����Ƿ���ȷ��";
                    dealDocs = myDocs;
                }
                else
                    dealDocs = myDocs.Substring(fromPos + from.Length);

                int toPos = dealDocs.LastIndexOf(to);
                if (toPos == -1)
                    msg += "@û�ҵ�[" + to + "]����ȷ�� to ����Ƿ���ȷ��";
                else
                    dealDocs = dealDocs.Substring(0, toPos);

                if (msg != "")
                    BP.DA.Log.DefaultLogWriteLineWarning("GetContext Error:" + msg);
            }
            catch (Exception ex)
            {
                throw new Exception(" StrGetContext ����:" + ex.Message + "@λ��:" + msg);
            }

            dealDocs = dealDocs.Replace("    ", " ");
            dealDocs = dealDocs.Replace("    ", " ");
            dealDocs = dealDocs.Replace("    ", " ");
            return dealDocs.Trim();
        }
        /// <summary>
        /// ����һ���ı���ȡhrefs,
        /// </summary>
        /// <param name="from">��</param>
        /// <param name="to">��</param>
        /// <param name="myDocs">�ı�</param>
        /// <param name="ysURL">ԭʼ��URL�������жϻ�·��</param>
        /// <param name="hrefFlag"></param>
        /// <returns></returns>
        public static Hrefs GenerHrefs(string from, string to, string myDocs, string ysURL, string hrefFlag)
        {
            if (myDocs == "EXIT")
                return new Hrefs(); /* �����������ļ����Ͳ�Ҫ��ȡ�ˡ�*/

            string docs = StrGetContext(from, to, myDocs);
            if (docs == null)
            {
                BP.DA.Log.DefaultLogWriteLineInfo("��ȡ Hrefs �ڼ����û����ָ�����ı����ҵ�from or to������url=" + ysURL);
            }

            Hrefs hrefs = new Hrefs();
            if (docs == null || docs == "")
            {
                string msg = " ��ȡ url �б�ʧ�ܣ���ȷ������Ƿ���ȷ @From=" + from + "@To=" + to + "@�������Ϣ�ο���־��";
                throw new Exception(msg);
            }
            return GenerHrefs(docs, hrefFlag, ysURL);
        }
        /// <summary>
        /// ����һ��url ��ȡ���Ļ���·����
        /// http://www.chichengsoft.com/update/demo.htm
        /// return 
        /// http://www.chichengsoft.com/update/
        /// </summary>
        /// <param name="url">ԭʼ��URL</param>
        /// <returns>���ػ�����Ϣ</returns>
        public static string GetBasePath(string url)
        {
            int i = url.LastIndexOf('/');
            string str =  url.Substring(0, i+1);
            if (str.Length == 7 && str.Contains("://"))
                return url;
            return str;
        }
        /// <summary>
        /// ����http://www.xyx.com/sww/sh.htm ���� http://www.xyx.com/
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHost(string url)
        {
            Uri uri = new Uri(url);
            return uri.Host;
        }
        /// <summary>
        /// �ҵ���Χ������
        /// </summary>
        /// <param name="docs">���ı�����</param>
        /// <param name="lab">Ҫ�ҵ�Ŀ��</param>
        /// <param name="span">Ŀ�����ҷ�Χ</param>
        /// <returns>ִ�е����ݣ�����������ͷ���lab.</returns>
        public static string StrGetSpan(string docs, string lab, int span)
        {
            int from = docs.IndexOf(lab);
            if (from == -1)
                return lab;
            from = from - span;
            if (from < 0)
                from = 0;

            try
            {
                return docs.Substring(from, span * 2 + lab.Length);
            }
            catch
            {
                return docs.Substring(from);
            }
        }
        /// <summary>
        /// �����ı���ȡurl.
        /// </summary>
        /// <param name="docs">�ı�</param>
        /// <param name="hrefFlag">url�еı�ǣ������ض��ı�ǲŸ�����¼��������������Ϊ�ձ�ʾȫ��¼ȡ</param>
        /// <param name="ysURL">ԭʼ��URL�����ڱ�ʾ����·��</param>
        /// <returns>�������</returns>
        public static Hrefs GenerHrefs(string docs, string hrefFlag, string ysURL)
        {
            //  docs = "template <a href=' asb.htm ' target=_blank >mylab</a> template <a href=' asb1.htm ' target=_blank >mylab2</a> wewww";

            Uri ysUri = new Uri(ysURL);
            string basePath = HTTPClass.GetBasePath(ysURL);
            Hrefs hfs = new Hrefs();
            char[] chars = docs.ToCharArray();
            bool isStartRecordMarket = false; // ��ʼ��¼ < 
            bool isStartRecordLab = false; // ��ʼ��¼ lab 

            string url = "";
            string str2 = "";
            string str3 = "";
            string Recing = "";
            string doc = "";

            bool isChecUrl = false;
            foreach (char c in chars)
            {
                try
                {
                    #region ��ʼ����str1
                    if (c == '<')
                    {
                        /*��ʼ��¼���*/
                        isStartRecordMarket = true;
                        Recing = "";
                        isStartRecordLab = false; // ֹͣ��¼lab.
                    }

                    if (c == '>')
                    {
                        isStartRecordMarket = false; // ֹͣ��¼���
                        //�жϼ�¼���������ݡ�
                        doc = Recing.ToLower();
                        if (doc.IndexOf("href") == -1)
                        {
                            /* ˵������ href ���� */
                            if (doc.IndexOf("/a") == -1)
                            {
                                /* ˵��Ҳ���� str3 ���� */
                                Recing = "";
                                continue; //��¼�ǵ���Ч�ı�ǡ�
                            }
                        }

                        if (doc.ToLower().IndexOf("href") != -1)
                        {
                            /* ���� str1 �����ݡ�*/
                            url = Recing;
                            isStartRecordLab = true; // ��ʼ��¼ ��ǩ��
                            //Recing = "";
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

                    Recing += c.ToString();

                    if (isStartRecordLab)
                        str2 += c.ToString();

                    #endregion

                }
                catch (Exception ex)
                {
                    throw new Exception("error0000" + ex.Message);
                }


                if (isChecUrl == false)
                    continue;

                // ���������û�м�¼�����Ͱ����Ƿ��ص���
                if (url == "" || str2 == "" || str3 == "")
                    continue;

                try
                {

                    #region ����������
                    // ���� str1 . href='ab.htm   ' ;
                    if (url.IndexOf("HREF") != -1)
                        url = url.Substring(url.IndexOf("HREF=") + 5);
                    else
                        url = url.Substring(url.IndexOf("href=") + 5);


                    int pos = url.IndexOf("target");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //ȥ��target ����Ĳ��֡�
                        // ˵�� �� target . 
                    }

                    pos = url.IndexOf("class");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //ȥ��class ����Ĳ��֡�
                        // ˵�� �� class . 
                    }

                    pos = url.IndexOf("title");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //ȥ��title ����Ĳ��֡�
                        // ˵�� �� class . 
                    }

                    pos = url.IndexOf("style");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //ȥ��style ����Ĳ��֡�
                        // ˵�� �� class . 
                    }

                    #endregion ����������

                }
                catch (Exception ex)
                {
                    throw new Exception("error1111" + ex.Message);
                }

                url = url.Replace("<", "");
                url = url.Replace(">", "");
                url = url.Replace(" ", "");
                url = url.Replace("'", "");
                url = url.Replace(".html/", "html");
                url = url.Replace(".HTML/", "HTML");

                url = url.Replace(".html\\", "html");
                url = url.Replace(".HTML\\", "HTML");
                url = url.Replace("\"", "");

                str2 = str2.Replace(">", "");
                str2 = str2.Replace("<", "");

                if (url.ToLower().Contains(":") == false)
                {

                    /* �������������·������Ҫ���ǻ���·���� ��ǰ�����ӡ�
                     * ���磺 str1= '/N1232/N2323/xya.htm'  
                     */

                    if (url.Substring(0, 1) == "/")
                    {
                        /*����Ǵӻ���·����ʼ�ġ�*/
                        url = "http://" + ysUri.Authority + "" + url;
                    }
                    else
                    {

                        string[] strs = url.Split('/');
                        string myurl = basePath + "/";
                        foreach (string s in strs)
                        {
                            if (s == null)
                                continue;

                            if (myurl.Contains("/" + s) == false)
                                myurl += "/" + s;
                        }
                        url = myurl.Replace("//", "/").Replace("//", "/"); // = basePath + str1;
                        url = url.Replace(":/", "://");
                    }
                }

                #region ��ִ�еĽ���Ž�ȥ��
                Href href = new Href();
                try
                {
                    href.Url = url.Trim();
                }
                catch
                {
                    //�ָ����ñ���
                    isChecUrl = false;
                    url = "";
                    str2 = "";
                    str3 = "";
                    Recing = "";
                    doc = "";
                    isStartRecordLab = false;
                    isStartRecordMarket = false;
                    continue;
                }

                href.Lab = str2.Trim();
                if (hrefFlag != null)
                {
                    if (href.Url.Contains(hrefFlag) == false)
                        continue;
                }

                if (hfs.Contains(href) == false)
                {
                    hfs.Add(href);
                }

                //�ָ����ñ���
                isChecUrl = false;
                url = "";
                str2 = "";
                str3 = "";
                Recing = "";
                doc = "";
                isStartRecordLab = false;
                isStartRecordMarket = false;
                #endregion

            }
            return hfs;
        }
        public static void MakeDir(string dirFullName)
        {
            try
            {
                System.IO.Directory.CreateDirectory(dirFullName);
            }
            catch
            {
            }
        }
    }
}

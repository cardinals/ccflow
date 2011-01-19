using System;
using System.Web;
using System.Data;
using System.Text;

namespace Tax666.Common
{
    /// <summary>
    /// ϵͳ��ʾ��ͬʱ��ֹҳ�洫�估��Ӧ
    /// </summary>
    public class Terminator
    {
        #region ҳ������ַ���
        private void Echo(string s)
        {
            HttpContext.Current.Response.Write(s);
        }
        #endregion

        #region ��ֹҳ�������ʾ
        /// <summary>
        /// ��ֹҳ�������ʾ
        /// </summary>
        private void End()
        {
            HttpContext.Current.Response.End();
        }
        #endregion

        #region Javascript��Alert��ʾ
        /// <summary>
        /// alert javascript
        /// </summary>
        /// <param name="s"></param>
        public virtual void Alert(string s)
        {
            Echo("<script language='javascript'>alert('" + s.Replace("'", @"\'") + "');history.back();</script>");
            End();
        }

        /// <summary>
        /// ҳ���ַ��ת����
        /// </summary>
        /// <param name="s">��ʾ�ַ���</param>
        /// <param name="backurl">��ת��ַ</param>
        public virtual void Alert(string s, string backurl)
        {
            Echo("<script language='javascript'>alert('" + s.Replace("'", @"\'") + "');location.href='" + backurl + "';</script>");
            End();
        }
        #endregion

        #region �׳��쳣��Ϣ
        /// <summary>
        /// �׳��쳣��Ϣ
        /// </summary>
        /// <param name="message">�����쳣��Ϣ</param>
        public virtual void Throw(string message)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            string linkurl = "<li><a href='" + WebBootPath + "/Default.aspx'>������ҳ</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            Throw(message, null, linkurl, null, true);
        }
        #endregion

        #region �׳��쳣��Ϣ
        /// <summary>
        /// �׳��쳣��Ϣ
        /// </summary>
        /// <param name="message">�����쳣��Ϣ</param>
        public virtual void ThrowError(string message)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            Throw(message, null, null, null, true);
        }

        /// <summary>
        /// �׳��쳣��Ϣ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="retUrl">���ص����ӵ�ַ</param>
        public virtual void ThrowError(string message, string retUrl)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            Throw(message, null, retUrl, null, true);
        }
        #endregion

        #region ���ָ������ʾ��Ϣ
        /// <summary>
        /// ���ָ������ʾ��Ϣ
        /// </summary>
        /// <param name="message">��ʾ����</param>
        /// <param name="title">����</param>
        /// <param name="links">���ӵ�ַ</param>
        /// <param name="autojump">�Զ���ת�����ַ</param>
        /// <param name="showback">�Ƿ���ʾ��������</param>
        public virtual void Throw(string message, string title, string links, string autojump, bool showback)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            StringBuilder sb = new StringBuilder(template);

            sb.Replace("{$Message}", message);
            sb.Replace("{$Title}", (title == null || title == "") ? "ϵͳ��ʾ" : title);

            if (links != null && links != "" && !showback)
            {
                string s = "<li style='padding-left:20px;'>" + links + "</li>";
                sb.Replace("{$Links}", s);
            }

            if (autojump != null && autojump != string.Empty)
            {
                string s = autojump == "back" ? "javascript:history.back()" : autojump;
                //5���Ӻ����ҳ����ת��
                sb.Replace("{$AutoJump}", "<meta http-equiv='refresh' content='5; url=" + s + "' />");
            }
            else
            {
                sb.Replace("{$AutoJump}", "<!-- no jump -->");
            }

            if (showback)
            {
                if (links != null)
                    sb.Replace("{$Links}", "<a href='" + links + "'>������һҳ</a></li>");
                else
                    sb.Replace("{$Links}", "<li><a href='javascript:history.back()'>������һҳ</a></li>");
            }
            else
            {
                sb.Replace("{$Links}", "<!-- no back -->");
            }
            Echo(sb.ToString());
            End();
        }
        #endregion

        #region ҳ����ֹҳ��ģ��
        /// <summary>
        /// ҳ����ֹҳ��ģ��
        /// </summary>
        public virtual string template
        {
            get
            {
                return @"<html xmlns:v>
				<head>
				<title>{$Title}</title>
				<meta http-equiv='Content-Type' content='text/html; charset=" + Encoding.Default.BodyName + @"' />
				<meta name='description' content='.NET��� ҳ����ֹ����' />
				<meta name='copyright' content='http://www.zmsoft.net/' />
				<meta name='generator' content='vs2005' />
				<meta name='usefor' content='application termination' />
				{$AutoJump}
				<style rel='stylesheet'>
				v\:*	{
					behavior:url(#default#vml);
				}

				body, div, span, li, td, a {
					color: #222222;
					font-size: 12px !important;
					font-size: 11px;
					font-family: tahoma, arial, 'courier new', verdana, sans-serif;
					line-height: 19px;
				}
				a {
					color: #2c78c5;
					text-decoration: none;
				}
				a:hover {
					color: red;
					text-decoration: none;
				}
				</style>
				</head>
				<body style='text-align:center;margin:90px 20px 50px 20px'>
				<?xml:namespace prefix='v' />
				<div style='margin:auto; width:450px; text-align:center'>
					<v:roundrect style='text-align:left; display:table; margin:auto; padding:15px; width:450px; height:210px; overflow:hidden; position:relative;' arcsize='3200f' coordsize='21600,21600' fillcolor='#fdfdfd' strokecolor='#e6e6e6' strokeweight='1px'>
						<table width='100%' cellpadding='0' cellspacing='0' border='0' style='padding-bottom:6px; border-bottom:1px #cccccc solid'>
							<tr>
								<td><b>{$Title}</b></td>
								<td align='right' style='color:#c0c0c0'>--- ���������365����ϵͳ���������С�</td>
							</tr>
						</table>
						<table width='100%' cellpadding='0' cellspacing='0' border='0' style='word-break:break-all; overflow:hidden'>
							<tr>
								<td width='80' valign='top' style='padding-top:13px'><font style='font-size:16px; zoom:4; color:#aaaaaa;font-family:webdings;'>i</font></td>
								<td valign='top' style='padding-top:17px'>
									<p style='margin-bottom:22px'>{$Message}</p>
									{$Links}
								</td>
							</tr>
						</table>
					</v:roundrect>
				</div>
				</body>
				</html>";
            }
        }
        #endregion

        #region ����ϵͳ��װ���Web��Ŀ¼�����·��(/Tax666 )
        /// <summary>
        /// ����ϵͳ��װ���Web��Ŀ¼�����·��(/Tax666 )��
        /// </summary>
        public static string WebBootPath
        {
            get
            {
                if (!HttpContext.Current.Request.Url.IsDefaultPort)
                {
                    return @"http://" + string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString());
                }
                else
                {
                    return @"http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                }
            }
        }
        #endregion

    }
}

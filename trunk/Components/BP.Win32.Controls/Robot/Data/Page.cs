using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// ��ҳ����
	/// </summary>
    public class PageAttr : EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// DocHtml
        /// </summary>
        public const string DocHtml = "DocHtml";
        /// <summary>
        /// DocText
        /// </summary>
        public const string DocText = "DocText";
        /// <summary>
        /// Hrefs
        /// </summary>
        public const string Hrefs = "Hrefs";
        /// <summary>
        /// ����
        /// </summary>
        public const string NumOfHrefs = "NumOfHrefs";
        /// <summary>
        /// URL
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// RDT
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 
        /// </summary>
        public const string PageSize = "PageSize";
        /// <summary>
        /// Emails
        /// </summary>
        public const string Emails = "Emails";
        /// <summary>
        /// Files
        /// </summary>
        public const string Files = "Files";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_WebSite = "FK_WebSite";
        /// <summary>
        /// HostName
        /// </summary>
        public const string HostName = "HostName";


        #endregion


        #region Tag
        public const string T0 = "T0";
        public const string T1 = "T1";
        public const string T2 = "T2";
        public const string T3 = "T3";
        public const string T4 = "T4";
        public const string T5 = "T5";
        public const string T6 = "T6";
        public const string T7 = "T7";
        public const string T8 = "T8";
        public const string T9 = "T9";
        #endregion
    }
	/// <summary>
	/// ��ҳ����
	/// </summary>
    public class Page : EntityNoName
    {
        #region ��������

        public string FK_WebSite
        {
            get
            {
                return this.GetValStringByKey(PageAttr.FK_WebSite);
            }
            set
            {
                this.SetValByKey(PageAttr.FK_WebSite, value);
            }
        }
        /// <summary>
        /// HostName
        /// </summary>
        public string HostName
        {
            get
            {
                return this.GetValStringByKey(PageAttr.HostName);
            }
            set
            {
                this.SetValByKey(PageAttr.HostName, value);
            }
        }
        public string Files
        {
            get
            {
                return this.GetValStringByKey(PageAttr.Files);
            }
            set
            {
                this.SetValByKey(PageAttr.Files, value);
            }
        }
        public string Emails
        {
            get
            {
                return this.GetValStringByKey(PageAttr.Emails);
            }
            set
            {
                this.SetValByKey(PageAttr.Emails, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(PageAttr.RDT);
            }
            set
            {
                this.SetValByKey(PageAttr.RDT, value);
            }
        }
        /// <summary>
        /// ��ҳ���ݱ���
        /// </summary>
        public string DocHtml
        {
            get
            {
                return this.GetValStringByKey(PageAttr.DocHtml);
            }
            set
            {
                this.SetValByKey(PageAttr.DocHtml, value);
                this.PageSize = value.Length;
            }
        }
        public string DocText
        {
            get
            {
                return this.GetValStringByKey(PageAttr.DocText);
            }
            set
            {
                this.SetValByKey(PageAttr.DocText, value);
            }
        }
        public string Url
        {
            get
            {
                return this.GetValStringByKey(PageAttr.Url);
            }
            set
            {
                this.SetValByKey(PageAttr.Url, value);
            }
        }
        /// <summary>
        /// Hrefs
        /// </summary>
        public int NumOfHrefs
        {
            get
            {
                return this.GetValIntByKey(PageAttr.NumOfHrefs);
            }
            set
            {
                this.SetValByKey(PageAttr.NumOfHrefs, value);
            }
        }
        public int PageSize
        {
            get
            {
                return this.GetValIntByKey(PageAttr.PageSize);
            }
            set
            {
                this.SetValByKey(PageAttr.PageSize, value);
            }
        }
        #endregion

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// ��ҳ����
        /// </summary>		
        public Page()
        {
        }
        /// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="no"></param>
        public Page(string no)
            : base(no)
        {
        }
        /// <summary>
        /// PageMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "RB_Page";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "��ҳ";
                map.EnType = EnType.App;
                #endregion

                #region ��������
                map.AddTBStringPK(PageAttr.No, null, "���", true, true, 1, 500, 4);
                map.AddTBString(PageAttr.Name, null, "��ҳ����", true, true, 0, 4000, 30);
                map.AddTBString(PageAttr.Url, null, "URL", true, true, 0, 500, 30);

                map.AddTBString(PageAttr.FK_WebSite, null, "������վ", true, true, 0, 500, 30);
                map.AddTBString(PageAttr.HostName, null, "����", true, true, 0, 500, 30);

                map.AddTBStringDoc(PageAttr.DocHtml, null, "DocHtml", true, false);
                map.AddTBStringDoc(PageAttr.DocText, null, "DocText", true, false);
                map.AddTBStringDoc(PageAttr.Hrefs, null, "Hrefs", true, false);


                map.AddTBInt(PageAttr.NumOfHrefs, 0, "NumOfHrefs", true, true);
                map.AddTBInt(PageAttr.PageSize, 0, "PageSize", true, true);

                map.AddTBStringDoc(PageAttr.Emails, null, "Emails", true, false);
                map.AddTBStringDoc(PageAttr.Files, null, "Files", true, false);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ���ط���
        #endregion

        #region ����
        private bool IsCN(string str)
        {
            Boolean strResult;
            String cn_Regex = @"^[\u4e00-\u9fa5]+$";
            if (Regex.IsMatch(str, cn_Regex))
            {
                strResult = true;
            }
            else
            {
                strResult = false;
            }
            return strResult;
        }
        #endregion


        #region ��ӹ�����
        /// <summary>
        /// ���� 
        /// </summary>
        /// <returns></returns>
        public int GenerTag(Tags tgs)
        {


            int i = 0;
            foreach (Tag tg in tgs)
            {
                int idx = this.DocText.IndexOf(tg.Name);
                if (idx >= 0)
                {

                    PageTag pt = new PageTag();

                    pt.No = this.No + "@" + tg.OID;
                    pt.Name = this.Name;

                    pt.HostName = this.HostName;
                    WebSite ws = new WebSite(pt.HostName);
                    pt.HostNameText = ws.HostName;

                    pt.FK_Tag = tg.OID;
                    pt.FK_TagText = tg.Name;
                    pt.URL = this.Url;
                    pt.RDT = DataType.CurrentDataTimeCNOfShort;

                    int from = 0;

                    if (idx >= 20)
                        from = idx - 19;
                    else
                        from = 0;


                    try
                    {
                        pt.Docs = this.DocText.Substring(from, 500);
                    }
                    catch
                    {
                        pt.Docs = this.DocText.Substring(from);
                    }

                    //pt.Docs = PubClass.StringCutHtml(pt.Docs);

                    // ���� DocHtml ������Ϣ��
                    pt.Insert();
                    i++;
                }

            }
            return i;
        }
        public int GenerTag_del()
        {

            Tags tgs = new Tags();
            tgs.RetrieveAll();

            int i = 0;
            foreach (Tag tg in tgs)
            {
                if (this.DocHtml.Contains(tg.Name))
                {
                    this.SetValByKey("T" + i, tg.OID);
                    i++;
                }

                if (i == 10)
                    break;
            }


            if (i > 0)
                this.Update();

            return i;
        }
        /// <summary>
        /// GenerEmails
        /// </summary>
        /// <returns></returns>
        public int GenerEmails()
        {
            if (this.DocHtml.Contains("@") == false)
                return 0;

            string body = this.DocHtml.ToLower();
            body = body.Replace("\t", "");
            body = body.Replace("\n", "");
            body = body.Replace("<td>", "");
            body = body.Replace("<tr>", "");
            body = body.Replace("<html>", "");
            body = body.Replace("</html>", "");
            body = body.Replace("</table>", "");
            body = body.Replace("title", "");
            body = body.Replace("img", "");
            body = body.Replace("<!--", "");
            body = body.Replace("-->", "");
            body = body.Replace("mailto:", "");

            char[] chs = body.ToCharArray();

            // string mailEnd = ",.com,.net,.cn,.org,";

            string recordering = "";
            string mails = "";
            bool isCheckMail = false;
            foreach (char c in chs)
            {
                switch (c)
                {
                    case '=':
                    case ':':
                    case '��':
                    case '<':
                        recordering = "";
                        continue;
                    default:
                        break;
                }

                // �Ƿ������ġ�
                bool iscn = this.IsCN(c.ToString());
                if (iscn && recordering.Contains("@") == false)
                {
                    /*��ǰ�����ģ����һ������� @ ��*/
                    recordering = "";
                    continue;
                }

                #region �����ַ�
                switch (c)
                {
                    case '@':
                        recordering += c.ToString();
                        break;
                    case '\t':
                    case '\r':
                    case '\n':
                    case '>':
                    case '<':
                        continue;
                    case '?':
                    case ',':
                    case '/':
                    case '}':
                    case ']':
                    case '|':
                    case '\'':
                    case '!':
                    case '#':
                    case '$':
                    case '%':
                    case '^':
                    case '&':
                    case '*':
                    case '(':
                    case ')':
                        break;
                    default:
                        recordering += c.ToString();
                        break;
                }
                #endregion

                if (recordering.Contains("@") == false)
                    continue;


                // ����Ƿ���mail��������
                isCheckMail = false;
                if (c == 32   // �ո� .
                    || iscn   // �Ǻ��ּ�顣
                    || c == ','
                    || c == '��'
                    || c == '��'
                    || c == '��'
                    || c == '>'
                    || c == '\''
                    || c == '\"'
                    || c == ':'
                    || c == '��'
                    || c == '<'
                    )
                {

                    isCheckMail = true;
                }

                if (isCheckMail)
                {
                    /*�����ʼִ�м�飬 ��ִ�����²����� */
                    if (recordering.Contains(".com")
                        || recordering.Contains(".cn")
                        || recordering.Contains(".net")
                        || recordering.Contains(".edu")
                        || recordering.Contains(".org")
                        )
                    {
                        if (mails.Contains(recordering.Trim()) == false)
                        {
                            mails += "#" + recordering.Trim();
                        }
                        recordering = "";
                        continue;
                    }
                    else
                    {
                        recordering = "";
                        continue;
                    }
                }
            }

            if (mails == "")
                return 0;

            mails = mails.Replace("\"", "");

            this.Emails = mails;
            string[] strs = mails.Split('#');
            foreach (string str in strs)
            {
                if (str == null || str.Length <= 3)
                    continue;

                PageMail ml = new PageMail();
                ml.No = str;
                ml.Name = str;
                if (ml.IsExits)
                    continue;

                ml.RDT = DataType.CurrentData;
                ml.SDT = DataType.CurrentData;
                ml.SendTimes = 0;
                ml.Insert();
            }

            this.Update(PageAttr.Emails, this.Emails);
            return 100;
        }
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <returns></returns>
        public int GenerFiles(Hrefs hfs, string pageUrl)
        {

            string body = this.DocHtml;
            int i = 0;
            foreach (Href hf in hfs)
            {
                if (hf.IsFile == false)
                    continue;

                PageFile pf = new PageFile();
                pf.No = hf.Url;
                if (pf.IsExits)
                    continue;

                #region ��ȡ������Ϣ��
                int idx = this.DocHtml.IndexOf(hf.PageUrl, StringComparison.CurrentCultureIgnoreCase);
                int from = idx - 500;

                if (from <= 0)
                    from = 0;

                int leng = 1000;
                int leftLen = this.DocHtml.Length - from;
                if (leftLen <= 1000)
                    leng = leftLen;

                string desc = this.DocHtml.Substring(from, leng);
                desc = PubClass.StringCutHtml(desc);
                pf.FDesc = desc;
                #endregion


                #region ��Ч�Ĵ����ļ�����
                string name = hf.Lab;
                pf.Name = name;
                #endregion


                pf.FK_WebSite = hf.HostName;
                pf.RDT = DataType.CurrentDataCNOfShort;
                pf.PageUrl = pageUrl;
                pf.Insert();

                i++;
            }
            //   Hrefs hfs = PubClass.GetHrefs(this.DocHtml,this.fk
            return i;
        }
        #endregion
    }
	/// <summary>
	/// ��ҳ����
	/// </summary>
    public class Pages : EntitiesNoName
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Page();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��ҳ����
		/// </summary>
		public Pages(){}


        public Pages(string hostNmae) 
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(PageAttr.FK_WebSite, hostNmae);
            qo.DoQuery();

        }
		#endregion

	 
	}
	
}

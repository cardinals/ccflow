using System;
using System.Data;
using System.IO;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// ��վ
	/// </summary>
    public class WebSiteAttr : EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// ��־
        /// </summary>
        public const string Log = "Log";
        /// <summary>
        /// 
        /// </summary>
        public const string Note1 = "Note1";
        /// <summary>
        /// Note
        /// </summary>
        public const string Note2 = "Note2";
        public const string Url = "Url";
        public const string RDT = "RDT";
     //   public const string HostIP = "HostIP";
        public const string HostName = "HostName";

        public const string NumOfPage = "NumOfPage";
        /// <summary>
        /// 
        /// </summary>
        public const string UDTFrom = "UDTFrom";
        public const string UDTTo = "UDTTo";
        public const string S = "S";
        public const string UDT = "UDT";
        public const string IsEnable = "IsEnable";
        public const string FK_WebSiteType = "FK_WebSiteType";
        public const string FK_Encode = "FK_Encode";
        /// <summary>
        /// �ٶ�
        /// </summary>
        public const string Pace = "Pace";
        /// <summary>
        /// �ļ�����
        /// </summary>
        public const string NumOfFile = "NumOfFile";
        #endregion
    }
	/// <summary>
	/// ��վ
	/// </summary>
    public class WebSite : EntityOID
    {
        #region ��������
        public string Log
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Log);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Log, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Name);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Name, value);
            }
        }
        public System.Text.Encoding HisEncode
        {
            get
            {
                return System.Text.Encoding.GetEncoding(this.FK_Encode);
                //  return System.Text.Encoding.GetEncoding(this.FK_Encode);
            }
        }
        public string FK_Encode
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.FK_Encode);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.FK_Encode, value);
            }
        }
        public string UDT
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.UDT);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.UDT, value);
            }
        }
        public int NumOfFile
        {
            get
            {
                return this.GetValIntByKey(WebSiteAttr.NumOfFile);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.NumOfFile, value);
            }
        }
        public int S
        {
            get
            {
                return this.GetValIntByKey(WebSiteAttr.S);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.S, value);
            }
        }
        public string UDTTo
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.UDTTo);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.UDTTo, value);
            }
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        public string UDTFrom
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.UDTFrom);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.UDTFrom, value);
            }
        }
        /// <summary>
        /// NumOfPage
        /// </summary>
        public int NumOfPage
        {
            get
            {
                return this.GetValIntByKey(WebSiteAttr.NumOfPage);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.NumOfPage, value);
            }
        }
      
        public decimal Pace
        {
            get
            {
                return this.GetValDecimalByKey(WebSiteAttr.Pace);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Pace, value);
            }
        }
        /// <summary>
        /// HostName
        /// </summary>
        public string HostName
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.HostName);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.HostName, value);
            }
        }
        /// <summary>
        /// Url
        /// </summary>
        public string Url
        {
            get
            {
                string url = this.GetValStringByKey(WebSiteAttr.Url);
                if (url.ToLower().Contains("http://") == false)
                    url = "http://" + url;

                url = url.Replace("\t", "");
                url = url.Replace("\n", "");
                url = url.Replace("\r", "");
                return url;
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Url, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.RDT);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.RDT, value);
            }
        }
        /// <summary>
        /// ��վ����
        /// </summary>
        public string Note1
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Note1);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Note1, value);
            }
        }
        /// <summary>
        /// Note2
        /// </summary>
        public string Note2
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Note2);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Note2, value);
            }
        }

        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(WebSiteAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.IsEnable, value);
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
        /// ��վ
        /// </summary>		
        public WebSite()
        {

        }
        /// <summary>
        /// ��վ
        /// </summary>
        /// <param name="no"></param>
        public WebSite(int no)
            : base(no)
        {
        }
        public WebSite(string hostname)
        {
            this.Retrieve(WebSiteAttr.HostName, hostname);
        }
        /// <summary>
        /// WebSiteMap
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
                map.PhysicsTable = "RB_WebSite";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "��վ";
                map.CodeStruct = "4";
                map.EnType = EnType.App;
                #endregion

                #region ��������
                map.AddTBIntPKOID();

                map.AddTBString(WebSiteAttr.Name, null, "��վ����", true, false, 0, 400, 30);
                map.AddTBString(WebSiteAttr.Url, null, "��ʼ��", true, false, 0, 700, 30);
                map.AddTBString(WebSiteAttr.HostName, null, "��������", true, false, 0, 700, 30);
                map.AddBoolean(WebSiteAttr.IsEnable, true, "�Ƿ����", true, true);
                map.AddTBDate(WebSiteAttr.RDT, "��¼����", true, true);

                //map.AddTBStringDoc(WebSiteAttr.Note1, null, "��ע1", true, false);
                //map.AddTBStringDoc(WebSiteAttr.Note2, null, "��ע2", true, false);
                // map.AddTBString(WebSiteAttr.HostIP, null, "����IP", true, false, 0, 700, 30);


                map.AddTBDate(WebSiteAttr.UDT, null, "��ȡ����", true, true);
                map.AddTBInt(WebSiteAttr.NumOfPage, 0, "ҳ����", true, true);
                map.AddTBInt(WebSiteAttr.NumOfFile, 0, "�ļ���", true, true);
                map.AddTBInt(WebSiteAttr.S, 0, "��ʱ(��)", true, true);
                map.AddTBDecimal(WebSiteAttr.Pace, 0, "��ȡ�ٶ�(s/p)", true, true);

                //map.AddTBString(WebSiteAttr.UDTFrom, null, "��ȡ���ڴ�", true, true, 0, 700, 30);
                //map.AddTBString(WebSiteAttr.UDTTo, null, "��", true, true, 0, 700, 30);
                


                map.AddDDLEntities(WebSiteAttr.FK_WebSiteType, "01", "��վ����", new BP.SE.WebSiteTypes(), true);
                map.AddDDLEntities(WebSiteAttr.FK_Encode, "GB2312", "��������", new Encodes(), true);

                map.AddTBString(WebSiteAttr.Log, null, "��־", true, false, 0, 7000, 30);
                #endregion

                RefMethod rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerPageFace";
                rm.Title = "ץȡ";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerTag";
                rm.Title = "��ȡ��ǩ";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerFile";
                rm.Title = "��ȡ�ļ�";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerFLink";
                rm.Title = "��ȡ�ⲿ����";
                map.AddRefMethod(rm);



                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoRelease";
                rm.Title = "��Ϣ����";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoAll";
                rm.Title = "ִ��ȫ��";
                map.AddRefMethod(rm);

                map.AddSearchAttr(WebSiteAttr.FK_WebSiteType);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ����
        public string DoAll()
        {
            // ��ѯȫ����
            WebSites ens = new WebSites();
            ens.RetrieveAll();

            int i = 0;
            foreach (WebSite en in ens)
            {
                i++;
                if (en.IsEnable == false)
                    continue;

                try
                {
                    BP.DA.Log.DefaultLogWriteLineInfo("&&&&&&&&&&&&&&&& ��ʼִ����վ��" + en.Name + " ���ȣ�" + i + " / " + ens.Count);

                    // ִ��ǰ��顣
                    en.DoBeforeRun();
                    if (en.IsEnable == false)
                        continue;

                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerPage());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerTag());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerFLink());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerFile());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoRelease());
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError(ex.Message);
                }
            }

            BP.DA.Log.OpenLogDir();
            return "�ɹ�ִ����ɡ�";
        }
        /// <summary>
        /// ��ȡ�ļ�
        /// </summary>
        /// <returns></returns>
        public string DoGenerFile()
        {
            Pages pgs = new Pages();
            int i = 0;
            try
            {
                DBAccess.RunSQL(" delete RB_PageFile where HostName='" + this.HostName + "'");
                pgs.Retrieve(PageAttr.HostName, this.HostName);
                try
                {
                    foreach (Page pg in pgs)
                    {
                        Hrefs hfs = PubClass.GetHrefs(pg.Url, this.HisEncode);
                        try
                        {
                            i += pg.GenerFiles(hfs, pg.Url);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "@ִ��DoGenerFile���ִ���" + ex.Message;
            }
            return "ִ����ϣ�ִ����ҳ[" + pgs.Count + "]�����ҵ��ļ�[" + i + "]����";
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="hfs"></param>
        /// <returns></returns>
        public int GenerFLink(Hrefs hfs)
        {
            int num = 0;
            foreach (Href hf in hfs)
            {
                if (hf.IsLocalHost)
                    continue;

                try
                {
                    WebSiteLink wl = new WebSiteLink();
                    wl.HostName = hf.HostName;
                    wl.No = hf.HostNameOfUrl;
                    wl.Name = hf.Lab;
                    wl.Insert();
                    num++;
                }
                catch (Exception ex)
                {
                }
            }

            return num;
        }
        public string DoGenerFLink()
        {
            DBAccess.RunSQL(" delete RB_WebSiteLink where HostName='" + this.HostName + "'");
            Pages pgs = new Pages();
            pgs.Retrieve(PageAttr.HostName, this.HostName);

            int i = 0;
            try
            {
                foreach (Page pg in pgs)
                {
                    Hrefs hfs = PubClass.GetHrefs(pg.Url, this.HisEncode);
                    try
                    {
                        i += this.GenerFLink(hfs);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            string sql = "DELETE RB_WebSiteLink WHERE NO IN (SELECT HOSTNAME FROM RB_WebSite)";
            int num = DBAccess.RunSQL(sql);

            return "ִ����ϣ�ִ����ҳ[" + pgs.Count + "]�����ҵ��ⲿ����[" + i + "]�������ظ���[" + num + "]����";
        }
        public string DoGenerPage()
        {
            try
            {
                string docs = PubClass.ReadContext(this.Url, this.HisEncode);
                Hrefs hfs = PubClass.GetHrefs(this.Url, this.HisEncode);
                foreach (Href hf in hfs)
                    PubClass.DoPage(hf);

                return "��ҳ�ɹ�ִ����ɡ�";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DoGenerPageFace()
        {

            Frm.FrmRunWebSiteOne frm = new BP.RB.Frm.FrmRunWebSiteOne();
            frm.HisWebSite = this;
            frm.Text = this.Url + this.Name;
            frm.ShowDialog();
            return null;
        }
        /// <summary>
        /// ����Tag
        /// </summary>
        /// <returns></returns>
        public string DoGenerTag()
        {
            DBAccess.RunSQL(" delete RB_PageTag where HostName='" + this.HostName + "'");

            Pages pgs = new Pages();
            pgs.Retrieve(PageAttr.HostName, this.HostName);

            Tags tgs = new Tags();
            tgs.RetrieveAll();

            int i = 0;
            try
            {
                foreach (Page pg in pgs)
                    i = i + pg.GenerTag(tgs);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "ִ����ϣ�ִ����ҳ[" + pgs.Count + "]�����ҵ���ǩ[" + i + "]����";
        }
        /// <summary>
        /// ���ļ���������վ
        /// </summary>
        /// <returns></returns>
        public string DoRelease()
        {
            string msg = "";
            // Release page
            string sql = "delete SE_Page WHERE HOSTNAME='" + this.HostName + "'";
            int count = DBAccess.RunSQL(sql);

            sql = "insert  SE_Page (URL,Name,HostName,docHtml,docText) SELECT No as URL,Name,HostName,docHtml,docText from RB_Page WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            sql = "UPDATE SE_Page SET RDT='" + DataType.CurrentDataCNOfShort + "' WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            msg += "��ҳ������Ϣ��" + count + "����\t\n";

            // Release file
            sql = "delete SE_PageFile WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            sql = "INSERT  SE_PageFile (URL,Name,HostName,FDesc,PageUrl) SELECT No as URL,Name,HostName,FDesc,PageUrl FROM RB_PageFile WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            sql = "UPDATE SE_PageFile SET RDT='" + DataType.CurrentDataCNOfShort + "' WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);
            msg += "��ҳ������Ϣ��[" + count + "]����";

            return msg;
        }
        /// <summary>
        /// ִ��ǰҪ���е�
        /// </summary>
        public void DoBeforeRun()
        {
            #region ���������������Ƿ����
            try
            {
                Uri uri = new Uri(this.Url);
                string path = uri.AbsolutePath;
                path = path.Substring(0, path.LastIndexOf('/') + 1);
                path = path.ToLower();

                string hostName = uri.Authority.ToLower();
                Uri u = new Uri(this.Url);
                this.HostName = u.Authority;
            }
            catch (Exception ex)
            {
                this.Log = "��������ʱ���ִ��󣺿�������㲻��ȷ�������������Ӳ��ϡ�" + ex.Message;
                this.IsEnable = false;
                this.Update();
            }
            #endregion


            BP.DA.DBAccess.RunSQL("DELETE RB_Page WHERE HostName='" + this.HostName + "'");
            BP.DA.DBAccess.RunSQL("DELETE RB_PageFile WHERE HostName='" + this.HostName + "'");

        }

        protected override bool beforeUpdate()
        {
            try
            {
                if (this.NumOfPage != 0)
                {
                    decimal p = decimal.Parse(this.S.ToString()) / decimal.Parse(this.NumOfPage.ToString());
                    this.Pace = p;
                }
            }
            catch
            {

            }
            return base.beforeUpdate();
        }
        #endregion
    }
	/// <summary>
	/// ��վ
	/// </summary>
    public class WebSites : EntitiesOID
    {
        #region �õ����� Entity
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new WebSite();
            }
        }
        /// <summary>
        /// ��ȡһ��վ��
        /// </summary>
        /// <returns></returns>
        public WebSite GetOne()
        {
            string sql = "";
            return null;
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��վ
        /// </summary>
        public WebSites() { }
        #endregion

        /// <summary>
        /// ��ѯȫ��
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            // return this.Retrieve(WebSiteAttr.IsEnable, 1);
            return base.RetrieveAll();
        }
        public static void RunAllSite()
        {
            WebSites ens = new WebSites();
            ens.RetrieveAll();

            foreach (WebSite en in ens)
            {
                try
                {

                    en.DoBeforeRun();
                    if (en.IsEnable == false)
                        continue;

                    Log.DefaultLogWriteLineInfo("================== ��ʼ���У�" + en.Name + "====================");

                    DateTime dt = DateTime.Now;
                    Hrefs hfs = PubClass.GetHrefs(en.Url, en.HisEncode);
                    foreach (Href hf in hfs)
                    {
                        PubClass.DoPage(hf);
                    }

                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts = dt - dt2;
                    en.S = ts.Seconds;
                    en.UDT = dt2.ToString("MM��dd��HHʱmm��");

                    en.NumOfPage = DBAccess.RunSQLReturnValInt("select count(*) NUM from RB_Page WHERE HostName='" + en.HostName + "'");
                    en.NumOfFile = DBAccess.RunSQLReturnValInt("select count(*) NUM from RB_PageFile WHERE HostName='" + en.HostName + "'");
                    en.Log = "OK";
                    en.Update();
                }
                catch (Exception ex)
                {
                    en.Log = "ץȡ��ҳ���ִ���" + ex.Message;
                    en.Update();
                    Log.DefaultLogWriteLineInfo("���У�" + en.Name + "�ڼ�����������⣺" + ex.Message);
                }
            }
        }
    }
	
}

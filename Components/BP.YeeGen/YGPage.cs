using System;
using BP.YG;
using BP.Web;


namespace BP.YG
{
	
	/// <summary>
	/// THPage ��ժҪ˵����
	/// </summary>
	public class YGPage:System.Web.UI.Page
	{
        public string PRI
        {
            get
            {
                string s = this.Request.QueryString["PRI"];
                if (s == "")
                    return null;
                return s;
            }
        } 
        public string FK_SF
        {
            get
            {
                string s = this.Request.QueryString["FK_SF"];
                if (s == null || s == "")
                {
                    if (this.FK_City == null || this.FK_City == "")
                        return null;

                    return this.FK_City.Substring(0, 2);
                }
                return s;
            }
        }
        public string FK_Sort
        {
            get
            {
                return this.Request.QueryString["FK_Sort"];
            }
        }
        public string FK_Type
        {
            get
            {
                return this.Request.QueryString["FK_Type"];
            }
        }
        public string FK_City
        {
            get
            {
                return this.Request.QueryString["FK_City"];
            }
        }
        public string DoType
        {
            get
            {
                string s = this.Request.QueryString["DoType"];
                if (s == null)
                    return "About";
                return s;
            }
        }
        public string RefNo
        {
            get
            {
                return this.Request.QueryString["RefNo"];
            }
        }
        public int RefOID
        {
            get
            {
                try
                {
                    return int.Parse(this.Request.QueryString["RefOID"]);
                }
                catch
                {
                    try
                    {
                        return int.Parse(this.Request.QueryString["OID"]);
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
        }
        public string BureauNo
        {
            get
            {
                string s = ViewState["B"] as string;
                if (s == null)
                {
                    s = this.Request.QueryString["B"];
                    if (s == null)
                    {
                        s = Global.BureauNo;
                        ViewState["B"] = s;
                    }

                    if (s.IndexOf(",") != -1)
                    {
                        string[] strs = s.Split(',');
                        s = strs[0];
                        ViewState["B"] = s;
                    }
                }
                return s;
            }
        }

		public int PageIdx
		{
			get
			{
				try
				{
					return int.Parse(this.Request.QueryString["PageIdx"]) ; 
				}
				catch
				{
					return 1;
				}
			}
		}

		public void ToUrl(string url)
		{
			string msg1="<script language=javascript>window.location.href='"+url+"';</script>";
			this.Response.Write(msg1);
			return;
		}
		public void Alert(string msg)
		{
			string msg1="<script language=javascript>alert('"+msg+"');</script>";
			this.Response.Write(msg1);
			return;
			 
		}
		public void Close()
		{
			string msg1="<script language=javascript>window.close();</script>";
			this.Response.Write(msg1);
			return;
		}
		public void WinOpen(string url)
		{
			string msg1="<script language=javascript>window.open("+url+");</script>";
			this.Response.Write(msg1);
			return;
		}
        public void WinClose()
        {
            string msg1 = "<script language=javascript>window.close();</script>";
            this.Response.Write(msg1);
            return;
        }
        ///// <summary>
        ///// CheckCustomrSession
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public bool CheckCustomrSession(string msg, string B)
        //{
        //    if (Global.CustomerNo == null)
        //    {
        //        //string msg = "����½ʱ��̫��������û�е�½����½�ɹ���ϵͳ���Զ�ת����һҳ����ȥ��";
        //        Global.MsgOfReLogin = msg;
        //        Global.GoWhere = this.Request.RawUrl;
        //        //this.ToUrl("/Login.aspx?B=" + B);
        //        return false;
        //    }
        //    return true;
        //}
        //public bool CheckCustomrSession(string msg)
        //{
        //    if (Global.CustomerNo == null)
        //    {
        //        //string msg1 = " var url='/Port.aspx';";
        //        //msg1 += " var b=window.showModalDialog(url, 'ass' ,'dialogHeight: 450px; dialogWidth: 500px;center: yes; help: no');";
        //        //msg1 += "  window.location.reload();";
        //        //System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> " + msg1 + "</script> ");
        //        return false;
        //    }
        //    else
        //        return true;
        //    //  return this.CheckCustomrSession(msg, this.BureauNo);
        //}
        public string CheckCustomrSession()
        {
            return this.CheckCustomrSession("���ĵ�½ʱ��̫��������û�е�¼��");
        }
        public string CheckCustomrSession(string msg)
        {
            if (Global.CustomerNo != null)
            {
                return null;
            }
            return "<a>" + msg + "������Ҫ<a href='javascript:DoPort();'>���µ�½����ע��</a>��ע�Ừ�Ĳ�˰�������ºô���</a><hr><li>1�������Է������»�ȡ���֡�</li> <li>2�������Թ����ļ���ȡ���֡�</li> <li>3�����������û��ֹ����������</li> ";
        }
        public bool CheckCustomrSessionAlert()
        {
            if (Global.CustomerNo == null)
            {
                BP.PubClass.Alert("���µ�½����ע��");
                return false;
            }
            return true;
        }
		public void ToMsgPage(string msg)
		{
			try
			{
				Global.Msg = msg;
				this.ToUrl("/Msg.aspx?B="+this.BureauNo);
			}
			catch
			{
                this.ToUrl("/Msg.aspx?B=" + this.BureauNo);
			}
			return;
		}
		public void ToLoginPage_(string msg)
		{
			Global.MsgOfReLogin = msg;
			string url ="/Login.aspx?GoWhere="+this.Request.RawUrl;
			this.Response.Redirect(url,true);
		}
	}
}

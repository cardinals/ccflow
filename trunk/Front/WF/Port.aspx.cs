using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;


namespace BP.Web.Port
{
	/// <summary>
	/// Port ��ժҪ˵����
	/// </summary>
	public partial class Port : System.Web.UI.Page
	{
		#region ���봫�ݲ���
		public string DoWhat
		{
			get
			{
				return this.Request.QueryString["DoWhat"];
			}
		}
		public string UserNo
		{
			get
			{
				return this.Request.QueryString["UserNo"];
			}
		}
		public string RequestKey
		{
			get
			{
				return this.Request.QueryString["Key"];
			}
		}
		#endregion

		#region  ��ѡ��Ĳ���
		public string Taxpayer
		{
			get
			{
				if ( this.Request.QueryString["Taxpayer"]==null) 
					throw new Exception("��˰�˱��û�д��ݹ�����");

				return this.Request.QueryString["Taxpayer"];
			}
		}
		public string FK_Flow
		{
			get
			{
				return this.Request.QueryString["FK_Flow"];
			}
		}
       
        public string SID
        {
            get
            {
                return this.Request.QueryString["SID"];
            }
        }
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
            Response.AddHeader("P3P", "CP=CAO PSA OUR");

            if (this.UserNo != null && this.SID != null)
            {
                string sql = "select sid  from Port_Emp WHERE no='" + this.UserNo + "'";
                string sid = BP.DA.DBAccess.RunSQLReturnVal(sql).ToString();
                if (sid != this.SID)
                {
                    this.Response.Write("�Ƿ��ķ��ʣ��������Ա��ϵ��");
                    //this.UCSys1.AddMsgOfWarning("����", "�Ƿ��ķ��ʣ��������Ա��ϵ��");
                    return;
                }
                else
                {
                    Emp em = new Emp(this.UserNo);
                    WebUser.Token = this.Session.SessionID;
                    WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);
                }
                this.Response.Redirect("EmpWorks.aspx", true);
                return;
            }

            foreach (string str in this.Request.QueryString)
            {
                string val = this.Request.QueryString[str];
                if (val.IndexOf('@') != -1)
                    throw new Exception("��û���ܲ���: [ " + str + " ," + val + " ] ��ֵ ��URL �����ܱ�ִ�С�");
            }

			if (this.IsPostBack==false)
			{
				this.IsCanLogin();
				BP.Port.Emp emp = new BP.Port.Emp(this.UserNo) ; 
				BP.Web.WebUser.SignInOfGener(emp); //��ʼִ�е�½��
				switch( this.DoWhat )
				{
					case DoWhatList.RequestCJ: // �����������
						this.Response.Redirect("../ZF/CJ.htm",true);
						break;
					case DoWhatList.RequsetMyCT: // �����������
						this.Response.Redirect("../../CT/CT/Port/Home.htm",true);
						break;
					case DoWhatList.RequsetMyZF: // �����������
						this.Response.Redirect("../../ZF/ZF/Port/Home.htm",true);
						break;
					case DoWhatList.RequestStart: // �����������ҵ������
						this.Response.Redirect("Start.aspx",true);
						break;
					case DoWhatList.RequestMyWork: // ����������� �ҵĹ�����
						this.Response.Redirect("MyWork.aspx",true);
						break;
					case DoWhatList.RequestHome: // �����������
						this.Response.Redirect("./Port/Home.htm",true);
						break;
					case DoWhatList.RequestMyFlow: // �����������
						if (this.FK_Flow==null)
							throw new Exception("û��ָ�����̱��");
						this.Response.Redirect("MyFlow.aspx?FK_Flow="+this.FK_Flow,true);
						break;
					case DoWhatList.Login:
						this.Response.Redirect("./Port/Home.htm",true);
						break;
					case DoWhatList.FlowSearch: // ���̲�ѯ��
						this.Response.Redirect("../Comm/PanelEns.aspx?EnsName=BP.WF.CHOfFlows&FK_Flow="+this.FK_Flow,true);
						break;	
					case DoWhatList.KYDJ:
//						BP.Port.WF.DJ.ND1 kydj= new BP.Port.WF.DJ.ND1();
//						if (kydj.IsExit("FK_Taxpayer",this.Taxpayer)==true)
//						{
//							this.ShowMsg( "�����Ѿ�����." );
//							WebUser.Exit();
//							return;
//						}
//						kydj.FK_Taxpayer=this.Taxpayer;
//						kydj.Rec=WebUser.No;
//						kydj.RDT=DataType.CurrentDataTime;
//						kydj.CDT=DataType.CurrentDataTime;
//						kydj.Insert();
//
//						WorkNode wn = new WorkNode(kydj,new Node( 11001 ));
//						string msg=wn.AfterNodeSave(true,false);
//						WebUser.Exit();
//						this.ShowMsg(msg); 
						break;
					default:
						this.ToErrorPage("û��Լ���ı��:DoWhat="+this.DoWhat );
						break;
				}
			}
		}
		public void ShowMsg(string msg)
		{
			this.Response.Write(msg);
		}
		public bool IsCanLogin()
		{
			return true;

			if (this.RequestKey!=this.GetKey())
			{
				if (SystemConfig.IsDebug)
					return true;
					//throw new Exception("Կ����Ч������ִ����������key="+this.GetKey() );
				else
					throw new Exception("Կ����Ч������ִ����������" );
			}
			return true;
		}
		public string GetKey()
		{
			int a=int.Parse( DateTime.Now.ToString("hhMMdd") );
			decimal b=decimal.Parse( this.UserNo );
			
			decimal c =  a+b ;
			return c.ToString("0");
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion

		public   void ToMsgPage(string mess)
		{		
			System.Web.HttpContext.Current.Session["info"]=mess;
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Port/InfoPage.aspx",true);
			return;
		}
		/// <summary>
		/// �л�����ϢҲ�档
		/// </summary>
		/// <param name="mess"></param>
		public   void ToErrorPage(string mess)
		{		
			System.Web.HttpContext.Current.Session["info"]=mess;
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Port/InfoPage.aspx");
			return;

		}
	}
}

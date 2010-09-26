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
using BP.Web;
using BP.WF;
using BP.En;
using BP.En;

namespace BP.Web.WF
{
    /// <summary>
    /// Do ��ժҪ˵����
    /// </summary>
    public partial class Do : PageBase
    {
        public string ActionType
        {
            get
            {
                string s = this.Request.QueryString["ActionType"];
                if (s == null)
                    s = this.Request.QueryString["DoType"];
                return s;
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.Request.QueryString["FK_Emp"];
            }
        }
        public int WorkID
        {
            get
            {
                return int.Parse(this.Request.QueryString["WorkID"]);
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                string str = "";
                switch (this.ActionType)
                {
                    case "OF":
                        string sid = this.Request.QueryString["SID"];
                        string[] strs = sid.Split('_');
                        WorkerList wl = new WorkerList();
                        int i = wl.Retrieve(WorkerListAttr.FK_Emp, strs[0], WorkerListAttr.WorkID, strs[1], WorkerListAttr.FK_Node, strs[2]);
                        if (i == 0)
                            return;
                        BP.Port.Emp emp155 = new BP.Port.Emp(wl.FK_Emp);
                        Web.WebUser.SignInOfGener(emp155, true);
                        string u="MyFlow.aspx?FK_Flow=" + wl.FK_Flow + "&WorkID=" + wl.WorkID ;
                        this.Response.Write("<script> window.location.href='" + u + "'</script> *^_^*  <br><br>���ڽ���ϵͳ���Ժ������ʱ��û�з�Ӧ����<a href='" + u + "'>��������롣</a>");
                        return;
                    case "ExitAuth":
                        BP.Port.Emp emp = new BP.Port.Emp(this.FK_Emp);
                        BP.Web.WebUser.SignInOfGenerLang(emp, WebUser.SysLang);
                        this.WinClose();
                        return;
                    case "LogAs":
                        BP.Port.Emp emp1 = new BP.Port.Emp(this.FK_Emp);
                        BP.Web.WebUser.SignInOfGener(emp1, WebUser.SysLang, WebUser.No, true);
                        this.WinClose();
                        return;
                    case "TakeBack": // ȡ����Ȩ��
                        BP.WF.Port.WFEmp myau = new BP.WF.Port.WFEmp();
                        myau.No = WebUser.No;
                        myau.Update(BP.WF.Port.WFEmpAttr.AuthorIsOK, 0);
                        this.WinClose();
                        return;
                    case "AutoTo": // ִ����Ȩ��
                        BP.WF.Port.WFEmp au = new BP.WF.Port.WFEmp();
                        au.No = WebUser.No;
                        au.AuthorDate = BP.DA.DataType.CurrentData;
                        au.Author = this.FK_Emp;
                        au.AuthorIsOK = true;
                        au.Save();
                        this.WinClose();
                        return;
                    case "UnSend": // ִ�г������͡�
                        try
                        {
                            WorkFlow mwf = new WorkFlow(this.FK_Flow, this.WorkID);
                            str = mwf.UnSend(WebUser.No);
                            this.Session["info"] = str;
                            this.Response.Redirect("MyFlowInfo.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID, true);
                            return;
                        }
                        catch (Exception ex)
                        {
                            this.Session["info"] = "@ִ�г���ʧ�ܡ�@ʧ����Ϣ" + ex.Message;
                            this.Response.Redirect("MyFlowInfo.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&FK_Type=warning", true);
                            return;
                            //  this.ToMsgPage("@ִ�г���ʧ�ܡ�@ʧ����Ϣ"+ex.Message);
                        }
                        // this.Response.Redirect("MyFlow.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow, true);
                        break;
                    case "SetBillState":
                        
                        break;
                    case "WorkRpt":
                        Bill bk1 = new Bill(this.Request.QueryString["OID"]);
                        Node nd = new Node(bk1.FK_Node);
                        this.Response.Redirect("WFRpt.aspx?WorkID=" + bk1.WorkID + "&FID=" + bk1.FID + "&FK_Flow=" + nd.FK_Flow + "&NodeId=" + bk1.FK_Node, false);
                        //this.WinOpen();
                        //this.WinClose();
                        break;
                    case "PrintBill":
                        //Bill bk2 = new Bill(this.Request.QueryString["OID"]);
                        //Node nd2 = new Node(bk2.FK_Node);
                        //this.Response.Redirect("NodeRefFunc.aspx?NodeId=" + bk2.FK_Node + "&FlowNo=" + nd2.FK_Flow + "&NodeRefFuncOID=" + bk2.FK_NodeRefFunc + "&WorkFlowID=" + bk2.WorkID);
                        ////this.WinClose();
                        break;
                    //ɾ�������е�һ���ڵ�����ݣ��������칤��
                    case "DeleteFlow":
                        string fk_flow = this.Request.QueryString["FK_Flow"];
                        int workid = int.Parse(this.Request.QueryString["WorkID"]);

                        //����DoDeleteWorkFlowByReal����
                        WorkFlow wf = new WorkFlow(new Flow(fk_flow), workid);
                        wf.DoDeleteWorkFlowByReal();

                        this.ToMsgPage(this.ToE("FlowDelOK", "����ɾ���ɹ�"));
                        break;
                    default:
                        throw new Exception("ActionType error" + this.ActionType);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ToErrorPage("ִ����������쳣��<BR>" + ex.Message);
            }
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
    }
}

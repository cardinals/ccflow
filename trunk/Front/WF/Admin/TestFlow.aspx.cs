﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;


public partial class WF_Admin_TestFlow : WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string Lang
    {
        get
        {
            return this.Request.QueryString["Lang"];
        }
    }
    public void BindFlowList()
    {
        this.Title = this.ToE("TestTitle", "感谢您选择驰骋工作流程引擎-流程设计&测试界面");

        this.Pub1.Add("<h3>"+this.Title+"</h3>");

        if (this.FK_Flow == null)
        {
            BP.WF.Flows fls = new BP.WF.Flows();
            fls.RetrieveAll();

            this.Left.AddFieldSet("List");
            FlowSorts fss = new FlowSorts();
            fss.RetrieveAll();

            string FlowChart = this.ToE("FlowChart", "流程图");
            string FlowProperty = this.ToE("FlowProperty", "流程属性");

            this.Left.AddUL();
            foreach (FlowSort fs in fss)
            {
                this.Left.Add("<li><b>" + fs.Name + "</b></li>");
                foreach (BP.WF.Flow fl in fls)
                {
                    if (fs.No != fl.FK_FlowSort)
                        continue;

                    if (fl.No == this.FK_Flow)
                        this.Left.AddLi("<a href='TestFlow.aspx?FK_Flow=" + fl.No + "&Type=New&Lang=" + WebUser.SysLang + "'><b><font color=green>" + fl.Name + "</b></font></a> - <a href='./../../Data/FlowDesc/" + fl.No + ".gif' target=_blank>" + FlowChart + "</a> - <a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + fl.No + "','s')\" >" + FlowProperty + "</a>");
                    else
                        this.Left.AddLi("<a href='TestFlow.aspx?FK_Flow=" + fl.No + "&Type=New&Lang=" + WebUser.SysLang + "'>" + fl.Name + "</a> - <a  href='./../../Data/FlowDesc/" + fl.No + ".gif' target=_blank>" + FlowChart + "</a> - <a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + fl.No + "','s')\" >" + FlowProperty + "</a>");
                }
            }
            this.Left.AddULEnd();
            this.Left.AddFieldSetEnd();
        }
        else
        {
            Flow fl = new Flow(this.FK_Flow);

            this.Left.AddFieldSet( fl.Name);

            this.Left.AddH3("<a href='./../../Data/FlowDesc/" + fl.No + ".gif' target=_blank >" + this.ToE("FlowChart", "流程图") + "</a>");
            this.Left.AddH3("<a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.FlowSheet&PK=" + fl.No + "','s')\" >"+this.ToE("FlowProperty","流程属性")+"</a>");
            this.Left.AddH3("<a href='TestFlow.aspx?Lang=" + this.Lang + "'>" + this.ToE("AllFlow", "全部流程") + "....</a>");
            this.Left.AddFieldSetEnd();
        }


        this.Left.AddFieldSet(this.ToE("PortData", "组织结构管理"));
        this.Left.AddUL();
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.WF.Port.Stations')\">" + this.ToE("Station", "岗位维护") + "</a>");
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.WF.Port.Depts')\">" + this.ToE("Dept", "部门维护") + "</a>");
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Ens.aspx?EnsName=BP.WF.Port.Emps')\">" + this.ToE("Emp", "人员维护") + "</a>");
        this.Left.AddULEnd();
        this.Left.AddFieldSetEnd();


        this.Left.AddFieldSet(this.ToE("Tools", "系统工具"));
        //this.Left.AddFieldSet("系统工具");
        this.Left.AddUL();
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../WF/ClearDatabase.aspx')\">" + this.ToE("ClearDatabase","清除所有流程数据")+ "</a>");
        this.Left.AddLi("<a href=\"javascript:WinOpen('../../Comm/Sys/EditWebConfig.aspx')\">" + this.ToE("WebConfig", "系统设置") + "</a>");
        this.Left.AddULEnd();
        this.Left.AddFieldSetEnd(); 


        //this.Left.AddFieldSet("相关下载");
        //this.Left.Add("<a href=http://flow.ccflow.cn/ > http://flow.ccflow.cn/ 相关下载</a>");
        //this.Left.AddBR("<a href=/WF/Login.aspx > 直接登陆 (嵌入方式)</a>");
        //this.Left.AddBR("<a href=/WF/Port/Signin.aspx > 直接登陆 (系统方式)</a>");
        //this.Left.AddFieldSetEnd(); 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Emp emp1 = new Emp("admin");
        WebUser.SignInOfGenerLang(emp1, this.Lang);

        this.BindFlowList();

        if (this.FK_Flow == null)
        {
            this.Ucsys1.AddFieldSet("关于流程测试");

            this.Ucsys1.AddUL();
            this.Ucsys1.AddLi("现在是流程测试状态，此功能紧紧提供给流程设计人员使用。");
            this.Ucsys1.AddLi("提供此功能的目的是，快速的让各个角色人员登录，以便减少登录的繁琐麻烦。");
            this.Ucsys1.AddLi("点左边的流程列表后，系统自动显示能够发起此流程的工作人员，点一个工作人员就直接登录了。");
            this.Ucsys1.AddULEnd();

            this.Ucsys1.AddFieldSetEnd();
            return;
        }

        if (this.RefNo != null)
        {
            Emp emp = new Emp(this.RefNo);
            BP.Web.WebUser.SignInOfGenerLang(emp, this.Lang);

            this.Session["FK_Flow"] = this.FK_Flow;

            if (this.Request.QueryString["Type"] != null)
            {
                string url = "../../WAP/MyFlow.aspx?FK_Flow=" + this.FK_Flow;
                if (this.Request.QueryString["IsWap"] == "1")
                {
                    //  this.Response.Write("<script  language=javascript>  window.open( '" + url + "' , 'ass' ,'width=50,top=50,left=50,height=20,scrollbars=yes,resizable=yes,toolbar=false,location=false') </script>");
                    this.Response.Redirect("../../WAP/MyFlow.aspx?FK_Flow=" + this.FK_Flow, true);
                }
                else
                    this.Response.Redirect("../../WF/MyFlow.aspx?FK_Flow=" + this.FK_Flow, true);
            }
            else
            {
                this.Response.Redirect("../Port/Home.htm?FK_Flow=" + this.FK_Flow, true);
            }
            return;
        }

        BP.Web.WebUser.SysLang = this.Lang;


        Flow fl = new Flow(this.FK_Flow);

        this.Ucsys1.AddFieldSet(fl.Name);
        this.Ucsys1.AddUL();
        Nodes nds = new Nodes(this.FK_Flow);
        foreach (BP.WF.Node nd in nds)
        {
            this.Ucsys1.AddLi("Step " + nd.Step + " :<a href=\"javascript:WinOpen('../../Comm/UIEn.aspx?EnName=BP.WF.Ext.NodeO&PK=" + nd.NodeID + "')\">" + nd.Name + "</a>, <a href=\"javascript:WinOpen('../MapDef/MapDef.aspx?PK=ND" + nd.NodeID + "')\">" + this.ToE("DNode", "设计表单") + "</a>");
        }
        this.Ucsys1.AddULEnd();
        this.Ucsys1.AddFieldSetEnd();

        int nodeid = int.Parse(this.FK_Flow + "01");
        Emps emps = new Emps();
        emps.RetrieveInSQL_Order("select fk_emp from Port_Empstation WHERE fk_station in (select fk_station from WF_NodeStation WHERE FK_Node=" + nodeid + " )", "FK_Dept");

        if (emps.Count == 0)
        {
            this.Ucsys1.AddMsgOfWarning("Error",
                this.ToE("StartError", "错误原因 <br>@1，可能是您没有正确的设置岗位、部门、人员。<br>@2，可能是没有给开始节点设置工作岗位。。"));
            return;
        }

        this.Ucsys1.AddFieldSet(this.ToE("ChoseStarter", "可发起(<font color=red>" + fl.Name + "</font>)流程的人员"));
        //this.Ucsys1.Add( "说明:点击用户名，您就可以以他的身份登录并发起流程。");
        this.Ucsys1.AddUL();
        foreach (Emp emp in emps)
        {
            this.Ucsys1.AddLi(emp.No + "," + emp.Name + "&nbsp;&nbsp;&nbsp;&nbsp;<a href='TestFlow.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "'  >IE</a> - <a href='TestFlow.aspx?RefNo=" + emp.No + "&FK_Flow=" + this.FK_Flow + "&Lang=" + BP.Web.WebUser.SysLang + "&Type=" + this.Request.QueryString["Type"] + "&IsWap=1'  >Mobile</a>  " + emp.FK_DeptText);
        }
        this.Ucsys1.AddULEnd();
        this.Ucsys1.AddFieldSetEnd();

        // this.Ucsys1.Add("<a href='../../Data/FlowDesc/" + this.FK_Flow + ".gif' target=_blank><img border=0 src='../../Data/FlowDesc/" + this.FK_Flow + ".gif' width=300px height=300px ></a>");
    }
}

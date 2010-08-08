﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Web;

public partial class WF_UC_WFRpt : BP.Web.UC.UCBase3
{


    #region 属性
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public int StartNodeID
    {
        get
        {
            return int.Parse(this.FK_Flow + "01");
        }
    }
    public string FK_Flow
    {
        get
        {
            string flow = this.Request.QueryString["FK_Flow"];
            if (flow == null)
            {
                throw new Exception("@没有获取它的流程编号。");
                //BP.WF.CHOfFlow fl = new CHOfFlow(this.WorkID);
                //return fl.FK_Flow;
            }
            else
            {
                return flow;
            }
        }
    }
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public int NodeID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["NodeID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public int FID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Clear();
        Flow fl = new Flow(this.FK_Flow);
        switch (fl.HisFlowType)
        {
            case FlowType.Panel:
                this.BindPanel(fl);
                break;
            default:
                if (this.WorkID == this.FID)
                    this.BindRavie(fl);
                else
                    this.BindBrach(fl);
                break;
        }
    }
    public void BindPanel(Flow fl)
    {
        try
        {
            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
            this.BindWFRptV2(wf);
        }
        catch (Exception ex)
        {
            this.AddHR();
            this.AddMsgOfInfo("生成工作报告期间出现错误,造成此原因如下:", "<BR><BR>1, 此流程为虚拟流程。<BR><BR>2，流程数据已经删除。3，参与此流程上的人员编号错误。<BR><BR>技术信息:" + ex.Message);
        }
    }

    #region 分流
    /// <summary>
    /// 分流干流
    /// </summary>
    /// <param name="fl"></param>
    public void BindRavie(Flow fl)
    {
        //  WorkFlow wf = new WorkFlow(fl, this.WorkID, this.FID);

        this.AddH4("关于（" + fl.Name + "）工作报告");
        this.AddHR();

        Node ndStart = fl.HisStartNode;
        StartWork sw = ndStart.HisWork as StartWork;
        sw.OID = this.WorkID;
        sw.Retrieve();

        this.Add("流程发起人：" + sw.RecText + "，发起日期：" + sw.RDT + " ；流程状态：" + sw.WFStateT);

        Nodes nds = fl.HisNodes;
        foreach (Node nd in nds)
        {
            if (nd.HisFNType != FNType.Branch)
                continue;

            string sql = "SELECT OID,Rec,RDT FROM ND" + nd.NodeID + " WHERE FID=" + this.FID;
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                continue;

            this.AddBR("分支流程如下：");
            this.AddTable();
            this.AddTR();
            this.AddTDTitle("接受人");
            this.AddTDTitle("接受日期");
            this.AddTDTitle("工作报告");
            this.AddTREnd();

            foreach (DataRow dr in dt.Rows)
            {
                this.AddTR();
                this.AddTD(dr["Rec"].ToString());
                this.AddTD(dr["RDT"].ToString());
                this.AddTD("<a href='WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&FID=" + this.FID + "&WorkID=" + dr["OID"] + "' target=_blank >工作报告</a>");
                this.AddTREnd();
            }
            this.AddTableEnd();
            break;
        }

        ReturnWorks rws = new ReturnWorks();
        rws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        ForwardWorks fws = new ForwardWorks();
        fws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        WorkNodes wns = new WorkNodes();
        wns.GenerByFID(fl, this.FID);

        this.BindWorkNodes(wns, rws, fws);

        this.AddHR("流程报告结束");
    }
    /// <summary>
    /// 分流支流
    /// </summary>
    /// <param name="fl"></param>
    public void BindBrach(Flow fl)
    {
        //  WorkFlow wf = new WorkFlow(fl, this.WorkID, this.FID);
        WorkNodes wns = new WorkNodes();
        wns.GenerByFID(fl, this.FID);

        this.AddH4("关于（" + fl.Name + "）工作报告");
        this.AddHR();

        Node nd = fl.HisStartNode;
        StartWork sw = nd.HisWork as StartWork;
        sw.FID = this.FID;
        sw.OID = this.WorkID;
        sw.RetrieveFID();

        this.Add("流程发起人：" + sw.RecText + "，发起日期：" + sw.RDT + " ；流程状态：" + sw.WFStateT);

        ReturnWorks rws = new ReturnWorks();
        rws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        ForwardWorks fws = new ForwardWorks();
        fws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        this.BindWorkNodes(wns, rws, fws);

        this.AddHR("流程报告结束");
    }
    #endregion 分流


    #region 合流
    /// <summary>
    /// 合流干流
    /// </summary>
    /// <param name="fl"></param>
    public void BindHeLiuRavie(Flow fl)
    {

    }
    /// <summary>
    /// 合流支流
    /// </summary>
    /// <param name="fl"></param>
    public void BindHeLiuBrach(Flow fl)
    {

    }
    #endregion 合流

    public void BindFHLWork(GenerFH hf)
    {
        this.AddH4(hf.Title);
        this.AddHR();

        this.AddFieldSet("当前节点基本信息");
        this.AddBR("接受时间：" + hf.RDT);
        this.AddBR("接受人：" + hf.ToEmpsMsg);
        this.AddFieldSetEndBR();

        GenerWorkFlows gwfs = new GenerWorkFlows();
        gwfs.Retrieve(GenerWorkFlowAttr.FID, this.FID);

        this.AddFieldSet("分流人员信息");

        this.AddTable();
        this.AddTR();
        this.AddTDTitle("标题");
        this.AddTDTitle("发起人");
        this.AddTDTitle("发起日期");
        this.AddTDTitle("");
        this.AddTREnd();

        foreach (GenerWorkFlow gwf in gwfs)
        {
            if (gwf.WorkID == this.FID)
                continue;

            this.AddTR();
            this.AddTD(gwf.Title);
            this.AddTD(gwf.Rec);
            this.AddTD(gwf.RDT);
            this.AddTD("<a href='WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=" + gwf.FID + "' target=_b" + gwf.WorkID + ">工作报告</a>");
            this.AddTREnd();
        }
        this.AddTableEndWithBR();
        this.AddFieldSetEnd();
    }


    public void BindWFRptV2(WorkFlow wf)
    {
        Int64 workid = wf.WorkID;
        this.Add("<p align='center'>　</p>");
        this.Add("<p align='center' style='line-height: 100%' ><b>" + this.ToE("About", "关于") + "《" + wf.HisStartWork.Title + "》" + this.ToE("WorkRpt", "工作报告") + "</b><hr width=75% /></p>");
        this.Add("<p align='center'>　</p>");

        #region 附件信息
        // 加上附件信息 
        FileManagers fms = new FileManagers();
        fms.Retrieve(FileManagerAttr.WorkID, workid);

        if (fms.Count > 0)
        {
            this.AddTable();
            this.AddCaption(this.ToE("FJ", "附件"));
            this.AddTR();
            this.AddTDTitle("ID");
            this.AddTDTitle(this.ToE("Node", "节点"));
            this.AddTDTitle(this.ToE("Date", "日期"));
            this.AddTDTitle(this.ToE("Worker", "工作者"));
            this.AddTDTitle(this.ToE("Name", "名称"));
            this.AddTDTitle(this.ToE("Size", "大小"));
            this.AddTREnd();
            int ii = 1;
            foreach (FileManager fm in fms)
            {
                this.AddTR();
                this.AddTDIdx(ii++);
                this.AddTD(fm.FK_NodeText);
                this.AddTD(fm.RDT);
                this.AddTD(fm.FK_EmpText);
                this.AddTD("<a href='/FlowFile/" + BP.Web.WebUser.FK_Dept + "/" + fm.OID + "." + fm.Ext + "' target=_bl ><img src='../Images/FileType/" + fm.Ext + ".gif' border=0/>" + fm.Name + "</a>");
                this.AddTD(fm.FileSize);
                this.AddTREnd();
            }
            this.AddTableEnd();
        }
        #endregion

        //判断当前人员是否管理员。
        if (WebUser.No == "admin" && wf.IsComplete == false)
        {
            this.Add("<BR><a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + workid + "&FK_Flow=" + wf.HisFlow.No + "' /><img src='../Images/Btn/Delete.gif' border=0/>强制删除流程:(请慎重操作，系统会记录在操作日志中)</a><BR>");
        }

        WorkNodes wns = wf.HisWorkNodesOfWorkID;

        this.Add("<p align='left' style='line-height: 100%' > &nbsp;&nbsp;&nbsp; " + wf.HisStartWork.Title + " " + this.ToE("Work", "工作") + ", " + this.ToE("From", "从") + " " + wf.HisStartWork.HisRec.Name + "  " + wf.HisStartWork.RDT + " " + wns.Count + " " + this.ToE("WorkStep", "工作步骤"));

        //this.AddHtml(wf.IsCompleteStr + "结束，<a href=\"javascript:WinOpen('Option.aspx?WorkID=" + workid + "&FK_Flow=" + wf.HisFlow.No + "');\" >流程操作</a>。现在历经如下步骤,详细报告如下:</p>");
        this.Add(wf.IsCompleteStr + this.ToE("WFRpt1", "结束，现在历经如下步骤,详细报告如下:") + "</p>");
        int i = 0;
        foreach (WorkNode wn in wns)
        {
            if (wn.HisWork.NodeState == 0)
            {
                this.Add("<BR><div align=left >&nbsp;&nbsp;&nbsp;&nbsp;" + this.ToE("CrrStopNode", "当前停留工作节点") + "：" + wn.HisNode.Name + " " + this.ToE("Worker", "工作人员") + " " + wn.HisWork.Emps + "。</div>");
                i++;
            }
        }

        if (i > 1)
        {
            this.Clear();
            this.AddMsgOfWarning(this.ToE("Warning", "预警"), "流程出现错误，实际当前工作是[" + wf.HisGenerWorkFlow.FK_NodeText + "]，请关闭当前窗口体检此流程。");
            return;
        }

        ReturnWorks rws = new ReturnWorks();
        rws.Retrieve(ReturnWorkAttr.WorkID, wf.WorkID);

        ForwardWorks fws = new ForwardWorks();
        fws.Retrieve(ReturnWorkAttr.WorkID, wf.WorkID);

        this.BindWorkNodes(wns, rws, fws);

        //			this.AddHtml("</TD></TR><TR><TD>");
        //			 
        this.Add("<p align='left'> &nbsp;&nbsp;&nbsp;&nbsp;<hr> </p>");
        //  this.Add("<p align='center'> 流程系统 </p>");
        this.Add("<p align='center'> " + BP.DA.DataType.CurrentDataTime + " </p>");
        this.Add("<p align='center'>　</p>");
        this.Add("</TABLE>");
        return;
    }
    public void BindWorkNodes(WorkNodes wns, ReturnWorks rws, ForwardWorks fws)
    {
        int idx = 0;
        Int64 workid = 0;
        foreach (WorkNode wn in wns)
        {
            idx++;
            workid = wn.HisWork.OID;
            if (wn.HisWork.NodeState == 0)
            {
                WorkerLists wls = new WorkerLists(workid, wn.HisNode.NodeID);
                this.Add("<p align='left' style='line-height: 100%' >&nbsp;&nbsp;&nbsp;&nbsp;<a name='ND" + wn.HisNode.NodeID + "' >" + this.ToEP1("NStep", "@第{0}步", idx.ToString()) + "</a>" + wn.HisNode.Name + "，" + this.ToE("NodeState", "节点状态") + "：" + wn.HisWork.NodeStateText + "。");
                string msg = this.ToE("WFRpt0", "当前操作人员:");
                foreach (WorkerList wl in wls)
                {
                    if (wl.IsEnable)
                        msg += wl.FK_Emp + wl.FK_EmpText + "、";
                    else
                        msg += "<strike>" + wl.FK_Emp + wl.FK_EmpText + "</strike>、";
                }
                this.Add(msg);
            }
            else
            {
                this.Add("<p align='left' style='line-height: 100%' >&nbsp;&nbsp;&nbsp;&nbsp;<a name='ND" + wn.HisNode.NodeID + "' >" + this.ToEP1("NStep", "@第{0}步", idx.ToString()) + "</a>" + wn.HisNode.Name + "，" + this.ToE("DealEmp", "处理人") + "：" + wn.HisWork.Rec + wn.HisWork.HisRec.Name + "，" + this.ToE("NodeState", "节点状态") + "：" + wn.HisWork.NodeStateText + "。");
            }

            BookTemplates reffunc = wn.HisNode.HisBookTemplates;
            if (reffunc.Count > 0)
            {
                this.Add("&nbsp;" + this.ToE("Book", "文书") + "：");
                bool isMyBook = false;
                if (wn.HisWork.Rec == WebUser.No)
                    isMyBook = true;

                string year = DateTime.Now.Year.ToString();
                foreach (BookTemplate func in reffunc)
                {
                    if (wn.HisWork.NodeState == 0)
                    {
                        this.Add("<img src='../Images/Btn/Word.gif' /> " + func.Name);
                    }
                    else
                    {
                        string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                        string[] paths = file.Split('_');
                        string path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";
                        string bookInfo = "<img src='./../Images/Btn/Word.gif' /><a href='./../FlowFile/Book/" + path + file + "' target=_blank >" + func.Name + "</a>";
                        this.Add(bookInfo);

                        //  string  = BP.SystemConfig.GetConfig("FtpPath") + file;
                        // path = BP.SystemConfig.AppSettings["FtpPath"].ToString() + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                        // this.Add("<img src='../Images/Btn/Word.gif' /><a href=\"javascript:Run('C:\\\\ds2002\\\\OpenBook.EXE','" + file + "','0');\"  >" + func.Name + "</a>");
                    }
                }

                this.Add("</p>");
            }

            if (Glo.IsQL)
                this.Add("<a href=\"javascript:WinOpen('DoQL.aspx?WorkID=" + wn.HisWork.OID + "&FK_Node=" + wn.HisNode.NodeID + "','sds' )\" ><img src='../Images/Btn/Do.gif' border=0 />质量考核</a>");

            this.Add("<div align=center>");
            this.ADDWork(wn.HisWork, rws, fws, wn.HisNode.NodeID);
            this.Add("</div>");
        }
    }

    protected void AddContral(string desc, string text)
    {
        this.Add("<td  class='FDesc' nowrap> " + desc + "</td>");
        this.Add("<td width='40%' class=TD>");
        if (text == "")
            text = "&nbsp;";
        this.Add(text);
        this.AddTDEnd();
    }
    /// <summary>
    /// 增加一个工作
    /// </summary>
    /// <param name="en"></param>
    /// <param name="rws"></param>
    /// <param name="fws"></param>
    /// <param name="nodeId"></param>
    public void ADDWork(Work en, ReturnWorks rws, ForwardWorks fws, int nodeId)
    {
        this.BindViewEn(en, "width=90%");

        foreach (ReturnWork rw in rws)
        {
            if (rw.NodeId != nodeId)
                continue;

            this.AddBR();
            this.AddMsgOfInfo("退回信息：", rw.NoteHtml);
        }

        foreach (ForwardWork fw in fws)
        {
            if (fw.NodeId != nodeId)
                continue;

            this.AddBR();
            this.AddMsgOfInfo("转发信息：", fw.NoteHtml);
        }


        string refstrs = "";
        if (en.IsEmpty)
        {
            refstrs += "";
            return;
        }

        string keys = "&PK=" + en.PKVal.ToString();
        foreach (Attr attr in en.EnMap.Attrs)
        {
            if (attr.MyFieldType == FieldType.Enum ||
                attr.MyFieldType == FieldType.FK ||
                attr.MyFieldType == FieldType.PK ||
                attr.MyFieldType == FieldType.PKEnum ||
                attr.MyFieldType == FieldType.PKFK)
                keys += "&" + attr.Key + "=" + en.GetValStrByKey(attr.Key);
        }
        Entities hisens = en.GetNewEntities;

        #region 加入他的明细

        EnDtls enDtls = en.EnMap.Dtls;
        if (enDtls.Count > 0)
        {
            foreach (EnDtl enDtl in enDtls)
            {
                string url = "WFRptDtl.aspx?RefPK=" + en.PKVal.ToString() + "&EnName=" + enDtl.Ens.GetNewEntity.ToString();
                int i = 0;
                try
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM " + enDtl.Ens.GetNewEntity.EnMap.PhysicsTable + " WHERE " + enDtl.RefKey + "='" + en.PKVal + "'");
                }
                catch
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM " + enDtl.Ens.GetNewEntity.EnMap.PhysicsTable + " WHERE " + enDtl.RefKey + "=" + en.PKVal );
                }

                if (i == 0)
                    refstrs += "[<a href=\"javascript:WinOpen('" + url + "','u8');\" >" + enDtl.Desc + "</a>]";
                else
                    refstrs += "[<a  href=\"javascript:WinOpen('" + url + "','u8');\" >" + enDtl.Desc + "-" + i + "</a>]";
            }
        }
        #endregion

        #region 加入一对多的实体编辑
        AttrsOfOneVSM oneVsM = en.EnMap.AttrsOfOneVSM;
        if (oneVsM.Count > 0)
        {
            foreach (AttrOfOneVSM vsM in oneVsM)
            {
                string url = "UIEn1ToM.aspx?EnsName=" + en.ToString() + "&AttrKey=" + vsM.EnsOfMM.ToString() + keys;
                string sql = "SELECT COUNT(*) FROM " + vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable + " WHERE " + vsM.AttrOfOneInMM + "='" + en.PKVal + "'";
                int i = DBAccess.RunSQLReturnValInt(sql);

                if (i == 0)
                    refstrs += "[<a href='" + url + "' target='_blank' >" + vsM.Desc + "</a>]";
                else
                    refstrs += "[<a href='" + url + "' target='_blank' >" + vsM.Desc + "-" + i + "</a>]";
            }
        }
        #endregion

        #region 加入他门的相关功能
        //			SysUIEnsRefFuncs reffuncs = en.GetNewEntities.HisSysUIEnsRefFuncs ;
        //			if ( reffuncs.Count > 0  )
        //			{
        //				foreach(SysUIEnsRefFunc en1 in reffuncs)
        //				{
        //					string url="RefFuncLink.aspx?RefFuncOID="+en1.OID.ToString()+"&MainEnsName="+hisens.ToString()+keys;
        //					//int i=DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM "+vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable+" WHERE "+vsM.AttrOfMInMM+"='"+en.PKVal+"'");
        //					refstrs+="[<a href='"+url+"' target='_blank' >"+en1.Name+"</a>]";
        //					//refstrs+="编辑: <a href=\"javascript:window.open(RefFuncLink.aspx?RefFuncOID="+en1.OID.ToString()+"&MainEnsName="+ens.ToString()+"'> )\" > "+en1.Name+"</a>";
        //					//var newWindow= window.open( this.Request.ApplicationPath+'/Comm/'+'RefFuncLink.aspx?RefFuncOID='+OID+'&MainEnsName='+ CurrEnsName +CurrKeys,'chosecol', 'width=100,top=400,left=400,height=50,scrollbars=yes,resizable=yes,toolbar=false,location=false' );
        //					//refstrs+="编辑: <a href=\"javascript:EnsRefFunc('"+en1.OID.ToString()+"')\" > "+en1.Name+"</a>";
        //					//refstrs+="编辑:"+en1.Name+"javascript: EnsRefFunc('"+en1.OID.ToString()+"',)";
        //					//this.AddItem(en1.Name,"EnsRefFunc('"+en1.OID.ToString()+"')",en1.Icon);
        //				}
        //			}
        #endregion

        // 不知道为什么去掉。
        this.Add(refstrs);
    }

}

﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using BP.Web.Controls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Sys;
using BP.En;
using BP.Web;
using BP.Web.UC;
using BP.DA;

public partial class Comm_MapDef_EditF : BP.Web.PageBase
{
    /// <summary>
    /// 执行类型
    /// </summary>
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public int FType
    {
        get
        {
            return int.Parse(this.Request.QueryString["FType"]);
        }
    }
    public string IDX
    {
        get
        {
            return this.Request.QueryString["IDX"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.MyPK == null)
            throw new Exception("Mypk==null");

        this.Title = this.ToE("GuideNewField", "增加新字段向导");
        switch (this.DoType)
        {
            case "Add":
                this.Pub1.AddFieldSet(this.GetCaption);
                this.Add();
                this.Pub1.AddFieldSetEnd();
                break;
            case "Edit":
                this.Pub1.AddFieldSet(this.GetCaption);
                MapAttr attr = new MapAttr(this.RefOID);
                attr.MyDataType = this.FType;
                switch (attr.MyDataType)
                {
                    case BP.DA.DataType.AppString:
                        this.EditString(attr);
                        break;
                    case BP.DA.DataType.AppDateTime:
                    case BP.DA.DataType.AppDate:
                    case BP.DA.DataType.AppInt:
                    case BP.DA.DataType.AppFloat:
                    case BP.DA.DataType.AppMoney:
                        this.EditInt(attr);
                        break;
                    case BP.DA.DataType.AppBoolean:
                        this.EditBool(attr);
                        break;
                    default:
                        throw new Exception("为考虑的类型" + this.FType);
                        break;
                }
                this.Pub1.AddFieldSetEnd();
                break;
            default:
                break;
        }
    }
    public void Add()
    {

        MapAttr attr = new MapAttr();
        attr.MyDataType = this.FType;
        attr.FK_MapData = this.MyPK;
        attr.UIIsEnable = true;

        switch (this.FType)
        {
            case DataType.AppString:
                this.EditString(attr);
                break;
            case DataType.AppInt:
            case DataType.AppDateTime:
            case DataType.AppDate:
            case DataType.AppFloat:
            case DataType.AppMoney:
                this.EditInt(attr);
                break;
            case DataType.AppBoolean:
                this.EditBool(attr);
                break;
            default:
                break;
        }


    }
    public void EditBeforeAdd(MapAttr mapAttr)
    {

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("&nbsp;");
        this.Pub1.AddTDTitle("&nbsp;");
        this.Pub1.AddTDTitle("&nbsp;");
        this.Pub1.AddTREnd();


        if (mapAttr.IsTableAttr)
        {
            /* if here is table attr, It's will let use can change data type. */
            this.Pub1.AddTR();
            this.Pub1.AddTD("改变数据类型");
            DDL ddlType = new DDL();
            ddlType.ID = "DDL_DTType";
            BP.Sys.XML.SysDataTypes xmls = new BP.Sys.XML.SysDataTypes();
            xmls.RetrieveAll();
            ddlType.Bind(xmls, "No", "Name");
            ddlType.SetSelectItem(mapAttr.MyDataTypeS);

            ddlType.AutoPostBack = true;
            ddlType.SelectedIndexChanged += new EventHandler(ddlType_SelectedIndexChanged);

            this.Pub1.AddTD(ddlType);
            this.Pub1.AddTD("");
            this.Pub1.AddTREnd();
        }
        


        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("FEName", "字段英文名称"));
        TB tb = new TB();
        tb.ID = "TB_KeyOfEn";
        tb.Text = mapAttr.KeyOfEn;
        if (this.RefOID > 0)
            tb.Enabled = false;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD(this.ToE("FENameD", "字母或者字母数字组合"));
        this.Pub1.AddTREnd();
    

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("FLabel", "字段中文名称"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = mapAttr.Name;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("DefaultVal", "默认值"));
        tb = new TB();
        tb.ID = "TB_DefVal";
        tb.Text = mapAttr.DefValReal;

        switch (this.FType)
        {
            case BP.DA.DataType.AppDouble:
            case BP.DA.DataType.AppInt:
            case BP.DA.DataType.AppFloat:
                this.Pub1.AddTDNum(tb);
                tb.ShowType = TBType.Num;
                tb.Text = mapAttr.DefVal;
                if (tb.Text == "")
                    tb.Text = "0";
                break;
            case BP.DA.DataType.AppMoney:
            case BP.DA.DataType.AppRate:
                this.Pub1.AddTDNum(tb);
                tb.ShowType = TBType.Moneny;
                break;
            default:
                this.Pub1.AddTD(tb);
                break;
        }

        tb.ShowType = mapAttr.HisTBType;
        switch (this.FType)
        {
            case DataType.AppDateTime:
            case DataType.AppDate:
                CheckBox cb = new CheckBox();
                cb.Text = this.ToE("DefSysData", "默认系统当前日期");
                cb.ID = "CB_DefVal";
                if (mapAttr.Tag == "1")
                    cb.Checked = true;
                else
                    cb.Checked = false;
                this.Pub1.AddTD(cb);
                break;
            default:
                this.Pub1.AddTD("&nbsp;");
                break;
        }
        this.Pub1.AddTREnd();

        RadioButton rb = new RadioButton();
        if (MapData.IsEditDtlModel == false)
        {
            //this.Pub1.AddTR();
            //this.Pub1.AddTD("界面上是否可见");
            //this.Pub1.Add("<TD>");
            //rb = new RadioButton();
            //rb.ID = "RB_UIVisible_0";
            //rb.Text = "不 可 见";
            //rb.GroupName = "s1";
            //if (mapAttr.UIVisible)
            //    rb.Checked = false;
            //else
            //    rb.Checked = true;
            //this.Pub1.Add(rb);

            //rb = new RadioButton();
            //rb.ID = "RB_UIVisible_1";
            //rb.Text = "可见 ";
            //rb.GroupName = "s1";

            //if (mapAttr.UIVisible)
            //    rb.Checked = true;
            //else
            //    rb.Checked = false;
            //this.Pub1.Add(rb);
            //this.Pub1.Add("</TD>");
            //this.Pub1.AddTD("控制是否显示在页面上");
            //this.Pub1.AddTREnd();
        }

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("IsCanEdit", "是否可编辑"));
        this.Pub1.Add("<TD>");

        rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_0";
        rb.Text = this.ToE("UnCanEdit", "不可编辑");
        rb.GroupName = "s";
        rb.Checked = !mapAttr.UIIsEnable;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_1";
        rb.Text = this.ToE("CanEdit", "可编辑");  
        rb.GroupName = "s";
        rb.Checked = mapAttr.UIIsEnable;

        this.Pub1.Add(rb);
        this.Pub1.Add("</TD>");
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();


        #region 是否可单独行显示
        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("CtlShowWay", "呈现方式")); //呈现方式;
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_0";
        rb.Text = this.ToE("Row2", "两列显示"); // 两行
        rb.GroupName = "sa";
        if (mapAttr.UIIsLine)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_1";
        rb.Text = this.ToE("Row1", "整行显示"); // "一行";
        rb.GroupName = "sa";

        if (mapAttr.UIIsLine)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTD("控制该它在表单的显示方式");
        this.Pub1.AddTREnd();
        #endregion 是否可编辑


        #region 是否可界面可见
        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("IsView", "是否界面可见")); //是否界面可见
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIVisible_0";
        rb.Text = this.ToE("IsView0", "不可见"); // 界面不可见
        rb.GroupName = "sa3";
        if (mapAttr.UIVisible)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        if (mapAttr.IsTableAttr)
            rb.Enabled = false;

        rb = new RadioButton();
        rb.ID = "RB_UIVisible_1";
        rb.Text = this.ToE("IsView1", "界面可见"); // 界面可见;
        rb.GroupName = "sa3";

        if (mapAttr.UIVisible)
            rb.Checked = true;
        else
            rb.Checked = false;

        if (mapAttr.IsTableAttr)
            rb.Enabled = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTD("控制该它在表单的界面里是否可见");
        this.Pub1.AddTREnd();
        #endregion 是否可界面可见

    }

    void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MapAttr attr = new MapAttr(this.RefOID);
        attr.MyDataTypeS = this.Pub1.GetDDLByID("DDL_DTType").SelectedItemStringVal;
        attr.Update();


        this.Response.Redirect("EditF.aspx?DoType=" + this.DoType + "&MyPK=" + this.MyPK + "&RefOID=" + this.RefOID + "&FType=" + attr.MyDataType, true);

        // this.Response.Redirect(this.Request.RawUrl, true);
    }
    public void EditBeforeEnd(MapAttr mapAttr)
    {
        this.Pub1.AddTRSum();
        this.Pub1.Add("<TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " " + this.ToE("Save","保存") + " ";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndClose";
        btn.Text = this.ToE("SaveAndClose", "保存并关闭"); // "保存并关闭";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndNew";
        btn.Text = this.ToE("SaveAndNew", "保存新建"); // "保存新建";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        if (this.RefOID > 0)
        {
            btn = new Button();
            btn.ID = "Btn_AutoFull";
            btn.Text = this.ToE("AutoFull", "自动填写");
          //  btn.Click += new EventHandler(btn_Save_Click);
            btn.Attributes["onclick"] = "javascript:WinOpen('AutoFill.aspx?RefOID="+this.RefOID+"',''); return false;";
            this.Pub1.Add(btn);

            if (mapAttr.HisEditType == EditType.Edit)
            {
                btn = new Button();
                btn.ID = "Btn_Del";
                btn.Text = this.ToE("Del", "删除");
                btn.Click += new EventHandler(btn_Save_Click);
                btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
                this.Pub1.Add(btn);
            }
        }

        string url = "Do.aspx?DoType=AddF&MyPK=" + mapAttr.FK_MapData + "&IDX=" + mapAttr.IDX;
        this.Pub1.Add("<a href='" + url + "'><img src='../../Images/Btn/New.gif' border=0>" + this.ToE("New", "新建") + "</a></TD>");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapAttr"></param>
    public void EditString(MapAttr mapAttr)
    {
        this.EditBeforeAdd(mapAttr);
        TB tb = new TB();
        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("MinLen", "最小长度"));
        tb = new TB();
        tb.ID = "TB_MinLen";
        tb.CssClass = "TBNum";
        tb.Text = mapAttr.MinLen.ToString();
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("MaxLen", "最大长度"));
        tb = new TB();
        tb.ID = "TB_MaxLen";
        tb.CssClass = "TBNum";
        tb.Text = mapAttr.MaxLen.ToString();

        //DDL cb = new DDL();
        //cb.ID = "DDL_TBType";
        //cb.Items.Add(new ListItem("单行文本框", "0"));
        //cb.Items.Add(new ListItem("多行文本框", "1"));
        //cb.Items.Add(new ListItem("Sina编辑框", "2"));
        //cb.Items.Add(new ListItem("FCKEditer编辑框", "3"));

        this.Pub1.AddTD(tb);
        CheckBox cb = new CheckBox();
        cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
        cb.ID = "CB_IsM";
        cb.Text = this.ToE("IsBigDoc", "是否大块文本");
        cb.AutoPostBack = true;
        if (mapAttr.MaxLen >= 3000)
        {
            cb.Checked = true;
            tb.Enabled = false;
        }
        else
        {
            cb.Checked = false;
            tb.Enabled = true;
        }

        this.Pub1.AddTD(cb);
        if (mapAttr.IsTableAttr)
            cb.Enabled = false;

        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("TBWidth", "文本框宽度"));
        tb = new TB();
        tb.ID = "TB_UIWidth";
        tb.CssClass = "TBNum";
        tb.Text = mapAttr.UIWidth.ToString();

        this.Pub1.AddTD(tb);
        this.Pub1.AddTDB(this.ToE("ForDtl", "对明细表有效"));
        this.Pub1.AddTREnd();

        this.EditBeforeEnd(mapAttr);
    }

    void cb_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = this.Pub1.GetCBByID("CB_IsM");
        if (cb.Checked)
        {
            this.Pub1.GetTBByID("TB_MaxLen").Enabled = false;
            this.Pub1.GetTBByID("TB_MaxLen").Text = "4000";
        }
        else
        {
            this.Pub1.GetTBByID("TB_MaxLen").Enabled = true;
            this.Pub1.GetTBByID("TB_MaxLen").Text = "50";
        }
    }
    public void EditInt(MapAttr mapAttr)
    {
        this.EditBeforeAdd(mapAttr);
        this.EditBeforeEnd(mapAttr);
    }
    public void EditBool(MapAttr mapAttr)
    {
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(""); // 项目
        this.Pub1.AddTDTitle("");   // 值
        this.Pub1.AddTDTitle(""); // 描述
        this.Pub1.AddTREnd();

        if (mapAttr.IsTableAttr)
        {
            /* if here is table attr, It's will let use can change data type. */
            this.Pub1.AddTR();
            this.Pub1.AddTD("改变数据类型");
            DDL ddlType = new DDL();
            ddlType.ID = "DDL_DTType";
            BP.Sys.XML.SysDataTypes xmls = new BP.Sys.XML.SysDataTypes();
            xmls.RetrieveAll();
            ddlType.Bind(xmls, "No", "Name");
            ddlType.SetSelectItem(mapAttr.MyDataTypeS);

            ddlType.AutoPostBack = true;
            ddlType.SelectedIndexChanged += new EventHandler(ddlType_SelectedIndexChanged);

            this.Pub1.AddTD(ddlType);
            this.Pub1.AddTD("");
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("FEName", "字段英文名称"));
        TB tb = new TB();
        tb.ID = "TB_KeyOfEn";
        tb.Text = mapAttr.KeyOfEn;

        if (this.RefOID > 0)
            tb.Enabled = false;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD(this.ToE("FENameD", "字母或者字母数字组合"));
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("DefaultVal", "默认值"));
        CheckBox cb = new CheckBox();
        cb.ID = "CB_DefVal";
        cb.Text = this.ToE("PChose", "请选择");
        cb.Checked = mapAttr.DefValOfBool;

        this.Pub1.AddTD(cb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("FLabel", "字段中文名称"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = mapAttr.Name;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("IsEdit", "是否可编辑"));
        this.Pub1.Add("<TD>");

        RadioButton rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_0";
        rb.Text = this.ToE("UnCanEdit", "不可编辑");
        rb.GroupName = "s";         
        rb.Checked = !mapAttr.UIIsEnable;
        this.Pub1.Add(rb);


        rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_1";
        rb.Text = this.ToE("CanEdit", "可编辑");
        rb.GroupName = "s";
        rb.Checked = mapAttr.UIIsEnable;
        this.Pub1.Add(rb);

        this.Pub1.Add("</TD>");
        this.Pub1.AddTD();
        // this.Pub1.AddTD(this.ToE("IsReadonly", "是否只读"));

        this.Pub1.AddTREnd();

        #region 是否可单独行显示
        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("CtlShowWay", "呈现方式")); //呈现方式;
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_0";
        rb.Text = this.ToE("Row2", "两列显示"); // 两行
        rb.GroupName = "sa";
        if (mapAttr.UIIsLine)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_1";
        rb.Text = this.ToE("Row1", "整行显示"); // "一行";
        rb.GroupName = "sa";

        if (mapAttr.UIIsLine)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTD("控制该它在表单的显示方式");
        this.Pub1.AddTREnd();
        #endregion 是否可编辑


        #region 是否可界面可见
        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("IsView", "是否界面可见")); //是否界面可见
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIVisible_0";
        rb.Text = this.ToE("IsView0", "不可见"); // 界面不可见
        rb.GroupName = "sa3";
        if (mapAttr.UIVisible)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIVisible_1";
        rb.Text = this.ToE("IsView1", "界面可见"); // 界面可见;
        rb.GroupName = "sa3";

        if (mapAttr.UIVisible)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTD("控制该它在表单的界面里是否可见");
        this.Pub1.AddTREnd();
        #endregion 是否可界面可见


        this.EditBeforeEnd(mapAttr);
    }
    public void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            switch (btn.ID)
            {
                case "Btn_Del":
                    this.Response.Redirect("Do.aspx?DoType=Del&MyPK=" + this.MyPK + "&RefOID=" + this.RefOID, true);
                    return;
                case "Btn_SaveAndNew":
                    this.Response.Redirect("Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&IDX=" + this.IDX, true);
                    return;
                default:
                    break;
            }

            MapAttr attr = new MapAttr();
            if (this.RefOID > 1)
            {
                attr.OID = this.RefOID;
                attr.Retrieve();
                attr = (MapAttr)this.Pub1.Copy(attr);
                switch (this.FType)
                {
                    case DataType.AppBoolean:
                        attr.MyDataType = BP.DA.DataType.AppBoolean;
                        attr.DefValOfBool = this.Pub1.GetCBByID("CB_DefVal").Checked;
                        break;
                    case DataType.AppDateTime:
                    case DataType.AppDate:
                        if (this.Pub1.GetCBByID("CB_DefVal").Checked)
                            attr.Tag = "1";
                        else
                            attr.Tag = "";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                attr = (MapAttr)this.Pub1.Copy(attr);
                MapAttrs attrS = new MapAttrs(this.MyPK);
                int idx = 0;
                foreach (MapAttr en in attrS)
                {
                    idx++;
                    en.IDX = idx;
                    en.Update();
                    if (en.KeyOfEn == attr.KeyOfEn)
                        throw new Exception(this.ToE("FExits", "字段已经存在") + " Key=" + attr.KeyOfEn);
                }
                if (this.IDX == null || this.IDX == "")
                    attr.IDX = 0;
                else
                    attr.IDX = int.Parse(this.IDX) - 1;

                attr.MyDataType = this.FType;
                switch (this.FType)
                {
                    case DataType.AppBoolean:
                        attr.MyDataType = BP.DA.DataType.AppBoolean;
                        attr.DefValOfBool = this.Pub1.GetCBByID("CB_DefVal").Checked;
                        break;
                    default:
                        break;
                }
            }

            attr.FK_MapData = this.MyPK;
            attr.Save();
            switch (btn.ID)
            {
                case "Btn_SaveAndClose":
                    this.WinClose();
                    return;
                default:
                    break;
            }
            this.Response.Redirect("EditF.aspx?DoType=Edit&MyPK=" + this.MyPK + "&RefOID=" + attr.OID + "&FType=" + this.FType, true);
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    public string GetCaption
    {

        get
        {
            if (this.DoType == "Add")
                return this.ToE("GuideNewField", "增加新字段向导") + " - <a href='Do.aspx?DoType=ChoseFType'>" + this.ToE("ChoseType", "返回类型选择") + "</a> - " + this.ToE("Edit", "编辑");
            else
                return "<a href='Do.aspx?DoType=ChoseFType&MyPK=" + this.MyPK + "&RefOID=" + this.RefOID + "'>" + this.ToE("ChoseType", "返回类型选择") + "</a> - " + this.ToE("Edit", "编辑");
        }
    }
}

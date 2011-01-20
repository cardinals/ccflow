﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.DA;

namespace BP.YG
{
    public partial class RegUser : YGPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
            string m = "<b><font color=red>*</font></b>";
            this.Pub1.AddTableDef();
            this.Pub1.AddTR();
            this.Pub1.AddTDTitleDef("colspan=2", "如果您已经caishui800.cn用户，请点这里<U><a href='/Login.aspx?B=" + this.BureauNo + "' style='color:blue' >登陆系统</a></U>，<U><a href='/Home.aspx?B=" + this.BureauNo + "' style='color:blue' >返回主页</a></U>。");
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.Add("<TD valign=top align=right><BR>用户名" + m + "：</TD>");
            TextBox tb = new TextBox();
            tb.ID = "TB_No";
            tb.MaxLength = 14;
            this.Pub1.Add("<TD class=Note >");
            this.Pub1.Add(tb);
            this.Pub1.Add("<BR>他是您登陆本系统的帐号，也是您blog空间的帐号。<BR>不超过7个汉字或14个字节(数字，字母和下划线)</TD>");
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.Add("<TD valign=top align=right>密码" + m + "：</TD>");
            tb = new TextBox();
            tb.ID = "TB_Pass1";
            tb.MaxLength = 15;
            tb.TextMode = TextBoxMode.Password;
            this.Pub1.Add("<TD class=Note >");
            this.Pub1.Add(tb);
            this.Pub1.Add("<BR>请注意您的密码安全，2-15位。</TD>");
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.Add("<TD align=right >重输密码" + m + "：</TD>");
            tb = new TextBox();
            tb.ID = "TB_Pass2";
            tb.MaxLength = 15;
            tb.TextMode = TextBoxMode.Password;

            this.Pub1.Add("<TD>");
            this.Pub1.Add(tb);
            this.Pub1.Add("</TD>");
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.Add("<TD valign=top align=right>邮件：</TD>");
            tb = new TextBox();
            tb.ID = "TB_Mail";
            tb.MaxLength = 100;
            tb.Width = 300;
            this.Pub1.Add("<TD class=Note>");
            this.Pub1.Add(tb);
            this.Pub1.Add("<BR>请真实填写，在您丢失密码时可以用它来找回，也可以接受的通知与定阅网送税法。</TD>");
            this.Pub1.AddTREnd();


            this.Pub1.AddTR();
            this.Pub1.Add("<TD valign=top align=right>称谓：</TD>");
            this.Pub1.Add("<TD>");
            RadioButton rb = new RadioButton();
            rb.Text = "先生";
            rb.ID = "RB_1";
            rb.Checked = false;
            rb.GroupName = "s";
            this.Pub1.Add(rb);
            rb = new RadioButton();
            rb.Text = "女士";
            rb.ID = "RB_2";
            rb.Checked = true;
            rb.GroupName = "s";
            this.Pub1.Add(rb);
            this.Pub1.Add("</TD>");
            this.Pub1.AddTREnd();


            this.Pub1.AddTR();
            this.Pub1.Add("<TD></TD><TD>");
            Button btn = new Button();
            btn.Text = "接受协议并创建用户";
            btn.ID = "Btn_Submit";
            btn.CssClass = "Btn";
            btn.Click += new EventHandler(btn_Click);

            this.Pub1.Add(btn);
            this.Pub1.Add("</TD>");
            this.Pub1.AddTREnd();


            this.Pub1.AddTR();
            this.Pub1.Add("<TD></TD>");
            this.Pub1.Add("<TD>");

            Label lab = new Label();
            lab.ID = "Lab_Msg";
            lab.ForeColor = Color.Red;
            lab.Font.Bold = true;

            this.Pub1.Add(lab);
            this.Pub1.Add("</TD>");
            this.Pub1.Add("<TD></TD>");
            this.Pub1.AddTREnd();



            this.Pub1.AddTR();
            this.Pub1.Add("<TD valign=top align=right>许可协议：</TD>");
            tb = new TextBox();
            tb.ID = "TB_Leic";
            tb.MaxLength = 100;
            tb.Width = 500;
            tb.Rows = 10;
            tb.TextMode = TextBoxMode.MultiLine;
            tb.Text = "文明上网，信息发布真实有效。";

            this.Pub1.Add("<TD class=Note>");
            this.Pub1.Add(tb);
            this.Pub1.AddTREnd();
            this.Pub1.AddTableEnd();
        }

        void btn_Click(object sender, EventArgs e)
        {
            try
            {
                string no = this.Pub1.GetTextBoxByID("TB_No").Text.Trim();
                if (no.Length <= 1)
                    throw new Exception("用户名长度必须等于2位数。");

                Member c = new Member();
                if (c.IsExit("No", no))
                    throw new Exception("很抱歉，用户名[" + no + "]，已经存在，请选择其它的用户名。");

                string mail = this.Pub1.GetTextBoxByID("TB_Mail").Text.Trim();
                //if (mail.IndexOf("@") == -1
                //    || mail.Length == 0
                //    || mail.IndexOf(".") == -1 )
                //    throw new Exception("邮件错误。");

                string pass1 = this.Pub1.GetTextBoxByID("TB_Pass1").Text.Trim();
                string pass2 = this.Pub1.GetTextBoxByID("TB_Pass2").Text.Trim();

                if (pass1 != pass2)
                    throw new Exception("两次密码不一致。");

                if (pass1 == "" || pass1.Length == 0)
                    throw new Exception("密码不能为空。");

                c.No = no;
                c.Name = no;
                c.Email = mail;
                if (this.Pub1.GetRadioButtonByID("RB_1").Checked)
                    c.SEX = 1;
                else
                    c.SEX = 2;

                c.ADT = DataType.CurrentDataTime;
                c.RDT = DataType.CurrentDataTime;
                c.Pass = pass1;

                c.FK_Area = "37";
                c.RegFrom = "37";

                c.Insert();

                Glo.Signin(c, true);
                Glo.Trade(CBuessType.CZ_Reg, "Reg", "注册用户赠送");

                string comefrom = this.Request.QueryString["WhereGo"];
                if (comefrom == null || comefrom.Contains("RegUser") || comefrom.Contains("Login.asp"))
                    comefrom = "Home.aspx";

                string url = this.Request.RawUrl;
                if (comefrom.IndexOf("?") != -1)
                    comefrom = comefrom + "&from=23&" + url.Substring(url.IndexOf("?"));
                else
                    comefrom = comefrom + "?from=23&";

                if (comefrom.IndexOf("Msg.aspx") != -1)
                    comefrom = "Home.aspx";

                this.ToUrl(comefrom);
            }
            catch (Exception ex)
            {
                this.Pub1.GetLabelByID("Lab_Msg").Text = "注册期间出现如下错误：" + ex.Message;
            }
        }
    }
}

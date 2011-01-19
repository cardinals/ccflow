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
using BP.YG;
using BP.En;
using BP.DA;
using BP.Web;

namespace BP.Web.YG.HiTax
{
	/// <summary>
	/// RegUser ��ժҪ˵����
	/// </summary>
	public partial class IRegUser :YGPage
	{
        public Button Btn_Submit
        {
            get
            {
                return this.UCPub1.GetButtonByID("Btn_Submit");
            }
        }
		protected void Page_Load(object sender, System.EventArgs e)
		{

            ////foreach (string s in this.Request.ServerVariables)
            ////{
            ////    this.Response.Write("<BR>" + s + " = ");
            ////    this.Response.Write("<B>" + this.Request.ServerVariables[s] + "</b>");
            ////}
            ////return;
        //    this.Request.ServerVariables[.


            //System.Net.IPHostEntry hostinf = Dns.GetHostByName(TxtDomain.Text);
            //TxtIp.Text = hostinf.AddressList[0].ToString();
            //IpLab.Text = IpLab.Text = "��������:" + TxtDomain.Text + "       ��Ӧ��IP��ַ:" + TxtIp.Text; 

			this.BindRegUser();

			this.Btn_Submit.Click+=new EventHandler(Btn_Submit_Click);
		}
		public void BindRegUser()
		{
            string m = "<b><font color=red>*</font></b>";
			this.UCPub1.AddTableDef();
			this.UCPub1.AddTR();
            this.UCPub1.AddTDTitleDef("colspan=2", "������Ѿ�caishui800.cn�û����������<U><a href='/Login.aspx?B=" + this.BureauNo + "' style='color:blue' >��½ϵͳ</a></U>��<U><a href='/Home.aspx?B=" + this.BureauNo + "' style='color:blue' >������ҳ</a></U>��");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right><BR>�û���"+m+"��</TD>");
			TextBox tb = new TextBox();
			tb.ID="TB_No";
			tb.MaxLength=14;
            this.UCPub1.Add("<TD class=Note >");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("<BR>��������½��ϵͳ���ʺţ�Ҳ����blog�ռ���ʺš�<BR>������7�����ֻ�14���ֽ�(���֣���ĸ���»���)</TD>");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right>����"+m+"��</TD>");
			tb = new TextBox();
			tb.ID="TB_Pass1";
			tb.MaxLength=15;
			tb.TextMode =TextBoxMode.Password;
			this.UCPub1.Add("<TD class=Note >");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("<BR>��ע���������밲ȫ��2-15λ��</TD>");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD align=right >��������"+m+"��</TD>");
			tb = new TextBox();
			tb.ID="TB_Pass2";
			tb.MaxLength=15;
			tb.TextMode =TextBoxMode.Password;

			this.UCPub1.Add("<TD>");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("</TD>");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right>�ʼ���</TD>");
			tb = new TextBox();
			tb.ID="TB_Mail";
			tb.MaxLength=100;
			tb.Width=300;
			this.UCPub1.Add("<TD class=Note>");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("<BR>����ʵ��д��������ʧ����ʱ�����������һأ�Ҳ���Խ�������˰��ֵ�֪ͨ�붨������˰����</TD>");
			this.UCPub1.AddTREnd();


            //this.UCPub1.AddTR();
            //this.UCPub1.Add("<TD valign=top align=right>���ԣ�</TD>");
            //tb = new TextBox();
            //tb.ID = "TB_Addr";
            //tb.MaxLength = 100;
            //tb.Width = 300;
            //this.UCPub1.Add("<TD class=Note>");
            //this.UCPub1.Add(tb);
            //this.UCPub1.Add("<BR>���磺ɽ�����ϡ���д����Դ������ϵͳ�����ṩ���򻯷���</TD>");
            //this.UCPub1.AddTREnd();


			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right>��ν��</TD>");
			this.UCPub1.Add("<TD>");
			RadioButton rb = new RadioButton();
			rb.Text="����";
			rb.ID="RB_1";
			rb.Checked=false;
			rb.GroupName="s";
			this.UCPub1.Add(rb);
			rb = new RadioButton();
			rb.Text="Ůʿ";
			rb.ID="RB_2";
			rb.Checked=true;
			rb.GroupName="s";
			this.UCPub1.Add(rb);
			this.UCPub1.Add("</TD>");
			this.UCPub1.AddTREnd();


			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD></TD><TD>");
			Button btn = new Button();
			btn.Text="����Э�鲢�����û�";
			btn.ID="Btn_Submit";
            btn.CssClass = "Btn";

			this.UCPub1.Add(btn);
			this.UCPub1.Add("</TD>");
			this.UCPub1.AddTREnd();


			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD></TD>");
			this.UCPub1.Add("<TD>");

			Label lab = new Label();
			lab.ID="Lab_Msg";
			lab.ForeColor=Color.Red;
			lab.Font.Bold=true;

			this.UCPub1.Add(lab);
			this.UCPub1.Add("</TD>");
			this.UCPub1.Add("<TD></TD>");
			this.UCPub1.AddTREnd();



            this.UCPub1.AddTR();
            this.UCPub1.Add("<TD valign=top align=right>���Э�飺</TD>");
            tb = new TextBox();
            tb.ID = "TB_Leic";
            tb.MaxLength = 100;
            tb.Width = 500;
            tb.Rows = 10;
            tb.TextMode = TextBoxMode.MultiLine;
            tb.Text = "������������Ϣ������ʵ��Ч��";

            this.UCPub1.Add("<TD class=Note>");
            this.UCPub1.Add(tb);
            this.UCPub1.AddTREnd();


			this.UCPub1.AddTableEnd();
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

		private void Btn_Submit_Click(object sender, EventArgs e)
		{

            try
            {
                string no = this.UCPub1.GetTextBoxByID("TB_No").Text.Trim();
                if (no.Length <= 1)
                    throw new Exception("�û������ȱ������2λ����");

                Customer c = new Customer();
                if (c.IsExit("No", no))
                    throw new Exception("�ܱ�Ǹ���û���[" + no + "]���Ѿ����ڣ���ѡ���������û�����");


                string mail = this.UCPub1.GetTextBoxByID("TB_Mail").Text.Trim();
                //if (mail.IndexOf("@") == -1
                //    || mail.Length == 0
                //    || mail.IndexOf(".") == -1 )
                //    throw new Exception("�ʼ�����");

                string pass1 = this.UCPub1.GetTextBoxByID("TB_Pass1").Text.Trim();
                string pass2 = this.UCPub1.GetTextBoxByID("TB_Pass2").Text.Trim();

                if (pass1 != pass2)
                    throw new Exception("�������벻һ�¡�");

                if (pass1 == "" || pass1.Length == 0)
                    throw new Exception("���벻��Ϊ�ա�");

                c.No = no;
                c.Name = no;
                c.Email = mail;
                if (this.UCPub1.GetRadioButtonByID("RB_1").Checked)
                    c.SEX = 1;
                else
                    c.SEX = 2;

                c.ADT = DataType.CurrentDataTime;
                c.RDT = DataType.CurrentDataTime;
              //  c.Addr = this.UCPub1.GetTextBoxByID("TB_Addr").Text.Trim();
                c.Pass = pass1;
           //     c.FK_Bureau = Global.BureauNo;

                c.FK_Area = "37";

                c.RegFrom = "37";


                //string ip = this.Request.ServerVariables["REMOTE_ADDR"];
                //try
                //{
                //    //BP.CN.IP pp = new BP.CN.IP(ip);
                //    //c.FK_Area = pp.FK_Area;
                //}
                //catch (Exception ex)
                //{
                //    string fk_area = BP.CN.Area.GenerAreaNoByName(c.Addr, null);
                //    if (fk_area == null)
                //        throw new Exception("@ϵͳ�޷��ж�������Դ���޷������ṩ��������������д����ϸ�ĵ�ַ��" + ex.Message);
                //    c.FK_Area = fk_area;
                //}

                c.Insert();

                Global.Signin(c,true);
                Global.Trade(CBuessType.CZ_Reg, "Reg", "ע���û�����");
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
                this.UCPub1.GetLabelByID("Lab_Msg").Text = "ע���ڼ�������´���" + ex.Message;
            }

		}
	}
}

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
using BP.Web.Comm;
using BP.Web.Comm.UC;
using BP.XML;

namespace BP.Web.CT.GTS.Port
{
	/// <summary>
	/// Bar ��ժҪ˵����
	/// </summary>
	public partial class Bar : System.Web.UI.Page
	{
        public void Bind()
        {
            BP.Sys.Xml.ShortKeys sks = new BP.Sys.Xml.ShortKeys();
            sks.RetrieveAll();

            this.UCSys1.Add("<div class='top_nav'>");
            foreach (BP.Sys.Xml.ShortKey sk in sks)
            {
                //this.UCSys1.Add("<a href='" + sk.URL + "' target='left' ><img src='" + sk.Img + "' border=0 />" + sk.Name + "</a>");
                this.UCSys1.Add("<a href='" + sk.URL + "' target='" + sk.Target + "' ><img src='./Style/img/"+sk.No+".gif' border=0 />" + sk.Name + "</a>&nbsp;");
            }
            this.UCSys1.AddDivEnd(); // ("<div id='top_nav'>");


        }
		protected void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                this.Bind();
            }
            catch
            {
                this.Bind();
            }
			return ;
			
			this.UCSys1.Add("<a href='../Home.aspx' target='mainfrm' ><img src='../Images/Home.gif' border=0 />��ҳ</a>");
			this.UCSys1.Add("<a href='../List.aspx?StateOfHD=1' target='mainfrm' ><img src='../Images/Home.gif' border=0 />��˰���</a>");
			this.UCSys1.Add("<a href='../../Comm/Port/ChangeSystem.aspx' target='mainfrm' ><img src='../../Images/System/Power.gif' border=0 />�л�ϵͳ</a>");
			return;
			 
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

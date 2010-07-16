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


namespace BP.Web.CT.GTS.Port
{
	/// <summary>
	/// Bar ��ժҪ˵����
	/// </summary>
	public partial class Bar : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
            ens.RetrieveAll();

            foreach (BP.WF.XML.ToolBar en in ens)
            {
                this.UCSys1.Add("<a href='" + en.Url + "'  title='" + en.Title + "' ><img src='" + en.Img + "' border='0' >" + en.Name + "</a>&nbsp;");
            }
            return;

            this.UCSys1.Add("<a href='../Home.aspx' target='mainfrm' title='˰��ҵ��������ҳ��' ><img src='images/home.gif' border='0' width='15' height='15'>��ҳ&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../Start.aspx' target='mainfrm' title='�����б���ҳ��'><img src='images/file_folder.gif' border='0' width='15' height='15'>���̷���&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");
            this.UCSys1.Add("<a href='../MyWork.aspx' target='mainfrm' title='���칤������'><img src='images/MyRptOn.gif' border='0' width='15' height='15'>���칤��&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../Runing.aspx' target='mainfrm' title='��;����'><img src='images/MyRptOn.gif' border='0' width='15' height='15'>��;����&nbsp;</a>");

            

            this.UCSys1.Add("<a href='../Warning.aspx' target='mainfrm' title='���칤������'><img src='images/alert.gif' border='0' width='15' height='15'>����Ԥ��&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../Book/Home.aspx' target='mainfrm' title='����ͳ�ơ���������'><img src='images/index.gif' border='0' width='15' height='15'>�������&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;&nbsp;</font>");

      //      this.UCSys1.Add("<a href='../../NSR/shenbaotaizhang.aspx' target='mainfrm' title='��˰̨��'><img src='images/311.gif' border='0' width='15' height='15'>��˰̨��&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='Help.htm' target='mainfrm' title='����'><img src='images/Help.gif' border='0'>����&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../../Comm/Port/ChangeSystem.aspx' target='mainfrm' title='���Ǳ�ϵͳ���еĹ���ģ�飬��ϵͳ֮���޷���ϣ���������ϵͳ�л�ҳ��'><img src='images/quit.gif' border='0' width='15' height='15'>�л�ϵͳ</a>");
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

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

namespace BP.Web.CT.CT
{
	/// <summary>
	/// Delete ��ժҪ˵����
	/// </summary>
	public partial class UIDelete : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{

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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			return ;

			BP.DA.DBAccess.RunSQL("delete ct_hd");
			BP.DA.DBAccess.RunSQL("delete ct_hdresult");
			BP.DA.DBAccess.RunSQL("delete ct_history");
			BP.DA.DBAccess.RunSQL("delete ct_historyresult");
			BP.DA.DBAccess.RunSQL("delete ct_wjds");
			BP.DA.DBAccess.RunSQL("delete ct_wjdsdtl");
			BP.DA.DBAccess.RunSQL("delete ct_fszhd");
			BP.DA.DBAccess.RunSQL("delete ct_dsggh");
			BP.DA.DBAccess.RunSQL("delete ct_ddds");
			BP.DA.DBAccess.RunSQL("delete ct_dddsdtl");
			BP.DA.DBAccess.RunSQL("delete ct_ddds");
			BP.DA.DBAccess.RunSQL("delete ct_checkdtl");
			BP.DA.DBAccess.RunSQL("delete ct_checkdtlfsz");
			BP.DA.DBAccess.RunSQL("delete CT_CheckDtlIncome");
			BP.DA.DBAccess.RunSQL("delete ct_HDHistory");
			BP.DA.DBAccess.RunSQL("delete CT_CheckDtlHistory");
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			BP.DA.DBAccess.RunSQL("delete ct_hd where StateOfHD=1  and workid  not in (select workid from dswf.wf_generworkflow)");
			BP.DA.DBAccess.RunSQL("delete ct_hd where StateOfHD=2  and workid  not in (select workid from dswf.wf_generworkflow)");
			BP.DA.DBAccess.RunSQL("delete ct_hd where StateOfHD=3  and workid  not in (select workid from dswf.wf_generworkflow)");
			BP.DA.DBAccess.RunSQL("delete ct_hd where StateOfHD=4  and workid  not in (select workid from dswf.wf_generworkflow)");
		}
	}
}

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

namespace BP.Web.App
{
	/// <summary>
	/// Home ��ժҪ˵����
	/// </summary>
	public partial class Head : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			string path = this.Request.ApplicationPath;
			string str="<b>";
			if (this.Session["UserNo"]==null)
			{
				str+="<br><br><center><img  src='users.gif' border='0'   ><a href='"+SystemConfig.PageOfLostSession+"' target='mainfrm' >��½ʱ��̫����Ҫ�����µ�¼</a><img src='../../Images/Btn/Refurbish.gif'><a href='Head.aspx'  border=0 >ˢ��</a></center>";
				//this.Response.Write(str+"</b>");
				return;
			}

			//
			//			str="<p align=right ><b>";
			//			str+="<img  src='Home.gif'          border='0'   ><a href='Wel.aspx' target='mainfrm' ><b>��ҳ</b></a>";
			//			str+="<img  src='OnlineUser.gif'    border='0'   ><a href='"+path+"/Comm/Port/OnlineUsers.aspx' target='_OnlineUserWin' >����("+OnlineUserManager.GetUserCount()+")</a>";
			//			str+="<img  src='Help.gif'          border='0'   ><a href='Helper.htm' target='_blank' >����</a>";
			//			str+="<img  src='Personal.gif'      border='0'   ><a href='"+path+"/Comm/Port/Personal.aspx' target='mainfrm' >����</a>";
			//			str+="<img  src='ChangeUser.gif'    border='0'   ><a href='SignIn.aspx?IsChangeUser=1' target='mainfrm' >�����û�("+WebUser.Name+")</a>";
			//
			//			str+="<br>";
			//          str+="<br>";
			// 
			//	str+="<img  src='"+path+"/App/Paper/ChoseOne.gif'         border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=ChoseOne' target='mainfrm' >��ѡ��</a>";
			//	str+="<img  src='"+path+"/App/Paper/ChoseM.gif'           border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=ChoseM' target='mainfrm' >��ѡ��</a>";
			//	str+="<img  src='"+path+"/App/Paper/FillBlank.gif'        border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=FillBlank' target='mainfrm' >�����</a>";
			//	str+="<img  src='"+path+"/App/Paper/JudgeTheme.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=JudgeTheme' target='mainfrm' >�ж���</a>";
			//	str+="<img  src='"+path+"/App/Paper/EssayQuestion.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=EssayQuestion' target='mainfrm' >�ʴ���</a>";
			//	str+="<img  src='"+path+"/App/Paper/RC.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=RC' target='mainfrm' >�Ķ����</a>";
			//	str+="<img  src='"+path+"/App/Paper/RC.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=RC' target='mainfrm' >�Ķ����</a>";
			//	str+="<img  src='"+path+"/App/Paper/RC.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=RC' target='mainfrm' >�Ķ����</a>";
			//	str+="</b></p>";
			//	this.Response.Write(str);

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

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using BP.Web;
using BP.Web.Controls;
using BP.En;
using BP.Sys;
using BP.DA;
using BP.Port;

namespace BP.Web.Controls
{
	/// <summary>
	/// WebMenm ��ժҪ˵����
	/// ����Web���Ҽ��̲˵���
	/// </summary>	
	[System.Drawing.ToolboxBitmap(typeof(System.Web.UI.WebControls.Panel))]	
	public class WebMenu:BPPanel
	{
		public void BindSystem()
		{
			this.Attributes["width"]="100%";
			Script="<Table id='"+this.ClientID+"'   border='2' cellpadding='0' cellspacing='0' style='border-collapse: collapse' width=100%    bordercolorlight='#C0C0C0' bordercolordark='#808080' >\n";
			this.Script+="<tr  >";
			this.Script+="<td bgcolor='#C0C0C0'><b>ϵͳ���</b></td>\n";
			this.Script+="<td bgcolor='#C0C0C0'><b>����</b></td>\n";
			this.Script+="<td bgcolor='#C0C0C0'><b>�汾</b></td>\n";
			this.Script+="<td bgcolor='#C0C0C0' ><b>��������</b></td>\n";
			this.Script+="</tr>\n";

			BPSystems ens = new BPSystems();
			ens.RetrieveAll();

			foreach(BPSystem en in ens)
			{
				this.Script+="<tr >\n";
				this.Script+="<td >"+en.No+"</td>\n";
				if (en.IsOk && SystemConfig.SysNo !=en.No )
				{
					this.Script+="<td ><a href='"+en.URL+"&Token="+WebUser.Token+"&No="+WebUser.No+"' target='_parent' >"+en.Name+"</a></td>\n";
				}
				else
				{
					this.Script+="<td >"+en.Name+"</td>\n";
				}
				this.Script+="<td >"+en.Ver+"</td>\n";
				this.Script+="<td >"+en.IssueDate+"</td>\n";
				this.Script+="</tr>\n";
			}

			this.Script+="</Table>\n";
 
			this.Controls.Clear();
			LiteralControl li =new LiteralControl(this.Script);
			this.Controls.Add(li);
			this.Visible=true;
		}
		 
		#region ����
		/// <summary>
		/// 
		/// </summary>
		public WebMenu()
		{
		}
		 
		public void BindWorks()
		{
			this.beforeAdd();
			this.AddItem("��������","DealWork()","");
			this.AddItem("���̴���","DealWorkFlow()","/Images/Btn/WorkFlowOp.gif");
			this.AddItem("��������","Rpt()","/Images/Btn/PrintWorkRpt.gif");
			//this.AddItem("��������","DealWork()","/Images/Btn/PrintWorkRpt.gif");
			this.AddItem("���乤��","AllotTask()","/Images/Btn/AllotTask.gif");
			this.AddItem("�˻�","ReturnWork()","/Images/Btn/ReturnWork.gif");
			this.AddItem("����","WorkHelp()","/Images/Btn/Help.gif");
			this.AddItem("����","WorkAdjunct()","/Images/Btn/Adjunct.gif");
			this.AddItem("<b>��Ƭ</b>","Card()","/Images/Btn/Card.gif");
			/*			 
			this.AddItem("���̲���","OpWork()","");
			this.AddItem("��������","Rpt()","");
			this.AddItem("��������","CH()","");
			//this.AddItem("����","Att","");
			this.AddItem("���乤��","AllotTask()","");
			//this.AddItem("��������","WorkOut","");
			*/
			this.EndAdd();

			this.Controls.Clear();
			LiteralControl li =new LiteralControl(this.Script);
			this.Controls.Add(li);
			this.Visible=false;
		}
		public void BindRpt(Rpt.Rpt3D rpt,string d )
		{
			this.beforeAdd();
			foreach(Attr attr in rpt.DAttrs)
			{
				this.AddItem( attr.Desc ,"DClick('"+rpt.ToString()+"' ,'"+attr.Key+"','"+d+"')", "/Comm/Rpt/D.ico" );
			}

			this.EndAdd();
			this.Controls.Clear();
			LiteralControl li =new LiteralControl(this.Script);
			this.Controls.Add(li);
			this.Visible=true;
		}

		
		public void BindRptCell(Rpt.Rpt3D rpt)
		{
			this.beforeAdd();

			string ShowType=PageBase.GetSessionByKey(rpt.ToString(),"ShowType"); 
			string rptName=rpt.ToString();

			switch(ShowType)
			{
				case "rpt":
					this.AddItem("��״ͼ","ShowType('"+rptName+"', 'a')","/Images/Pub/Histogram.ico");
					this.AddItem("��״ͼ","ShowType('"+rptName+"', 'b')","/Images/Pub/pie.ico");
					this.AddItem("����ͼ","ShowType('"+rptName+"', 'c')","/Images/Pub/zx.ico");

					this.addSpt();
					this.AddItem("��ӡ","Print('"+rptName+"')","/Images/Btn/Print.gif");
					this.AddItem("������ Excel","Excel('"+rptName+"')","/Images/Btn/ExportToExcel.gif");
					this.addSpt();

					this.AddItem("����ʾ����","RateType('"+rptName+"','0')",null);
					this.AddItem("�����","RateType('"+rptName+"','1')","/Images/Pub/Rate.ico");
					this.AddItem("�����","RateType('"+rptName+"','2')","/Images/Pub/Rate.ico");
					//this.AddItem("���ͼƬ","Print('"+rptName+"')","/Images/Btn/ExportToExcel.gif");
					break;
				case "a":
					this.AddItem("����","ShowType('"+rptName+"','rpt')","/Images/Pub/Rpts.ico");
					this.AddItem("��״ͼ","ShowType('"+rptName+"','b')","/Images/Pub/pie.ico");
					this.AddItem("����ͼ","ShowType('"+rptName+"','c')","/Images/Pub/zx.ico");
					break;
				case "b":
					this.AddItem("����","ShowType('"+rptName+"','rpt')","/Images/Pub/Rpts.ico");
					this.AddItem("��״ͼ","ShowType('"+rptName+"','a')","/Images/Pub/Histogram.ico");
					this.AddItem("����ͼ","ShowType('"+rptName+"','c')","/Images/Pub/zx.ico");
					break;
				case "c":
					this.AddItem("����","ShowType('"+rptName+"','rpt')","/Images/Pub/Rpts.ico");
					this.AddItem("��״ͼ","ShowType('"+rptName+"','a')","/Images/Pub/Histogram.ico");
					this.AddItem("��״ͼ","ShowType('"+rptName+"','b')","/Images/Pub/pie.ico");
					break;
				default:
					this.AddItem("��״ͼ","ShowType('"+rptName+"','a')","/Images/Pub/Histogram.ico");
					this.AddItem("��״ͼ","ShowType('"+rptName+"','b')","/Images/Pub/pie.ico");
					this.AddItem("����ͼ","ShowType('"+rptName+"','c')","/Images/Pub/zx.ico");
					break;

			}
			this.addSpt();

			this.AddItem("�����ѯ","PanelEns('"+rpt.HisEns.ToString()+"')","/Images/Btn/ChoseCol.gif");
			//this.AddItem("ѡ���в�ѯ","ChoseColSearch('"+rpt.HisEns.ToString()+"')","/Images/Btn/ChoseCol.gif");
			this.AddItem("���ݷ���","GroupEns('"+rpt.HisEns.ToString()+"')","/Images/Btn/ChoseCol.gif");

			this.EndAdd();

			this.Controls.Clear();
			LiteralControl li =new LiteralControl(this.Script);
			this.Controls.Add(li);
			this.Visible=true;
		}

		public void BindTADay()
		{
			this.beforeAdd();
            if (BP.Web.WebUser.IsCurrUser)
            {
                this.AddItem("<B>�½���־</b>", "NewLog()", "/TA/Images/Log_s.ico");
                this.AddItem("�½�����", "NewWork()", "/TA/Images/Work_s.ico");
                this.AddItem("�½�����", "NewTask()", "/TA/Images/Task_s.ico");
                this.AddItem("�½������¼�", "NewCycleEvent()", "/TA/Images/CycleEvent_s.gif");

                this.AddItem("�½��ʼ�", "NewMail()", "/TA/Images/Mail_s.gif");
                this.AddItem("�鿴����", "Share()", "/TA/Images/Notepad_s.ico");

                this.AddItem("����", "NewNotepad()", "/TA/Images/Notepad_s.ico");
            }
            else
            {
                this.AddItem("�����ҵ�����", "ToMyCalendar('" + WebUser.No + "')", "/TA/Images/Notepad_s.ico");
            }

			this.AddItem("ת������","TurnToADay()","/TA/Images/To.ico");
			this.AddItem("ת������","TurnToDay('"+DateTime.Now.ToString("yyyy-MM-dd")+"')","/TA/Images/To.ico");

			//this.AddItem("ת���·����","javascript:window.location.href='MyMonth.aspx'","/TA/Images/MonWell_s.ico");
			this.EndAdd();

			this.Controls.Clear();
			LiteralControl li =new LiteralControl(this.Script);
			this.Controls.Add(li);
			this.Visible=true;
		}
		 
		public void BindTAWeek()
		{
			this.beforeAdd();
			this.AddItem("<B>�½���־</b>","NewLog()", "/TA/Images/Log_s.ico");
			this.AddItem("�½�����","NewWork()","/TA/Images/Work_s.ico");
			this.AddItem("�½�����","NewTask()","/TA/Images/Task_s.ico");
			this.AddItem("�½������¼�","NewCycleEvent()","/TA/Images/CycleEvent_s.gif");
			this.AddItem("����","NewNotepad()","/TA/Images/Notepad_s.ico");
			this.AddItem("ת���·����","javascript:window.location.href='MyMonth.aspx'","/TA/Images/MonWell_s.ico");
			this.EndAdd();

			this.Controls.Clear();
			LiteralControl li =new LiteralControl(this.Script);
			this.Controls.Add(li);
			this.Visible=true;

		}
		public string EnName
		{
			get
			{
				return ViewState["EnName"].ToString();
			}
			set
			{
				ViewState["EnName"]=value;				
			}
		}
		public new string EnsName
		{
			get
			{
				return ViewState["EnsName"].ToString();
			}
			set
			{
				ViewState["EnsName"]=value;				
			}
		}
		public void BindEn(Entity en, UAC uac,string ctrl)
		{
            return;
			//			if (BP.DA.Cash.IsExits("WebMenu"+en.ToString(),Depositary.Session)==false)
			//				BP.DA.Cash.AddObj("WebMenu"+en.ToString(),Depositary.Session, this.GenerWebMenu(en,uac,ctrl) );
			//			else
			//				this.Script=(string)BP.DA.Cash.GetObjFormApplication("WebMenu"+en.ToString(),null);

			this.Controls.Clear();
			this.Script= this.GenerWebMenu(en,uac,ctrl);
			LiteralControl li =new LiteralControl(this.Script);
			this.Controls.Add(li);
			this.Visible=true;
			//	this.Width=150;
		}

		#endregion
		
		#region ��������
		 
		 
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="en"></param>
		/// <param name="uac"></param>
        public void DataPanel(Entities ens, UAC uac)
        {
            Entity en = ens.GetNewEntity;
            string webpath = this.Page.Request.ApplicationPath;
            this.beforeAdd();

            #region ���ʿ��ƣ�
            this.AddItem("��Ƭ", "Card()", "/Images/Btn/Card.gif");
            if (uac.IsInsert)
                this.AddItem("�½�", "New()", "/Images/Btn/New.gif");
            if (uac.IsDelete)
                this.AddItem("ɾ��", "Delete()", "/Images/Btn/Delete.gif");
            if (uac.IsUpdate)
                this.AddItem("�༭", "Item3()", "/Images/Btn/Edit.gif");
            #endregion

            #region �������ŵ� ����
            RefMethods myreffuncs = en.EnMap.HisRefMethods;
            if (myreffuncs.Count > 0)
            {
                this.AddSpt();
                foreach (RefMethod func in myreffuncs)
                {
                    if (func.Visable == false)
                        continue;

                    string url = "/Comm/RefMethod.aspx?Index=" + func.Index + "&EnsName=" + this.EnsName;
                    this.AddItem(func.Title, "RefMethod('" + func.Index + "', '" + func.Warning + "', '" + func.Target + "', '" + this.EnsName + "')", func.Icon);
                }
            }
            #endregion

            #region  ���ݷ���
            //			foreach(Attr attr in en.EnMap.Attrs)
            //			{
            //				if (attr.MyFieldType==FieldType.Enum
            //					|| attr.MyFieldType==FieldType.FK 
            //					|| attr.MyFieldType==FieldType.PKEnum
            //					|| attr.MyFieldType==FieldType.PKFK
            //					)
            //					this.AddItem("����:"+attr.Desc,"GroupBy('"+ens.ToString()+"','"+attr.Key+"')",null);
            //			}
            #endregion

            #region ����������ϸ
            EnDtls enDtls = en.EnMap.Dtls;
            if (enDtls.Count > 0)
            {
                this.AddSpt();
                foreach (EnDtl enDtl in enDtls)
                {
                    this.AddItem("�༭:" + enDtl.Desc, "EditDtl('" + enDtl.EnsName + "','" + enDtl.RefKey + "')", enDtl.Ens.GetNewEntity.EnMap.Icon);
                }
            }
            #endregion

            #region ����һ�Զ��ʵ��༭
            AttrsOfOneVSM oneVsM = en.EnMap.AttrsOfOneVSM;
            if (oneVsM.Count > 0)
            {
                this.AddSpt();
                foreach (AttrOfOneVSM vsM in oneVsM)
                {
                    this.AddItem("�༭:" + vsM.Desc, "EditOneVsM('" + vsM.EnsOfMM.ToString() + "','" + vsM.EnsOfMM + "&dt=" + DateTime.Now.ToString("hhss") + "')", vsM.EnsOfM.GetNewEntity.EnMap.Icon);
                }
            }
            #endregion

            #region �������ŵ���ع���
            //			SysUIEnsRefFuncs reffuncs = ens.HisSysUIEnsRefFuncs;
            //			if ( reffuncs.Count > 0  )
            //			{
            //				this.AddSpt();
            //				foreach(SysUIEnsRefFunc en1 in reffuncs)
            //				{
            //					this.AddItem(en1.Name,"EnsRefFunc('"+en1.OID.ToString()+"')",en1.Icon);
            //				}
            //			}
            #endregion

            if (en.EnMap.AdjunctType != AdjunctType.None)
                this.AddAdjunct(en);

            //this.addSpt();

            //this.AddItem("ѡ���в�ѯ","UIEnsCols('"+ens.ToString()+"')","/Images/Btn/ChoseCol.gif");
            this.AddItem("���ݷ���", "GroupEns('" + ens.ToString() + "')", "/Images/Btn/DataGroup.gif");
            this.AddItem("�������", "DataCheck('" + en.ToString() + "')", "/Images/Btn/DTS.gif");


            this.AddHelp();
            this.EndAdd();

            this.Controls.Clear();
            LiteralControl li = new LiteralControl(this.Script);
            this.Controls.Add(li);
            this.Visible = true;
        }
		public string Script=null;
		/// <summary>
		/// �����˵�
		/// </summary>
		/// <param name="en">ʵ��</param>
		/// <param name="uac">SysEnsUAC</param>
		/// <returns></returns>
		private string GenerWebMenu(Entity en, UAC uac, string ctrl)
		{
            return "";

			string webpath=this.Page.Request.ApplicationPath; 
			//Script+="<DIV id='MDG1' style='backgroundColor=white'  style.display='none' >\n";
			this.beforeAdd();

			#region ���ʿ��ƣ�
			this.AddItem("��Ƭ","Card()","/Images/Btn/Card.gif");
			//			if (uac.IsInsert)
			//				this.AddItem("��Ƭ��ʽ�½�","New()","/Images/Btn/New.gif");
			//if (uac.IsDelete)			
			//this.AddItem("ɾ��","Delete()","/Images/Btn/Delete.gif");
			//	if (uac.IsUpdate)
			//	this.AddItem("����","Update()","/Images/Btn/Update.gif");			
			#endregion

			#region �������ŵ� ����
			RefMethods myreffuncs = en.EnMap.HisRefMethods ;
			if ( myreffuncs.Count > 0  )
			{
				this.AddSpt();
				foreach(RefMethod func in myreffuncs)
				{
					if (func.Visable==false)
						continue;


					string url="/Comm/RefMethod.aspx?Index="+func.Index+"&EnsName="+this.EnsName ;

					this.AddItem(func.Title, "RefMethod('"+func.Index+"', '"+func.Warning+"', '"+func.Target+"', '"+this.EnsName+"')", func.Icon);
				}
			}
			#endregion


			#region ����������ϸ
			EnDtls enDtls= en.EnMap.Dtls;
			if ( enDtls.Count > 0 )
			{
				this.AddSpt();
				foreach(EnDtl enDtl in enDtls)
				{
					this.AddItem( enDtl.Desc,"EditDtl('"+enDtl.EnsName+"','"+enDtl.RefKey+"')",enDtl.Ens.GetNewEntity.EnMap.Icon );
				}
			}
			#endregion

			#region ����һ�Զ��ʵ��༭
			AttrsOfOneVSM oneVsM= en.EnMap.AttrsOfOneVSM;
			if ( oneVsM.Count > 0 )
			{
				this.AddSpt();
				foreach(AttrOfOneVSM vsM in oneVsM)
				{
					this.AddItem("�༭:"+vsM.Desc,"EditOneVsM('"+vsM.EnsOfMM.ToString()+"','"+vsM.EnsOfMM+"&dt="+DateTime.Now.ToString("hhss")+"')",vsM.EnsOfM.GetNewEntity.EnMap.Icon);
				}
			}
			#endregion

			#region �������ŵ���ع���
//			SysUIEnsRefFuncs reffuncs = new SysUIEnsRefFuncs();
//			reffuncs.Retrieve(SysUIEnsRefFuncAttr.No, this.EnsName);
//			if ( reffuncs.Count > 0  )
//			{
//				this.AddSpt();
//				foreach(SysUIEnsRefFunc en1 in reffuncs)
//				{
//					this.AddItem(en1.Name,"EnsRefFunc('"+en1.OID.ToString()+"')",en1.Icon);
//				}
//			}
			#endregion	 

			if ( en.EnMap.AdjunctType!=AdjunctType.None)
				this.AddAdjunct(en);

			this.AddFont(ctrl);


			this.AddItem("�����ѯ","PanelEns('"+this.EnsName.ToString()+"')","/Images/Btn/ChoseCol.gif");
			//this.AddItem("ѡ���в�ѯ","UIEnsCols('"+ens.ToString()+"')","/Images/Btn/ChoseCol.gif");
			this.AddItem("���ݷ���","GroupEns('"+this.EnsName.ToString()+"')","/Images/Btn/DataGroup.gif");

			//this.AddItem("ѡ���в�ѯ","ChoseColSearch('"+ens.ToString()+"')","/Images/Btn/ChoseCol.gif");
			//this.AddItem("�����ѯ","GroupEns('"+ens.ToString()+"')","/Images/Btn/ChoseCol.gif");

			this.AddHelp();
			this.EndAdd();
			return this.Script;
		}
		#endregion

		#region ��������
		/// <summary>
		/// StartAdd
		/// </summary>
		public void beforeAdd()
		{
			Script="<Table class='MTable"+WebUser.Style+"' id='"+this.ClientID+"'  bordercolorlight='#C0C0C0' bordercolordark='#808080' border='2'  cellpadding='0' cellspacing='0'>\n";
			Script+="<TR><TD height='5px' ></TD> <TD></TD><TD></TD></TR>";
		}
		public void addSpt()
		{
			Script+="<TR><TD height='5px' ></TD> <TD></TD><TD></TD></TR>";
		}
		/// <summary>
		/// EndAdd
		/// </summary>
        public void EndAdd()
        {
            Script += "<TR><TD height='5px' ></TD> <TD></TD><TD></TD></TR>";

            this.Script += "</Table>\n";

            string script1 = "<script language=JavaScript >HideMenu( '" + this.ID + "' );</script>";

            if (this.Page.ClientScript.IsStartupScriptRegistered("W" + this.ID) == false)
                this.Page.ClientScript.RegisterStartupScript(Page.GetType(), "W" + this.ID, script1);

            //string script1= "<script language=JavaScript >document.getElementById( '"+this.ID+"' ).style.visibility='hidden' ;</script>";
            //this.Page.Response.Write(script1);
            this.Width = 200;
        }
		/// <summary>
		/// ����һ��Item.
		/// </summary>
		/// <param name="text">����</param>
		/// <param name="onclick">�¼�����</param>
		/// <param name="imgSrc">ͼƬ·��</param>
        public void AddItem(string text, string onclick, string imgSrc)
        {
            string path = this.Page.Request.ApplicationPath; 
            this.Script += "<TR  class='Title' onclick=\"" + onclick + "\"   onmouseout='MTROut" + WebUser.Style + "(this);' onmouseover='MTROn" + WebUser.Style + "(this);'    >\n";
            if (imgSrc == null || imgSrc == "")
                this.Script += "     <TD nowrap='nowrap' ></td>\n";
            else
                this.Script += "    <TD nowrap='nowrap' >&nbsp;&nbsp;<img  border='0' src='" + path + imgSrc + "'  ></td>\n";

            this.Script += "       <TD nowrap='nowrap'  >&nbsp;" + text + "&nbsp;&nbsp;</td>\n";
            this.Script += "</TR>\n";
        }

		public void AddSpt()
		{
		}
		#endregion 

		#region ϵͳ�Լ�����ķ���
		/// <summary>
		/// ���Ӵ�����
		/// </summary>
		public void AddFont(string ctrl)
		{
			//this.AddItem("������","DoZoom('"+ctrl+"', '"+this.ClientID+"', '16px')",null);
			//this.AddItem("������","DoZoom('"+ctrl+"', '"+this.ClientID+"', '14px')",null);
			//this.AddItem("С����","DoZoom('"+ctrl+"', '"+this.ClientID+"', '12px')",null);
			 
			/*
			//return ;
			this.AddItem("������","DoZoom('"+ctrl+"', '"+this.ClientID+"', '16px')","/images/Btn/Help.gif");
			this.AddItem("������","DoZoom('"+ctrl+"', '"+this.ClientID+"', '14px')","/images/Btn/Help.gif");
			this.AddItem("С����","DoZoom('"+ctrl+"', '"+this.ClientID+"', '12px')","/images/Btn/Help.gif");
			*/
		}		 
		/// <summary>
		/// ���Ӱ���
		/// </summary>
		public void AddHelp()
		{
			this.AddItem("����","Helper()","/images/Btn/Help.gif");
		}
		/// <summary>
		/// ���Ӹ���
		/// </summary>
		/// <param name="en"></param>
		public void AddAdjunct(Entity en)
		{
			this.AddItem("����","Adjunct('"+en.ToString()+"')", "/images/Btn/Adjunct.gif");
		}
		#endregion
	}
	/// <summary>
	/// �˵���Ŀ
	/// </summary>
	public class MItem
	{
		public string OnClick=null;
		public string ImgSrc=null; 
		public MItem(string OnClick, string ImgSrc)
		{
			this.ImgSrc=ImgSrc;
			this.OnClick=OnClick;
		}
		public MItem(){}
	}
	/// <summary>
	/// �˵�����
	/// </summary>
	public class MItems:System.Collections.CollectionBase
	{
		/// <summary>
		/// ����һ���˵�
		/// </summary>
		/// <param name="entity">ʵ��</param>
		/// <returns></returns>
		public virtual int AddItem(MItem entity)
		{
			return this.InnerList.Add(entity);
		}
		/// <summary>
		/// ����һ����Ŀ
		/// </summary>
		/// <param name="onclickScript">onclickScript</param>
		/// <param name="ImgSrc">ImgSrc</param>
		/// <returns></returns>
		public int AddItem(string onclickScript, string ImgSrc)
		{
			MItem en = new MItem();
			en.ImgSrc = ImgSrc;
			en.OnClick = onclickScript;
			return this.InnerList.Add(en);
		}
		/// <summary>
		/// MItem
		/// </summary>
		public MItem this[int index]
		{
			get 
			{
				return (MItem)this.InnerList[index];
			}
		}
		/// <summary>
		/// MItems
		/// </summary> 
		public MItems()
		{
		}

	}
}

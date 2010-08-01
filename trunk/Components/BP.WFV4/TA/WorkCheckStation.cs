using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;
using BP.Web;
using BP.Sys;

namespace BP.TA
{
	/// <summary>
	/// ����ڵ�����
	/// </summary>
	public class WorkAttr:EntityOIDAttr
	{
		/// <summary>
		/// ���ڵ�ID
		/// </summary>
		public const string ParentID="ParentID"; 
		/// <summary>
		/// ����
		/// </summary>
		public const string Step="Step"; 
		/// <summary>
		/// ���˷�
		/// </summary>
		public const string CentOfCheck="CentOfCheck"; 
		/// <summary>
		/// �ڵ�״̬
		/// </summary>
		public const string WorkState="WorkState"; 	
		/// <summary>
		/// ���ȼ�
		/// </summary>
		public const string PRI="PRI"; 
		/// <summary>
		/// ���˷�Χ
		/// </summary>
		public const string CheckScope="CheckScope"; 
		/// <summary>
		/// �Ƿ���Ҫ�Ķ���ִ
		/// </summary>
		public const string IsRe="IsRe"; 
		/// <summary>
		/// ����
		/// </summary>
		public const string Title="Title"; 
		/// <summary>
		/// ����
		/// </summary>
		public const string Docs="Docs"; 
		/// <summary>
		/// Emps
		/// </summary>
		public const string Emps="Emps"; 	
		/// <summary>
		/// ִ����
		/// </summary>
		public const string Executer="Executer";
		/// <summary>
		/// ������
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const string DateTimeOfAccept="DateTimeOfAccept";
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public const string DateTimeOfSend_del="DateTimeOfSend";

		/// <summary>
		/// ����ʼʱ��
		/// </summary>
		public const string DateTimeOfTaskStart="DateTimeOfTaskStart";
		/// <summary>
		/// Ҫ�����ʱ��
		/// </summary>
		public const string DateTimeOfTaskEnd="DateTimeOfTaskEnd";


		#region ���˷�ʽ
		/// <summary>
		/// ���˷�ʽ
		/// </summary>
		public const string CheckWay="CheckWay";

		/// <summary>
		/// һ���Կ�
		/// </summary>
		public const string CentOfOnce="CentOfOnce";
		/// <summary>
		/// ÿ���
		/// </summary>
		public const string CentOfPerDay="CentOfPerDay";
		/// <summary>
		/// ��߿۷�
		/// </summary>
		public const string CentOfMax="CentOfMax";
		#endregion


		#region ѭ����ʽ
		/// <summary>
		/// ѭ����ʽ
		/// </summary>
		public const string CycleWay="CycleWay";
		/// <summary>
		/// ��
		/// </summary>
		public const string CycleWeek="CycleWeek";
		/// <summary>
		/// ��
		/// </summary>
		public const string CycleDay="CycleDay";
		/// <summary>
		/// ����
		/// </summary>
		public const string CycleYearDay="CycleYearDay";
		/// <summary>
		/// ��
		/// </summary>
		public const string CycleMonth="CycleMonth";
		#endregion


	}
	/// <summary>
	/// ����ڵ�
	/// </summary> 
	public class Work : EntityOID
	{
		public static Work GenerDraftWork()
		{
			Work tn = new Work();

			if (tn.IsExit(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No)==false)
			{
				tn.MyWorkState=WorkState.None;
				tn.Sender=WebUser.No;
				tn.Insert();
			}
			else
			{
				tn.RetrieveByAttrAnd(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No);
			}

			return tn;			 
		}
		#region ��չ ����
		/// <summary>
		/// ���Ļظ��ڵ�
		/// </summary>
		public ReWork HisReWork
		{
			get
			{
				ReWork nd = new ReWork();
				nd.OID=this.OID;
				if (nd.IsExit(WorkAttr.OID, this.OID)==false)
				{
					nd.InsertAsOID(this.OID);
					
				}
				else
				{
					nd.Retrieve();
					return nd;
				}			 

				nd.Title="��:"+this.Title;
				//nd.Docs="����"+this.SenderText+": \n\n  ������ġ�"+this.Title+"������������������: \n  1��\n  2��     \n\n "+WebUser.Name+"\n"+DataType.CurrentDataTimeCN +"\n------------------ ������ԭ�� -------------------\n����:"+this.Title+"\nʱ��:"+this.DateTimeOfTaskStart+"\n\n"+this.Docs;
				nd.Docs="����"+this.SenderText+": \n\n  ������ġ�"+this.Title+"������������������: \n  1��\n  2��     \n\n "+WebUser.Name+"\n"+DataType.CurrentDataTimeCN;

				nd.ParentID=this.ParentID;
				nd.Reer=this.Executer;
				nd.ReActionDateTime=DataType.CurrentDataTime;
				nd.MyReWorkState=ReWorkState.None; 
				nd.Accepter=this.Sender; 
				nd.Update();
				return nd;
			}
		}
		/// <summary>
		/// �����˻ؽڵ�
		/// </summary>
		public ReturnWork HisReturnWork
		{
			get
			{
				ReturnWork nd = new ReturnWork();
				nd.OID=this.OID;
				if (nd.IsExit(WorkAttr.OID, this.OID)==false)
				{
					nd.InsertAsOID(this.OID);
				}
				else
				{
					nd.Retrieve();
					return nd;
				}
				

				nd.ParentID=this.ParentID;
				//nd.ReturnReason=reason;
				nd.Returner=this.Executer;
				nd.ReturnActionDateTime=DataType.CurrentDataTime;
				//nd.MyReturnWorkState=ReturnWorkState.None; 
			
				nd.Accepter=this.Sender; 
				nd.Update();

				return nd;
			}
		}
		/// <summary>
		/// ���ĸ��ڵ�
		/// </summary>
		public Work HisParentNode
		{
			get
			{
				if (this.ParentID==0)
					return this;

				return new Work(this.ParentID);
			}
		}

		/// <summary>
		/// ������һ���ڵ�
		/// </summary>
		public Works HisNextChildNodes
		{
			get
			{
				Works tns  = new Works();
				QueryObject qo = new QueryObject(tns);
				qo.AddWhere(WorkAttr.ParentID,this.OID);
				qo.DoQuery();
				return tns;
			}
		}
		#endregion

		#region �������ԡ�
		/// <summary>
		/// �����̶�
		/// </summary>
		public int PRI 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.PRI);
			}
			set
			{
				SetValByKey(WorkAttr.PRI,value);
			}
		}
		/// <summary>
		/// �Ƿ��Ķ���ִ
		/// </summary>
		public bool IsRe 
		{
			get
			{
				return this.GetValBooleanByKey(WorkAttr.IsRe);
			}
			set
			{
				SetValByKey(WorkAttr.IsRe,value);
			}
		}
		/// <summary>
		/// ���ڵ�
		/// </summary>
		public int ParentID 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.ParentID);
			}
			set
			{
				SetValByKey(WorkAttr.ParentID,value);
			}
		}
		 
		/// <summary>
		/// ����
		/// </summary>
		public int Step 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.Step);
			}
			set
			{
				SetValByKey(WorkAttr.Step,value);
			}
		}
		/// <summary>
		/// ���˷�
		/// </summary>
		public int CentOfCheck
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfCheck);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfCheck,value);
			}
		}
		/// <summary>
		/// ִ����
		/// </summary>
		public string Executer 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Executer);
			}
			set
			{
				SetValByKey(WorkAttr.Executer,value);
			}
		}
		/// <summary>
		/// ִ����
		/// </summary>
		public string ExecuterText
		{
			get
			{
				return this.GetValRefTextByKey(WorkAttr.Executer);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Title 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Title);
			}
			set
			{
				SetValByKey(WorkAttr.Title,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string DocsHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(WorkAttr.Docs);
			}
			
		}
		public string Docs
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Docs);
			}
			set
			{
				SetValByKey(WorkAttr.Docs,value);
			}
		}
		/// <summary>
		/// Emps
		/// </summary>
		public string EmpStrs
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Emps);
			}
			set
			{
				SetValByKey(WorkAttr.Emps,value);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string Sender 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Sender);
			}
			set
			{
				SetValByKey(WorkAttr.Sender,value);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string SenderText
		{
			get
			{
				return this.GetValRefTextByKey(WorkAttr.Sender);
			}
		}
//		/// <summary>
//		/// ����ʱ��
//		/// </summary>
//		public string DateTimeOfAccept_del
//		{
//			get
//			{
//				return this.GetValStringByKey(WorkAttr.DateTimeOfAccept);
//			}
//			set
//			{
//				SetValByKey(WorkAttr.DateTimeOfAccept,value);
//			}
//		}
//		public string DateOfAccept
//		{
//			get
//			{
//				return DataType.ParseSysDateTime2SysDate(this.DateTimeOfAccept);
//			}
//		}
//		 
//		/// <summary>
//		/// DateTimeOfAccept_S
//		/// </summary>
//		public DateTime DateTimeOfAccept_S
//		{
//			get
//			{
//				return DataType.ParseSysDate2DateTime(this.DateTimeOfAccept);
//			}
//		}
		/// <summary>
		/// ����
		/// </summary>
		public string MyWorkStateOpStr
		{
			get
			{
				if (WebUser.No==this.Sender && this.ParentID==0)
				{
					/* ������Ǵ˹����ķ����� */					  
					return this.MyWorkStateTextImg+"<a href=\"javascript:TaskTree('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ this.Title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID)+"�����"+this.EmpStrs;
				}

				string title=this.Title;
				switch(this.MyWorkState)
				{
					case WorkState.ReOver: //����Ѿ��Ͽ�
					case WorkState.Re: //����Ѿ��Ͽ�
					case WorkState.Returning: //����Ѿ��Ͽ�
					case WorkState.ReturnOver: //����Ѿ��Ͽ�
					case WorkState.CallBack: //����Ѿ��Ͽ�
						title="<strike>"+title+"</strike>";
						break;
					default:
						break;
				}
				return this.MyWorkStateTextImg+"<b>������:</b>"+this.SenderText+"<b>ִ����:</b>"+this.ExecuterText+"<a href=\"javascript:Task('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID);
			}
		}
		public string MyWorkStateOpStrShort
		{
			get
			{
				if (WebUser.No==this.Sender && this.ParentID==0)
				{
					/* ������Ǵ˹����ķ����� */					  
					return "<a href=\"javascript:TaskTree('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ this.Title +"</a>";
				}

				string title=this.Title;
				switch(this.MyWorkState)
				{
					case WorkState.ReOver: //����Ѿ��Ͽ�
					case WorkState.Re: //����Ѿ��Ͽ�
					case WorkState.Returning: //����Ѿ��Ͽ�
					case WorkState.ReturnOver: //����Ѿ��Ͽ�
					case WorkState.CallBack: //����Ѿ��Ͽ�
						title="<strike>"+title+"</strike>";
						break;
					default:
						break;
				}
				return "<a href=\"javascript:Task('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ title +"</a>";
			}
		}
		public string MyWorkStateTextImg
		{
			get
			{
				return "<img src='./images/MyWork.gif' border=0 /><img src='./images/"+this.MyWorkState+".gif' border=0 />"+this.MyWorkStateText;
			}
		}
		public string MyWorkStateImg
		{
			get
			{
				return "<img src='./images/"+this.MyWorkState+".gif' border=0 />";
			}
		}
		/// <summary>
		/// �ڵ�״̬
		/// </summary>
		public WorkState MyWorkState
		{
			get
			{
				return (WorkState)this.GetValIntByKey(WorkAttr.WorkState);
			}
			set
			{
				SetValByKey(WorkAttr.WorkState,(int)value);
			}
		}
		/// <summary>
		/// �ڵ�״̬ ����
		/// </summary>
		public string MyWorkStateText
		{
			get
			{
				return  GetValRefTextByKey(WorkAttr.WorkState);
			}
		}
		public string DateOfTaskStart
		{
			get
			{
				return DataType.ParseSysDateTime2SysDate(this.DateTimeOfTaskStart);
			}
		}

		/// <summary>
		/// ����ʼʱ��
		/// </summary>
		public string DateTimeOfTaskStart 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DateTimeOfTaskStart);
			}
			set
			{
				SetValByKey(WorkAttr.DateTimeOfTaskStart,value);
			}
		}
		/// <summary>
		/// ����ʼʱ��
		/// </summary>
		public DateTime DateTimeOfTaskStart_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DateTimeOfTaskStart);
			}
		}

		/// <summary>
		/// ���� ���� ʱ��
		/// </summary>
		public string DateTimeOfTaskEnd 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DateTimeOfTaskEnd);
			}
			set
			{
				SetValByKey(WorkAttr.DateTimeOfTaskEnd,value);
			}
		}
		/// <summary>
		/// ���� ���� ʱ��
		/// </summary>
		public DateTime DateTimeOfTaskEnd_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DateTimeOfTaskEnd);
			}
		}
		#endregion

		#region ��������
		/// <summary>
		/// ���˷�Χ
		/// </summary>
		public CheckScope MyCheckScope
		{
			get
			{
				return (CheckScope)this.GetValIntByKey(WorkAttr.CheckScope);
			}
			set
			{
				this.SetValByKey(WorkAttr.CheckScope,(int)value);
			}
		}
		/// <summary>
		/// ���˷�ʽ
		/// </summary>
		public CheckWay MyCheckWay
		{
			get
			{
				return (CheckWay)this.GetValIntByKey(WorkAttr.CheckWay);
			}
			set
			{
				this.SetValByKey(WorkAttr.CheckWay,(int)value);
			}
		}
		public string MyCheckWayText
		{
			get
			{
				return this.GetValRefTextByKey(WorkAttr.CheckWay);
			}
		}
		public string MyCheckWayTextExt
		{
			get
			{
				
				switch(this.MyCheckWay)
				{
					case CheckWay.UnSet:
						return this.MyCheckWayText;
						break;
					case CheckWay.ByDays:
					case CheckWay.ByOnce:
						return " <a href=\"javascript:WinOpen('WorkOption.aspx?RefOID="+this.OID+"','ParentID')\" >"+this.MyCheckWayText+"</a>";
						break;
					default:
						break;
				}
				return null;
			}
		}
		/// <summary>
		/// �۷ֳ߶�
		/// </summary>
		public int CentOfOnce 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfOnce);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfOnce,value);
			}
		}
		public int CentOfPerDay 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfPerDay);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfPerDay,value);
			}
		}
		/// <summary>
		/// ��߿۷�(�ڰ�������۷�ʱ������) 
		/// </summary>
		public int CentOfMax 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfMax);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfMax,value);
			}
		}
		#endregion 

		#region �¼�ѭ������
		/// <summary>
		/// ���˷�ʽ
		/// </summary>
		public CycleWay MyCycleWay
		{
			get
			{
				return (CycleWay)this.GetValIntByKey(WorkAttr.CycleWay);
			}
			set
			{
				this.SetValByKey(WorkAttr.CycleWay,(int)value);
			}
		}
		/// <summary>
		/// CycleWeek
		/// </summary>
		public string CycleWeek 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleWeek);
			}
			set
			{
				SetValByKey(WorkAttr.CycleWeek,value);
			}
		}
		/// <summary>
		/// CycleDay
		/// </summary>
		public string CycleDay 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleDay);
			}
			set
			{
				SetValByKey(WorkAttr.CycleDay,value);
			}
		}
		/// <summary>
		/// CycleYearDay
		/// </summary>
		public string CycleYearDay 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleYearDay);
			}
			set
			{
				SetValByKey(WorkAttr.CycleYearDay,value);
			}
		}
		/// <summary>
		/// CycleMonth
		/// </summary>
		public string CycleMonth 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleMonth);
			}
			set
			{
				SetValByKey(WorkAttr.CycleMonth,value);
			}
		}


		/// <summary>
		/// CycleWeekInt
		/// </summary>
		public int CycleWeekInt
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CycleWeek);
			}
			set
			{
				SetValByKey(WorkAttr.CycleWeek,value);
			}
		}
	 
		/// <summary>
		/// CycleMonthInt
		/// </summary>
		public int CycleMonthInt
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CycleMonth);
			}
			set
			{
				SetValByKey(WorkAttr.CycleMonth,value);
			}
		}
		public int CycleDayInt
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CycleDay);
			}
			set
			{
				SetValByKey(WorkAttr.CycleDay,value);
			}
		}
		#endregion 
		 
		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
//				UAC uac = new UAC();
//				uac.OpenAll();
				//return uac;
				return base.HisUAC;
			}
		}
		/// <summary>
		/// ����ڵ�
		/// </summary>
		public Work()
		{
		  
		}
		/// <summary>
		/// ����ڵ�
		/// </summary>
		/// <param name="_No">No</param>
		public Work(int oid):base(oid)
		{
			this.OID=oid;
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_Work");
				map.EnDesc="����ڵ�";
				map.Icon="./Images/Work_s.ico";

				map.AddTBIntPKOID();
				map.AddTBInt(WorkAttr.ParentID,0,"���ڵ�ID",true,true);
				map.AddTBInt(WorkAttr.Step,1,"����",true,true);
				map.AddBoolean(WorkAttr.IsRe,false,"�Ķ���ִ",true,false);
				map.AddDDLSysEnum(WorkAttr.WorkState, (int) WorkState.None ,"�ڵ�״̬",true,true);
				map.AddDDLSysEnum(WorkAttr.PRI,0,"���ȼ�",true,true);
				map.AddTBString(WorkAttr.Emps,null,"������",true,false,0,5000,15);
				map.AddTBString(WorkAttr.Title,null,"�������",true,false,0,500,15);	
				map.AddTBString(WorkAttr.Docs,null,"��������",true,false,0,5000,15);
				map.AddDDLEntitiesNoName(WorkAttr.Sender,null,"�����´���", new EmpExts(),false );
				map.AddTBDateTime(WorkAttr.DateTimeOfAccept,"�����´�ʱ��",false,false );

				map.AddDDLEntitiesNoName(WorkAttr.Executer,null,"ִ����", new EmpExts(),false );

				map.AddTBDateTime(WorkAttr.DateTimeOfTaskStart,"����ʼʱ��",false,false );
				map.AddTBDateTime(WorkAttr.DateTimeOfTaskEnd,"�������ʱ��",false,false );


				map.AddDDLSysEnum(WorkAttr.CheckWay,(int)CheckWay.UnSet,"���˷�ʽ",true,true);
				map.AddDDLSysEnum(WorkAttr.CheckScope,(int)CheckScope.ToEmp,"���˷�Χ",true,true);

				map.AddTBInt(WorkAttr.CentOfOnce,5,"�۷ֳ߶�",true,true);
				map.AddTBInt(WorkAttr.CentOfPerDay,5,"�۷ֳ߶�",true,true);
				map.AddTBInt(WorkAttr.CentOfMax,10,"��߿۷�",true,true);

				map.AddTBInt(WorkAttr.CentOfCheck,10,"���˷�",true,true);

				map.AddDDLSysEnum(WorkAttr.CycleWay,(int)CycleWay.UnSet,"ѭ����ʽ",true,true);
				map.AddTBString(WorkAttr.CycleWeek,"1","Weeks",true,false,0,5000,15);
				map.AddTBString(WorkAttr.CycleDay,"1","Days",true,false,0,5000,15);
				map.AddTBString(WorkAttr.CycleMonth,"1","Months",true,false,0,5000,15);
				map.AddTBString(WorkAttr.CycleYearDay,"1","Days",true,false,0,5000,15);


				
				map.AddSearchAttr(WorkAttr.CheckWay);
				map.AttrsOfSearch.AddHidden(WorkAttr.Sender,"=",Web.WebUser.No);
				map.AttrsOfSearch.AddFromTo("��ʼ����",WorkAttr.DateTimeOfTaskStart,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);

 
 
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
//			if (this.ParentID==0)
//				this.DateTimeOfAccept=this.DateOfTaskStart;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		 

		/// <summary>
		/// ִ��ǩ��
		/// </summary>
		public void DoRead()
		{
			
			if (this.MyWorkState == WorkState.Allot )
				return;
			 
			this.Update( WorkAttr.WorkState,(int)WorkState.Read);
			 
		}
		/// <summary>
		/// ִ���ջ�����
		/// </summary>
		public void DoTakeBack()
		{
			this.Delete();
		}
		/// <summary>
		/// ִ�ж�ȡ�˻ؽڵ�
		/// </summary>
		public void DoReadReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;
			rn.Update(ReturnWorkAttr.ReturnWorkState, (int)ReturnWorkState.Read);
			rn.Update();
		}
		/// <summary>
		/// ִ�ж�ȡ�ظ��ڵ�
		/// </summary>
		public void DoReadReWork()
		{
			ReWork rn =this.HisReWork;
			rn.Update(ReWorkAttr.ReWorkState, (int)ReWorkState.Read);
			rn.Update();
		}
		/// <summary>
		/// ִ���˻�
		/// </summary>
		/// <param name="reason">�˻�ԭ��</param>
		public ReturnWork DoReturn(string reason)
		{
			ReturnWork nd = new ReturnWork();
			if (nd.IsExit(WorkAttr.OID, this.OID)==false)
				nd.InsertAsOID(this.OID);

			nd.ParentID=this.ParentID;
			nd.ReturnReason=reason;
			nd.Returner=this.Executer;
			nd.ReturnActionDateTime=DataType.CurrentDataTime;
			nd.MyReturnWorkState=ReturnWorkState.UnRead; //����δ��.
			
			nd.Accepter=this.Sender; 
			nd.Update();			 
			this.Update(WorkAttr.WorkState,(int)WorkState.Returning);  // ���õ�ǰ��״̬Ϊ�˻ء�
			return nd;
		}
		/// <summary>
		/// ִ�г����˻�
		/// </summary>
		public void DoReturnOfRecall()
		{
			/* ִ�г����˻� */
			ReturnWork rn = this.HisReturnWork;			 
			rn.Update(ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.CallBack  );

			this.Update(WorkAttr.WorkState,(int)WorkState.Read); //��������ڵ�Ϊ�Ѿ��Ķ�
		}
		/// <summary>
		/// ִ�лظ�
		/// </summary>
		public void DoRe(string title,string docs)
		{
			ReWork rn = this.HisReWork;
			rn.Title=title;
			rn.Docs=docs;
			rn.ReActionDateTime=DataType.CurrentDataTime;
			rn.MyReWorkState=ReWorkState.UnRead;
			rn.Update();

			this.MyWorkState=WorkState.Re;
			//this.Sender=WebUser.No;
			this.Update();
		}
		/// <summary>
		/// ִ�з���
		/// </summary>
		public void DoAllot()
		{
			this.MyWorkState=WorkState.Allot;
			this.Update();
		}
		/// <summary>
		/// ִ����ֹ����
		/// </summary>
		public void DoStopWork()
		{

		}

		#region �ظ�
		/// <summary>
		/// ִ��ִ��
		/// </summary>
		public void DoStop()
		{
			ReWork rn =this.HisReWork;			 
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.Stop);
		}
		/// <summary>
		/// ִ�в��Ͽ��˻�
		/// </summary>
		public void DoUnRatifyReWork()
		{
			ReWork rn =this.HisReWork;			 
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.UnRatify);
		}
		 
		/// <summary>
		/// ִ���Ͽ��˻�
		/// </summary>
		public void DoRatifyReWork()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.Ratify);

			this.Update( WorkAttr.WorkState,(int)WorkState.ReOver); // ����Ϊ�Ѿ��˻�״̬��
		}
		/// <summary>
		/// �����ظ�
		/// </summary>
		public void DoTakeBackRe()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.None);

			this.Update( WorkAttr.WorkState,(int)WorkState.Read ); // ����Ϊ�Ѿ��˻�״̬��
		}

		#endregion

		#region �˻�
		/// <summary>
		/// ִ�в��Ͽ��˻�
		/// </summary>
		public void DoUnRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;			 
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.UnRatify);
		}
		 
		/// <summary>
		/// ִ���Ͽ��˻�
		/// </summary>
		public void DoRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;			 
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.Ratify);

			this.Update( WorkAttr.WorkState,(int)WorkState.ReturnOver); // ����Ϊ�Ѿ��˻�״̬��
		}
		#endregion

		/// <summary>
		/// ִ�з��͡�
		/// </summary>
		public Works DoSend()
		{
			try
			{
				//���ȼ��������Ƿ���ȷ��
				string empsstrs= PubClass.CheckEmps(this.EmpStrs); // ������Ա�ַ�����
				if (this.ParentID==0)
				{
					/*�����ǰ��������ķ����� */
					this.Executer=WebUser.No;
					this.Sender=WebUser.No;
					//this.FK_Task=this.OID;
				}


				// ȡ����ǰ�ڵ�ĸ���
				SysFileManagers fils = this.HisSysFileManagers;
				if (fils.Count!=0)
				{
					// ɾ��ԭ�����͵���Ϣ����С�/ 
					Works nds  =new Works();
					nds.SearchByParentID(this.OID);
					foreach(Work tn in nds)
					{
						SysFileManagers fs = tn.HisSysFileManagers;
						foreach(SysFileManager f in fs)
							f.DirectDelete(); // ���ַ�ʽ���Ա���ɾ���ļ���
					}
				}
				 
				DBAccess.RunSQL("DELETE TA_Work WHERE  ParentID="+this.OID );

				/// ���ڷ��ؽ����
				Works tns = new Works();
				// ɾ��ԭ���ĸ���
				string[] strs=empsstrs.Split(',');
				foreach(string str in strs)
				{
					if (str=="")
						continue;
					if (str==WebUser.No)
						continue;

					Work tn = new Work();
					tn.ParentID=this.OID; // ���ĸ��ڵ�������Ľڵ㡣
					//tn.FK_Task=this.FK_Task;
					tn.MyWorkState=WorkState.UnRead;
					tn.Sender=this.Executer; // ������
					tn.Executer=str;
					tn.DateTimeOfTaskStart=this.DateTimeOfTaskStart;  // �ӽڵ�Ľ���ʱ���Ǹ��ڵ�ķ���ʱ�䡣
					tn.DateTimeOfTaskEnd=tn.DateTimeOfTaskEnd;        // �ӽڵ�Ľ���ʱ���Ǹ��ڵ�ķ���ʱ�䡣
					tn.Title = this.Title;
					tn.Docs=this.Docs;
					tn.Step=this.Step+1; //����.
					tn.Insert();

					// ������Ĺ����Ľ����߼Ӹ���
					foreach(SysFileManager f in fils)
					{
						f.Copy();
						f.RefTable=tn.ToString();
						f.RefKey=tn.OID;
						f.Recorder=WebUser.No;
						f.Insert();
					}
					tns.AddEntity(tn); // ���ӵ������������ȥ��
				}

				// �ж��Ƿ��н����ˡ�
				if (tns.Count==0)
					throw new Exception("@��û��ѡ��Ҫ���͵��ˡ�");

				// ���÷���״̬��
				//this.MyWorkState=WorkState.Allot;
				
 
				this.Update();
				return tns; // ���ؽ��
			}
			catch(Exception ex)
			{
				// ������ͳ��ִ���󣬾�ɾ���Ѿ����͵�����
				DBAccess.RunSQL("DELETE TA_Work WHERE  ParentID="+this.OID ); 
				throw new Exception("�����ڼ�������´���:"+ex.Message);
			}
		}
		 
		/// <summary>
		/// ��������
		/// </summary>
		/// <returns></returns>
		public int TimeOutDays()
		{
			TimeSpan ts;
			DateTime dtfrom;
			switch(this.MyWorkState)
			{
				case WorkState.Allot:
				case WorkState.Read:
				case WorkState.UnRead:
					dtfrom=DateTime.Now; // ������Ѿ����䣬��ȡ��δ��ȡ��״̬���յ�ǰ������Ϊ�������ڡ�
					break;
				case WorkState.Re:
				case WorkState.ReOver:
				case WorkState.CallBack: // �ظ���ֹ
					dtfrom=this.HisReWork.ReActionDateTime_S; //����ڻظ����ظ��Ͽɺ����ڰ��ظ��˻�����ڼ��㡣
					break;
				case WorkState.Returning:
				case WorkState.ReturnOver:
					dtfrom=this.HisReWork.ReActionDateTime_S; //������˻أ��˻��Ͽɺ����ڰ��˻��˻�����ڼ��㡣
					break;
				default:
					throw new Exception("û���漰���������");
			}

			ts=dtfrom-this.DateTimeOfTaskEnd_S;
			return ts.Days;
		}
		/// <summary>
		/// �������˵ķ�
		/// </summary>
		public void GenerCheckCent()
		{
			//�����ǰ�ǵ�һ���ڵ㣬�Ͳ����ˡ�
			if (this.ParentID==0) 
			{
				this.CentOfCheck=0;
				return; 
			}
			// ���û�����ÿ��˾Ͳ����ˡ�
			if (this.MyCheckWay==CheckWay.UnSet)
			{
				this.CentOfCheck=0;
				return; 
			}


			//Work tn =this.HisParentNode;
			switch(this.MyWorkState)
			{
				case WorkState.CallBack: // �ջ��ˡ�
					if (this.MyCheckWay==CheckWay.ByDays)
						this.CentOfCheck=this.CentOfMax; /* ����ǰ����������ˡ�*/
					else
						this.CentOfCheck=this.CentOfOnce;
					break;
				default:					 
					if (this.MyCheckWay==CheckWay.ByDays)
					{
						int cent=this.TimeOutDays()*this.CentOfPerDay;
						if (cent>this.CentOfMax)
						{
							cent=this.CentOfMax;
						}
						this.CentOfCheck=cent; /* ����ǰ����������ˡ�*/
					}
					else if (this.MyCheckWay==CheckWay.ByOnce)
					{
						if (this.TimeOutDays() >=1)
						{
							/*������ھͿ۷�*/
							this.CentOfCheck=this.CentOfOnce;
						}
					}
					break;
			}

			this.Update(WorkAttr.CentOfCheck, this.CentOfCheck);
		}

	}
	/// <summary>
	/// ����ڵ�s
	/// </summary> 
	public class Works: Entities
	{
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Work();
			}
		}
		/// <summary>
		/// Works
		/// </summary>
		public Works()
		{

		}
		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="emp">��Ա</param>
		/// <param name="ny">����</param>
		public Works(string ExecuterEmp,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkAttr.DateTimeOfTaskStart, " like ", ny+"%");
			qo.addAnd();
			qo.AddWhere(WorkAttr.Executer,ExecuterEmp);
			qo.addAnd();
			qo.AddWhere(WorkAttr.WorkState,">",0);
  			qo.DoQuery();
		}
		 
		/// <summary>
		/// Works
		/// </summary>
		/// <param name="parentNodeID"></param>
		public int SearchByParentID(int parentNodeID)
		{
			if (parentNodeID==0)
				return 0 ;

			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkAttr.ParentID, parentNodeID);
			return qo.DoQuery();
		}
		 
				   
				  
				  
	}
}
 
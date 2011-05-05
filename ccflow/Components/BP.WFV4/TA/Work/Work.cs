using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Sys;
using BP.Port;

namespace BP.TA
{
 
	/// <summary>
	/// ���˷�ʽ
	/// </summary>
	public enum CheckWay
	{
		/// <summary>
		/// û������
		/// </summary>
		UnSet,
		/// <summary>
		/// һ�����趨
		/// </summary>
		ByOnce,
		/// <summary>
		/// �����趨
		/// </summary>
		ByDays
	}
	/// <summary>
	/// ����״̬
	/// </summary>
    public enum WorkState
    {
        /// <summary>
        /// ��(�ݸ�)
        /// </summary>
        None,
        /// <summary>
        /// ����(ִ�з��ͺ�)
        /// </summary>
        Runing,
        /// <summary>
        /// ���(��������������,���Ѿ��ظ���,�����Ѿ��Ͽ�.)
        /// </summary>
        Over
    }
	/// <summary>
	/// �����ڵ�����
	/// </summary>
	public class WorkAttr:EntityOIDAttr
	{
        /// <summary>
        /// ToEmpStrs
        /// </summary>
        public const string ToEmpStrs = "ToEmpStrs";
        /// <summary>
        /// ��������
        /// </summary>
        public const string RDT = "RDT";
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
		/// ����״̬
		/// </summary>
		public const string WorkState="WorkState"; 	
		/// <summary>
		/// ���ȼ�
		/// </summary>
		public const string PRI="PRI"; 
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
        public const string Doc = "Doc"; 
		/// <summary>
		/// ToEmps
		/// </summary>
		public const string ToEmps="ToEmps"; 	
		/// <summary>
		/// ������
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// Ҫ��ʼʱ��
		/// </summary>
		public const string DTOfStart="DTOfStart";
		/// <summary>
		/// Ҫ�����ʱ��
		/// </summary>
		public const string DTOfEnd="DTOfEnd";

        public const string FK_WorkTemplate = "FK_WorkTemplate";


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

		#region ��������
		/// <summary>
		/// ִ���˸���
		/// </summary>
		public const string NumOfUnRead="NumOfUnRead";
		/// <summary>
		/// ��ȡ�˸���
		/// </summary>
		public const string NumOfRead="NumOfRead";
		/// <summary>
		/// �ظ���
		/// </summary>
		public const string NumOfReing="NumOfReing";
		/// <summary>
		/// �˻���
		/// </summary>
		public const string NumOfReturning="NumOfReturning";
		/// <summary>
		/// �ظ���ɸ���
		/// </summary>
		public const string NumOfOverByRe="NumOfOverByRe";
		/// <summary>
		/// �˻���ɸ���
		/// </summary>
		public const string NumOfOverByReturn="NumOfOverByReturn";
		/// <summary>
		/// ��������
		/// </summary>
		public const string AdjunctNum="AdjunctNum";
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
	/// �����ڵ�
	/// </summary> 
	public class Work : EntityOID
	{
		public static int GetMyReturnWorkNum
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_ReturnWork WHERE ReturnWorkState="+(int)ReturnWorkState.Sending+" AND Sender='"+WebUser.No+"'";
              //  throw new Exception(sql);
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		public static int GetMyReWorkNum
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_ReWork WHERE ReWorkState="+(int)ReWorkState.Sending+" AND Sender='"+WebUser.No+"'";
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
      
        /// <summary>
        /// �ȴ������
        /// </summary>
        public static int GetMyWorkNum
        {
            get
            {
                string sql = "SELECT COUNT(*) FROM TA_WorkDtl WHERE ( WorkDtlState=" + (int)WorkDtlState.Read + " OR WorkDtlState=" + (int)WorkDtlState.UnRead + " ) AND Executer='" + WebUser.No + "'";
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }

        public static int GetMyWorkNumOfHistory
        {
            get
            {
                string sql = "SELECT COUNT(*) FROM TA_WorkDtl WHERE   Executer='" + WebUser.No + "' AND ( WorkDtlState=" + (int)WorkDtlState.OverByRe + " OR WorkDtlState=" + (int)WorkDtlState.OverByRead + "  OR WorkDtlState=" + (int)WorkDtlState.OverByReturn + "  ) ";
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }

        public static int GetMyWorkNumOfDeal
        {
            get
            {
                string sql = "SELECT COUNT(*) FROM TA_WorkDtl WHERE   Executer='" + WebUser.No + "' AND ( WorkDtlState=" + (int)WorkDtlState.Read + " OR WorkDtlState=" + (int)WorkDtlState.UnRead + " ) ";
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }

		/// <summary>
		/// ����һ���ݸ幤��
		/// </summary>
		/// <returns></returns>
		public static Work GenerDraftWork()
		{
			Work tn = new Work();
			if (tn.IsExit(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No)==false)
			{
				tn.WorkState=WorkState.None;
				tn.Sender=WebUser.No;
				tn.Insert();
			}
			else
			{
				tn.RetrieveByAttrAnd(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No);
			}
			return tn;
		}
		/// <summary>
		/// ����һ���ݸ幤��
		/// </summary>
		/// <returns></returns>
		public static Work GenerParentWork(int dtlWorkId)
		{
			Work tn = new Work();
			WorkDtl dtl = new WorkDtl(dtlWorkId);
			if (tn.IsExit(WorkAttr.ParentID, dtl.ParentID )==false)
			{
				Work wk = dtl.HisWork;
				tn.WorkState=WorkState.None;
				tn.Sender=WebUser.No;
				tn.Title="FW:"+wk.Title;
				tn.Doc="\n\n\n---- ��������һ�ڵ�Ĺ������� -----\n"+wk.Doc;
				tn.ParentID=wk.OID;
				tn.Step=wk.Step+1;
				tn.Insert();
			}
			else
			{
				tn.RetrieveByAttr(WorkAttr.ParentID, dtlWorkId );
			}
			return tn;
		}

		#region ��չ ����
		#endregion

		#region �������ԡ�
		/// <summary>
		/// �Ƿ��ǽ����ڵ�
		/// </summary>
		public bool IsEndNode
		{
			get
			{
				return false;
			}
		}
		/// <summary>
		/// �Ƿ��ǿ�ʼ�ڵ�
		/// </summary>
		public bool IsStartNode
		{
			get
			{
				if (this.ParentID==0)
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// �Ƿ����м�ڵ�
		/// </summary>
		public bool IsMiddle
		{
			get
			{
				if (this.IsStartNode)
					return false;
				if (this.IsEndNode)
					return false;

				return true;
			}
		}
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
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.RDT);
            }
            set
            {
                SetValByKey(WorkAttr.RDT, value);
            }
        }
		public string IsReText
		{
			get
			{
				if (this.IsRe)
					return "��";
				else
					return "��";
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

        public string FK_WorkTemplate
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.FK_WorkTemplate);
            }
            set
            {
                SetValByKey(WorkAttr.FK_WorkTemplate, value);
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
		public string DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(WorkAttr.Doc);
			}
			
		}
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Doc);
			}
			set
			{
				SetValByKey(WorkAttr.Doc,value);
			}
		}
        public string ToEmpStrs
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.ToEmpStrs);
            }
            set
            {
                SetValByKey(WorkAttr.ToEmpStrs, value);
            }
        }
		/// <summary>
		/// Emps
		/// </summary>
		public string ToEmps
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.ToEmps );
			}
			set
			{
				string strs=value;
				if (strs.Substring(0,1)==",")
					strs=strs.Substring(1);

				SetValByKey(WorkAttr.ToEmps,strs);
                this.ToEmpStrs = Web.WebUser.Tag;
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
		/// ��������
		/// </summary>
		public int AdjunctNum 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.AdjunctNum);
			}
			set
			{
				SetValByKey(WorkAttr.AdjunctNum,value);
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
//		public string DTOfAccept_del
//		{
//			get
//			{
//				return this.GetValStringByKey(WorkAttr.DTOfAccept);
//			}
//			set
//			{
//				SetValByKey(WorkAttr.DTOfAccept,value);
//			}
//		}
//		public string DateOfAccept
//		{
//			get
//			{
//				return DataType.ParseSysDateTime2SysDate(this.DTOfAccept);
//			}
//		}
//		 
//		/// <summary>
//		/// DTOfAccept_S
//		/// </summary>
//		public DateTime DTOfAccept_S
//		{
//			get
//			{
//				return DataType.ParseSysDate2DateTime(this.DTOfAccept);
//			}
//		}
		/// <summary>
		/// ����
		/// </summary>
		public string WorkStateOpStr
		{
			get
			{
				return null;
				//				if (WebUser.No==this.Sender && this.ParentID==0)
				//				{
				//					/* ������Ǵ˹����ķ����� */					  
				//					return this.WorkStateTextImg+"<a href=\"javascript:TrackTree('"+this.OID+"','"+(int)BP.TA.ActionType.ViewNode+"')\" >"+ this.Title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID)+"�����"+this.ToEmps;
				//				}
				//
				//				string title=this.Title;
				//				switch(this.WorkState)
				//				{
				//					case WorkState.ReOver: //����Ѿ��Ͽ�
				//					case WorkState.Re: //����Ѿ��Ͽ�
				//					case WorkState.Returning: //����Ѿ��Ͽ�
				//					case WorkState.ReturnOver: //����Ѿ��Ͽ�
				//					case WorkState.CallBack: //����Ѿ��Ͽ�
				//						//title="<strike>"+title+"</strike>";
				//						break;
				//					default:
				//						break;
				//				}
				//				return this.WorkStateTextImg+"<b>������:</b>"+this.SenderText+"<b>ִ����:</b>"+this.ExecuterText+"<a href=\"javascript:Task('"+this.OID+"','"+(int)BP.TA.ActionType.ViewNode+"')\" >"+ title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID);
			}
		}
		/// <summary>
		/// �ڵ�״̬
		/// </summary>
		public WorkState WorkState
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
		public string WorkStateImg
		{
			get
			{
                return "./Images/" + this.WorkState.ToString() + ".gif";
			}
		}
		public string WorkStateImgExt
		{
			get
			{
				return  "<img src='"+this.WorkStateImg+"' border=0 />"+this.WorkStateText;
			}
		}
		/// <summary>
		/// �ڵ�״̬ ����
		/// </summary>
		public string WorkStateText
		{
			get
			{
				return  GetValRefTextByKey(WorkAttr.WorkState);
			}
		}
		/// <summary>
		/// ����ʼʱ��
		/// </summary>
		public string DTOfStart 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DTOfStart);
			}
			set
			{
				SetValByKey(WorkAttr.DTOfStart,value);
                SetValByKey(WorkAttr.RDT, DataType.CurrentDataTime);
			}
		}
		/// <summary>
		/// ����ʼʱ��
		/// </summary>
		public DateTime DTOfStart_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DTOfStart);
			}
		}
		/// <summary>
		/// ���� ���� ʱ��
		/// </summary>
		public string DTOfEnd 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DTOfEnd);
			}
			set
			{
				SetValByKey(WorkAttr.DTOfEnd,value);
			}
		}
		public DateTime DTOfEnd_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DTOfEnd);
			}
		}
		/// <summary>
		/// ���ڵ�ʱ���.
		/// </summary>
		public TimeSpan DateTimeOfOverTime
		{
			get
			{
				TimeSpan ts=this.DTOfStart_S - this.DTOfEnd_S;
				return ts;
			}
		}
		#endregion

		#region ��������
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
				return this.MyCheckWayText;
                //switch (this.MyCheckWay)
                //{
                //    case CheckWay.UnSet:
                //        return this.MyCheckWayText;
                //    case CheckWay.ByDays:
                //    case CheckWay.ByOnce:
                //        return " <a href=\"javascript:WinOpen('WorkOption.aspx?RefOID=" + this.OID + "','ParentID')\" >" + this.MyCheckWayText + "</a>";
                //    default:
                //        break;
                //}
                //return null;
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
 				return base.HisUAC;
			}
		}
		/// <summary>
		/// �����ڵ�
		/// </summary>
		public Work()
		{
		  
		}
		/// <summary>
		/// �����ڵ�
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
				map.EnDesc="�����ڵ�";
                map.Icon = "./Images/Work_s.gif";

				map.AddTBIntPKOID();
				map.AddTBInt(WorkAttr.ParentID,0,"���ڵ�ID",true,true);
				map.AddTBInt(WorkAttr.Step,1,"����",true,true);
				map.AddBoolean(WorkAttr.IsRe,false,"�Ƿ��Ķ���ִ",true,false);

                map.AddDDLSysEnum(WorkAttr.WorkState, (int)WorkState.None, "�����˽ڵ�״̬", true, true, "WorkState", "@0=��(�ݸ�)@1=����@2=���");
                map.AddDDLSysEnum(TaskAttr.PRI, 0, "���ȼ�", false, true, TaskAttr.SharingType, "@0=��@1=��@2=��");

                map.AddTBString(WorkAttr.ToEmps, null, "������", true, false, 0, 3000, 15);
                map.AddTBString(WorkAttr.ToEmpStrs, null, "ToEmpStrs", true, false, 0, 3000, 15);

				map.AddTBString(WorkAttr.Title,null,"�������",true,false,0,500,15);

				map.AddTBString(WorkAttr.Doc,null,"��������",true,false,0,4000,15);

                map.AddTBDateTime(WorkAttr.RDT, "��������", false, false);


				map.AddDDLEntities(WorkAttr.Sender,null,"�����´���", new Emps(),false );
				map.AddTBDateTime(WorkAttr.DTOfStart,"����ʼʱ��",false,false );
				map.AddTBDateTime(WorkAttr.DTOfEnd,"����ʱ��",false,false );

                map.AddDDLSysEnum(WorkAttr.CheckWay, (int)CheckWay.UnSet, "���˷�ʽ", true, true, TaskAttr.SharingType, "@0=δ����@1=����@2=����");
				map.AddTBInt(WorkAttr.CentOfOnce,5,"һ���Կ۷�",true,true);
				map.AddTBInt(WorkAttr.CentOfPerDay,2,"�۷ֳ߶�",true,true);
				map.AddTBInt(WorkAttr.CentOfMax,10,"��߿۷�",true,true);
				map.AddTBInt(WorkAttr.CentOfCheck,10,"���˷�",true,true);

                map.AddDDLSysEnum(WorkAttr.CycleWay, (int)CycleWay.UnSet, "ѭ����ʽ", true, true, "CycleWay", "@0=δ����@1=����@2=����@3=����");
				map.AddTBString(WorkAttr.CycleWeek,"1","Weeks",true,false,0,50,15);
				map.AddTBString(WorkAttr.CycleDay,"1","Days",true,false,0,50,15);
				map.AddTBString(WorkAttr.CycleMonth,"1","Months",true,false,0,50,15);
				map.AddTBString(WorkAttr.CycleYearDay,"1","Days",true,false,0,50,15);


				map.AddTBInt(WorkAttr.AdjunctNum,0,"��������",true,true);

				// ��������
				map.AddTBInt(WorkAttr.CentOfMax,10,"��߿۷�",true,true);
                map.AddTBString(WorkAttr.FK_WorkTemplate, null, "ģ��ID", true, false, 0, 500, 15);

              //  map.AddDDLEntities(WorkAttr.FK_WorkTemplate, "99", "ģ��ID", new WorkTemplates(), false);
                

				//map.AddSearchAttr(WorkAttr.CheckWay);
				//map.AttrsOfSearch.AddHidden(WorkAttr.Sender,"=",Web.WebUser.No);
				//map.AttrsOfSearch.AddFromTo("��ʼ����",WorkAttr.DTOfStart,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//			if (this.ParentID==0)
			//				this.DTOfAccept=this.DateOfTaskStart;
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

		#region ��������ͳ��
		/// <summary>
		/// û���Ķ��Ĺ�������
		/// </summary>
		public int NumOfUnRead
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.UnRead);
			}
		}
		public int NumOfRead
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.Read);
			}
		}
		public int NumOfReing
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.DoReing);
			}
		}
		public int NumOfReturning
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.DoReturning);
			}
		}
		public int NumOfOverByRe
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByRe);
			}
		}
		public int NumOfOverByRead
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByRead);
			}
		}
		public int NumOfOverByReturn
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByReturn);
			}
		}
		public int NumOfComplete
		{
			get
			{
				return this.NumOfOverByRe+this.NumOfOverByReturn;
				//return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByReturn);
			}
		}
		/// <summary>
		/// ��������ʡ�
		/// </summary>
		public string RateOfComplete
		{
			get
			{
				if (this.HisWorkDtls.Count==0)
					return "0";

				int num=this.NumOfOverByRe+this.NumOfOverByReturn+this.NumOfOverByRead;
				decimal rate = decimal.Parse(num.ToString())  / decimal.Parse( this.HisWorkDtls.Count.ToString());
				return rate.ToString("0.00%");
			}
		}
		#endregion



		private WorkDtls _WorkDtls=null;
		public WorkDtls HisWorkDtls
		{
			get
			{
				if (_WorkDtls==null)
				{
					WorkDtls dtls = new WorkDtls();
					dtls.Retrieve(WorkDtlAttr.ParentID, this.OID);
					_WorkDtls= dtls;
				}
				return _WorkDtls;
			}
		}
		/// <summary>
		/// ���Ľڵ㡣
		/// </summary>
		public Work HisParentWork
		{
			get
			{
				return new Work(this.ParentID);
			}
		}
		public Works HisCharedWork
		{
			get
			{
				Works wks = new Works();
				QueryObject qo = new QueryObject(wks);
				qo.AddWhere(WorkAttr.ParentID,this.OID);
				qo.DoQuery();
				return wks;
			}
		}
		/// <summary>
		/// ��ÿһ�����乤��ִ����Ϻ�͵�������
		/// ��������Ҫ��Ŀ���ǣ��ж����幤���Ƿ���������⡣
		/// </summary>
        public void DoSettleAccounts()
        {
            WorkDtls dtls = new WorkDtls();
            QueryObject qo = new QueryObject(dtls);
            qo.AddWhere(WorkDtlAttr.ParentID, this.OID);
            qo.DoQuery();


            foreach (WorkDtl dtl in dtls)
            {
                switch (dtl.WorkDtlState)
                {
                    case WorkDtlState.UnRead:
                    case WorkDtlState.Read:
                    case WorkDtlState.DoReing:
                    case WorkDtlState.DoReturning:
                        return;
                    default:
                        break;
                }
            }

            this.WorkState = WorkState.Over;
            this.Update();
            return;
        }
		/// <summary>
		/// ִ����ֹ����
		/// </summary>
		public void DoStopWork()
		{

		}
		/// <summary>
		/// ִ�з��͡�
		/// </summary>
		public WorkDtls DoSend()
		{
			//�ж�һ���Ƿ���Է��乤����
			WorkDtls dtls = new WorkDtls();
			dtls.Retrieve(WorkDtlAttr.ParentID,this.OID, WorkDtlAttr.WorkDtlState, (int)WorkDtlState.Read);
			if (dtls.Count > 0)
				throw new Exception("����ǰ����Ĺ����Ѿ���ʼ�������������ڷ��乤����");

            this.RDT = DataType.CurrentDataTime;

			dtls.Clear();
			try
			{

				//���ȼ��������Ƿ���ȷ��
				string empsstrs= PubClass.CheckEmps(this.ToEmps); // ������Ա�ַ�����
                this.ToEmpStrs = BP.Web.WebUser.Tag;

				if (this.ParentID==0)
				{
					/*�����ǰ��������ķ����� */
					this.Sender=WebUser.No;
				}
				 
				// ɾ����ǰ����Ĺ�����
				DBAccess.RunSQL("DELETE FROM TA_WorkDtl WHERE  ParentID="+this.OID );

				/// ���ڷ��ؽ����
				// ɾ��ԭ���ĸ���
				string[] strs=empsstrs.Split(',');
				foreach(string str in strs)
				{
					if (str=="")
						continue;
					if (str==WebUser.No)
						continue;

					WorkDtl dtl = new WorkDtl();
					dtl.ParentID=this.OID; // ���ĸ��ڵ�������Ľڵ㡣
					dtl.WorkDtlState=WorkDtlState.UnRead;
					dtl.Executer=str;
					dtl.DTOfActive=DataType.CurrentDataTime;  // �ӽڵ�Ľ���ʱ���Ǹ��ڵ�ķ���ʱ�䡣
                    dtl.DTOfSend = DataType.CurrentDataTime;  // �ӽڵ�Ľ���ʱ���Ǹ��ڵ�ķ���ʱ�䡣
					dtl.Insert();
					//dtl.Retrieve();
					dtls.AddEntity(dtl); // ���ӵ������������ȥ��
				}

				// �ж��Ƿ��н����ˡ�
				if (dtls.Count==0)
					throw new Exception("@��û��ѡ��Ҫ���͵��ˡ�");

				// ���÷���״̬��
				this.WorkState=WorkState.Runing;
				this.AdjunctNum = this.HisSysFileManagers.Count;
				this.Update();
				return dtls; // ���ؽ��
			}
			catch(Exception ex)
			{
				// ������ͳ��ִ���󣬾�ɾ���Ѿ����͵�����
				DBAccess.RunSQL("DELETE TA_WorkDtl WHERE  ParentID="+this.OID ); 
				throw new Exception("�����ڼ�������´���:"+ex.Message);
			}
		}
		 
		#region ���ڿ���
		/// <summary>
		/// ��������
		/// </summary>
		/// <returns></returns>
		public TimeSpan TimeOutDays()
		{
			TimeSpan ts =DateTime.Now - this.DTOfEnd_S;
			return ts;
		}
		/// <summary>
		/// ִ�������趨
		/// </summary>
		public void DoResetWorkState()
		{
			bool isEnd=true;
			WorkDtls dtls = this.HisWorkDtls;
			foreach(WorkDtl dtl in dtls)
			{
				if (dtl.WDS==WDS.Checking || dtl.WDS == WDS.UnComplete )
				{
					isEnd=false;
					break; /* �����һ��û����ɵģ�����˷���Ĺ�����û����ɡ�*/
				}
			}

			if (isEnd)
				if (this.WorkState == BP.TA.WorkState.Over)
					this.Update();
		}
		#endregion


	}
	/// <summary>
	/// �����ڵ�s
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
		public Works(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkAttr.DTOfStart, " like ", ny+"%" );
			qo.addAnd();
			qo.AddWhere(WorkAttr.Sender, userNo );
//			qo.addAnd();
//			qo.AddWhere(WorkAttr.WorkState, "<>" , 0 );

			qo.DoQuery();
		}
			 
	}
}
 
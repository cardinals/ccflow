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
	/// ����ڵ�����
	/// </summary>
	public class WorkExtAttr:WorkAttr
	{
		/// <summary>
		/// ��߿۷�
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		/// <summary>
		/// FK_Dept
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// FK_Station
		/// </summary>
		public const string FK_Station="FK_Station";
		/// <summary>
		/// FK_NY
		/// </summary>
		public const string FK_NY="FK_NY";
		/// <summary>
		/// FK_JD
		/// </summary>
		public const string FK_JD="FK_JD";
        public const string DateTimeOfAccept = "DateTimeOfAccept";
        public const string Executer = "Executer";
        public const string DateTimeOfTaskEnd = "DateTimeOfTaskEnd";
        public const string DateTimeOfTaskStart = "DateTimeOfTaskStart";

        
	}
	/// <summary>
	/// ����ڵ�
	/// </summary> 
	public class WorkExt : Work
	{
		 
		 
		#region ���캯��
		
		 
		/// <summary>
		/// ����ڵ�
		/// </summary>
		public WorkExt()
		{
		  
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

				Map map = new Map("V_TA_Work");
				map.EnDesc="����ڵ�";
				map.AddTBIntPKOID();
				//map.AddTBInt(WorkExtAttr.ParentID,0,"���ڵ�ID",true,true);
				//map.AddDDLEntities(WorkExtAttr.FK_Task,0,DataType.AppInt,"����",new Tasks(),TaskAttr.TaskID, TaskAttr.Title,false);
				//map.AddTBInt(WorkExtAttr.Step,1,"����",true,true);
				//map.AddBoolean(WorkExtAttr.IsRe,false,"�Ķ���ִ",true,false);
				map.AddDDLSysEnum(WorkExtAttr.WorkState, (int) WorkState.None ,"�ڵ�״̬",true,true);
				map.AddDDLSysEnum(WorkExtAttr.PRI,0,"���ȼ�",true,true);
				//map.AddTBString(WorkExtAttr.Emps,null,"������",true,false,0,4000,15);
				map.AddTBString(WorkExtAttr.Title,null,"�������",true,false,0,500,15);	
				//map.AddTBString(WorkExtAttr.Docs,null,"��������",true,false,0,4000,15);
				map.AddDDLEntities(WorkExtAttr.Sender,null,"�����´���", new Emps(),false );
				map.AddTBDateTime(WorkExtAttr.DateTimeOfAccept,"�����´�ʱ��",false,false );

                map.AddDDLEntities(WorkExtAttr.Executer, null, "ִ����", new Emps(), false);
				map.AddDDLEntities(WorkExtAttr.FK_Dept,null,"ִ���˲���", new BP.Port.Depts(),false );
			//	map.AddDDLEntities(WorkExtAttr.FK_ZSJG,null,"ִ���˹������", new BP.Tax.ZSJGs(),false );
				map.AddDDLEntities(WorkExtAttr.FK_Station,null,"ִ���˸�λ", new BP.Port.Stations(),false );

				map.AddDDLEntities(WorkExtAttr.FK_JD,null,"����", new BP.Pub.APs(),false );
				map.AddDDLEntities(WorkExtAttr.FK_NY,null,"����", new BP.Pub.NYs(),false );


				map.AddTBDateTime(WorkExtAttr.DateTimeOfTaskStart,"����ʼʱ��",false,false );
				map.AddTBDateTime(WorkExtAttr.DateTimeOfTaskEnd,"�������ʱ��",false,false );


				map.AddDDLSysEnum(WorkExtAttr.CheckWay,(int)CheckWay.UnSet,"���˷�ʽ",true,true);
				map.AddTBInt(WorkExtAttr.CentOfCheck,10,"���˷�",true,true);

			 
				map.AddSearchAttr(WorkExtAttr.CheckWay);
				map.AddSearchAttr(WorkExtAttr.FK_Station);
				map.AddSearchAttr(WorkExtAttr.FK_Station);


				//map.AttrsOfSearch.AddHidden(WorkExtAttr.Sender,"=",Web.WebUser.No);
				map.AttrsOfSearch.AddFromTo("��ʼ����",WorkExtAttr.DateTimeOfTaskStart,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);

 
 
				this._enMap=map;
				return this._enMap;
			}
		}
		 

		#endregion 

	}
	/// <summary>
	/// ����ڵ�s
	/// </summary> 
	public class WorkExts: Works
	{
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkExt();
			}
		}
		/// <summary>
		/// WorkExts
		/// </summary>
		public WorkExts()
		{

		}
	 
				  
				  
	}
}
 
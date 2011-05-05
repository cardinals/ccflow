using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.TA
{
	/// <summary>
	/// �ƻ�״̬
	/// </summary>
	public enum TaskSta
	{
		/// <summary>
		/// δ��ʼ
		/// </summary>
		UnRun,
		/// <summary>
		/// ������
		/// </summary>
		Run,
		/// <summary>
		/// ���
		/// </summary>
		Complete,
		/// <summary>
		/// �Ƴ�
		/// </summary>
		Suspend
	}
	/// <summary>
	/// ��������
	/// </summary>
	public enum Sharing
	{
		SPublic,
		SPrivate
	}
	/// <summary>
	/// �ƻ�����
	/// </summary>
	public class TaskAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// ����
		/// </summary>
		public const string TaskSta="TaskSta";
		/// <summary>
		/// PRI
		/// </summary>
		public const string PRI="PRI";
		/// <summary>
		/// �Ƿ��п�ʼʱ��
		/// </summary>
		public const string SharingType="SharingType";
		/// <summary>
		/// �Ƿ��н���ʱ��
		/// </summary>
		public const string IsHaveEndDate="IsHaveEndDate";
		/// <summary>
		/// ��������
		/// </summary>
		public const string TaskEndDate="TaskEndDate";
		/// <summary>
		/// ʵ�ʽ�������
		/// </summary>
		public const string InfactEndDate="InfactEndDate";
		/// <summary>
		/// ��¼��
		/// </summary>
		public const string Recorder="Recorder"; 
		/// <summary>
		/// ��¼����
		/// </summary>
		public const string RDT="RDT"; 	
		/// <summary>
		/// ��ע
		/// </summary>
		public const string Notes="Notes"; 	
		/// <summary>
		/// �������
		/// </summary>
		public const string FK_TaskGroup="FK_TaskGroup";


        public const string Doc = "Doc";
	}
	/// <summary>
	/// �ƻ�
	/// </summary> 
	public class Task : EntityOIDName
	{
		#region  ����
		public TaskSta HisTaskSta
		{
			get
			{
				return (TaskSta)this.GetValIntByKey(TaskAttr.TaskSta);
			}
			set
			{
				this.SetValByKey(TaskAttr.TaskSta,(int)value);
			}
		}
		
		public string MyTaskStateStr
		{
			get
			{
				return "<img src='./images/Task.gif' border=0 /><a href=\"javascript:OpenWork('"+this.OID+"')\" >"+this.Name+"</a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
			}
		}
		public string NameExt
		{
			get
			{
                switch (this.HisTaskSta)
				{
					case TaskSta.Complete:
						return "<img src='./images/Task.gif' border=0 /><a href=\"javascript:OpenWork('"+this.OID+"')\" ><strike>"+this.Name+"</strike></a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
					default:
						return "<img src='./images/Task.gif' border=0 /><a href=\"javascript:OpenWork('"+this.OID+"')\" >"+this.Name+"</a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
				}
			}
			 
		}
		public string NameExtNoImg
		{
			get
			{
				switch(this.HisTaskSta)
				{
					case TaskSta.Complete:
						return "<a ondblclick=\"javascript:WinOpen('Task.aspx?RefID="+this.OID+"')\" ><strike>"+this.Name+"</strike></a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
					default:
						return "<a ondblclick=\"javascript:WinOpen('Task.aspx?RefID="+this.OID+"')\" >"+this.Name+"</a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
				}
			}
			 
		}
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.Doc);
            }
            set
            {
                SetValByKey(TaskAttr.Doc, value);
            }
        } 
		 
		public string PRI
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.PRI);
			}
			set
			{
				SetValByKey(TaskAttr.PRI,value);
			}
		} 
		/// <summary>
		/// 
		/// </summary>
		public string Recorder
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.Recorder);
			}
			set
			{
				SetValByKey(TaskAttr.Recorder,value);
			}
		}

        public int TaskStaInt
        {
            get
            {
                return this.GetValIntByKey(TaskAttr.TaskSta);
            }
            set
            {
                SetValByKey(TaskAttr.TaskSta, value);
            }
        }
		 
        
		public string TaskEndDate
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.TaskEndDate);
			}
			set
			{
				SetValByKey(TaskAttr.TaskEndDate,value);
			}
		}
		public string InfactEndDate
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.InfactEndDate);
			}
			set
			{
				SetValByKey(TaskAttr.InfactEndDate,value);
			}
		}
		public DateTime InfactEndDate_S
		{
			get
			{
				return this.GetValDateTime(TaskAttr.InfactEndDate);
			}
		}
		public bool IsHaveEndDate
		{
			get
			{
				return this.GetValBooleanByKey(TaskAttr.IsHaveEndDate);
			}
			set
			{
				this.SetValByKey(TaskAttr.IsHaveEndDate,value);
			}
		}
		public Sharing MySharingType
		{
			get
			{
				return (Sharing)this.GetValIntByKey(TaskAttr.SharingType);
			}
			set
			{
				this.SetValByKey(TaskAttr.SharingType,(int)value);
			}
		}
		public string Notes
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.Notes);
			}
			set
			{
				SetValByKey(TaskAttr.Notes,value);
			}
		}


        public void SetUnComplete()
        {
            this.HisTaskSta = TaskSta.Run;
            this.Update();
        }
		public void SetComplete()
		{

            this.HisTaskSta = TaskSta.Complete;
			this.Update();

				/*����Ѿ�����ˡ�*/
//				TaLog tl = new TaLog();
//			tl.Recorder=this.Recorder;
//			tl.Title =this.Name;
//			tl.Title="����ƻ���ʼʱ��:"+this.StartDate+"����ʱ��"+this.EndDate;
//			tl.OID=this.OID;
//			tl.Save();
//			pl.Update("TaskState", (int)TaskState.Complete );

			 
		}

		#endregion 
		 
		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}

		/// <summary>
		/// �ƻ�
		/// </summary>
		public Task()
		{
		  
		}
		/// <summary>
		/// �ƻ�
		/// </summary>
		/// <param name="_No">No</param>
		public Task(int oid):base(oid)
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

				Map map = new Map("TA_Task");
				map.EnDesc="��������";
				map.AddTBIntPKOID();
                map.Icon = "../TA/Images/Task_s.gif";

				map.AddTBString(TaskAttr.Name,null,"����",true,false,0,500,500);

				map.AddDDLEntities(TaskAttr.FK_TaskGroup,null,"���",new TaskGroups(),true);
                map.AddDDLSysEnum(TaskAttr.TaskSta, (int)TaskSta.UnRun, "״̬", true, true, TaskAttr.TaskSta, "@0=δ��ʼ@1=������@2=���@3=�Ƴ�");
                map.AddDDLSysEnum(TaskAttr.PRI, 0, "���ȼ�", false, true,TaskAttr.SharingType, "@0=��@1=��@2=��");
                map.AddDDLSysEnum(TaskAttr.SharingType, 0, "��������", false, true, TaskAttr.SharingType,"@0=����@1=˽��");
				//map.AddTBDateTime(TaskAttr.TaskStartDate,"��ʼ����",true,false );
				map.AddTBDateTime(TaskAttr.TaskEndDate,"��������",true,false );

				map.AddTBDateTime(TaskAttr.InfactEndDate,"ʵ�ʽ�������",false,false );
				map.AddBoolean(TaskAttr.IsHaveEndDate,false,"����ʱ��",false,true);

                map.AddTBString(TaskAttr.Recorder, null, "��¼��", false, false, 0, 30, 150);

                map.AddTBStringDoc();

				map.AddSearchAttr(TaskAttr.FK_TaskGroup);
				map.AddSearchAttr(TaskAttr.TaskSta);
				map.AddSearchAttr(TaskAttr.PRI);

				map.AttrsOfSearch.AddHidden(TaskAttr.Recorder,"=",Web.WebUser.No);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			this.InfactEndDate=DataType.CurrentDataTime;
			return base.beforeUpdateInsertAction ();
		}


		#endregion 

	}
	/// <summary>
	/// �ƻ�s
	/// </summary> 
	public class Tasks: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new Task();
			}
		}
		public Tasks()
		{
		}
		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="emp">��Ա</param>
		/// <param name="ny">����</param>
		public Tasks(string emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TaskAttr.Recorder,emp);
			qo.addOrderBy(TaskAttr.PRI);
			qo.DoQuery();
		}
		public Tasks(string emp, string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TaskAttr.Recorder,emp);
			qo.addAnd();
			qo.AddWhere(TaskAttr.TaskSta,(int)TaskSta.Complete );
			qo.addAnd();
			qo.AddWhere(TaskAttr.InfactEndDate, " like ", ny+"%" );
			qo.DoQuery();
		}
	}
}
 
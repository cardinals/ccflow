using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Collections;
using BP.En;
using BP.DA;

namespace BP.DTS
{
	
	/// <summary>
	/// ���Ȱ���ϸ����
	/// </summary>
	public class DTSAttr :EntityNoNameAttr
	{
		/// <summary>
		/// ���
		/// </summary>
		public const string FK_PackNo="FK_PackNo";
		/// <summary>
		/// ��������
		/// </summary>
		public const string RunType="RunType";
		/// <summary>
		/// �����ı�(sql,����)
		/// </summary>
		public const string RunText="RunText";
		/// <summary>
		/// ����Ƶ��
		/// </summary>
		public const string RunTimeType="RunTimeType";
		/// <summary>
		/// ÿ��
		/// </summary>
		public const string EveryMonth="EveryMonth";
		/// <summary>
		/// ÿ����
		/// </summary>
		public const string EveryQuarter="EveryQuarter";
		/// <summary>
		/// ÿ��
		/// </summary>
		public const string EveryDay="EveryDay";
		/// <summary>
		/// ÿʱ
		/// </summary>
		public const string EveryHour="EveryHour";
		/// <summary>
		/// ÿ��
		/// </summary>
		public const string EveryMinute="EveryMinute";
		/// <summary>
		/// ��ע
		/// </summary>
		public const string Note="Note";
		/// <summary>
		/// ִ����־
		/// </summary>
		public const string RunLog="RunLog";
		public const string FK_Sort="FK_Sort";

	}
	/// <summary>
	/// ���Ȱ���ϸ
	/// </summary>
	public class SysDTS : EntityNoName 
	{

		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		public RunType RunTypeOfEnum
		{
			get
			{
				return (RunType)this.GetValIntByKey(DTSAttr.RunType);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunType,(int)value);
			}
		}
		/// <summary>
		/// Ƶ��
		/// </summary>
		public  RunTimeType  RunTimeType_del
		{
			get
			{
				return (RunTimeType)this.GetValIntByKey(DTSAttr.RunTimeType);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunTimeType,(int)value);
			}	
		}
		/// <summary>
		/// Ƶ��
		/// </summary>
		public  BP.DTS.RunTimeType  RunTimeTypeOfEnum_del
		{
			get
			{
				return (BP.DTS.RunTimeType)this.GetValIntByKey(DTSAttr.RunTimeType);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunTimeType,(int)value);
			}	
		}
		/// <summary>
		/// ����ʱ������
		/// </summary>
		public string RunTimeTypeText
		{
			get
			{
				return this.GetValRefTextByKey(DTSAttr.RunTimeType);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public  string  RunText
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.RunText);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunText,value);
			}	
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public  string  Note
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.Note);
			}
			set
			{
				this.SetValByKey(DTSAttr.Note,value);
			}	
		}
		public  string  FK_Sort
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.FK_Sort);
			}
			set
			{
				this.SetValByKey(DTSAttr.FK_Sort,value);
			}	
		}
		/// <summary>
		/// log
		/// </summary>
		public  string  RunLog
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.RunLog);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunLog,value);
			}	
		}		
		/// <summary>
		/// EveryMonth
		/// </summary>
		public  string  EveryMonth
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryMonth);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryMonth,value);
			}	
		}
		/// <summary>
		/// EveryDay
		/// </summary>
		public  string  EveryDay
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryDay);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryDay,value);
			}	
		}
		/// <summary>
		/// EveryHour
		/// </summary>
		public  string  EveryHour
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryHour);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryHour,value);
			}	
		}	
		/// <summary>
		/// EveryMinute
		/// </summary>
		public  string  EveryMinute
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryMinute);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryMinute,value);
			}	
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// һ������
		/// </summary>
		public SysDTS()
		{
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="_No">���</param>
		public SysDTS( string _No ) :base(_No)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_DTS");
				map.EnDesc="������ϸ";
				map.EnType=EnType.Sys; //ʵ�����ͣ�admin ϵͳ����Ա��PowerAble Ȩ�޹����,Ҳ���û���,��Ҫ���������Ȩ�޹������������������á���
				map.CodeStruct ="2";
				map.IsAllowRepeatNo=false;
				//map.IsAllowRepeatName=false;

				map.AddTBStringPK(DTSAttr.No,null,"������",true,true,3,3,3);
				map.AddTBString(DTSAttr.Name,null,"����",true,false,1,2000,20);
				map.AddDDLEntities(DTSAttr.FK_Sort,"0","���",new Sys.SysDTSSorts(),false);
				map.AddTBStringDoc(DTSAttr.Note,null,"��ע",true,false);
				map.AddTBString(DTSAttr.EveryMonth,"00","��",true,false,2,200,4);
				map.AddTBString(DTSAttr.EveryDay,"00","��",true,false,2,200,4);
				map.AddTBString(DTSAttr.EveryHour,"00","ʱ",true,false,2,200,4);
				map.AddTBString(DTSAttr.EveryMinute,"00","��",true,false,2,200,4);
				map.AddTBString(DTSAttr.RunText,null,"��������",true,false,1,3000,20);
				map.AddTBStringDoc(DTSAttr.RunLog,null,"��־",true,false);
				map.AddDDLSysEnum(DTSAttr.RunType,0,"��������",true,false);

				this._enMap=map;
				return this._enMap; 
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

		#region ִ������
		/// <summary>
		/// ��ǰʱ���ܷ�����.
		/// </summary>
		public bool IsCanRun
		{
			get
			{
				string MM=DateTime.Now.ToString("MM"); 
				string dd=DateTime.Now.ToString("dd");
				string HH=DateTime.Now.ToString("HH");
				string min=DateTime.Now.ToString("mm");

				if (this.EveryMonth!="00")				 
					if (this.EveryMonth.IndexOf(MM)==-1)
						return false;

				if (this.EveryDay!="00")				 
					if (this.EveryDay.IndexOf(dd)==-1)
						return false;

				if (this.EveryHour!="00")				 
					if (this.EveryHour.IndexOf(HH)==-1)
						return false;

				if (this.EveryMinute!="00")				 
					if (this.EveryMinute.IndexOf(min)==-1)
						return false;

				return true;
			}
		}
		public Thread thisThread = null;
		/// <summary>
		/// ִ������(Execute(),ͨ���߳�״̬)
		/// </summary>
		public void Run()
		{
			if (this.IsCanRun==false)
				return ;

			if( thisThread==null)
			{
				ThreadStart ts =new ThreadStart( this.Execute )  ; 
				thisThread = new Thread(ts);		 
				thisThread.Start();
			}
			else
			{
				if( thisThread.ThreadState == System.Threading.ThreadState.Running )
				{
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "@����һ���߳�["+this.No+"]û��ִ����Ϳ�ʼ���´ε���.ϵͳ������Ƿ��ء�" ) ;
					return ;  // ����������У� �ͷ��ء�					 
				}
				else
				{
					thisThread.Abort();
					thisThread = new Thread(new ThreadStart( this.Execute ));
					thisThread.Start();
				}
			}
		}		 
		/// <summary>
		/// ִ������(0  �м��,1  SQL�ı�,2  SQL�洢����)
		/// </summary>
		public void Execute()
		{
			string log="\n��ʼִ��ʱ��:"+DataType.CurrentDataTimess;
			string task = "TaskNo["+this.No+"],TaskName["+this.Name+"],RunType["+this.RunTypeOfEnum.ToString()+"], RunText["+this.RunText+"].";
			//BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  task ) ;
			try
			{
				switch( this.RunTypeOfEnum )
				{
					case RunType.Method : // �м�㷽�� 
						this.ExecClassMethod();
						break;
					case RunType.SQL: // SQL�ı�
						this.ExecSqlText();
						break;
					case RunType.SP: // SQL�洢����
						this.ExecSqlStoredPro();
						break;
					case RunType.DataIO: // SQL�洢����
						DataIOEn ioen =(DataIOEn)BP.DA.ClassFactory.GetDataIOEn(this.RunText);
						ioen.Do();
						break;
					default :
						task = "�������Ͳ���ȷ��\r\n" + task;
						BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Warning,  task ) ;
						throw new Exception(task);
				}
				//task = "ִ����ϣ�\r\n*****TaskInfo:" + task;
				BP.DA.Log.DefaultLogWriteLineInfo( task );
			}
			catch(Exception ex)
			{
				string err = "��������\r\n*****TaskInfo:" + task + "\r\n*****������Ϣ��"+ex.Message ;
				BP.DA.Log.DefaultLogWriteLineInfo(  err ) ;
				throw new Exception("@ִ�е���:"+this.Name+"�������´���:"+ex.Message);
			}
			log= log+ "����ִ��ʱ��"+DataType.CurrentDataTimess;

			//this.RunLog=log+this.RunLog.Substring(0,450);
			this.Update("RunLog", log );
		}
		#endregion ִ������

		#region ����ִ�з���
		/// <summary>
		/// ����ִ�з���(�м�㷽��)
		/// </summary>
		private void ExecClassMethod()
		{
			/*
			switch(this.No)
			{
				case "001":	  // ��ҵ�Ǽ�					 
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "��ʼִ��"+this.Name ) ;
//					BP.WF.WFDTS.TransferAutoGenerWorkBreed( new BP.WF.Breed("1000"));
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "����ִ��"+this.Name ) ;
					break;
				case "002": // ��ȡ�ⲿ���ԡ�
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "��ʼִ��"+this.Name ) ;
//					BP.WF.WFDTS.DTSPCWork();
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "����ִ��"+this.Name ) ;
					break;
				case "003": // ����
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "��ʼִ��"+this.Name ) ;
					//BP.WF.WFDTS.InitCHOfStation();
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "����ִ��"+this.Name ) ;
					break;
				case "004": // ͳ��
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "��ʼִ��"+this.Name ) ;
					//BP.WF.WFDTS.InitBreedStat() ;
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "����ִ��"+this.Name ) ;
					break;
				default:
					throw new Exception("@û�д���� dts method .");
			}
*/

			 
			string str =this.RunText.Trim(' ',';','.');
			int pos = str.LastIndexOf(".");
			string clas = str.Substring(0,pos);
			string meth = str.Substring(pos,str.Length-pos).Trim('.',' ','(',')');
			object obj =  DA.ClassFactory.GetEn( clas );
			if(obj==null)
				throw new Exception("��������["+clas+"]ʵ��ʧ�ܣ�");
			Type tp = obj.GetType();
			MethodInfo mp = tp.GetMethod( meth );
			if(mp==null)
				throw new Exception("@����ʵ��["+tp.FullName+"]��û���ҵ�����["+meth+"]��");
			mp.Invoke( obj ,null ); //�����ɴ� MethodInfo ʵ������ķ������캯����

		}
		/// <summary>
		/// ����ִ�з���(SQL�ı�)
		/// </summary>
		private void ExecSqlText()
		{
			DA.DBAccess.RunSQL(this.RunText) ;			 
		}
		
		/// <summary>
		/// ����ִ�з���(SQL�洢����)
		/// </summary>
		private void ExecSqlStoredPro()
		{
			DA.DBAccess.RunSP(this.RunText);
			return ;

			/*
			SqlConnection conn2 = new SqlConnection((DA.DBAccess.GetAppCenterDBConn as SqlConnection).ConnectionString);
			try
			{
				conn2.Open();
				string sql = this.RunText.Trim();
				DA.DBAccess.RunSQL( sql ,conn2 ,CommandType.StoredProcedure );
			}
			catch(Exception ex)
			{
				conn2.Close();
				conn2.Dispose();
				throw new Exception(ex.Message);
			}
			finally
			{
				conn2.Close();
				conn2.Dispose();
			}
			*/
		}
		#endregion ����ִ�з���

		#region ��ѯ
		/// <summary>
		/// ����ִ�е����ݲ�ѯ��
		/// </summary>
		/// <param name="runText"></param>
		/// <returns></returns>
		public int RetrieveByRunText(string runText)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DTSAttr.RunText,runText);
			return qo.DoQuery();
		}
		#endregion
	}
	/// <summary>
	/// ���Ȱ���ϸ�����캯��
	/// </summary> 
	public class SysDTSs : EntitiesNoName
	{
		/// <summary>
		/// ��������ǰ�ϵͳ�б�д�� DataIOEn ʵ�壮û�м��뵽�����еļ����ȥ��
		/// �������в����ڵ�ɾ����
		/// </summary>
		public static void InitDataIOEns()
		{
			//baseEnsName
			ArrayList al =BP.DA.ClassFactory.GetObjects("BP.DTS.DataIOEn");
			//int i=1;
			// ��ʼ��ϵͳʵ�塣
            foreach (DataIOEn en in al)
            {
                SysDTS dts = new SysDTS();
                int ii = (int)en.HisRunTimeType;
                if (dts.RetrieveByRunText(en.ToString()) == 1)
                    continue;
                dts.Name = en.Title;
                dts.RunTypeOfEnum = RunType.DataIO;
                //dts.RunTimeType=en.HisRunTimeType;
                dts.RunText = en.ToString();
                //dts.RunTimeTypeOfEnum=en.HisRunTimeType;

                dts.Note = en.Note + "@����" + BP.DA.DataType.CurrentDataTimess;
                dts.EveryDay = en.DefaultEveryDay;
                dts.EveryHour = en.DefaultEveryHH;
                dts.EveryMinute = en.DefaultEveryMin;
                dts.EveryMonth = en.DefaultEveryMonth;
                dts.FK_Sort = en.FK_Sort;
                dts.No = dts.GenerNewNo;
                dts.Save();
            }
		}

		#region  tick

		
	  
		/// <summary>
		/// ��ȡ��ǰ����ִ��SQL,�󶨵��Ȱ�������ϸ��
		/// </summary>
		public static SysDTSs GetNowTasks_del()
		{
			SysDTSs dts =new SysDTSs();
			try
			{
				DateTime dt = DateTime.Now;
				string month = dt.ToString("MM");
				string dd = dt.ToString("dd");
				string HH = dt.ToString("HH"); // 24 Сʱ�ơ�
				string mm = dt.ToString("mm");
				/*WF_AttemperPack���Ȱ�,WF_DTS���Ȱ���ϸ*/
				string sql="SELECT No FROM Sys_DTS WHERE (RunTimeType=3 and EveryMonth  LIKE '%@month%' and EveryDay  LIKE '%@dd%' and EveryHour LIKE '%@hh%' and EveryMinute='%@mm%') OR (RunTimeType=2 and EveryDay  LIKE '%@dd%' and EveryHour LIKE '%@HH%' and EveryMinute='%@mm%') OR (RunTimeType=1 and EveryHour LIKE '%@HH%' and EveryMinute LIKE'%@mm%') OR ( RunTimeType=0 and EveryMinute like '%@mm%')";
				sql=sql.Replace("@dd",dd);
				sql=sql.Replace("@mm",mm);
				sql=sql.Replace("@month",month);
				sql=sql.Replace("@HH",HH);

				DA.Log.DebugWriteInfo(" dts run sql="+sql);
				DataTable table = DA.DBAccess.RunSQLReturnTable(sql);
				foreach(DataRow dr in table.Rows)
				{
					SysDTS en = new SysDTS();
					en.No = dr["No"].ToString();
					dts.AddEntity(en);
				}
				return dts;
			}
			catch(Exception ex)
			{
				string error="GetNowTasks()  "+ ex.Message;
				DA.Log.DefaultLogWriteLine(DA.LogType.Error, error);
				throw new Exception(error);			
			}
		}
		/// <summary>
		/// ִ������
		/// </summary>
		public string Run()
		{
			string strs="";
			foreach(SysDTS en in this)
			{
				try
				{
					en.Run();
				}
				catch(Exception ex)
				{
					strs+="@����������DTS "+en.Name+"�������쳣."+ex.Message;
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Error,strs);
				}
			}
			return strs;
		}
		#endregion

		/// <summary>
		/// ���Ȱ���ϸ�����캯��
		/// </summary>
		public SysDTSs()
		{
		}		
		/// <summary>
		/// ���Ȱ���ϸ�����캯��
		/// </summary>
		public new SysDTS this[int index]
		{
			get
			{
				return base[index] as SysDTS;
			}
		}
		/// <summary>
		/// ���Ȱ���ϸ�����캯��
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysDTS();
			}
		}
	}
}


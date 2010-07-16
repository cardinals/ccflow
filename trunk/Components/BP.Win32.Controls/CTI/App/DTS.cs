using System;
using System.Data;
using BP.DTS;
using BP.DA;


namespace BP.CTI
{
	public class Bank:DataIOEn2
	{
		public Bank()
		{
			this.HisDoType=DoType.UnName;
			this.Title="���д������";
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		public override void Do()
		{
			TelDTS dts = new TelDTS();
			BP.SystemConfig.AppSettings["Tax_WSB"]=BP.SystemConfig.AppSettings["Tax_YEBZ"];
			dts.Do();
		}
	}
	/// <summary>
	/// ��������δδ�걨��˰��
	/// </summary>
	public class TelDTSOfLinYi:DataIOEn
	{
		public TelDTSOfLinYi()
		{
			this.HisDoType=DoType.UnName;
			this.Title="(���ʵ�˰)�绰�޽�";
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		public override void Do()
		{
			TelDTS dts = new TelDTS();
			BP.SystemConfig.AppSettings["Tax_WSB"]=BP.SystemConfig.AppSettings["Tax_YEBZ"];
			dts.Do();
		}
	}


	/// <summary>
	/// ����ͳ��
	/// </summary>
	public class TelDTS:DataIOEn
	{
		/// <summary>
		/// ����ͳ��
		/// </summary>
		public TelDTS()
		{
			this.HisDoType=DoType.UnName;
			if (SystemConfig.SysNo !="CTISRVOfLinYiTax" )
			{
				this.Title="Ƿ������";
			}
			else
			{
				this.Title="(���ʵ�˰)δ�걨����";
			}
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		public override void Do()
		{
			#region ���Ӳ��Լ�¼.�ڸ���֮ǰ.
			if (Sys.SysConfigs.GetValByKeyBoolen("CallTestTelEveryDay",true))
			{
				BP.CTI.App.TelTests tests1 = new BP.CTI.App.TelTests();
				tests1.InitCallList();
			}
			#endregion

			// �õ���ǰ�ĵ��ȣ��ڵ���ϵͳ�еı�š�
			string dtsNo=this.GetNoInDTS();
			string  CurrScheduleId=DataType.CurrentMonth+ "_" + dtsNo  ; // ȡ����ǰ  �·�+"_"+���ȱ��  
			string  ConfigScheduleId=Sys.SysConfigs.GetValByKey("CurrentScheduleID", DataType.CurrentMonth+"_0" );

			if ( CurrScheduleId != ConfigScheduleId)
			{
				/* �������ID ����Ⱦ�˵������һ�ο�ʼʹ�ô˵��ȡ� �������� */
				string  bakTable="CTI_CallList_"+ConfigScheduleId ;
				DBAccess.RunSQLDropTable(bakTable); 
				DBAccess.RunSQL("SELECT * into "+bakTable+" FROM CTI_CallList"); //bak it.
				DBAccess.RunSQL("DELETE CTI_CallList"); //.�����ǰ�ĺ���.
				Sys.SysConfigs.SetValByKey("CurrentScheduleID", CurrScheduleId); //���õ�ǰ��Ϊ
			}
			else
			{
				/* ˵����һ���Ѿ�ִ�й��� dts.�����κδ��� */
			}

			  
			try
			{
				Log.DefaultLogWriteLineInfo("****** Start OF "+SystemConfig.CustomerNo+" Tel DTS ����ִ��ȡ����");
				switch (SystemConfig.CustomerNo)
				{
					case CustomerNoList.LYTax:
					case CustomerNoList.YSDS:
					case CustomerNoList.DS371301:
					case CustomerNoList.DS371306:
					case CustomerNoList.DS371307:
					case CustomerNoList.DS371311:
					case CustomerNoList.DS371312:
					case CustomerNoList.DS371321:
					case CustomerNoList.DS371323:
					case CustomerNoList.DS371324:
					case CustomerNoList.DS371325:
					case CustomerNoList.DS371326:
					case CustomerNoList.DS371327:
					case CustomerNoList.DS371328:
					case CustomerNoList.DS371329:
						WSB ws= new WSB();
						ws.Do(); // �γ�Ϊ�걨����.						
						//DBAccess.RunSP("ProCTI_WillCall");      //�˴洢���̣����걨���������Ϣ���뵽�����б����档��
						DBAccess.RunSP("ProCTI_Tax_NotDeclare");      //�˴洢���̣����걨���������Ϣ���뵽�����б����档��
						break;
					case CustomerNoList.YSGS:
						DBAccess.RunSQL("DELETE CTI_WillCall"); // ɾ���걨��
						DBAccess.RunSQL( BP.SystemConfig.AppSettings["Tax_WSB"] ); //ȡ����Ҫ���µ����ݡ�
						break;
					case CustomerNoList.YSNet: //��ͨ��˾
						this.DoOfYSNet();
						break;
					default:
						throw new Exception("CustomerNoList errory");
				}
				Log.DefaultLogWriteLineInfo("****** END OF "+SystemConfig.CustomerNo+" Tel DTS ����ִ��ȡ����");

			}
			catch (Exception ex)
			{
				Log.DefaultLogWriteLineError("******  "+SystemConfig.CustomerNo+" Tel DTS ����ʧ��"+ex.Message );
				throw new Exception(ex.Message);

				/* �������ʧ����δ���? */
				int  val=Sys.SysConfigs.GetValByKeyInt("WhenDTSError",3);	
				if (val==3)
				{
					/* ����ԭ����״̬������. */
					return;
				}
				else
				{
					/* ���ÿ��Ĺ���״̬. */
					Sys.SysConfigs.SetValByKey("CardWorkState",val);
				}
			}

			#region ���Ӳ��Լ�¼.�ڸ���֮��
			if (Sys.SysConfigs.GetValByKeyBoolen("CallTestTelEveryDay",true))
			{
				/* ������������������. */
				BP.CTI.App.TelTests tests = new BP.CTI.App.TelTests();
				tests.InitCallList();
			}
			#endregion
		}
		/// <summary>
		/// ����ͳ��
		/// </summary>
		public void DoOfYSNet()
		{
			
			Log.DefaultLogWriteLineInfo("****** START OF TelDTS ��ʼִ��ȡ����");
			// �ж��Ƿ���ȹ���������.
			int i =DBAccess.RunSQLReturnValInt("select count(*) from ywxttbl");
			if (i==0)
			{
				/*˵�����Ȱ�û�еõ�����*/
				Log.DefaultLogWriteLineError("���Ȱ�û�еõ�����,ϵͳִ������ͣ...");
				Card.DoPause();
				return;
			}

			#region  �ж������Ƿ�仯��
			/* ���������ֵ����ȣ�*/
			DBAccess.RunSP("ProCTIData"); // ���´˴洢���̣�
			#endregion

			Log.DefaultLogWriteLineInfo("****** END OF TelDTS ����ִ��ȡ����");
		}
	 
		public  void DoOfTax()
		{
			Log.DefaultLogWriteLineInfo("****** START OF Tel DTS ��ʼִ��ȡ����");

			WSB wsb = new WSB();
			wsb.Do();

			// ����cti_temp ��ʱ�� 	
			DBAccess.RunSQLDropTable("CTI_Temp");
			// ���������п�����������ҵ����һ���绰����ġ�
			DBAccess.RunSQL("SELECT DISTINCT Tel  INTO CTI_Temp from Tax_WSB where NY='"+DataType.CurrentYearMonth+"'");
			 
			/* ���������ֵ����ȣ�*/
			try
			{
				DBAccess.RunSP("ProCTIData_Tax"); // ���´˴洢���̣�
			}
			catch(Exception ex)
			{
				Log.DefaultLogWriteLineError("run ProCTIData_Tax"+ex.Message);
			}
			Log.DefaultLogWriteLineInfo("****** END OF Tel DTS ����ִ��ȡ����");
		}

		/// <summary>
		/// ����δ�걨��˰��
		/// </summary>
		public class WSB :DataIOEn
		{
			public WSB()
			{
				this.HisDoType=DoType.UnName;
				this.Title="����δ�걨��˰��";
				this.HisRunTimeType= RunTimeType.Month;
				this.FromDBUrl=DBUrlType.DBAccessOfOracle9i;
				this.ToDBUrl=DBUrlType.AppCenterDSN;
				this.Note="��Ҫÿ�µ�ִ��һ�Σ�ִ�еĽ�����Թ��־���Ա��ѯ��";
			}
			/// <summary>
			/// ִ��
			/// </summary>
			public override void Do()
			{
				//this.Directly( "SELECT  QYBM AS NO ,  JKJJ   from DSBM.DJSW  where city_num='"+SystemConfigOfTax.FK_ZSJG+"' ","DS_TaxpayerJJLX");  // ������ҵ��ʱ��

				//return;

				string sql="";
			 
				DBAccess.RunSQL("DELETE  CTI_WillCall"); // ɾ��ԭ�����걨����.
				//DBAccess.RunSQL("DELETE  CTI_WillCall"); // ɾ��ԭ�����걨����.

				int thisYear=DateTime.Now.Year;
				string thisDay=DateTime.Now.ToString("yyyyMMdd");

				#region ��ѯ���廧
				switch(DateTime.Now.Month)
				{
					case 1:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in ( select qybm from dsbm.shd where ( B_YEAR='"+thisYear+"'  AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' )  union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd ) ";
						break;
					case 4:
					case 10:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in ( select qybm from dsbm.shd where ( B_YEAR='"+thisYear+"'  AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' )  and ( SBFS  LIKE  '%A%' OR SBFS  LIKE  '%C%' ) union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd WHERE JNQX='01' OR JNQX='02' ) ";
						break;
					case 7:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in ( select qybm from dsbm.shd where ( B_YEAR='"+thisYear+"'  AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' )  and ( SBFS  Not like '%E%' ) union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd WHERE JNQX<>'03' ) ";
						break;			 
					default:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in (select qybm from dsbm.shd where B_YEAR='"+thisYear+"' AND SBFS like '%A%' AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd where JNQX='01' ) ";
						break;
				}
				this.FromDBUrl=DBUrlType.DBAccessOfOracle9i;
				this.Directly(sql,"Tax_TempAll_GT");  // ���ɸ�����ʱ��

				//DBAccess.RunSQL("update Tax_TempAll set NY='"+DataType.CurrentYearMonth+"'" ); // ɾ����ǰ��.

				sql="SELECT QYBM   FROM DSBM.DJSWTY  WHERE city_num ='"+SystemConfigOfTax.FK_ZSJG+"' AND  TO_CHAR(TY_BEGIN,'YYYYMMDD')<='"+DateTime.Now.ToString("yyyyMM")+"01'   AND  TO_CHAR(  TY_END, 'YYYYMMDD' )  >=  '"+DateTime.Now.ToString("yyyyMM")+"20'  ";
				this.Directly(sql,"Tax_Temp1");

				sql="SELECT REGIS_NUM   No FROM DS000000.SZZ88 WHERE city_num = '"+SystemConfigOfTax.FK_ZSJG+"' AND TO_CHAR(INQU_DATE,'YYYYMM')='"+DateTime.Now.ToString("yyyyMM")+"' AND WASTE_FLAG<>'1'";
				this.Directly(sql,"Tax_Temp2");

				DBAccess.RunSQL("DELETE  Tax_TempAll_GT WHERE No in (SELECT * from Tax_Temp1 ) ");
				DBAccess.RunSQL("DELETE  Tax_TempAll_GT WHERE No in (SELECT * from Tax_Temp2 ) ");

				// ɾ����ǰ�·ݵ����ݡ�
				//DBAccess.RunSQL("DELETE Tax_WSB WHERE NY='"+DataType.CurrentYearMonth+"' ");
				// �Ѳ����ļ�¼�������档
				DBAccess.RunSQL("INSERT INTO CTI_WillCall  SELECT No,  '"+DataType.CurrentYearMonth+"' as NY ,Name, Tel , 1 as FK_TelType, 1000 AS JE FROM Tax_TempAll_GT");

				//DBAccess.RunSQL("INSERT INTO Tax_WSB (No, Name, Tel, TaxpayerType ) SELECT No, NY,Name, Tel, 1 as TaxpayerType  FROM Tax_TempAll");
				#endregion

				#region  ��ҵ
				// ��ʼ��ѯ��ҵ��
				switch(DateTime.Now.Month)
				{
					case 1: // ���꣬���·ݡ�
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel FROM DSBM.DJSW WHERE city_num like '"+SystemConfigOfTax.FK_ZSJG+"' AND   FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD ) ";
						break;
					case 4:
					case 10:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel FROM DSBM.DJSW WHERE city_num like '"+SystemConfigOfTax.FK_ZSJG+"' AND  FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD WHERE JNQX='01' or JNQX='02') ";
						break;
					case 7:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel FROM DSBM.DJSW WHERE city_num = '"+SystemConfigOfTax.FK_ZSJG+"' AND  FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD WHERE JNQX='01' OR JNQX='04') ";
						break;			 
					default:
						sql="SELECT qybm as No, TRIM(QYMC) as Name, TRIM(QYDH) as Tel FROM DSBM.DJSW WHERE city_num='"+SystemConfigOfTax.FK_ZSJG+"' AND  FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD WHERE JNQX='01') ";
						break;
				}

				this.Directly(sql,"Tax_TempAll_QY");  // ������ҵ��ʱ��
 
				DBAccess.RunSQL("DELETE  Tax_TempAll_QY WHERE No in (select * from Tax_Temp1 ) ");
				DBAccess.RunSQL("DELETE  Tax_TempAll_QY WHERE No in (select * from Tax_Temp2 ) ");
				DBAccess.RunSQL("DELETE  Tax_TempAll_QY WHERE No in (select No from CTI_WillCall  ) ");

				// �Ѳ����ļ�¼�������档
				DBAccess.RunSQL("INSERT INTO CTI_WillCall  SELECT No, '"+DataType.CurrentYearMonth+"' as NY,Name, Tel , 2 as FK_TelType, 1000 AS JE FROM Tax_TempAll_QY");
				#endregion

				this.Directly( "SELECT  QYBM AS NO ,  JKJJ   from DSBM.DJSW  where city_num='"+SystemConfigOfTax.FK_ZSJG+"' ","DS_TaxpayerJJLX");  // ������ҵ��ʱ��


				


			}
		}

	}

}

	 
 

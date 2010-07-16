using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.CTI;


namespace BP.CTI.App
{
	/// <summary>
	/// ����״̬
	/// </summary>
	public enum CallingState
	{
		/// <summary>
		/// �ȴ�����
		/// </summary>
		Init,
		/// <summary>
		/// ���ں���
		/// </summary>
		Calling,
		/// <summary>
		/// �����ɹ�
		/// </summary>
		OK,
		/// <summary>
		/// ������ʱ
		/// </summary>
		TimeOut,
		/// <summary>
		/// ��������
		/// </summary>
		Error
	}
	/// <summary>
	/// �����б�����
	/// </summary>
	public class CallListAttr:EntityOIDAttr
	{
		#region ����
		/// <summary>
		/// tel
		/// </summary>
		public const string Tel="Tel";
		public const string TelName="TelName";

		/// <summary>
		/// ���
		/// </summary>
		public const string JE="JE";
		/// <summary>
		/// ����ʱ���
		/// </summary>
		public const string FK_TelType="FK_TelType";
		/// <summary>
		/// ���ջ���
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";

		/// <summary>
		/// ����ʱ�䵽
		/// </summary>
		public const string FK_Context="FK_Context";
		/// <summary>
		/// ���д���
		/// </summary>
		public const string CallDegree="CallDegree";		 
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const string CallDateTime="CallDateTime";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const string CallingState="CallingState";
		/// <summary>
		/// ��������
		/// </summary>
		public const string CallDate="CallDate";
		/// <summary>
		/// ��ע1
		/// </summary>
		public const string Note="Note";
		#endregion
	}
	/// <summary>
	/// �����б�
	/// </summary> 
	public class CallList :Entity
	{
		#region ִ�в���
		/// <summary>
		/// �����ļ�
		/// </summary>
		public string[] CallFiles=null;
		/// <summary>
		/// ���ú�������.
		/// </summary>
		/// <param name="context">Ҫ����������</param>
		public void DoCalling(string context)
		{
			if (context.IndexOf(",")!=-1)
			{
				/*˵���Ƕ��ļ�����*/
				context=context.Replace("@YF@", "D"+DateTime.Now.AddMonths(-1).Month+".TW");
				context=context.Replace("@Tel@", this.TelOfFile);
				context=context.Replace("@JE@", this.JEOfFile);
			}
			context=context.Replace(",,", ",");
			this.CallFiles=context.Split(',');

			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Calling); // �����´���ȡ����.
		}
		public void DoInit(string msg)
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Init,
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,msg);
		}
		public void DoTimeOut(string msg)
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Init,
				CallListAttr.CallDegree, this.CallDegree+1 , 
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,msg);
		}
		public void DoError(string msg)
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Error,				
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,msg);
		}
		public void DoOK()
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.OK,	
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,"�ɹ�����");
		}
		#endregion

		#region ��������
		public TelType HisTelType
		{
			get
			{
				return new TelType(this.FK_TelType);
			}
		}
		 
		public string TelOfFile
		{
			get
			{
				string telstr=this.Tel;
				telstr=telstr.Replace("0","D0.TW,");
				telstr=telstr.Replace("1","D1.TW,");
				telstr=telstr.Replace("2","D2.TW,");
				telstr=telstr.Replace("3","D3.TW,");
				telstr=telstr.Replace("4","D4.TW,");
				telstr=telstr.Replace("5","D5.TW,");
				telstr=telstr.Replace("6","D6.TW,");
				telstr=telstr.Replace("7","D7.TW,");
				telstr=telstr.Replace("8","D8.TW,");
				telstr=telstr.Replace("9","D9.TW,");
				return telstr; 
			}
		}
		 
		/// <summary>
		/// �绰
		/// </summary>
		public string  Tel
		{
			get
			{
				return BP.SystemConfig.AppSettings["BeforeDial"]+GetValStringByKey(CallListAttr.Tel);
			}
			set
			{
				SetValByKey(CallListAttr.Tel,value);
			}
		}
		/// <summary>
		/// TelName
		/// </summary>
		public string  TelName
		{
			get
			{
				return GetValStringByKey(CallListAttr.TelName);
			}
			set
			{
				SetValByKey(CallListAttr.TelName,value);
			}
		}
		public string JEOfFile
		{
			get
			{
				return DataType.TurnToFiels(this.JE);
			}
		}
		/// <summary>
		/// JE
		/// </summary>
		public float  JE
		{
			get
			{
				return GetValFloatByKey(CallListAttr.JE);
			}
			set
			{
				this.SetValByKey(CallListAttr.JE,value);
			}
		}
		/// <summary>
		/// �绰����
		/// </summary>
		public int  FK_TelType
		{
			get
			{
				return GetValIntByKey(CallListAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListAttr.FK_TelType,value);
			}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(CallListAttr.Note);
			}
			set
			{
				SetValByKey(CallListAttr.Note,value);
			}
		} 
		/// <summary>
		/// ��������
		/// </summary>
		public string  CallDate
		{
			get
			{
				return GetValStringByKey(CallListAttr.CallDate);
			}
			set
			{
				SetValByKey(CallListAttr.CallDate,value);
			}
		}
		/// <summary>
		/// ������ʱ��
		/// </summary>
		public int CallDegree
		{
			get
			{
				return GetValIntByKey(CallListAttr.CallDegree);
			}
			set
			{
				SetValByKey(CallListAttr.CallDegree,value);
			}
		}
		public string CallDateTime
		{
			get
			{
				return GetValStringByKey(CallListAttr.CallDateTime);
			}
			set
			{
				SetValByKey(CallListAttr.CallDateTime,value);
			}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public int CallingState
		{
			get
			{
				return GetValIntByKey(CallListAttr.CallingState);
			}
			set
			{
				SetValByKey(CallListAttr.CallingState,value);
			}
		}
		public CallingState CallingStateOfEnum
		{
			get
			{
				return (CallingState)this.CallingState;
			}
			set
			{
				SetValByKey(CallListAttr.CallingState,(int)value);
			}
		}
		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����
		/// </summary>
		public CallList()
		{
		}
		#endregion 

		#region Map
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("CTI_CallList");
				//map.DepositaryOfMap=Depositary.Application;
				//map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="�����б�";
			 
				map.AddTBStringPK(CallListAttr.Tel,null,"�绰",true,false,5,12,20);
				map.AddTBString(CallListAttr.TelName,null,"�ͻ�",true,false,0,200,20);
				map.AddTBFloat(CallListAttr.JE,100,"���",true,false);
				map.AddDDLEntities(CallListAttr.FK_TelType,1,DataType.AppInt,"�绰����",new TelTypes(),TelTypeAttr.OID,TelTypeAttr.Name,true);
				//map.AddDDLEntitiesNoName(CallListAttr.FK_Context,"0","��������",new CallContexts(),true);
				map.AddTBInt(CallListAttr.CallDegree,0,"�Ѻ�������",true,false);
				map.AddDDLSysEnum(CallListAttr.CallingState,0,"����״̬",true,false);

				map.AddTBString(CallListAttr.CallDate,DataType.CurrentData, "��������",true,false,0,10,10);
				map.AddTBString(CallListAttr.CallDateTime,DataType.CurrentTime,"����ʱ��",true,false,0,5,5);
				map.AddTBStringDoc(CallListAttr.Note,null,"��ע",true,false);


				if (SystemConfig.CustomerNo == CustomerNoList.LYTax)
				{
					map.AddDDLEntitiesNoName(CallListAttr.FK_ZSJG, BP.Web.TaxUser.FK_ZSJG, "�ؾ�", new BP.Tax.ZSJGs(),false);
					map.AddSearchAttr(CallListAttr.FK_ZSJG);
				}


				map.AddSearchAttr(CallListAttr.FK_TelType);
				map.AddSearchAttr(CallListAttr.CallingState);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		protected override bool beforeUpdateInsertAction()
		{
			this.CallDate=DataType.CurrentData;
			this.CallDateTime=DataType.CurrentTime;
			if (this.HisTelType.MaxCallTime<=this.CallDegree)
				this.CallingStateOfEnum=BP.CTI.App.CallingState.TimeOut;
			return base.beforeUpdateInsertAction ();
		}

		

	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class CallLists :EntitiesOID
	{
		public static DataTable GetCurrentCall
		{
			get
			{
				CallLists cls =new CallLists();
				string time =DateTime.Now.ToString("HH:mm");					 
				QueryObject qo = new QueryObject(cls);
				if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
					qo.Top=1000;
				else
					qo.Top=1000;

				qo.AddWhere(CallListAttr.CallingState,0);
				qo.addAnd();
				qo.AddWhereInSQL(CallListAttr.FK_TelType, "SELECT OID from CTI_TelType where (FromTime1 < '"+time+"'  and ToTime1 > '"+time+"') OR ( FromTime0 < '"+time+"'  and ToTime0 > '"+time+"') ");
				qo.addAnd();
				qo.AddWhere(CallListAttr.JE, ">=",Card.DefaultMinJE);

					 
				// �ж��Ƿ���ȫ���ĵ绰���͡�
				bool isHaveO=false;
				if (Card.dtOfContext==null)
					Card.GetCurrentContextByTelType(0);
				foreach(DataRow dr in Card.dtOfContext.Rows)
				{
					if (dr["�ʺ��û�����"].ToString()=="0")
					{
						isHaveO=true;
					}
				}

				if (isHaveO==false)
				{
					/* ���û��ȫ�� �ĵ绰���� */
					qo.addAnd();
					string sql="SELECT b.FK_TelTypeOfFit   FROM CTI_Schedule a, CTI_Context b WHERE "
						+"a.FK_YF='"+DataType.CurrentMonth+"' and ( a.DateFrom <='"+DataType.CurrentDay+"' and a.DateTo>='"+DataType.CurrentDay+"') and a.FK_Context=b.No";
					qo.AddWhereInSQL(CallListAttr.FK_TelType, sql );
				}
				qo.addOrderBy(CallListAttr.CallDegree);

				//qo.addOrderBy(CallListAttr.Tel);
				//Log.DefaultLogWriteLineInfo(qo.SQL);

				return qo.DoQueryToTable();
			}
		}
 
		 

		#region �õ�һ������
		public static CallLists _HisCallList=null;		
		public static CallLists HisCallList
		{
			get
			{
				if (_HisCallList==null)
					_HisCallList = new CallLists();

				if (_HisCallList.Count==0)
				{
					/*���û�п��Ժ��������ݣ��Ϳ�ʼװ�ڡ�*/

					string time =DateTime.Now.ToString("HH:mm");					 
					QueryObject qo = new QueryObject(_HisCallList);
					if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
						qo.Top=100;
					else
						qo.Top=10;

					qo.AddWhere(CallListAttr.CallingState,0);
					qo.addAnd();
					qo.AddWhereInSQL(CallListAttr.FK_TelType, "SELECT OID from CTI_TelType where (FromTime1 < '"+time+"'  and ToTime1 > '"+time+"') OR ( FromTime0 < '"+time+"'  and ToTime0 > '"+time+"') ");
					qo.addAnd();
					qo.AddWhere(CallListAttr.JE, ">=",Card.DefaultMinJE);

					 
					// �ж��Ƿ���ȫ���ĵ绰���͡�
					bool isHaveO=false;
					if (Card.dtOfContext==null)
						Card.GetCurrentContextByTelType(0);
					foreach(DataRow dr in Card.dtOfContext.Rows)
					{
						if (dr["�ʺ��û�����"].ToString()=="0")
						{
							isHaveO=true;
						}
					}

					if (isHaveO==false)
					{
						/* ���û��ȫ�� �ĵ绰���� */
						qo.addAnd();
						string sql="SELECT b.FK_TelTypeOfFit   FROM CTI_Schedule a, CTI_Context b WHERE "
							+"a.FK_YF='"+DataType.CurrentMonth+"' and ( a.DateFrom <='"+DataType.CurrentDay+"' and a.DateTo>='"+DataType.CurrentDay+"') and a.FK_Context=b.No";
						qo.AddWhereInSQL(CallListAttr.FK_TelType, sql );
					}

					if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
						qo.addOrderBy(CallListAttr.CallDegree);
					else
						qo.addOrderBy(CallListAttr.FK_TelType+" desc " , CallListAttr.CallDegree ); // ���Ⱥ���ҵ�ġ�
					qo.DoQuery();
				}

				return _HisCallList;				
			}
		}
		/// <summary>
		/// �õ�һ������
		/// </summary>
		/// <returns></returns>
		public static CallList GetCall()
		{
			if (HisCallList.Count==0)
			{
				/* ���û�п��Ժ��� */
				return null;
			}
			else
			{
				CallList en= (CallList)HisCallList[0];
				_HisCallList.RemoveEn(en);
				return en;
			}
		}
		#endregion

		#region ����
		#endregion 

		#region ���췽������
		/// <summary>
		/// CallLists
		/// </summary>
		public CallLists(){}
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallList();
			}
		}
		#endregion

		 
	}
}

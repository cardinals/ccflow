using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using BP.Web;
using BP.Port;
 

namespace BP.TA
{
	/// <summary>
	/// ����״̬
	/// </summary>
	public enum WDS
	{
		/// <summary>
		/// δ���
		/// </summary>
		UnComplete,
		/// <summary>
		/// �����
		/// </summary>
		Checking,
		/// <summary>
		/// �������
		/// </summary>
		Over
	}
	/// <summary>
	/// ��������״̬.
	/// </summary>
	public enum WorkDtlState
	{
		/// <summary>
		/// δ�Ķ� 0 
		/// </summary>
		UnRead,
		/// <summary>
		/// ���Ķ� 1
		/// </summary>
		Read,
		/// <summary>
		/// ִ�лظ������� 2
		/// </summary>
		DoReing,
		/// <summary>
		/// ִ���˻ع����� 3
		/// </summary>
		DoReturning,
		/// <summary>
		/// �Իظ���ʽ���� 4
		/// </summary>
		OverByRe,
		/// <summary>
		/// ���˻صķ�ʽ���� 5
		/// </summary>
		OverByReturn,
		/// <summary>
		/// ��ȡ�ķ�ʽ���� 6 
		/// </summary>
		OverByRead
	} 
	/// <summary>
	/// ������ϸ����
	/// </summary>
	public class WorkDtlAttr:WorkDtlBaseAttr
	{
		/// <summary>
		/// ����״̬��ϸ
		/// </summary>
		public const string WorkDtlState="WorkDtlState";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const string WDS="WDS";
		/// <summary>
		/// ��������
		/// </summary>
		public const string DTOfSend="DTOfSend";
		/// <summary>
		/// ʱ����
		/// </summary>
		public const string SpanDays="SpanDays";
		/// <summary>
		/// ���˳ɼ�
		/// </summary>
		public const string Cent="Cent";
	}
	/// <summary>
	/// ������ϸ
	/// </summary> 
	public class WorkDtl :WorkDtlBase
	{
		#region ��չ����
		public int TimeOutDays
		{
			get
			{
				Work wk = this.HisWork ; 
				return DataType.GetSpanDays(this.DTOfActive,wk.DTOfEnd);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SpanDays
		{
			get
			{
				return DataType.GetSpanDays(this.DTOfSend, this.DTOfActive);
			}
		}
		/// <summary>
		/// ���Ļظ��ڵ�
		/// </summary>
		public new ReWork HisReWork
		{
            get
            {
                ReWork nd = new ReWork();
                nd.OID = this.OID;
                nd.ParentID = this.ParentID;
                if (nd.RetrieveFromDBSources() == 0)
                {
                    nd.InsertAsOID(this.OID);
                }
                else
                {
                    return nd;
                }

                Work wk = this.HisWork;
                nd.Title = "��:" + wk.Title;
                nd.Doc = "����" + wk.SenderText + ": \n\n  ���ġ�" + wk.Title + "�� �ִ�������: \n  1��\n  2��     \n\n " + WebUser.Name + "\n" + DataType.CurrentDataTimeCN;
                nd.Executer = this.Executer;
                nd.DTOfActive = DataType.CurrentDataTime;
                nd.Sender = wk.Sender;
                nd.Update();
                return nd;
            }
		}
		/// <summary>
		/// �����˻ؽڵ�
		/// </summary>
		public new ReturnWork HisReturnWork
		{
			get
			{
				ReturnWork nd = new ReturnWork();
				nd.OID=this.OID;
				if (nd.RetrieveFromDBSources()==0 )
				{
					nd.ParentID=this.ParentID;
					nd.Executer=this.Executer;
					nd.Sender=this.Sender;
					nd.DTOfActive=DataType.CurrentDataTime;
					//nd.MyReturnWorkState=ReturnWorkState.None; 
					//	nd.Update();
					nd.InsertAsOID(this.OID);
					return nd;
				}
				else
				{
					return nd;
				}
			}
		}
		/// <summary>
		/// ���ĸ��ڵ�
		/// </summary>
		public new Work HisWork
		{
			get
			{
				Work wk  =new Work(this.ParentID);
				return wk;
			}
		}
		#endregion ��չ����

		#region ��������
		public bool IsOver
		{
			get
			{
				if (this.WorkDtlState==BP.TA.WorkDtlState.OverByRe 
					|| this.WorkDtlState==BP.TA.WorkDtlState.OverByRead
					|| this.WorkDtlState==BP.TA.WorkDtlState.OverByReturn)
					return true;
				else
					return false;
			}
		}
		#endregion

		#region ��������
		
		 
		/// <summary>
		/// ���˳ɼ�
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkDtlAttr.Cent);
			}
			set
			{
				SetValByKey(WorkDtlAttr.Cent,value);
			}
		}
		public string CentText
		{
			get
			{
				if (this.HisWork.MyCheckWay==CheckWay.UnSet)
					return "NULL";
				else
					return this.Cent.ToString();
			}
		}
		 
		/// <summary>
		/// ״̬
		/// </summary>
		public WorkDtlState WorkDtlState
		{
			get
			{
				return (BP.TA.WorkDtlState)this.GetValIntByKey(WorkDtlAttr.WorkDtlState); 
			}
			set
			{
				SetValByKey(WorkDtlAttr.WorkDtlState,(int)value);
			}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public WDS WDS
		{
			get
			{
				return (BP.TA.WDS)this.GetValIntByKey(WorkDtlAttr.WDS); 
			}
			set
			{
				SetValByKey(WorkDtlAttr.WDS,(int)value);
			}
		}

		/// <summary>
		/// ״̬
		/// </summary>
		public string WorkDtlStateText
		{
			get
			{
				return this.GetValRefTextByKey(WorkDtlAttr.WorkDtlState);
			}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public string WDSText
		{
			get
			{
				return this.GetValRefTextByKey(WorkDtlAttr.WDS);
			}
		}
		public string WorkDtlStateImg
		{
			get
			{
                return "<img src='./Images/" + this.WorkDtlState.ToString() + ".gif' border=0 />";
			}
		}
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string DTOfSend
		{
			get
			{
				return this.GetValAppDateByKey(WorkDtlAttr.DTOfSend); 
			}
			set
			{
				SetValByKey(WorkDtlAttr.DTOfSend,value);
			}
		}
		public DateTime DTOfSend_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DTOfSend);
			}
		}
		#endregion
 
		#region ���캯��
		/// <summary>
		/// ��������
		/// </summary>
		public WorkDtl()
		{
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="_No">No</param>
		public WorkDtl(int oid):base(oid)
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

				Map map = new Map("TA_WorkDtl");
				map.EnDesc="��������";
                map.Icon = "../TA/Images/WorkDtl_s.gif";
				map.AddTBIntPKOID();
				map.AddTBInt(WorkDtlAttr.ParentID, 0, "���ڵ�ID",true,false);
                map.AddDDLSysEnum(WorkDtlAttr.WorkDtlState, 0, "����״̬��ϸ", false, false, "WorkDtlState", "@0=δ�Ķ�@1=���Ķ�@2=ִ�лظ�������@3=ִ���˻ع�����@4=�Իظ���ʽ����@5=���˻صķ�ʽ����@6=��ȡ�ķ�ʽ����");
                map.AddDDLSysEnum(WorkDtlAttr.WDS, 0, "����״̬", false, false,  "WDS","@0=δ���@1=�����@2=�������");

				//map.AddDDLEntities(WorkDtlAttr.FK_Work,0,DataType.AppInt,"����", new Works(), WorkAttr.OID, WorkAttr.Title,false);
				map.AddDDLEntities(WorkDtlAttr.Executer,WebUser.No,"ִ����",new Emps(),false);
				map.AddDDLEntities(WorkDtlAttr.Sender,WebUser.No,"��������",new Emps(),false);

				map.AddTBDateTime(WorkDtlAttr.DTOfActive,"�ʱ��",true,true);
				map.AddTBDateTime(WorkDtlAttr.DTOfSend,"����ʱ��",true,true);

				map.AddTBInt(WorkDtlAttr.SpanDays,0,"ʱ����",true,false);
				map.AddTBInt(WorkDtlAttr.Cent,0,"���˳ɼ�",true,false);
				map.AddTBInt(WorkDtlAttr.AdjunctNum,0,"��������",true,true);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region �ظ�
		/// <summary>
		/// ִ�в��Ͽ��˻�
		/// </summary>
		public void DoUnRatifyReWork()
		{
			ReWork rn =this.HisReWork;
            rn.ReWorkState = ReWorkState.UnRatify;
            rn.Update();
           
			this.WorkDtlState = WorkDtlState.UnRead;
			this.Update();
			//this.GenerCheckCent(); // �������˷�
		}
		/// <summary>
		/// ִ���Ͽɻظ�
		/// </summary>
		public void DoRatifyReWork()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.Ratify );
			this.WorkDtlState = WorkDtlState.OverByRe;
			this.Update();
			this.GenerCheckCent(); // �������˷�
            this.HisWork.DoSettleAccounts(); // ���ʡ�
		}
		/// <summary>
		/// �����ظ�
		/// </summary>
		public void DoTakeBackRe()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.None);
			//this.Update( WorkAttr.WorkState,(int)WorkState.Read ); // ����Ϊ�Ѿ��˻�״̬��
			this.WorkDtlState = WorkDtlState.Read;
			this.Update();
			this.GenerCheckCent(); //��������
		}
		#endregion

		#region �˻�
		/// <summary>
		/// ִ�в��Ͽ��˻�
		/// </summary>
		public void DoUnRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.UnRatify, ReturnWorkAttr.DTOfActive,DataType.CurrentDataTime );
			
			this.Update(WorkDtlAttr.WorkDtlState, BP.TA.WorkDtlState.UnRead);
		}
		 
		/// <summary>
		/// ִ���Ͽ��˻�(������ɡ�)
		/// </summary>
		public void DoRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.Ratify,ReturnWorkAttr.DTOfActive,DataType.CurrentDataTime );
			this.Update( WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByReturn ); // ����Ϊ�Ѿ��˻�״̬��
			
			this.GenerCheckCent(); //���ÿ������⡣
			this.HisWork.DoSettleAccounts(); // ���ʡ�
		}
		#endregion

		#region  ��������
		/// <summary>
		/// ִ��ǩ��
		/// </summary>
		public void DoRead()
		{
			if (this.WorkDtlState==BP.TA.WorkDtlState.UnRead)
			{

				if (this.HisWork.IsRe)
				{
					this.Update( WorkDtlAttr.WorkDtlState,(int)WorkDtlState.Read);
				}
				else
				{
					this.Update( WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByRead );
				}
			}


		
		}
		/// <summary>
		/// ִ���ջ�����
		/// </summary>
        public void DoTakeBack()
        {
            Work wk = this.HisWork;
            switch (this.WorkDtlState)
            {
                case WorkDtlState.UnRead:
                    this.Delete();
                    break;
                case WorkDtlState.Read:
                    this.Delete();

                    ReWork re = new ReWork();
                    re.Delete(ReWorkAttr.ParentID, this.ParentID);

                    ReturnWork ret = new ReturnWork();
                    ret.Delete(ReturnWorkAttr.ParentID, this.ParentID);
                    break;
                default:
                    throw new Exception("�����Ѿ���ִ����ȥ�ˣ����������ջ�����");
            }

            WorkDtls dtls = new WorkDtls();
            dtls.Retrieve(WorkDtlAttr.ParentID, this.ParentID);

            if (dtls.Count == 0)
                wk.Delete();
        }
		/// <summary>
		/// ִ���˻�
		/// </summary>
		/// <param name="reason">�˻�ԭ��</param>
		public ReturnWork DoReturn(string reason)
		{
			ReturnWork nd = this.HisReturnWork;

			nd.OID=this.OID;
			nd.ReturnReason = reason;
			nd.DTOfActive=DataType.CurrentDataTime;
			nd.ReturnWorkState = ReturnWorkState.Sending;
			nd.AdjunctNum=nd.HisSysFileManagers.Count;
			nd.Update();
			this.Update(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.DoReturning );  // ���õ�ǰ��״̬Ϊ�˻ء�
			return nd;
		}
		/// <summary>
		/// ִ�г����˻�
		/// </summary>
		public void DoReturnOfRecall()
		{
			/* ִ�г����˻� */
			ReturnWork rn = this.HisReturnWork;			 
			rn.Update(ReturnWorkAttr.ReturnWorkState,(int)BP.TA.ReturnWorkState.None  );
			this.Update(WorkDtlAttr.WorkDtlState,(int)BP.TA.WorkDtlState.DoReturning ); 
			//���ù����ڵ�Ϊ�Ѿ��Ķ�
		}
		/// <summary>
		/// ִ�лظ�
		/// </summary>
		public void DoRe(string title,string Doc)
		{
			ReWork rn = this.HisReWork;
			rn.Title=title;
			rn.Doc=Doc;
			rn.DTOfActive=DataType.CurrentDataTime;
			rn.ReWorkState=BP.TA.ReWorkState.Sending;
			rn.Update();


			this.WorkDtlState = BP.TA.WorkDtlState.DoReing;
			this.AdjunctNum = rn.HisSysFileManagers.Count;
			this.Update();  
		}
		#endregion

		#region ���ڿ���
		public void GenerCheckCent()
		{
			Work wk=this.HisWork;

			// ���û�����ÿ��˾�δ���á�
			if (wk.MyCheckWay==CheckWay.UnSet)
				return;

			this.Cent=0;
			// ���ɿ۷֡�
			switch(this.WorkDtlState)
			{
				case BP.TA.WorkDtlState.OverByRe: //�Իظ�����ʽ�����˴ι�����
					if (wk.MyCheckWay==CheckWay.ByDays)
					{
						int cent=-this.TimeOutDays*wk.CentOfPerDay;
						if (cent >0)
						{
							if (cent>wk.CentOfMax)
								cent=wk.CentOfMax;
						}
						this.Cent=cent; /* ����ǰ����������ˡ�*/
					}
					else if (wk.MyCheckWay==CheckWay.ByOnce )
					{
						if (this.TimeOutDays >=1 )
						{
							/*������ھͿ۷�*/
							this.Cent=-wk.CentOfOnce;
						}
						else
						{
							this.Cent = wk.CentOfOnce ;
						}
					}
					break;
				case BP.TA.WorkDtlState.OverByReturn: //���˻ص���ʽ�����˴ι�����
					this.Cent=0; // û�д���������������ĵ÷־�Ϊ0�� 
					break;
				default:
					throw new Exception("������û�н��������ܽ��нڵ㿼�ˡ�");
			}
			this.Update();

			wk.DoResetWorkState(); // ִ��״̬���á�			
			
		}
		#endregion

		#region ��д����
		protected override bool beforeUpdateInsertAction()
		{
			this.DTOfActive = DataType.CurrentDataTime;

			switch(this.WorkDtlState)
			{
				case BP.TA.WorkDtlState.Read:
				case BP.TA.WorkDtlState.UnRead:
					this.WDS=BP.TA.WDS.UnComplete;
					break;
				case BP.TA.WorkDtlState.DoReing:
				case BP.TA.WorkDtlState.DoReturning:
					this.WDS=BP.TA.WDS.Checking;
					break;
				case BP.TA.WorkDtlState.OverByRe:
				case BP.TA.WorkDtlState.OverByRead:
				case BP.TA.WorkDtlState.OverByReturn:
					this.WDS=BP.TA.WDS.Over;
					break;
				default:
					throw new Exception("no sumch casde.");
			}
			return base.beforeUpdateInsertAction ();
		}

		#endregion

		
	}
	/// <summary>
	/// ������ϸs
	/// </summary> 
	public class WorkDtls: Entities
	{
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkDtl();
			}
		}
		public override int RetrieveAll()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkDtlAttr.WorkDtlState, WebUser.No);
			qo.addOrderBy(WorkDtlAttr.Executer);
			return qo.DoQuery();
		}
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		/// <returns></returns>
		public int StatCOUNT(string key,string val)
		{
			Entity en = this.GetNewEntity;
			string sql="SELECT COUNT(*) FROM "+en.EnMap.PhysicsTable+" WHERE "+en.EnMap.GetFieldByKey(key)+" ='"+val+"'";
			return int.Parse( en.RunSQLReturnTable(sql).Rows[0][0].ToString()) ;
		}
		public int StatCOUNT(string key1,string val1, string key2,string val2 )
		{
			Entity en = this.GetNewEntity;
			string sql="SELECT COUNT(*) FROM "+en.EnMap.PhysicsTable+" WHERE "+en.EnMap.GetFieldByKey(key1)+" ='"+val1+"' AND "+en.EnMap.GetFieldByKey(key2)+" ='"+val2+"'";
			return int.Parse( en.RunSQLReturnTable(sql).Rows[0][0].ToString()) ;
		}
		/// <summary>
		/// WorkDtls
		/// </summary>
		public WorkDtls()
		{
		}
		/// <summary>
		/// WorkDtls
		/// </summary>
		public WorkDtls(WorkDtlState wds)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkDtlAttr.WorkDtlState, (int)wds);
			qo.DoQuery();
		}
		public WorkDtls(string empNo, string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkDtlAttr.Executer, empNo);
			qo.addAnd();
			qo.AddWhere(WorkDtlAttr.DTOfSend, " LIKE ", ny+"%" );
			qo.DoQuery();
		}
		/// <summary>
		/// �����ڴ���Ĺ���
		/// </summary>
		/// <returns></returns>
        public int QueryPending()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkDtlAttr.Executer, WebUser.No);
            qo.addAnd();
            qo.AddWhere(WorkDtlAttr.WorkDtlState, "<=", 1);
            qo.addOrderByDesc(WorkDtlAttr.WorkDtlState);
            return qo.DoQuery();
        }

        public int QueryHistory()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkDtlAttr.Executer, WebUser.No);
            qo.addAnd();
            qo.AddWhere(WorkDtlAttr.WorkDtlState, ">=", 4);
            qo.addOrderByDesc(WorkDtlAttr.WorkDtlState);
            return qo.DoQuery();
        }

        public int QueryRuning()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkDtlAttr.Executer, WebUser.No);
            qo.addAnd();
            qo.AddWhereIn(WorkDtlAttr.WorkDtlState, " ('2','3')");
            qo.addOrderByDesc(WorkDtlAttr.WorkDtlState);
            return qo.DoQuery();
        }
	}
}
 
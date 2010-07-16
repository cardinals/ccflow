
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port;
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// ��ǰ���� ����
	/// </summary>
	public class CurrWorkAttr  
	{
		#region ��������
		/// <summary>
		/// �����ڵ�
		/// </summary>
		public const  string WorkID="WorkID";
        /// <summary>
        /// MyPK
        /// </summary>
        public const string MyPK = "MyPK";
		/// <summary>
		/// �������ݱ��
		/// </summary>
		public const  string FK_Node="FK_Node";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// ��������ǲ��Ƿ���
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// Ӧ�����ʱ��
		/// </summary>
		public const  string SDT="SDT";
		/// <summary>
		/// ��¼����
		/// </summary>
		public const  string RDT="RDT";

		/// <summary>
		/// ��������
		/// </summary>
		public const  string DTOfWarning="DTOfWarning";
		/// <summary>
		/// ��ǰ����
		/// </summary>
		public const  string CurrDate="CurrDate";
		public const  string FK_SJ="FK_SJ";
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// Ԥ����
		/// </summary>
		public const  string OutDays="OutDays";
		/// <summary>
		/// ����ʱ��״̬
		/// </summary>
		public const  string WorkTimeState="WorkTimeState";
        public const string WorkFloor = "WorkFloor";
        public const string FID = "FID";

        

		#endregion
	}
	/// <summary>
	/// ��ǰ����
	/// </summary>
	public class CurrWork :Entity
	{
		#region ��������
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.FK_Emp);
			}
		}
		public string FK_NodeText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.FK_Node);
			}
		}
		public string FK_FlowText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.FK_Flow);
			}
		}
		public int OutDays
		{
			get
			{
				return this.GetValIntByKey( CurrWorkAttr.OutDays);
			}
		}
        public int FID
        {
            get
            {
                return this.GetValIntByKey(CurrWorkAttr.FID);
            }
        }
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(CurrWorkAttr.MyPK);
            }
        }
		public string WorkTimeStateText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.WorkTimeState);
			}
		}
		public int WorkTimeState
		{
			get
			{
				return this.GetValIntByKey(CurrWorkAttr.WorkTimeState);
			}
		}

		/// <summary>
		/// WorkID
		/// </summary>
		public string WorkID
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.WorkID);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.WorkID,value);
			}
		}
		/// <summary>
		/// Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(CurrWorkAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// ������Ա
		/// </summary>
		public Emp HisEmp
		{
			get
			{
				return new Emp(this.FK_Emp);
			}
		}
		public string CurrDate
		{
			get
			{
                return DataType.CurrentData;
				//return this.GetValStringByKey(CurrWorkAttr.CurrDate);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.CurrDate,value);
			}
		}
		
		/// <summary>
		/// Ӧ���������
		/// </summary>
		public string SDT
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.SDT);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.SDT,value);
			}
		}
		public string DTOfWarning
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.DTOfWarning);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.DTOfWarning,value);
			}
		}
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.RDT);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.RDT,value);
			}
		}
		/// <summary>
		/// ������Ա
		/// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CurrWorkAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CurrWorkAttr.FK_Emp, value);
            }
        }
		/// <summary>
		/// FK_Flow
		/// </summary>		 
		public string  FK_Flow
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.FK_Flow );
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.FK_Flow,value);
			}
		}
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(CurrWorkAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CurrWorkAttr.FK_Dept, value);
            }
        }
		/// <summary>
		/// �ڵ�
		/// </summary>
		public Node HisNode
		{
			get
			{
				return new Node(this.FK_Node);
			}
		}
		#endregion

		#region ���캯��
		/// <summary>
		/// CurrWork
		/// </summary>
		public CurrWork()
		{
		}
		/// <summary>
		/// ��д���෽��
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("V_WF_CURRWROKS");
                map.EnDesc = "��ǰ����";
                map.EnType = EnType.App;
                map.DepositaryOfEntity = Depositary.None;

                map.AddDDLEntities(CurrWorkAttr.FK_Dept, null, "��������", new Depts(), false);
                map.AddDDLEntities(CurrWorkAttr.FK_Emp, Web.WebUser.No, "������Ա", new Emps(), false);

                map.AddDDLEntities(CurrWorkAttr.FK_Flow, null, "����", new Flows(), false);
                map.AddDDLEntities(CurrWorkAttr.FK_Node,null, "ͣ���ڵ�", new NodeExts(), false);

                map.AddTBDate(CurrWorkAttr.RDT, "��¼����", false, false);
                map.AddTBDate(CurrWorkAttr.DTOfWarning, "��������", false, false);
                map.AddTBDate(CurrWorkAttr.SDT, "Ӧ�������", false, false);

             //   map.AddTBDate(CurrWorkAttr.CurrDate, "��ǰ����", false, false);
                map.AddTBInt(CurrWorkAttr.OutDays, 0, "������", false, false);

                map.AddTBInt(CurrWorkAttr.FID, 0, "FID", false, false);

                map.AddDDLSysEnum(CurrWorkAttr.WorkTimeState, 0, "״̬", true, false);

                map.AddTBInt("MyNum", 1, "��������", true, false);
                map.AddTBString(CurrWorkAttr.WorkID, "0", "����ID", true, true, 0, 100, 0);


                map.AddTBStringPK(CurrWorkAttr.MyPK, "0", "MyPK", false, false, 0, 500, 0);
                map.AddDDLSysEnum(CurrWorkAttr.WorkFloor, 0, "����", true, false);


                map.AddSearchAttr(CurrWorkAttr.FK_Dept);
                map.AddSearchAttr(CurrWorkAttr.FK_Emp);
                map.AddSearchAttr(CurrWorkAttr.WorkTimeState);
                map.AddSearchAttr(CurrWorkAttr.WorkFloor);


                map.IsShowSearchKey = false;



                RefMethod rm = new RefMethod();
                rm.Title = "��ϸ��Ϣ";
                rm.ClassMethodName = this.ToString() + ".DoWarningDtl()";
                //rm.Icon="../../WFQH/Images/System/Details.ico";
                rm.Width = 0;
                rm.Height = 0;
                rm.HisAttrs = null;
                rm.Target = null;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "��������");  //"��������";
                rm.ClassMethodName = this.ToString() + ".DoShowWorkRpt()";
                //rm.Icon="../../WFQH/Images/System/workinfo.gif";
                rm.Width = 0;
                rm.Height = 0;
                rm.HisAttrs = null;
                rm.Target = null;
                map.AddRefMethod(rm);

                /*
                rm = new RefMethod();
                rm.Title="ִ��";
                rm.ClassMethodName=this.ToString()+".DoWork()";
                rm.Icon="../../WFQH/Images/System/Exe.gif";
                rm.Width=0;
                rm.Height=0;
                rm.HisAttrs=null;
                rm.Target=null;
                map.AddRefMethod(rm);
                */

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		public string DoWarningDtl()
		{
			//if (this.WorkID.ToString().Length >7)
			//return "�˹������������޷�ִ�д˲�����";
			PubClass.WinOpen("../ZF/Warning.aspx?FK_Emp="+this.FK_Emp+"&WorkID="+this.WorkID+"&WorkProgress="+this.WorkTimeState );
			return null;
		}
        public string DoShowWorkRpt()
        {
            if (this.WorkID.ToString().Length > 10)
                return "�˹������������޷�ִ�д˲�����";


            if (this.FK_Dept.Contains("99"))
                return "�������̲�����ʾ�������档";

            PubClass.WinOpen("../../" + SystemConfig.AppName + "/WF/WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID+"&FID="+this.FID);
            return null;
        }

		public string DoWork()
		{
            if (this.WorkID.ToString().Length > 10 )
				return "�˹������������޷�ִ�д˲�����";

            if (this.FK_Dept.Contains("99"))
                return "�������̲��ܲ�����";

            PubClass.WinOpen("../../" + SystemConfig.AppName + "/WF/MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID);
			return null;
		}
	}
	/// <summary>
	/// ��ǰ����
	/// </summary>
	public class CurrWorks: Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CurrWork();
			}
		}
		/// <summary>
		/// CurrWork
		/// </summary>
		public CurrWorks(){} 		 
		#endregion
	}
	
}

using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// �����ʹ�״̬
	/// </summary>
	public enum BookState
	{
		/// <summary>
		/// δ�ʹ�
		/// </summary>
		UnSend,
		/// <summary>
		/// ����δ�ʹ�
		/// </summary>
		UnSendTimeout,
		/// <summary>
		/// ���ʹ�
		/// </summary>
		Send,
		/// <summary>
		/// δ�ҵ���
		/// </summary>
		Notfind,
		/// <summary>
		///�Ѿ��鵵
		/// </summary>
		Pigeonhole
	}
	/// <summary>
	/// ��������
	/// </summary>
	public class BookAttr
	{
		#region ����
        public const string MyPK = "MyPK";
		/// <summary>
		/// ����ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// ��ع���
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
        public const string BookName = "BookName";

		/// <summary>
		/// �ʹ��
		/// </summary>
		public const string BookState="BookState";
		/// <summary>
		/// �˻�ʱ��
		/// </summary>
		public const string ReturnDateTime="ReturnDateTime";
		/// <summary>
		/// BookNo
		/// </summary>
		public const string BookNo="BookNo";
		/// <summary>
		/// �ĺ�
		/// </summary>
		public const string FilePrix="FilePrix";
		public const string FileName="FileName";
		/// <summary>
		/// �鵵����
		/// </summary>
		public const string PigeDate="PigeDate";
		/// <summary>
		/// ������
		/// </summary>
		public const string AJNo="AJNo";
		/// <summary>
		/// ����Ա
		/// </summary>
		public const string BookAdmin="BookAdmin";
		/// <summary>
		/// ��¼ʱ�䣮
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// Ӧ�ʹ�ʱ��
		/// </summary>
		public const string ShouldSendDT="ShouldSendDT";
		/// <summary>
		/// ��¼�ˣ�
		/// </summary>
		public const string Rec="Rec";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_Dept="FK_Dept";
		///<summary>
		///�ʹ���
		///</summary>
		public const string Sender="Sender";
		/// <summary>
		/// ���ʹ���
		/// </summary>
		public const string Accepter="Accepter";
		/// <summary>
		/// �ʹ�ص�
		/// </summary>
		public const string AccepterAddr="AccepterAddr";
		/// <summary>
		/// �ռ�����
		/// </summary>
		public const string AccepterDateTime="AccepterDateTime";

		/// <summary>
		/// �����˴�������
		/// </summary>
		public const string AccepterNote="AccepterNote";
		/// <summary>
		/// ���ʹ��˾������ɺ�����
		/// </summary>
		public const string AccepterDisNote="AccepterDisNote";
		/// <summary>
		/// ��֤��ǩ�������
		/// </summary>
		public const string JZR="JZR";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_NY="FK_NY";
		/// <summary>
		/// ��������
		/// </summary>
		public const string BGQX="BGQX";
        /// <summary>
        /// Ҫ�滻����Ϣ
        /// </summary>
        public const string ReplaceVal = "ReplaceVal";
        public const string FID = "FID";

        
		#endregion
	}
	/// <summary>
	/// ����
	/// </summary> 
    public class Book : Entity
    {
        #region ͳ����Ϣ������
        /// <summary>
        /// δ�ʹ�
        /// </summary>
        public static int NumOfUnSend
        {
            get
            {
                if (int.Parse(DateTime.Now.ToString("hh")) < 9)
                {
                    string sq1l = "UPDATE WF_Book SET BookState=" + (int)BookState.UnSendTimeout + "  WHERE ShouldSendDT > '" + DataType.CurrentData + "' AND  BookState=" + (int)BookState.UnSend;
                    DBAccess.RunSQL(sq1l);
                }
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.UnSend;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// ����δ�͵�
        /// </summary>
        public static int NumOfUnSendTimeout
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND  BookState=" + (int)BookState.UnSendTimeout;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// �Ѿ��ʹ�
        /// </summary>
        public static int NumOfSend
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.Send;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// û�з�����
        /// </summary>
        public static int NumOfNotfind
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.Notfind;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// �Ѿ��鵵
        /// </summary>
        public static int NumOfPigeonhole
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.Pigeonhole;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        ///   �����ʹ�״̬��
        /// </summary>
        public BookState BookState
        {
            get
            {
                return (BookState)GetValIntByKey(BookAttr.BookState);
            }
            set
            {
                SetValByKey(BookAttr.BookState, (int)value);
            }
        }
        public string FilePrix
        {
            get
            {
                return this.GetValStringByKey(BookAttr.FilePrix);
            }
            set
            {
                this.SetValByKey(BookAttr.FilePrix, value);
            }
        }
        public string FileName
        {
            get
            {
                return this.GetValStringByKey(BookAttr.FileName);
            }
            set
            {
                this.SetValByKey(BookAttr.FileName, value);
            }
        }

        public string PigeDate
        {
            get
            {
                return this.GetValStringByKey(BookAttr.PigeDate);
            }
            set
            {
                this.SetValByKey(BookAttr.PigeDate, value);
            }
        }
        public string AJNo
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AJNo);
            }
            set
            {
                this.SetValByKey(BookAttr.AJNo, value);
            }
        }
      
        /// <summary>
        ///  ������
        /// </summary>
        public string BookNo
        {
            get
            {
                return this.GetValStringByKey(BookAttr.BookNo);
            }
            set
            {
                this.SetValByKey(BookAttr.BookNo, value);
            }
        }
        public string FK_NodeRefFuncText
        {
            get
            {
                return this.GetValRefTextByKey(BookAttr.FK_NodeRefFunc);
            }
        }
        public string FK_NodeRefFunc
        {
            get
            {
                return this.GetValStrByKey(BookAttr.FK_NodeRefFunc);
            }
            set
            {
                this.SetValByKey(BookAttr.FK_NodeRefFunc, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(BookAttr.WorkID);
            }
            set
            {
                this.SetValByKey(BookAttr.WorkID, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(BookAttr.FID);
            }
            set
            {
                this.SetValByKey(BookAttr.FID, value);
            }
        }
        /// <summary>
        /// Node
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(BookAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(BookAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// �ʹ�ʱ��
        /// </summary>
        public string AccepterDateTime
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterDateTime);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterDateTime, value);
            }
        }
        public string ShouldSendDT
        {
            get
            {
                return this.GetValStringByKey(BookAttr.ShouldSendDT);
            }
            set
            {
                this.SetValByKey(BookAttr.ShouldSendDT, value);
            }
        }
        /// <summary>
        /// �黹ʱ��
        /// </summary>
        public string ReturnDateTime
        {
            get
            {
                return this.GetValStringByKey(BookAttr.ReturnDateTime);
            }
            set
            {
                this.SetValByKey(BookAttr.ReturnDateTime, value);
            }
        }
        /// <summary>
        /// �����ӡʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(BookAttr.RDT);
            }
            set
            {
                this.SetValByKey(BookAttr.RDT, value);
            }
        }
        /// <summary>
        /// ��ӡ��
        /// </summary>
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(BookAttr.Rec);
            }
            set
            {
                this.SetValByKey(BookAttr.Rec, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(BookAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(BookAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// �ʹ���
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(BookAttr.Sender);
            }
            set
            {
                this.SetValByKey(BookAttr.Sender, value);
            }
        }
        public string Accepter
        {
            get
            {
                return this.GetValStringByKey(BookAttr.Accepter);
            }
            set
            {
                this.SetValByKey(BookAttr.Accepter, value);
            }
        }
        public string AccepterAddr
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterAddr);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterAddr, value);
            }
        }

        public string AccepterDisNote
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterDisNote);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterDisNote, value);
            }
        }
        public string AccepterNote
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterNote);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterNote, value);
            }
        }
        public string JZR
        {
            get
            {
                return this.GetValStringByKey(BookAttr.JZR);
            }
            set
            {
                this.SetValByKey(BookAttr.JZR, value);
            }
        }
        public string BookName
        {
            get
            {
                return this.GetValStringByKey(BookAttr.BookName);
            }
            set
            {
                this.SetValByKey(BookAttr.BookName, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// HisUAC
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = false;
                uac.IsView = true;
                return uac;
            }
        }
        /// <summary>
        /// ��Ŀ
        /// </summary>
        public Book() { }
        public Book(string pk) 
        {
            this.FileName = pk;
            this.Retrieve();
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
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_Book");
                map.DepositaryOfMap = Depositary.None;
                map.EnDesc = "����";

                map.AddTBStringPK(BookAttr.FileName, null, "�����ļ�����", false, false, 1, 100, 5);
                map.AddTBInt(BookAttr.WorkID, 0, "����ID", false, true);
                map.AddTBInt(BookAttr.FID, 0, "FID", false, true);

                map.AddDDLEntities(BookAttr.FK_Node, 0, DataType.AppInt, "��������", new Nodes(), NodeAttr.NodeID, NodeAttr.Name, false);
                map.AddTBString(BookAttr.FilePrix, null, "�ĺ�", true, true, 0, 100, 5);
                map.AddDDLEntities(BookAttr.FK_NodeRefFunc, "", "����", new BookTemplates(), false);
                map.AddTBString(BookAttr.BookName, null, "��������", true, true, 0, 100, 5);
                map.AddTBDate(BookAttr.RDT, "�����ӡʱ��", true, true);
  
                map.AddDDLEntities(BookAttr.Rec, Web.WebUser.No, "��ӡ��", new Emps(), false);
                map.AddTBString(BookAttr.PigeDate, null, "�鵵����", true, true, 0, 100, 5);

                map.AddTBString(BookAttr.AJNo, null, "������", true, true, 0, 100, 5);

                map.AddTBString(BookAttr.BookNo, null, "���", true, true, 0, 100, 5);
                map.AddTBString(BookAttr.BGQX, "10��", "��������", true, true, 0, 100, 5);

                map.AddDDLSysEnum(BookAttr.BookState, 1, "����״̬", false, true);
                map.AddTBDate(BookAttr.ShouldSendDT, "Ӧ�ʹ�ʱ��", true, true);

                //�ʹ���
                map.AddTBString(BookAttr.Sender, null, "�ʹ���", false, true, 0, 100, 5);
                 

                //�ʹ�ص�
                map.AddTBString(BookAttr.Accepter, null, "���ʹ���", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterAddr, null, "�ʹ�ص�", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterDateTime, null, "�ռ�����", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterNote, null, "�����˴�������", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterDisNote, null, "���ʹ��˾������ɺ�����", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.JZR, null, "��֤��ǩ�������", false, true, 0, 100, 5);

                map.AddDDLEntities(BookAttr.FK_Dept, null, "����", new BP.Port.Depts(), false);
                map.AddDDLEntities(BookAttr.FK_NY, DataType.CurrentYearMonth, "��������", new BP.Pub.NYs(), false);
                map.AddTBIntMyNum();

                //����������ѯ
                //map.AddSearchAttr(BookAttr.FK_NY);
                map.AddSearchAttr(BookAttr.FK_Dept);
                map.AddSearchAttr(BookAttr.BookState);
                map.AddSearchAttr(BookAttr.FK_NY);
                map.AddSearchAttr(BookAttr.Rec);
                map.AddSearchAttr(BookAttr.FK_NodeRefFunc);

               // map.AttrsOfSearch.AddFromTo("��ӡ����", BookAttr.RDT, DateTime.Now.AddDays(-15).ToString(DataType.SysDataFormat),
                 //  DataType.CurrentData, 8);

                //map.AddSearchAttr(BookAttr.FK_NodeRefFunc);
                //map.AddSearchAttr(BookAttr.BookState);
                //map.AttrsOfSearch.AddFromTo("����",BookAttr.RDT, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,6);

                RefMethod rm = new RefMethod();
                rm.Title = "��������ʹ�";
                rm.ClassMethodName = this.ToString() + ".DoMarketSend()";
                rm.Icon = "/Images/Btn/do.gif";
                rm.ToolTip = "��������ʹ";
                rm.Warning = "���Ƿ�Ҫ�����������ʹ���";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "�������δ�ҵ���";
                rm.ClassMethodName = this.ToString() + ".DoMarketNotfind()";
                rm.Icon = "/Images/Btn/AlertBell.gif";
            //    rm.ToolTip = "��������ʹ";
                rm.Warning = "���Ƿ�Ҫ����������δ�ҵ�����";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);

                    
                rm = new RefMethod();
                rm.Title = "�鿴����";
                rm.ClassMethodName = this.ToString() + ".DoPrint()";
                rm.Icon = "/Images/Btn/Word.gif";
                rm.ToolTip = "�鿴���顣";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);


                //rm = new RefMethod();
                //rm.Title = "һ��ʽ����";
                //rm.ClassMethodName = this.ToString() + ".DoYHS()";
                //rm.Icon = "/Images/Btn/Authorize.gif";
                //rm.ToolTip = "һ��ʽ���ϡ�";
                //rm.Width = 0;
                //rm.Height = 0;
                //rm.Target = null;
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "��������"); // "��������";
                rm.ClassMethodName = this.ToString() + ".DoWorkRpt()";
                rm.Icon = "/Images/Btn/Authorize.gif";
                rm.ToolTip = "�������档";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region �����鹦�ܴ���
        public string DoMarketSend()
        {
            this.BookState = BookState.Send;
            this.Update();
            return "�Ѿ���������ʹ";
        }
        public string DoMarketNotfind()
        {
            this.BookState = BookState.Notfind;
            this.Update();
            return "�Ѿ��������δ�ҵ��ˡ�";
        }
        #endregion


        #region ��ӡ����
        public string DoWorkRpt()
        {
            Node nd = new Node(this.FK_Node);
            PubClass.WinOpen("../WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow=" + nd.FK_Flow + "&FlowNo=" + nd.FK_Flow + "&NodeId=" + this.FK_Node);
            return null;
        }
        public string DoYHS()
        {
            //string url = "/"+SystemConfig.AppName+"/Comm/UIEn.aspx?EnsName=BP.Port.TaxpayerDtls&PK=" + this.FK_Taxpayer;
            //PubClass.WinOpen(url);
            return null;
        }
        /// <summary>
        /// ִ�д�ӡ
        /// </summary>
        /// <returns></returns>
        public string DoPrint()
        {
            //string script = "<a href=\"javascript:WinOpem(+ "&1" + "', '0' );\"  ><img src='../../Images/Btn/WORD.gif' border=0 /> ��</a>";
            string path = this.FileName;

            path = path.Replace("_"+this.WorkID+".doc", "");
            path = path.Replace("_", "/");



            string url = "/"+System.Web.HttpContext.Current.Request.ApplicationPath+"/FlowFile/" + path +"/"+ this.FileName;
            PubClass.WinOpen(url);
            return null;

            // string script = "Run2('C:\\\\ds2002\\\\OpenBook.exe', '" + this.FileName + "', '1', '0');";
            // string script = "Run('C:\\\\ds2002\\\\OpenBook.exe', '" + this.FileName + "', '0' );";
            // PubClass.ResponseWriteScript(script);
            // return null;
            // PubClass.WinOpen("../WF/NodeRefFunc.aspx?NodeRefFuncOID=" + this.FK_NodeRefFunc + "&WorkFlowID=" + this.WorkID + "&FlowNo=" + nd.FK_Flow + "&NodeId=" + this.FK_Node);
            //  return null;
        }
        #endregion
    }
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class Books :Entities
	{
		#region ���췽������
		/// <summary>
		/// Books
		/// </summary>
		public Books(){}
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Book();
			}
		}
		#endregion
	}
}

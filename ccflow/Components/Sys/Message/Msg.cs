using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ��Ϣ״̬
    /// </summary>
    public enum MsgSta
    {
        /// <summary>
        /// δ�Ķ�
        /// </summary>
        UnRead,
        /// <summary>
        /// �Ѿ��Ķ�
        /// </summary>
        Read,
        /// <summary>
        /// �Ѿ��ظ�
        /// </summary>
        Reply
    }
	/// <summary>
	/// ��Ϣ
	/// </summary>
    public class MsgAttr
    {
        /// <summary>
        /// OID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ����
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        /// ������
        /// </summary>
        public const string Accepter = "Accepter";
        /// <summary>
        /// ״̬
        /// </summary>
        public const string MsgSta = "MsgSta";
        /// <summary>
        /// ODT
        /// </summary>
        public const string ODT = "ODT";
       /// <summary>
       /// ������
       /// </summary>
        public const string SenderT = "SenderT";
        /// <summary>
        /// ��Ϣ��ID
        /// </summary>
        public const string MsgID = "MsgID";
    }
	/// <summary>
	/// ��Ϣ
	/// </summary> 
	public class Msg:EntityOID 
	{
		#region ��������
        /// <summary>
        /// ��ϢID
        /// </summary>
        public string MsgID
        {
            get
            {
                return this.GetValStringByKey(MsgAttr.MsgID);
            }
            set
            {
                this.SetValByKey(MsgAttr.MsgID, value);
            }
        }
		/// <summary>
		/// ����������
		/// </summary>
		public  string  SenderText
		{
            get
			{
				return this.GetValStringByKey(MsgAttr.SenderT);
			}
			set
			{
                this.SetValByKey(MsgAttr.SenderT, value);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public  string  Sender
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Sender);
			}
			set
			{
				this.SetValByKey(MsgAttr.Sender,value);
			}
		}
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public  string  RDT
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.RDT);
			}
			set
			{
				this.SetValByKey(MsgAttr.RDT,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Title
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Title);
			}
			set
			{
				this.SetValByKey(MsgAttr.Title,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Doc
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Doc);
			}
			set
			{
				this.SetValByKey(MsgAttr.Doc,value);
			}
		}
        /// <summary>
        /// ��Ϣ
        /// </summary>
		public  string  DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(MsgAttr.Doc);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public  string  Accepter
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Accepter);
			}
			set
			{
				this.SetValByKey(MsgAttr.Accepter,value);
			}
		}
		/// <summary>
		/// �Ķ���
		/// </summary>
        public MsgSta HisMsgSta
		{
			get
			{
                return (MsgSta)this.GetValIntByKey(MsgAttr.MsgSta);
			}
			set
			{
				this.SetValByKey(MsgAttr.MsgSta,(int)value);
			}
		}
		#endregion 

        #region ����
        public Msg(int oid)
            : base(oid)
        {

        }
        public string DoSend(string accepter, string doc)
        {
            this.HisMsgSta = MsgSta.UnRead;
            this.Sender = Web.WebUser.No;
            this.Accepter = accepter;
            this.Doc = doc;
            this.RDT = DataType.CurrentDataTimeCN;
            this.Insert();
            return "���ͳɹ�<hr>��Ϣ�ɹ��ķ��͸�:" + accepter;
        }
        public string DoSend(string accepter, string doc, string msgID)
        {
            if (this.Retrieve(MsgAttr.MsgID, msgID) != 0)
                return "���ͳɹ�<hr>��Ϣ�ɹ��ķ��͸�:" + accepter;

            this.HisMsgSta = MsgSta.UnRead;
            this.Sender = Web.WebUser.No;
            this.Accepter = accepter;
            this.Doc = doc;
            this.RDT = DataType.CurrentDataTimeCN;
            this.Insert();
            return "���ͳɹ�<hr>��Ϣ�ɹ��ķ��͸�:" + accepter;
        }
        #endregion

        #region ���췽��
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
		/// ��Ϣ
		/// </summary>
		public Msg(){}
		 
		/// <summary>
		/// Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_Msg");
                map.EnDesc = "��Ϣ";
                map.EnType = EnType.Admin;

                map.AddTBIntPKOID();

                map.AddTBString(MsgAttr.MsgID, null, "��ϢID", true, false, 0, 500, 20);


                map.AddTBString(MsgAttr.Sender, null, "������", true, false, 0, 500, 20);
                map.AddTBString(MsgAttr.SenderT, null, "������T", true, false, 0, 500, 20);

                map.AddTBString(MsgAttr.Accepter, null, "������", true, false, 0, 500, 20);
                map.AddDDLSysEnum(MsgAttr.MsgSta, 0, "״̬", true, false, "MsgSta", "@0=δ��@1=�Ѷ�");

                map.AddTBString(MsgAttr.Title, null, "����", true, false, 0, 500, 20);
                map.AddTBStringDoc(MsgAttr.Doc, null, "����", true, false);

                map.AddTBDateTime(MsgAttr.ODT, "��ʱ��", true, false);
                map.AddTBDateTime(MsgAttr.RDT, "����ʱ��", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// ��Ϣ
	/// </summary> 
    public class Msgs : EntitiesOID
    {
        #region ���캯��
        /// <summary>
        /// ����ʵ����ʵĹ���
        /// </summary>
        public Msgs()
        {
        }
        /// <summary>
        /// New entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Msg();
            }
        }
        #endregion
    }
}

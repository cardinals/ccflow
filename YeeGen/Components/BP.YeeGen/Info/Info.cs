using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
namespace BP.YG
{
	/// <summary>
	/// ��Ѷ
	/// </summary>
	public class InfoAttr
	{
		#region ��������
        public const string Title = "Title";
		public const string ReadTimes="ReadTimes";
        public const string Doc = "Doc";
        public const string RDT = "RDT";
        public const string ShareType = "ShareType";
        public const string Author = "Author";

        public const string FK_Item = "FK_Item";
        public const string FK_Model = "FK_Model";

        #endregion
    }
	/// <summary>
	/// ��Ѷ
	/// </summary>
	public class Info :EntityOID
	{	
		#region ��������
        /// <summary>
        /// �Ķ�����
        /// </summary>
		public int ReadTimes
		{
			get
			{
				return this.GetValIntByKey(InfoAttr.ReadTimes);
			}
			set
			{
				this.SetValByKey(InfoAttr.ReadTimes,value);
			}
		}
        public string FK_Item
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.FK_Item);
            }
            set
            {
                this.SetValByKey(InfoAttr.FK_Item, value);
            }
        }
        public string FK_Model
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.FK_Model);
            }
            set
            {
                this.SetValByKey(InfoAttr.FK_Model, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Author
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.Author);
            }
            set
            {
                this.SetValByKey(InfoAttr.Author, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
		{
			get
			{
				return this.GetValStringByKey(InfoAttr.Title);
			}
			set
			{
                this.SetValByKey(InfoAttr.Title, value);
			}
		}
        /// <summary>
        /// ����
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.Doc);
            }
            set
            {
                this.SetValByKey(InfoAttr.Doc, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.RDT);
            }
            set
            {
                this.SetValByKey(InfoAttr.RDT, value);
            }
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
		/// ��Ѷ
		/// </summary>		
		public Info()
		{
		}
		/// <summary>
		/// InfoMap
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "YG_Info";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "��Ѷ";
                map.EnType = EnType.App;

                map.AddTBIntPKOID();

                map.AddTBString(InfoAttr.Title, null, "����", true, false, 0, 300, 200, true);
                map.AddTBStringDoc();// (InfoAttr.Doc, null, "����", true, false, true);

                map.AddDDLSysEnum(InfoAttr.ShareType, 0, "�鿴Ȩ��", true, true, InfoAttr.ShareType, "@0=�κ���@1=��ͨ��Ա@2=�׽��Ա@3=�ƽ��Ա@4=��ʯ��Ա");
                map.AddTBString(InfoAttr.Author, null, "����", true, false, 0, 50, 200);
                map.AddTBInt(InfoAttr.ReadTimes, 0, "�Ķ�����", true, false);
                map.AddTBDate(InfoAttr.RDT, null, "��������", true, true);

                map.AddDDLEntities(InfoAttr.FK_Item, null, "��ϸ", new InfoItems(), true);
                map.AddDDLEntities(InfoAttr.FK_Model, "01", "ģ��", new InfoModels(), false);

                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public void UpdateReadTimes()
		{
		}
		protected override bool beforeUpdateInsertAction()
		{
            InfoItem it = new InfoItem(this.FK_Item);
            this.FK_Model = it.FK_Model;

            this.RDT = DataType.CurrentData;
			return base.beforeUpdateInsertAction ();
		}
		#endregion 
	}
	/// <summary>
	/// ��Ѷ
	/// </summary>
    public class Infos : EntitiesOID
	{
		#region Entity
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Info();
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��Ѷ
		/// </summary>
		public Infos()
		{
		}
		#endregion

        public int Search(int stat, string fk_tp)
        {
            QueryObject qo = new QueryObject(this);
            return qo.DoQuery();
        }
	}
	
}

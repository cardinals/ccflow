using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.TA
{
	/// <summary>
	/// �����¼�����
	/// </summary>
    public class CycleEventDoAttr : EntityOIDAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_CEDtl = "FK_CEDtl";
        /// <summary>
        /// FK_CE
        /// </summary>
        public const string FK_CycleEvent = "FK_CycleEvent";
        /// <summary>
        /// ��ʼѭ������
        /// </summary>
        public const string RefData = "RefData";
        /// <summary>
        /// �Ƿ���
        /// </summary>
        public const string IsBM = "IsBM";
        /// <summary>
        /// Doc
        /// </summary>
        public const string Doc = "Doc";
        public const string FK_Emp = "FK_Emp";
        public const string RDT = "RDT";
    }
	/// <summary>
	/// �����¼�
	/// </summary> 
    public class CycleEventDo : EntityOID
    {
        public string FileInfo
        {
            get
            {
                return this.GetValStrByKey("MyFileExt");
            }
        }
        public string MyFileExt
        {
            get
            {
                return this.GetValStrByKey("MyFileExt");
            }
            set
            {
                SetValByKey("MyFileExt", value);
            }
        }
        public string MyFileName
        {
            get
            {
                return this.GetValStrByKey("MyFileName");
            }
            set
            {
                SetValByKey("MyFileName", value);
            }
        }
        #region  ����
      
        public int FK_CycleEvent
        {
            get
            {
                return this.GetValIntByKey(CycleEventDoAttr.FK_CycleEvent);
            }
            set
            {
                SetValByKey(CycleEventDoAttr.FK_CycleEvent, value);
            }
        }
        public string FK_CEDtl
        {
            get
            {
                return this.GetValStringByKey(CycleEventDoAttr.FK_CEDtl);
            }
            set
            {
                SetValByKey(CycleEventDoAttr.FK_CEDtl, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string RefData
        {
            get
            {
                return this.GetValStringByKey(CycleEventDoAttr.RefData);
            }
            set
            {
                SetValByKey(CycleEventDoAttr.RefData, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(CycleEventDoAttr.RDT);
            }
            set
            {
                SetValByKey(CycleEventDoAttr.RDT, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CycleEventDoAttr.FK_Emp);
            }
            set
            {
                SetValByKey(CycleEventDoAttr.FK_Emp, value);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(CycleEventDoAttr.FK_Emp);
            }
        }

        public bool IsBM
        {
            get
            {
                return this.GetValBooleanByKey(CycleEventDoAttr.IsBM);
            }
            set
            {
                SetValByKey(CycleEventDoAttr.IsBM, value);
            }
        }

        /// <summary>
        /// �Ƿ���
        /// </summary>
        public bool IsBaoMi(string creater)
        {

            bool isBM = this.GetValBooleanByKey(CycleEventDoAttr.IsBM);
            if (isBM == false)
                return false;

            if (Web.WebUser.No == this.FK_Emp)
                return false;

            if (Web.WebUser.No == creater)
                return false;

            return true;
        }

        public string Doc
        {
            get
            {
                return this.GetValStringByKey(CycleEventDoAttr.Doc);
            }
            set
            {
                SetValByKey(CycleEventDoAttr.Doc, value);
            }
        }
        public string DocHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(CycleEventDoAttr.Doc);
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
        /// �����¼�
        /// </summary>
        public CycleEventDo()
        {
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="_No">No</param>
        public CycleEventDo(int oid)
            : base(oid)
        {
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("TA_CycleEventDo");
                map.EnDesc = "�����¼�����";
                map.AddTBIntPKOID();
                map.Icon = "./Images/CycleEventDo_s.ico";

                map.AddTBIntPKOID();
                map.AddTBString(CycleEventDoAttr.FK_CEDtl, null, "FK_CEDtl", true, false, 0, 20, 15);
                map.AddTBString(CycleEventDoAttr.RefData, null, "RefData", true, false, 0, 20, 15);

                map.AddDDLEntities(CycleEventDoAttr.FK_Emp, null, "FK_Emp", new Emps(), false);

                map.AddTBInt(CycleEventDoAttr.FK_CycleEvent, 0, "FK_CE", true, true);
                map.AddBoolean(CycleEventDoAttr.IsBM, true, "�Ƿ���", true, true);


                map.AddTBDate(CycleEventDoAttr.RDT, null, "RDT", true, true);
                map.AddTBStringDoc();
                map.AddMyFile();

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �����¼�s
	/// </summary> 
	public class CycleEventDos: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new CycleEventDo();
			}
		}
		public CycleEventDos()
		{

		}
		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="emp">��Ա</param>
		/// <param name="RefData">��ʼ����</param>
		public CycleEventDos(int fk_cycle, string fk_emp)
		{
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(CycleEventDoAttr.FK_CycleEvent, fk_cycle);
            qo.addAnd();
            qo.AddWhere(CycleEventDoAttr.FK_CycleEvent, fk_cycle);


            qo.addOrderByDesc(CycleEventDoAttr.OID);
            qo.DoQuery();
		}
	}
}
 
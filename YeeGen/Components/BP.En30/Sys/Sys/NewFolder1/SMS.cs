using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// ����Ϣ
	/// </summary>
    public class SMSAttr
    {
        /// <summary>
        /// OID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// ������
        /// </summary>
        public const string Asker = "Asker";
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
        public const string Docs = "Docs";
        /// <summary>
        /// dtl num
        /// </summary>
        public const string DtlNum = "DtlNum";
        /// <summary>
        /// ReadNum
        /// </summary>
        public const string ReadNum = "ReadNum";
    }
	/// <summary>
	/// ����Ϣ
	/// </summary> 
    public class SMS : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// AskerText
        /// </summary>
        public string AskerText
        {
            get
            {
                return this.GetValRefTextByKey(SMSAttr.Asker);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Asker
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Asker);
            }
            set
            {
                this.SetValByKey(SMSAttr.Asker, value);
            }
        }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.RDT);
            }
            set
            {
                this.SetValByKey(SMSAttr.RDT, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Title);
            }
            set
            {
                this.SetValByKey(SMSAttr.Title, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSAttr.Docs, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSAttr.Docs, value);
            }
        }
        public string DocsHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(SMSAttr.Docs);
            }
        }
        /// <summary>
        /// dtl Num
        /// </summary>
        public int DtlNum
        {
            get
            {
                return this.GetValIntByKey(SMSAttr.DtlNum);
            }
            set
            {
                this.SetValByKey(SMSAttr.DtlNum, value);
            }
        }
        /// <summary>
        /// �Ķ���
        /// </summary>
        public int ReadNum
        {
            get
            {
                return this.GetValIntByKey(SMSAttr.ReadNum);
            }
            set
            {
                this.SetValByKey(SMSAttr.ReadNum, value);
            }
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
        /// ����Ϣ
        /// </summary>
        public SMS() { }
        /// <summary>
        /// ����Ϣ
        /// </summary>
        /// <param name="oid"></param>
        public SMS(string oid)
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
                Map map = new Map("Sys_SMS");
                map.EnDesc = "����Ϣ";
                map.EnType = EnType.Admin;

                map.AddMyPK();
                map.AddDDLEntities(SMSAttr.Asker, Web.WebUser.No, "������", new Emps(), false);
                map.AddTBString(SMSAttr.Title, null, "����", true, false, 0, 500, 20);
                map.AddTBStringDoc(SMSAttr.Docs, null, "��������", true, false);
                map.AddTBDateTime(SMSAttr.RDT, "����ʱ��", true, false);
                map.AddTBInt(SMSAttr.DtlNum, 0, "�ش����", true, false);
                map.AddTBInt(SMSAttr.ReadNum, 0, "�Ķ���", true, false);

                //map.AddDtl( new SMSDtls(), SMSDtlAttr.FK_SMS);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����Ϣ
	/// </summary> 
	public class SMSs : EntitiesMyPK
	{
		#region ���캯��
		/// <summary>
		/// ����ʵ����ʵĹ���
		/// </summary>
		public SMSs()
		{
		}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SMS();
			}
		}
		#endregion
	
	}
}

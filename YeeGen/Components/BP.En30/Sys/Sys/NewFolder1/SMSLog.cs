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
    public class SMSLogAttr
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
    public class SMSLog : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// AskerText
        /// </summary>
        public string AskerText
        {
            get
            {
                return this.GetValRefTextByKey(SMSLogAttr.Asker);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Asker
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Asker);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Asker, value);
            }
        }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.RDT);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.RDT, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Title);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Title, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Docs, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Docs, value);
            }
        }
        public string DocsHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(SMSLogAttr.Docs);
            }
        }
        /// <summary>
        /// dtl Num
        /// </summary>
        public int DtlNum
        {
            get
            {
                return this.GetValIntByKey(SMSLogAttr.DtlNum);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.DtlNum, value);
            }
        }
        /// <summary>
        /// �Ķ���
        /// </summary>
        public int ReadNum
        {
            get
            {
                return this.GetValIntByKey(SMSLogAttr.ReadNum);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.ReadNum, value);
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
        public SMSLog() { }
        /// <summary>
        /// ����Ϣ
        /// </summary>
        /// <param name="oid"></param>
        public SMSLog(string oid)
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
                Map map = new Map("Sys_SMSLog");
                map.EnDesc = "����Ϣ";
                map.EnType = EnType.Admin;

                map.AddMyPK();
                map.AddDDLEntities(SMSLogAttr.Asker, Web.WebUser.No, "������", new Emps(), false);
                map.AddTBString(SMSLogAttr.Title, null, "����", true, false, 0, 500, 20);
                map.AddTBStringDoc(SMSLogAttr.Docs, null, "��������", true, false);
                map.AddTBDateTime(SMSLogAttr.RDT, "����ʱ��", true, false);
                map.AddTBInt(SMSLogAttr.DtlNum, 0, "�ش����", true, false);
                map.AddTBInt(SMSLogAttr.ReadNum, 0, "�Ķ���", true, false);

                //map.AddDtl( new SMSLogDtls(), SMSLogDtlAttr.FK_SMSLog);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ����Ϣ
	/// </summary> 
    public class SMSLogs : EntitiesMyPK
    {
        #region ���캯��
        /// <summary>
        /// ����ʵ����ʵĹ���
        /// </summary>
        public SMSLogs()
        {
        }
        /// <summary>
        /// New entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SMSLog();
            }
        }
        #endregion
    }
}

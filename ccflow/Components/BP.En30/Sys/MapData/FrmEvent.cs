using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.Sys
{
    public enum EventDoType
    {
        /// <summary>
        /// ����
        /// </summary>
        Disable,
        /// <summary>
        /// ִ�д洢����.
        /// </summary>
        SP,
        /// <summary>
        /// ִ��SQL
        /// </summary>
        SQL,
        /// <summary>
        /// ִ��URL
        /// </summary>
        URL,
        /// <summary>
        /// WebServices
        /// </summary>
        WebServices,
        /// <summary>
        /// EXE
        /// </summary>
        EXE,
        /// <summary>
        /// JS
        /// </summary>
        Javascript
    }
    public class EventListFrm
    {
        /// <summary>
        /// ����ǰ
        /// </summary>
        public const string FrmLoadBefore = "FrmLoadBefore";
        /// <summary>
        /// �����
        /// </summary>
        public const string FrmLoadAfter = "FrmLoadAfter";
        /// <summary>
        /// ����ǰ
        /// </summary>
        public const string SaveBefore = "SaveBefore";
        /// <summary>
        /// �����
        /// </summary>
        public const string SaveAfter = "SaveAfter";
    }
    /// <summary>
    /// �¼�����б�
    /// </summary>
    public class EventListOfNode : EventListFrm
    {
        #region �ڵ��¼�
      
        /// <summary>
        /// �ڵ㷢��ǰ
        /// </summary>
        public const string SendWhen = "SendWhen";
        /// <summary>
        /// �ڵ㷢�ͳɹ�
        /// </summary>
        public const string SendSuccess = "SendSuccess";
        /// <summary>
        /// �ڵ㷢��ʧ��
        /// </summary>
        public const string SendError = "SendError";
        /// <summary>
        /// ���ڵ��˻�ǰ
        /// </summary>
        public const string ReturnBefore = "ReturnBefore";
        /// <summary>
        /// ���ڵ��˺�
        /// </summary>
        public const string ReturnAfter = "ReturnAfter";
        /// <summary>
        /// ���ڵ㳷������ǰ
        /// </summary>
        public const string UndoneBefore = "UndoneBefore";
        /// <summary>
        /// ���ڵ㳷�����ͺ�
        /// </summary>
        public const string UndoneAfter = "UndoneAfter";
        #endregion �ڵ��¼�

        #region �����¼�
        /// <summary>
        /// �������ʱ
        /// </summary>
        public const string WhenFlowOver = "WhenFlowOver";
        /// <summary>
        /// ����ɾ��ǰ
        /// </summary>
        public const string BeforeFlowDel = "BeforeFlowDel";
        /// <summary>
        /// ����ɾ����
        /// </summary>
        public const string AfterFlowDel = "AfterFlowDel";
        #endregion �����¼�
    }
	/// <summary>
	/// �¼�����
	/// </summary>
    public class FrmEventAttr
    {
        /// <summary>
        /// �¼�����
        /// </summary>
        public const string FK_Event = "FK_Event";
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// ִ������
        /// </summary>
        public const string DoType = "DoType";
        /// <summary>
        /// ִ������
        /// </summary>
        public const string DoDoc = "DoDoc";
        /// <summary>
        /// ��ǩ
        /// </summary>
        public const string MsgOK = "MsgOK";
        /// <summary>
        /// ִ�д�����ʾ
        /// </summary>
        public const string MsgError = "MsgError";
    }
	/// <summary>
	/// �¼�
	/// �ڵ�Ľڵ㱣���¼������������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
    public class FrmEvent : EntityMyPK
    {
        #region ��������
        public override En.UAC HisUAC
        {
            get
            {
                UAC uac = new En.UAC();
                uac.IsAdjunct = false;
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// �ڵ�
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.FK_MapData, value);
            }
        }
        public string DoDoc
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.DoDoc).Replace("~","'");
            }
            set
            {
                string doc = value.Replace("'","~");
                this.SetValByKey(FrmEventAttr.DoDoc, doc);
            }
        }
        /// <summary>
        /// ִ�гɹ���ʾ
        /// </summary>
        public string MsgOK(Entity en)
        {
            string val = this.GetValStringByKey(FrmEventAttr.MsgOK);
            if (val.Trim() == "")
                return null;

            if (val.IndexOf('@') == -1)
                return val;

            foreach (Attr attr in en.EnMap.Attrs)
            {
                val = val.Replace("@" + attr.Key, en.GetValStringByKey(attr.Key));
            }
            return val;
        }
        public string MsgOKString
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.MsgOK);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.MsgOK, value);
            }
        }
        public string MsgErrorString
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.MsgError);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.MsgError, value);
            }
        }
        /// <summary>
        /// ������쳣��ʾ
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public string MsgError(Entity en)
        {
            string val = this.GetValStringByKey(FrmEventAttr.MsgError);
            if (val.Trim() == "")
                return null;

            if (val.IndexOf('@') == -1)
                return val;

            foreach (Attr attr in en.EnMap.Attrs)
            {
                val = val.Replace("@" + attr.Key, en.GetValStringByKey(attr.Key));
            }
            return val;
        }
 
        public string FK_Event
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.FK_Event);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.FK_Event, value);
            }
        }
        /// <summary>
        /// ִ������
        /// </summary>
        public EventDoType HisDoType
        {
            get
            {
                return (EventDoType)this.GetValIntByKey(FrmEventAttr.DoType);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.DoType, (int)value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �¼�
        /// </summary>
        public FrmEvent()
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

                Map map = new Map("Sys_FrmEvent");
                map.EnDesc = "�¼�";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddMyPK();
                 
                map.AddTBString(FrmEventAttr.FK_Event, null, "�¼�����", true, true, 0, 400, 10);
                map.AddTBString(FrmEventAttr.FK_MapData, null, "FK_MapData", true, true, 0, 400, 10);

                map.AddTBInt(FrmEventAttr.DoType, 0, "�¼�����", true, true);
                map.AddTBString(FrmEventAttr.DoDoc, null, "ִ������", true, true, 0, 400, 10);
                map.AddTBString(FrmEventAttr.MsgOK, null, "�ɹ�ִ����ʾ", true, true, 0, 400, 10);
                map.AddTBString(FrmEventAttr.MsgError, null, "�쳣��Ϣ��ʾ", true, true, 0, 400, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_MapData + "_" + this.FK_Event;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// �¼�
	/// </summary>
    public class FrmEvents : EntitiesOID
    {
        /// <summary>
        /// ִ���¼����¼������ EventList.
        /// </summary>
        /// <param name="dotype"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public string DoEventNode(string dotype, Entity en)
        {
            if (this.Count == 0)
                return null;

            FrmEvent nev = this.GetEntityByKey(FrmEventAttr.FK_Event, dotype) as FrmEvent;
            if (nev == null || nev.HisDoType == EventDoType.Disable)
                return null;

            string doc = nev.DoDoc.Trim();
            if (doc == null || doc == "")
                return null;

            #region ����ִ������
            Attrs attrs = en.EnMap.Attrs;

            string MsgOK = "";
            string MsgErr = "";
            foreach (Attr attr in attrs)
            {
                if (doc.Contains("@" + attr.Key) == false)
                    continue;
                if (attr.MyDataType == DataType.AppString)
                    doc = doc.Replace("@" + attr.Key, "'" + en.GetValStrByKey(attr.Key) + "'");
                else
                    doc = doc.Replace("@" + attr.Key, en.GetValStrByKey(attr.Key));
            }
            doc = doc.Replace("~", "'");

            if (nev.HisDoType == EventDoType.URL)
            {
                //doc += "&FK_Flow=" + nev.FK_Flow.ToString();
                //doc += "&FK_MapData=" + nev.FK_MapData.ToString();
                //doc += "&WorkID=" + en.GetValStrByKey("OID");
                //doc += "&FID=" + en.GetValStrByKey("FID");

                doc += "&UserNo=" + WebUser.No;
                doc += "&SID=" + WebUser.SID;
                doc += "&FK_Dept=" + WebUser.FK_Dept;
                doc += "&FK_Unit=" + WebUser.FK_Unit;
            }
            #endregion ����ִ������

            switch (nev.HisDoType)
            {
                case EventDoType.SP:
                    try
                    {
                        DBAccess.RunSP(doc);
                        return nev.MsgOK(en);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(nev.MsgError(en) + " Error:" + ex.Message);
                    }
                    break;
                case EventDoType.SQL:
                    try
                    {
                        DBAccess.RunSQL(doc);
                        return nev.MsgOK(en);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(  nev.MsgError(en) + " Error:" + ex.Message);
                    }
                    break;
                case EventDoType.URL:
                    doc = doc.Replace("@AppPath", System.Web.HttpContext.Current.Request.ApplicationPath);
                    try
                    {
                        string text = DataType.ReadURLContext(doc, 800, System.Text.Encoding.UTF8);
                        if (text != null && text.Substring(0, 7).Contains("Err"))
                            throw new Exception(text);

                        if (text == null || text.Trim() == "")
                            return null;
                        return text;

                        //Log.DebugWriteInfo(doc + " ------ " + text);
                        //return "@" + nev.MsgOK + text;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@" + nev.MsgError(en) + " Error:" + ex.Message);
                    }
                    break;
                default:
                    throw new Exception("@no such way.");
            }
        }
        /// <summary>
        /// �¼�
        /// </summary>
        public FrmEvents() { }
        /// <summary>
        /// �¼�
        /// </summary>
        /// <param name="FK_MapData">FK_MapData</param>
        public FrmEvents(string fk_MapData)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FrmEventAttr.FK_MapData, fk_MapData);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmEvent();
            }
        }
    }
}

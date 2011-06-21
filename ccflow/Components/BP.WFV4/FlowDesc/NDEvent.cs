using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.WF
{
    public enum EventDoType
    {
        /// <summary>
        /// ����
        /// </summary>
        Disable,
        /// <summary>
        /// ִ��SQL
        /// </summary>
        SQL,
        /// <summary>
        /// ִ��URL
        /// </summary>
        URL
    }
    /// <summary>
    /// �¼�����б�
    /// </summary>
    public class EventListOfNode
    {
        #region �ڵ��¼�
        /// <summary>
        /// ���ڵ㱣��ǰ
        /// </summary>
        public const string SaveBefore = "SaveBefore";
        /// <summary>
        /// ���ڵ㱣���
        /// </summary>
        public const string SaveAfter = "SaveAfter";
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
    public class NDEventAttr
    {
        /// <summary>
        /// �¼�����
        /// </summary>
        public const string FK_Event = "FK_Event";
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string FK_Node = "FK_Node";
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
        public const string MsgErr = "MsgErr";
        /// <summary>
        /// FK_Flow
        /// </summary>
        public const string FK_Flow = "FK_Flow";
    }
	/// <summary>
	/// �¼�
	/// �ڵ�Ľڵ㱣���¼������������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
    public class NDEvent : EntityMyPK
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
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(NDEventAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NDEventAttr.FK_Node, value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStrByKey(NDEventAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(NDEventAttr.FK_Flow, value);
            }
        }
        public string DoDoc
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.DoDoc);
            }
            set
            {
                this.SetValByKey(NDEventAttr.DoDoc, value);
            }
        }
        /// <summary>
        /// ִ�гɹ���ʾ
        /// </summary>
        public string MsgOK
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.MsgOK);
            }
            set
            {
                this.SetValByKey(NDEventAttr.MsgOK, value);
            }
        }
        /// <summary>
        /// ������ʾ
        /// </summary>
        public string MsgError
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.MsgErr);
            }
            set
            {
                this.SetValByKey(NDEventAttr.MsgErr, value);
            }
        }
        public string FK_Event
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.FK_Event);
            }
            set
            {
                this.SetValByKey(NDEventAttr.FK_Event, value);
            }
        }
        /// <summary>
        /// ִ������
        /// </summary>
        public EventDoType HisDoType
        {
            get
            {
                return (EventDoType)this.GetValIntByKey(NDEventAttr.DoType);
            }
            set
            {
                this.SetValByKey(NDEventAttr.DoType, (int)value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �¼�
        /// </summary>
        public NDEvent()
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

                Map map = new Map("WF_Event");
                map.EnDesc = "�¼�";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddMyPK();

                map.AddTBString(NDEventAttr.FK_Event, null, "�¼�", true, true, 0, 400, 10);

                map.AddTBString(NDEventAttr.FK_Flow, null, "FK_Flow", true, true, 0, 3, 4);
                map.AddTBInt(NDEventAttr.FK_Node, 0, "FK_Node", true, true);

                map.AddTBInt(NDEventAttr.DoType, 0, "��������(Disable/SQL/URL)", true, true);
                map.AddTBString(NDEventAttr.DoDoc, null, "ִ������", true, true, 0, 400, 10);
                map.AddTBString(NDEventAttr.MsgOK, null, "��ʾ", true, true, 0, 400, 10);
                map.AddTBString(NDEventAttr.MsgErr, null, "��ʾ", true, true, 0, 400, 10);


                //map.AddTBString(NDEventAttr.SaveBefore, null, "���ڵ㱣��ǰ", true, false, 0, 400, 10,true);
                //map.AddTBString(NDEventAttr.SaveAfter, null, "���ڵ㱣���", true, false, 0, 400, 10, true);

                //map.AddTBString(NDEventAttr.SendWhen, null, "���ڵ㷢��ʱ", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.SendSuccess, null, "�ڵ㷢�ͳɹ�", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.SendError, null, "�ڵ㷢��ʧ��", true, false, 0, 400, 10, true);

                //map.AddTBString(NDEventAttr.ReturnBefore, null, "���ڵ��˻�ǰ", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.ReturnAfter, null, "���ڵ��˻غ�", true, false, 0, 400, 10, true);

                //map.AddTBString(NDEventAttr.UndoneBefore, null, "���ڵ㳷������ǰ", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.UndoneAfter, null, "���ڵ㳷�����ͺ�", true, false, 0, 400, 10, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeUpdateInsertAction()
        {
            Node nd = new Node(this.FK_Node);
            this.FK_Flow = nd.FK_Flow;

            this.MyPK = this.FK_Node + "_" + this.FK_Event;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// �¼�
	/// </summary>
    public class NDEvents : EntitiesOID
    {
        /// <summary>
        /// ִ���¼����¼������ EventList.
        /// </summary>
        /// <param name="dotype"></param>
        /// <param name="wk"></param>
        /// <returns></returns>
        public string DoEventNode(string dotype, Work wk)
        {
            if (this.Count == 0)
                return null;

            NDEvent nev = this.GetEntityByKey(NDEventAttr.FK_Event, dotype) as NDEvent;
            if (nev == null || nev.HisDoType == EventDoType.Disable)
                return null;

            string doc = nev.DoDoc.Trim();
            if (doc == null || doc == "")
                return null;

            #region ����ִ������
            Attrs attrs = wk.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (doc.Contains("@" + attr.Key) == false)
                    continue;
                if (attr.MyDataType == DataType.AppString)
                    doc = doc.Replace("@" + attr.Key, "'" + wk.GetValStrByKey(attr.Key) + "'");
                else
                    doc = doc.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
            }

            //doc = doc.Replace("@FK_Node", nev.FK_Node.ToString());
            //doc = doc.Replace("@WorkID", wk.OID.ToString());
            //doc = doc.Replace("@FID", wk.OID.ToString());
            //doc = doc.Replace("@UserNo", BP.Web.WebUser.No);
            //doc = doc.Replace("@UserName", BP.Web.WebUser.Name);
            //doc = doc.Replace("@SID", BP.Web.WebUser.SID);
            //doc = doc.Replace("@UserDept", BP.Web.WebUser.FK_Dept);
            // doc += "&FK_Flow=" + nev.FK_Node.ToString();

            if (nev.HisDoType != EventDoType.SQL)
            {
                doc += "&FK_Flow=" + nev.FK_Flow.ToString();
                doc += "&FK_Node=" + nev.FK_Node.ToString();
                doc += "&WorkID=" + wk.OID.ToString();
                doc += "&FID=" + wk.FID.ToString();
                doc += "&UserNo=" + WebUser.No;
                doc += "&SID=" + WebUser.SID;
                doc += "&FK_Dept=" + WebUser.FK_Dept;
                doc += "&FK_Unit=" + WebUser.FK_Unit;
            }
            #endregion ����ִ������

            switch (nev.HisDoType)
            {
                case EventDoType.SQL:
                    try
                    {
                        DBAccess.RunSQL(doc);
                        return "@" + nev.MsgOK + " -ִ�гɹ���";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@" + nev.MsgOK + " Error:" + ex.Message);
                    }
                    break;
                case EventDoType.URL:
                    doc = doc.Replace("@AppPath", System.Web.HttpContext.Current.Request.ApplicationPath);
                    try
                    {
                        string text = DataType.ReadURLContext(doc, 800, System.Text.Encoding.UTF8);
                        if (text != null && text.Substring(0, 7).Contains("Err"))
                            throw new Exception(text);

                        Log.DebugWriteInfo(doc + " ------ " + text);
                        return "@" + nev.MsgOK + text;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@" + nev.MsgError + ex.Message);
                    }
                    break;
                default:
                    throw new Exception("@no such way.");
            }
        }
        /// <summary>
        /// �¼�
        /// </summary>
        public NDEvents() { }
        /// <summary>
        /// �¼�
        /// </summary>
        /// <param name="fk_node">�ڵ�</param>
        public NDEvents(int fk_node)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NDEventAttr.FK_Node, fk_node);
            qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NDEvent();
            }
        }
    }
}

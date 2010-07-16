
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// ��˹���
    /// </summary>
    public class GECheckStandAttr : WorkAttr
    {
        /// <summary>
        /// ���״̬( 0, ��ͬ��, 1, ��ʾͬ��, 2 ����) 
        /// </summary>
        public const string CheckState = "CheckState";
        /// <summary>
        /// NodeID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// �ο���Ϣ
        /// </summary>
        public const string RefMsg = "RefMsg";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
    }
    /// <summary>
    /// ��˹���
    /// </summary>
    public class GECheckStand : GEWork
    {
        #region ��������
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(GECheckStandAttr.Note);
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(GECheckStandAttr.Note);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.Note, value);
            }
        }
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public string RefMsg
        {
            get
            {
                return this.GetValStringByKey(GECheckStandAttr.RefMsg);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.RefMsg, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public CheckState HisCheckState
        {
            get
            {
                return (CheckState)this.CheckState;
            }
        }
        /// <summary>
        /// ���״̬( 0,��ͬ��,1,ͬ��,2����) 
        /// </summary>
        public CheckState CheckState
        {
            get
            {
                return (CheckState)this.GetValIntByKey(GECheckStandAttr.CheckState);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.CheckState, (int)value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(GECheckStandAttr.Sender);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.Sender, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ��˹���
        /// </summary>
        public GECheckStand()
        {
        }
        /// <summary>
        /// ��˹���
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        public GECheckStand(int nodeid)
        {
            this.NodeID = nodeid;
            this.SQLCash = null;
        }
        /// <summary>
        /// ��˹���
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="_oid">OID</param>
        public GECheckStand(int nodeid, Int64 _oid)
        {
            this.NodeID = nodeid;
            this.OID = _oid;
            this.SQLCash = null;
        }
        #endregion

        #region Map
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                //if (this._enMap != null)
                //    return this._enMap;

                //   BP.Sys.MapData md = new BP.Sys.MapData();
                this._enMap = BP.Sys.MapData.GenerHisMap("ND" + this.NodeID.ToString());
                return this._enMap;
            }
        }
        /// <summary>
        /// GECheckStands
        /// </summary>
        public override Entities GetNewEntities
        {
            get
            {
                if (this.NodeID == 0)
                    return new GECheckStands();
                return new GECheckStands(this.NodeID);
            }
        }
        #endregion
    }
    /// <summary>
    /// ��˹���s
    /// </summary>
    public class GECheckStands : GEWorks
    {
        #region ���ػ��෽��
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID = 0;
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.NodeID == 0)
                    return new GECheckStand();
                return new GECheckStand(this.NodeID);
            }
        }
        /// <summary>
        /// ��˹���ID
        /// </summary>
        public GECheckStands()
        {
        }
        /// <summary>
        /// ��˹���ID
        /// </summary>
        /// <param name="nodeid"></param>
        public GECheckStands(int nodeid)
        {
            this.NodeID = nodeid;
        }
        #endregion
    }
}

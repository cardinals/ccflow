using System;
using System.Collections.Generic;
using System.Text;
using BP.En;
using BP.DA;
using BP.Sys;

namespace BP.WF
{
    public enum ActionType
    {
        /// <summary>
        /// ����
        /// </summary>
        Start,
        /// <summary>
        /// ǰ��
        /// </summary>
        Forward,
        /// <summary>
        /// �˻�
        /// </summary>
        Return,
        /// <summary>
        /// �ƽ�
        /// </summary>
        Shift
    }
    /// <summary>
    ///  ����
    /// </summary>
    public class TrackAttr
    {
        /// <summary>
        /// ����Ա
        /// </summary>
        public const string FromEmp = "FromEmp";
        /// <summary>
        /// ����Ա
        /// </summary>
        public const string ToEmp = "ToEmp";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �������
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �����
        /// </summary>
        public const string ActionType = "ActionType";
        /// <summary>
        /// ʱ����
        /// </summary>
        public const string WorkTimeSpan = "WorkTimeSpan";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string NodeData = "NodeData";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// �켣�ֶ�
        /// </summary>
        public const string TrackFields = "TrackFields";
    }
    /// <summary>
    /// �켣
    /// </summary>
    public class Track : BP.En.EntityMyPK
    {
        #region attrs
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TrackAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string FromEmp
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.FromEmp);
            }
            set
            {
                this.SetValByKey(TrackAttr.FromEmp, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string ToEmp
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.ToEmp);
            }
            set
            {
                this.SetValByKey(TrackAttr.ToEmp, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.RDT);
            }
            set
            {
                this.SetValByKey(TrackAttr.RDT, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.CDT);
            }
            set
            {
                this.SetValByKey(TrackAttr.CDT, value);
            }
        }
        /// <summary>
        /// fid
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(TrackAttr.FID);
            }
            set
            {
                this.SetValByKey(TrackAttr.FID, value);
            }
        }
        /// <summary>
        /// Workid
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(TrackAttr.WorkID);
            }
            set
            {
                this.SetValByKey(TrackAttr.WorkID, value);
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public ActionType HisActionType
        {
            get
            {
                return (ActionType)this.GetValIntByKey(TrackAttr.ActionType);
            }
            set
            {
                this.SetValByKey(TrackAttr.ActionType, (int)value);
            }
        }
        /// <summary>
        /// ���̽���ʱ��
        /// </summary>
        public string NodeData
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.NodeData);
            }
            set
            {
                this.SetValByKey(TrackAttr.NodeData, value);
            }
        }
        #endregion attrs

        #region ����
        public string RptName = null;
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //Ҫ���ӵ�����Դ����ʾҪ���ӵ����Ǹ�ϵͳ���ݿ⣩��
                map.PhysicsTable = "WF_Track"; // Ҫ�����
                map.EnDesc = "�켣��";
                map.EnType = EnType.App;
                #endregion

                #region �ֶ�
                map.AddMyPK();
                map.AddTBString(TrackAttr.FK_Flow, null, "����", true, false, 0, 100, 100);
                map.AddDDLSysEnum(TrackAttr.ActionType, 0, "��������", true, false, TrackAttr.ActionType,
                  "@0=����@1=ǰ��@2=����@3=�ƽ�@4=ɾ��");

                map.AddTBString(TrackAttr.FromEmp, null, "����Ա", true, false, 0, 100, 100);
                map.AddTBString(TrackAttr.ToEmp, null, "����Ա", true, false, 0, 100, 100);
                map.AddTBDateTime(TrackAttr.RDT, null, "��¼����", true, false);
                map.AddTBDateTime(TrackAttr.CDT, null, "�������", true, false);

                map.AddTBInt(TrackAttr.FID, 0, "����ID", true, false);
                map.AddTBInt(TrackAttr.WorkID, 0, "����ID", true, false);
              
                map.AddTBFloat(TrackAttr.WorkTimeSpan, 0, "ʱ����(��)", true, false);
                map.AddTBStringDoc(TrackAttr.NodeData, null, "�ڵ�����", true, false);
                #endregion �ֶ�

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// �켣
        /// </summary>
        /// <param name="rptName"></param>
        public Track(string rptName)
        {
            this.RptName = rptName;
        }
        /// <summary>
        /// �켣
        /// </summary>
        public Track()
        {
        }
        /// <summary>
        /// �켣
        /// </summary>
        /// <param name="rptName"></param>
        /// <param name="WorkID"></param>
        public Track(string rptName, int WorkID)
        {
            this.RptName = rptName;
            this.Retrieve();
        }
        #endregion attrs
    }
    /// <summary>
    /// �켣����
    /// </summary>
    public class Tracks : BP.En.Entities
    {
        /// <summary>
        /// �켣����
        /// </summary>
        public Tracks()
        {
        }
        public override Entity GetNewEntity
        {
            get
            {
                return new Track();
            }
        }
    }
}

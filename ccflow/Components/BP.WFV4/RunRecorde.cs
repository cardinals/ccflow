
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port;
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// ���м�¼ ����
	/// </summary>
    public class RunRecordAttr
    {
        #region ��������
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ��ǰ�ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// workid
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ����
        /// </summary>
        public const string HostIP = "HostIP";
        /// <summary>
        /// ����
        /// </summary>
        public const string ClientIP = "ClientIP";
        /// <summary>
        /// ������Ա����ѡ)
        /// </summary>
        public const string FromSaveTime = "FromSaveTime";
        /// <summary>
        /// ������Ա��������ѡ)
        /// </summary>
        public const string FromAfterNoteTime = "FromAfterNoteTime";
        /// <summary>
        /// day
        /// </summary>
        public const string FK_Day = "FK_Day";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// ������Ա����ѡ)
        /// </summary>
        public const string RDT = "RDT";

        #endregion
    }
	/// <summary>
	/// ���м�¼
	/// </summary>
    public class RunRecord : Entity
    {
        #region ����
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.RDT);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.RDT, value);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_NY, value);
            }
        }
        public string FK_Day
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.FK_Day);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_Day, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_Emp, value);
            }
        }
        public string HostIP
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.HostIP);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.HostIP, value);
            }
        }
        public string ClientIP
        {
            get
            {
                return this.GetValStringByKey(RunRecordAttr.ClientIP);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.ClientIP, value);
            }
        }
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(RunRecordAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FK_Node, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(RunRecordAttr.WorkID);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.WorkID, value);
            }
        }

        public int FromSaveTime
        {
            get
            {
                return this.GetValIntByKey(RunRecordAttr.FromSaveTime);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FromSaveTime, value);
            }
        }
        public int FromAfterNoteTime
        {
            get
            {
                return this.GetValIntByKey(RunRecordAttr.FromAfterNoteTime);
            }
            set
            {
                this.SetValByKey(RunRecordAttr.FromAfterNoteTime, value);
            }
        }
        
        #endregion


        #region ���캯��
        /// <summary>
        /// RunRecord
        /// </summary>
        public RunRecord()
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
                Map map = new Map("WF_RunRecord");
                map.EnDesc = "���м�¼";
                map.EnType = EnType.Admin;

                map.AddDDLEntitiesPK(RunRecordAttr.FK_Node, null, "�ڵ�", new NodeExts(), false);
                map.AddTBIntPK(RunRecordAttr.WorkID, 0, "WorkID", false, false);

                map.AddTBString(RunRecordAttr.HostIP, "", "HostIP", true, false, 0, 200, 10);
                map.AddTBString(RunRecordAttr.ClientIP, "", "ClientIP", true, false, 0, 200, 10);

                map.AddTBInt(RunRecordAttr.FromAfterNoteTime, 0, "����", true, false);
                map.AddTBInt(RunRecordAttr.FromSaveTime, 0, "���浽����", true, false);

                map.AddDDLEntities(RunRecordAttr.FK_Day, null, "��", new Pub.Days(), false);
                map.AddDDLEntities(RunRecordAttr.FK_NY, null, "�·�", new Pub.YFs(), false);
                

                map.AddTBDateTime(RunRecordAttr.RDT, "RDT", true, false);
                map.AddTBIntMyNum();

                map.AddSearchAttr(RunRecordAttr.FK_NY);
                map.AddSearchAttr(RunRecordAttr.FK_Day);
                map.AddSearchAttr(RunRecordAttr.FK_Node);

               
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            DateTime now = DateTime.Now;
            this.FK_Day = now.ToString("dd");
            this.FK_NY = now.ToString("MM");
            this.RDT = now.ToString("yyyy-MM-dd hh:mm:ss");

            this.FK_Emp = Web.WebUser.No;
            return base.beforeUpdateInsertAction();
        }

    }
	/// <summary>
	/// ���м�¼
	/// </summary>
	public class RunRecords: Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RunRecord();
			}
		}
		/// <summary>
		/// RunRecord
		/// </summary>
		public RunRecords(){} 		 
		#endregion
	}
	
}

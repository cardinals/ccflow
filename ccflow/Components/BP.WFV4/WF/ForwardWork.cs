

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
	/// ת����¼
	/// </summary>
    public class ForwardWorkAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string NodeId = "NodeId";
        /// <summary>
        /// ������Ա
        /// </summary>
        public const string Worker = "Worker";
        /// <summary>
        /// �˻�ԭ��
        /// </summary>
        public const string Note = "Note";
        public const string FK_Emp = "FK_Emp";
        public const string Emps = "Emps";
        /// <summary>
        /// �Ƿ����ջ�
        /// </summary>
        public const string IsTakeBack = "IsTakeBack";
        #endregion
    }
	/// <summary>
	/// ת����¼
	/// </summary>
	public class ForwardWork : Entity
	{		
		#region ��������
        /// <summary>
        /// �Ƿ����ջ�
        /// </summary>
        public bool IsTakeBack
        {
            get
            {
                return this.GetValBooleanByKey(ForwardWorkAttr.IsTakeBack);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.IsTakeBack, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
		public int  WorkID
		{
			get
			{
				return this.GetValIntByKey(ForwardWorkAttr.WorkID);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.WorkID,value);
			}
		}
		/// <summary>
		/// NodeId
		/// </summary>
		public int  NodeId
		{
			get
			{
				return this.GetValIntByKey(ForwardWorkAttr.NodeId);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.NodeId,value);
			}
		}
		public string  Note
		{
			get
			{
				return this.GetValStringByKey(ForwardWorkAttr.Note);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.Note,value);
			}
		}
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.Emps);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.Emps, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.FK_Emp);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.FK_Emp, value);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(ForwardWorkAttr.FK_Emp);
            }
        }
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(ForwardWorkAttr.Note);
            }
        }
		#endregion 

		#region ���캯��
		/// <summary>
		/// ת����¼
		/// </summary>
		public ForwardWork(){}

        public ForwardWork(int workid, int nodeid)
        {
            this.WorkID = workid;
            this.NodeId = nodeid;
            this.Retrieve();
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

                Map map = new Map("WF_ForwardWork");
                map.EnDesc = "ת����¼";
                map.EnType = EnType.App;

                map.AddTBIntPK(ForwardWorkAttr.WorkID, 0, "����ID", true, true);
                map.AddTBIntPK(ForwardWorkAttr.NodeId, 0, "NodeId", true, true);
                map.AddTBString(ForwardWorkAttr.Note, "", "Note", true, true, 0, 4000, 10);
                map.AddTBString(ForwardWorkAttr.Emps, "", "Emps", true, true, 0, 4000, 10);
                map.AddTBString(ForwardWorkAttr.FK_Emp, "", "FK_Emp", true, true, 0, 4000, 10);
                map.AddBoolean(ForwardWorkAttr.IsTakeBack, false, "�Ƿ����ջ�", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 
	}
	/// <summary>
	/// ת����¼s 
	/// </summary>
	public class ForwardWorks : Entities
	{	 
		#region ����
		/// <summary>
		/// ת����¼s
		/// </summary>
		public ForwardWorks()
		{

		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ForwardWork();
			}
		}
		#endregion
	}
	
}

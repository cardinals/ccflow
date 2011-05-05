

using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.WF;
using BP.Tax; 
using BP.Port; 
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// ������¼
	/// </summary>
    public class RebackAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkId = "WorkId";
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
        #endregion
    }
	/// <summary>
	/// ������¼
	/// </summary>
	public class Reback : Entity
	{		
		#region ��������
		/// <summary>
		/// ����ID
		/// </summary>
		public int  WorkId
		{
			get
			{
				return this.GetValIntByKey(RebackAttr.WorkId);
			}
			set
			{
				SetValByKey(RebackAttr.WorkId,value);
			}
		}
		/// <summary>
		/// NodeId
		/// </summary>
		public int  NodeId
		{
			get
			{
				return this.GetValIntByKey(RebackAttr.NodeId);
			}
			set
			{
				SetValByKey(RebackAttr.NodeId,value);
			}
		}
		public string  Note
		{
			get
			{
				return this.GetValStringByKey(RebackAttr.Note);
			}
			set
			{
				SetValByKey(RebackAttr.Note,value);
			}
		}
        /// <summary>
        /// ������
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStringByKey(RebackAttr.Emps);
            }
            set
            {
                SetValByKey(RebackAttr.Emps, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RebackAttr.FK_Emp);
            }
            set
            {
                SetValByKey(RebackAttr.FK_Emp, value);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(RebackAttr.FK_Emp);
            }
        }
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(RebackAttr.Note);
            }
        }
		#endregion 

		#region ���캯��
		/// <summary>
		/// ������¼
		/// </summary>
		public Reback(){}

		public Reback(int workid, int nodeid)
		{
			this.WorkId = workid;
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

                Map map = new Map("WF_Reback");
                map.EnDesc = "������¼";
                map.EnType = EnType.App;

                map.AddTBIntPK(RebackAttr.WorkId, 0, "����ID", true, true);
                map.AddTBIntPK(RebackAttr.NodeId, 0, "NodeId", true, true);
                map.AddTBString(RebackAttr.FK_Emp, "", "FK_Emp", true, true, 0, 4000, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 
	}
	/// <summary>
	/// ������¼s 
	/// </summary>
	public class Rebacks : Entities
	{	 
		#region ����
		/// <summary>
		/// ������¼s
		/// </summary>
		public Rebacks()
		{

		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Reback();
			}
		}
		#endregion
	}
	
}

using System;
using BP.En.Base;
using BP.En;
using BP.DA;

namespace BP.WF
{
	/// <summary>
	/// ��ע��������������
	/// </summary>
	public class NoteWorkAttr:CheckWorkAttr
	{
		/// <summary>
		/// ��ע
		/// </summary>
		public const string Note1="Note1";
		/// <summary>
		/// ��ע
		/// </summary>
		public const string Note2="Note2";
	}
	/// <summary>
	/// SimpleCheckWork ��ժҪ˵����
	/// ��ע����������
	/// </summary>
	public class NoteWork : Work
	{
		#region ��������
		public string FK_Taxpayer
		{
			get
			{
				return this.GetValStringByKey(NoteWorkAttr.FK_Taxpayer);
			}
			set
			{
				this.SetValByKey(NoteWorkAttr.FK_Taxpayer,value);
			}
		}
		/// <summary>
		/// NodeID
		/// </summary>
		public int NodeID
		{
			get
			{
				return this.GetValIntByKey(NoteWorkAttr.NodeID);
			}
			set
			{
				this.SetValByKey(NoteWorkAttr.NodeID,value);
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// ��ע����������
		/// </summary>
		public NoteWork()
		{
		}
		/// <summary>
		/// ��ע����������
		/// </summary>
		/// <param name="wfoid">��������ID</param>
		public NoteWork(int wfoid)
		{
			this.OID=wfoid;
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(NoteWorkAttr.OID,this.OID) ;
			if (qo.DoQuery() > 1)
				throw new Exception("@�˹�������������������ע�����ڵ�,�����ô˷����õ���˵Ľ��.����� new NoteWork(oid, nodeId) ���� ");
		}
		/// <summary>
		/// ��ע����������
		/// </summary>
		/// <param name="oid">��������ID</param>
		/// <param name="nodeId">�ڵ�ID</param>
		public NoteWork(int oid, int nodeId)
		{
			this.OID = oid;
			this.NodeID = nodeId;
			this.Retrieve();
		}
		/// <summary>
		/// ����
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("WF_NoteWork");
				map.EnDesc="��ע�ڵ�";

				map.AddDDLEntities(NoteWorkAttr.Sender,Web.WebUser.No,"������",new En.Emps(),false);
				map.AddTBDateTime(NoteWorkAttr.RDT,"��������",true,true);


				map.AddTBStringDoc(NoteWorkAttr.Note1,null,"��ע1",true,false);
				map.AddTBStringDoc(NoteWorkAttr.Note2,null,"��ע2",true,true);

				
				map.AddDDLEntities(NoteWorkAttr.Recorder,Web.WebUser.No,"��¼��",new En.Emps(),false);
				map.AddTBInt(NoteWorkAttr.NodeState,0,"NodeState",false,true);

				map.AddTBDateTime(StandardCheckAttr.CDT,"��","�������",true,true);
				map.AddTBString(NoteWorkAttr.FK_Taxpayer,null,"FK_Taxpayer",true,false,0,100,100);

				map.AddTBIntPK(NoteWorkAttr.OID,0,"��������ID",false,true);
				map.AddTBIntPK(NoteWorkAttr.NodeID,0,"�ڵ�ID",false,true);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ��ע���������༯��
	/// </summary>
	public class NoteWorks :Works
	{
		#region ����
		/// <summary>
		/// _nodeId
		/// </summary>
		public int _nodeId=0;
		/// <summary>
		/// _flowNo
		/// </summary>
		public string _flowNo="";
		/// <summary>
		/// �ڵ�ID
		/// </summary>
		public override int NodeId
		{
			get
			{
				return this._nodeId;
			}			 
		}
		/// <summary>
		/// �������̱��
		/// </summary>
		public override string FlowNo
		{
			get
			{
				
				return this._flowNo;
			}
		}
		#endregion

		/// <summary>
		/// ��׼���
		/// </summary>
		public NoteWorks()
		{
		}
		/// <summary>
		/// �����б�s
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NoteWork();
			}
		}
	}
}

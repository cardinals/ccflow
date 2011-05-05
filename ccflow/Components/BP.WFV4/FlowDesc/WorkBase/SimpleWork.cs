using System;
using System.Collections;
using BP.DA;
using BP.En;
 
 
using BP.En;

namespace BP.WF
{
	
	/// <summary>
	/// �򵥵�work
	/// </summary>
	abstract public class SimpleWork : Work
	{		 
		#region ����
		/// <summary>
		/// ss
		/// </summary>
		public SimpleWork()
		{
			//this.No  = this.GenerNewNo ;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="oid"></param>
		public SimpleWork(int oid) : base(oid){}
		/// <summary>
		/// map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map(this.PhysicsTable);
				map.EnDesc=this.Desc;
				
				map.AddTBIntPK(WorkAttr.OID,0,"OID",false,true);
				map.AddDDLSysEnum(WorkAttr.NodeState,0,"�ڵ�״̬",true,false,"NodeState");

				map.AddDDLEntities(WorkAttr.Rec,Web.WebUser.No,"��¼��",new Port.Emps(),false);
				map.AddTBDateTime(WorkAttr.RDT,"��¼����",true,true);
				map.AddTBDateTime(WorkAttr.CDT,"�������",true,true);

				//map.AddTBStringDoc("Docs",null,"����",true,false,0,500,50,20);
				map.AddTBStringDoc(WorkAttr.Note,null,"��ע1",true,false);

				map.AddTBStringDoc("Note1",null,"��ע2",true,false);

				map.AddTBString("FK_Taxpayer",null,"��˰�˱��",false,false,0,100,100);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 		

		#region ��Ҫ������д�ķ���
		/// <summary>
		/// ָ����
		/// </summary>
		protected abstract string PhysicsTable{get;}
		/// <summary>
		/// ����
		/// </summary>
		public abstract string Desc{get;}
		#endregion

	}
	/// <summary>
	/// SimpleWorks
	/// </summary>
	abstract public class SimpleWorks : Works
	{
		/// <summary>
		/// SimpleWorks
		/// </summary>
		public SimpleWorks()
		{}
	}
}

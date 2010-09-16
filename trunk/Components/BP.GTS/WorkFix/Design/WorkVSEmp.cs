using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	
	/// <summary>
	/// vaemp
	/// </summary>
	public class WorkVSEmpAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Work="FK_Work";
		/// <summary>
		/// �Ķ���
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// UseState
		/// </summary>
		public const  string UseState="UseState";

	 
		#endregion	
	}
	/// <summary>
	/// emp ��ժҪ˵��
	/// </summary>
	public class WorkVSEmp :Entity
	{
		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(WorkVSEmpAttr.FK_Emp);
			}
			set
			{
				SetValByKey(WorkVSEmpAttr.FK_Emp,value);
			}
		}
		/// <summary>
		///���
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkVSEmpAttr.FK_Work);
			}
			set
			{
				SetValByKey(WorkVSEmpAttr.FK_Work,value);
			}
		}
		#endregion

		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForAppAdmin();
				return uac;
				//return base.HisUAC;
			}
		}

		/// <summary>
		/// �Ķ���������
		/// </summary> 
		public WorkVSEmp()
		{
		}
		public WorkVSEmp(string Work,string Equestion)
		{
			this.FK_Work = Work;
			this.FK_Emp = Equestion;
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
			}
		}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_WorkVSEmp");
				map.EnDesc="����ѧ��";	
				map.EnType=EnType.Dot2Dot;

				map.AddDDLEntitiesPK(WorkVSEmpAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkVSEmpAttr.FK_Emp,null,"ѧ��",new Emps(),false);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// emp 
	/// </summary>
	public class WorkVSEmps : Entities
	{
		#region ����
		/// <summary>
		/// emp
		/// </summary>
		public WorkVSEmps(){}
		
		/// <summary>
		/// 
		/// </summary>
		public WorkVSEmps(string fk_emp, int UseState)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkVSEmpAttr.FK_Emp, fk_emp);
			qo.addAnd();
			qo.AddWhere(WorkVSEmpAttr.UseState, UseState);

			qo.DoQuery();
		}
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkVSEmp();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
//		public WorkRandoms GetWorkRandoms(string fk_emp)
//		{
//			WorkRandoms pps = new WorkRandoms();
//			QueryObject qo = new QueryObject(pps);
//			qo.AddWhereInSQL(WorkFixAttr.No, "SELECT FK_Work FROM GTS_WorkVSEmp WHERE Len(FK_Work)=4 AND FK_Emp='"+fk_emp+"'") ;
//			qo.DoQuery();
//			return pps;
//		}
		 
		#endregion
	}
	
}

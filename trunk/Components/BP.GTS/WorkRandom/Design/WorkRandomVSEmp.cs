using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// vaemp
	/// </summary>
	public class WorkRandomVSEmpAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Work="FK_Work";
		/// <summary>
		/// 阅读题
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// UseState
		/// </summary>
		public const  string UseState="UseState";

	 
		#endregion	
	}
	/// <summary>
	/// emp 的摘要说明
	/// </summary>
	public class WorkRandomVSEmp :Entity
	{
		#region 基本属性
		/// <summary>
		///阅读题
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSEmpAttr.FK_Emp);
			}
			set
			{
				SetValByKey(WorkRandomVSEmpAttr.FK_Emp,value);
			}
		}
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSEmpAttr.FK_Work);
			}
			set
			{
				SetValByKey(WorkRandomVSEmpAttr.FK_Work,value);
			}
		}
		#endregion

		#region 构造函数
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
		/// 阅读题分数设计
		/// </summary> 
		public WorkRandomVSEmp()
		{
		}
		public WorkRandomVSEmp(string Work,string Equestion)
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
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_WorkRandomVSEmp");
				map.EnDesc="考试学生";	
				map.EnType=EnType.Dot2Dot;

				map.AddDDLEntitiesPK(WorkRandomVSEmpAttr.FK_Work,"0001","试卷",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkRandomVSEmpAttr.FK_Emp,null,"学生",new BP.Port.Emps(),false);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// emp 
	/// </summary>
	public class WorkRandomVSEmps : Entities
	{
		#region 构造
		/// <summary>
		/// emp
		/// </summary>
		public WorkRandomVSEmps(){}
		
		/// <summary>
		/// 
		/// </summary>
		public WorkRandomVSEmps(string fk_emp, int UseState)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomVSEmpAttr.FK_Emp, fk_emp);
			qo.addAnd();
			qo.AddWhere(WorkRandomVSEmpAttr.UseState, UseState);

			qo.DoQuery();
		}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSEmp();
			}
		}	
		#endregion 

		#region 查询方法
		 
//		public WorkRandoms GetWorkRandoms(string fk_emp)
//		{
//			WorkRandoms pps = new WorkRandoms();
//			QueryObject qo = new QueryObject(pps);
//			qo.AddWhereInSQL(WorkFixAttr.No, "SELECT FK_Work FROM GTS_WorkRandomVSEmp WHERE Len(FK_Work)=4 AND FK_Emp='"+fk_emp+"'") ;
//			qo.DoQuery();
//			return pps;
//		}
		 
		#endregion
	}
	
}

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
	public class PaperVSEmpAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Paper="FK_Paper";
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
	public class PaperVSEmp :Entity
	{
		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperVSEmpAttr.FK_Emp);
			}
			set
			{
				SetValByKey(PaperVSEmpAttr.FK_Emp,value);
			}
		}
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSEmpAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSEmpAttr.FK_Paper,value);
			}
		}
		#endregion

		#region ���캯��
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}

		/// <summary>
		/// �Ķ���������
		/// </summary> 
		public PaperVSEmp()
		{
		}
		public PaperVSEmp(string paper,string Equestion)
		{
			this.FK_Paper = paper;
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
				
				Map map = new Map("GTS_PaperVSEmp");
				map.EnDesc="����ѧ��";	
				map.EnType=EnType.Dot2Dot;

				map.AddDDLEntitiesPK(PaperVSEmpAttr.FK_Paper,"0001","�Ծ�",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSEmpAttr.FK_Emp,null,"ѧ��",new Emps(),false);
				//map.AddTBInt(PaperVSEmpAttr.UseState,0,"״̬",true,true);
				/* 0 ,û�п��Թ��� 1 �Ѿ����Թ��ˡ� */
				/* UseState*/
				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// emp 
	/// </summary>
	public class PaperVSEmps : Entities
	{
		#region ����
		/// <summary>
		/// emp
		/// </summary>
		public PaperVSEmps(){}
		
		/// <summary>
		/// 
		/// </summary>
		public PaperVSEmps(string fk_emp, int UseState)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperVSEmpAttr.FK_Emp, fk_emp);
			qo.addAnd();
			qo.AddWhere(PaperVSEmpAttr.UseState, UseState);

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
				return new PaperVSEmp();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		public WorkRDs GetWorkRDs(string fk_emp)
		{
			WorkRDs pps = new WorkRDs();
			QueryObject qo = new QueryObject(pps);
			qo.AddWhereInSQL(PaperFixAttr.No, "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE Len(FK_Paper)=4 AND FK_Emp='"+fk_emp+"'") ;
			qo.DoQuery();
			return pps;
		}
		 
		#endregion
	}
	
}

using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// �Ķ������
	/// </summary>
	public class WorkRandomVSRCDtlAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Work="FK_Work";
		/// <summary>
		/// FK_RC
		/// </summary>
		public const  string FK_RC="FK_RC";
		/// <summary>
		/// �Ķ���
		/// </summary>
		public const  string FK_RCDtl="FK_RCDtl";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// �Ķ������ ��ժҪ˵����
	/// </summary>
	public class WorkRandomVSRCDtl :Entity
	{
		
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForAppAdmin();
				return uac;
			}
		}

		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(WorkRandomVSRCDtlAttr.FK_RCDtl);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.FK_RCDtl,value);
			}
		}		  
		public RCDtl HisRCDtl
		{
			get
			{
				return new RCDtl(this.FK_RCDtl);
			}
		}
		public string FK_RCDtlText
		{
			get
			{
				return this.GetValRefTextByKey(WorkRandomVSRCDtlAttr.FK_RCDtl);
			}
			 
		}
		/// <summary>
		///���
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSRCDtlAttr.FK_Work);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.FK_Work,value);
			}
		}
		/// <summary>
		/// FK_RC 
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSRCDtlAttr.FK_RC);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.FK_RC,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkRandomVSRCDtlAttr.Cent);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.Cent,value);
			}
		}
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// �Ķ���������
		/// </summary> 
		public WorkRandomVSRCDtl()
		{
		}
		 
		public WorkRandomVSRCDtl(string fk_Work, int  fk_rcdtl)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_Work,fk_Work);
			qo.addAnd();
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_RCDtl,fk_rcdtl);
			qo.DoQuery();
			 
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
				
				Map map = new Map("GTS_WorkRandomVSRCDtl");
				map.EnDesc="�Ķ���������";	
				map.EnType=EnType.Dot2Dot;
			 

				map.AddDDLEntitiesPK(WorkRandomVSRCDtlAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkRandomVSRCDtlAttr.FK_RCDtl,1,DataType.AppInt,"�Ķ��ʴ���",new RCDtls(), RCDtlAttr.OID, RCDtlAttr.Name,false);
				
				map.AddTBInt(WorkRandomVSRCDtlAttr.Cent,1,"��",true,false);
				map.AddDDLEntities(WorkRandomVSRCDtlAttr.FK_RC,"0001","�Ķ���",new RCs(),false);

				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}

		 
		#endregion

		#region ���ػ��෽��

		#endregion 
	
	}
	/// <summary>
	/// �Ķ������ 
	/// </summary>
	public class WorkRandomVSRCDtls : Entities
	{
		#region ����
		/// <summary>
		/// �Ķ������
		/// </summary>
		public WorkRandomVSRCDtls(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_Work">�Ծ���</param>
		/// <param name="fk_rc">�Ķ���Ŀ</param>
		public WorkRandomVSRCDtls(string fk_Work,string fk_rc)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_Work,fk_Work);
			qo.addAnd();
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_RC,fk_rc);
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
				return new WorkRandomVSRCDtl();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}

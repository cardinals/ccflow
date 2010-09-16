using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	/// <summary>
	/// �Ķ������
	/// </summary>
	public class PaperVSRCDtlAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Paper="FK_Paper";
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
	public class PaperVSRCDtl :Entity
	{
		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(PaperVSRCDtlAttr.FK_RCDtl);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.FK_RCDtl,value);
			}
		}		  
		public string FK_RCDtlText
		{
			get
			{
				return this.GetValRefTextByKey(PaperVSRCDtlAttr.FK_RCDtl);
			}
			 
		}
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCDtlAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_RC 
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCDtlAttr.FK_RC);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.FK_RC,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSRCDtlAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.Cent,value);
			}
		}
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
                uc.IsDelete = false;
                uc.IsInsert = false;

				return uc;
			}
		}
		#endregion

		#region ��չ����
		 
		#endregion		

		#region ���캯��
		/// <summary>
		/// �Ķ���������
		/// </summary> 
		public PaperVSRCDtl()
		{
		}
		 
		public PaperVSRCDtl(string fk_paper, int  fk_rcdtl)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperVSRCDtlAttr.FK_Paper,fk_paper);
			qo.addAnd();
			qo.AddWhere(PaperVSRCDtlAttr.FK_RCDtl,fk_rcdtl);
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
				
				Map map = new Map("GTS_PaperVSRCDtl");
				map.EnDesc="�Ķ���������";	
				map.EnType=EnType.Dot2Dot;
			 

				map.AddDDLEntitiesPK(PaperVSRCDtlAttr.FK_Paper,"0001","�Ծ�",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSRCDtlAttr.FK_RCDtl,1,DataType.AppInt,"�Ķ��ʴ���",new RCDtls(), RCDtlAttr.OID, RCDtlAttr.Name,false);
				
				map.AddTBInt(PaperVSRCDtlAttr.Cent,5,"��",true,false);
				map.AddDDLEntities(PaperVSRCDtlAttr.FK_RC,"0001","�Ķ���",new RCs(),false);
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
	public class PaperVSRCDtls : Entities
	{
		#region ����
		/// <summary>
		/// �Ķ������
		/// </summary>
		public PaperVSRCDtls(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_paper">�Ծ���</param>
		/// <param name="fk_rc">�Ķ���Ŀ</param>
		public PaperVSRCDtls(string fk_paper,string fk_rc)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperVSRCDtlAttr.FK_Paper,fk_paper);
			qo.addAnd();
			qo.AddWhere(PaperVSRCDtlAttr.FK_RC,fk_rc);
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
				return new PaperVSRCDtl();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �Ծ��Ķ������
	/// </summary>
	public class PaperVSRCAttr  
	{
		#region ��������
		/// <summary>
		/// �Ծ�
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// �Ķ���
		/// </summary>
		public const  string FK_RC="FK_RC";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// �Ծ��Ķ������ ��ժҪ˵��
	/// </summary>
	public class PaperVSRC :Entity
	{
		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCAttr.FK_RC);
			}
			set
			{
				SetValByKey(PaperVSRCAttr.FK_RC,value);
			}
		}
		/// <summary>
		///���
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSRCAttr.FK_Paper,value);
			}
		}
		#endregion

		#region ��չ����
//		/// <summary>
//		/// ������ϸ
//		/// </summary>
//		public RCs HisRCs
//		{
//			get
//			{
//				return new RCs(
//			}
//		}
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
		public PaperVSRC()
		{
		}
		public PaperVSRC(string paper,string Equestion)
		{
			this.FK_Paper = paper;
			this.FK_RC = Equestion;
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
				
				Map map = new Map("GTS_PaperVSRC");
				map.EnDesc="�Ķ���������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSRCAttr.FK_Paper,"0001","�Ծ�",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSRCAttr.FK_RC,null,"�Ķ���",new RCs(),false);
				//map.AddTBInt(PaperVSRCAttr.Cent,1,"��",true,false);
				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region ���ػ��෽��

		protected override void afterInsert()
		{
			RC rc = new RC(this.FK_RC);
			RCDtls rcs = rc.HisRCDtls;
			foreach(RCDtl dtl in rcs)
			{
				PaperVSRCDtl en = new PaperVSRCDtl();
				en.Cent=0;

				QueryObject qo = new QueryObject(en);
				qo.AddWhere(PaperVSRCDtlAttr.FK_Paper, this.FK_Paper);
				qo.addAnd();
				qo.AddWhere(PaperVSRCDtlAttr.FK_RCDtl,dtl.OID);
				if (qo.DoQuery()==1)
				{
					continue;
				}
				else
				{
					en.FK_Paper=this.FK_Paper;
					en.FK_RCDtl= dtl.OID;
					en.FK_RC=this.FK_RC;
					en.Insert();
				}
				 
			}

			base.afterInsert ();

		}
		protected override bool beforeDelete()
		{
			DBAccess.RunSQL("delete GTS_PaperVSRCDtl where fk_paper='"+this.FK_Paper+"' and fk_rc='"+this.FK_RC+"'");
			return base.beforeDelete ();
		}
		#endregion 
	
	}
	/// <summary>
	/// �Ծ��Ķ������ 
	/// </summary>
	public class PaperVSRCs : Entities
	{
		#region ����
		/// <summary>
		/// �Ծ��Ķ������
		/// </summary>
		public PaperVSRCs(){}

		/// <summary>
		///  �Ծ��Ķ������
		/// </summary>
		/// <param name="fk_paper"></param>
		public PaperVSRCs(string fk_paper)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere( PaperVSRCAttr.FK_Paper, fk_paper);
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
				return new PaperVSRC();
			}
		}	
		#endregion 

		#region ��ѯ����
		public RCs GetRCs(string fk_paper)
		{

			RCs rcs = new RCs();
			QueryObject qo =new QueryObject(rcs);
			qo.AddWhereInSQL( RCAttr.No , "SELECT FK_RC FROM GTS_PaperVSRCs  WHERE FK_Paper='"+fk_paper+"'");

			qo.DoQuery();
			
			return rcs;

		}
		 
		#endregion
	}
	
}

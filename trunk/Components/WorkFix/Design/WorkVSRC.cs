using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// �Ծ��Ķ������
	/// </summary>
	public class WorkVSRCAttr  :WorkVSBaseAttr
	{
		#region ��������
		/// <summary>
		/// �Ķ���
		/// </summary>
		public const  string FK_RC="FK_RC";
		#endregion	
	}
	/// <summary>
	/// �Ծ��Ķ������ ��ժҪ˵��
	/// </summary>
	public class WorkVSRC :WorkVSBase
	{
		#region ��������
		/// <summary>
		///�Ķ���
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(WorkVSRCAttr.FK_RC);
			}
			set
			{
				SetValByKey(WorkVSRCAttr.FK_RC,value);
			}
		}
		#endregion

		 

		#region ���캯��
		/// <summary>
		/// �Ķ���������
		/// </summary> 
		public WorkVSRC()
		{
		}
		public WorkVSRC(string Work,string Equestion)
		{
			this.FK_Work = Work;
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
				
				Map map = new Map("GTS_WorkVSRC");
				map.EnDesc="�Ķ���������";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSRCAttr.FK_Work,"0001","�Ծ�",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkVSRCAttr.FK_RC,null,"�Ķ���",new RCs(),false);
				//map.AddTBInt(WorkVSRCAttr.Cent,1,"��",true,false);
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
				WorkVSRCDtl en = new WorkVSRCDtl();
				en.Cent=0;

				QueryObject qo = new QueryObject(en);
				qo.AddWhere(WorkVSRCDtlAttr.FK_Work, this.FK_Work);
				qo.addAnd();
				qo.AddWhere(WorkVSRCDtlAttr.FK_RCDtl,dtl.OID);
				if (qo.DoQuery()==1)
				{
					continue;
				}
				else
				{
					en.FK_Work=this.FK_Work;
					en.FK_RCDtl= dtl.OID;
					en.FK_RC=this.FK_RC;
					en.Insert();
				}
			}

			base.afterInsert ();

		}
		protected override bool beforeDelete()
		{
			DBAccess.RunSQL("delete GTS_WorkVSRCDtl where fk_Work='"+this.FK_Work+"' and fk_rc='"+this.FK_RC+"'");
			return base.beforeDelete ();
		}
		#endregion 
	
	}
	/// <summary>
	/// �Ծ��Ķ������ 
	/// </summary>
	public class WorkVSRCs : WorkVSBases
	{
		#region ����
		/// <summary>
		/// �Ծ��Ķ������
		/// </summary>
		public WorkVSRCs(){}

		/// <summary>
		///  �Ծ��Ķ������
		/// </summary>
		/// <param name="fk_Work"></param>
		public WorkVSRCs(string fk_Work)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere( WorkVSRCAttr.FK_Work, fk_Work);
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
				return new WorkVSRC();
			}
		}	
		#endregion 

		#region ��ѯ����
		public RCs GetRCs(string fk_Work)
		{

			RCs rcs = new RCs();
			QueryObject qo =new QueryObject(rcs);
			qo.AddWhereInSQL( RCAttr.No , "SELECT FK_RC FROM GTS_WorkVSRCs  WHERE FK_Work='"+fk_Work+"'");
			qo.DoQuery();
			return rcs;
		}
		#endregion
	}
	
}
